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

namespace LegendaryExcelAddIn
{
    public partial class frmPhoenixCheck : Form
    {
        public frmPhoenixCheck()
        {
            InitializeComponent();
        }

        public SortedList<string, SortedList<string, string>> PhoenixByCU = null;
        public SortedList<string, SortedList<string, string>> PhoenixBySubIndex = null;
        public SortedList<string, SortedList<string, string>> PhoenixByBD = null;
        public SortedList<string, SortedList<string, string>> PhoenixByRep = null;
        public SortedList<string, SortedList<string, string>> ISS = null;
        public SortedList<string, SortedList<string, string>> CommissionBDR = null;
        public SortedList<string, SortedList<string, string>> CommissionMarketing = null;
        public SortedList<string, SortedList<string, string>> MasterDistribution = null;
        public SortedList<string, SortedList<string, string>> CRDashboardSubscriptions = null;
        public SortedList<string, SortedList<string, string>> CRDashboardBDs = null;
        public SortedList<string, SortedList<string, string>> CRDashboardReps = null;

        private void frmPhoenixCheck_Load(object sender, EventArgs e)
        {
            var keyValues = LegendaryKeyValue.LoadKeyValuesIntoSortedList();

            if (keyValues.ContainsKey("PhoenixCheck-PhoenixFile"))
                this.filePhoenix.FullFilePath = keyValues["PhoenixCheck-PhoenixFile"].Value;

            if (keyValues.ContainsKey("PhoenixCheck-CommissionFile"))
                this.fileCommission.FullFilePath = keyValues["PhoenixCheck-CommissionFile"].Value;

            if (keyValues.ContainsKey("PhoenixCheck-CapitalRaiseDashboardFile"))
                this.fileCapitalRaisedDashboard.FullFilePath = keyValues["PhoenixCheck-CapitalRaiseDashboardFile"].Value;

            if (keyValues.ContainsKey("PhoenixCheck-MasterDistributionFile"))
                this.fileMasterDistribution.FullFilePath = keyValues["PhoenixCheck-MasterDistributionFile"].Value;

            if (keyValues.ContainsKey("PhoenixCheck-ISSInvestorExportFile"))
                this.fileISSFile.FullFilePath = keyValues["PhoenixCheck-ISSInvestorExportFile"].Value;

            LegendaryConstants.StatusTextBox = this.txtStatus;
        }

