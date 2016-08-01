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
using CarlosAg.ExcelXmlWriter;
using System.Data;
namespace Attendance_Master
{
    public partial class GetRecords : Form
    {
        int Year = 0;
        int Month = 0;
        int Days = 0;
        public GetRecords()
        {
            InitializeComponent();
            BindEmployeeName();
        }

        private void BindEmployeeName()
        {
            cmbEmployeeName.DataSource = Common.ReturnDataTableBasedOnStoreProcedure("GetRecordsEmployee");
            cmbEmployeeName.ValueMember = "UID";
            cmbEmployeeName.DisplayMember = "Name";
        }

        int Index = 0;

        private int IndexIncrement()
        {
            Index += 1;
            return Index;
        }
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            Index = 0;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = @"D:\";
            saveFileDialog1.Title = "Save text Files";
            //saveFileDialog1.CheckFileExists = true;
            //saveFileDialog1.CheckPathExists = true;
            saveFileDialog1.DefaultExt = "txt";
            saveFileDialog1.Filter = "Excel files (*.xls)|*.xls";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            string fileName = string.Empty;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = saveFileDialog1.FileName;
            }
            CarlosAg.ExcelXmlWriter.Workbook or = new Workbook();

            //Add a workbook

            CarlosAg.ExcelXmlWriter.Workbook book = new CarlosAg.ExcelXmlWriter.Workbook();

            // Specify which Sheet should be opened and the size of window by default
            book.ExcelWorkbook.ActiveSheetIndex = 1;
            book.ExcelWorkbook.WindowTopX = 100;
            book.ExcelWorkbook.WindowTopY = 200;
            book.ExcelWorkbook.WindowHeight = 7000;
            book.ExcelWorkbook.WindowWidth = 8000;

            // Some optional properties of the Document
            book.Properties.Author = "Joginder Singh";
            book.Properties.Title = "Excel Export";
            book.Properties.Created = DateTime.Now;

