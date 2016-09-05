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
using System.IO;
using DPUruNet;
using System.Threading;
using System.Drawing.Imaging;
using System.Globalization;
using System.Net;
using Newtonsoft.Json;
namespace Attendance_Master
{
    public partial class PunchMaster : Form
    {
        public PunchMaster()
        {
            InitializeComponent();
            Timerview.Enabled = true;
            Timerview.Interval = 60000;
           
        }
        private void PunchMaster_Load(object sender, EventArgs e)
        {
            lblDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            digitalDisplayControl1.DigitText = DateTime.Now.ToString("HH:mm");
            BindImage(FirstFinger);
            this.ControlBox = false;
        }
        private void BindImage(PictureBox picturebox)
        {
            lblStatus.Text = "Please Wait";
            MantraFingerScanner ObjFingerScanner = new MantraFingerScanner();
            ObjFingerScanner.MantraConnection(ref lblStatus, true, ref picturebox, lblEmployeeName, lblEmployeeId);
            lblStatus.Text = "Put your finger on Finger Scanner";
        }
        /// <summary>
        /// Open a device and check result for errors.
        /// </summary>
        /// <returns>Returns true if successful; false if unsuccessful</returns>
        private void timer1_Tick(object sender, EventArgs e)
        {
            digitalDisplayControl1.DigitText = DateTime.Now.ToString("HH:mm");
            lblDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
        }
        private void BtnClose_Click(object sender, EventArgs e)
        {
                   Common.CloseMantraConnection();
                   this.Close();
        }
    }
}
