using DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BAL
{
    /// <summary>
    /// Developer Piyush Diwan
    /// user related database functionality
    /// </summary>
    public class User
    {
        SqlCommand oraCommand = null;
        public User()
        {
        }
        public string CheckUserCredential(string UserName, string Password)
        {
            string ReturnValue = string.Empty;
            try
            {
                oraCommand = new SqlCommand();
                if (Common.ValidateStringValue(UserName) && Common.ValidateStringValue(Password))
                {
                    SqlCommand ObjCommand = new SqlCommand("CheckUserCredential");
                    SqlParameter SqlParameters = new SqlParameter();
                    SqlParameters.SqlDbType = SqlDbType.VarChar;
                    SqlParameters.Value = UserName;
                    SqlParameters.ParameterName = "@UserName";
                    SqlParameter password = new SqlParameter();
                    password.SqlDbType = SqlDbType.VarChar;
                    password.Value = Common.Encrypt(Password, "rosmerta");
                    password.ParameterName = "@password";
                    ObjCommand.Parameters.Add(SqlParameters);
                    ObjCommand.Parameters.Add(password);
                    ReturnValue = DMLSql.MYInstance.GetSingleRecord(ObjCommand, CommandType.StoredProcedure).Rows[0]["TempValue"].ToString();
                   
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ReturnValue;
        }
        public DataTable GetDataForUser()
        {
            try
            {
                oraCommand = new SqlCommand();
                string Query = "GetDataForUser";
                return DMLSql.MYInstance.GetRecords(Query, CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Boolean InsertDataForUser(string name, string userName, string password, int userType, string dateValidFrom, string dateValidTill)
        {
            try
            {
                bool Value = false;
                oraCommand = new SqlCommand();
                //query to insert user info
                string Query = "InsertDataForUser";
                SqlParameter[] sqlParameter = {
                new SqlParameter("@NAME",name),
                new SqlParameter("@USERNAME",userName),
                  new SqlParameter("@PASSWORD",password),
                  new SqlParameter("@userType",userType),
                  new SqlParameter("@DATE_VALID_FROM", Convert.ToDateTime(dateValidFrom).ToString("dd-MMM-yyyy")),
                  new SqlParameter("@DATE_VALID_TIL", Convert.ToDateTime(dateValidTill).ToString("dd-MMM-yyyy"))
                          };
                int TempValue = DMLSql.MYInstance.ExecuteNonquery(Query, sqlParameter, CommandType.StoredProcedure);
                if (TempValue > (int)Common.InDatabaseInsertData.Zero)
                {
                    Value = true;
                }
                return Value;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Boolean UserNameExist(string userName)
        {
            try
            {
                oraCommand = new SqlCommand();
                //query to insert user info
                string Query = "UserNameExist";
                SqlParameter[] sqlParameter = {
                new SqlParameter("@USERNAME",userName)};
                object obj = DMLSql.MYInstance.GetSingleRecord(Query, sqlParameter, CommandType.StoredProcedure);
                string TempValue = string.Empty;
                if (obj != null)
                    TempValue = obj.ToString();
                if (Common.ValidateStringValue(TempValue))
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string GetUserType(int userID)
        {
            try
            {
                oraCommand = new SqlCommand();
                //Query Get The User Type 
                string Query = "GetUserTypeBasedOnUserID";
                SqlParameter[] sqlParameter = {
                new SqlParameter("@UserID",userID)
                                              };
                return DMLSql.MYInstance.GetSingleRecord(Query, sqlParameter, CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Boolean InsertLoginDetails(string userName, DateTime loginDateTime, string computerName, int loginStatus)
        {
            try
            {
                oraCommand = new SqlCommand();
                //query to insert user login details
                string Query = "InsertLoginDetails";
                SqlParameter[] sqlParameter = {
                new SqlParameter("@USERNAME", userName),
            new SqlParameter("@LOGIN_DATETIME", loginDateTime.ToString("dd-MMM-yyyy hh:mm:ss tt")),
            new SqlParameter("@COMPUTER_NAME", computerName),
           new SqlParameter("@LOGIN_STATUS", loginStatus)};
                int TempValue = DMLSql.MYInstance.ExecuteNonquery(Query, sqlParameter, CommandType.StoredProcedure);
                if (TempValue > (int)Common.InDatabaseInsertData.Zero)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Boolean InsertLogoutDetails(string userName, DateTime logoutDateTime, string computerName, int loginStatus, int logoutStatus)
        {
            try
            {
                oraCommand = new SqlCommand();
                //query to insert user logout details
                string Query = "InsertLogoutDetails";
                SqlParameter[] sqlParameter = {
                new SqlParameter("@userName", userName),
            new SqlParameter("@computerName", computerName)};
                int TempValue = DMLSql.MYInstance.ExecuteNonquery(Query, sqlParameter, CommandType.StoredProcedure);
                if (TempValue > (int)Common.InDatabaseInsertData.Zero)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Check Connect to DataBase or Not...
        /// </summary>
        /// <returns></returns>
        public int GetConnect_Staus()
        {
            try
            {
                string Query = "Getconnect_status";
                return Convert.ToInt32(DMLSql.MYInstance.GetSingleRecord(Query, CommandType.StoredProcedure));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
