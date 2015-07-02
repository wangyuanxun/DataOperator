using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace TY.DatabaseOperation
{
  public class DB_Helper
  {
    private string strConnection = ((object) ConfigurationSettings.AppSettings["sqlConnString"]).ToString();
    private SqlConnection conn = (SqlConnection) null;
    private SqlCommand cmd = (SqlCommand) null;
    private SqlDataAdapter da = (SqlDataAdapter) null;

    public string DBRunProcedure(string procedureName, SqlParameter[] paras)
    {
      if (procedureName == null || procedureName == "")
        return "存储过程名字为空!";
      try
      {
        this.conn = new SqlConnection(this.strConnection);
        if (this.conn.State == ConnectionState.Closed)
          this.conn.Open();
        this.cmd = new SqlCommand();
        this.cmd.Connection = this.conn;
        this.cmd.CommandType = CommandType.StoredProcedure;
        this.cmd.CommandText = procedureName;
        this.cmd.Parameters.Clear();
        if (paras != null && paras.Length > 0)
        {
          foreach (SqlParameter sqlParameter in paras)
            this.cmd.Parameters.Add(sqlParameter);
        }
        this.cmd.ExecuteNonQuery();
        return this.cmd.Parameters["@ResultValues"].Value.ToString();
      }
      catch (InvalidOperationException ex)
      {
        return ex.Message;
      }
      catch (Exception ex)
      {
        return ex.Message;
      }
      finally
      {
        if (this.conn.State == ConnectionState.Open)
          this.conn.Close();
      }
    }

    public string DBRunProcedure(string procedureName, SqlParameter[] paras, ref DataSet ds)
    {
      if (procedureName == null || procedureName == "")
        return "存储过程名字为空!";
      try
      {
        this.conn = new SqlConnection(this.strConnection);
        if (this.conn.State == ConnectionState.Closed)
          this.conn.Open();
        this.cmd = new SqlCommand();
        this.cmd.Connection = this.conn;
        this.cmd.CommandType = CommandType.StoredProcedure;
        this.cmd.CommandText = procedureName;
        this.cmd.Parameters.Clear();
        if (paras != null && paras.Length > 0)
        {
          foreach (SqlParameter sqlParameter in paras)
            this.cmd.Parameters.Add(sqlParameter);
        }
        this.da = new SqlDataAdapter();
        this.da.SelectCommand = this.cmd;
        ((DataAdapter) this.da).Fill(ds);
        return "-1";
      }
      catch (InvalidOperationException ex)
      {
        return ex.Message;
      }
      catch (SqlException ex)
      {
        return ex.Message;
      }
      catch (Exception ex)
      {
        return ex.Message;
      }
      finally
      {
        if (this.conn.State == ConnectionState.Open)
          this.conn.Close();
      }
    }
  }
}
