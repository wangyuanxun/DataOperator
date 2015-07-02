using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using TY;

namespace TY.DatabaseOperation
{
  public class DataOperator : IDisposable
  {
    private static string connectionString = string.Empty;
    private static string connectionName = string.Empty;
    private static string key = "fengyi";
    private static string iv = "fengyibi";
    private SqlConnection _conn = (SqlConnection) null;
    private SqlCommand _cmd = (SqlCommand) null;
    private SqlDataAdapter _adp = (SqlDataAdapter) null;
    private static string uid;
    public static string password;

    public static string ConnectionName
    {
      get
      {
        return DataOperator.connectionName;
      }
    }

    public static string Key
    {
      get
      {
        return DataOperator.key;
      }
      set
      {
        DataOperator.key = value;
      }
    }

    public static string IV
    {
      get
      {
        return DataOperator.iv;
      }
      set
      {
        DataOperator.iv = value;
      }
    }

    public SqlConnection Connection
    {
      get
      {
        return this._conn;
      }
    }

    public string UId
    {
      get
      {
        return DataOperator.uid;
      }
    }

    public string Password
    {
      get
      {
        return DataOperator.password;
      }
    }

    public DataOperator()
    {
      if (DataOperator.connectionString == "")
        throw new ArgumentNullException("ConnectionString");
    }

