using System;
using System.Diagnostics;

namespace TY
{
  public class WriteLog
  {
    private string _eventName = "服务日志";
    private string _sourece = "GanStaticFile";

    public WriteLog()
    {
    }

    public WriteLog(string eventName, int saveDay, string sourece)
    {
      this._eventName = eventName;
      this._sourece = sourece;
    }

    public void WriteEvent(string strMessage, EventLogEntryType type)
    {
      if (!EventLog.SourceExists(this._sourece))
        EventLog.CreateEventSource(this._sourece, this._eventName);
      EventLog eventLog = new EventLog();
      eventLog.Log = this._eventName;
      eventLog.Source = this._sourece;
      try
      {
        eventLog.WriteEntry(strMessage, type);
      }
      catch (Exception ex)
      {
        eventLog.Clear();
        this.WriteEvent("日志文件已满,执行清除操作!", EventLogEntryType.Warning);
        eventLog.WriteEntry(strMessage, type);
      }
      finally
      {
        eventLog.Close();
        eventLog.Dispose();
      }
    }

    public void WriteEvent(string strMessage)
    {
      this.WriteEvent(strMessage, EventLogEntryType.Information);
    }

    public void WriteEvent(Exception ex)
    {
      this.WriteEvent(ex.Message, EventLogEntryType.Error);
    }

    public void WriteEvent(Exception ex, string strMessage)
    {
      this.WriteEvent(strMessage + "\r\n" + ex.Message, EventLogEntryType.Error);
    }

    public void WriteLine(string message)
    {
      Debug.WriteLine(message);
    }

    public void WriteLine(string baseStr, params string[] list)
    {
      this.WriteLine(string.Format(baseStr, (object[]) list));
    }

    public void WriteLine(string baseStr, params object[] list)
    {
      this.WriteLine(string.Format(baseStr, list));
    }
  }
}
