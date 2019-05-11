using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TImeSheet.BusinessLogic;

namespace TImeSheet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private List<TimeSheetDetailsTable> allTasksList = new List<TimeSheetDetailsTable>();
        ExcelWriter excelWriter = new ExcelWriter();

        #region Click Events

        private void Form1_Load(object sender, EventArgs e)
        {
            ClearTimeValues();
            RestorePreviousState();

            FillClientListDropDown();
            FillTaskNameDropDown();
            setDatePickerDefault();
            setFormDefaultPosition();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            ClearTimeValues();
            if (ValidateDetails())
            {
                string taskID = this.taskIDTextBox.Text;
                string taskName = this.taskNameComboBox.Text;
                string clientName = this.clientNameComboBox.Text;
                string taskDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                string isCoded = this.codingCheckBox.Checked ? "1" : "0";
                string isReviewd = this.reviewCheckBox.Checked ? "1" : "0";
                string isCheckin = this.checkinCheckBox.Checked ? "1" : "0";
                string startTime = DateTime.Now.TimeOfDay.ToString();
                string comment = this.commentTextBox.Text == "" ? null : this.commentTextBox.Text;

                DbTransaction dbTransaction = new DbTransaction();


                dbTransaction.TaskStarted(taskID, taskName, clientName, isCoded, isReviewd, isCheckin, taskDate, startTime, comment: comment);

                DisableTextBoxes();
                dbTransaction.AddNonExistingClient(clientName);
                ExportButton_Disable();
                this.startButton.Enabled = false;
                this.endButton.Enabled = true;
                this.startTimeValue.Text = excelWriter.GetStandardTimeFormat(startTime);
                this.WindowState = FormWindowState.Minimized;
            }
            else
            {
                this.errorMessageLabel.ForeColor = Color.Red;
                this.errorMessageLabel.Text = "Please enter Task Name";
            }
        }

        private void endButton_Click(object sender, EventArgs e)
        {
            string taskID = this.taskIDTextBox.Text;
            string taskName = this.taskNameComboBox.Text;
            string clientName = this.clientNameComboBox.Text;
            string isCoded = this.codingCheckBox.Checked ? "1" : "0";
            string isReviewd = this.reviewCheckBox.Checked ? "1" : "0";
            string isCheckin = this.checkinCheckBox.Checked ? "1" : "0";
            string endTime = DateTime.Now.TimeOfDay.ToString();
            string comment = this.commentTextBox.Text == "" ? null : this.commentTextBox.Text;

            DbTransaction dbTransaction = new DbTransaction();

            dbTransaction.TaskEnded(taskID, taskName, clientName, isCoded, isReviewd, isCheckin, endTime, comment);

            this.durationValue.Text = excelWriter.GetStandardTimeFormat(excelWriter.GetTimeDifference(this.startTimeValue.Text, endTime));
            dbTransaction.AddNonExistingClient(clientName);
            this.endButton.Enabled = false;
            this.startButton.Enabled = true;
            ExportButton_Enable();
            ClearForNewTask();
            EnableTextBoxes();
            this.ActiveControl = this.taskIDTextBox;

            FillClientListDropDown();
            FillTaskNameDropDown();

        }

        private void taskIDLabel_Click(object sender, EventArgs e)
        {
            this.taskIDTextBox.Enabled = true;
        }

        private void taskNameLabel_Click(object sender, EventArgs e)
        {
            this.taskNameComboBox.Enabled = true;
        }

        private void taskNameComboBox_TextUpdate(object sender, EventArgs e)
        {
            ClearErrorMessage();
        }

        private void clientNameLabel_Click(object sender, EventArgs e)
        {
            this.clientNameComboBox.Enabled = true;
        }

        private void clientNameComboBox_Enter(object sender, EventArgs e)
        {
            this.clientNameComboBox.DroppedDown = true;
        }

        private void taskNameComboBox_Enter(object sender, EventArgs e)
        {
            this.taskNameComboBox.DroppedDown = true;
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            DateTime fromDate = this.fromDateTimePicker.Value.Date;
            DateTime toDate = this.toDateTimePicker.Value.Date.AddDays(1);

            if (validateExportDates(fromDate, toDate))
            {
                this.exportButton.Enabled = false;
                this.exportButton.Cursor = Cursors.WaitCursor;
                // pass date ranges to this, dont fetch and pass list
                string savedFile = excelWriter.WriteExportDataToExcel(fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"));

                Process.Start(savedFile);
                this.Close();
            }
            else
            {
                this.errorMessageLabel.ForeColor = Color.Red;
                this.errorMessageLabel.Text = "From Date should be less than or equal to ToDate";
                setDatePickerDefault();
            }
        }

        private void toDateLabel_Click(object sender, EventArgs e)
        {
            // click on today 
            enableDatePickers();
        }

        private void TaskNameComboBox_Leave(object sender, EventArgs e)
        {
            string taskName = this.taskNameComboBox.Text.Trim();
            if (taskName != "")
            {
                string taskID = this.taskIDTextBox.Text;
                this.taskIDTextBox.Text = allTasksList.Where(x => x.TaskName.Trim() == taskName).OrderByDescending(x => DateTime.Parse(x.TaskDate)).Select(x => x.TaskID).FirstOrDefault();
                if (this.taskIDTextBox.Text.Trim() == "")
                {
                    this.taskIDTextBox.Text = taskID;
                }

            }
        }

        #endregion Click Events


        #region Form updates

        private void setFormDefaultPosition()
        {
            int screenWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorMaximizedWindowSize.Width;

            int formWidth = this.Width;

            this.StartPosition = FormStartPosition.Manual;
            this.Left = screenWidth - formWidth;
            this.Top = 0;
        }

        private void ClearTimeValues()
        {
            this.startTimeValue.Text = "";
            this.durationValue.Text = "";
        }

        private void ClearForNewTask()
        {
            this.taskIDTextBox.Text = "";
            this.taskNameComboBox.Text = "";
            this.codingCheckBox.Checked = false;
            this.reviewCheckBox.Checked = false;
            this.checkinCheckBox.Checked = false;
            this.commentTextBox.Text = "";
            this.clientNameComboBox.Text = "";

            this.ActiveControl = this.taskIDTextBox;

        }       

        private void FillClientListDropDown()
        {
            DbTransaction dbTransaction = new DbTransaction();
            List<ClientListTable> allClientList = dbTransaction.GetAllClientList();

            if(allClientList.Count > 0)
            {
                this.clientNameComboBox.Items.Clear();
                this.clientNameComboBox.Items.AddRange(allClientList.Where(x => x.ClientName != "").Select(x => x.ClientName).Distinct().ToArray());
            }


        }

        private void FillTaskNameDropDown()
        {
            DbTransaction dbTransaction = new DbTransaction();
            allTasksList = dbTransaction.GetAllTasksForPeriod(DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"));

            if(allTasksList.Count > 0)
            {
                this.taskNameComboBox.Items.Clear();
                this.taskNameComboBox.Items.AddRange(allTasksList.Where(x => x.TaskName != "").Select(x => x.TaskName).Distinct().ToArray());
            }

        }

        private void RestorePreviousState()
        {
            DbTransaction dbTransaction = new DbTransaction();
            TimeSheetDetailsTable previousTask = dbTransaction.GetUnfinshedTaskDetails();

            if(previousTask != null)
            {
                this.taskIDTextBox.Text = previousTask.TaskID;
                this.taskNameComboBox.Text = previousTask.TaskName;
                this.clientNameComboBox.Text = previousTask.ClientName;
                this.codingCheckBox.Checked = bool.Parse(previousTask.IsCoded);
                this.reviewCheckBox.Checked = bool.Parse(previousTask.IsReviewed);
                this.checkinCheckBox.Checked = bool.Parse(previousTask.IsCheckin);
                this.commentTextBox.Text = previousTask.Comment;
                this.startTimeValue.Text = excelWriter.GetStandardTimeFormat(previousTask.TaskStartTime);

                this.endButton.Enabled = true;
                this.startButton.Enabled = false;
                this.endButton.Focus();
                this.ActiveControl = this.endButton;

                ExportButton_Disable();
                DisableTextBoxes();
            }
            else
            {
                this.endButton.Enabled = false;
                ExportButton_Enable();
            }
        }

        

        private bool ValidateDetails()
        {
            if (this.taskNameComboBox.Text == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void EnableTextBoxes()
        {
            this.taskIDTextBox.Enabled = true;
            this.taskNameComboBox.Enabled = true;
            this.clientNameComboBox.Enabled = true;
        }

        private void DisableTextBoxes()
        {
            this.taskIDTextBox.Enabled = false;
            this.taskNameComboBox.Enabled = false;
            this.clientNameComboBox.Enabled = false;
        }

        private void ClearErrorMessage()
        {
            this.errorMessageLabel.Text = "";
        }        

        private void setDatePickerDefault()
        {
            this.toDateTimePicker.Value = DateTime.Now.Date;
            this.fromDateTimePicker.Value = DateTime.Now.Date;
            this.fromDateTimePicker.Enabled = false;
            this.toDateTimePicker.Enabled = false;
            this.fromDateLabel.Enabled = false;
            this.toDateLabel.Text = "Today";

        }

        private void enableDatePickers()
        {
            this.toDateTimePicker.Value = DateTime.Now.Date; // set week start day
            this.fromDateTimePicker.Value = GetWeekStartDayDate(DateTime.Now.Date);
            this.fromDateTimePicker.Enabled = true;
            this.toDateTimePicker.Enabled = true;
            this.fromDateLabel.Enabled = true;
            this.toDateLabel.Text = "To";
        }

        private DateTime GetWeekStartDayDate(DateTime someDate)
        {
            string weekStartDay = GetWeekStartDay() ?? "Monday";
            while(!someDate.DayOfWeek.ToString().ToLower().Equals(weekStartDay.ToLower()))
            {
                someDate = someDate.AddDays(-1);
            }
            return someDate;
        }

        private string GetWeekStartDay()
        {
            return ConfigurationManager.AppSettings["WeekStartDay"];
        }

        private bool validateExportDates(DateTime fromDate, DateTime toDate)
        {
            return fromDate <= toDate;
        }

        private void ExportButton_Enable()
        {
            this.exportButton.Enabled = true;
            this.exportButton.Cursor = Cursors.Hand;
        }

        private void ExportButton_Disable()
        {
            this.exportButton.Enabled = false;
            this.exportButton.Cursor = Cursors.Cross;
        }


        #endregion Form updates



    }
}