        private void btnRunCheck_Click(object sender, EventArgs e)
        {
            try
            {
                LegendaryConstants.UpdateStatus("Phoenix File by CU Account Loading");
                PhoenixByCU = ExcelLoader.LoadExcelFile(filePhoenix.FullFilePath, "Cap Table Account Number");
                if (PhoenixByCU == null)
                {
                    LegendaryConstants.UpdateStatus("\nTerminating Phoenix File Check without Completing");
                    return;
                }
                LegendaryConstants.UpdateStatus("Phoenix File by Subscription Index Loading");
                PhoenixBySubIndex = ExcelLoader.LoadExcelFile(filePhoenix.FullFilePath, "Subscription Index");
                if (PhoenixByCU == null)
                {
                    LegendaryConstants.UpdateStatus("\nTerminating Phoenix File Check without Completing");
                    return;
                }
                LegendaryConstants.UpdateStatus("Phoenix File by Broker Dealer Loading");
                PhoenixByBD = ExcelLoader.LoadExcelFile(filePhoenix.FullFilePath, "Orig Broker Name");
                if (PhoenixByBD == null)
                {
                    LegendaryConstants.UpdateStatus("\nTerminating Phoenix File Check without Completing");
                    return;
                }
                LegendaryConstants.UpdateStatus("Phoenix File by Rep Name Loading");
                PhoenixByRep = ExcelLoader.LoadExcelFile(filePhoenix.FullFilePath, "Orig Rep Name 1");
                if (PhoenixByRep == null)
                {
                    LegendaryConstants.UpdateStatus("\nTerminating Phoenix File Check without Completing");
                    return;
                }

                LegendaryConstants.UpdateStatus("\r\nISS File Loading");
                ISS = ExcelLoader.LoadExcelFile(fileISSFile.FullFilePath, "Cu Account Number");
                if (ISS == null)
                {
                    LegendaryConstants.UpdateStatus("\nTerminating Phoenix File Check without Completing");
                    return;
                }

                LegendaryConstants.UpdateStatus("\r\nCommission File Commission Sheet Loading");
                CommissionBDR = ExcelLoader.LoadExcelFile(fileCommission.FullFilePath, "Index", "Commission", 1, 7);
                if (CommissionBDR == null)
                {
                    LegendaryConstants.UpdateStatus("\nTerminating Phoenix File Check without Completing");
                    return;
                }
                LegendaryConstants.UpdateStatus("\r\nCommission File Marketing Fee Sheet Loading");
                CommissionMarketing = ExcelLoader.LoadExcelFile(fileCommission.FullFilePath, "Index", "Marketing Overide", 1, 7);
                if (CommissionMarketing == null)
                {
                    LegendaryConstants.UpdateStatus("\nTerminating Phoenix File Check without Completing");
                    return;
                }

                LegendaryConstants.UpdateStatus("\r\nMaster Distribuiton File Loading");
                MasterDistribution = ExcelLoader.LoadExcelFile(fileMasterDistribution.FullFilePath, "Account Number", "Investor Contacts");
                if (MasterDistribution == null)
                {
                    LegendaryConstants.UpdateStatus("\nTerminating Phoenix File Check without Completing");
                    return;
                }

                LegendaryConstants.UpdateStatus("\r\nCapital Raise Dashboard Subscriptions Sheet File Loading");
                CRDashboardSubscriptions = ExcelLoader.LoadExcelFile(fileCapitalRaisedDashboard.FullFilePath, "Index", "Subscriptions", 4, 1);
                if (CRDashboardSubscriptions == null)
                {
                    LegendaryConstants.UpdateStatus("\nTerminating Phoenix File Check without Completing");
                    return;
                }
                LegendaryConstants.UpdateStatus("\r\nCapital Raise Dashboard File BDs Sheet Loading");
                CRDashboardBDs = ExcelLoader.LoadExcelFile(fileCapitalRaisedDashboard.FullFilePath, "Full Name", "BDs", 4, 1);
                if (CRDashboardBDs == null)
                {
                    LegendaryConstants.UpdateStatus("\nTerminating Phoenix File Check without Completing");
                    return;
                }
                LegendaryConstants.UpdateStatus("\r\nCapital Raise Dashboard File Reps Sheet Loading");
                CRDashboardReps = ExcelLoader.LoadExcelFile(fileCapitalRaisedDashboard.FullFilePath, "Producing Rep Name", "Reps", 5, 1);
                if (CRDashboardReps == null)
                {
                    LegendaryConstants.UpdateStatus("\r\nTerminating Phoenix File Check without Completing");
                    return;
                }

                LegendaryConstants.UpdateStatus("\r\n\r\n");
                LegendaryConstants.UpdateStatus("--------------------------------------------------------------------------");
                LegendaryConstants.UpdateStatus("                       DOING COMPARISON CHECK NOW !!                      ");
                LegendaryConstants.UpdateStatus("--------------------------------------------------------------------------");
                LegendaryConstants.UpdateStatus("   ");

                ComparePhoenixToISS();
                ComparePhoenixToCommission();
                ComparePhoenixToMasterDistribution();
                ComparePhoenixToCapitalRaiseDashboard();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.ToString()}\n\n{ex.InnerException?.ToString()}\n\n{ex.InnerException?.InnerException?.ToString()}",
                                $"Error!",  MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }


        private bool CompareStringValues(SortedList<string, string> phoenixList, string phoenixField,
                                         SortedList<string, string> otherList, string otherField, string otherListName,
                                         string matchCriteriaName, string matchCriteriaValue)
        {
            string phoenixValue;
            string otherValue;

            try
            {
                phoenixValue = phoenixList[phoenixField];
                otherValue = otherList[otherField];

                if (string.Compare(phoenixValue.Trim(), otherValue.Trim(), true) == 0)
                    return true;

                LegendaryConstants.UpdateStatus($"{otherListName} {otherField} \"{otherValue}\" vs \"{phoenixValue}\" " +
                                                $"does not equal Phoenix {phoenixField}  " +
                                                $"for {matchCriteriaName} '{matchCriteriaValue}'");
            }
            catch (Exception ex)
            {
                LegendaryConstants.UpdateStatusFromException(ex);
            }

            return false;
        }

