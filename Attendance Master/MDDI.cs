using BAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Attendance_Master
{
    public partial class MDDI : Form
    {
        public MDDI()
        {
            InitializeComponent();
        }
        private void registerUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormRegisterUser ObjRegister = new FormRegisterUser();
            ObjRegister.Show();
            ObjRegister.MdiParent = this;
        }
        private void attendanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PunchMaster ObjPunchMaster = new PunchMaster();
            ObjPunchMaster.MdiParent = this;
            ObjPunchMaster.Show();
           
        }
        private void getReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetRecords ObjGetRecords = new GetRecords();
            ObjGetRecords.MdiParent = this;
            ObjGetRecords.Show();
        }
        private void userRegistrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserRegistration ObjUserRegistration = new UserRegistration();
            ObjUserRegistration.MdiParent = this;
            ObjUserRegistration.Show();
        }
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Common.CloseMantraConnection();
            Application.Exit();
        }
        private void MDDI_FormClosing(object sender, FormClosedEventArgs e)
        {
            Common.CloseMantraConnection();
            Application.Exit();
        }

        private void officeListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegisterOffice ObjRegisterOffice = new RegisterOffice();
            ObjRegisterOffice.MdiParent = this;
            ObjRegisterOffice.Show();

        }
    }
}
