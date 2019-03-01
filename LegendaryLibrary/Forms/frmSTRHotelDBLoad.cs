using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LegendaryLibrary.Zoho;

namespace LegendaryLibrary
{
    public partial class frmSTRHotelDBLoad : Form
    {
        private SortedList<string, SortedList<string, string>> STRExisting = null;
        private List<KeyValuePair<string, SortedList<string, string>>> STRFiltered = null;
        private SortedList<string, SortedList<string, string>> ZohoHotels = null;
        private SortedList<string, SortedList<string, string>> ZohoAccounts = null;

        public frmSTRHotelDBLoad()
        {
            InitializeComponent();
        }

        private void CreateHotelImportFilesFromSTR()
        {
            Legendary.UpdateStatus("\r\n-------------------- Start Creating ZOHO Import Files --------------------\r\n");


            string exportDirectory = this.fileOutputDirectory.FullFilePath;
            if (! System.IO.Directory.Exists(exportDirectory))
            {
                Legendary.UpdateStatus($"\r\nOutput Directory {exportDirectory} could not be found!\r\n");
                return;
            }

            if (!System.IO.Directory.Exists(this.fileZohoBackupDirectory.FullFilePath))
            {
                Legendary.UpdateStatus($"\r\nZoho Backup Directory {this.fileZohoBackupDirectory.FullFilePath} could not be found!\r\n");
                return;
            }

            Legendary.UpdateStatus("    STR Excel File 'Existing' Sheet Loading");
            STRExisting = ExcelLoader.LoadExcelFile(fileSTRExcelDB.FullFilePath, "STR Number", "Existing", 1, 1, null, 65000, 104);
            if (STRExisting == null)
            {
                Legendary.UpdateStatus($"\r\nTerminating as Could Not Read Existing sheet from {fileSTRExcelDB.FullFilePath}\r\n");
                return;
            }

            STRFiltered = STRExisting.Where(x => ((string.Compare(x.Value["PARENT COMPANY"], "Hyatt", true) == 0) ||
                                                  (string.Compare(x.Value["PARENT COMPANY"], "Intercontinental Hotels Group", true) == 0) ||
                                                  (string.Compare(x.Value["PARENT COMPANY"], "Hilton Worldwide", true) == 0) ||
                                                  (string.Compare(x.Value["PARENT COMPANY"], "Radisson Hotel Group", true) == 0) ||
                                                  (string.Compare(x.Value["PARENT COMPANY"], "Marriott International", true) == 0)) &&
                                                 ((int.Parse(x.Value["ROOMS"]) > 79) && (int.Parse(x.Value["ROOMS"]) < 226)) &&
                                                 (x.Value["OPEN DATE"].StartsWith("2")))
                                     .ToList() ?? new List<KeyValuePair<string, SortedList<string, string>>>();

            foreach (var pair in STRFiltered)
            {
                var sortedList = pair.Value;

                sortedList["TELEPHONE"] = NormalizePhone(sortedList["TELEPHONE"]);

                var parentCompany = sortedList["PARENT COMPANY"];
                var brandChain = "";
                switch (parentCompany)
                {
                    case "Marriott International": brandChain = "Marriott"; break;
                    case "Radisson Hotel Group": brandChain = "Radisson"; break;
                    case "Intercontinental Hotels Group": brandChain = "IHG"; break;
                    case "Hilton Worldwide": brandChain = "Hilton"; break;
                    case "Hyatt": brandChain = "Hyatt"; break;
                }
                sortedList["PARENT COMPANY"] = brandChain;

                var openDate = sortedList["OPEN DATE"];
                string yearBuilt = openDate;
                switch (openDate.Length)
                {
                    case 5: case 6: case 8: yearBuilt = $@"{openDate.Substring(0, 4)}"; break;
                    default: yearBuilt = openDate; break;
                }
                sortedList["OPEN DATE"] = yearBuilt;

                // Overloading continent to hold the ZOHO field "Project Phase"
                sortedList["CONTINENT"] = "Open";
            }

            Legendary.UpdateStatus("    STR Excel File 'Existing' Sheet IS Loaded");


       //////     CreateHotelImportFiles(this.fileZohoBackupDirectory.FullFilePath, exportDirectory);

            CreateCompanyImportFiles(this.fileZohoBackupDirectory.FullFilePath, exportDirectory);

            Legendary.UpdateStatus("\r\n-------------------- Creation of ZOHO Import Files Is COMPLETE! --------------------\r\n");
        }