        private bool CompareDateValues(SortedList<string, string> phoenixList, string phoenixField,
                                       SortedList<string, string> otherList, string otherField, string otherListName,
                                       string matchCriteriaName, string matchCriteriaValue)
        {
            string phoenixValue;
            double phoenixDouble;
            DateTime phoenixDate;
            string otherValue;
            double otherDouble;
            DateTime otherDate;

            try
            {
                phoenixValue = phoenixList[phoenixField];
                otherValue = otherList[otherField];

                if (!double.TryParse(phoenixValue, out phoenixDouble))
                    LegendaryConstants.UpdateStatus($"Parsing of Phoenix {phoenixField} for {matchCriteriaName} '{matchCriteriaValue}' failed.  Skipping comparison.");
                else if (!double.TryParse(otherValue, out otherDouble))
                    LegendaryConstants.UpdateStatus($"Parsing of {otherListName} {otherField} for {matchCriteriaName} '{matchCriteriaValue}' failed.");
                else
                {
                    phoenixDate = DateTime.FromOADate(phoenixDouble);
                    otherDate = DateTime.FromOADate(otherDouble);

                    if (phoenixDate == otherDate)
                        return true;

                    LegendaryConstants.UpdateStatus($"{otherListName} {otherField} {otherDate.ToShortDateString()} does not equal " +
                                                    $"Phoenix {phoenixField} {phoenixDate.ToShortDateString()} " +
                                                    $"for {matchCriteriaName} '{matchCriteriaValue}'");
                }
            }
            catch (Exception ex)
            {
                LegendaryConstants.UpdateStatusFromException(ex);
            }

            return false;
        }

        private bool CompareDoubleValues(SortedList<string,string> phoenixList, string phoenixField, 
                                         SortedList<string,string> otherList, string otherField, string otherListName, double otherMultiplier,
                                         string matchCriteriaName, string matchCriteriaValue )
        {
            bool success = false;
            bool parseStatus;
            string phoenixValue;
            double phoenixDouble;
            string otherValue;
            double otherDouble;

            try
            {

                phoenixValue = phoenixList[phoenixField];
                parseStatus = double.TryParse(phoenixValue, out phoenixDouble);
                if (!parseStatus)
                    LegendaryConstants.UpdateStatus($"Parsing of Phoenix {phoenixField} for {matchCriteriaName} '{matchCriteriaValue}' failed.  Skipping comparison.");
                else
                {
                    otherValue = otherList[otherField];
                    parseStatus = double.TryParse(otherValue, out otherDouble);
                    if (!parseStatus)
                        LegendaryConstants.UpdateStatus($"Parsing of {otherListName} {otherField} for {matchCriteriaName} '{matchCriteriaValue}' failed.");
                    else
                    {
                        if (phoenixDouble != (otherDouble * otherMultiplier))
                            LegendaryConstants.UpdateStatus($"{otherListName} {otherField} {otherDouble.ToString("0.00")} does not equal " +
                                                            $"Phoenix {phoenixField} {phoenixDouble.ToString("0.00")} " +
                                                            $"for {matchCriteriaName} '{matchCriteriaValue}'");
                    }
                }
            }
            catch (Exception ex)
            {
                LegendaryConstants.UpdateStatusFromException(ex);
            }

            return success;
        }

        private bool ComparePhoenixToISS()
        {
            LegendaryConstants.UpdateStatus("\r\nComparing Phoenix to ISS");

            SortedList<string, string> phoenixList = null;
            SortedList<string, string> issList = null;

            foreach (var phoenixPair in PhoenixByCU)
            {
                if (phoenixPair.Key == "HEADER")
                    continue;

                phoenixList = phoenixPair.Value;
                if (!ISS.ContainsKey(phoenixPair.Key))
                {
                    LegendaryConstants.UpdateStatus($"    Cannot Find CAP Account {phoenixPair.Key} In ISS Data, continuing to next row");
                    continue;
                }

                issList = ISS[phoenixPair.Key];

                string issValue = "issValue";
                string phoenixValue = "phoenixValue";

                if (issList["ACCOUNT"].Length >= 9)
                    issValue = issList["ACCOUNT"].Substring(0, 9);

                phoenixValue = phoenixList["SSN"];
                if (string.Compare(phoenixValue, issValue, true) != 0)
                    LegendaryConstants.UpdateStatus($"Social Security --> For CAP Account: {phoenixPair.Key} The ISS[Account] {issValue} " + 
                                                    $"does not match Phoenix[SSN] {phoenixValue} ");

                phoenixValue = phoenixList["INV TAX ID"];
                if (string.Compare(phoenixValue, issValue, true) != 0)
                    LegendaryConstants.UpdateStatus($"Social Security --> For CAP Account: {phoenixPair.Key} The ISS[Account] {issValue} " +
                                                    $"does not match Phoenix[Inv Tax ID] {phoenixValue} ");
            }

            LegendaryConstants.UpdateStatus("Phoenix to ISS Comparison is Complete (see above for issues)");

            return true;
        }

