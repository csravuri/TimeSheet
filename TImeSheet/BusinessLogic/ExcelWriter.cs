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
        //Workbook workbook = Excel.ex
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

            dbTransaction.GetExportData(fromDate, toDate, out emailData, out sentrifugoData);

            var xlApplication = new Excel.Application();
            xlApplication.Workbooks.Add("");


            Excel._Workbook excelWorkbook = xlApplication.ActiveWorkbook;

            Excel._Worksheet emailWorkSheet = excelWorkbook.ActiveSheet;
            excelWorkbook.Sheets.Add(emailWorkSheet);
            Excel._Worksheet onlineWorkSheet = excelWorkbook.ActiveSheet;

            emailWorkSheet.Name = "Email";
            onlineWorkSheet.Name = "Sentrifugo";

            // fill two sheets

            // fill email work sheet
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


            // fill sentrifugo work sheet

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


            excelWorkbook.SaveAs(GetFileExportPath() + "TimeSheet" + DateTime.Now.ToOADate().ToString().Replace(" ","_").Replace(":","") + ".xlsx");
            excelWorkbook.Close();
            xlApplication.Quit();
            
            


        }


        private string GetFileExportPath()
        {
            string exportPath = ConfigurationManager.AppSettings["ExportFolderPath"];
            return exportPath;
        }


    }
}
