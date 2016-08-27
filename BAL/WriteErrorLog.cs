using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;
using System.Data.SqlClient;
namespace BAL
{
    public class WriteErrorLog
    {
        public static void ErrorLog(Exception Ex)
        {
            try
            {
                string Query = string.Empty;
                Query = "InsertErrorLogData";
                SqlParameter[] sqlParameter = {
          new SqlParameter("@HelpLink",Ex.HelpLink),
          new SqlParameter("@InnerException",Ex.InnerException),
          new SqlParameter("@Message",Ex.Message),
          new SqlParameter("@Source",Ex.Source),
          new SqlParameter("@StackTrace",Ex.StackTrace)
                                        };
                DMLSql.MYInstance.ExecuteNonquery(Query, sqlParameter, CommandType.StoredProcedure);
                Common.MessageBoxError(Ex.ToString());
            }
            catch (Exception ex)
            {
                Common.MessageBoxError(ex.ToString());
            }
        }
    }
}