            //Add styles to the workbook 
            WorksheetStyle s31 = book.Styles.Add("s31");
            s31.Font.FontName = "Tahoma";
            s31.Font.Size = 8;
            s31.Font.Color = "#000000";
            s31.Alignment.Horizontal = StyleHorizontalAlignment.Center;
            s31.Alignment.Vertical = StyleVerticalAlignment.Center;
            s31.Alignment.WrapText = true;
            s31.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1);
            s31.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1);
            s31.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1);
            s31.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1);
            s31.NumberFormat = "@";

            // Add styles for header of the Workbook
            WorksheetStyle style = book.Styles.Add("HeaderStyle");
            style.Font.FontName = "Tahoma";
            style.Font.Size = 10;
            // style.Font.Bold = true;
            style.Alignment.Horizontal = StyleHorizontalAlignment.Center;
            style.Font.Color = "Black";
            style.Interior.Color = "White";
            //style.Interior.Pattern = StyleInteriorPattern.DiagCross;


            // Add a Worksheet with some data
            Worksheet sheet = book.Worksheets.Add(Common.ConvertMonth(Convert.ToDateTime( dtFrom.Text).ToShortDateString()));
         
            DataTable Obj = new System.Data.DataTable();
            Obj = ReturnColumnName();

            DataTable GetRecords = new System.Data.DataTable();
            GetRecord ObjRegister = new GetRecord();

            DataTable GetEmployeeID = Common.ReturnDataTableBasedOnStoreProcedure("GetRecordsEmployee");


            for (int Employee = 0; Employee < GetEmployeeID.Rows.Count; Employee++)
            {


                WorksheetRow TitleRows = sheet.Table.Rows.Add();
                TitleRows.Index = IndexIncrement();
                TitleRows.Height = 26;
                TitleRows.AutoFitHeight = true;


                int EmployeeID = Convert.ToInt32(GetEmployeeID.Rows[Employee]["Name"].ToString().Split('(')[1].Split(')')[0]);
                GetRecords = ObjRegister.GetRecordsAttendanceLog(EmployeeID, Convert.ToDateTime(dtFrom.Text));


                bool AddEmptyCell = false;
                for (int i = 1; i <= Obj.Columns.Count; i++)
                {
                    if (i == Obj.Columns.Count / 2)
                    {
                        WorksheetCell wcHeader = new WorksheetCell("Employee ID : "+ GetEmployeeID.Rows[Employee]["Name"].ToString().Split('(')[1].Split(')')[0]+" Name : "+ GetEmployeeID.Rows[Employee]["Name"].ToString().Split('(')[0], "HeaderStyle");
                        TitleRows.Cells.Add(wcHeader);
                    }
                    else
                    {
                        WorksheetCell wcHeader = new WorksheetCell();
                        TitleRows.Cells.Add(wcHeader);
                    }

                }

                //Add row with some properties
                WorksheetRow row = sheet.Table.Rows.Add();
                row.Index = IndexIncrement();
                row.Height = 26;
                row.AutoFitHeight = true;

                WorksheetRow intime1 = sheet.Table.Rows.Add();
                intime1.Index = IndexIncrement();
                intime1.Height = 26;
                intime1.AutoFitHeight = true;

                WorksheetRow intime2 = sheet.Table.Rows.Add();
                intime2.Index = IndexIncrement();
                intime2.Height = 26;
                intime2.AutoFitHeight = true;
                WorksheetRow intime3 = sheet.Table.Rows.Add();
                intime3.Index = IndexIncrement();
                intime3.Height = 26;
                intime3.AutoFitHeight = true;
                WorksheetRow intime4 = sheet.Table.Rows.Add();
                intime4.Index = IndexIncrement();
                intime4.Height = 26;
                intime4.AutoFitHeight = true;
                WorksheetRow intime6 = sheet.Table.Rows.Add();
                intime6.Index = IndexIncrement();
                intime6.Height = 26;
                intime6.AutoFitHeight = true;
                WorksheetRow intime7 = sheet.Table.Rows.Add();
                intime7.Index = IndexIncrement();
                intime7.Height = 26;
                intime7.AutoFitHeight = true;
                WorksheetRow Status = sheet.Table.Rows.Add();
                Status.Index = IndexIncrement();
                Status.Height = 26;
                Status.AutoFitHeight = true;

                WorksheetRow TotalReports = sheet.Table.Rows.Add();
                TotalReports.Index = IndexIncrement();
                TotalReports.Height = 26;
                TotalReports.AutoFitHeight = true;
                int Persent = 0, WeekOff = 0, MISPunch = 0, Abbsent = 0;

                for (int i = 1; i <= Obj.Columns.Count; i++)
                {
                    bool Intime1 = false, Intime2 = false, Intime3 = false, Intime4 = false;
                    bool Empty = false, Empty1 = false, Empty2 = false, Empty3 = false, isMIS = false;

                    if (i < Obj.Columns.Count / 2)
                    {
                        WorksheetCell ReporstStatus = new WorksheetCell();
                        TotalReports.Cells.Add(ReporstStatus);
                    }



                    if (!AddEmptyCell)
                    {
                        WorksheetCell EmptyCell = new WorksheetCell("DAY", "HeaderStyle");
                        row.Cells.Add(EmptyCell);
                        AddEmptyCell = true;
                        WorksheetCell IN = new WorksheetCell("IN", "HeaderStyle");
                        intime1.Cells.Add(IN);
                        WorksheetCell Out = new WorksheetCell("OUT", "HeaderStyle");
                        intime2.Cells.Add(Out);
                        WorksheetCell IN1 = new WorksheetCell("IN1", "HeaderStyle");
                        intime3.Cells.Add(IN1);
                        WorksheetCell OUT1 = new WorksheetCell("OUT1", "HeaderStyle");
                        intime4.Cells.Add(OUT1);
                        WorksheetCell IN2 = new WorksheetCell("IN2", "HeaderStyle");
                        intime6.Cells.Add(IN2);
                        WorksheetCell OUT2 = new WorksheetCell("OUT2", "HeaderStyle");
                        intime7.Cells.Add(OUT2);
                        WorksheetCell StatusHeader = new WorksheetCell("Status", "HeaderStyle");
                        Status.Cells.Add(StatusHeader);
                        WorksheetCell StatusTotal = new WorksheetCell("Reports", "HeaderStyle");
                        TotalReports.Cells.Add(StatusTotal);
                    }
                    WorksheetCell wcHeader = new WorksheetCell(i.ToString(), "HeaderStyle");
                    row.Cells.Add(wcHeader);

                    int CountNumber = 0;
                    bool IsPersent = false;
                    for (int j = 0; j < GetRecords.Rows.Count; j++)
                    {
                        CountNumber += 1;



                        if (Convert.ToInt32(GetRecords.Rows[j]["INOUTDate"]) == i)
                        {

                            IsPersent = true;
                            if (Intime4)
                            {
                                isMIS = false;
                                WorksheetCell INOutTime = new WorksheetCell(GetRecords.Rows[j]["INOUTTime"].ToString(), "HeaderStyle");
                                intime4.Cells.Add(INOutTime);
                                Empty3 = true;

                            }
                            else
                                if (Intime3)
                                {
                                    isMIS = true;
                                    WorksheetCell INOutTime = new WorksheetCell(GetRecords.Rows[j]["INOUTTime"].ToString(), "HeaderStyle");
                                    intime3.Cells.Add(INOutTime);
                                    Intime4 = true;
                                    Empty2 = true;

                                }
                                else
                                    if (Intime2)
                                    {
                                        isMIS = false;
                                        WorksheetCell INOutTime = new WorksheetCell(GetRecords.Rows[j]["INOUTTime"].ToString(), "HeaderStyle");
                                        intime2.Cells.Add(INOutTime);
                                        Intime3 = true;
                                        Empty1 = true;
                                    }
                                    else
                                    {
                                        isMIS = true;
                                        Intime1 = true;
                                        WorksheetCell INOutTime = new WorksheetCell(GetRecords.Rows[j]["INOUTTime"].ToString(), "HeaderStyle");
                                        intime1.Cells.Add(INOutTime);
                                        Intime2 = true;
                                        Empty = true;

                                    }

                        }

                        if (CountNumber == GetRecords.Rows.Count)
                        {
                            if (!Empty)
                            {
                                Empty = true;
                                WorksheetCell INOutTime = new WorksheetCell();
                                intime1.Cells.Add(INOutTime);
                            }
                            if (!Empty1)
                            {
                                Empty1 = true;
                                WorksheetCell INOutTime1 = new WorksheetCell();
                                intime2.Cells.Add(INOutTime1);
                            }
                            if (!Empty2)
                            {
                                Empty2 = true;
                                WorksheetCell INOutTime2 = new WorksheetCell();
                                intime3.Cells.Add(INOutTime2);
                            }
                            if (!Empty3)
                            {
                                Empty3 = true;
                                WorksheetCell INOutTime3 = new WorksheetCell();
                                intime4.Cells.Add(INOutTime3);
                            }
                            if (IsPersent)
                            {
                                string PersentORMIS = string.Empty;
                                if (isMIS)
                                {
                                    MISPunch += 1;
                                    PersentORMIS = "MIS";
                                }
                                else
                                {
                                    Persent += 1;
                                    PersentORMIS = "P";
                                }
                                WorksheetCell WorkStatus = new WorksheetCell(PersentORMIS, "HeaderStyle");
                                Status.Cells.Add(WorkStatus);
                            }
                            else
                            {
                                int Month = Convert.ToDateTime(dtFrom.Text).Month;
                                int Year = Convert.ToDateTime(dtFrom.Text).Year;
                                int Day = i;
                                string TempDate = Convert.ToDateTime(Day + "-" + Month + "-" + Year).ToShortDateString();
                                string SundayORAbbesent = string.Empty;
                                if (Convert.ToDateTime(TempDate).DayOfWeek == DayOfWeek.Sunday)
                                {
                                    WeekOff += 1;
                                    SundayORAbbesent = "WO";
                                }
                                else
                                {
                                    Abbsent += 1;
                                    SundayORAbbesent = "A";

                                }
                                WorksheetCell WorkStatus = new WorksheetCell(SundayORAbbesent, "HeaderStyle");
                                Status.Cells.Add(WorkStatus);
                            }

                        }

                    }


                }


                string Reports = "Reports OF Month Week Off=" + WeekOff + " Persent= " + Persent + " Mis Punch= " + MISPunch + " Abbsent =" + Abbsent + "";
                WorksheetCell ReporstStatusAdd = new WorksheetCell(Reports, "HeaderStyle");
                TotalReports.Cells.Add(ReporstStatusAdd);



                //Save the work book
                book.Save(fileName);


            }
        }

        public System.Data.DataTable ReturnColumnName()
        {
            System.Data.DataTable table = new System.Data.DataTable();

            ReturnMonthandDays(dtFrom.Text, ref Year, ref Month, ref Days);
            for (int i = 0; i < Days; i++)
            {
                table.Columns.Add(i.ToString(), typeof(int));
            }
            return table;
        }
        public void ReturnMonthandDays(string values, ref int year, ref int month, ref int days)
        {
            year = Convert.ToDateTime(values).Year;
            month = Convert.ToDateTime(values).Month;
            days = DateTime.DaysInMonth(year, month);

        }

    }
}
