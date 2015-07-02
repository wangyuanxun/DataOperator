using System;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace TY
{
  public class EmailTools
  {
    public bool SendTo(Email email)
    {
      try
      {
        SmtpClient smtpClient = new SmtpClient(email.MailServer);
        smtpClient.UseDefaultCredentials = true;
        smtpClient.Credentials = (ICredentialsByHost) new NetworkCredential(email.ServerUserName, email.ServerPassWord);
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        MailMessage message = new MailMessage();
        message.From = new MailAddress(email.From, email.FromName);
        message.Subject = email.Title;
        message.BodyEncoding = Encoding.Default;
        message.Body = email.Body;
        if (email.SendToList == null)
          return false;
        foreach (string addresses in email.SendToList)
          message.To.Add(addresses);
        message.IsBodyHtml = true;
        smtpClient.Timeout = 600000;
        smtpClient.Send(message);
        return true;
      }
      catch (Exception ex)
      {
        Debug.WriteLine("EmailTools::SendTo " + ex.StackTrace);
      }
      return false;
    }
  }
}
