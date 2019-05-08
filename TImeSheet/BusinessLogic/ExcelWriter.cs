using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel=Microsoft.Office.Interop.Excel;

namespace TImeSheet.BusinessLogic
{
    public class ExcelWriter
    {
        // export to excell

        /// <summary>
        /// Dates in yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <param name="fromDate">yyyy-MM-dd HH:mm:ss</param>
        /// <param name="toDate">yyyy-MM-dd HH:mm:ss</param>
        public void WriteExportDataToExcel(string fromDate, string toDate)
        {
            DbTransaction dbTransaction = new DbTransaction();

            List<EmailTemplate> emailData = new List<EmailTemplate>();
            List<SentrifugoTemplate> sentrifugoData = new List<SentrifugoTemplate>();
            List<TimeSheetDetailsTable> allTimesheetData = new List<TimeSheetDetailsTable>();

            dbTransaction.GetExportData(fromDate, toDate, out emailData, out sentrifugoData, out allTimesheetData);

            var xlApplication = new Excel.Application();
            xlApplication.Workbooks.Add("");


            Excel._Workbook excelWorkbook = xlApplication.ActiveWorkbook;

            Excel._Worksheet emailWorkSheet = excelWorkbook.ActiveSheet;
            excelWorkbook.Sheets.Add(emailWorkSheet);
            Excel._Worksheet onlineWorkSheet = excelWorkbook.ActiveSheet;
            excelWorkbook.Sheets.Add(onlineWorkSheet);
            Excel._Worksheet allDetailsSheet = excelWorkbook.ActiveSheet;

            emailWorkSheet.Name = "Email";
            onlineWorkSheet.Name = "Sentrifugo";
            allDetailsSheet.Name = "Detailed Tasks";

            // fill two sheets

            // fill email work sheet

            #region Email sheet
            emailWorkSheet.Cells[1, 1] = "TaskID";
            emailWorkSheet.Cells[1, 2] = "Client Name";
            emailWorkSheet.Cells[1, 3] = "Task Name";
            emailWorkSheet.Cells[1, 4] = "State";

            
            for(int i = 0; i < emailData.Count; i++)
            {
                emailWorkSheet.Cells[i + 2, 1] = emailData[i].TaskID.Equals("") ? "-" : emailData[i].TaskID;
                emailWorkSheet.Cells[i + 2, 2] = emailData[i].ClientName;
                emailWorkSheet.Cells[i + 2, 3] = emailData[i].TaskName;
                emailWorkSheet.Cells[i + 2, 4] = emailData[i].Status;

            }

            emailWorkSheet.Columns.AutoFit();
            emailWorkSheet.Columns.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
            emailWorkSheet.UsedRange.Cells.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            #endregion Email sheet

            // fill sentrifugo work sheet

            #region Online Sheet
            onlineWorkSheet.Cells[1, 1] = "ClientName";
            onlineWorkSheet.Cells[1, 2] = "Duration";
            onlineWorkSheet.Cells[1, 3] = "Task Names";
            onlineWorkSheet.Cells[1, 4] = "Task Date";


            for(int i = 0; i < sentrifugoData.Count; i++)
            {
                onlineWorkSheet.Cells[i + 2, 1] = sentrifugoData[i].ClientName;
                onlineWorkSheet.Cells[i + 2, 2] = sentrifugoData[i].Duration;
                onlineWorkSheet.Cells[i + 2, 3] = sentrifugoData[i].TaskNames;
                onlineWorkSheet.Cells[i + 2, 4] = sentrifugoData[i].TaskDate;
            }

            onlineWorkSheet.Columns.AutoFit();
            onlineWorkSheet.Columns.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
            onlineWorkSheet.UsedRange.Cells.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            #endregion Online Sheet

            #region Detailed sheet
            allDetailsSheet.Cells[1, 1] = "Task Date";
            allDetailsSheet.Cells[1, 2] = "Task ID";
            allDetailsSheet.Cells[1, 3] = "Task Name";
            allDetailsSheet.Cells[1, 4] = "Client Name";
            allDetailsSheet.Cells[1, 5] = "Task Start Time"; 
            allDetailsSheet.Cells[1, 6] = "Task End Time";
            allDetailsSheet.Cells[1, 7] = "Task Duration";
            allDetailsSheet.Cells[1, 8] = "Coding state";
            allDetailsSheet.Cells[1, 9] = "Review State";
            allDetailsSheet.Cells[1, 10] = "Checkin State";
            allDetailsSheet.Cells[1, 11] = "Remarks";

            for(int i = 0; i < allTimesheetData.Count; i++)
            {
                allDetailsSheet.Cells[i + 2, 1] = allTimesheetData[i].TaskDate;
                allDetailsSheet.Cells[i + 2, 2] = allTimesheetData[i].TaskID;
                allDetailsSheet.Cells[i + 2, 3] = allTimesheetData[i].TaskName;
                allDetailsSheet.Cells[i + 2, 4] = allTimesheetData[i].ClientName;
                allDetailsSheet.Cells[i + 2, 5] = allTimesheetData[i].TaskStartTime;
                allDetailsSheet.Cells[i + 2, 6] = allTimesheetData[i].TaskEndTime;
                allDetailsSheet.Cells[i + 2, 7] = GetTimeDifference(allTimesheetData[i].TaskStartTime, allTimesheetData[i].TaskEndTime); 
                allDetailsSheet.Cells[i + 2, 8] = allTimesheetData[i].IsCoded;
                allDetailsSheet.Cells[i + 2, 9] = allTimesheetData[i].IsReviewed;
                allDetailsSheet.Cells[i + 2, 10] = allTimesheetData[i].IsCheckin;
                allDetailsSheet.Cells[i + 2, 11] = allTimesheetData[i].Comment;
            }

            allDetailsSheet.Columns.AutoFit();
            allDetailsSheet.Columns.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
            allDetailsSheet.UsedRange.Cells.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            #endregion Detailed sheet

            
            excelWorkbook.SaveAs(GetFileExportPath() + "TimeSheet" + GetDateTimeForFileName() + ".xlsx");
            excelWorkbook.Close();
            xlApplication.Quit();

        }

        private string GetTimeDifference(string startTime, string endTime)
        {
            if(endTime == null || endTime == "")
            {
                endTime = DateTime.Now.ToString("HH:mm:ss");
            }
            TimeSpan startd = DateTime.Parse(startTime).TimeOfDay;
            TimeSpan ended = DateTime.Parse(endTime).TimeOfDay;

            return ended.Subtract(startd).ToString();

        }
        private string GetDateTimeForFileName()
        {
            return DateTime.Now.ToString("_yyyy_MM_dd-HH_mm_ss");
        }

        private string GetFileExportPath()
        {
            string exportPath = ConfigurationManager.AppSettings["ExportFolderPath"];
            return exportPath;
        }        

    }
}
