using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LegendaryExcelAddIn
{
    public partial class frmMarketingChoice : Form
    {
        public frmMarketingChoice()
        {
            InitializeComponent();
        }

        private void chkHomeOffice_CheckedChanged(object sender, EventArgs e)
        {
            chkPartners.Enabled = chkHomeOffice.Checked;
            chkDueDiligence.Enabled = chkHomeOffice.Checked;
            chkOtherSubset.Enabled = chkHomeOffice.Checked;
        }

        private void chkRegions_CheckedChanged(object sender, EventArgs e)
        {
            chkAll.Enabled = chkRegions.Checked;
            chkWest.Enabled = chkRegions.Checked;
            chkSouthCentral.Enabled = chkRegions.Checked;
            chkNorthCentral.Enabled = chkRegions.Checked;
            chkSouthEast.Enabled = chkRegions.Checked;
            chkNorthEast.Enabled = chkRegions.Checked;
        }

        private void chkNationalAccountTeam_CheckedChanged(object sender, EventArgs e)
        {
            chkRickVitalie.Checked = chkNationalAccountTeam.Checked;
            chkJessicaNeill.Checked = chkNationalAccountTeam.Checked;
        }
    }
}
