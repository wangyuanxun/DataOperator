using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace TY
{
  public class Encrypt
  {
    private static RijndaelManaged RMCrypto = (RijndaelManaged) null;
    private Hashtable keyList = new Hashtable();

    public static string EncryptStr(string str, string strKey, string strIV)
    {
      CryptoStream cryptoStream = (CryptoStream) null;
      MD5CryptoServiceProvider cryptoServiceProvider = new MD5CryptoServiceProvider();
      try
      {
        Encrypt.RMCrypto = new RijndaelManaged();
        if (str == null || str.Trim().Length == 0)
          return "";
        byte[] hash1 = cryptoServiceProvider.ComputeHash(Encoding.ASCII.GetBytes(strKey));
        byte[] hash2 = cryptoServiceProvider.ComputeHash(Encoding.ASCII.GetBytes(strIV));
        MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(str));
        cryptoStream = new CryptoStream((Stream) memoryStream, Encrypt.RMCrypto.CreateEncryptor(hash1, hash2), CryptoStreamMode.Write);
        return Convert.ToBase64String(memoryStream.ToArray());
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        if (null != cryptoStream)
          ;
        if (null != Encrypt.RMCrypto)
        {
          Encrypt.RMCrypto.Clear();
          Encrypt.RMCrypto = (RijndaelManaged) null;
        }
      }
    }

    public static string DecryptStr(string str, string strKey, string strIV)
    {
      if (str == null || str.Trim().Length == 0)
        return "";
      CryptoStream cryptoStream = (CryptoStream) null;
      MD5CryptoServiceProvider cryptoServiceProvider = new MD5CryptoServiceProvider();
      try
      {
        Encrypt.RMCrypto = new RijndaelManaged();
        byte[] buffer = Convert.FromBase64String(str);
        byte[] hash1 = cryptoServiceProvider.ComputeHash(Encoding.ASCII.GetBytes(strKey));
        byte[] hash2 = cryptoServiceProvider.ComputeHash(Encoding.ASCII.GetBytes(strIV));
        MemoryStream memoryStream = new MemoryStream(buffer);
        cryptoStream = new CryptoStream((Stream) memoryStream, Encrypt.RMCrypto.CreateDecryptor(hash1, hash2), CryptoStreamMode.Read);
        return Encoding.UTF8.GetString(memoryStream.ToArray());
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        if (null != cryptoStream)
          ;
        if (null != Encrypt.RMCrypto)
        {
          Encrypt.RMCrypto.Clear();
          Encrypt.RMCrypto = (RijndaelManaged) null;
        }
      }
    }

    public void Add(string key, string value)
    {
      if (this.keyList.ContainsKey((object) key))
        this.keyList[(object) key] = (object) value;
      else
        this.keyList.Add((object) key, (object) value);
    }

    public string GetEncryptXml(string name, string key, string iv)
    {
      XmlDocument xmlDocument = new XmlDocument();
      XmlElement element1 = xmlDocument.CreateElement(name);
      foreach (object index in (IEnumerable) this.keyList.Keys)
      {
        XmlElement element2 = xmlDocument.CreateElement(Convert.ToString(index));
        element2.InnerText = Convert.ToString(this.keyList[index]);
        element1.AppendChild((XmlNode) element2);
      }
      xmlDocument.AppendChild((XmlNode) element1);
      return Encrypt.EncryptStr(xmlDocument.OuterXml, key, iv);
    }

    public Hashtable GetKeyList(string name, string encryptStr, string key, string iv)
    {
      string xml = Encrypt.DecryptStr(encryptStr, key, iv);
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.LoadXml(xml);
      this.keyList.Clear();
      XmlNode xmlNode1 = xmlDocument.SelectSingleNode(name);
      if (null != xmlNode1)
      {
        foreach (XmlNode xmlNode2 in xmlNode1.ChildNodes)
          this.keyList.Add((object) xmlNode2.Name, (object) xmlNode2.InnerText);
      }
      return this.keyList;
    }
  }
}
