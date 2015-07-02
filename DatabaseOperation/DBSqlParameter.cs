// Decompiled with JetBrains decompiler
// Type: TY.DatabaseOperation.DBSqlParameter
// Assembly: TY.DataOperator, Version=1.0.4391.21002, Culture=neutral, PublicKeyToken=null
// MVID: 84D45891-4911-4E19-B0F7-4AAE0BD7FFAF
// Assembly location: E:\study\TY.DataOperator.dll

using System;

namespace TY.DatabaseOperation
{
  [Serializable]
  public class DBSqlParameter
  {
    public string Name;
    public string Val;
    public DataType DBType;
    public int Size;

    public DBSqlParameter()
    {
    }

    public DBSqlParameter(string name, string val, DataType dataType, int size)
    {
      this.Name = name;
      this.Val = val;
      this.DBType = dataType;
      this.Size = size;
    }
  }
}
