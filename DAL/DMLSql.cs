using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INI;
namespace DAL
{
    public class DMLSql
    {
        private static SqlDataAdapter myAdapter;
        private static DMLSql Instance = null;
        private DMLSql() { }
        private static object lockThis = new object();
        private static SqlConnection conn;
        private static SqlConnection localConnection;
        private static string DatabaseName = string.Empty;
        private static string UserID = string.Empty;
        private static string Password = string.Empty;
        private static string ServerName = string.Empty;
        //Local connection details
        private static string localDatabaseName = string.Empty;
        private static string localUserID = string.Empty;
        private static string localPassword = string.Empty;
        private static string localServerName = string.Empty;
        // Initialise Connection
        public static DMLSql MYInstance
        {
            get
            {
                try
                {       if (Instance == null)
                        {
                            Instance = new DMLSql();
                            myAdapter = new SqlDataAdapter();
                            ReadINIFile objReadINIFile = new ReadINIFile(@"D:\a-master.ini");
                            //ReadINIFile objReadINIFile = new ReadINIFile(@"D:\NICSYNC.ini");
                            DatabaseName = objReadINIFile.GetSetting("DatabaseName", "DatabaseName");
                            UserID = objReadINIFile.GetSetting("UserName", "UserName");
                            Password = objReadINIFile.GetSetting("Password", "Password");
                            ServerName = objReadINIFile.GetSetting("ServerName", "ServerName");
                            localDatabaseName = objReadINIFile.GetSetting("localDatabaseName", "localDatabaseName");
                            localUserID = objReadINIFile.GetSetting("localUserName", "localUserName");
                            localPassword = objReadINIFile.GetSetting("localPassword", "localPassword");
                            localServerName = objReadINIFile.GetSetting("localServerName", "localServerName");
                            string LocalconncetionString = @"Data Source=" + localServerName + ";Database=" + localDatabaseName + ";User ID=" + localUserID + ";Password=" + localPassword + ";";
                            string TempConnectionstring = @"Data Source=" + ServerName + ";Database=" + DatabaseName + ";User ID=" + UserID + ";Password=" + Password + ";";
                            conn = new SqlConnection(TempConnectionstring);
                            localConnection = new SqlConnection(LocalconncetionString);
                        }
                        return Instance;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        // Open Database Connection if Closed or Broken
        private SqlConnection openConnection(bool isLocalConnection)
        {
            SqlConnection Connection = null;
            if (isLocalConnection)
                Connection = localConnection; 
            else
                Connection = conn;
            if (Connection.State == ConnectionState.Closed || Connection.State == ConnectionState.Broken)
                Connection.Open();
            return Connection;
        }
        // Close Connection....
        private SqlConnection CloseConnection(bool isLocalConnection)
        {
            SqlConnection Connection = null;
            if (isLocalConnection)
                Connection = localConnection;
            else
                Connection = conn;

            if (Connection.State == ConnectionState.Open || Connection.State == ConnectionState.Broken)
            {
                Connection.Close();
            }
            return Connection;
        }
        /// <summary>
        /// Get A Single Records 
        /// </summary>
        /// <param name="Cmd" Sql Command ></param>
        /// <param name="commandType" Pass Paramerters and Depand on Requirements></param>
        /// <returns></returns>
        public DataTable GetSingleRecord(SqlCommand Cmd, CommandType commandType, bool isLocal = false)
        {
            string Result = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                Cmd.Connection = openConnection(isLocal);
                Cmd.CommandType = commandType;
                dt.Load(Cmd.ExecuteReader());
            }
            catch (SqlException)
            {
                CloseConnection(isLocal);
                throw;
            }
            finally
            {
                CloseConnection(isLocal);
            }
            return dt;
        }
        // Insert data through Text/Procedure with sql parameters
        public int ExecuteNonquery(string query, SqlParameter[] sqlParameter, CommandType commandType,bool isLocal=false)
        {
            int TempValue = 0;
            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                sqlCommand.Connection = openConnection(isLocal);
                sqlCommand.CommandText = query;
                sqlCommand.CommandType = commandType;
                if (sqlParameter != null)
                    sqlCommand.Parameters.AddRange(sqlParameter);
                TempValue = sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                CloseConnection(isLocal);
                throw ex;
            }
            finally
            {
                CloseConnection(isLocal);
            }
            return TempValue;
        }
        public int ExecuteNonquery(SqlCommand cmd, bool isLocal = false)
        {
            int TempValue = 0;
            try
            {
                cmd.Connection = openConnection(isLocal);
                TempValue = cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                CloseConnection(isLocal);
                throw ex;
            }
            finally
            {
                CloseConnection(isLocal);
            }
            return TempValue;
        }
        // To get Single Record with passing parameters
        public string GetSingleRecord(string query, SqlParameter[] sqlParameter, CommandType commandType, bool isLocal = false)
        {
            string TempValue = string.Empty;
            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                sqlCommand.Connection = openConnection(isLocal);
                sqlCommand.CommandText = query;
                sqlCommand.CommandType = commandType;
                if (sqlParameter != null)
                    sqlCommand.Parameters.AddRange(sqlParameter);
                TempValue = Convert.ToString(sqlCommand.ExecuteScalar());
            }
            catch (SqlException ex)
            {
                CloseConnection(isLocal);
                throw ex;
            }
            finally
            {
                CloseConnection(isLocal);
            }
            return TempValue;
        }
        // To get Single Record without passing parameters
        public string GetSingleRecord(string query, CommandType commandType, bool isLocal = false)
        {
            string TempValue = string.Empty;
            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                sqlCommand.Connection = openConnection(isLocal);
                sqlCommand.CommandText = query;
                sqlCommand.CommandType = commandType;
                TempValue = Convert.ToString(sqlCommand.ExecuteScalar());
            }
            catch (SqlException ex)
            {
                CloseConnection(isLocal);
                throw ex;
            }
            finally
            {
                CloseConnection(isLocal);
            }
            return TempValue;
        }
        // To get Single Record without passing parameters
        public string GetSingleRecord(SqlCommand cmd, bool isLocal = false)
        {
            string TempValue = string.Empty;
            try
            {
                cmd.Connection = openConnection(isLocal);
                TempValue = Convert.ToString(cmd.ExecuteScalar());
            }
            catch (SqlException ex)
            {
                CloseConnection(isLocal);
                throw ex;
            }
            finally
            {
                CloseConnection(isLocal);
            }
            return TempValue;
        }
        // To get multiple records with passing parameters
        public DataTable GetRecords(string query, SqlParameter[] sqlParameter, CommandType commandType, bool isLocal = false)
        {
            SqlCommand sqlCommand = new SqlCommand();
            DataTable records = null;
            try
            {
                records = new DataTable();
                sqlCommand.Connection = openConnection(isLocal);
                sqlCommand.CommandText = query;
                sqlCommand.CommandType = commandType;
                if (sqlParameter != null)
                    sqlCommand.Parameters.AddRange(sqlParameter);
                records.Load(sqlCommand.ExecuteReader());
            }
            catch (SqlException ex)
            {
                CloseConnection(isLocal);
                throw ex;
            }
            finally
            {
                CloseConnection(isLocal);
            }
            return records;
        }
        public DataTable GetRecords(SqlCommand cmd, bool isLocal = false)
        {
            DataTable records = null;
            try
            {
                records = new DataTable();
                cmd.Connection = openConnection(isLocal);
                records.Load(cmd.ExecuteReader());
            }
            catch (SqlException ex)
            {
                CloseConnection(isLocal);
                throw ex;
            }
            finally
            {
                CloseConnection(isLocal);
            }
            return records;
        }
        // To get multiple records without passing parameters
        public DataTable GetRecords(string query, CommandType commandType, bool isLocal = false)
        {
            SqlCommand sqlCommand = new SqlCommand();
            DataTable records = null;
            try
            {
                records = new DataTable();
                sqlCommand.Connection = openConnection(isLocal);
                sqlCommand.CommandText = query;
                sqlCommand.CommandType = commandType;
                records.Load(sqlCommand.ExecuteReader());
            }
            catch (SqlException ex)
            {
                CloseConnection(isLocal);
                throw ex;
            }
            catch (Exception ex)
            {
                CloseConnection(isLocal);
                throw ex;
            }
            finally
            {
                CloseConnection(isLocal);
            }
            return records;
        }
    }
}