        private void CreateCompanyImportFiles(string zohoDataBackupPath, string exportDirectory)
        {
            string zohoOrganizationsFullPath = System.IO.Path.Combine(this.fileZohoBackupDirectory.FullFilePath, "Accounts_001.csv");

            if (!System.IO.File.Exists(zohoOrganizationsFullPath))
            {
                Legendary.UpdateStatus($"\r\nZoho Accounts Data File {zohoOrganizationsFullPath} could not be found!\r\n");
                return;
            }

            Legendary.UpdateStatus("    Zoho Accounts Data File Loading");
            ZohoAccounts = ExcelLoader.LoadExcelFile(zohoOrganizationsFullPath, "Company ID", "Accounts_001", 1, 1, null, 12000, 78);
            if (ZohoAccounts == null)
            {
                Legendary.UpdateStatus($"\r\nFailed to load Zoho Accounts Data File {zohoOrganizationsFullPath}!\r\n");
                return;
            }

            var listOfLists = new SortedList<string, SortedList<string, string>>();

            foreach (var pair in STRFiltered)
            {
                var account = pair.Value;

                if (!string.IsNullOrEmpty(account["OWNER COMPANY"]))
                {
                    var companyName = account["OWNER COMPANY"].Trim();
                    companyName = TrimCompanyName(companyName);
                    var key = companyName.ToUpper();
                    var sortedList = new SortedList<string, string>();
                    sortedList.Add("COMPANYID", "0");
                    sortedList.Add("COMPANYTYPE", "Owner");
                    sortedList.Add("NAME", companyName);
                    sortedList.Add("STREET", account["OWNER ADDRESS 1"]);
                    sortedList.Add("STREET2", account["OWNER ADDRESS 2"]);
                    sortedList.Add("CITY", account["OWNER CITY"]);
                    sortedList.Add("STATE", account["OWNER STATE"]);
                    sortedList.Add("COUNTRY", account["OWNER COUNTRY"]);
                    sortedList.Add("POSTALCODE", account["OWNER POSTAL CODE"]);
                    sortedList.Add("PHONE", account["OWNER PHONE"]);
                    sortedList.Add("EMAIL", account["OWNER EMAIL"]);
                    sortedList.Add("WEBSITE", account["OWNER WEBSITE"]);
                    sortedList.Add("MAGICKEY", account["STR NUMBER"]);
                    if (listOfLists.ContainsKey(key))
                    {
                        var newList = OverwriteBlanksInStringList(listOfLists[key], sortedList);
                        listOfLists[key] = newList;
                        listOfLists[key]["COMPANYTYPE"] = "Owner";  // Owner is higher ranking than management...
                    }
                    else
                        listOfLists.Add(key, sortedList);
                }

                if (!string.IsNullOrEmpty(account["MANAGEMENT COMPANY"]))
                {
                    var companyName = account["MANAGEMENT COMPANY"].Trim();
                    companyName = TrimCompanyName(companyName);
                    var key = companyName.ToUpper();
                    var sortedList = new SortedList<string, string>();
                    sortedList.Add("COMPANYID", "0");
                    sortedList.Add("COMPANYTYPE", "Management");
                    sortedList.Add("NAME", companyName);
                    sortedList.Add("STREET", account["MANAGEMENT ADDRESS 1"]);
                    sortedList.Add("STREET2", account["MANAGEMENT ADDRESS 2"]);
                    sortedList.Add("CITY", account["MANAGEMENT CITY"]);
                    sortedList.Add("STATE", account["MANAGEMENT STATE"]);
                    sortedList.Add("COUNTRY", account["MANAGEMENT COUNTRY"]);
                    sortedList.Add("POSTALCODE", account["MANAGEMENT POSTAL CODE"]);
                    sortedList.Add("PHONE", account["MANAGEMENT PHONE"]);
                    sortedList.Add("EMAIL", account["MANAGEMENT EMAIL"]);
                    sortedList.Add("WEBSITE", account["MANAGEMENT WEBSITE"]);
                    sortedList.Add("MAGICKEY", account["STR NUMBER"]);
                    if (listOfLists.ContainsKey(key))
                    {
                        var newList = OverwriteBlanksInStringList(listOfLists[key], sortedList);
                        listOfLists[key] = newList;
                    }
                    else
                        listOfLists.Add(key, sortedList);
                }

            }

            foreach (var pair in listOfLists)
            {
                var sortedList = pair.Value;
                var companyName = sortedList["NAME"];
                var zohoMatch = ZohoAccounts.FirstOrDefault(x => MatchCompanyNames(x.Value["COMPANY NAME"], companyName));
                if (string.IsNullOrEmpty(zohoMatch.Key))
                    sortedList["COMPANYID"] = "";
                else
                    CompanyCopyZohoToSTR(zohoMatch, sortedList);
                sortedList["PHONE"] = NormalizePhone(sortedList["PHONE"]);
                sortedList["STATE"] = Legendary.GetStateByName(sortedList["STATE"]);
            }

            SortedList<string, string> columnsToExport = new SortedList<string, string>();
            columnsToExport.Add("COMPANYID", "Company ID");
            columnsToExport.Add("COMPANYTYPE", "Company Type");
            columnsToExport.Add("NAME", "Company Name");
            columnsToExport.Add("STREET", "Street");
            columnsToExport.Add("STREET2", "Street 2");
            columnsToExport.Add("CITY", "City");
            columnsToExport.Add("STATE", "State");
            columnsToExport.Add("COUNTRY", "Country");
            columnsToExport.Add("POSTALCODE","Postal Code");
            columnsToExport.Add("PHONE", "Phone");
            columnsToExport.Add("EMAIL", "Email");
            columnsToExport.Add("WEBSITE", "Website");
            columnsToExport.Add("MAGICKEY", "Magic Key");

            Legendary.UpdateStatus("\r\n    Saving the individual Company Excel files to output directory...");
            string baseFileName = $"{this.txtBaseFilename.Text}-Companies-";

            for (int i = 0; i < listOfLists.Count - 2; i++)
            {
                var firstList = listOfLists.Values[i];
                var secondList = listOfLists.Values[i + 1];
                if (string.Compare(firstList["COMPANYID"], secondList["COMPANYID"], true) == 0)
                {
                    string firstListCompanyType = firstList["COMPANYTYPE"];
                    string secondListCompanyType = secondList["COMPANYTYPE"];
                    if ((firstListCompanyType == "Management") && ((secondListCompanyType == "Owner") || (secondListCompanyType == "Developer")))
                        firstList["COMPANYTYPE"] = secondListCompanyType;
                    if ((secondListCompanyType == "Management") && ((firstListCompanyType == "Owner") || (firstListCompanyType == "Developer")))
                        secondList["COMPANYTYPE"] = firstListCompanyType;
                }
            }
            var distinctList = listOfLists.Distinct(new DistinctCompanyComparer()).ToList();
            ExcelReporter.OutputListOfListsOfKeyValuesToNewWorkbook(distinctList, columnsToExport, exportDirectory, baseFileName, 5000);
            Legendary.UpdateStatus("    Individual Company Excel files ARE SAVED to output directory");

        }

