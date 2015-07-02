using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.IO;

namespace TY
{
  public class UnZipTools
  {
    public static void UnZipFile(string[] args)
    {
      ZipInputStream zipInputStream = new ZipInputStream((Stream) File.OpenRead(args[0].Trim()));
      string directoryName = Path.GetDirectoryName(args[1].Trim());
      if (!Directory.Exists(args[1].Trim()))
        Directory.CreateDirectory(directoryName);
      ZipEntry nextEntry;
      while ((nextEntry = zipInputStream.GetNextEntry()) != null)
      {
        string fileName = Path.GetFileName(nextEntry.Name);
        if (fileName != string.Empty)
        {
          FileStream fileStream = File.Create(args[1].Trim() + fileName);
          byte[] buffer = new byte[2048];
          while (true)
          {
            int count = zipInputStream.Read(buffer, 0, buffer.Length);
            if (count > 0)
              fileStream.Write(buffer, 0, count);
            else
              break;
          }
          fileStream.Close();
        }
      }
      zipInputStream.Close();
    }

    public static void ZipFileDictory(string[] args)
    {
      string[] files = Directory.GetFiles(args[0]);
      Crc32 crc32 = new Crc32();
      ZipOutputStream zipOutputStream = new ZipOutputStream((Stream) File.Create(args[1]));
      zipOutputStream.SetLevel(6);
      foreach (string str in files)
      {
        FileStream fileStream = File.OpenRead(str);
        byte[] buffer = new byte[fileStream.Length];
        fileStream.Read(buffer, 0, buffer.Length);
        ZipEntry entry = new ZipEntry(str);
        entry.DateTime = DateTime.Now;
        entry.Size = fileStream.Length;
        fileStream.Close();
        crc32.Reset();
        crc32.Update(buffer);
        entry.Crc = crc32.Value;
        zipOutputStream.PutNextEntry(entry);
        zipOutputStream.Write(buffer, 0, buffer.Length);
      }
      zipOutputStream.Finish();
      zipOutputStream.Close();
    }
  }
}
