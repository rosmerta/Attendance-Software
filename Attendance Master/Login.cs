using BAL;
using System;
using System.Windows.Forms;
namespace Attendance_Master
{
    public partial class Login : Form
    {
        User objUser;
        public Login()
        {
            InitializeComponent();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Login Here 
        /// Joginder Singh 
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                objUser = new User();
                // 0 means everything is correct
                // 1 Means userName and Passwrod is Correct but date not Valid.
                // 2 means Everything is wrong. 

                string CheckValue = objUser.CheckUserCredential(txtUsername.Text, txtPassword.Text).ToString();
                if (Convert.ToInt32(CheckValue.Split('\\')[1]) == (int)Common.LoginStatus.Correct)
                {

                    LoggedInUser.userName = txtUsername.Text;
                    DateTime loginDateTime = DateTime.Now;
                    LoggedInUser.computerName = Environment.MachineName;
                    LoggedInUser.loginStatus = 1;
                    string UserType = objUser.GetUserType(Convert.ToInt32(CheckValue.Split('\\')[0]));
                    if (objUser.InsertLoginDetails(LoggedInUser.userName, loginDateTime, LoggedInUser.computerName, LoggedInUser.loginStatus))
                    {
                        if (Common.UserRoles.Admin.ToString() != UserType)
                        {
                            this.Hide();
                            MDDI_Users mdi = new MDDI_Users();
                            mdi.Show();
                        }
                        else
                        {

                            this.Hide();
                            MDDI mdi = new MDDI();
                            mdi.Show();
                        }
                    }

                }
                //else if (CheckValue == 1)
                else if (Convert.ToInt32(CheckValue.Split('\\')[1]) == (int)Common.LoginStatus.NameANDPasswordValidDateNOValid)
                {
                    Common.MessageBoxError("You have no Permission, Contact to admin");
                    return;
                }
                else if (Convert.ToInt32(CheckValue.Split('\\')[1]) == (int)Common.LoginStatus.EveryThingWrong)
                {
                    Common.MessageBoxError("Please Check UserName and Password.");
                    return;
                }
            }
            catch (Exception ex)
            {
                WriteErrorLog.ErrorLog(ex);
                return;
                //ThrowException throwException = new ThrowException();
                //throwException.WriteError(ex.ToString());
            }
        }
        /// <summary>
        /// Check User Name 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtUsername_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!Common.ValidateStringValue(txtUsername.Text.Trim()))
                {
                    Common.MessageBoxError("Please Enter Valid UserName");
                    txtUsername.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                WriteErrorLog.ErrorLog(ex);
            }
        }
        /// <summary>
        /// Check User Password Validation 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPassword_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!Common.ValidateStringValue(txtPassword.Text.Trim()))
                {
                    Common.MessageBoxError("Please Enter Valid passWord");
                    txtPassword.Focus();
                    return;
                }
                else
                    btnLogin.Focus();
            }
            catch (Exception ex)
            {
                WriteErrorLog.ErrorLog(ex);
            }
        }
        private void Login_Load(object sender, EventArgs e)
        {
            try
            {
                this.MaximizeBox = false;
                User objUser = new User();
                //Check users Data Connection Successfully or Not ...
                // Created By Joginder Singh 
                objUser.GetConnect_Staus();
            }
            catch (Exception ex)
            {
                WriteErrorLog.ErrorLog(ex);
            }
        }
    }
}