        public class DistinctCompanyComparer : IEqualityComparer<KeyValuePair<string, SortedList<string, string>>>
        {
            static long count = 0;
            public bool Equals(KeyValuePair<string, SortedList<string, string>> x, KeyValuePair<string, SortedList<string, string>> y)
            {
                return (x.Value["COMPANYID"] ?? "12345") == (y.Value["COMPANYID"] ?? "67890");
            }

            public int GetHashCode(KeyValuePair<string, SortedList<string, string>> x)
            {
                count++;
                return (string.IsNullOrWhiteSpace(x.Value["COMPANYID"]) ? $"junk{count}".GetHashCode() : x.Value["COMPANYID"].GetHashCode());
            }
        }

        private string NormalizePhone(string phone)
        {
            string newPhone = phone.Replace("(","").Replace(")","").Replace("-","").Replace(" ","");
            switch (phone.Length)
            {
                case 7: newPhone = $@"{phone.Substring(0, 3)}-{phone.Substring(3, 4)}"; break;
                case 10: newPhone = $@"({phone.Substring(0, 3)}) {phone.Substring(3, 3)}-{phone.Substring(6, 4)}"; break;
                case 11: newPhone = $@"({phone.Substring(1, 3)}) {phone.Substring(4, 3)}-{phone.Substring(7, 4)}"; break;
                default: newPhone = phone; break;
            }
            return newPhone;
        }

