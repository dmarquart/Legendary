namespace LegendaryExcelAddIn
{
    partial class ctlFileBrowser
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnBrowseForFile = new System.Windows.Forms.Button();
            this.lblFileLabel = new System.Windows.Forms.Label();
            this.txtFullPathToFile = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnBrowseForFile
            // 
            this.btnBrowseForFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseForFile.Location = new System.Drawing.Point(988, 3);
            this.btnBrowseForFile.Name = "btnBrowseForFile";
            this.btnBrowseForFile.Size = new System.Drawing.Size(48, 31);
            this.btnBrowseForFile.TabIndex = 5;
            this.btnBrowseForFile.Text = "...";
            this.btnBrowseForFile.UseVisualStyleBackColor = true;
            this.btnBrowseForFile.Click += new System.EventHandler(this.btnBrowseForFile_Click);
            // 
            // lblFileLabel
            // 
            this.lblFileLabel.AutoSize = true;
            this.lblFileLabel.Location = new System.Drawing.Point(3, 6);
            this.lblFileLabel.Name = "lblFileLabel";
            this.lblFileLabel.Size = new System.Drawing.Size(183, 25);
            this.lblFileLabel.TabIndex = 4;
            this.lblFileLabel.Text = "<label for the file>";
            // 
            // txtFullPathToFile
            // 
            this.txtFullPathToFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFullPathToFile.Location = new System.Drawing.Point(192, 3);
            this.txtFullPathToFile.Name = "txtFullPathToFile";
            this.txtFullPathToFile.Size = new System.Drawing.Size(790, 31);
            this.txtFullPathToFile.TabIndex = 3;
            // 
            // ctlFileBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnBrowseForFile);
            this.Controls.Add(this.lblFileLabel);
            this.Controls.Add(this.txtFullPathToFile);
            this.Name = "ctlFileBrowser";
            this.Size = new System.Drawing.Size(1041, 36);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBrowseForFile;
        private System.Windows.Forms.Label lblFileLabel;
        private System.Windows.Forms.TextBox txtFullPathToFile;
    }
}
