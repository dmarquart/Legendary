using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data;
using System.Data.SqlClient;
using ExcelDna.Integration.CustomUI;
using ExcelDna.Integration;
using ExcelDna.IntelliSense;

namespace LegendaryExcelAddIn
{
    //public class The9thAgeBuilderAddIn : IExcelAddIn
    //{
    //    public void AutoOpen()
    //    {
    //        //IntelliSenseServer.Install(); 
    //    }
    //    public void AutoClose()
    //    {
    //        //IntelliSenseServer.Uninstall(); 
    //    }
    //}

    public class WinFormWrapper : IWin32Window
    {
        private IntPtr _IntPtrHandle;
        public WinFormWrapper(IntPtr handle)
        {
            _IntPtrHandle = handle;
        }
        public IntPtr Handle
        {
            get
            {
                return _IntPtrHandle;
            }
        }
    }

    [ComVisible(true)]
    public class MyRibbon : ExcelRibbon
    {
        private List<string> _ArmyList = new List<string>();
        //private Application _excel;
        //private IRibbonUI _thisRibbon;

        public MyRibbon()
        {
            _ArmyList.Add("Orcs + Goblins");
            _ArmyList.Add("Equitaine");
            _ArmyList.Add("Syvestine Elfs");
            _ArmyList.Add("Warriors of Dark Gods");
        }

        public override string GetCustomUI(string RibbonID)
        {
            string ribbonXml = GetCustomRibbonXML();
            return ribbonXml;
        }

        private string GetCustomRibbonXML()
        {  
            string[] mylist = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames();
            string ribbonXml;
            var thisAssembly = typeof(MyRibbon).Assembly;
            var resourceName = typeof(MyRibbon).Namespace + ".LegendaryExcelRibbon.xml";

            using (Stream stream = thisAssembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                ribbonXml = reader.ReadToEnd();
            }

            if (ribbonXml == null)
            {
                throw new Exception("Ribbon Resource Not Found");
            }
            return ribbonXml;
        }

        //public System.Drawing.Image LoadImage_Callback()
        //{
        //    return null;
        //}

        //public void OnLoad_Callback()
        //{
        //}

        public string GetLabel_Callback(IRibbonControl control)
        {
            if (control.Id == "tabLegendary")
                return "Legendary";
            return "Unknown";
        }

        public bool GetVisible_Callback(IRibbonControl control)
        {
            return true;
        }

        public void cboArmyOnChange_Callback(IRibbonControl control, string text)
        {
            MessageBox.Show($"ComboBox Army On Change Called {text} Selected");
        }

        public int GetItemCount_Callback(IRibbonControl control)
        {
            return _ArmyList.Count;
        }

        public string GetItemID_Callback(IRibbonControl control, int index)
        {
            return index.ToString("0000");
        }

        public string GetItemLabel_Callback(IRibbonControl control, int index)
        {
            return _ArmyList[index];
        }

        public System.Drawing.Image GetImage_Callback(IRibbonControl control)
        {
            System.Drawing.Image bmp = null;

            if (control.Tag == "btnPhoenixCheck")
                bmp = new Bitmap(LegendaryExcelAddIn.Properties.Resources.btnPhoenixCheck);
            else if (control.Tag == "btnCreateConReport")
                bmp = new Bitmap(LegendaryExcelAddIn.Properties.Resources.btnCreateConReport);
            else if (control.Tag == "btnGetPipedriveList")
                bmp = new Bitmap(LegendaryExcelAddIn.Properties.Resources.btnGetPipedriveList);
            
            return (System.Drawing.Image)bmp;
        }

        public void DoAction(IRibbonControl control)
        {
            if (control.Id == "btnPhoenixCheck")
            {
                var form = new LegendaryLibrary.frmPhoenixCheck();
                form.Show();
            }
            else if (control.Id == "btnCreateConReport")
            {
                var form = new LegendaryLibrary.frmConsolidatedReport();
                form.Show();
            }
            else if (control.Id == "btnGetPipedriveList")
            {
                var form = new LegendaryLibrary.frmPipedriveFilter();
                form.Show();
            }
        }

    }
}