        private bool MatchCompanyNames(string name1, string name2)
        {
            bool result = false;
            if (string.Compare(MatchableCoName(name1), MatchableCoName(name2), true) == 0)
                result = true;
            return result;
        }

        private void CompanyCopyZohoToSTR(KeyValuePair<string, SortedList<string, string>> zohoCompany, SortedList<string,string> company)
        {
            company["COMPANYID"] = zohoCompany.Value["COMPANY ID"];
            CopyOneCompanyFieldIfNotEmpty(zohoCompany, "COMPANY TYPE", company, "COMPANYTYPE");
            CopyOneCompanyFieldIfNotEmpty(zohoCompany, "COMPANY NAME", company, "NAME");

            bool forceCopy = false;

            if (string.IsNullOrWhiteSpace(zohoCompany.Value["STREET"]) ||
                string.IsNullOrWhiteSpace(zohoCompany.Value["CITY"]) ||
                string.IsNullOrWhiteSpace(zohoCompany.Value["STATE"]) ||
                string.IsNullOrWhiteSpace(zohoCompany.Value["COUNTRY"]) ||
                string.IsNullOrWhiteSpace(zohoCompany.Value["ZIP CODE"]))
                forceCopy = true;

            if (forceCopy && (string.IsNullOrWhiteSpace(company["STREET"]) ||
                              string.IsNullOrWhiteSpace(company["CITY"]) ||
                              string.IsNullOrWhiteSpace(company["STATE"]) ||
                              string.IsNullOrWhiteSpace(company["COUNTRY"]) ||
                              string.IsNullOrWhiteSpace(company["POSTALCODE"])) )
                forceCopy = false;

            CopyOneCompanyFieldIfNotEmpty(zohoCompany, "STREET", company, "STREET", forceCopy);
            CopyOneCompanyFieldIfNotEmpty(zohoCompany, "STREET 2", company, "STREET2", forceCopy);
            CopyOneCompanyFieldIfNotEmpty(zohoCompany, "CITY", company, "CITY", forceCopy);
            CopyOneCompanyFieldIfNotEmpty(zohoCompany, "STATE", company, "STATE", forceCopy);
            CopyOneCompanyFieldIfNotEmpty(zohoCompany, "COUNTRY", company, "COUNTRY", forceCopy);
            CopyOneCompanyFieldIfNotEmpty(zohoCompany, "ZIP CODE", company, "POSTALCODE", forceCopy);

            CopyOneCompanyFieldIfNotEmpty(zohoCompany, "PHONE", company, "PHONE");
            company["PHONE"] = NormalizePhone(company["PHONE"]);

            CopyOneCompanyFieldIfNotEmpty(zohoCompany, "EMAIL", company, "EMAIL");
            CopyOneCompanyFieldIfNotEmpty(zohoCompany, "WEBSITE", company, "WEBSITE");

            //CopyOneCompanyFieldIfNotEmpty(zohoCompany, "MAGIC KEY", company, "MAGICKEY");
        }

