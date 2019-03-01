using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LegendaryLibrary
{ 
    public partial class frmPipedriveResults : Form
    {
        public frmPipedriveResults()
        {
            InitializeComponent();
        }

        public void SetFilterDescription(string text)
        {
            this.label1.Text = text;
        }

        public void ShowPipedriveData(List<PipedriveAugmentation.PipedriveTarget> targets)
        {
            this.dataGridView1.DataSource = targets;
            this.dataGridView1.Refresh();
            this.label1.Text = $@"Count: {targets.Count} ";
        }

        private void btnToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                ExcelReporter.OutputGridToNewWorkbook(this.dataGridView1, this.label1.Text);
            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
        }
    }
}
