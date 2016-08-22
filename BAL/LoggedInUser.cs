using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
   public class LoggedInUser
    {
        public static string userName { get; set; }
        public static string rtoCode { get; set; }
        public static string rtoLocation { get; set; }
        public static string computerName { get; set; }
        public static int loginStatus { get; set; }
    }
   public class ConnectionDetails
    {
        public static string ServerName { get; set; }
        public static string DatabaseName { get; set; }
        public static string UserID { get; set; }
        public static string Password { get; set; }
        public static string CurrentDirectory { get; set; }
        public static string DSNName { get; set; }

    }

}
