namespace LegendaryExcelAddIn
{
    partial class frmPhoenixCheck
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRunCheck = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblHeading = new System.Windows.Forms.Label();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.fileMasterDistribution = new LegendaryLibrary.ctlFileBrowser();
            this.fileCapitalRaisedDashboard = new LegendaryLibrary.ctlFileBrowser();
            this.fileISSFile = new LegendaryLibrary.ctlFileBrowser();
            this.fileCommission = new LegendaryLibrary.ctlFileBrowser();
            this.filePhoenix = new LegendaryLibrary.ctlFileBrowser();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.ForeColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(525, 147);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(623, 2);
            this.panel1.TabIndex = 106;
            // 
            // btnRunCheck
            // 
            this.btnRunCheck.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRunCheck.BackColor = System.Drawing.Color.LightGray;
            this.btnRunCheck.Font = new System.Drawing.Font("Calibri", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRunCheck.ForeColor = System.Drawing.Color.Black;
            this.btnRunCheck.Location = new System.Drawing.Point(25, 430);
            this.btnRunCheck.Name = "btnRunCheck";
            this.btnRunCheck.Size = new System.Drawing.Size(1623, 50);
            this.btnRunCheck.TabIndex = 111;
            this.btnRunCheck.Text = "RUN CHECK";
            this.btnRunCheck.UseVisualStyleBackColor = false;
            this.btnRunCheck.Click += new System.EventHandler(this.btnRunCheck_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.ForeColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(525, 398);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(623, 2);
            this.panel2.TabIndex = 107;
            // 
            // lblHeading
            // 
            this.lblHeading.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblHeading.Location = new System.Drawing.Point(25, 17);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(1623, 43);
            this.lblHeading.TabIndex = 112;
            this.lblHeading.Text = "Phoenix Investor Import File Check";
            this.lblHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtStatus
            // 
            this.txtStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatus.Location = new System.Drawing.Point(25, 508);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtStatus.Size = new System.Drawing.Size(1623, 590);
            this.txtStatus.TabIndex = 113;
            // 
            // fileMasterDistribution
            // 
            this.fileMasterDistribution.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileMasterDistribution.FileFilterString = "Excel files (*.xl*)|*.xl*|Excel files (*.xl*)|*.xl*";
            this.fileMasterDistribution.FileLabel = "Master Distribution Excel File";
            this.fileMasterDistribution.FullFilePath = "";
            this.fileMasterDistribution.InitialDirectory = "C:\\";
            this.fileMasterDistribution.Location = new System.Drawing.Point(25, 330);
            this.fileMasterDistribution.Name = "fileMasterDistribution";
            this.fileMasterDistribution.Size = new System.Drawing.Size(1623, 36);
            this.fileMasterDistribution.TabIndex = 110;
            // 
            // fileCapitalRaisedDashboard
            // 
            this.fileCapitalRaisedDashboard.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileCapitalRaisedDashboard.FileFilterString = "Excel files (*.xl*)|*.xl*|Excel files (*.xl*)|*.xl*";
            this.fileCapitalRaisedDashboard.FileLabel = "Capital Raised Dashboard Excel File";
            this.fileCapitalRaisedDashboard.FullFilePath = "";
            this.fileCapitalRaisedDashboard.InitialDirectory = "C:\\";
            this.fileCapitalRaisedDashboard.Location = new System.Drawing.Point(25, 282);
            this.fileCapitalRaisedDashboard.Name = "fileCapitalRaisedDashboard";
            this.fileCapitalRaisedDashboard.Size = new System.Drawing.Size(1623, 36);
            this.fileCapitalRaisedDashboard.TabIndex = 109;
            // 
            // fileISSFile
            // 
            this.fileISSFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileISSFile.FileFilterString = "";
            this.fileISSFile.FileLabel = "ISS Investor Excel Export File";
            this.fileISSFile.FullFilePath = "";
            this.fileISSFile.InitialDirectory = "C:\\";
            this.fileISSFile.Location = new System.Drawing.Point(25, 234);
            this.fileISSFile.Name = "fileISSFile";
            this.fileISSFile.Size = new System.Drawing.Size(1623, 36);
            this.fileISSFile.TabIndex = 108;
            // 
            // fileCommission
            // 
            this.fileCommission.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileCommission.FileFilterString = "Excel files (*.xl*)|*.xl*|Excel files (*.xl*)|*.xl*";
            this.fileCommission.FileLabel = "Commission Excel File";
            this.fileCommission.FullFilePath = "";
            this.fileCommission.InitialDirectory = "C:\\";
            this.fileCommission.Location = new System.Drawing.Point(25, 186);
            this.fileCommission.Name = "fileCommission";
            this.fileCommission.Size = new System.Drawing.Size(1623, 36);
            this.fileCommission.TabIndex = 107;
            // 
            // filePhoenix
            // 
            this.filePhoenix.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filePhoenix.FileFilterString = "Excel files (*.xl*)|*.xl*|Excel files (*.xl*)|*.xl*";
            this.filePhoenix.FileLabel = "Phoenix File to Check";
            this.filePhoenix.FullFilePath = "";
            this.filePhoenix.InitialDirectory = "C:\\";
            this.filePhoenix.Location = new System.Drawing.Point(25, 83);
            this.filePhoenix.Name = "filePhoenix";
            this.filePhoenix.Size = new System.Drawing.Size(1623, 36);
            this.filePhoenix.TabIndex = 0;
            // 
            // frmPhoenixCheck
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1673, 1128);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnRunCheck);
            this.Controls.Add(this.fileMasterDistribution);
            this.Controls.Add(this.fileCapitalRaisedDashboard);
            this.Controls.Add(this.fileISSFile);
            this.Controls.Add(this.fileCommission);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.filePhoenix);
            this.Name = "frmPhoenixCheck";
            this.Text = "  Legendary Capital";
            this.Load += new System.EventHandler(this.frmPhoenixCheck_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private LegendaryLibrary.ctlFileBrowser filePhoenix;
        private System.Windows.Forms.Panel panel1;
        private LegendaryLibrary.ctlFileBrowser fileCommission;
        private LegendaryLibrary.ctlFileBrowser fileISSFile;
        private LegendaryLibrary.ctlFileBrowser fileCapitalRaisedDashboard;
        private LegendaryLibrary.ctlFileBrowser fileMasterDistribution;
        private System.Windows.Forms.Button btnRunCheck;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblHeading;
        private System.Windows.Forms.TextBox txtStatus;
    }
}