    public DataOperator(string server, string puid, string pwd, string dbname)
    {
      try
      {
        if (server == null || server.Trim().Length == 0)
          throw new ArgumentNullException("Type:string pramName:server", "数据库服务器为空！");
        if (DataOperator.uid == null || DataOperator.uid.Trim().Length == 0)
          throw new ArgumentNullException("Type:string pramName:uid", "数据库用户名为空！");
        if (dbname == null || dbname.Trim().Length == 0)
          throw new ArgumentNullException("Type:string pramName:dbname", "数据库名为空！");
        DataOperator.uid = puid;
        DataOperator.password = pwd;
        this._conn = new SqlConnection(string.Format("server={0};uid={1};pwd={2};database={3};", (object) server, (object) DataOperator.uid, (object) pwd, (object) dbname));
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public bool getTable(string sqlstr, ref DataTable rdt)
    {
      try
      {
        if (null == rdt)
          rdt = new DataTable();
        else
          rdt.Clear();
        if (null == this._conn)
          this._conn = new SqlConnection(DataOperator.connectionString);
        this._adp = new SqlDataAdapter(sqlstr, this._conn);
        this._adp.Fill(rdt);
        return true;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        if (null != this._adp)
        {
          this._adp.Dispose();
          this._adp = (SqlDataAdapter) null;
        }
      }
    }

    public DataTable getTable(string sqlstr)
    {
      try
      {
        DataTable dataTable = new DataTable();
        if (null == this._conn)
          this._conn = new SqlConnection(DataOperator.connectionString);
        this._adp = new SqlDataAdapter(sqlstr, this._conn);
        this._adp.Fill(dataTable);
        return dataTable;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        if (null != this._adp)
        {
          this._adp.Dispose();
          this._adp = (SqlDataAdapter) null;
        }
      }
    }

    public DataTable GetTable(string sqlstr)
    {
      try
      {
        DataTable dataTable = new DataTable();
        if (null == this._conn)
          this._conn = new SqlConnection(DataOperator.connectionString);
        this._adp = new SqlDataAdapter(sqlstr, this._conn);
        this._adp.Fill(dataTable);
        return dataTable;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        if (null != this._adp)
        {
          this._adp.Dispose();
          this._adp = (SqlDataAdapter) null;
        }
      }
    }

    public bool getTable(string sqlstr, SqlParameter[] param, ref DataTable rdt)
    {
      try
      {
        if (null == rdt)
          rdt = new DataTable();
        else
          rdt.Clear();
        if (null == this._conn)
          this._conn = new SqlConnection(DataOperator.connectionString);
        this._adp = new SqlDataAdapter(sqlstr, this._conn);
        for (int index = 0; index < param.Length; ++index)
          this._adp.SelectCommand.Parameters.Add((SqlParameter) ((ICloneable) param[index]).Clone());
        this._adp.Fill(rdt);
        return true;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        if (null != this._adp)
        {
          this._adp.Dispose();
          this._adp = (SqlDataAdapter) null;
        }
      }
    }

    public DataTable GetTable(string sqlstr, SqlParameter[] param)
    {
      try
      {
        DataTable dataTable = new DataTable();
        if (null == this._conn)
          this._conn = new SqlConnection(DataOperator.connectionString);
        this._adp = new SqlDataAdapter(sqlstr, this._conn);
        for (int index = 0; index < param.Length; ++index)
          this._adp.SelectCommand.Parameters.Add((SqlParameter) ((ICloneable) param[index]).Clone());
        this._adp.Fill(dataTable);
        return dataTable;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        if (null != this._adp)
        {
          this._adp.Dispose();
          this._adp = (SqlDataAdapter) null;
        }
      }
    }

    public DataTable getTable(string sqlstr, SqlParameter[] param)
    {
      try
      {
        DataTable dataTable = new DataTable();
        if (null == this._conn)
          this._conn = new SqlConnection(DataOperator.connectionString);
        this._adp = new SqlDataAdapter(sqlstr, this._conn);
        for (int index = 0; index < param.Length; ++index)
          this._adp.SelectCommand.Parameters.Add((SqlParameter) ((ICloneable) param[index]).Clone());
        this._adp.Fill(dataTable);
        return dataTable;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        if (null != this._adp)
        {
          this._adp.Dispose();
          this._adp = (SqlDataAdapter) null;
        }
      }
    }

    public object getObject(string sqlstr)
    {
      try
      {
        if (sqlstr == null || sqlstr.Trim().Length == 0)
          throw new ArgumentNullException("sqlstr", "SQL语句不能为空！");
        if (null == this._conn)
          this._conn = new SqlConnection(DataOperator.connectionString);
        this._cmd = new SqlCommand(sqlstr, this._conn);
        if (ConnectionState.Closed == this._conn.State)
          this._conn.Open();
        return this._cmd.ExecuteScalar();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        if (null != this._cmd)
        {
          this._cmd.Dispose();
          this._cmd = (SqlCommand) null;
        }
      }
    }

    public object getObject(string sqlstr, SqlParameter[] param)
    {
      try
      {
        if (sqlstr == null || sqlstr.Trim().Length == 0)
          throw new ArgumentNullException("sqlstr", "SQL语句不能为空！");
        if (null == this._conn)
          this._conn = new SqlConnection(DataOperator.connectionString);
        this._cmd = new SqlCommand(sqlstr, this._conn);
        this._cmd.Parameters.Clear();
        for (int index = 0; index < param.Length; ++index)
          this._cmd.Parameters.Add((SqlParameter) ((ICloneable) param[index]).Clone());
        if (ConnectionState.Closed == this._conn.State)
          this._conn.Open();
        return this._cmd.ExecuteScalar();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        if (null != this._cmd)
        {
          this._cmd.Dispose();
          this._cmd = (SqlCommand) null;
        }
      }
    }

    public int getCount(string sqlstr)
    {
      try
      {
        if (sqlstr == null || sqlstr.Trim().Length == 0)
          throw new ArgumentNullException("sqlstr", "SQL语句不能为空！");
        if (null == this._conn)
          this._conn = new SqlConnection(DataOperator.connectionString);
        if (ConnectionState.Closed == this._conn.State)
          this._conn.Open();
        this._cmd = new SqlCommand(sqlstr, this._conn);
        object obj = this._cmd.ExecuteScalar();
        if (null == obj)
          throw new ArgumentNullException("_cmd.ExecuteScalar()", "结果返回空！");
        else
          return Convert.ToInt32(obj);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        if (null != this._cmd)
        {
          this._cmd.Dispose();
          this._cmd = (SqlCommand) null;
        }
      }
    }

    public int getCount(string sqlstr, SqlParameter[] param)
    {
      try
      {
        if (sqlstr == null || sqlstr.Trim().Length == 0)
          throw new ArgumentNullException("sqlstr", "SQL语句不能为空！");
        if (null == this._conn)
          this._conn = new SqlConnection(DataOperator.connectionString);
        if (ConnectionState.Closed == this._conn.State)
          this._conn.Open();
        this._cmd = new SqlCommand(sqlstr, this._conn);
        this._cmd.Parameters.Clear();
        for (int index = 0; index < param.Length; ++index)
          this._cmd.Parameters.Add((SqlParameter) ((ICloneable) param[index]).Clone());
        object obj = this._cmd.ExecuteScalar();
        if (null == obj)
          throw new ArgumentNullException("_cmd.ExecuteScalar()", "结果返回空！");
        else
          return Convert.ToInt32(obj);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        if (null != this._cmd)
        {
          this._cmd.Dispose();
          this._cmd = (SqlCommand) null;
        }
      }
    }

    public bool executeSql(string sqlstr)
    {
      try
      {
        if (sqlstr == null || sqlstr.Trim().Length == 0)
          throw new ArgumentNullException("sqlstr", "SQL语句为空！");
        if (this._conn == null)
          this._conn = new SqlConnection(DataOperator.connectionString);
        this._cmd = new SqlCommand(sqlstr, this._conn);
        if (ConnectionState.Closed == this._conn.State)
          this._conn.Open();
        this._cmd.ExecuteNonQuery();
        return true;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        if (this._cmd != null)
        {
          this._cmd.Dispose();
          this._cmd = (SqlCommand) null;
        }
      }
    }

    public bool executeSql(string sqlstr, SqlParameter[] param)
    {
      try
      {
        if (sqlstr == null || sqlstr.Trim().Length == 0)
          throw new ArgumentNullException("sqlstr", "SQL语句为空！");
        if (this._conn == null)
          this._conn = new SqlConnection(DataOperator.connectionString);
        this._cmd = new SqlCommand(sqlstr, this._conn);
        this._cmd.Parameters.Clear();
        for (int index = 0; index < param.Length; ++index)
          this._cmd.Parameters.Add((SqlParameter) ((ICloneable) param[index]).Clone());
        if (ConnectionState.Closed == this._conn.State)
          this._conn.Open();
        this._cmd.ExecuteNonQuery();
        return true;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        if (this._cmd != null)
        {
          this._cmd.Dispose();
          this._cmd = (SqlCommand) null;
        }
      }
    }

    public bool executeSql(List<string> list)
    {
      string[] list1 = new string[list.Count];
      for (int index = 0; index < list.Count; ++index)
        list1[index] = list[index];
      return this.executeSql(list1);
    }

    public bool executeSql(string[] list)
    {
      SqlTransaction sqlTransaction = (SqlTransaction) null;
      try
      {
        if (this._conn == null)
          this._conn = new SqlConnection(DataOperator.connectionString);
        if (ConnectionState.Closed == this._conn.State)
          this._conn.Open();
        sqlTransaction = this._conn.BeginTransaction();
        foreach (string cmdText in list)
        {
          if (cmdText == null || cmdText.Trim().Length == 0)
            throw new ArgumentNullException("sqlstr", "SQL语句为空！");
          this._cmd = new SqlCommand(cmdText, this._conn);
          this._cmd.Transaction = sqlTransaction;
          this._cmd.ExecuteNonQuery();
        }
        sqlTransaction.Commit();
        return true;
      }
      catch (Exception ex)
      {
        if (null != sqlTransaction)
          ((DbTransaction) sqlTransaction).Rollback();
        throw ex;
      }
      finally
      {
        if (this._cmd != null)
        {
          this._cmd.Dispose();
          this._cmd = (SqlCommand) null;
        }
        if (null != sqlTransaction)
          sqlTransaction.Dispose();
      }
    }

    public bool executeSql(SQLExecEntity entity)
    {
      return this.executeSql(new List<SQLExecEntity>()
      {
        entity
      });
    }

    public bool executeSql(List<SQLExecEntity> list)
    {
      SqlTransaction sqlTransaction = (SqlTransaction) null;
      try
      {
        if (this._conn == null)
          this._conn = new SqlConnection(DataOperator.connectionString);
        if (ConnectionState.Closed == this._conn.State)
          this._conn.Open();
        sqlTransaction = this._conn.BeginTransaction();
        foreach (SQLExecEntity sqlExecEntity in list)
        {
          this._cmd = new SqlCommand(sqlExecEntity.SqlStr, this._conn);
          foreach (SqlParameter sqlParameter in new ParamCollections(sqlExecEntity.Param).GetParams())
            this._cmd.Parameters.Add(sqlParameter);
          this._cmd.Transaction = sqlTransaction;
          this._cmd.ExecuteNonQuery();
        }
        sqlTransaction.Commit();
        return true;
      }
      catch (Exception ex)
      {
        if (null != sqlTransaction)
          ((DbTransaction) sqlTransaction).Rollback();
        throw ex;
      }
      finally
      {
        if (this._cmd != null)
        {
          this._cmd.Dispose();
          this._cmd = (SqlCommand) null;
        }
        if (null != sqlTransaction)
          ;
      }
    }

    public bool executeProduct(string sqlstr, ref ArrayList outAl)
    {
      DataTable dataTable = new DataTable();
      try
      {
        if (sqlstr == null || sqlstr.Trim().Length == 0)
          throw new ArgumentNullException("sqlstr", "SQL不能为空！");
        if (null != outAl)
          outAl.Clear();
        else
          outAl = new ArrayList();
        if (this._conn == null)
          this._conn = new SqlConnection(DataOperator.connectionString);
        this._adp = new SqlDataAdapter(sqlstr, this._conn);
        this._adp.Fill(dataTable);
        if (null != dataTable)
        {
          for (int index = 0; index < dataTable.Columns.Count; ++index)
          {
            if (null != dataTable.Rows[0][index])
              outAl.Add(dataTable.Rows[0][index]);
          }
        }
        return true;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        if (null != this._adp)
        {
          this._adp.Dispose();
          this._adp = (SqlDataAdapter) null;
        }
        if (null != dataTable)
        {
          dataTable.Clear();
          dataTable.Dispose();
        }
      }
    }

    public ArrayList executeProduct(string produreName, SqlParameter[] param, bool returnDt)
    {
      DataTable dataTable = new DataTable();
      try
      {
        ArrayList arrayList = new ArrayList();
        if (produreName == null || produreName.Trim().Length == 0)
          throw new ArgumentNullException("produreName", "过程名不能为空！");
        if (this._conn == null)
          this._conn = new SqlConnection(DataOperator.connectionString);
        if (ConnectionState.Closed == this._conn.State)
          this._conn.Open();
        this._cmd = new SqlCommand();
        this._cmd.Connection = this._conn;
        this._cmd.CommandType = CommandType.StoredProcedure;
        this._cmd.CommandText = produreName;
        for (int index = 0; index < param.Length; ++index)
          this._cmd.Parameters.Add(param[index]);
        if (returnDt)
        {
          SqlDataReader sqlDataReader = this._cmd.ExecuteReader();
          for (int ordinal = 0; ordinal < sqlDataReader.FieldCount; ++ordinal)
            dataTable.Columns.Add(sqlDataReader.GetName(ordinal), sqlDataReader.GetFieldType(ordinal));
          while (sqlDataReader.Read())
          {
            DataRow row = dataTable.NewRow();
            row.BeginEdit();
            for (int ordinal = 0; ordinal < sqlDataReader.FieldCount; ++ordinal)
              row[ordinal] = sqlDataReader.GetValue(ordinal);
            row.EndEdit();
            dataTable.Rows.Add(row);
          }
          sqlDataReader.Close();
          arrayList.Add((object) dataTable);
        }
        else
        {
          object obj = this._cmd.ExecuteScalar();
          arrayList.Add(obj);
        }
        return arrayList;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public DataTable ExecuteProductData(string produreName, SqlParameter[] param)
    {
      try
      {
        if (produreName == null || produreName.Trim().Length == 0)
          throw new ArgumentNullException("produreName", "过程名不能为空！");
        if (this._conn == null)
          this._conn = new SqlConnection(DataOperator.connectionString);
        if (ConnectionState.Closed == this._conn.State)
          this._conn.Open();
        this._cmd = new SqlCommand();
        this._cmd.Connection = this._conn;
        this._cmd.CommandType = CommandType.StoredProcedure;
        this._cmd.CommandText = produreName;
        for (int index = 0; index < param.Length; ++index)
          this._cmd.Parameters.Add(param[index]);
        DataTable dataTable = new DataTable();
        this._adp = new SqlDataAdapter(this._cmd);
        this._adp.Fill(dataTable);
        return dataTable;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        if (null != this._adp)
        {
          this._adp.Dispose();
          this._adp = (SqlDataAdapter) null;
        }
      }
    }

    public DataSet ExecuteProductDataSet(string produreName, SqlParameter[] param)
    {
      try
      {
        if (produreName == null || produreName.Trim().Length == 0)
          throw new ArgumentNullException("produreName", "过程名不能为空！");
        if (this._conn == null)
          this._conn = new SqlConnection(DataOperator.connectionString);
        if (ConnectionState.Closed == this._conn.State)
          this._conn.Open();
        this._cmd = new SqlCommand();
        this._cmd.Connection = this._conn;
        this._cmd.CommandType = CommandType.StoredProcedure;
        this._cmd.CommandText = produreName;
        for (int index = 0; index < param.Length; ++index)
          this._cmd.Parameters.Add(param[index]);
        DataSet dataSet = new DataSet();
        this._adp = new SqlDataAdapter(this._cmd);
        ((DataAdapter) this._adp).Fill(dataSet);
        return dataSet;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        if (null != this._adp)
        {
          this._adp.Dispose();
          this._adp = (SqlDataAdapter) null;
        }
      }
    }

    public bool BatchUpdate(DataTable dt, string sqlStr, SqlParameter[] param)
    {
      return this.BatchUpdate(dt, sqlStr, param, 100, false);
    }

    public bool BatchUpdate(DataTable dt, string sqlStr, SqlParameter[] param, int batchSize, bool updateAllRows)
    {
      try
      {
        if (updateAllRows)
        {
          dt.AcceptChanges();
          foreach (DataRow dataRow in (InternalDataCollectionBase) dt.Rows)
            dataRow.SetModified();
        }
        if (this._conn == null)
          this._conn = new SqlConnection(DataOperator.connectionString);
        if (ConnectionState.Closed == this._conn.State)
          this._conn.Open();
        this._adp = new SqlDataAdapter();
        this._adp.UpdateCommand = new SqlCommand(sqlStr, this._conn);
        for (int index = 0; index < param.Length; ++index)
          this._adp.UpdateCommand.Parameters.Add(param[index]);
        this._adp.UpdateCommand.UpdatedRowSource = UpdateRowSource.None;
        this._adp.UpdateBatchSize = batchSize;
        this._adp.Update(dt);
        return true;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        if (null != this._adp)
        {
          this._adp.Dispose();
          this._adp = (SqlDataAdapter) null;
        }
        if (null != this._conn)
        {
          this._conn.Close();
          this._conn = (SqlConnection) null;
        }
      }
    }

    public Hashtable executeProduct(string produreName, SqlParameter[] param)
    {
      try
      {
        Hashtable hashtable = new Hashtable();
        if (produreName == null || produreName.Trim().Length == 0)
          throw new ArgumentNullException("produreName", "过程名不能为空！");
        if (this._conn == null)
          this._conn = new SqlConnection(DataOperator.connectionString);
        if (ConnectionState.Closed == this._conn.State)
          this._conn.Open();
        this._cmd = new SqlCommand();
        this._cmd.Connection = this._conn;
        this._cmd.CommandType = CommandType.StoredProcedure;
        this._cmd.CommandText = produreName;
        for (int index = 0; index < param.Length; ++index)
          this._cmd.Parameters.Add((SqlParameter) ((ICloneable) param[index]).Clone());
        this._cmd.ExecuteNonQuery();
        for (int index = 0; index < this._cmd.Parameters.Count; ++index)
        {
          if (this._cmd.Parameters[index].Direction == ParameterDirection.Output || this._cmd.Parameters[index].Direction == ParameterDirection.InputOutput)
            hashtable.Add((object) this._cmd.Parameters[index].SourceColumn, this._cmd.Parameters[index].Value);
        }
        return hashtable;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public bool ExecuteProduct(string produreName, SqlParameter[] param)
    {
      try
      {
        if (produreName == null || produreName.Trim().Length == 0)
          throw new ArgumentNullException("produreName", "过程名不能为空！");
        if (this._conn == null)
          this._conn = new SqlConnection(DataOperator.connectionString);
        if (ConnectionState.Closed == this._conn.State)
          this._conn.Open();
        this._cmd = new SqlCommand();
        this._cmd.Connection = this._conn;
        this._cmd.CommandType = CommandType.StoredProcedure;
        this._cmd.CommandText = produreName;
        for (int index = 0; index < param.Length; ++index)
          this._cmd.Parameters.Add(param[index]);
        this._cmd.ExecuteNonQuery();
        return true;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public string ExecuteDBProduct(string produreName, SqlParameter[] param)
    {
      try
      {
        if (produreName == null || produreName.Trim().Length == 0)
          throw new ArgumentNullException("produreName", "过程名不能为空！");
        if (this._conn == null)
          this._conn = new SqlConnection(DataOperator.connectionString);
        if (ConnectionState.Closed == this._conn.State)
          this._conn.Open();
        this._cmd = new SqlCommand();
        this._cmd.Connection = this._conn;
        this._cmd.CommandType = CommandType.StoredProcedure;
        this._cmd.CommandText = produreName;
        for (int index = 0; index < param.Length; ++index)
          this._cmd.Parameters.Add(param[index]);
        this._cmd.ExecuteNonQuery();
        return (string) this._cmd.Parameters["@ResultValues"].Value;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static void SetConnectionName(string serverName)
    {
      DataOperator.SetConnectionName(serverName, DataOperator.key, DataOperator.iv);
    }

    public static void SetConnectionString(string conStr, string key, string iv)
    {
      if (conStr.Trim().Length == 0)
        throw new ArgumentNullException("conStr");
      DataOperator.connectionString = Encrypt.DecryptStr(conStr, key, iv);
    }

    public static void SetConnectionName(string serverName, string key, string iv)
    {
      if (serverName.Trim().Length == 0)
        throw new ArgumentNullException("serverName");
      DataOperator.connectionName = serverName.Trim();
      DataOperator.connectionString = DataOperator.GetConnectionObjString(DataOperator.connectionName, "connectionString", key, iv);
      DataOperator.uid = DataOperator.GetConnectionObjString(DataOperator.connectionName, "Uid", key, iv);
      DataOperator.password = DataOperator.GetConnectionObjString(DataOperator.connectionName, "Password", key, iv);
    }

    private static string GetConnectionObjString(string serverName, string itemName)
    {
      return DataOperator.GetConnectionObjString(serverName, itemName, DataOperator.key, DataOperator.iv);
    }

    private static string GetConnectionObjString(string serverName, string itemName, string key, string iv)
    {
      return Encrypt.DecryptStr(Regedit.GetValue(serverName, itemName, false), key, iv);
    }

    public bool TestDboState()
    {
      if (this._conn == null)
        this._conn = new SqlConnection(DataOperator.connectionString);
      try
      {
        if (ConnectionState.Closed == this._conn.State)
          this._conn.Open();
        return true;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public string checkSql(string sqlstr)
    {
      sqlstr = sqlstr.Replace("--", "__").Replace("'", "‘").Replace(";", "；").Trim();
      return sqlstr;
    }

    public void Close()
    {
      if (null != this._conn)
      {
        try
        {
          if (ConnectionState.Open == this._conn.State)
            this._conn.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
          this._conn = (SqlConnection) null;
        }
      }
      if (null != this._adp)
        this._adp.Dispose();
      this._adp = (SqlDataAdapter) null;
      if (null == this._cmd)
        return;
      this._cmd.Dispose();
    }

    public void Dispose()
    {
      this.Close();
    }
  }
}