        private bool ComparePhoenixToCommission()
        {
            LegendaryConstants.UpdateStatus("\r\nComparing Phoenix to Commission");

            SortedList<string, string> phoenixList = null;
            SortedList<string, string> commissionList = null;

            foreach (var phoenixPair in PhoenixBySubIndex)
            {
                if (phoenixPair.Key == "HEADER")
                    continue;

                if (!CommissionBDR.ContainsKey(phoenixPair.Key))
                    LegendaryConstants.UpdateStatus($"    Cannot Find Subscription Index {phoenixPair.Key} In Commission Broker Dealer Data, continuing...");
                else
                {
                    phoenixList = phoenixPair.Value;
                    commissionList = CommissionBDR[phoenixPair.Key];

                    CompareDoubleValues(phoenixList, "BROKER COMMISSION AMT", commissionList, "AMOUNT", "BD Commission", -1.0, "Subscription Index", phoenixPair.Key);
                    CompareDateValues(phoenixList, "PAYMENT DATE", commissionList, "DATE", "BD Commission", "Subscription Index", phoenixPair.Key);
                }

                if (!CommissionMarketing.ContainsKey(phoenixPair.Key))
                    LegendaryConstants.UpdateStatus($"    Cannot Find Subscription Index {phoenixPair.Key} In Commission Marketing Data, continuing...");
                else
                {
                    phoenixList = phoenixPair.Value;
                    commissionList = CommissionMarketing[phoenixPair.Key];

                    CompareDoubleValues(phoenixList, "MARKING FEE AMT", commissionList, "AMOUNT", "BD Commission", -1.0, "Subscription Index", phoenixPair.Key);
                    CompareDateValues(phoenixList, "PAYMENT DATE", commissionList, "DATE", "BD Commission", "Subscription Index", phoenixPair.Key);
                }
            }

            LegendaryConstants.UpdateStatus("Phoenix to Commission Comparison is Complete (see above for issues)");

            return true;
        }

        private bool ComparePhoenixToMasterDistribution()
        {
            LegendaryConstants.UpdateStatus("\r\nComparing Phoenix to MasterDistribution");

            SortedList<string, string> phoenixList = null;
            SortedList<string, string> masterDistributionList = null;

            foreach (var phoenixPair in PhoenixByCU)
            {
                if (phoenixPair.Key == "HEADER")
                    continue;

                if (!MasterDistribution.ContainsKey(phoenixPair.Key))
                    LegendaryConstants.UpdateStatus($"    Cannot Find CU Account {phoenixPair.Key} In Master Distribution Data, continuing...");
                else
                {
                    phoenixList = phoenixPair.Value;
                    masterDistributionList = MasterDistribution[phoenixPair.Key];

                    CompareStringValues(phoenixList, "INVESTOR LEGAL ADDR 1", masterDistributionList, "ADDRESS LINE 1", "Master Distribution", "CU Account", phoenixPair.Key);

                    string phoenixCityStateZip = phoenixList["INVESTOR LEGAL CITY ST ZIP"];
                    string masterDistributionCityStateZip = $"{masterDistributionList["CITY"]}, {masterDistributionList["STATE"]} {masterDistributionList["ZIP CODE"]}";

                    if (string.Compare(phoenixCityStateZip.Trim(), masterDistributionCityStateZip.Trim(), true) != 0)
                        LegendaryConstants.UpdateStatus($"Master Distribution CITY STATE ZIP CODE '{masterDistributionCityStateZip}' vs '{phoenixCityStateZip}' " +
                                                        $"does not equal Phoenix INVESTOR LEGAL CITY ST ZIP " +
                                                        $"for CU Account '{phoenixPair.Key}'");
                    
                }

            }

            LegendaryConstants.UpdateStatus("Phoenix to Commission Comparison is Complete (see above for issues)");

            return true;
        }

