using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace TY.DatabaseOperation
{
  [Serializable]
  public class ParamCollections
  {
    private List<SqlParameter> param = new List<SqlParameter>();
    private List<DBSqlParameter> dbParam = new List<DBSqlParameter>();

    public ParamCollections()
    {
    }

    public ParamCollections(List<DBSqlParameter> list)
    {
      for (int index = 0; index < list.Count; ++index)
        this.Add(list[index].Name, list[index].Val, this.convertDataType(list[index].DBType), list[index].Size, ParameterDirection.Input);
    }

    public void Add(string name, object val)
    {
      this.Add(name, Convert.ToString(val));
    }

    public void Add(string name, object val, SqlDbType type)
    {
      this.Add(name, Convert.ToString(val), type);
    }

    public void Add(string name, object val, SqlDbType type, ParameterDirection direction)
    {
      this.Add(name, Convert.ToString(val), type, direction);
    }

    public void Add(string name, object val, SqlDbType type, int size, ParameterDirection direction)
    {
      this.Add(name, Convert.ToString(val), type, size, direction);
    }

    public void Add(string name, string val)
    {
      this.Add(name, val, SqlDbType.VarChar);
    }

    public void Add(string name, string val, SqlDbType type)
    {
      this.Add(name, val, type, this.getDefaultSize(type), ParameterDirection.Input);
    }

    public void Add(string name, string val, SqlDbType type, ParameterDirection direction)
    {
      this.Add(name, val, type, this.getDefaultSize(type), direction);
    }

    public void Add(string name, string val, SqlDbType type, int size, ParameterDirection direction)
    {
      this.param.Add(new SqlParameter(name, type, size, direction, true, (byte) 0, (byte) 0, name, DataRowVersion.Default, (object) val));
      this.dbParam.Add(new DBSqlParameter(name, val, this.convertDataType(type), size));
    }

    public void Add(string name, SqlDbType type, int size, string sourceName)
    {
      this.param.Add(new SqlParameter(name, type, size, sourceName));
    }

    public SqlParameter[] GetParams()
    {
      return this.GetParams(false);
    }

    public SqlParameter[] GetParams(bool clear)
    {
      SqlParameter[] sqlParameterArray = new SqlParameter[this.param.Count];
      for (int index = 0; index < sqlParameterArray.Length; ++index)
        sqlParameterArray[index] = this.param[index];
      if (clear)
      {
        this.param.Clear();
        this.dbParam.Clear();
      }
      return sqlParameterArray;
    }

    public List<DBSqlParameter> GetDBParams()
    {
      return this.dbParam;
    }

    private int getDefaultSize(SqlDbType type)
    {
      int num = 0;
      switch (type)
      {
        case SqlDbType.Int:
          num = 0;
          break;
        case SqlDbType.VarChar:
          num = 1024;
          break;
      }
      return num;
    }

    private DataType convertDataType(SqlDbType type)
    {
      switch (type)
      {
        case SqlDbType.Int:
          return DataType.Int;
        case SqlDbType.VarChar:
          return DataType.VarChar;
        default:
          return DataType.VarChar;
      }
    }

    private SqlDbType convertDataType(DataType type)
    {
      switch (type)
      {
        case DataType.Int:
          return SqlDbType.Int;
        case DataType.VarChar:
          return SqlDbType.VarChar;
        default:
          return SqlDbType.VarChar;
      }
    }
  }
}
