﻿using DPUruNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BAL;
using System.IO;
namespace Attendance_Master
{
    public partial class FormRegisterUser : Form
    {
        #region Global Variable
        private ReaderCollection _readers;
        private string FingerCaptrueReaderName = string.Empty;
        private Image _FirstImage = null;
        private Image _SecondImage = null;
        private Image _ThridImage = null;
        byte[] _strFirstFinger = null;
        byte[] _strSecondFinger = null;
        byte[] _strThridFinger = null;
        string CameraName = string.Empty;
        // When set by child forms, shows s/n and enables buttons.
        public Reader CurrentReader
        {
            get { return currentReader; }
            set
            {
                currentReader = value;
            }
        }
        private Reader currentReader;
        /// <summary>
        /// Holds fmds enrolled by the enrollment GUI.
        /// </summary>
        public Dictionary<int, Fmd> Fmds
        {
            get { return fmds; }
            set { fmds = value; }
        }
        private Dictionary<int, Fmd> fmds = new Dictionary<int, Fmd>();
        /// <summary>
        /// Reset the UI causing the user to reselect a reader.
        /// </summary>
        public bool Reset
        {
            get { return reset; }
            set { reset = value; }
        }
        private bool reset;
        private Fmd _firstFinger;
        private Fmd _secondFinger;
        private Fmd _ThridFinger;
        #endregion
        public FormRegisterUser()
        {
            InitializeComponent();
        }
        private bool ClearImageControls()
        {
            try
            {
                FirstFinger.Image = null;
                SecondFinger.Image = null;
                ThridFinger.Image = null;
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void RegisterUser_Load(object sender, EventArgs e)
        {
            try
            {
                BindImage(FirstFinger);
                BindOfficeList();
                //FaceRecognition ObjFaceRecoginition = new FaceRecognition();
                //ObjFaceRecoginition.GetCameraName(ref CameraName);
                this.ControlBox = false;
            }
            catch (Exception ex)
            {
                Common.MessageBoxError(ex.ToString());
            }
            #region Finger Scanner
            //if (FingerCaptrueReaderName == string.Empty)
            //{
            //    FingerCaptrueReaderName = FingerCapture.ReturnListofFingerCapture();
            //}
            //GetReaderList();
            //if (currentReader != null)
            //{
            //    if (ClearImageControls())
            //    {
            //        if (OpenReader())
            //        {
            //            StartCaptureAsync(this.OnCaptured);
            //        }
            //        else
            //        {
            //            MessageBox.Show("Something Wrong when trying to open Reader");
            //        }
            //    }
            //}
            #endregion
        }
        private void BindOfficeList()
        {
            Dictionary<int, string> Values = new Dictionary<int, string>();
            Values.Add(0, "--Select Office--");
            foreach (DataRow dr in Common.ReturnDataTableBasedOnStoreProcedure("GetOfficeList").Rows)
            {
                int key = Convert.ToInt32(dr["UID"]);
                string value = dr["OfficeName"].ToString();
                Values.Add(key, value);
            }
            CmbOffice.DataSource = Values.ToList();
            CmbOffice.ValueMember = "Key";
            CmbOffice.DisplayMember = "Value";
        }
       private void BindImage(PictureBox pictruebox)
        {
            MantraFingerScanner ObjFingerScanner = new MantraFingerScanner();
            ObjFingerScanner.MantraConnection(ref LblStatus, false,ref pictruebox);
            ObjFingerScanner._SecondPictureBox = SecondFinger;
            ObjFingerScanner._ThirdPicturebox = ThridFinger;
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
         if(ThridFinger.Image!=null)
         {
             _strFirstFinger = MantraFingerScanner._FirstImageData;
             _strSecondFinger = MantraFingerScanner._SecondImageData;
             _strThridFinger = MantraFingerScanner._ThridImageData;
         }
            bool Isvalidate = true;
            string errorStringValue = string.Empty;
            ValidateControls(ref Isvalidate, ref errorStringValue);
            if (Isvalidate)
            {
                if (_strFirstFinger != null && _strSecondFinger != null && _strThridFinger != null)
                {
                    Register ObjRegister = new Register();
                    string IsSuccessInsertData = ObjRegister.InsertDataForRegisterUser(txtEmployeeId.Text.Trim(), txtName.Text.Trim(), txtAddress.Text.Trim(),Convert.ToString(CmbOffice.SelectedValue), 1, _strFirstFinger, _strSecondFinger, _strThridFinger);
                    if (IsSuccessInsertData == Convert.ToString( Common.IsSuccess.Sucess))
                    {
                        BtnReSet_Click(sender, e);
                        Common.MessageBoxInformation("Your Data successfully inserted");
                    }
                }
            }
            else
                Common.MessageBoxError(errorStringValue);
        }
        public void ValidateControls(ref bool isValidate, ref string errorstring)
        {
            if (!Common.ValidateStringValue(txtName.Text.Trim()))
            {
                errorstring = "Please Enter Name";
                isValidate = false;
                return;
            }
            if (!Common.ValidateStringValue(txtAddress.Text.Trim()))
            {
                errorstring = "Please Enter Address";
                isValidate = false;
                return;
            }
            if(CmbOffice.Text=="--Select Office--")
            {
                errorstring = "Please Select Valid Office";
                isValidate = false;
                return;
            }
            if (!Common.ValidateStringValue(txtEmployeeId.Text.Trim()))
            {
                errorstring = "Please Enter Employee ID ";
                isValidate = false;
                return;
            }
        }
        public static Bitmap LoadBitmap(string path)
        {
            //Open file in read only mode
            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            //Get a binary reader for the file stream
            using (BinaryReader reader = new BinaryReader(stream))
            {
                //copy the content of the file into a memory stream
                var memoryStream = new MemoryStream(reader.ReadBytes((int)stream.Length));
                //make a new Bitmap object the owner of the MemoryStream
                return new Bitmap(memoryStream);
            }
        }
        public byte[] imageToByteArray(Image image)
        {
            using (var ms = new MemoryStream())
            {
                ImageConverter converter = new ImageConverter();
                byte[] imgArray = (byte[])converter.ConvertTo(image, typeof(byte[]));
                return imgArray;
            }
        }
        private void BtnReSet_Click(object sender, EventArgs e)
        {
            _strFirstFinger = null;
            _strSecondFinger = null;
            _strThridFinger = null;
            _FirstImage = null;
            _firstFinger = null;
            _secondFinger = null;
            _SecondImage = null;
            _ThridFinger = null;
            FirstFinger.Image = null;
            SecondFinger.Image = null;
            ThridFinger.Image = null;
            txtName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtEmployeeId.Text = string.Empty;
            _ThridImage = null;
        }
        private void txtEmployeeId_Leave(object sender, EventArgs e)
        {
            Register ObjRegister = new Register();
            if (!Common.ValidateStringValue(txtEmployeeId.Text))
            {
                Common.ValidateStringValue("Please Enter Valid Employee Id");
            }
            if (Common.ValidateStringValue(ObjRegister.GetEmployeeForValidateRecords(txtEmployeeId.Text)))
            {
                Common.MessageBoxInformation("This Employee ID already used in Database, Contact To Admin");
                txtEmployeeId.Focus();
            }
        }
        #region Not in Use...
        private void GetReaderList()
        {
            _readers = ReaderCollection.GetReaders();
            foreach (Reader reader in _readers)
            {
                currentReader = reader;
                break;
            }
            if (currentReader == null)
            {
                MessageBox.Show("No Reader Found!!!");
            }
        }
           /// <summary>
        /// Open a device and check result for errors.
        /// </summary>
        /// <returns>Returns true if successful; false if unsuccessful</returns>
        public bool OpenReader()
        {
            reset = false;
            Constants.ResultCode result = Constants.ResultCode.DP_DEVICE_FAILURE;
            if (currentReader.Status != null)
                currentReader.Dispose();
            // Open reader
            result = currentReader.Open(Constants.CapturePriority.DP_PRIORITY_COOPERATIVE);
            if (result != Constants.ResultCode.DP_SUCCESS)
            {
                MessageBox.Show("Error:  " + result);
                reset = true;
                return false;
            }
            return true;
        }
        /// <summary>
        /// Close window.
        /// </summary>
        private void btnBack_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Hookup capture handler and start capture.
        /// </summary>
        /// <param name="OnCaptured">Delegate to hookup as handler of the On_Captured event</param>
        /// <returns>Returns true if successful; false if unsuccessful</returns>
        public bool StartCaptureAsync(Reader.CaptureCallback OnCaptured)
        {
            // Activate capture handler
            currentReader.On_Captured += new Reader.CaptureCallback(OnCaptured);
            // Call capture
            if (!CaptureFingerAsync())
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Check the device status before starting capture.
        /// </summary>
        /// <returns></returns>
        public void GetStatus()
        {
            Constants.ResultCode result = currentReader.GetStatus();
            if ((result != Constants.ResultCode.DP_SUCCESS))
            {
                if (CurrentReader != null)
                {
                    CurrentReader.Dispose();
                    CurrentReader = null;
                }
                throw new Exception("" + result);
            }
            if ((currentReader.Status.Status == Constants.ReaderStatuses.DP_STATUS_BUSY))
            {
                Thread.Sleep(50);
            }
            else if ((currentReader.Status.Status == Constants.ReaderStatuses.DP_STATUS_NEED_CALIBRATION))
            {
                currentReader.Calibrate();
            }
            else if ((currentReader.Status.Status != Constants.ReaderStatuses.DP_STATUS_READY))
            {
                throw new Exception("Reader Status - " + currentReader.Status.Status);
            }
        }
        /// <summary>
        /// Function to capture a finger. Always get status first and calibrate or wait if necessary.  Always check status and capture errors.
        /// </summary>
        /// <param name="fid"></param>
        /// <returns></returns>
        public bool CaptureFingerAsync()
        {
            try
            {
                GetStatus();
                Constants.ResultCode captureResult = currentReader.CaptureAsync(Constants.Formats.Fid.ANSI, Constants.CaptureProcessing.DP_IMG_PROC_DEFAULT, currentReader.Capabilities.Resolutions[0]);
                if (captureResult != Constants.ResultCode.DP_SUCCESS)
                {
                    reset = true;
                    throw new Exception("" + captureResult);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:  " + ex.Message);
                return false;
            }
        }
        /// <summary>
        /// Handler for when a fingerprint is captured.
        /// </summary>
        /// <param name="captureResult">contains info and data on the fingerprint capture</param>
        public void OnCaptured(CaptureResult captureResult)
        {
            try
            {
                DataResult<Fmd> resultConversion = FeatureExtraction.CreateFmdFromFid(captureResult.Data, Constants.Formats.Fmd.ANSI);
                if (resultConversion.ResultCode != Constants.ResultCode.DP_SUCCESS)
                {
                    Reset = true;
                    throw new Exception(resultConversion.ResultCode.ToString());
                }
                // Check capture quality and throw an error if bad.
                if (!CheckCaptureResult(captureResult)) return;
                PassFingreDataForCompare(resultConversion);
                // Create bitmap
                foreach (Fid.Fiv fiv in captureResult.Data.Views)
                {
                    SendMessage(Action.SendBitmap, CreateBitmap(fiv.RawImage, fiv.Width, fiv.Height));
                }
            }
            catch (Exception ex)
            {
                // Send error message, then close form
                SendMessage(Action.SendMessage, "Error:  " + ex.Message);
            }
        }
        public void PassFingreDataForCompare(DataResult<Fmd> resultConversion)
        {
            //if (_firstFinger == null)
            //{
            //    _strFirstFinger = Fmd.SerializeXml(resultConversion.Data);
            //    _firstFinger = resultConversion.Data;
            //    return;
            //}
            //if (_secondFinger == null)
            //{
            //    _strSecondFinger = Fmd.SerializeXml(resultConversion.Data);
            //    _secondFinger = resultConversion.Data;
            //    return;
            //}
            //if (_ThridFinger == null)
            //{
            //    _strThridFinger = Fmd.SerializeXml(resultConversion.Data);
            //    _ThridFinger = resultConversion.Data;
            //}
        }
        /// <summary>
        /// Create a bitmap from raw data in row/column format.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public Bitmap CreateBitmap(byte[] bytes, int width, int height)
        {
            byte[] rgbBytes = new byte[bytes.Length * 3];
            for (int i = 0; i <= bytes.Length - 1; i++)
            {
                rgbBytes[(i * 3)] = bytes[i];
                rgbBytes[(i * 3) + 1] = bytes[i];
                rgbBytes[(i * 3) + 2] = bytes[i];
            }
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
            for (int i = 0; i <= bmp.Height - 1; i++)
            {
                IntPtr p = new IntPtr(data.Scan0.ToInt64() + data.Stride * i);
                System.Runtime.InteropServices.Marshal.Copy(rgbBytes, i * bmp.Width * 3, p, bmp.Width * 3);
            }
            bmp.UnlockBits(data);
            return bmp;
        }
        /// <summary>
        /// Check quality of the resulting capture.
        /// </summary>
        public bool CheckCaptureResult(CaptureResult captureResult)
        {
            if (captureResult.Data == null)
            {
                if (captureResult.ResultCode != Constants.ResultCode.DP_SUCCESS)
                {
                    reset = true;
                    throw new Exception(captureResult.ResultCode.ToString());
                }
                // Send message if quality shows fake finger
                if ((captureResult.Quality != Constants.CaptureQuality.DP_QUALITY_CANCELED))
                {
                    throw new Exception("Quality - " + captureResult.Quality);
                }
                return false;
            }
            return true;
        }
        #region SendMessage
        private enum Action
        {
            SendBitmap,
            SendMessage
        }
        private delegate void SendMessageCallback(Action action, object payload);
        private void SendMessage(Action action, object payload)
        {
            try
            {
                if (this.FirstFinger.InvokeRequired)
                {
                    SendMessageCallback d = new SendMessageCallback(SendMessage);
                    this.Invoke(d, new object[] { action, payload });
                }
                else
                {
                    switch (action)
                    {
                        case Action.SendBitmap:
                            if (FirstFinger.Image == null)
                            {
                                _FirstImage = (Bitmap)payload;
                                FirstFinger.Image = Common.ResizeImage((Bitmap)payload, 200, 200);
                                FirstFinger.Refresh();
                                break;
                            }
                            if (SecondFinger.Image == null)
                            {
                                bool isNoDuplicateFound = false;
                                ValidateFingre(true, false, ref isNoDuplicateFound);
                                if (isNoDuplicateFound)
                                {
                                    _SecondImage = (Bitmap)payload;
                                    SecondFinger.Image = Common.ResizeImage((Bitmap)payload, 200, 200);
                                    SecondFinger.Refresh();
                                }
                                else
                                {
                                    Common.MessageBoxInformation("No allowed same Fingre");
                                }
                                break;
                            }
                            if (ThridFinger.Image == null)
                            {
                                bool isNoDuplicateFound = false;
                                ValidateFingre(false, true, ref isNoDuplicateFound);
                                if (isNoDuplicateFound)
                                {
                                    _ThridImage = (Bitmap)payload;
                                    ThridFinger.Image = Common.ResizeImage((Bitmap)payload, 200, 200);
                                    ThridFinger.Refresh();
                                }
                                else
                                {
                                    Common.MessageBoxInformation("No allowed same Fingre");
                                }
                                break;
                            }
                            break;
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        private void ValidateFingre(bool IsSecondFinger, bool isThridFingre, ref bool isSuccess)
        {
            if (IsSecondFinger)
            {
                CompareResult compareResult = Comparison.Compare(_firstFinger, 0, _secondFinger, 0);
                if (compareResult.Score == Common.IsSameFinger)
                {
                    SecondFinger.Image = null;
                    _secondFinger = null;
                    isSuccess = false;
                    //throw new Exception(compareResult.ResultCode.ToString());
                }
                else
                    isSuccess = true;
            }
            if (isThridFingre)
            {
                CompareResult compareResult = Comparison.Compare(_firstFinger, 0, _ThridFinger, 0);
                if (compareResult.Score != Common.IsSameFinger)
                {
                    CompareResult SecondORThrid = Comparison.Compare(_secondFinger, 0, _ThridFinger, 0);
                    if (SecondORThrid.Score == Common.IsSameFinger)
                    {
                        _ThridFinger = null;
                        ThridFinger.Image = null;
                        isSuccess = false;
                    }
                    else
                        isSuccess = true;
                }
                else
                {
                    _ThridFinger = null;
                    ThridFinger.Image = null;
                    isSuccess = false;
                }
            }
        }
        #endregion
        #endregion
        private void BtnClose_Click(object sender, EventArgs e)
        {
            Common.CloseMantraConnection();
            this.Close();
        }
    }
}
