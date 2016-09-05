using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BAL;

namespace Attendance_Master
{
    public partial class MACAddressForm : Form
    {
        public MACAddressForm()
        {
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Register ObjRegister = new Register();
           if( ObjRegister.InsertMacAddressApprov(txtUserName.Text,txtEmailId.Text,txtMobileNo.Text,txtLocation.Text,Common.GetMACAddress())==Common.IsSuccess.Sucess.ToString())
           {

               LocalConditionRecords ObjLocalConditionReocrds = new LocalConditionRecords();
               ObjLocalConditionReocrds.InsertLocalAddress(Common.GetMACAddress());

               Common.MessageBoxInformation("Your Request has been submitted, Please wait Your request is under Process");
           }
           else
           {
               Common.MessageBoxWarning("Something went wrong when trying to send request for approval");
           }

        }

        private void BtnReSet_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
