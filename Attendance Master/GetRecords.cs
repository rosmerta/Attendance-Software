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

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            CarlosAg.ExcelXmlWriter.Workbook or = new Workbook();

            //Add a workbook
            string fileName = @"C:\Sample.xls";
            CarlosAg.ExcelXmlWriter.Workbook book = new CarlosAg.ExcelXmlWriter.Workbook();

            // Specify which Sheet should be opened and the size of window by default
            book.ExcelWorkbook.ActiveSheetIndex = 1;
            book.ExcelWorkbook.WindowTopX = 100;
            book.ExcelWorkbook.WindowTopY = 200;
            book.ExcelWorkbook.WindowHeight = 7000;
            book.ExcelWorkbook.WindowWidth = 8000;

            // Some optional properties of the Document
            book.Properties.Author = "Murali";
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
            Worksheet sheet = book.Worksheets.Add("Sample Data");
            // we can optionally set some column settings
            //sheet.Table.Columns.Add(new WorksheetColumn(100));
            //sheet.Table.Columns.Add(new WorksheetColumn(100));
            //sheet.Table.Columns.Add(new WorksheetColumn(250));

            WorksheetRow TitleRows = sheet.Table.Rows.Add();
            TitleRows.Index = 0;
            TitleRows.Height = 26;
            TitleRows.AutoFitHeight = true;





            //Add header text for the columns	
            DataTable Obj = new System.Data.DataTable();
            Obj = ReturnColumnName();

            DataTable GetRecords = new System.Data.DataTable();
            GetRecord ObjRegister = new GetRecord();

            GetRecords = ObjRegister.GetRecordsAttendanceLog(1065, Convert.ToDateTime(dtFrom.Text));


            bool AddEmptyCell = false;



            for (int i = 1; i <= Obj.Columns.Count; i++)
            {
                if (i == Obj.Columns.Count / 2)
                {
                    WorksheetCell wcHeader = new WorksheetCell("Joginder Singh Banger", "HeaderStyle");
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
            row.Index = 1;
            row.Height = 26;
            row.AutoFitHeight = true;

            WorksheetRow intime1 = sheet.Table.Rows.Add();
            intime1.Index = 2;
            intime1.Height = 26;
            intime1.AutoFitHeight = true;
            WorksheetRow intime2 = sheet.Table.Rows.Add();
            intime2.Index = 3;
            intime2.Height = 26;
            intime2.AutoFitHeight = true;
            WorksheetRow intime3 = sheet.Table.Rows.Add();
            intime3.Index = 4;
            intime3.Height = 26;
            intime3.AutoFitHeight = true;
            WorksheetRow intime4 = sheet.Table.Rows.Add();
            intime4.Index = 5;
            intime4.Height = 26;
            intime4.AutoFitHeight = true;

            for (int i = 1; i <= Obj.Columns.Count; i++)
            {
                bool Intime1 = false, Intime2 = false, Intime3 = false, Intime4 = false;
                bool Empty = false, Empty1 = false, Empty2 = false, Empty3 = false;

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
                }
                WorksheetCell wcHeader = new WorksheetCell(i.ToString(), "HeaderStyle");
                row.Cells.Add(wcHeader);

                int CountNumber = 0;
                for (int j = 0; j < GetRecords.Rows.Count; j++)
                {
                    CountNumber += 1;
                    if (Convert.ToInt32(GetRecords.Rows[j]["INOUTDate"]) == i)
                    {


                        if (Intime4)
                        {

                            WorksheetCell INOutTime = new WorksheetCell(GetRecords.Rows[j]["INOUTTime"].ToString(), "HeaderStyle");
                            intime4.Cells.Add(INOutTime);
                            Empty3 = true;

                        }
                        else
                            if (Intime3)
                            {

                                WorksheetCell INOutTime = new WorksheetCell(GetRecords.Rows[j]["INOUTTime"].ToString(), "HeaderStyle");
                                intime3.Cells.Add(INOutTime);
                                Intime4 = true;
                                Empty2 = true;

                            }
                            else
                                if (Intime2)
                                {

                                    WorksheetCell INOutTime = new WorksheetCell(GetRecords.Rows[j]["INOUTTime"].ToString(), "HeaderStyle");
                                    intime2.Cells.Add(INOutTime);
                                    Intime3 = true;
                                    Empty1 = true;
                                }
                                else
                                {

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

                    }

                }


            }



            //Save the work book
            book.Save(fileName);



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
