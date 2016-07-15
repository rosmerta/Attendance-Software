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
            ObjPunchMaster.Show();
           
        }
    }
}
