namespace LegendaryApp
{
    partial class frmLegendaryMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLegendaryMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiConsolidatedReport = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPipedriveFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPhoenixCheck = new System.Windows.Forms.ToolStripMenuItem();
            this.windowManagerPanel1 = new MDIWindowManager.WindowManagerPanel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsBtnConsolidatedReport = new System.Windows.Forms.ToolStripButton();
            this.tsBtnPipedriveFilter = new System.Windows.Forms.ToolStripButton();
            this.tsBtnSTRLoad = new System.Windows.Forms.ToolStripButton();
            this.tsmiSTRHotelDBLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.reportsToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1075, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // reportsToolStripMenuItem
            // 
            this.reportsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiConsolidatedReport});
            this.reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            this.reportsToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.reportsToolStripMenuItem.Text = "Reports";
            // 
            // tsmiConsolidatedReport
            // 
            this.tsmiConsolidatedReport.Name = "tsmiConsolidatedReport";
            this.tsmiConsolidatedReport.Size = new System.Drawing.Size(153, 22);
            this.tsmiConsolidatedReport.Text = "Consolidated...";
            this.tsmiConsolidatedReport.Click += new System.EventHandler(this.tsmiConsolidatedReport_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiPipedriveFilter,
            this.tsmiPhoenixCheck,
            this.tsmiSTRHotelDBLoad});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // tsmiPipedriveFilter
            // 
            this.tsmiPipedriveFilter.Name = "tsmiPipedriveFilter";
            this.tsmiPipedriveFilter.Size = new System.Drawing.Size(182, 22);
            this.tsmiPipedriveFilter.Text = "Pipedrive Filter...";
            this.tsmiPipedriveFilter.Click += new System.EventHandler(this.tsmiPipedriveFilter_Click);
            // 
            // tsmiPhoenixCheck
            // 
            this.tsmiPhoenixCheck.Name = "tsmiPhoenixCheck";
            this.tsmiPhoenixCheck.Size = new System.Drawing.Size(182, 22);
            this.tsmiPhoenixCheck.Text = "Phoenix Check...";
            this.tsmiPhoenixCheck.Click += new System.EventHandler(this.tsmiPhoenixCheck_Click);
            // 
            // windowManagerPanel1
            // 
            this.windowManagerPanel1.AllowUserVerticalRepositioning = false;
            this.windowManagerPanel1.AutoDetectMdiChildWindows = true;
            this.windowManagerPanel1.AutoHide = false;
            this.windowManagerPanel1.ButtonRenderMode = MDIWindowManager.ButtonRenderMode.System;
            this.windowManagerPanel1.DisableCloseAction = false;
            this.windowManagerPanel1.DisableHTileAction = false;
            this.windowManagerPanel1.DisablePopoutAction = false;
            this.windowManagerPanel1.DisableTileAction = false;
            this.windowManagerPanel1.EnableTabPaintEvent = false;
            this.windowManagerPanel1.Location = new System.Drawing.Point(2, 51);
            this.windowManagerPanel1.MinMode = false;
            this.windowManagerPanel1.Name = "windowManagerPanel1";
            this.windowManagerPanel1.Orientation = MDIWindowManager.WindowManagerOrientation.Top;
            this.windowManagerPanel1.ShowCloseButton = false;
            this.windowManagerPanel1.ShowIcons = false;
            this.windowManagerPanel1.ShowLayoutButtons = false;
            this.windowManagerPanel1.ShowTitle = false;
            this.windowManagerPanel1.Size = new System.Drawing.Size(1071, 30);
            this.windowManagerPanel1.Style = MDIWindowManager.TabStyle.ModernTabs;
            this.windowManagerPanel1.TabIndex = 6;
            this.windowManagerPanel1.TabRenderMode = MDIWindowManager.TabsProvider.System;
            this.windowManagerPanel1.Text = "windowManagerPanel1";
            this.windowManagerPanel1.TitleBackColor = System.Drawing.SystemColors.ControlDark;
            this.windowManagerPanel1.TitleForeColor = System.Drawing.SystemColors.ControlLightLight;
            // 
            // statusStrip1
            // 
            this.statusStrip1.AutoSize = false;
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 883);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1075, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(300, 16);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.AutoSize = false;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(1800, 17);
            this.toolStripStatusLabel2.Text = "toolStripStatusLabel2";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnConsolidatedReport,
            this.tsBtnPipedriveFilter,
            this.tsBtnSTRLoad});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1075, 25);
            this.toolStrip1.TabIndex = 9;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsBtnConsolidatedReport
            // 
            this.tsBtnConsolidatedReport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnConsolidatedReport.Image = global::LegendaryApp.Properties.Resources.ConsolidatedReport;
            this.tsBtnConsolidatedReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnConsolidatedReport.Name = "tsBtnConsolidatedReport";
            this.tsBtnConsolidatedReport.Size = new System.Drawing.Size(23, 22);
            this.tsBtnConsolidatedReport.Text = "Consolidated Report Generation...";
            this.tsBtnConsolidatedReport.Click += new System.EventHandler(this.tsBtnConsolidatedReport_Click);
            // 
            // tsBtnPipedriveFilter
            // 
            this.tsBtnPipedriveFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnPipedriveFilter.Image = global::LegendaryApp.Properties.Resources.Pipedrive;
            this.tsBtnPipedriveFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnPipedriveFilter.Name = "tsBtnPipedriveFilter";
            this.tsBtnPipedriveFilter.Size = new System.Drawing.Size(23, 22);
            this.tsBtnPipedriveFilter.Text = "Create Pipedrive Target List";
            this.tsBtnPipedriveFilter.Click += new System.EventHandler(this.tsBtnPipedriveFilter_Click);
            // 
            // tsBtnSTRLoad
            // 
            this.tsBtnSTRLoad.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnSTRLoad.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnSTRLoad.Image")));
            this.tsBtnSTRLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnSTRLoad.Name = "tsBtnSTRLoad";
            this.tsBtnSTRLoad.Size = new System.Drawing.Size(23, 22);
            this.tsBtnSTRLoad.Text = "Load STR DB Into Zoho";
            this.tsBtnSTRLoad.Click += new System.EventHandler(this.tsBtnSTRLoad_Click);
            // 
            // sTRHotelDBLoadToolStripMenuItem
            // 
            this.tsmiSTRHotelDBLoad.Name = "sTRHotelDBLoadToolStripMenuItem";
            this.tsmiSTRHotelDBLoad.Size = new System.Drawing.Size(182, 22);
            this.tsmiSTRHotelDBLoad.Text = "STR Hotel DB Load...";
            this.tsmiSTRHotelDBLoad.Click += new System.EventHandler(this.tsmiSTRHotelDBLoad_Click);
            // 
            // frmLegendaryMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1075, 905);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.windowManagerPanel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmLegendaryMain";
            this.Text = "Legendary Capital";
            this.Load += new System.EventHandler(this.frmLegendaryMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiConsolidatedReport;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiPipedriveFilter;
        private System.Windows.Forms.ToolStripMenuItem tsmiPhoenixCheck;
        private MDIWindowManager.WindowManagerPanel windowManagerPanel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsBtnConsolidatedReport;
        private System.Windows.Forms.ToolStripButton tsBtnPipedriveFilter;
        private System.Windows.Forms.ToolStripButton tsBtnSTRLoad;
        private System.Windows.Forms.ToolStripMenuItem tsmiSTRHotelDBLoad;
    }
}

