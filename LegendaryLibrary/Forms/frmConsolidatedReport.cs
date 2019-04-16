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
    public partial class frmConsolidatedReport : Form
    {
        public frmConsolidatedReport()
        {
            InitializeComponent();
        }

        private List<EntityData> EntityDataMaster = new List<EntityData>();
        private List<LedgerAccount> LedgerAccountMaster = new List<LedgerAccount>();
        private List<ConsolidatedAccount> ConsolidatedAccountMaster = new List<ConsolidatedAccount>();

        private void frmConsolidatedDataUpload_Load(object sender, EventArgs e)
        {
            try
            {
                Legendary.StatusTextBox = this.txtStatus;
                EntityDataMaster = EntityData.GetAllEntityData();
                LedgerAccountMaster = LedgerAccount.GetAllLedgerAccounts();
                ConsolidatedAccountMaster = ConsolidatedAccount.GetAllConsolidatedAccounts();

                var keyValues = LegendaryKeyValue.LoadKeyValuesIntoSortedList();

                if (keyValues.ContainsKey("ConsolidatedDataUpload-LastTrialBalanceDate"))
                {
                    var trialBalanceDate = DateTime.Today;
                    string dateString = keyValues["ConsolidatedDataUpload-LastTrialBalanceDate"].Value;
                    DateTime.TryParse(dateString, out trialBalanceDate);
                    this.dateTrialBalanceDate.Value = trialBalanceDate;

                    int lastSelectedIndex = 0;
                    string intString = keyValues["ConsolidatedDataUpload-LastFundSelectedIndex"].Value;
                    int.TryParse(intString, out lastSelectedIndex);
                    this.cbFund.SelectedIndex = lastSelectedIndex;

                    if (keyValues.ContainsKey("ConsolidatedDataUpload-LastInputDirectory"))
                        this.fileInputDirectory.FullFilePath = keyValues["ConsolidatedDataUpload-LastInputDirectory"].Value;

                    if (keyValues.ContainsKey("ConsolidatedDataUpload-LastReportDirectory"))
                        this.fileReportDirectory.FullFilePath = keyValues["ConsolidatedDataUpload-LastReportDirectory"].Value;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.ToString()}\n\n{ex.InnerException?.ToString()}\n\n{ex.InnerException?.InnerException?.ToString()}",
                                $"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGetFileList_Click(object sender, EventArgs e)
        {
            Legendary.StatusSuspendStatusBarUpdating = true;
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                var fileList = ConsolidatedInputFile.GetInputFileListFromDate(this.fileInputDirectory.FullFilePath, this.dateTrialBalanceDate.Value);

                this.chkLstBoxFiles.Items.Clear();

                foreach (var file in fileList.Values)
                    this.chkLstBoxFiles.Items.Add(file, true);

                Legendary.UpdateStatus($"---------------------------------  Get File List Completed  ---------------------------------\r\n");
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Legendary.UpdateStatus(ex);
            }
            finally
            {
                Legendary.StatusSuspendStatusBarUpdating = false;
            }
        }

        private void btnLoadToDB_Click(object sender, EventArgs e)
        {
            Legendary.StatusSuspendStatusBarUpdating = true;
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                Legendary.UpdateStatus($"---------------------------------  Start Loading Files To DB  ---------------------------------");

                SortedList<string, SortedList<string, string>> rowData = null;

                foreach (var fileObject in this.chkLstBoxFiles.CheckedItems)
                {
                    ConsolidatedInputFile inputFile = (fileObject as ConsolidatedInputFile);

                    Legendary.UpdateStatus($"\r\nLoading file:   {inputFile.FileName}");

                    if (inputFile.FileName.Contains("TB") && inputFile.FileName.Contains("LOF REIT"))
                    {
                        Legendary.UpdateStatus($"\r\nLOF REIT workbook processing...");
                        rowData = null;
                        string[] extraCellValues = { "C1" };
                        rowData = ExcelLoader.LoadExcelFile(inputFile.FullPath, "Account", "TB", 2, 2, extraCellValues);
                        ProcessQuickBooksData("LOF REIT", EntityDataMaster, this.dateTrialBalanceDate.Value, extraCellValues, rowData);
                    }
                    else if (inputFile.FileName.Contains("TB") && inputFile.FileName.Contains("LOF II OP"))
                    {
                        Legendary.UpdateStatus($"\r\nLOF II OP workbook processing...");
                        rowData = null;
                        string[] extraCellValues2 = { "C1" };
                        rowData = ExcelLoader.LoadExcelFile(inputFile.FullPath, "Account", "TB", 2, 2, extraCellValues2);
                        ProcessQuickBooksData("LOF II", EntityDataMaster, this.dateTrialBalanceDate.Value, extraCellValues2, rowData);
                    }
                    else if (inputFile.FileName.Contains("TB") && inputFile.FileName.Contains("LOF Inc"))
                    {
                        Legendary.UpdateStatus($"\r\nLOF INC Workbook processing...");
                        rowData = null;
                        string[] extraCellValues3 = { "C1" };
                        rowData = ExcelLoader.LoadExcelFile(inputFile.FullPath, "Account", "TB", 2, 2, extraCellValues3);
                        ProcessQuickBooksData("LOF INC", EntityDataMaster, this.dateTrialBalanceDate.Value, extraCellValues3, rowData);
                    }
                    else 
                    {
                        rowData = null;
                        string[] extraCellValues = { "A1", "A3", "D3" };
                        rowData = ExcelLoader.LoadExcelFile(inputFile.FullPath, "Account #", "", 5, 1, extraCellValues, 5000, 52, 4);
                        ProcessM3Data(EntityDataMaster, this.dateTrialBalanceDate.Value, extraCellValues, rowData);
                    }
                }

                Legendary.UpdateStatus($"\r\n---------------------------------  Finished Loading of Files To DB  ---------------------------------");
            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                Legendary.StatusSuspendStatusBarUpdating = false;
            }
        }

        private void ProcessM3Data(List<EntityData> entityData, DateTime reportDate, string[] extraCellValues, SortedList<string, SortedList<string, string>> rowData )
        {
            try
            {
                bool isTRS = false;
                string entityValue = extraCellValues[0]?.ToUpper();
                string dateString = reportDate.ToString("MM/dd/yyy");

                if (entityValue.Contains("TRS"))
                    isTRS = true;

                EntityData foundEntity = null;
                foreach (var entity in entityData)
                    if (entityValue.Contains(entity.Trial_Balance_Name.ToUpper()) && 
                        (((string.Compare(entity.Entity_Type, "TRS", true) == 0) && isTRS) || ((string.Compare(entity.Entity_Type, "LLC", true) == 0) && (!isTRS))))
                    {
                        foundEntity = entity;
                        break;
                    }

                if (foundEntity == null)
                {
                    Legendary.UpdateStatus($"Could not find match for entity description '{entityValue}' in entity database.");
                    return;
                }

                if (!extraCellValues[1].Replace(" ", "").Contains(dateString))
                {
                    Legendary.UpdateStatus($"Trial Balance Date not found in \"Period from\" '{extraCellValues[1]}' date string of file.");
                    return;
                }

                if (!extraCellValues[2].Replace(" ", "").Contains(dateString))
                {
                    Legendary.UpdateStatus($"Trial Balance Date not found in \"Year from\" '{extraCellValues[2]}' date string of file.");
                    return;
                }

                var accountDataToWriteToDB = new List<AccountData>();

                foreach (var pair in rowData)
                {
                    if (pair.Key == "HEADER")
                        continue;

                    var oneRow = pair.Value;

                    string accountNumber = oneRow["ACCOUNT #"];
                    string accountName = oneRow["ACCOUNT NAME"];
                    accountName = accountName.Replace("'", "");
                    string yearToDateValueString = oneRow["YEAR TO DATE"];
                    decimal decimalValue = 0;

                    if (string.IsNullOrWhiteSpace(accountNumber) || accountNumber.ToUpper().StartsWith($"GRAND TOTAL") ||
                        accountNumber.ToUpper().StartsWith($"RUN ON:") || accountNumber.ToUpper().StartsWith($"PERIOD FROM") ||
                        accountNumber.ToUpper().StartsWith($"TRIAL BALANCE") || accountNumber.ToUpper().StartsWith(entityValue) ||
                        accountNumber.ToUpper().StartsWith($"HIDE ZERO") || accountNumber.ToUpper().StartsWith($"ACCOUNT #"))
                        continue;

                    var ledgerAccount = LedgerAccount.GetLedgerAccountByAccountNumberAndEntityTypeAndName(accountNumber, foundEntity.Entity_Type, accountName, LedgerAccountMaster);

                    if (ledgerAccount == null)
                    {
                        Legendary.UpdateStatus($"Could not find ledger account for account number '{accountNumber}' with account name '{accountName}'.  Skipping row...");
                        continue;
                    }

                    bool parseStatus = decimal.TryParse(yearToDateValueString, out decimalValue);
                    if (!parseStatus)
                    {
                        Legendary.UpdateStatus($"Could parse \"Year To Date\" column value for  '{accountNumber}' with account name '{accountName}'.  Skipping row...");
                        continue;
                    }

                    var consolidatedAccount = ConsolidatedAccount.GetConsolidatedAccountById(ledgerAccount.Consolidated_Account_Id, ConsolidatedAccountMaster);
                    if ((string.Compare(consolidatedAccount.Category, Legendary.ConsolidatedAccountCategories.Expense) == 0) ||
                        (string.Compare(consolidatedAccount.Category, Legendary.ConsolidatedAccountCategories.Revenue) == 0) ||
                        (string.Compare(consolidatedAccount.Category, Legendary.ConsolidatedAccountCategories.Other) == 0))
                        decimalValue *= -1;

                    var accountData = new AccountData(0, ledgerAccount.Ledger_Account_Id, foundEntity.Entity_Id, reportDate, decimalValue);
                    accountDataToWriteToDB.Add(accountData);
                }

                AccountData.DeleteByEntityIdValueDate(foundEntity.Entity_Id, reportDate);
                AccountData.InsertAccountData(accountDataToWriteToDB);
            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
        }

        private void ProcessQuickBooksData(string entityName, List<EntityData> entityData, DateTime reportDate, string[] extraCellValues, SortedList<string, SortedList<string, string>> rowData)
        {
            try
            {
                string entityValue = extraCellValues[0].ToUpper().Trim();
                string dateString = reportDate.ToString("MMMdd,yy").ToUpper();

                EntityData foundEntity = null;
                foreach (var entity in entityData)
                    if (string.Compare(entity.Name, entityName, true) == 0)
                    {
                        foundEntity = entity;
                        break;
                    }

                if (foundEntity == null)
                {
                    Legendary.UpdateStatus($"Could not find match for entity '{entityName}' in entity database.");
                    return;
                }

                if (!extraCellValues[0].ToUpper().Replace(" ", "").Contains(dateString))
                {
                    Legendary.UpdateStatus($"Trial Balance Date not found in '{extraCellValues[0]}' date string of file.");
                    return;
                }

                var accountDataToWriteToDB = new List<AccountData>();

                foreach (var pair in rowData)
                {
                    if (pair.Key == "HEADER")
                        continue;

                    var oneRow = pair.Value;

                    string accountName = oneRow["ACCOUNT"];
                    string debitValueString = oneRow["DEBIT"];
                    string creditValueString = oneRow["CREDIT"];
                    decimal debitValue = decimal.MinValue;
                    decimal creditValue = decimal.MinValue;
                    decimal decimalValue = decimal.MinValue;

                    var ledgerAccount = LedgerAccount.GetLedgerAccountByAccountName(accountName.Replace("'",""), LedgerAccountMaster);

                    if (ledgerAccount == null)
                    {
                        Legendary.UpdateStatus($"Could not find ledger account for account name '{accountName}'.  Skipping row...");
                        continue;
                    }

                    bool parseStatus = decimal.TryParse(debitValueString, out debitValue);
                    if (!parseStatus)
                        debitValue = decimal.MinValue;

                    parseStatus = decimal.TryParse(creditValueString, out creditValue);
                    if (!parseStatus)
                        creditValue = decimal.MinValue;

                    if ((debitValue == decimal.MinValue) && (creditValue == decimal.MinValue))
                    {
                        Legendary.UpdateStatus($"Could not parse debit or credit value for account name '{accountName}'.  Skipping row...");
                        continue;
                    }

                    if ((debitValue != decimal.MinValue) && (creditValue != decimal.MinValue))
                    {
                        Legendary.UpdateStatus($"Debit or credit value for account name '{accountName}' both have a value.  Skipping row...");
                        continue;
                    }

                    if (debitValue == decimal.MinValue)
                        decimalValue = creditValue * -1;
                    else
                        decimalValue = debitValue;

                    var consolidatedAccount = ConsolidatedAccount.GetConsolidatedAccountById(ledgerAccount.Consolidated_Account_Id, ConsolidatedAccountMaster);
                    if ((string.Compare(consolidatedAccount.Category, Legendary.ConsolidatedAccountCategories.Expense) == 0) ||
                        (string.Compare(consolidatedAccount.Category, Legendary.ConsolidatedAccountCategories.Revenue) == 0) ||
                        (string.Compare(consolidatedAccount.Category, Legendary.ConsolidatedAccountCategories.Other) == 0))
                        decimalValue *= -1;

                    var accountData = new AccountData(0, ledgerAccount.Ledger_Account_Id, foundEntity.Entity_Id, reportDate, decimalValue);
                    accountDataToWriteToDB.Add(accountData);
                }

                AccountData.DeleteByEntityIdValueDate(foundEntity.Entity_Id, reportDate);
                AccountData.InsertAccountData(accountDataToWriteToDB);
            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
        }


        private void btnCreateReport_Click(object sender, EventArgs e)
        {
            Legendary.StatusSuspendStatusBarUpdating = true;
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                var reportDate = this.dateTrialBalanceDate.Value;

                Legendary.UpdateStatus($"");
                Legendary.UpdateStatus($"");
                Legendary.UpdateStatus($"---------- Creating Consolidated Report Excel Workbook for {reportDate.ToLongDateString()} ----------\r\n");

                Legendary.UpdateStatus($"Reading in the account and entity static data from the SQL Server database...");

                ConsolidatedReport MainConsolidatedReport = null;
                List<AccountData> AccountDataMaster = AccountData.LoadAcccountDataByDate(reportDate);
                List<EntityData> EntityDataMaster = EntityData.GetAllEntityData();
                List<ConsolidatedAccount> ConsolidatedAccountMaster = ConsolidatedAccount.GetAllConsolidatedAccounts();
                List<LedgerAccount> LedgerAccountMaster = LedgerAccount.GetAllLedgerAccounts();

                LedgerAccount incomeLossForPeriodLedgerAccount = null;
                foreach (var ledgerAccount in LedgerAccountMaster)
                    if (string.Compare(ledgerAccount.Number, "2990999.000") == 0)
                        incomeLossForPeriodLedgerAccount = ledgerAccount;

                string fundFullName = "Lodging Opportunity Fund, REIT";
                string fund = "LOF REIT";
                string consolidatedType = "IS";
                string reportTitle = $"CONSOLIDATING STATEMENT OF OPERATIONS";

                ConsolidatedReportType reportType = ConsolidatedReportType.IncomeStatement;
                var reportConsolidatedAccounts = ConsolidatedAccount.GetByConsolidatedType(consolidatedType, ConsolidatedAccountMaster);
                var reportEntities = ConsolidatedReport.GetConsolidatedReportEntities(reportDate, consolidatedType, fund, EntityDataMaster,
                                                                                      reportConsolidatedAccounts, LedgerAccountMaster, AccountDataMaster);
                MainConsolidatedReport = new ConsolidatedReport(fundFullName, reportTitle, reportDate, reportType, reportConsolidatedAccounts, reportEntities);
                ConsolidatedReport.WriteConsolidatedSumToAccountData(incomeLossForPeriodLedgerAccount, reportDate, reportEntities, true);

                string fullPath = $@"C:\Temp\Consolidated-{reportDate.ToString("yyyyMMdd")}-{DateTime.Now.ToString("yyMMddhhmmss")}.xlsx";
                string templatePath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath), $@"Excel\Templates\", $@"Consolidated-Template.xlsx");

                Legendary.UpdateStatus($"Starting template: {templatePath}");
                Legendary.UpdateStatus($"Writing to workbok {fullPath} using template");

                Legendary.UpdateStatus($"Creating {reportTitle} for {reportDate.ToLongDateString()}");
                string sheetName = $@"Consol IS";
                ExcelReporter.OutputConsolidatedReportToWorkbook(MainConsolidatedReport, templatePath, fullPath, sheetName, false);

                ExcelReporter.OutputAccountDataToWorkbook(MainConsolidatedReport, fullPath, "IS DATA", EntityDataMaster,
                                                          reportConsolidatedAccounts, LedgerAccountMaster, AccountDataMaster);

                // Income statement results were added above so re-read the values...
                AccountDataMaster = AccountData.LoadAcccountDataByDate(reportDate);

                sheetName = $@"Consol BS";
                consolidatedType = "BS";
                reportTitle = $"CONSOLIDATING BALANCE SHEET";
                Legendary.UpdateStatus($"Creating {reportTitle} for {reportDate.ToLongDateString()}");
                reportType = ConsolidatedReportType.BalanceSheet;
                reportConsolidatedAccounts = ConsolidatedAccount.GetByConsolidatedType(consolidatedType, ConsolidatedAccountMaster);
                reportEntities = ConsolidatedReport.GetConsolidatedReportEntities(reportDate, consolidatedType, fund, EntityDataMaster,
                                                                                      reportConsolidatedAccounts, LedgerAccountMaster, AccountDataMaster);
                MainConsolidatedReport = new ConsolidatedReport(fundFullName, reportTitle, reportDate, reportType, reportConsolidatedAccounts, reportEntities);
                ExcelReporter.OutputConsolidatedReportToWorkbook(MainConsolidatedReport, templatePath, fullPath, sheetName, true);

                ExcelReporter.OutputAccountDataToWorkbook(MainConsolidatedReport, fullPath, "BS DATA", EntityDataMaster,
                                                          reportConsolidatedAccounts, LedgerAccountMaster, AccountDataMaster);

                this.lblLastCreatedFullPath.Text = fullPath;

                Legendary.UpdateStatus($"\r\n---------- Finished Consolidated Report Excel Workbook ----------");

            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                Legendary.StatusSuspendStatusBarUpdating = false;
            }
        }

        private void btnOpenLastCreated_Click(object sender, EventArgs e)
        {
            try
            {
                ExcelReporter.OpenReportFile(this.lblLastCreatedFullPath.Text);
            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
        }
    }
}
