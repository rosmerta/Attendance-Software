// *** INIFile class test ***
using System;
using System.IO;
using System.Text;
namespace INI
{
    public  class CreateINIFile
    {
        string FileName = string.Empty;
        string ServerName = string.Empty;
        string UserName = string.Empty;
        string Password = string.Empty;
        string DSN = string.Empty;
        string DatabaseName = string.Empty;
        public CreateINIFile(string fileName)
        {
            FileName = fileName;
        }
        private static string PrintByteArray(byte[] Value)
        {
	        StringBuilder sb = new StringBuilder();
	        if (Value != null)
	        {
		        if (Value.Length > 0)
		        {
			        sb.Append(Value[0].ToString());
			        for (int i=1; i<Value.Length; i++)
			        {
				        sb.Append(", ");
				        sb.Append(Value[i].ToString());
			        }
		        }
	        }
	        return sb.ToString();
        }
        #region Create INI File- this File is created only First Time..
        public bool CreateFile(string serverName, string userName, string password, string dSN, string databaseName)
        {
            bool IsCreateFile = false;
            StreamReader sr = null;
            try
            {
                ServerName = serverName.Trim();
                UserName = userName.Trim();
                Password = password.Trim();
                DSN = dSN.Trim();
                DatabaseName = databaseName.Trim();
                INIFile MyINIFile = new INIFile(FileName);
                MyINIFile.SetValue("ServerName", "ServerName", ServerName);
                MyINIFile.SetValue("UserName", "UserName", UserName);
                MyINIFile.SetValue("Password", "Password", Password);
                MyINIFile.SetValue("DSN", "DSN", DSN);
                MyINIFile.SetValue("DatabaseName", "DatabaseName", DatabaseName);
                MyINIFile.Flush();
                IsCreateFile= true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (sr != null) sr.Close();
                sr = null;
            }
            return IsCreateFile;
        }
        #endregion
	}
}