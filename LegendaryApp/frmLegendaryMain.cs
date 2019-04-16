using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LegendaryLibrary;

namespace LegendaryApp
{
    public partial class frmLegendaryMain : Form


    {
        public frmLegendaryMain()
        {
            InitializeComponent();
           
        }

        private void btnConsolidatedReport_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new frmConsolidatedReport();
                form.Show();
            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
        }

        private void btnPhoenixCheck_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new LegendaryLibrary.frmPhoenixCheck();
                form.Show();
            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
        }

        private void btnPipedriveFilter_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new LegendaryLibrary.frmPipedriveFilter();
                form.Show();
            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
        }

        private void frmLegendaryMain_Load(object sender, EventArgs e)
        {
            try
            {
                Legendary.StatusStripProgressBar = this.toolStripProgressBar1;
                Legendary.StatusStripLabel = this.toolStripStatusLabel1;
            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
        }

        private void tsmiConsolidatedReport_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new LegendaryLibrary.frmConsolidatedReport();
                form.MdiParent = this;
                form.Show();
            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
        }

        private void tsmiPipedriveFilter_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new LegendaryLibrary.frmPipedriveFilter();
                form.MdiParent = this;
                form.Show();
            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
        }

        private void tsmiPhoenixCheck_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new LegendaryLibrary.frmPhoenixCheck();
                form.MdiParent = this;
                form.Show();
            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
        }

        private void tsmiSTRHotelDBLoad_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new LegendaryLibrary.frmSTRHotelDBLoad();
                form.MdiParent = this;
                form.Show();
            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
        }

        private void tsBtnConsolidatedReport_Click(object sender, EventArgs e)
        {
            tsmiConsolidatedReport_Click(null, null);
        }

        private void tsBtnPipedriveFilter_Click(object sender, EventArgs e)
        {
            tsmiPipedriveFilter_Click(null, null); 
        }

        private void tsBtnSTRLoad_Click(object sender, EventArgs e)
        {
            tsmiSTRHotelDBLoad_Click(null, null);
        }

        private void tsBtnGitHub_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new LegendaryLibrary.frmGitHubAcquisitionModel();
                form.MdiParent = this;
                form.Show();
            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
        }
    }
}
