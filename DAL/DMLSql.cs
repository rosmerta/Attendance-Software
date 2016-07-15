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
        private SqlDataAdapter myAdapter;
        private SqlConnection conn;
        private string DatabaseName = string.Empty;
        private string UserID = string.Empty;
        private string Password = string.Empty;
        private string ServerName = string.Empty;

        // Initialise Connection
        public DMLSql()
        {
            myAdapter = new SqlDataAdapter();
            ReadINIFile objReadINIFile = new ReadINIFile(@"D:\a-master.ini");
            //ReadINIFile objReadINIFile = new ReadINIFile(@"D:\NICSYNC.ini");
            DatabaseName = objReadINIFile.GetSetting("DatabaseName", "DatabaseName");
            UserID = objReadINIFile.GetSetting("UserName", "UserName");
            Password = objReadINIFile.GetSetting("Password", "Password");
            ServerName = objReadINIFile.GetSetting("ServerName", "ServerName");
            string TempConnectionstring = @"Data Source=" + ServerName + ";Database=" + DatabaseName + ";User ID=" + UserID + ";Password=" + Password + ";";
            conn = new SqlConnection(TempConnectionstring); 
       
        }

        // Open Database Connection if Closed or Broken
        private SqlConnection openConnection()
        {
            if (conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken)
            {
                conn.Open();
            }
            return conn;
        }

        // Close Connection....
        private SqlConnection CloseConnection()
        {
            if (conn.State == ConnectionState.Open || conn.State == ConnectionState.Broken)
            {
                conn.Close();
            }
            return conn;
        }

        // Insert data through Text/Procedure with sql parameters
        public int ExecuteNonquery(string query, SqlParameter[] sqlParameter, CommandType commandType)
        {
            int TempValue = 0;
            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                sqlCommand.Connection = openConnection();
                sqlCommand.CommandText = query;
                sqlCommand.CommandType = commandType;

                if (sqlParameter != null)
                    sqlCommand.Parameters.AddRange(sqlParameter);

                TempValue = sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                CloseConnection();
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return TempValue;
        }
        public int ExecuteNonquery(SqlCommand cmd)
        {
            int TempValue = 0;
        
            try
            {
                cmd.Connection = openConnection();



                TempValue = cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                CloseConnection();
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return TempValue;
        }

        // To get Single Record with passing parameters
        public string GetSingleRecord(string query, SqlParameter[] sqlParameter, CommandType commandType)
        {
            string TempValue = string.Empty;
            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                sqlCommand.Connection = openConnection();
                sqlCommand.CommandText = query;
                sqlCommand.CommandType = commandType;

                if (sqlParameter != null)
                    sqlCommand.Parameters.AddRange(sqlParameter);

                TempValue = Convert.ToString(sqlCommand.ExecuteScalar());

            }
            catch (SqlException ex)
            {
                CloseConnection();
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return TempValue;
        }

        // To get Single Record without passing parameters
        public string GetSingleRecord(string query, CommandType commandType)
        {
            string TempValue = string.Empty;
            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                sqlCommand.Connection = openConnection();
                sqlCommand.CommandText = query;
                sqlCommand.CommandType = commandType;

                TempValue = Convert.ToString(sqlCommand.ExecuteScalar());

            }
            catch (SqlException ex)
            {
                CloseConnection();
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return TempValue;
        }

        // To get Single Record without passing parameters
        public string GetSingleRecord(SqlCommand cmd)
        {
            string TempValue = string.Empty;
            try
            {
                cmd.Connection = openConnection();
                TempValue = Convert.ToString(cmd.ExecuteScalar());

            }
            catch (SqlException ex)
            {
                CloseConnection();
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return TempValue;
        }

        // To get multiple records with passing parameters
        public DataTable GetRecords(string query, SqlParameter[] sqlParameter, CommandType commandType)
        {
            SqlCommand sqlCommand = new SqlCommand();
            DataTable records = null;
            try
            {
                records = new DataTable();

                sqlCommand.Connection = openConnection();
                sqlCommand.CommandText = query;
                sqlCommand.CommandType = commandType;

                if (sqlParameter != null)
                    sqlCommand.Parameters.AddRange(sqlParameter);

                records.Load(sqlCommand.ExecuteReader());

            }
            catch (SqlException ex)
            {
                CloseConnection();
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return records;
        }

        public DataTable GetRecords(SqlCommand cmd)
        {

          
            DataTable records = null;
            try
            {
                records = new DataTable();

                cmd.Connection = openConnection();

                records.Load(cmd.ExecuteReader());

            }
            catch (SqlException ex)
            {
                CloseConnection();
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return records;
        }

        // To get multiple records without passing parameters
        public DataTable GetRecords(string query, CommandType commandType)
        {
            SqlCommand sqlCommand = new SqlCommand();
            DataTable records = null;
            try
            {
                records = new DataTable();

                sqlCommand.Connection = openConnection();
                sqlCommand.CommandText = query;
                sqlCommand.CommandType = commandType;

                records.Load(sqlCommand.ExecuteReader());

            }
            catch (SqlException ex)
            {
                CloseConnection();
                throw ex;
            }
            catch (Exception ex)
            {
                CloseConnection();
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return records;
        }
    }
}
