namespace LegendaryExcelAddIn
{
    partial class frmConsolidatedDataUpload
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
            this.components = new System.ComponentModel.Container();
            this.lblTrialBalanceDate = new System.Windows.Forms.Label();
            this.dateTrialBalanceDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.cbFund = new System.Windows.Forms.ComboBox();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.btnGetFileList = new System.Windows.Forms.Button();
            this.chkLstBoxFiles = new System.Windows.Forms.CheckedListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnLoadToDB = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btnCreateReport = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.fileReportDirectory = new LegendaryExcelAddIn.ctlFileBrowser();
            this.fileInputDirectory = new LegendaryExcelAddIn.ctlFileBrowser();
            this.btnOpenLastCreated = new System.Windows.Forms.Button();
            this.lblLastCreatedFullPath = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTrialBalanceDate
            // 
            this.lblTrialBalanceDate.AutoSize = true;
            this.lblTrialBalanceDate.Location = new System.Drawing.Point(18, 23);
            this.lblTrialBalanceDate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTrialBalanceDate.Name = "lblTrialBalanceDate";
            this.lblTrialBalanceDate.Size = new System.Drawing.Size(98, 13);
            this.lblTrialBalanceDate.TabIndex = 0;
            this.lblTrialBalanceDate.Text = "Trial Balance Date:";
            // 
            // dateTrialBalanceDate
            // 
            this.dateTrialBalanceDate.Location = new System.Drawing.Point(119, 20);
            this.dateTrialBalanceDate.Margin = new System.Windows.Forms.Padding(2);
            this.dateTrialBalanceDate.Name = "dateTrialBalanceDate";
            this.dateTrialBalanceDate.Size = new System.Drawing.Size(202, 20);
            this.dateTrialBalanceDate.TabIndex = 1;
            this.toolTip1.SetToolTip(this.dateTrialBalanceDate, "All files with matching date in the name of the file will be processed.  \r\nDate f" +
        "ormat is \"mm-dd-yy\".  \r\nExample:  \"Lin TRS 10-31-18 TrialBalance.xlsx\"");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(334, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Fund:";
            // 
            // cbFund
            // 
            this.cbFund.FormattingEnabled = true;
            this.cbFund.Items.AddRange(new object[] {
            "LOF REIT II",
            "LF REIT III"});
            this.cbFund.Location = new System.Drawing.Point(370, 20);
            this.cbFund.Margin = new System.Windows.Forms.Padding(2);
            this.cbFund.Name = "cbFund";
            this.cbFund.Size = new System.Drawing.Size(102, 21);
            this.cbFund.TabIndex = 4;
            // 
            // txtStatus
            // 
            this.txtStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatus.Location = new System.Drawing.Point(337, 168);
            this.txtStatus.Margin = new System.Windows.Forms.Padding(2);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtStatus.Size = new System.Drawing.Size(853, 537);
            this.txtStatus.TabIndex = 116;
            // 
            // btnGetFileList
            // 
            this.btnGetFileList.BackColor = System.Drawing.Color.LightGray;
            this.btnGetFileList.Font = new System.Drawing.Font("Gill Sans MT", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetFileList.ForeColor = System.Drawing.Color.Black;
            this.btnGetFileList.Location = new System.Drawing.Point(16, 120);
            this.btnGetFileList.Margin = new System.Windows.Forms.Padding(2);
            this.btnGetFileList.Name = "btnGetFileList";
            this.btnGetFileList.Size = new System.Drawing.Size(118, 26);
            this.btnGetFileList.TabIndex = 115;
            this.btnGetFileList.Text = "GET FILE LIST";
            this.btnGetFileList.UseVisualStyleBackColor = false;
            this.btnGetFileList.Click += new System.EventHandler(this.btnGetFileList_Click);
            // 
            // chkLstBoxFiles
            // 
            this.chkLstBoxFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.chkLstBoxFiles.CheckOnClick = true;
            this.chkLstBoxFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLstBoxFiles.FormattingEnabled = true;
            this.chkLstBoxFiles.Location = new System.Drawing.Point(16, 168);
            this.chkLstBoxFiles.Margin = new System.Windows.Forms.Padding(2);
            this.chkLstBoxFiles.Name = "chkLstBoxFiles";
            this.chkLstBoxFiles.ScrollAlwaysVisible = true;
            this.chkLstBoxFiles.Size = new System.Drawing.Size(305, 536);
            this.chkLstBoxFiles.TabIndex = 119;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(146, 128);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 120;
            this.label3.Text = "> > >";
            // 
            // label4
            // 
            this.label4.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.label4.Location = new System.Drawing.Point(180, 114);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 41);
            this.label4.TabIndex = 121;
            this.label4.Text = "Select Files You Want To Load Into DB\r\n";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(259, 128);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 122;
            this.label5.Text = "> > >";
            // 
            // btnLoadToDB
            // 
            this.btnLoadToDB.BackColor = System.Drawing.Color.LightGray;
            this.btnLoadToDB.Font = new System.Drawing.Font("Gill Sans MT", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadToDB.ForeColor = System.Drawing.Color.Black;
            this.btnLoadToDB.Location = new System.Drawing.Point(299, 120);
            this.btnLoadToDB.Margin = new System.Windows.Forms.Padding(2);
            this.btnLoadToDB.Name = "btnLoadToDB";
            this.btnLoadToDB.Size = new System.Drawing.Size(118, 26);
            this.btnLoadToDB.TabIndex = 123;
            this.btnLoadToDB.Text = "LOAD TO DB";
            this.btnLoadToDB.UseVisualStyleBackColor = false;
            this.btnLoadToDB.Click += new System.EventHandler(this.btnLoadToDB_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(425, 128);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 124;
            this.label6.Text = "> > >";
            // 
            // btnCreateReport
            // 
            this.btnCreateReport.BackColor = System.Drawing.Color.LightGray;
            this.btnCreateReport.Font = new System.Drawing.Font("Gill Sans MT", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateReport.ForeColor = System.Drawing.Color.Black;
            this.btnCreateReport.Location = new System.Drawing.Point(461, 120);
            this.btnCreateReport.Margin = new System.Windows.Forms.Padding(2);
            this.btnCreateReport.Name = "btnCreateReport";
            this.btnCreateReport.Size = new System.Drawing.Size(155, 26);
            this.btnCreateReport.TabIndex = 125;
            this.btnCreateReport.Text = "CREATE REPORT";
            this.btnCreateReport.UseVisualStyleBackColor = false;
            this.btnCreateReport.Click += new System.EventHandler(this.btnCreateReport_Click);
            // 
            // fileReportDirectory
            // 
            this.fileReportDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileReportDirectory.DirectoryNotFile = true;
            this.fileReportDirectory.FileFilterString = "Excel files (*.xl*)|*.xl*|Excel files (*.xl*)|*.xl*";
            this.fileReportDirectory.FileLabel = "Report Directory:";
            this.fileReportDirectory.FullFilePath = "";
            this.fileReportDirectory.InitialDirectory = "C:\\";
            this.fileReportDirectory.Location = new System.Drawing.Point(16, 82);
            this.fileReportDirectory.Margin = new System.Windows.Forms.Padding(1);
            this.fileReportDirectory.Name = "fileReportDirectory";
            this.fileReportDirectory.Size = new System.Drawing.Size(1171, 19);
            this.fileReportDirectory.TabIndex = 126;
            // 
            // fileInputDirectory
            // 
            this.fileInputDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileInputDirectory.DirectoryNotFile = true;
            this.fileInputDirectory.FileFilterString = "Excel files (*.xl*)|*.xl*|Excel files (*.xl*)|*.xl*";
            this.fileInputDirectory.FileLabel = "Input Directory:";
            this.fileInputDirectory.FullFilePath = "";
            this.fileInputDirectory.InitialDirectory = "C:\\";
            this.fileInputDirectory.Location = new System.Drawing.Point(16, 53);
            this.fileInputDirectory.Margin = new System.Windows.Forms.Padding(1);
            this.fileInputDirectory.Name = "fileInputDirectory";
            this.fileInputDirectory.Size = new System.Drawing.Size(1171, 19);
            this.fileInputDirectory.TabIndex = 2;
            // 
            // btnOpenLastCreated
            // 
            this.btnOpenLastCreated.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenLastCreated.BackColor = System.Drawing.Color.LightGray;
            this.btnOpenLastCreated.Font = new System.Drawing.Font("Gill Sans MT", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenLastCreated.ForeColor = System.Drawing.Color.Black;
            this.btnOpenLastCreated.Location = new System.Drawing.Point(993, 121);
            this.btnOpenLastCreated.Margin = new System.Windows.Forms.Padding(2);
            this.btnOpenLastCreated.Name = "btnOpenLastCreated";
            this.btnOpenLastCreated.Size = new System.Drawing.Size(194, 26);
            this.btnOpenLastCreated.TabIndex = 127;
            this.btnOpenLastCreated.Text = "Open Last Created Report";
            this.btnOpenLastCreated.UseVisualStyleBackColor = false;
            this.btnOpenLastCreated.Click += new System.EventHandler(this.btnOpenLastCreated_Click);
            // 
            // lblLastCreatedFullPath
            // 
            this.lblLastCreatedFullPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLastCreatedFullPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastCreatedFullPath.Location = new System.Drawing.Point(299, 149);
            this.lblLastCreatedFullPath.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLastCreatedFullPath.Name = "lblLastCreatedFullPath";
            this.lblLastCreatedFullPath.Size = new System.Drawing.Size(888, 12);
            this.lblLastCreatedFullPath.TabIndex = 128;
            this.lblLastCreatedFullPath.Text = "<last created full path>";
            this.lblLastCreatedFullPath.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmConsolidatedDataUpload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1205, 716);
            this.Controls.Add(this.lblLastCreatedFullPath);
            this.Controls.Add(this.btnOpenLastCreated);
            this.Controls.Add(this.fileReportDirectory);
            this.Controls.Add(this.btnCreateReport);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnLoadToDB);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chkLstBoxFiles);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.btnGetFileList);
            this.Controls.Add(this.cbFund);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.fileInputDirectory);
            this.Controls.Add(this.dateTrialBalanceDate);
            this.Controls.Add(this.lblTrialBalanceDate);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmConsolidatedDataUpload";
            this.Text = "Consolidated Trial Balance Reporting";
            this.Load += new System.EventHandler(this.frmConsolidatedDataUpload_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTrialBalanceDate;
        private System.Windows.Forms.DateTimePicker dateTrialBalanceDate;
        private ctlFileBrowser fileInputDirectory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbFund;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Button btnGetFileList;
        private System.Windows.Forms.CheckedListBox chkLstBoxFiles;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnLoadToDB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnCreateReport;
        private ctlFileBrowser fileReportDirectory;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnOpenLastCreated;
        private System.Windows.Forms.Label lblLastCreatedFullPath;
    }
}