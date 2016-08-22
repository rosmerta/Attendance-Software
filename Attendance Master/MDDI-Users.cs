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
    public partial class MDDI_Users : Form
    {
        public MDDI_Users()
        {
            InitializeComponent();
        }

       
     
        private void getReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetRecords ObjGetRecords = new GetRecords();
            ObjGetRecords.IsMdiContainer = true;
            ObjGetRecords.Show();
        }

       
    }
}
