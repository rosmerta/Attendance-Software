using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
namespace BAL
{
    public static class ExcetionClass
    {
          public static bool CheckForInternetConnection(this bool IsActive)
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (var stream = client.OpenRead("http://www.google.com"))
                    {
                        IsActive= true;
                    }
                }
            }
            catch
            {
                IsActive= false;
            }
            return IsActive;
        }
    }
}
