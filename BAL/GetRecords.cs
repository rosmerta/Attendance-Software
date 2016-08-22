using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BAL
{
    public class GetRecord
    {
        public DataTable GetRecordsAttendanceLog(int employeeID,DateTime monthName)
        {
            try
            {
                string Query = "GetRecordsAttendanceLog";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = Query;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = employeeID;
                cmd.Parameters.Add("@MonthName", SqlDbType.DateTime).Value = monthName;
                return DMLSql.MYInstance.GetRecords(cmd);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
