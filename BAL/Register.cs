using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Drawing;
namespace BAL
{
    public class Register
    {

        /// <summary>
        /// Insert Data In Database using Stroe Procedure....
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="name"></param>
        /// <param name="address"></param>
        /// <param name="fkOfficeId"></param>
        /// <param name="createdBy"></param>
        /// <param name="image"></param>
        /// <param name="thumbImpression"></param>
        /// <param name="thumbImpression1"></param>
        /// <param name="thumbImpression2"></param>
        /// 
        /// <returns>Return 0 or 1.....if return 1 or 2 that means save the data in database..</returns>
        public int InsertDataForRegisterUser(string employeeID, string name, string address, string fkOfficeId, int createdBy, byte [] image, string thumbImpression, string thumbImpression1, string thumbImpression2)
        {

            

            DMLSql obj = new DMLSql();
            try
            {
                string Query = "InsertRegisterEmployee";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = Query;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@employeeId", SqlDbType.VarChar).Value = employeeID;
                cmd.Parameters.Add("@name",SqlDbType.VarChar).Value= name;
                cmd.Parameters.Add("@address", SqlDbType.VarChar).Value= address;
                cmd.Parameters.Add("@fkOfficeId", SqlDbType.VarChar).Value =fkOfficeId;
                cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value= createdBy;
                cmd.Parameters.Add("@EmployeeImage", SqlDbType.Binary).Value = image;
                cmd.Parameters.Add("@ThoumbImpression", SqlDbType.VarChar).Value = thumbImpression;
                cmd.Parameters.Add("@ThoumbImpression2", SqlDbType.VarChar).Value=thumbImpression1;
                cmd.Parameters.Add("@ThoumbImpression3", SqlDbType.VarChar).Value=thumbImpression2;



                return obj.ExecuteNonquery(cmd);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public DataTable GetRecordsBasedOnThumbExpression()
        {
            DMLSql obj = new DMLSql();
            try
            {
                
                string Query = "GetRecordsBasedOnThumbExpression";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = Query;
                cmd.CommandType = CommandType.StoredProcedure;
                
                return obj.GetRecords(cmd);
            }
            catch (Exception)
            {

                throw;
            }

        }


        public int IN_AttendanceLog(string employeeID)
        {
            DMLSql obj = new DMLSql();
            try
            {
                string Query = "InsertAttendanceLog_IN";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = Query;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@employeeId", SqlDbType.VarChar).Value = employeeID;
                return obj.ExecuteNonquery(cmd);
            }
            catch (Exception)
            {

                throw;
            }

        }

        

        public string GetEmployeeForValidateRecords(string employeeID)
        {
            DMLSql obj = new DMLSql();
            try
            {
                string Query = "GetEmployeeForValidateRecords";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = Query;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@employeeId", SqlDbType.VarChar).Value = employeeID;
                return obj.GetSingleRecord(cmd);
            }
            catch (Exception)
            {

                throw;
            }


        }

        



    }
}