        private void CopyOneCompanyFieldIfNotEmpty(KeyValuePair<string, SortedList<string, string>> zohoCompany, string zohoField,
                                                   SortedList<string, string> company, string field, bool forceKeepCompany = false)
        {
            if (string.IsNullOrWhiteSpace(zohoCompany.Value[zohoField]))
                return;

            string zohoName = zohoCompany.Value["COMPANY NAME"];
            string name = company["NAME"];

            if (string.IsNullOrWhiteSpace(company[field]))
            {
                company[field] = zohoCompany.Value[zohoField];
                string update = $"ZOHO~{zohoName}~{zohoField}~{zohoCompany.Value[zohoField]}~is being kept because~" +
                                $"STR~{name}~{field}~ ~is empty.";
                Legendary.UpdateStatus(update);
            }
            else
            {
                if ((string.Compare(zohoCompany.Value[zohoField], company[field], true) != 0) && (! forceKeepCompany))
                {
                    company[field] = zohoCompany.Value[zohoField];
                    string update = $"ZOHO~{zohoName}~{zohoField}~{zohoCompany.Value[zohoField]}~is different from~" +
                                    $"STR~{name}~{field}~{company[field]}~but not changing because default is to keep Zoho value.";
                    Legendary.UpdateStatus(update);
                }
                else if (forceKeepCompany)
                {
                    string update = $"ZOHO~{zohoName}~{zohoField}~{zohoCompany.Value[zohoField]}~is different from~" +
                                    $"STR~{name}~{field}~{company[field]}~but are changing because part of Zoho address is missing.";
                    Legendary.UpdateStatus(update);
                }
            }
        }

        private SortedList<string, string> OverwriteBlanksInStringList(SortedList<string, string> keepList, SortedList<string, string> newList)
        {
            var returnList = new SortedList<string, string>();

            foreach (var key in keepList.Keys)
                if (newList.ContainsKey(key))
                    if (string.IsNullOrWhiteSpace(keepList[key]) && (!string.IsNullOrWhiteSpace(newList[key])))
                        returnList.Add(key, newList[key]);
                    else
                        returnList.Add(key, keepList[key]);
                else
                    returnList.Add(key, keepList[key]);

            return returnList;
        }

        private string TrimCompanyName(string companyName)
        {
            var newCoName = companyName.Trim().ToUpper();
            if      (newCoName.EndsWith(" OW"))    newCoName = companyName.Trim().Substring(0, companyName.Trim().Length - 3);
            else if (newCoName.EndsWith(" MG"))    newCoName = companyName.Trim().Substring(0, companyName.Trim().Length - 3);
            else if (newCoName.EndsWith(" OWNER")) newCoName = companyName.Trim().Substring(0, companyName.Trim().Length - 6);
            else if (newCoName.EndsWith(" MGTCO")) newCoName = companyName.Trim().Substring(0, companyName.Trim().Length - 6);
            else if (newCoName.EndsWith(" OWNE"))  newCoName = companyName.Trim().Substring(0, companyName.Trim().Length - 5);
            else if (newCoName.EndsWith(" OWN"))   newCoName = companyName.Trim().Substring(0, companyName.Trim().Length - 4);
            else if (newCoName.EndsWith(" OWNCO")) newCoName = companyName.Trim().Substring(0, companyName.Trim().Length - 6);
            else if (newCoName.EndsWith(" O"))     newCoName = companyName.Trim().Substring(0, companyName.Trim().Length - 2);
            else if (newCoName.EndsWith(" M"))     newCoName = companyName.Trim().Substring(0, companyName.Trim().Length - 2);
            else if (newCoName.EndsWith(" MGMT"))  newCoName = companyName.Trim().Substring(0, companyName.Trim().Length - 5);
            else if (newCoName.EndsWith(" MGT"))   newCoName = companyName.Trim().Substring(0, companyName.Trim().Length - 4);
            else if (newCoName.EndsWith(" MGTC"))  newCoName = companyName.Trim().Substring(0, companyName.Trim().Length - 5);
            else                                   newCoName = companyName.Trim();
            newCoName = newCoName.TrimEnd();
            return newCoName;
        }

