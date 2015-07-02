using System.Collections.Generic;

namespace TY
{
  public struct Email
  {
    public string FromName;
    public string From;
    public string ServerUserName;
    public string ServerPassWord;
    public List<string> SendToList;
    public string Title;
    public string Body;
    public string MailServer;
  }
}
