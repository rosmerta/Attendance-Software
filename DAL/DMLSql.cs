﻿using System;
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
        private static string DatabaseName = string.Empty;
        private static string UserID = string.Empty;
        private static string Password = string.Empty;
        private static string ServerName = string.Empty;
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
                            string TempConnectionstring = @"Data Source=" + ServerName + ";Database=" + DatabaseName + ";User ID=" + UserID + ";Password=" + Password + ";";
                            conn = new SqlConnection(TempConnectionstring);
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
        /// <summary>
        /// Get A Single Records 
        /// </summary>
        /// <param name="Cmd" Sql Command ></param>
        /// <param name="commandType" Pass Paramerters and Depand on Requirements></param>
        /// <returns></returns>
        public DataTable GetSingleRecord(SqlCommand Cmd, CommandType commandType)
        {
            string Result = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                Cmd.Connection = openConnection();
                Cmd.CommandType = commandType;
                dt.Load(Cmd.ExecuteReader());
            }
            catch (SqlException)
            {
                CloseConnection();
                throw;
            }
            finally
            {
                CloseConnection();
            }
            return dt;
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
