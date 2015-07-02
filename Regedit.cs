using Microsoft.Win32;
using System;

namespace TY
{
  public class Regedit
  {
    private static RegistryKey LoadRoot(string path, bool writable)
    {
      try
      {
        RegistryKey registryKey = Regedit.LoadRoot(writable).OpenSubKey(path, writable);
        if (null == registryKey)
          registryKey = Registry.LocalMachine.CreateSubKey(path);
        return registryKey;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private static RegistryKey LoadRoot(bool writable)
    {
      RegistryKey registryKey1 = (RegistryKey) null;
      try
      {
        registryKey1 = Registry.LocalMachine.OpenSubKey("Software", writable);
        RegistryKey registryKey2 = registryKey1.OpenSubKey("YYBB", writable);
        if (null == registryKey2)
          registryKey2 = registryKey1.CreateSubKey("YYBB");
        return registryKey2;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        if (null != registryKey1)
          registryKey1.Close();
      }
    }

    private static RegistryKey LoadRegistryKey(bool writable)
    {
      RegistryKey registryKey1 = (RegistryKey) null;
      try
      {
        registryKey1 = Registry.LocalMachine.OpenSubKey("Software", writable);
        RegistryKey registryKey2 = registryKey1.OpenSubKey("YYBB", writable);
        if (null == registryKey2)
          registryKey2 = registryKey1.CreateSubKey("YYBB");
        RegistryKey registryKey3 = registryKey2.OpenSubKey("connectionString", writable);
        if (null == registryKey3)
          registryKey3 = registryKey2.CreateSubKey("connectionString");
        return registryKey3;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        if (null != registryKey1)
          registryKey1.Close();
      }
    }

    public static RegistryKey AddFolder(string itemName)
    {
      RegistryKey registryKey = (RegistryKey) null;
      try
      {
        registryKey = Regedit.LoadRoot(true);
        return registryKey.CreateSubKey(itemName);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        if (null != registryKey)
          registryKey.Close();
      }
    }

    public static bool SetFolderValueX(string path, string name, string val)
    {
      RegistryKey registryKey = (RegistryKey) null;
      try
      {
        registryKey = Regedit.LoadRoot(path, true);
        registryKey.SetValue(name, (object) val);
        registryKey.Flush();
        return true;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        if (null != registryKey)
          registryKey.Close();
      }
    }

    public static bool SetFolderValue(string itemName, string name, string itemValue)
    {
      RegistryKey registryKey = (RegistryKey) null;
      try
      {
        registryKey = Regedit.AddFolder(itemName);
        registryKey.SetValue(name, (object) itemValue);
        registryKey.Flush();
        return true;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        if (null != registryKey)
          registryKey.Close();
      }
    }

    public static bool DeleteFolder(string itemName)
    {
      RegistryKey registryKey = (RegistryKey) null;
      try
      {
        registryKey = Regedit.LoadRoot(true);
        registryKey.DeleteSubKey(itemName);
        return true;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        if (null != registryKey)
          registryKey.Close();
      }
    }

    public static bool DeleteFolder(string path, string itemName)
    {
      RegistryKey registryKey = (RegistryKey) null;
      try
      {
        registryKey = Regedit.LoadRoot(path, true);
        registryKey.DeleteSubKey(itemName);
        registryKey.Flush();
        return true;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        if (null != registryKey)
          registryKey.Close();
      }
    }

    public static bool DeleteFolderValue(string itemName, string name)
    {
      RegistryKey registryKey = (RegistryKey) null;
      try
      {
        registryKey = Regedit.LoadRoot(true);
        registryKey = Regedit.LoadRoot(true);
        registryKey.OpenSubKey(itemName, true).DeleteValue(name);
        return true;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        if (null != registryKey)
          registryKey.Close();
      }
    }

    public static RegistryKey GetFolder(string itemName)
    {
      RegistryKey registryKey1 = (RegistryKey) null;
      try
      {
        registryKey1 = Regedit.LoadRoot(false);
        RegistryKey registryKey2 = registryKey1.OpenSubKey(itemName, false);
        if (null == registryKey2)
          throw new Exception("不存在[" + itemName + "]的项...");
        else
          return registryKey2;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        if (null != registryKey1)
          registryKey1.Close();
      }
    }

    public static string[] GetFolderNames(string itemName)
    {
      RegistryKey registryKey1 = (RegistryKey) null;
      try
      {
        registryKey1 = Regedit.LoadRoot(false);
        RegistryKey registryKey2 = registryKey1.OpenSubKey(itemName, false);
        if (null == registryKey2)
          return (string[]) null;
        else
          return registryKey2.GetValueNames();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        if (null != registryKey1)
          registryKey1.Close();
      }
    }

    public static string GetFolderValueX(string path, string name)
    {
      RegistryKey registryKey = (RegistryKey) null;
      try
      {
        registryKey = Regedit.LoadRoot(path, false);
        string str = string.Empty;
        object obj = registryKey.GetValue(name);
        if (null != obj)
          return obj.ToString();
        else
          return (string) null;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        if (null != registryKey)
          registryKey.Close();
      }
    }

    public static string GetFolderValue(string itemName, string name)
    {
      RegistryKey registryKey = (RegistryKey) null;
      try
      {
        registryKey = Regedit.GetFolder(itemName);
        string str = string.Empty;
        object obj = registryKey.GetValue(name);
        if (null != obj)
          return obj.ToString();
        else
          return (string) null;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        if (null != registryKey)
          registryKey.Close();
      }
    }

    public static RegistryKey CreateItem(string itemName)
    {
      RegistryKey registryKey = (RegistryKey) null;
      try
      {
        registryKey = Regedit.LoadRegistryKey(true);
        return registryKey.CreateSubKey(itemName);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        if (null != registryKey)
          registryKey.Close();
      }
    }

    public static RegistryKey CreateItem(string path, string itemName)
    {
      RegistryKey registryKey = (RegistryKey) null;
      try
      {
        registryKey = Regedit.AddFolder(path);
        return registryKey.CreateSubKey(itemName);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        if (null != registryKey)
          registryKey.Close();
      }
    }

    public static bool DeleteItem(string itemName)
    {
      RegistryKey registryKey = (RegistryKey) null;
      try
      {
        registryKey = Regedit.LoadRegistryKey(true);
        registryKey.DeleteSubKey(itemName);
        return true;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        if (null != registryKey)
          registryKey.Close();
      }
    }

    public static RegistryKey GetItem(string itemName)
    {
      RegistryKey registryKey1 = (RegistryKey) null;
      try
      {
        registryKey1 = Regedit.LoadRegistryKey(false);
        RegistryKey registryKey2 = registryKey1.OpenSubKey(itemName, false);
        if (null == registryKey2)
          throw new Exception("不存在[" + itemName + "]的项...");
        else
          return registryKey2;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        if (null != registryKey1)
          registryKey1.Close();
      }
    }

    public static bool SetConnectionString(string name, string connectionString)
    {
      return Regedit.SetValue(name, "connectionString", connectionString);
    }

    public static bool SetValue(string itemName, string name, string itemValue)
    {
      RegistryKey registryKey = (RegistryKey) null;
      try
      {
        registryKey = Regedit.CreateItem(itemName);
        registryKey.SetValue(name, (object) Encrypt.EncryptStr(itemValue, "fengyi", "fengyibi"));
        registryKey.Flush();
        return true;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        if (null != registryKey)
          registryKey.Close();
      }
    }

    public static string GetConnectionString(string serverName)
    {
      foreach (string str in Regedit.GetConnectionLists())
      {
        if (str == serverName)
          return Regedit.GetValue(serverName, "connectionString", false);
      }
      throw new Exception("不存在配置名为[" + serverName + "]的设置,请检查!");
    }

    public static string GetValue(string itemName, string name)
    {
      return Regedit.GetValue(itemName, name, true);
    }

    public static string GetValue(string itemName, string name, bool isDecrpt)
    {
      RegistryKey registryKey = (RegistryKey) null;
      try
      {
        registryKey = Regedit.GetItem(itemName);
        string str1 = string.Empty;
        object obj = registryKey.GetValue(name);
        if (null == obj)
          throw new Exception(itemName + "中不存在[" + name + "]项！");
        string str2 = obj.ToString();
        if (isDecrpt)
          return Encrypt.DecryptStr(str2, "fengyi", "fengyibi");
        else
          return str2;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        if (null != registryKey)
          registryKey.Close();
      }
    }

    public static string[] GetConnectionLists()
    {
      RegistryKey registryKey = (RegistryKey) null;
      try
      {
        registryKey = Regedit.LoadRegistryKey(false);
        return registryKey.GetSubKeyNames();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        if (null != registryKey)
          registryKey.Close();
      }
    }
  }
}
