using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LegendaryExcelAddIn
{
    static public class LegendaryConstants
    {
        static public TextBox StatusTextBox = null;

        static public class ConsolidatedAccountCategories
        {
            static public string Assets { get; } = "Assets";
            static public string CurrentLiabilities { get; } = "Current Liabilities";
            static public string Expense { get; } = "Expense";
            static public string MembersEquity { get; } = "Members' Equity";
            static public string MITCH { get; } = "MITCH";
            static public string Other { get; } = "Other";
            static public string Revenue { get; } = "Revenue";
        }

        static public string GetSqlConnectionString()
        {
            return "Data Source=DESKTOP-TDFEK2J\\SQLEXPRESS;Initial Catalog=Legendary;Integrated Security=true";
        }

        static public void UpdateStatusFromException(Exception ex)
        {
            LegendaryConstants.UpdateStatus(ex.Message + "\r\n" + 
                                            ex.InnerException?.Message + "\r\n" +
                                            ex.InnerException?.InnerException?.Message);
        }

        static public void UpdateStatus(string statusText, bool insertCarriageReturn = true)
        {
            if (StatusTextBox != null)
            {
                if (insertCarriageReturn)
                    StatusTextBox.AppendText("\r\n");
                StatusTextBox.AppendText(statusText);
                StatusTextBox.Refresh();
            }
        }
    }
}
