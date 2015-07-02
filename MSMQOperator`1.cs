using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Messaging;
using TY.DatabaseOperation;

namespace TY
{
  public class MSMQOperator<T> : IDisposable
  {
    private static SortedList<string, MessageQueue> MqList = new SortedList<string, MessageQueue>();
    private string queueName = string.Empty;

    public MSMQOperator()
    {
    }

    public MSMQOperator(string queueName)
    {
      this.queueName = queueName;
    }

    public void Send(T bodyType, int priority)
    {
      if (string.IsNullOrEmpty(this.queueName))
        throw new Exception("MSMQOperator.queueName is Empty.");
      this.Send(bodyType, this.queueName, (MessagePriority) priority);
    }

    public void Send(T bodyType, string queueName, int priority)
    {
      if (string.IsNullOrEmpty(queueName))
        throw new Exception("MSMQOperator.queueName is Empty.");
      this.Send(bodyType, queueName, (MessagePriority) priority);
    }

    public void Send(T bodyType)
    {
      if (string.IsNullOrEmpty(this.queueName))
        throw new Exception("MSMQOperator.queueName is Empty.");
      this.Send(bodyType, this.queueName);
    }

    public void Send(T bodyType, string queueName)
    {
      this.Send(bodyType, queueName, MessagePriority.Normal);
    }

    public void Send(T bodyType, string queueName, MessagePriority priority)
    {
      MessageQueue messageQueue = (MessageQueue) null;
      try
      {
        string key = Convert.ToString(queueName).ToLower();
        lock (MSMQOperator<T>.MqList)
        {
          if (MSMQOperator<T>.MqList.ContainsKey(key))
            messageQueue = MSMQOperator<T>.MqList[key];
          if (null == messageQueue)
          {
            messageQueue = new MessageQueue(string.Format(".\\private$\\{0}", (object) key));
            messageQueue.Formatter = (IMessageFormatter) new XmlMessageFormatter(new Type[1]
            {
              typeof (T)
            });
            MSMQOperator<T>.MqList.Add(key, messageQueue);
          }
        }
        messageQueue.Send((object) new Message()
        {
          Body = (object) bodyType,
          Label = ((IMQEntity) (object) bodyType).ID,
          Priority = priority
        });
      }
      catch (Exception ex)
      {
        Debug.WriteLine("MSMQOperator::Send throw error " + ex.Message);
        throw ex;
      }
    }

    public T Get()
    {
      if (string.IsNullOrEmpty(this.queueName))
        throw new Exception("MSMQOperator.queueName is Empty.");
      else
        return this.Get(this.queueName);
    }

    public T Get(string queueName)
    {
      MessageQueue messageQueue = (MessageQueue) null;
      try
      {
        string key = Convert.ToString(queueName).ToLower();
        lock (MSMQOperator<T>.MqList)
        {
          if (MSMQOperator<T>.MqList.ContainsKey(key))
            messageQueue = MSMQOperator<T>.MqList[key];
          if (null == messageQueue)
          {
            messageQueue = new MessageQueue(string.Format(".\\private$\\{0}", (object) key));
            MSMQOperator<T>.MqList.Add(key, messageQueue);
          }
          messageQueue.Formatter = (IMessageFormatter) new XmlMessageFormatter(new Type[1]
          {
            typeof (T)
          });
        }
        Message message = messageQueue.Receive();
        if (null != message)
          return (T) message.Body;
        else
          return default (T);
      }
      catch (Exception ex)
      {
        Debug.WriteLine("MSMQOperator::Get throw error " + ex.Message);
        throw ex;
      }
    }

    void IDisposable.Dispose()
    {
      lock (MSMQOperator<T>.MqList)
      {
        foreach (MessageQueue item_0 in (IEnumerable<MessageQueue>) MSMQOperator<T>.MqList.Values)
        {
          try
          {
            if (null != item_0)
            {
              item_0.Close();
              item_0.Dispose();
            }
          }
          catch (Exception exception_0)
          {
            Debug.WriteLine("MSMQOperator::IDisposable" + exception_0.Message);
          }
        }
        MSMQOperator<T>.MqList.Clear();
      }
      GC.Collect();
    }
  }
}
