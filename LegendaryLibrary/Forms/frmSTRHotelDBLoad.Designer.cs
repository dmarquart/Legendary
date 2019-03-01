namespace LegendaryLibrary
{
    partial class frmSTRHotelDBLoad
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
            this.fileSTRExcelDB = new LegendaryLibrary.ctlFileBrowser();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.btnCreateZohoInportFile = new System.Windows.Forms.Button();
            this.fileOutputDirectory = new LegendaryLibrary.ctlFileBrowser();
            this.lblBaseFileName = new System.Windows.Forms.Label();
            this.txtBaseFilename = new System.Windows.Forms.TextBox();
            this.fileZohoBackupDirectory = new LegendaryLibrary.ctlFileBrowser();
            this.SuspendLayout();
            // 
            // fileSTRExcelDB
            // 
            this.fileSTRExcelDB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileSTRExcelDB.DirectoryNotFile = false;
            this.fileSTRExcelDB.FileFilterString = "Excel files (*.xl*)|*.xl*|Excel files (*.xl*)|*.xl*";
            this.fileSTRExcelDB.FileLabel = "STR Input File:";
            this.fileSTRExcelDB.FullFilePath = "";
            this.fileSTRExcelDB.InitialDirectory = "C:\\";
            this.fileSTRExcelDB.Location = new System.Drawing.Point(11, 11);
            this.fileSTRExcelDB.Margin = new System.Windows.Forms.Padding(2);
            this.fileSTRExcelDB.Name = "fileSTRExcelDB";
            this.fileSTRExcelDB.Size = new System.Drawing.Size(814, 25);
            this.fileSTRExcelDB.TabIndex = 1;
            // 
            // txtStatus
            // 
            this.txtStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatus.Location = new System.Drawing.Point(11, 152);
            this.txtStatus.Margin = new System.Windows.Forms.Padding(2);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtStatus.Size = new System.Drawing.Size(894, 495);
            this.txtStatus.TabIndex = 117;
            // 
            // btnCreateZohoInportFile
            // 
            this.btnCreateZohoInportFile.Location = new System.Drawing.Point(321, 107);
            this.btnCreateZohoInportFile.Name = "btnCreateZohoInportFile";
            this.btnCreateZohoInportFile.Size = new System.Drawing.Size(163, 22);
            this.btnCreateZohoInportFile.TabIndex = 118;
            this.btnCreateZohoInportFile.Text = "Create ZOHO Import File(s)";
            this.btnCreateZohoInportFile.UseVisualStyleBackColor = true;
            this.btnCreateZohoInportFile.Click += new System.EventHandler(this.btnCreateZOHOImportFile_Click);
            // 
            // fileOutputDirectory
            // 
            this.fileOutputDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileOutputDirectory.DirectoryNotFile = true;
            this.fileOutputDirectory.FileFilterString = "Excel files (*.xl*)|*.xl*|Excel files (*.xl*)|*.xl*";
            this.fileOutputDirectory.FileLabel = "Output Directory:";
            this.fileOutputDirectory.FullFilePath = "";
            this.fileOutputDirectory.InitialDirectory = "C:\\";
            this.fileOutputDirectory.Location = new System.Drawing.Point(11, 78);
            this.fileOutputDirectory.Margin = new System.Windows.Forms.Padding(1);
            this.fileOutputDirectory.Name = "fileOutputDirectory";
            this.fileOutputDirectory.Size = new System.Drawing.Size(814, 26);
            this.fileOutputDirectory.TabIndex = 127;
            // 
            // lblBaseFileName
            // 
            this.lblBaseFileName.AutoSize = true;
            this.lblBaseFileName.Location = new System.Drawing.Point(13, 112);
            this.lblBaseFileName.Name = "lblBaseFileName";
            this.lblBaseFileName.Size = new System.Drawing.Size(84, 13);
            this.lblBaseFileName.TabIndex = 128;
            this.lblBaseFileName.Text = "Base File Name:";
            // 
            // txtBaseFilename
            // 
            this.txtBaseFilename.Location = new System.Drawing.Point(110, 108);
            this.txtBaseFilename.Name = "txtBaseFilename";
            this.txtBaseFilename.Size = new System.Drawing.Size(196, 20);
            this.txtBaseFilename.TabIndex = 129;
            // 
            // fileZohoBackupDirectory
            // 
            this.fileZohoBackupDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileZohoBackupDirectory.DirectoryNotFile = false;
            this.fileZohoBackupDirectory.FileFilterString = "Excel files (*.xl*)|*.xl*|Excel files (*.xl*)|*.xl*";
            this.fileZohoBackupDirectory.FileLabel = "Zoho Backup Directory:";
            this.fileZohoBackupDirectory.FullFilePath = "";
            this.fileZohoBackupDirectory.InitialDirectory = "C:\\";
            this.fileZohoBackupDirectory.Location = new System.Drawing.Point(11, 39);
            this.fileZohoBackupDirectory.Margin = new System.Windows.Forms.Padding(2);
            this.fileZohoBackupDirectory.Name = "fileZohoBackupDirectory";
            this.fileZohoBackupDirectory.Size = new System.Drawing.Size(814, 25);
            this.fileZohoBackupDirectory.TabIndex = 130;
            // 
            // frmSTRHotelDBLoad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 658);
            this.Controls.Add(this.fileZohoBackupDirectory);
            this.Controls.Add(this.txtBaseFilename);
            this.Controls.Add(this.lblBaseFileName);
            this.Controls.Add(this.fileOutputDirectory);
            this.Controls.Add(this.btnCreateZohoInportFile);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.fileSTRExcelDB);
            this.Name = "frmSTRHotelDBLoad";
            this.Text = "STR Hotel DB Load";
            this.Load += new System.EventHandler(this.frmSTRHotelDBLoad_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctlFileBrowser fileSTRExcelDB;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Button btnCreateZohoInportFile;
        private ctlFileBrowser fileOutputDirectory;
        private System.Windows.Forms.Label lblBaseFileName;
        private System.Windows.Forms.TextBox txtBaseFilename;
        private ctlFileBrowser fileZohoBackupDirectory;
    }
}