        private string MatchableCoName(string companyName)
        {
            string result = NormalizeCompanyNameForMatch(companyName);
            result = NormalizeCompanyNameForMatch(result);
            return result;
        }

        private string NormalizeCompanyNameForMatch(string companyName)
        {
            var newCoName = companyName.Trim().ToUpper();
            if      (newCoName.EndsWith(" GRP")) newCoName = newCoName.Substring(0, newCoName.Length - 4);
            else if (newCoName.EndsWith(" GROUP")) newCoName = newCoName.Substring(0, newCoName.Length - 6);
            else if (newCoName.EndsWith(", GROUP")) newCoName = newCoName.Substring(0, newCoName.Length - 7);
            else if (newCoName.EndsWith(", CO")) newCoName = newCoName.Substring(0, newCoName.Length - 4);
            else if (newCoName.EndsWith(" CO")) newCoName = newCoName.Substring(0, newCoName.Length - 3);
            else if (newCoName.EndsWith(" COMPANY")) newCoName = newCoName.Substring(0, newCoName.Length - 8);
            else if (newCoName.EndsWith(", COMPANY")) newCoName = newCoName.Substring(0, newCoName.Length - 9);
            else if (newCoName.EndsWith(", LLC.")) newCoName = newCoName.Substring(0, newCoName.Length - 6);
            else if (newCoName.EndsWith(", LLC")) newCoName = newCoName.Substring(0, newCoName.Length - 5);
            else if (newCoName.EndsWith(" LLC")) newCoName = newCoName.Substring(0, newCoName.Length - 4);
            else if (newCoName.EndsWith(" LLC.")) newCoName = newCoName.Substring(0, newCoName.Length - 5);
            else if (newCoName.EndsWith(", INC.")) newCoName = newCoName.Substring(0, newCoName.Length - 6);
            else if (newCoName.EndsWith(", INC")) newCoName = newCoName.Substring(0, newCoName.Length - 5);
            else if (newCoName.EndsWith(" INC.")) newCoName = newCoName.Substring(0, newCoName.Length - 5);
            else if (newCoName.EndsWith(" INC")) newCoName = newCoName.Substring(0, newCoName.Length - 4);
            else if (newCoName.EndsWith(" L.P.")) newCoName = newCoName.Substring(0, newCoName.Length - 5);
            else if (newCoName.EndsWith(", L.P.")) newCoName = newCoName.Substring(0, newCoName.Length - 6);
            else if (newCoName.EndsWith(", LP")) newCoName = newCoName.Substring(0, newCoName.Length - 4);
            else if (newCoName.EndsWith(" LP")) newCoName = newCoName.Substring(0, newCoName.Length - 3);
            else if (newCoName.EndsWith(" CORPORATION")) newCoName = newCoName.Substring(0, newCoName.Length - 12);
            else if (newCoName.EndsWith(", CORPORATION")) newCoName = newCoName.Substring(0, newCoName.Length - 13);
            else if (newCoName.EndsWith(", CORP")) newCoName = newCoName.Substring(0, newCoName.Length - 6);
            else if (newCoName.EndsWith(" CORP")) newCoName = newCoName.Substring(0, newCoName.Length - 5);
            return newCoName;
        }

