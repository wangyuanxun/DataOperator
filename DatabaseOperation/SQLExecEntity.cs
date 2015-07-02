using System;
using System.Collections.Generic;

namespace TY.DatabaseOperation
{
  [Serializable]
  public class SQLExecEntity : IMQEntity
  {
    public static long SequenceId = 0L;
    private string Id = Convert.ToString(SQLExecEntity.SequenceId++);
    public int RedoTimes = 0;
    public string SqlStr;
    public List<DBSqlParameter> Param;

    public string ID
    {
      get
      {
        return this.Id;
      }
    }
  }
}
