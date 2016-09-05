using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data.SqlClient;
using System.Data;
namespace BAL
{
   public class LocalConditionRecords
    {

       public DataTable GetMACStatusWithMACAddress()
       {
           try
           {
               string Query = "GetMACStatusWithMACAddress";
               SqlCommand cmd = new SqlCommand();
               cmd.CommandText = Query;
               cmd.CommandType = CommandType.StoredProcedure;
               return DMLSql.MYInstance.GetRecords(cmd,true);
           }
           catch (Exception)
           {
               throw;
           }
       }

       public string InsertLocalAddress(string macAddress)
       {

           try
           {
               string Query = "InsertLocalAddress";
               SqlCommand cmd = new SqlCommand();
               cmd.CommandText = Query;
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.Add("@MacAddress", SqlDbType.VarChar).Value =Common.Encrypt(macAddress, "rosmerta");
               return DMLSql.MYInstance.GetSingleRecord(cmd, true);
           }
           catch (Exception)
           {
               throw;
           }
       }


       public string UpdateMACAddress(bool IsStatus)
       {
           try
           {
               string Query = "UpdateMACAddress";
               SqlCommand cmd = new SqlCommand();
               cmd.CommandText = Query;
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.Add("@Isstatus", SqlDbType.VarChar).Value = Common.Encrypt(Convert.ToString(IsStatus), "rosmerta"); ;
               return DMLSql.MYInstance.GetSingleRecord(cmd, true);
           }
           catch (Exception)
           {
               throw;
           }
       }


    }
}
