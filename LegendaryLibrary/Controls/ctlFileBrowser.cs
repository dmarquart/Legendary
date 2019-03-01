using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LegendaryLibrary
{
    public partial class ctlFileBrowser : UserControl
    {
        public ctlFileBrowser()
        {
            InitializeComponent();
        }

        public string FileLabel
        {
            get { return lblFileLabel.Text; }
            set
            {
                lblFileLabel.Text = value;
                txtFullPathToFile.Location = new Point(lblFileLabel.Location.X + lblFileLabel.Width + 10, txtFullPathToFile.Location.Y);
                txtFullPathToFile.Width = btnBrowseForFile.Location.X - 6 - txtFullPathToFile.Location.X;
            }
        }

        private bool _DirectoryNotFile = false;
        public bool DirectoryNotFile
        {
            get { return _DirectoryNotFile; }
            set { _DirectoryNotFile = value; }
        }

        private string _InitialDirectory = @"C:\";
        public string InitialDirectory
        {
            get { return _InitialDirectory; }
            set { _InitialDirectory = value; }
        }

        private string _FileFilterString = "Excel files (*.xl*)|*.xl*|Excel files (*.xl*)|*.xl*";
        public string FileFilterString
        {
            get { return _FileFilterString; }
            set { _FileFilterString = value; }
        }

        public string FullFilePath
        {
            get { return txtFullPathToFile.Text; }
            set { txtFullPathToFile.Text = value; }
        }
        private void btnBrowseForFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            FolderBrowserDialog fbdlg = new FolderBrowserDialog();

            string initialDirectory = _InitialDirectory;
            if (! string.IsNullOrWhiteSpace(txtFullPathToFile.Text))
            {
                string pathOnly = System.IO.Path.GetDirectoryName(txtFullPathToFile.Text);
                if (!string.IsNullOrEmpty(pathOnly))
                    initialDirectory = pathOnly;
            }

            if (_DirectoryNotFile)
            {
                fbdlg.SelectedPath = initialDirectory;
                DialogResult result = fbdlg.ShowDialog();
                if (result == DialogResult.OK)
                    txtFullPathToFile.Text = fbdlg.SelectedPath;
            }
            else
            {
                fdlg.Title = $"Open {FileLabel}";
                fdlg.InitialDirectory = initialDirectory;
                fdlg.Filter = _FileFilterString;
                fdlg.FilterIndex = 1;
                fdlg.RestoreDirectory = true;
                DialogResult result = fdlg.ShowDialog();
                if (result == DialogResult.OK)
                    txtFullPathToFile.Text = fdlg.FileName;
            }

        }
    }
}
