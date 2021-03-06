﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using BAL;
namespace Attendance_Master
{
    public partial class UserRegistration : Form
    {
        User objUser = null;
        DataTable dt = null;
        string errors;
        Boolean status;
        public UserRegistration()
        {
            status = false;
            errors = string.Empty;
            InitializeComponent();
            Reset();
            refresh();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDateTime(dtpDateValidFrom.Text) > Convert.ToDateTime(dtpDateValidTill.Text))
                {
                    Common.MessageBoxError("Please Enter Valid Date in From and To ");
                    return;
                }
                else
                {
                    errors = Common.fieldValidation(txtName.Text, txtUserName.Text, txtPassword.Text, txtConfirmPassword.Text,Convert.ToInt32(CmbUserType.SelectedValue), dtpDateValidFrom.Text, dtpDateValidTill.Text);
                    status = objUser.UserNameExist(txtUserName.Text);
                    if (status)
                    {
                        errors = txtUserName.Tag + " : '" + txtUserName.Text + "' already exists";
                    }
                    if (string.IsNullOrEmpty(errors))
                    {
                        if (objUser.InsertDataForUser(txtName.Text, txtUserName.Text, Common.Encrypt(txtPassword.Text, "rosmerta"),Convert.ToInt32( CmbUserType.SelectedValue), dtpDateValidFrom.Text, dtpDateValidTill.Text))
                        {
                            refresh();
                            Reset();
                        }
                        else
                            Common.MessageBoxError("Something Went Wrong");
                    }
                    else
                    {
                        Common.MessageBoxError(errors);
                        errors = string.Empty;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                WriteErrorLog.ErrorLog(ex);
            }
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (Convert.ToBoolean(row.Cells[0].Value) == true)
                {
                }
            }
        }
        private void Reset()
        {
            //To Reset all fields and refresh gridview
            try
            {
                txtName.Focus();
                txtName.Text = string.Empty;
                txtUserName.Text = string.Empty;
                txtPassword.Text = string.Empty;
                txtConfirmPassword.Text = string.Empty;
                dtpDateValidFrom.Text = string.Empty;
                dtpDateValidTill.Text = string.Empty;
                errors = string.Empty;
            }
            catch (Exception ex)
            {
                WriteErrorLog.ErrorLog(ex);
            }
        }
        public DataTable GetDataUser_Mast()
        {
            DataTable objDatatable = new DataTable();
            try
            {
                objUser = new User();
                dt = new DataTable();
                objDatatable = objUser.GetDataForUser();
            }
            catch (Exception ex)
            {
                WriteErrorLog.ErrorLog(ex);
            }
            return objDatatable;
        }
        public void refresh()
        {
            try
            {
                dataGridView1.Refresh();
                dataGridView1.DataSource = GetDataUser_Mast();
                //dataGridView1.Columns[0].HeaderText = "NAME";
                //dataGridView1.Columns[1].HeaderText = "USER NAME";
                //dataGridView1.Columns[3].HeaderText = "USER TYPE";
                //dataGridView1.Columns[4].HeaderText = "DATE VALID FROM";
                //dataGridView1.Columns[5].HeaderText = "DATE VALID TILL";
            }
            catch (Exception ex)
            {
                WriteErrorLog.ErrorLog(ex);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (Convert.ToBoolean(row.Cells[0].Value) == true)
                {
                    string UserName = string.Empty;
                    string Name = string.Empty;
                    Name = Convert.ToString(row.Cells["NAME"].Value);
                    UserName = Convert.ToString(row.Cells["USERNAME"].Value);
                    User ObjUser = new User();
                    if (objUser.DeleteUserNameRecords(UserName, Name) == Convert.ToString( Common.IsSuccess.Sucess))
                    {
                        Common.MessageBoxInformation("Your Request Done");
                    }
                    else
                    {
                        Common.MessageBoxError("Something Wrong when trying to delete records ");
                    }
                }
            }
        }
        private void UserRegistration_Load(object sender, EventArgs e)
        {
            Dictionary<int, string> ObjDic = new Dictionary<int, string>();
            foreach (DataRow Rows in  Common.ReturnDataTableBasedOnStoreProcedure("GetUserTypeName").Rows)
            {
                ObjDic.Add(Convert.ToInt32(Rows["UID"].ToString()), Rows["User_Type"].ToString());
            };         
            CmbUserType.DataSource = ObjDic.ToList();
            CmbUserType.ValueMember = "Key";
            CmbUserType.DisplayMember = "Value";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (Convert.ToBoolean(row.Cells[0].Value) == true)
                {
                    string UserName = string.Empty;
                    string Name = string.Empty;
                    Name = Convert.ToString(row.Cells["NAME"].Value);
                    UserName = Convert.ToString(row.Cells["USERNAME"].Value);
                    User ObjUser = new User();
                    if (objUser.ResetPasswordBasedonUserNameAndName(UserName, Name) > Common.Zero)
                    {
                        Common.MessageBoxInformation("Your Request Done");
                    }
                    else
                    {
                        Common.MessageBoxError("Something Wrong when trying to update password or Reset");
                    }
                }
            }
        }
    }
}