        private string NormalizeCompanyName(string companyName)
        {
            var newCoName = companyName.Trim().ToUpper();
            if      (newCoName.EndsWith(" GRP")) newCoName = newCoName.Substring(0, newCoName.Length - 4) + " GROUP";
            else if (newCoName.EndsWith(", GROUP")) newCoName = newCoName.Substring(0, newCoName.Length - 7) + " GROUP";
            else if (newCoName.EndsWith(", CO")) newCoName = newCoName.Substring(0, newCoName.Length - 4) + " COMPANY";
            else if (newCoName.EndsWith(" CO")) newCoName = newCoName.Substring(0, newCoName.Length - 3) + " COMPANY";
            else if (newCoName.EndsWith(", COMPANY")) newCoName = newCoName.Substring(0, newCoName.Length - 9) + " COMPANY";
            else if (newCoName.EndsWith(", LLC.")) newCoName = newCoName.Substring(0, newCoName.Length - 6) + ", LLC";
            else if (newCoName.EndsWith(" LLC")) newCoName = newCoName.Substring(0, newCoName.Length - 4) + ", LLC";
            else if (newCoName.EndsWith(" LLC.")) newCoName = newCoName.Substring(0, newCoName.Length - 5) + ", LLC";
            else if (newCoName.EndsWith(", INC.")) newCoName = newCoName.Substring(0, newCoName.Length - 6) + ", INC";
            else if (newCoName.EndsWith(" INC.")) newCoName = newCoName.Substring(0, newCoName.Length - 5) + ", INC";
            else if (newCoName.EndsWith(" INC")) newCoName = newCoName.Substring(0, newCoName.Length - 5) + ", INC";
            else if (newCoName.EndsWith(", L.P.")) newCoName = newCoName.Substring(0, newCoName.Length - 6) + " L.P.";
            else if (newCoName.EndsWith(", LP")) newCoName = newCoName.Substring(0, newCoName.Length - 4) + " L.P.";
            else if (newCoName.EndsWith(" LP")) newCoName = newCoName.Substring(0, newCoName.Length - 3) + " L.P.";
            else if (newCoName.EndsWith(", CORPORATION")) newCoName = newCoName.Substring(0, newCoName.Length - 13) + " CORPORATION";
            else if (newCoName.EndsWith(", CORP")) newCoName = newCoName.Substring(0, newCoName.Length - 6) + " CORPORATION";
            else if (newCoName.EndsWith(" CORP")) newCoName = newCoName.Substring(0, newCoName.Length - 5) + " CORPORATION";
            return newCoName;
        }

