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
namespace Attendance_Master
{
    public partial class PunchMaster : Form
    {
        public PunchMaster()
        {
            InitializeComponent();
            timer1.Enabled = true;
            timer1.Interval = 1000;
        }

        #region Global Veriable


        private ReaderCollection _readers;

        private string FingerCaptrueReaderName = string.Empty;





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
        private DataTable CompleteDataForValidateRecords = null;
        #endregion

        private void GetReaderList()
        {



            _readers = ReaderCollection.GetReaders();
            currentReader = null;
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
        private void PunchMaster_Load(object sender, EventArgs e)
        {

            // Register ObjRegister = new Register();
            //DataTable dt= ObjRegister.GetRecordsBasedOnThumbExpression();
            //for (int i = 0; i < dt.Rows.Count; i++)
            // {

            //     photo_aray = (byte[])dt.Rows[0]["EmployeeImage"];
            //     MemoryStream ms = new MemoryStream(photo_aray);
            //     pictureBox1.Image = Image.FromStream(ms);
            // } 

            if (FingerCaptrueReaderName == string.Empty)
            {
                FingerCaptrueReaderName = FingerCapture.ReturnListofFingerCapture();
            }

            GetReaderList();
            if (currentReader != null)
            {
                //if( currentReader.Status.Status==



                if (OpenReader())
                {

                    StartCaptureAsync(this.OnCaptured);

                }




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
                    SendMessage(Action.SendBitmap, Common.CreateBitmap(fiv.RawImage, fiv.Width, fiv.Height));

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


            _firstFinger = resultConversion.Data;



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



        #region MyRegion

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
                        //case Action.SendMessage:
                        //    MessageBox.Show((string)payload);
                        //    break;
                        case Action.SendBitmap:
                            {
                                FirstFinger.Image = Common.ResizeImage((Bitmap)payload, 200, 200);
                                Register ObjRegister = new Register();
                                if (CompleteDataForValidateRecords == null)
                                {
                                    CompleteDataForValidateRecords = ObjRegister.GetRecordsBasedOnThumbExpression();
                                }
                                if (CompleteDataForValidateRecords.Rows.Count > Common.Zero)
                                {
                                    for (int i = 0; i < CompleteDataForValidateRecords.Rows.Count; i++)
                                    {
                                        pictureBox1.Image = null;
                                        List<Fmd> ObjFMDHoldImageForComparison = new List<Fmd>();
                                        ObjFMDHoldImageForComparison.Add(Fmd.DeserializeXml(CompleteDataForValidateRecords.Rows[i]["ThumbImpression"].ToString()));
                                        ObjFMDHoldImageForComparison.Add(Fmd.DeserializeXml(CompleteDataForValidateRecords.Rows[i]["ThumbImpression2"].ToString()));
                                        ObjFMDHoldImageForComparison.Add(Fmd.DeserializeXml(CompleteDataForValidateRecords.Rows[i]["ThumbImpression3"].ToString()));
                                        byte[] Bytes = (byte[])CompleteDataForValidateRecords.Rows[i]["EmployeeImage"];
                                        foreach (Fmd FingerData in ObjFMDHoldImageForComparison)
                                        {

                                            CompareResult compareResult = Comparison.Compare(_firstFinger, 0, FingerData, 0);


                                            if (compareResult.Score == Common.IsValidateFinger && compareResult.ResultCode == Constants.ResultCode.DP_SUCCESS)
                                            {
                                                if (ObjRegister.IN_AttendanceLog(CompleteDataForValidateRecords.Rows[i]["EmployeeID"].ToString()) <= Common.Zero)
                                                {
                                                    lblStatus.Text = "No Allowed ";
                                                }
                                                else
                                                {
                                                    pictureBox1.Image = (Bitmap)((new ImageConverter()).ConvertFrom(Bytes));
                                                    lblEmployeeId.Text = CompleteDataForValidateRecords.Rows[i]["EmployeeID"].ToString().Trim();
                                                    lblEmployeeName.Text = CompleteDataForValidateRecords.Rows[i]["Name"].ToString().Trim();
                                                    lblEmployeeId.Visible = true;
                                                    lblEmployeeName.Visible = true;
                                                    lblStatus.Text = string.Empty;

                                                    lblStatus.Text = ReturnWelcomeMessage() + lblEmployeeName.Text;
                                                    lblStatus.ForeColor = Color.Green;

                                                    i = CompleteDataForValidateRecords.Rows.Count;
                                                }

                                                break;
                                            }
                                            else
                                            {
                                                if (pictureBox1.Image == null)
                                                {
                                                    pictureBox1.Image = null;
                                                    lblEmployeeId.Text = string.Empty;
                                                    lblEmployeeName.Text = string.Empty;
                                                    lblStatus.Text = "Please Try again";
                                                    lblEmployeeId.Visible = false;

                                                    lblEmployeeName.Visible = false;

                                                }
                                            }
                                        }


                                    }
                                }
                                else
                                    Common.MessageBoxInformation("No Records Found plz Contact to admin");


                            }


                            FirstFinger.Refresh();
                            break;

                    }
                }
            }
            catch (Exception ex)
            {
                Common.MessageBoxError(ex.ToString());
            }
        }

        public string ReturnWelcomeMessage()
        
        {
            int TempValue=0;
            string ReturnValue = string.Empty;
          string AM_PM=  DateTime.Now.ToString("tt", CultureInfo.InvariantCulture);
            TempValue =Convert.ToInt32( DateTime.Now.ToString("HH"));
            ReturnValue = "Welcome ";            
            if (TempValue > 12)
                TempValue = TempValue - 12;
           if((TempValue>=6 && TempValue<=12) &&  AM_PM=="AM")
              ReturnValue = "Good Morning ";
           if ((TempValue >= 1 && TempValue <= 4) && AM_PM == "PM")
               ReturnValue = "Good Afternoon ";
           if ((TempValue >= 4 && TempValue <= 9) && AM_PM == "PM")
               ReturnValue = "Good Evening ";             
           return ReturnValue;

        }



        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            digitalDisplayControl1.DigitText = DateTime.Now.ToString("HH:mm:ss");
        }

    }
}