        private bool ComparePhoenixToCapitalRaiseDashboard()
        {
            LegendaryConstants.UpdateStatus("\r\nComparing Phoenix to Capital Raise Dashboard");

            SortedList<string, string> phoenixList = null;
            SortedList<string, string> capitalRaiseList = null;

            foreach (var phoenixPair in PhoenixBySubIndex)
            {
                if (phoenixPair.Key == "HEADER")
                    continue;

                if (!CRDashboardSubscriptions.ContainsKey(phoenixPair.Key))
                    LegendaryConstants.UpdateStatus($"    Cannot Find Sub Index \"{phoenixPair.Key}\" In Capital Raise Dashboard Subscriptions Data, continuing...");
                else
                {
                    phoenixList = phoenixPair.Value;
                    capitalRaiseList = CRDashboardSubscriptions[phoenixPair.Key];
                    string listName = "Cap Raise Dashboard Subscriptions";

                    CompareDateValues(  phoenixList, "SUBDOC RECEIVED DATE",     capitalRaiseList, "ACCEPTANCE DATE",       listName,      "Sub Index", phoenixPair.Key);
                    CompareDateValues(  phoenixList, "ADMIT DATE",               capitalRaiseList, "ACCEPTANCE DATE",       listName,      "Sub Index", phoenixPair.Key);
                    CompareStringValues(phoenixList, "1ST PURCHASE OR ADD-ON?",  capitalRaiseList, "INITIAL OR ADDITIONAL", listName,      "Sub Index", phoenixPair.Key);
                    CompareDateValues(  phoenixList, "DEPOSIT DATE",             capitalRaiseList, "E.I. DATE",             listName,      "Sub Index", phoenixPair.Key);
                    CompareDoubleValues(phoenixList, "CAPITAL AMOUNT",           capitalRaiseList, "SALE AMT",              listName, 1.0, "Sub Index", phoenixPair.Key);
                    CompareDoubleValues(phoenixList, "SHARES",                   capitalRaiseList, "SHARES",                listName, 1.0, "Sub Index", phoenixPair.Key);
                    CompareStringValues(phoenixList, "CUSTODIAN ACC NUM",        capitalRaiseList, "CUST. ACCOUNT #",       listName,      "Sub Index", phoenixPair.Key);
                    CompareStringValues(phoenixList, "ORIG BROKER NAME",         capitalRaiseList, "BD LONG NAME",          listName,      "Sub Index", phoenixPair.Key);
                    CompareStringValues(phoenixList, "ORIG REP NAME 1",          capitalRaiseList, "REP",                   listName,      "Sub Index", phoenixPair.Key);

                    CompareStringValues(phoenixList, "DISTRIBUTION OPTION (CHECK, ACH, WIRE)", capitalRaiseList, "DRIP", listName, "Sub Index", phoenixPair.Key);
                }

            }

            foreach (var phoenixPair in PhoenixByBD)
            {
                if (phoenixPair.Key == "HEADER")
                    continue;

                if (!CRDashboardBDs.ContainsKey(phoenixPair.Key))
                    LegendaryConstants.UpdateStatus($"    Cannot Find Orig Broker Name \"{phoenixPair.Key}\" In Capital Raise Dashboard Data, continuing...");
                else
                {
                    phoenixList = phoenixPair.Value;
                    capitalRaiseList = CRDashboardBDs[phoenixPair.Key];
                    string listName = "Capital Raise Dashboard BDs";

                    string phoenixBDAddress1 = phoenixList["ORIG BROKER OFFICE ADDRESS LINE 1"];
                    string phoenixBDAddress2 = phoenixList["ORIG BROKER OFFICE ADDRESS LINE 2"];
                    string phoenixAddress = phoenixBDAddress1 + (phoenixBDAddress2.Length > 3 ? $", {phoenixBDAddress2}" : "");
                    string capitalRaiseBDAddress = capitalRaiseList["ADDRESS"].Replace("Ste.", "Suite").Replace("Ste", "Suite").Replace("#", "Suite");

                    if (string.Compare(phoenixAddress.Trim(), capitalRaiseBDAddress.Trim(), true) != 0)
                        LegendaryConstants.UpdateStatus($"Capital Raise Dashboard BDs ADDRESS '{capitalRaiseBDAddress}' vs '{phoenixAddress}' " +
                                                        $"does not equal Phoenix ORIG BROKER OFFICE ADDRESS LINE 1 + 2 " +
                                                        $"for Broker Name '{phoenixPair.Key}'");

                    string phoenixBDCityStateZip = phoenixList["ORIG BROKER OFFICE CITY ST ZIP"];
                    string capitalRaiseBDCityStateZip = capitalRaiseList["CITY/STATE/ZIP"];
                    if (string.Compare(phoenixBDCityStateZip.Trim(), capitalRaiseBDCityStateZip.Trim(), true) != 0)
                        LegendaryConstants.UpdateStatus($"Capital Raise Dashboard BDs CITY STATE ZIP CODE '{capitalRaiseBDCityStateZip}' vs '{phoenixBDCityStateZip}' " +
                                                        $"does not equal Phoenix ORIG BROKER OFFICE CITY ST ZIP " +
                                                        $"for Broker Name '{phoenixPair.Key}'");

                    CompareStringValues(phoenixList, "BROKER CRD", capitalRaiseList, "CRD", listName, "Broker Name", phoenixPair.Key);

                }
            }

            foreach (var phoenixPair in PhoenixByRep)
            {
                if (phoenixPair.Key == "HEADER")
                    continue;

                if (!CRDashboardReps.ContainsKey(phoenixPair.Key))
                    LegendaryConstants.UpdateStatus($"    Cannot Find Producing Broker Rep \"{phoenixPair.Key}\" In Capital Raise Dashboard Data, continuing...");
                else
                {
                    phoenixList = phoenixPair.Value;
                    capitalRaiseList = CRDashboardReps[phoenixPair.Key];
                    string listName = "Capital Raise Dashboard Reps";

                    string phoenixRepAddress1 = phoenixList["ORIG REP OFFICE ADDRESS LINE 1"];
                    string phoenixRepAddress2 = phoenixList["ORIG REP OFFICE ADDRESS LINE 2"];
                    string phoenixAddress = phoenixRepAddress1 + (phoenixRepAddress2.Length > 3 ? $", {phoenixRepAddress2}" : "");
                    string capitalRaiseRepAddress = capitalRaiseList["ADDRESS"].Replace("Ste.", "Suite").Replace("Ste", "Suite").Replace("#", "Suite");

                    if (string.Compare(phoenixAddress.Trim(), capitalRaiseRepAddress.Trim(), true) != 0)
                        LegendaryConstants.UpdateStatus($"Capital Raise Dashboard Reps ADDRESS '{capitalRaiseRepAddress}' vs '{phoenixAddress}' " +
                                                        $"does not equal Phoenix ORIG REP OFFICE ADDRESS LINE 1 + 2  " +
                                                        $"for Rep Name '{phoenixPair.Key}'");

                    string phoenixRepCityStateZip = phoenixList["ORIG REP OFFICE CITY ST ZIP"];
                    string capitalRaiseRepCityStateZip = capitalRaiseList["CITY/STATE/ZIP"];
                    if (string.Compare(phoenixRepCityStateZip.Trim(), capitalRaiseRepCityStateZip.Trim(), true) != 0)
                        LegendaryConstants.UpdateStatus($"Capital Raise Dashboard Reps CITY STATE ZIP CODE '{capitalRaiseRepCityStateZip}' vs '{phoenixRepCityStateZip}' " +
                                                        $"does not equal Phoenix ORIG REP OFFICE CITY ST ZIP " +
                                                        $"for Rep Name '{phoenixPair.Key}'");

                    CompareStringValues(phoenixList, "REP 1 CRD", capitalRaiseList, "CRD", listName, "Rep Name", phoenixPair.Key);
                    CompareStringValues(phoenixList, "REP 2 CRD", capitalRaiseList, "CRD", listName, "Rep Name", phoenixPair.Key);

                }
            }


            LegendaryConstants.UpdateStatus("Phoenix to Capital Raise Dashboard Comparison is Complete (see above for issues)");

            return true;
        }
    }
}