        private void CreateHotelImportFiles(string zohoDataBackupPath, string exportDirectory)
        {
            string zohoHotelsFullPath = System.IO.Path.Combine(this.fileZohoBackupDirectory.FullFilePath, "Hotels_C_001.csv");

            if (!System.IO.File.Exists(zohoHotelsFullPath))
            {
                Legendary.UpdateStatus($"\r\nZoho Hotels Data File {zohoHotelsFullPath} could not be found!\r\n");
                return;
            }

            Legendary.UpdateStatus("    Zoho Hotel Data File Loading");
            ZohoHotels = ExcelLoader.LoadExcelFile(zohoHotelsFullPath, "Hotel ID", "Hotels_C_001", 1, 1, null, 30000, 78);
            if (ZohoHotels == null)
            {
                Legendary.UpdateStatus($"\r\nFailed to load Zoho Hotel Data File {zohoHotelsFullPath}!\r\n");
                return;
            }

            var zohoHotelsWithSTRNumbers = ZohoHotels.Where(x => (!string.IsNullOrWhiteSpace(x.Value["STR NUMBER"]))).ToList();
            Legendary.UpdateStatus("    Zoho Hotel Data File  IS Loaded");

            Legendary.UpdateStatus("    Combine STR with ZOHO based on STR Number...");
            foreach (var pair in STRFiltered)
            {
                var sortedList = pair.Value;
                // Overloading sub continent to hold the ZOHO field "Hotel ID"
                var strNumber = sortedList["STR NUMBER"];
                var zohoMatch = zohoHotelsWithSTRNumbers.FirstOrDefault(x => x.Value["STR NUMBER"] == strNumber);
                if (string.IsNullOrEmpty(zohoMatch.Key))
                    sortedList["SUB-CONTINENT"] = "0";
                else
                    sortedList["SUB-CONTINENT"] = zohoMatch.Value["HOTEL ID"];
            }
            Legendary.UpdateStatus("    Combining STR with ZOHO based on STR Number is COMPLETED");

            SortedList<string, string> columnsToExport = new SortedList<string, string>();
            columnsToExport.Add("STR Number", "STR Number");
            columnsToExport.Add("Sub-Continent", "Hotel ID");
            columnsToExport.Add("Hotel Name", "Hotel Name");
            columnsToExport.Add("Open Date", "Year Built");
            columnsToExport.Add("Address 1", "Street");
            columnsToExport.Add("City", "City");
            columnsToExport.Add("State", "State");
            columnsToExport.Add("Postal Code", "Postal Code");
            columnsToExport.Add("Rooms", "Number of Rooms");
            columnsToExport.Add("Class", "Hotel Class");
            columnsToExport.Add("Affiliation", "Brand");
            columnsToExport.Add("Telephone", "Hotel Phone Number");
            columnsToExport.Add("MSA", "MSA");
            columnsToExport.Add("Parent Company", "Brand Chain");
            columnsToExport.Add("Latitude", "Latitude");
            columnsToExport.Add("Longitude", "Longitude");
            columnsToExport.Add("Continent", "Project Phase");

            Legendary.UpdateStatus("\r\n    Saving the individual Hotel Excel files to output directory...");
            string baseFileName = $"{this.txtBaseFilename.Text}-Hotels-";
            ExcelReporter.OutputListOfListsOfKeyValuesToNewWorkbook(STRFiltered, columnsToExport, exportDirectory, baseFileName, 5000);
            Legendary.UpdateStatus("    Individual Hotel Excel files ARE SAVED to output directory");

        }
        private void frmSTRHotelDBLoad_Load(object sender, EventArgs e)
        {
            var keyValues = LegendaryKeyValue.LoadKeyValuesIntoSortedList();

            if (keyValues.ContainsKey("STRDBLoad-LastFile"))
                this.fileSTRExcelDB.FullFilePath = keyValues["STRDBLoad-LastFile"].Value;

            if (keyValues.ContainsKey("STRDBLoad-LastOutputDirectory"))
                this.fileOutputDirectory.FullFilePath = keyValues["STRDBLoad-LastOutputDirectory"].Value;

            if (keyValues.ContainsKey("STRDBLoad-LastBaseFileName"))
                this.txtBaseFilename.Text = keyValues["STRDBLoad-LastBaseFileName"].Value;

            if (keyValues.ContainsKey("STRDBLoad-ZohoBackupDirectory"))
                this.fileZohoBackupDirectory.FullFilePath = keyValues["STRDBLoad-ZohoBackupDirectory"].Value;

            Legendary.StatusTextBox = this.txtStatus;
        }

        private void SaveKeyValues()
        {
            var keyValue = new LegendaryKeyValue("STRDBLoad-LastFile", this.fileSTRExcelDB.FullFilePath);
            LegendaryKeyValue.InsertOrUpdateOneKeyValue(keyValue);

            keyValue = new LegendaryKeyValue("STRDBLoad-LastOutputDirectory", this.fileOutputDirectory.FullFilePath);
            LegendaryKeyValue.InsertOrUpdateOneKeyValue(keyValue);

            keyValue = new LegendaryKeyValue("STRDBLoad-LastBaseFileName", this.txtBaseFilename.Text);
            LegendaryKeyValue.InsertOrUpdateOneKeyValue(keyValue);

            keyValue = new LegendaryKeyValue("STRDBLoad-ZohoBackupDirectory", this.fileZohoBackupDirectory.FullFilePath);
            LegendaryKeyValue.InsertOrUpdateOneKeyValue(keyValue);
        }

        private void btnCreateZOHOImportFile_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                CreateHotelImportFilesFromSTR();
                SaveKeyValues();
            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
    }
}
