using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Attendance_Master
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            LocalConditionRecords ObjLocalConditionRecords = new LocalConditionRecords();
            DataTable Dt = ObjLocalConditionRecords.GetMACStatusWithMACAddress();

            if (Dt.Rows.Count != Common.Zero)
            {
                if (string.IsNullOrEmpty(Dt.Rows[0]["Approve"].ToString()))
                {
                    Register ObjRegister = new Register();
                    string IsActiveMachine = ObjRegister.GetApproveSystemStatus(Common.GetMACAddress());
                    if (!string.IsNullOrEmpty(IsActiveMachine))
                    {
                        if (Convert.ToBoolean( IsActiveMachine))
                        {
                            LocalConditionRecords ObjLocalCondition = new LocalConditionRecords();
                            ObjLocalCondition.UpdateMACAddress(Convert.ToBoolean( IsActiveMachine));

                        }
                        else
                            Common.MessageBoxInformation("Sorry your Mechine not config for Attendance system, Please contact to Admin for Activate Mechine");
                    }
                    else
                    {
                        Common.MessageBoxInformation("Your Mechine is not config yet");
                    }
                  
                    // Application.Run(new MACAddressForm());
                }
                else
                {
                    if (Convert.ToBoolean(Common.Decrypt(Dt.Rows[0]["Approve"].ToString(), "rosmerta")))
                    {
                        Application.Run(new Login());

                    }
                    else
                        Common.MessageBoxInformation("Sorry your Mechine not config for Attendance system, Please contact to Admin for Activate Mechine");
                }

            }
            else
                if (Dt.Rows.Count == Common.Zero)
                {
                    Application.Run(new MACAddressForm());
                }
        }
    }
}
