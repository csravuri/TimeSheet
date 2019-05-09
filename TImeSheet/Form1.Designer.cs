namespace TImeSheet
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.startButton = new System.Windows.Forms.Button();
            this.endButton = new System.Windows.Forms.Button();
            this.taskNameComboBox = new System.Windows.Forms.ComboBox();
            this.taskIDTextBox = new System.Windows.Forms.TextBox();
            this.taskIDLabel = new System.Windows.Forms.Label();
            this.taskNameLabel = new System.Windows.Forms.Label();
            this.codingCheckBox = new System.Windows.Forms.CheckBox();
            this.reviewCheckBox = new System.Windows.Forms.CheckBox();
            this.checkinCheckBox = new System.Windows.Forms.CheckBox();
            this.commentTextBox = new System.Windows.Forms.TextBox();
            this.commentLabel = new System.Windows.Forms.Label();
            this.errorMessageLabel = new System.Windows.Forms.Label();
            this.clientNameLabel = new System.Windows.Forms.Label();
            this.clientNameComboBox = new System.Windows.Forms.ComboBox();
            this.exportButton = new System.Windows.Forms.Button();
            this.fromDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.toDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.fromDateLabel = new System.Windows.Forms.Label();
            this.toDateLabel = new System.Windows.Forms.Label();
            this.startTimeLable = new System.Windows.Forms.Label();
            this.startTimeValue = new System.Windows.Forms.Label();
            this.durationLabel = new System.Windows.Forms.Label();
            this.durationValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // startButton
            // 
            this.startButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.startButton.Location = new System.Drawing.Point(6, 127);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 7;
            this.startButton.Text = "&Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // endButton
            // 
            this.endButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.endButton.Location = new System.Drawing.Point(95, 127);
            this.endButton.Name = "endButton";
            this.endButton.Size = new System.Drawing.Size(75, 23);
            this.endButton.TabIndex = 8;
            this.endButton.Text = "&End";
            this.endButton.UseVisualStyleBackColor = true;
            this.endButton.Click += new System.EventHandler(this.endButton_Click);
            // 
            // taskNameComboBox
            // 
            this.taskNameComboBox.FormattingEnabled = true;
            this.taskNameComboBox.Location = new System.Drawing.Point(76, 29);
            this.taskNameComboBox.Name = "taskNameComboBox";
            this.taskNameComboBox.Size = new System.Drawing.Size(341, 21);
            this.taskNameComboBox.TabIndex = 2;
            this.taskNameComboBox.TextUpdate += new System.EventHandler(this.taskNameComboBox_TextUpdate);
            this.taskNameComboBox.Enter += new System.EventHandler(this.taskNameComboBox_Enter);
            this.taskNameComboBox.Leave += new System.EventHandler(this.TaskNameComboBox_Leave);
            // 
            // taskIDTextBox
            // 
            this.taskIDTextBox.Location = new System.Drawing.Point(10, 30);
            this.taskIDTextBox.Name = "taskIDTextBox";
            this.taskIDTextBox.Size = new System.Drawing.Size(60, 20);
            this.taskIDTextBox.TabIndex = 0;
            // 
            // taskIDLabel
            // 
            this.taskIDLabel.AutoSize = true;
            this.taskIDLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.taskIDLabel.Location = new System.Drawing.Point(10, 8);
            this.taskIDLabel.Name = "taskIDLabel";
            this.taskIDLabel.Size = new System.Drawing.Size(45, 13);
            this.taskIDLabel.TabIndex = 10;
            this.taskIDLabel.Text = "Task ID";
            this.taskIDLabel.Click += new System.EventHandler(this.taskIDLabel_Click);
            // 
            // taskNameLabel
            // 
            this.taskNameLabel.AutoSize = true;
            this.taskNameLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.taskNameLabel.Location = new System.Drawing.Point(76, 7);
            this.taskNameLabel.Name = "taskNameLabel";
            this.taskNameLabel.Size = new System.Drawing.Size(62, 13);
            this.taskNameLabel.TabIndex = 9;
            this.taskNameLabel.Text = "Task Name";
            this.taskNameLabel.Click += new System.EventHandler(this.taskNameLabel_Click);
            // 
            // codingCheckBox
            // 
            this.codingCheckBox.AutoSize = true;
            this.codingCheckBox.Location = new System.Drawing.Point(10, 55);
            this.codingCheckBox.Name = "codingCheckBox";
            this.codingCheckBox.Size = new System.Drawing.Size(57, 17);
            this.codingCheckBox.TabIndex = 4;
            this.codingCheckBox.Text = "&Coded";
            this.codingCheckBox.UseVisualStyleBackColor = true;
            // 
            // reviewCheckBox
            // 
            this.reviewCheckBox.AutoSize = true;
            this.reviewCheckBox.Location = new System.Drawing.Point(10, 78);
            this.reviewCheckBox.Name = "reviewCheckBox";
            this.reviewCheckBox.Size = new System.Drawing.Size(74, 17);
            this.reviewCheckBox.TabIndex = 5;
            this.reviewCheckBox.Text = "&Reviewed";
            this.reviewCheckBox.UseVisualStyleBackColor = true;
            // 
            // checkinCheckBox
            // 
            this.checkinCheckBox.AutoSize = true;
            this.checkinCheckBox.Location = new System.Drawing.Point(10, 103);
            this.checkinCheckBox.Name = "checkinCheckBox";
            this.checkinCheckBox.Size = new System.Drawing.Size(80, 17);
            this.checkinCheckBox.TabIndex = 6;
            this.checkinCheckBox.Text = "C&hecked-in";
            this.checkinCheckBox.UseVisualStyleBackColor = true;
            // 
            // commentTextBox
            // 
            this.commentTextBox.Location = new System.Drawing.Point(119, 72);
            this.commentTextBox.Multiline = true;
            this.commentTextBox.Name = "commentTextBox";
            this.commentTextBox.Size = new System.Drawing.Size(298, 48);
            this.commentTextBox.TabIndex = 3;
            // 
            // commentLabel
            // 
            this.commentLabel.AutoSize = true;
            this.commentLabel.Location = new System.Drawing.Point(116, 56);
            this.commentLabel.Name = "commentLabel";
            this.commentLabel.Size = new System.Drawing.Size(51, 13);
            this.commentLabel.TabIndex = 11;
            this.commentLabel.Text = "Comment";
            // 
            // errorMessageLabel
            // 
            this.errorMessageLabel.AutoSize = true;
            this.errorMessageLabel.Location = new System.Drawing.Point(233, 154);
            this.errorMessageLabel.Name = "errorMessageLabel";
            this.errorMessageLabel.Size = new System.Drawing.Size(0, 13);
            this.errorMessageLabel.TabIndex = 12;
            // 
            // clientNameLabel
            // 
            this.clientNameLabel.AutoSize = true;
            this.clientNameLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.clientNameLabel.Location = new System.Drawing.Point(193, 7);
            this.clientNameLabel.Name = "clientNameLabel";
            this.clientNameLabel.Size = new System.Drawing.Size(64, 13);
            this.clientNameLabel.TabIndex = 13;
            this.clientNameLabel.Text = "Client Name";
            this.clientNameLabel.Click += new System.EventHandler(this.clientNameLabel_Click);
            // 
            // clientNameComboBox
            // 
            this.clientNameComboBox.FormattingEnabled = true;
            this.clientNameComboBox.Location = new System.Drawing.Point(262, 3);
            this.clientNameComboBox.Name = "clientNameComboBox";
            this.clientNameComboBox.Size = new System.Drawing.Size(121, 21);
            this.clientNameComboBox.TabIndex = 1;
            this.clientNameComboBox.Enter += new System.EventHandler(this.clientNameComboBox_Enter);
            // 
            // exportButton
            // 
            this.exportButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.exportButton.Location = new System.Drawing.Point(342, 127);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(75, 23);
            this.exportButton.TabIndex = 14;
            this.exportButton.Text = "E&xport";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // fromDateTimePicker
            // 
            this.fromDateTimePicker.CustomFormat = "  dd-MM-yyyy";
            this.fromDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.fromDateTimePicker.Location = new System.Drawing.Point(332, 50);
            this.fromDateTimePicker.Name = "fromDateTimePicker";
            this.fromDateTimePicker.Size = new System.Drawing.Size(85, 20);
            this.fromDateTimePicker.TabIndex = 15;
            // 
            // toDateTimePicker
            // 
            this.toDateTimePicker.CustomFormat = "  dd-MM-yyyy";
            this.toDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.toDateTimePicker.Location = new System.Drawing.Point(256, 128);
            this.toDateTimePicker.Name = "toDateTimePicker";
            this.toDateTimePicker.Size = new System.Drawing.Size(80, 20);
            this.toDateTimePicker.TabIndex = 16;
            // 
            // fromDateLabel
            // 
            this.fromDateLabel.AutoSize = true;
            this.fromDateLabel.Location = new System.Drawing.Point(300, 54);
            this.fromDateLabel.Name = "fromDateLabel";
            this.fromDateLabel.Size = new System.Drawing.Size(30, 13);
            this.fromDateLabel.TabIndex = 17;
            this.fromDateLabel.Text = "From";
            // 
            // toDateLabel
            // 
            this.toDateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toDateLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.toDateLabel.Location = new System.Drawing.Point(218, 132);
            this.toDateLabel.Name = "toDateLabel";
            this.toDateLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toDateLabel.Size = new System.Drawing.Size(37, 13);
            this.toDateLabel.TabIndex = 18;
            this.toDateLabel.Text = "Today";
            this.toDateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toDateLabel.Click += new System.EventHandler(this.toDateLabel_Click);
            // 
            // startTimeLable
            // 
            this.startTimeLable.AutoSize = true;
            this.startTimeLable.Location = new System.Drawing.Point(6, 154);
            this.startTimeLable.Name = "startTimeLable";
            this.startTimeLable.Size = new System.Drawing.Size(58, 13);
            this.startTimeLable.TabIndex = 19;
            this.startTimeLable.Text = "Start Time:";
            // 
            // startTimeValue
            // 
            this.startTimeValue.AutoSize = true;
            this.startTimeValue.Location = new System.Drawing.Point(61, 154);
            this.startTimeValue.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.startTimeValue.Name = "startTimeValue";
            this.startTimeValue.Size = new System.Drawing.Size(0, 13);
            this.startTimeValue.TabIndex = 20;
            // 
            // durationLabel
            // 
            this.durationLabel.AutoSize = true;
            this.durationLabel.Location = new System.Drawing.Point(123, 154);
            this.durationLabel.Name = "durationLabel";
            this.durationLabel.Size = new System.Drawing.Size(50, 13);
            this.durationLabel.TabIndex = 21;
            this.durationLabel.Text = "Duration:";
            // 
            // durationValue
            // 
            this.durationValue.AutoSize = true;
            this.durationValue.Location = new System.Drawing.Point(171, 154);
            this.durationValue.Name = "durationValue";
            this.durationValue.Size = new System.Drawing.Size(0, 13);
            this.durationValue.TabIndex = 22;
            // 
            // Form1
            // 
            this.AcceptButton = this.startButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 171);
            this.Controls.Add(this.durationValue);
            this.Controls.Add(this.durationLabel);
            this.Controls.Add(this.startTimeValue);
            this.Controls.Add(this.startTimeLable);
            this.Controls.Add(this.toDateLabel);
            this.Controls.Add(this.fromDateLabel);
            this.Controls.Add(this.toDateTimePicker);
            this.Controls.Add(this.fromDateTimePicker);
            this.Controls.Add(this.exportButton);
            this.Controls.Add(this.clientNameComboBox);
            this.Controls.Add(this.clientNameLabel);
            this.Controls.Add(this.errorMessageLabel);
            this.Controls.Add(this.commentLabel);
            this.Controls.Add(this.commentTextBox);
            this.Controls.Add(this.checkinCheckBox);
            this.Controls.Add(this.reviewCheckBox);
            this.Controls.Add(this.codingCheckBox);
            this.Controls.Add(this.taskNameLabel);
            this.Controls.Add(this.taskIDLabel);
            this.Controls.Add(this.taskIDTextBox);
            this.Controls.Add(this.taskNameComboBox);
            this.Controls.Add(this.endButton);
            this.Controls.Add(this.startButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(445, 210);
            this.MinimumSize = new System.Drawing.Size(445, 210);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TimeSheet";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button endButton;
        private System.Windows.Forms.ComboBox taskNameComboBox;
        private System.Windows.Forms.TextBox taskIDTextBox;
        private System.Windows.Forms.Label taskIDLabel;
        private System.Windows.Forms.Label taskNameLabel;
        private System.Windows.Forms.CheckBox codingCheckBox;
        private System.Windows.Forms.CheckBox reviewCheckBox;
        private System.Windows.Forms.CheckBox checkinCheckBox;
        private System.Windows.Forms.TextBox commentTextBox;
        private System.Windows.Forms.Label commentLabel;
        private System.Windows.Forms.Label errorMessageLabel;
        private System.Windows.Forms.Label clientNameLabel;
        private System.Windows.Forms.ComboBox clientNameComboBox;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.DateTimePicker fromDateTimePicker;
        private System.Windows.Forms.DateTimePicker toDateTimePicker;
        private System.Windows.Forms.Label fromDateLabel;
        private System.Windows.Forms.Label toDateLabel;
        private System.Windows.Forms.Label startTimeLable;
        private System.Windows.Forms.Label startTimeValue;
        private System.Windows.Forms.Label durationLabel;
        private System.Windows.Forms.Label durationValue;
    }
}

