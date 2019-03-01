using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LegendaryLibrary
{
    static public class Legendary
    {
        static public bool StatusStopNow = false;
        static public TextBox StatusTextBox = null;
        static public ToolStripStatusLabel StatusStripLabel = null;
        static public ProgressBar StatusProgressBar = null;
        static public ToolStripProgressBar StatusStripProgressBar = null;

        static private bool _StatusSuspendStatusBarUpdating = false;
        static public bool StatusSuspendStatusBarUpdating
        {
            get
            {
                return _StatusSuspendStatusBarUpdating;
            }
            set
            {
                _StatusSuspendStatusBarUpdating = value;
                if (StatusStripLabel != null)
                    StatusStripLabel.Text = "";
                if (StatusStripProgressBar != null)
                    StatusStripProgressBar.Value = 0;
            }
        }

        static public void StatusClearStrip()
        {
            if (StatusStripLabel != null)
                StatusStripLabel.Text = "";
            if (StatusStripProgressBar != null)
                StatusStripProgressBar.Value = 0;
        }

        static public string PipedriveAPIKey => $@"34c7a9c7ee53f1aac2d5bb752c870b65ee12fb90";

        public static Uri PipedriveApiUrl => _apiUrl.Value; // { get { return _apiUrl.Value; } }

        static readonly Lazy<Uri> _apiUrl = new Lazy<Uri>(() =>
        {
            string uri = $@"https://lodgingopportunityf.pipedrive.com";
            if (uri != null)
                return new Uri(uri);
            return null;
        });

        static public class ConsolidatedAccountCategories
        {
            static public string NotSet { get; } = "NOT SET";
            static public string Assets { get; } = "Assets";
            static public string CurrentLiabilities { get; } = "Current Liabilities";
            static public string Expense { get; } = "Expense";
            static public string MembersEquity { get; } = "Members' Equity";
            static public string MITCH { get; } = "MITCH";
            static public string Other { get; } = "Other";
            static public string Revenue { get; } = "Revenue";
            static public bool IsValid(string value)
            {
                if ((string.Compare(value, NotSet, true) == 0) ||
                    (string.Compare(value, Assets, true) == 0) ||
                    (string.Compare(value, CurrentLiabilities, true) == 0) ||
                    (string.Compare(value, Expense, true) == 0) ||
                    (string.Compare(value, MembersEquity, true) == 0) ||
                    (string.Compare(value, MITCH, true) == 0) ||
                    (string.Compare(value, Other, true) == 0) ||
                    (string.Compare(value, Revenue, true) == 0))
                    return true;
                return false;
            }
        }

        static public string GetSqlConnectionString()
        {
            return "Data Source=DESKTOP-TDFEK2J\\SQLEXPRESS;Initial Catalog=Legendary;Integrated Security=true";
        }

        static public void UpdateProgress(double percentComplete)
        {
            UpdateProgress((int)percentComplete);
        }

        static public void UpdateProgress(int percentComplete)
        {
            if (StatusProgressBar != null)
            {
                StatusProgressBar.Increment(percentComplete - StatusProgressBar.Value);
            }
            if ((StatusStripProgressBar != null) && (!StatusSuspendStatusBarUpdating))
            {
                StatusStripProgressBar.Increment(percentComplete - StatusStripProgressBar.Value);
            }
        }

        static public void UpdateStatus(Exception ex)
        {
            Legendary.UpdateStatus(ex.Message + "\r\n" + 
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
            if ((StatusStripLabel != null) && (!StatusSuspendStatusBarUpdating))
            {
                statusText = statusText.Replace("\n", "").Replace("\r", "");
                StatusStripLabel.Text = statusText;
            }
        }

        static public string GetStateByName(string name)
        {
            switch (name.ToUpper())
            {
                case "ALABAMA": return "AL";
                case "ALASKA": return "AK";
                case "AMERICAN SAMOA": return "AS";
                case "ARIZONA": return "AZ";
                case "ARKANSAS": return "AR";
                case "CALIFORNIA": return "CA";
                case "COLORADO": return "CO";
                case "CONNECTICUT": return "CT";
                case "DELAWARE": return "DE";
                case "DISTRICT OF COLUMBIA": return "DC";
                case "FEDERATED STATES OF MICRONESIA": return "FM";
                case "FLORIDA": return "FL";
                case "GEORGIA": return "GA";
                case "GUAM": return "GU";
                case "HAWAII": return "HI";
                case "IDAHO": return "ID";
                case "ILLINOIS": return "IL";
                case "INDIANA": return "IN";
                case "IOWA": return "IA";
                case "KANSAS": return "KS";
                case "KENTUCKY": return "KY";
                case "LOUISIANA": return "LA";
                case "MAINE": return "ME";
                case "MARSHALL ISLANDS": return "MH";
                case "MARYLAND": return "MD";
                case "MASSACHUSETTS": return "MA";
                case "MICHIGAN": return "MI";
                case "MINNESOTA": return "MN";
                case "MISSISSIPPI": return "MS";
                case "MISSOURI": return "MO";
                case "MONTANA": return "MT";
                case "NEBRASKA": return "NE";
                case "NEVADA": return "NV";
                case "NEW HAMPSHIRE": return "NH";
                case "NEW JERSEY": return "NJ";
                case "NEW MEXICO": return "NM";
                case "NEW YORK": return "NY";
                case "NORTH CAROLINA": return "NC";
                case "NORTH DAKOTA": return "ND";
                case "NORTHERN MARIANA ISLANDS": return "MP";
                case "OHIO": return "OH";
                case "OKLAHOMA": return "OK";
                case "OREGON": return "OR";
                case "PALAU": return "PW";
                case "PENNSYLVANIA": return "PA";
                case "PUERTO RICO": return "PR";
                case "RHODE ISLAND": return "RI";
                case "SOUTH CAROLINA": return "SC";
                case "SOUTH DAKOTA": return "SD";
                case "TENNESSEE": return "TN";
                case "TEXAS": return "TX";
                case "UTAH": return "UT";
                case "VERMONT": return "VT";
                case "VIRGIN ISLANDS": return "VI";
                case "VIRGINIA": return "VA";
                case "WASHINGTON": return "WA";
                case "WEST VIRGINIA": return "WV";
                case "WISCONSIN": return "WI";
                case "WYOMING": return "WY";
            }
            return name;
        }
    }
}
