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
    public partial class MDDI_SUPERUSERS : Form
    {
        public MDDI_SUPERUSERS()
        {
            InitializeComponent();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Common.CloseMantraConnection();
            Application.Exit();
        }

        private void attendancePunchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PunchMaster ObjPunchMaster = new PunchMaster();
            ObjPunchMaster.MdiParent = this;
            ObjPunchMaster.Show();
        }

        private void registerEmployeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormRegisterUser ObjRegister = new FormRegisterUser();
            ObjRegister.Show();
            ObjRegister.MdiParent = this;
        }
    }
}
