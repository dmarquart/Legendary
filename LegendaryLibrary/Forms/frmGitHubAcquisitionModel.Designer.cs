namespace LegendaryLibrary
{
    partial class frmGitHubAcquisitionModel
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
            this.fileAcquisitionDirectory = new LegendaryLibrary.ctlFileBrowser();
            this.fileAcquisitionModelFile = new LegendaryLibrary.ctlFileBrowser();
            this.fileEditDirectory = new LegendaryLibrary.ctlFileBrowser();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnAction1 = new System.Windows.Forms.Button();
            this.btnAction2 = new System.Windows.Forms.Button();
            this.btnAction3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // fileAcquisitionDirectory
            // 
            this.fileAcquisitionDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileAcquisitionDirectory.DirectoryNotFile = false;
            this.fileAcquisitionDirectory.FileFilterString = "Excel files (*.xl*)|*.xl*|Excel files (*.xl*)|*.xl*";
            this.fileAcquisitionDirectory.FileLabel = "Acquisition Model Directory:";
            this.fileAcquisitionDirectory.FullFilePath = "L:\\REIT III\\Acquisition\\HamptonWitchita";
            this.fileAcquisitionDirectory.InitialDirectory = "C:\\";
            this.fileAcquisitionDirectory.Location = new System.Drawing.Point(11, 41);
            this.fileAcquisitionDirectory.Margin = new System.Windows.Forms.Padding(2);
            this.fileAcquisitionDirectory.Name = "fileAcquisitionDirectory";
            this.fileAcquisitionDirectory.Size = new System.Drawing.Size(752, 25);
            this.fileAcquisitionDirectory.TabIndex = 131;
            this.fileAcquisitionDirectory.Visible = false;
            // 
            // fileAcquisitionModelFile
            // 
            this.fileAcquisitionModelFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileAcquisitionModelFile.DirectoryNotFile = false;
            this.fileAcquisitionModelFile.FileFilterString = "Excel files (*.xl*)|*.xl*|Excel files (*.xl*)|*.xl*";
            this.fileAcquisitionModelFile.FileLabel = "Acquistion Model Master:";
            this.fileAcquisitionModelFile.FullFilePath = "L:\\REIT III\\Acquisition\\HamptonWitchita\\Hotel AW v12.7 - Hampton Wichita - 125.xl" +
    "sx";
            this.fileAcquisitionModelFile.InitialDirectory = "C:\\";
            this.fileAcquisitionModelFile.Location = new System.Drawing.Point(39, 70);
            this.fileAcquisitionModelFile.Margin = new System.Windows.Forms.Padding(2);
            this.fileAcquisitionModelFile.Name = "fileAcquisitionModelFile";
            this.fileAcquisitionModelFile.Size = new System.Drawing.Size(724, 25);
            this.fileAcquisitionModelFile.TabIndex = 132;
            // 
            // fileEditDirectory
            // 
            this.fileEditDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileEditDirectory.DirectoryNotFile = false;
            this.fileEditDirectory.FileFilterString = "Excel files (*.xl*)|*.xl*|Excel files (*.xl*)|*.xl*";
            this.fileEditDirectory.FileLabel = "Output Directory:";
            this.fileEditDirectory.FullFilePath = "C:\\LocalEditDirectory";
            this.fileEditDirectory.InitialDirectory = "C:\\";
            this.fileEditDirectory.Location = new System.Drawing.Point(62, 99);
            this.fileEditDirectory.Margin = new System.Windows.Forms.Padding(2);
            this.fileEditDirectory.Name = "fileEditDirectory";
            this.fileEditDirectory.Size = new System.Drawing.Size(701, 25);
            this.fileEditDirectory.TabIndex = 133;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblStatus.Location = new System.Drawing.Point(0, 133);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(774, 29);
            this.lblStatus.TabIndex = 134;
            this.lblStatus.Text = "Locked for Editing by Alec Worwa";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAction1
            // 
            this.btnAction1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAction1.Location = new System.Drawing.Point(178, 177);
            this.btnAction1.Name = "btnAction1";
            this.btnAction1.Size = new System.Drawing.Size(120, 34);
            this.btnAction1.TabIndex = 135;
            this.btnAction1.Text = "Get Read Only";
            this.btnAction1.UseVisualStyleBackColor = true;
            // 
            // btnAction2
            // 
            this.btnAction2.Enabled = false;
            this.btnAction2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAction2.Location = new System.Drawing.Point(348, 177);
            this.btnAction2.Name = "btnAction2";
            this.btnAction2.Size = new System.Drawing.Size(120, 34);
            this.btnAction2.TabIndex = 136;
            this.btnAction2.Text = "Edit";
            this.btnAction2.UseVisualStyleBackColor = true;
            // 
            // btnAction3
            // 
            this.btnAction3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAction3.Location = new System.Drawing.Point(508, 177);
            this.btnAction3.Name = "btnAction3";
            this.btnAction3.Size = new System.Drawing.Size(120, 34);
            this.btnAction3.TabIndex = 137;
            this.btnAction3.Text = "Update Master";
            this.btnAction3.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.label1.Location = new System.Drawing.Point(0, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(774, 29);
            this.label1.TabIndex = 138;
            this.label1.Text = "Hampton Inn, Wichita KS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmGitHubAcquisitionModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 226);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAction3);
            this.Controls.Add(this.btnAction2);
            this.Controls.Add(this.btnAction1);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.fileEditDirectory);
            this.Controls.Add(this.fileAcquisitionModelFile);
            this.Controls.Add(this.fileAcquisitionDirectory);
            this.Name = "frmGitHubAcquisitionModel";
            this.Text = "Acquisition Model";
            this.ResumeLayout(false);

        }

        #endregion

        private ctlFileBrowser fileAcquisitionDirectory;
        private ctlFileBrowser fileAcquisitionModelFile;
        private ctlFileBrowser fileEditDirectory;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnAction1;
        private System.Windows.Forms.Button btnAction2;
        private System.Windows.Forms.Button btnAction3;
        private System.Windows.Forms.Label label1;
    }
}