using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        
        private void startButton_Click(object sender, EventArgs e)
        {
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
                this.startButton.Enabled = false;
                this.endButton.Enabled = true;
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

            dbTransaction.AddNonExistingClient(clientName);
            this.endButton.Enabled = false;
            this.startButton.Enabled = true;
            ClearForNewTask();
            EnableTextBoxes();
            this.ActiveControl = this.taskIDTextBox;

            FillClientListDropDown();
            FillTaskNameDropDown();

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

        private void Form1_Load(object sender, EventArgs e)
        {
            this.endButton.Enabled = false;
            RestorePreviousState();

            FillClientListDropDown();
            FillTaskNameDropDown();            
        }

        private void FillClientListDropDown()
        {
            DbTransaction dbTransaction = new DbTransaction();
            List<ClientListTable> allClientList = dbTransaction.GetAllClientList();

            if(allClientList.Count > 0)
            {
                this.clientNameComboBox.Items.Clear();
                this.clientNameComboBox.Items.AddRange(allClientList.Select(x => x.ClientName).Distinct().ToArray());
            }


        }

        private void FillTaskNameDropDown()
        {
            DbTransaction dbTransaction = new DbTransaction();
            List<TimeSheetDetailsTable> allTasksList = dbTransaction.GetAllTasksForPeriod(DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"));

            if(allTasksList.Count > 0)
            {
                this.taskNameComboBox.Items.Clear();
                this.taskNameComboBox.Items.AddRange(allTasksList.Select(x => x.TaskName).Distinct().ToArray());
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

                this.endButton.Enabled = true;
                this.startButton.Enabled = false;
                this.endButton.Focus();
                this.ActiveControl = this.endButton;

                DisableTextBoxes();

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

        private void ClearErrorMessage()
        {
            this.errorMessageLabel.Text = "";
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
            ExcelWriter excelWriter = new ExcelWriter();
            // pass date ranges to this, dont fetch and pass list
            excelWriter.WriteExportDataToExcel(DateTime.Now.Date.ToString("yyyy-MM-dd HH:mm:ss"), DateTime.Now.AddDays(1).Date.ToString("yyyy-MM-dd HH:mm:ss"));

            this.Close();

        }

        private void toDateLabel_Click(object sender, EventArgs e)
        {
            // click on today 
        }
    }
}
