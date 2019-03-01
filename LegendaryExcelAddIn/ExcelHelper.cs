using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Office = NetOffice.OfficeApi;
using Excel = NetOffice.ExcelApi;

namespace LegendaryExcelAddIn
{
    static public class ExcelHelper
    {
        static public Excel.Worksheet GetWorksheet(Excel.Workbook book, string sheetName, bool returnFirstSheetIfNotFound = true)
        {
            try
            {
                Excel.Worksheet sheet = null;
                Excel.Worksheet firstSheet = null;
                foreach (var worksheet in book.Worksheets)
                {
                    if (firstSheet == null)
                        firstSheet = (Excel.Worksheet)worksheet;
                    Excel.Worksheet thisWorksheet = (Excel.Worksheet)worksheet;
                    if ((thisWorksheet != null) && (string.Compare(thisWorksheet.Name, sheetName, true) == 0))
                    {
                        sheet = thisWorksheet;
                        break;
                    }
                }

                if ((sheet == null) && returnFirstSheetIfNotFound)
                    return firstSheet;

                return sheet;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
