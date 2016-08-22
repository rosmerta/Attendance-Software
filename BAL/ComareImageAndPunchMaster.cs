using MANTRA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace BAL
{
    public class ComareImageAndPunchMaster
    {
        #region MyRegion
        bool reset = false;
        byte[] _FingerImageData = null;
        DataTable CompleteDataForValidateRecords = null;
        Register ObjRegister = null;
        int ImagesValidateScore = 0;
        PictureBox _PictureBox = null;
        Label _LblStatus = null;
        Label _LblEmployeeId = null;
        Label _LblEmployeeName = null;


        #endregion
        public void CompareImage(byte[] attachedFingerdata, byte[] DataAttachedFingerData)
        {

            try
            {
                MFS.GInstance.MatchANSI(attachedFingerdata, DataAttachedFingerData, ref ImagesValidateScore);
            }
            catch (Exception)
            {

                throw;
            }

        }
        List<byte[]> ListByteArray = null;
        public string ReturnWelcomeMessage()
        {
            try
            {
                int Hours = 0;
                string ReturnValue = string.Empty;
                string AM_PM = DateTime.Now.ToString("tt", CultureInfo.InvariantCulture);
                Hours = Convert.ToInt32(DateTime.Now.ToString("HH"));
                ReturnValue = "Welcome ";
                if (Hours > 12)
                    Hours = Hours - 12;
                if ((Hours >= 6 && Hours <= 12) && AM_PM == "AM")
                    ReturnValue = "Good Morning ";
                if ((Hours >= 1 && Hours <= 4) && AM_PM == "PM")
                    ReturnValue = "Good Afternoon ";
                if ((Hours >= 4 && Hours <= 9) && AM_PM == "PM")
                    ReturnValue = "Good Evening ";
                return ReturnValue;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void CompareFingerImageData(PictureBox employeeImage, Label status, Label employeeID, Label employeeName, Byte[] fingerImageData)
        {
            try
            {
                _FingerImageData = fingerImageData;
                _PictureBox = employeeImage;
                _LblStatus = status;
                _LblEmployeeId = employeeID;
                _LblEmployeeName = employeeName;
                if (CompleteDataForValidateRecords == null)
                {
                    ObjRegister = new Register();
                   
                    CompleteDataForValidateRecords = ObjRegister.GetRecordsBasedOnThumbExpression();
                }

                Task TT = Task.Run(() =>
                {
                    if (CompleteDataForValidateRecords.Rows.Count > Common.Zero)
                    {
                        for (int i = 0; i < CompleteDataForValidateRecords.Rows.Count; i++)
                        {
                            ListByteArray = new List<byte[]>();
                            _PictureBox.Image = null;
                            byte[] FirstImageByte = (byte[])CompleteDataForValidateRecords.Rows[i]["ThumbImpression"];
                            byte[] SecondImageByte = (byte[])CompleteDataForValidateRecords.Rows[i]["ThumbImpression2"];
                            byte[] ThridImageByte = (byte[])CompleteDataForValidateRecords.Rows[i]["ThumbImpression3"];
                            ListByteArray.Add(FirstImageByte);
                            ListByteArray.Add(SecondImageByte);
                            ListByteArray.Add(ThridImageByte);
                            byte[] Bytes = (byte[])CompleteDataForValidateRecords.Rows[i]["EmployeeImage"];
                            foreach (Byte[] FingerData in ListByteArray)
                            {
                                CompareImage(_FingerImageData, FingerData);
                                if (ImagesValidateScore > Common.MatchThreshold)
                                {
                                    if (ObjRegister.IN_AttendanceLog(CompleteDataForValidateRecords.Rows[i]["EmployeeID"].ToString()) <= Common.Zero)
                                    {
                                        MFS.GInstance.StartCapture(Common.Quentity, Common.Timer, true);
                                        _LblStatus.Text = "No Allowed ";
                                    }
                                    else
                                    {
                                        _PictureBox.Image = (Bitmap)((new ImageConverter()).ConvertFrom(Bytes));
                                        _LblEmployeeId.Text = CompleteDataForValidateRecords.Rows[i]["EmployeeID"].ToString().Trim();
                                        _LblEmployeeName.Text = CompleteDataForValidateRecords.Rows[i]["Name"].ToString().Trim();
                                       
                                        _LblStatus.Text = string.Empty;
                                        _LblStatus.Text = ReturnWelcomeMessage() + _LblEmployeeName.Text;
                                        _LblStatus.ForeColor = Color.Green;
                                        i = CompleteDataForValidateRecords.Rows.Count;
                                    }
                                    break;
                                }
                                else
                                {

                                    if (_PictureBox.Image == null)
                                    {
                                        Task t = Task.Run(() =>
                              {
                                  MFS.GInstance.StartCapture(Common.Quentity, Common.Timer, true);
                              });

                                        t.Wait();
                                        _PictureBox.Image = null;
                                        _LblEmployeeId.Text = string.Empty;
                                        _LblEmployeeName.Text = string.Empty;
                                        _LblStatus.Text = "Please Try again";
                                       
                                    }
                                }
                            }
                            // MFS.GInstance.StartCapture(Common.Quentity, Common.Timer, false);
                        }
                    }
                    else
                        Common.MessageBoxInformation("No Records Found plz Contact to admin");
                });
                TT.Wait();
              
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                GC.Collect();
            }
        }

    }
}
