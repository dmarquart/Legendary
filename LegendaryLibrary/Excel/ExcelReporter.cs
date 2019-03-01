using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Office = NetOffice.OfficeApi;
using Excel = NetOffice.ExcelApi;

namespace LegendaryLibrary
{
    static public class ExcelReporter
    {
        static public void OpenReportFile(string excelReportFileFullPath)
        {
            try
            {
                var excelApp = new Excel.Application();
                Excel.Workbook book = null;

                if (!System.IO.File.Exists(excelReportFileFullPath))
                {
                    Legendary.UpdateStatus($@"Report File Does Not Exist for Path: {excelReportFileFullPath}");
                    return;
                }

                book = excelApp.Workbooks.Open(excelReportFileFullPath);

                excelApp.Visible = true;
            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
        }

        static public void OutputListOfListsOfKeyValuesToNewWorkbook(List<KeyValuePair<string, SortedList<string, string>>> listOfListsOfKeyValues, 
                                                                     SortedList<string, string> columnsToExport, 
                                                                     string exportDirectory, string baseFileName, int maxRowsPerFile )
        {
            Excel.Application excelApp = null;
            Excel.Workbook book = null;
            Excel.Worksheet sheet = null;
            int skipCount = 0;
            int takeCount = 0;

            try
            {
                bool done = false;

                while (!done)
                {
                    excelApp = new Excel.Application();
                    excelApp.Visible = false;
                    book = excelApp.Workbooks.Add();
                    sheet = ExcelHelper.GetWorksheet(book, "Sheet1", true);

                    int row = 0;
                    int col = -1;
                    string key = "";

                    if ((listOfListsOfKeyValues.Count - skipCount) > maxRowsPerFile)
                        takeCount = maxRowsPerFile;
                    else
                        takeCount = (listOfListsOfKeyValues.Count - skipCount);

                    var listsToProcess = listOfListsOfKeyValues.Skip(skipCount).Take(takeCount).ToList();
                    object[,] cellData = new object[listsToProcess.Count + 2, columnsToExport.Count + 1];

                    foreach (var pair in columnsToExport)
                    {
                        col += 1;
                        row = 0;
                        cellData[row++, col] = pair.Value;

                        key = pair.Key.ToUpper();
                        foreach (var list in listsToProcess)
                            cellData[row++, col] = list.Value[key];
                    }

                    sheet.Range(sheet.Cells[1, 1], sheet.Cells[1, columnsToExport.Count]).Font.Bold = true;

                    // Write the entire object array at once to the excel sheet...
                    sheet.Range($@"A1:{GetColumnName(columnsToExport.Count + 1)}{listsToProcess.Count + 2}").Value2 = cellData;

                    for (int j = 1; j <= columnsToExport.Count; j++)
                        sheet.Columns[j].AutoFit();

                    string bookName = $@"{baseFileName}{(skipCount + 1).ToString("00000")}-to-" + 
                                      $@"{(skipCount + takeCount).ToString("00000")}-" + 
                                      $@"{DateTime.Now.ToString("yyMMddhhmmss")}.xlsx";
                    string bookNameFullPath = System.IO.Path.Combine(exportDirectory, bookName);
                    

                    try
                    {
                        if (System.IO.File.Exists(bookNameFullPath))
                            System.IO.File.Delete(bookNameFullPath);
                        book.SaveAs(bookNameFullPath);
                    }
                    catch (Exception)
                    {
                    }

                    Legendary.UpdateStatus($"        Saved: {bookNameFullPath} ");

                    excelApp.Quit();
                    excelApp.Dispose();
                    excelApp = null;

                    skipCount += takeCount;

                    if ((listOfListsOfKeyValues.Count - skipCount) <= 0)
                        done = true;
                }

            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
            finally
            {
                if ((excelApp != null) && (!excelApp.IsDisposed))
                {
                    excelApp.Quit();
                    excelApp.Dispose();
                    excelApp = null;
                }
            }
        }

        public static string GetColumnName(int columnNumber)
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string columnName = "";

            while (columnNumber > 0)
            {
                columnName = letters[(columnNumber - 1) % 26] + columnName;
                columnNumber = (columnNumber - 1) / 26;
            }

            return columnName;
        }

        static public void OutputGridToNewWorkbook(DataGridView grid, string descriptiveText)
        {
            var excelApp = new Excel.Application();
            Excel.Workbook book = null;
            Excel.Worksheet sheet = null;
            object[,] cellData = new object[5000, 104];

            try
            {
                excelApp.Visible = true;
                book = excelApp.Workbooks.Add();
                sheet = ExcelHelper.GetWorksheet(book, "Sheet1", true);

                int row = 1;
                int col = 1;

                sheet.Cells[row, col].Value2 = descriptiveText;
                sheet.Range(sheet.Cells[row, 1], sheet.Cells[row, col]).Font.Italic = true;

                row += 1;
                foreach (var column in grid.Columns)
                    sheet.Cells[row, col++].Value2 = (column as DataGridViewColumn).HeaderText;

                sheet.Range(sheet.Cells[row, 1], sheet.Cells[row, col]).Font.Bold = true;

                row += 1;
                col = 1;

                for (int i = 0; i < grid.Rows.Count; i++)
                    for (int j = 0; j < grid.Columns.Count; j++)
                        cellData[i, j] = grid[j, i].Value;

                // Write the entire object array at once to the excel sheet...
                sheet.Range("A3:CZ5003").Value2 = cellData;

                for (int j = 2; j <= grid.Columns.Count; j++)
                    sheet.Columns[j].AutoFit();

                // Let the user do the saving...
                
            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
            finally
            {
     //           excelApp.Quit();
     //           excelApp.Dispose();
            }
        }

        static public void OutputConsolidatedReportToWorkbook(ConsolidatedReport consolidatedReport, string templateFullPath, 
                                                              string excelReportFileFullPath, string sheetName, 
                                                              bool addToWorkbookIfExistsAlready)
        {
            var excelApp = new Excel.Application();
            Excel.Workbook book = null;
            Excel.Worksheet sheet = null;
            object[,] cellData = new object[5000, 104];

            try
            {
                excelApp.Visible = false;

                if ((addToWorkbookIfExistsAlready) && (System.IO.File.Exists(excelReportFileFullPath)))
                    book = excelApp.Workbooks.Open(excelReportFileFullPath);
                else
                {
                    if (System.IO.File.Exists(excelReportFileFullPath))
                        System.IO.File.Delete(excelReportFileFullPath);
                    book = excelApp.Workbooks.Open(templateFullPath);
                    book.SaveAs(excelReportFileFullPath);
                }
                
                sheet = ExcelHelper.GetWorksheet(book, sheetName);
                if (sheet is null)
                {
                    string msg = $@"Sheet: '{sheetName}' could not be found in workbook template: '{templateFullPath}'";
                    MessageBox.Show(msg);
                    return;
                }

                sheet.Activate();
                cellData[sheet.Range("FundName").Row - 1, sheet.Range("FundName").Column - 1] = consolidatedReport.FundFullName;
                cellData[sheet.Range("ReportTitle").Row - 1, sheet.Range("ReportTitle").Column - 1] = consolidatedReport.ReportTitle;
                cellData[sheet.Range("ReportDate").Row - 1, sheet.Range("ReportDate").Column - 1] = consolidatedReport.ReportDate;

                var reportStart = sheet.Range("ReportStart").Cells[1, 1];

                var accountListsByCategory = ConsolidatedAccount.GetSortedListsByCategory(consolidatedReport.ConsolidatedAccounts);

                int row = reportStart.Row - 1;
                int column = reportStart.Column - 1;
                bool[] sumLinesDrawn = { false, false, false, false, false, false, false, false, false, false };
                bool[] sumLinesDrawn2 = { false, false, false, false, false, false, false, false, false, false };

                var groups = new SortedList<int, string>();

                foreach (var accountList in accountListsByCategory)
                {
                    column = 0;

                    string category = accountList.Key.Substring(1);
                    cellData[row++, column] = category.ToUpper();
                    column += 1;
                    foreach (var account in accountList.Value.Values)
                        cellData[row++, column] = account.Name;
                    row += 1;
                    column += 1;
                    cellData[row++, column] = $"Total {category}".ToUpper();
                    row += 1;

                    int group = accountList.Value.Values[0].Group;
                    if (!groups.ContainsKey(group))
                        groups.Add(group, "Total ");
                    if (groups[group] == "Total ")
                        groups[group] += category.ToUpper();
                    else
                        groups[group] += " AND " + category.ToUpper();
                    
                    if (groups[group].Contains(" AND "))  // More than one in the list...
                    {
                        cellData[row++, column] = groups[group].ToUpper();
                        row += 1;
                    }
                }

                // If this is an IS then we need to output a Net Income (Loss) label...
                if (consolidatedReport.ReportType == ConsolidatedReportType.IncomeStatement)
                {
                    cellData[row++, column] = "NET INCOME (LOSS)";
                    row += 1;
                }

                column = reportStart.Column + 1;

                var sortedReportEntities = new SortedList<string, ConsolidatedReportEntity>();
                foreach (var reportEntity in consolidatedReport.ReportEntities)
                    sortedReportEntities.Add($"{reportEntity.Entity.SortOrder}-{reportEntity.Entity.Location}-{reportEntity.Entity.Entity_Type}", reportEntity);

                foreach (var reportEntity in sortedReportEntities.Values)
                {
                    var groupSums = new SortedList<int, SortedList<string, decimal>>();

                    row = reportStart.Row - 1 - 1;
                    column += 1;
                    if (reportEntity.Entity.Location.Contains(reportEntity.Entity.Entity_Type))
                        cellData[row++, column] = reportEntity.Entity.Location;
                    else
                        cellData[row++, column] = reportEntity.Entity.Location + " - " + reportEntity.Entity.Entity_Type;

                    int accountIndex = -1;
                    foreach (var accountList in accountListsByCategory)
                    {
                        accountIndex += 1;
                        decimal categorySum = decimal.Zero;
                        row += 1; // Skip header row for category...
                        foreach (var account in accountList.Value.Values)
                            foreach (var reportItem in reportEntity.ReportItems)
                                if (reportItem.ConsolidatedAccount.Consolidated_Account_Id == account.Consolidated_Account_Id)
                                {
                                    cellData[row++, column] = reportItem.Amount;
                                    categorySum += reportItem.Amount;
                                    break;
                                }
                        row += 1;

                        if (!sumLinesDrawn[accountIndex])
                        {
                            Excel.Range startCell = sheet.Cells[row + 1, column + 1];
                            Excel.Range endCell = sheet.Cells[row + 1, column + sortedReportEntities.Count];
                            sheet.Range(startCell, endCell).Borders[Excel.Enums.XlBordersIndex.xlEdgeTop].Weight = Excel.Enums.XlBorderWeight.xlThin;
                            sheet.Range(startCell, endCell).Borders[Excel.Enums.XlBordersIndex.xlEdgeTop].LineStyle = Excel.Enums.XlLineStyle.xlContinuous;
                            sheet.Range(startCell, endCell).Borders[Excel.Enums.XlBordersIndex.xlEdgeBottom].Weight = Excel.Enums.XlBorderWeight.xlThin;
                            sheet.Range(startCell, endCell).Borders[Excel.Enums.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.Enums.XlLineStyle.xlDouble;
                            sumLinesDrawn[accountIndex] = true;
                        }

                        cellData[row++, column] = categorySum;

                        row += 1;

                        int group = accountList.Value.Values[0].Group;
                        string category = accountList.Key;//.Substring(1);
                        if (!groupSums.ContainsKey(group))
                            groupSums.Add(group, new SortedList<string, decimal>());
                        groupSums[group].Add(category, categorySum);

                        // If we get to > 2 group, which is the max for now, then we'll add them together and output a line...
                        if (groupSums[group].Count > 1)
                        {
                            decimal groupSum = decimal.Zero;
                            foreach (var pair in groupSums[group])
                                groupSum += pair.Value;

                            if (!sumLinesDrawn2[accountIndex])
                            {
                                Excel.Range startCell = sheet.Cells[row + 1, column + 1];
                                Excel.Range endCell = sheet.Cells[row + 1, column + sortedReportEntities.Count];
                                sheet.Range(startCell, endCell).Borders[Excel.Enums.XlBordersIndex.xlEdgeTop].Weight = Excel.Enums.XlBorderWeight.xlThin;
                                sheet.Range(startCell, endCell).Borders[Excel.Enums.XlBordersIndex.xlEdgeTop].LineStyle = Excel.Enums.XlLineStyle.xlContinuous;
                                sheet.Range(startCell, endCell).Borders[Excel.Enums.XlBordersIndex.xlEdgeBottom].Weight = Excel.Enums.XlBorderWeight.xlThin;
                                sheet.Range(startCell, endCell).Borders[Excel.Enums.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.Enums.XlLineStyle.xlDouble;
                                if (consolidatedReport.ReportType != ConsolidatedReportType.IncomeStatement)
                                    sumLinesDrawn2[accountIndex] = true;
                            }

                            cellData[row++, column] = groupSum;

                            row += 1;
                        }
                    }

                    // If this is an IS then we need to calculate and output a Net Income (Loss) value...
                    if (consolidatedReport.ReportType == ConsolidatedReportType.IncomeStatement)
                    {
                        decimal netIncomeLoss = decimal.Zero;
                        foreach (var gs in groupSums.Values)
                            foreach (var cs in gs.Values)
                                netIncomeLoss += cs;

                        if (!sumLinesDrawn2[accountIndex])
                        {
                            Excel.Range startCell = sheet.Cells[row + 1, column + 1];
                            Excel.Range endCell = sheet.Cells[row + 1, column + sortedReportEntities.Count];
                            sheet.Range(startCell, endCell).Borders[Excel.Enums.XlBordersIndex.xlEdgeTop].Weight = Excel.Enums.XlBorderWeight.xlThin;
                            sheet.Range(startCell, endCell).Borders[Excel.Enums.XlBordersIndex.xlEdgeTop].LineStyle = Excel.Enums.XlLineStyle.xlContinuous;
                            sheet.Range(startCell, endCell).Borders[Excel.Enums.XlBordersIndex.xlEdgeBottom].Weight = Excel.Enums.XlBorderWeight.xlThin;
                            sheet.Range(startCell, endCell).Borders[Excel.Enums.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.Enums.XlLineStyle.xlDouble;
                            sumLinesDrawn2[accountIndex] = true;
                        }

                        cellData[row++, column] = netIncomeLoss;

                        row += 1;
                    }

                }

                // Write the entire object array at once to the excel sheet...
                sheet.Range("A1:CZ500").Value2 = cellData;

                sheet.Range("D1").Select();
                sheet.Cells[1, 1].Select();

                book.Save();
                book.Close();
                book.Dispose();
            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
            finally
            {
                excelApp.Quit();
                excelApp.Dispose();
            }
        }
        static public void OutputAccountDataToWorkbook(ConsolidatedReport consolidatedReport, 
                                                       string excelReportFileFullPath, string sheetName,
                                                       List<EntityData> entities,
                                                       List<ConsolidatedAccount> consolidatedAccounts,
                                                       List<LedgerAccount> ledgerAccounts,
                                                       List<AccountData> accountData)
        {
            var excelApp = new Excel.Application();
            Excel.Workbook book = null;
            Excel.Worksheet sheet = null;

            try
            {
                excelApp.Visible = false;

                if (!System.IO.File.Exists(excelReportFileFullPath))
                {
                    Legendary.UpdateStatus($@"File to add Account Data to doesn't exists: {excelReportFileFullPath}");
                    return;
                }

                book = excelApp.Workbooks.Open(excelReportFileFullPath);

                sheet = ExcelHelper.GetWorksheet(book, sheetName);
                if (sheet == null)
                {
                    sheet = book.Worksheets.Add() as Excel.Worksheet;
                    if (sheet == null)
                    {
                        MessageBox.Show($@"Could not add new worksheet to workbook: {excelReportFileFullPath}");
                        return;
                    }
                    sheet.Name = sheetName;
                }

                sheet.Activate();

                var sortedReportEntities = new SortedList<string, ConsolidatedReportEntity>();
                foreach (var reportEntity in consolidatedReport.ReportEntities)
                {
                    string entitySortKey = $@"{reportEntity.Entity.SortOrder}" +
                                           $@"-{reportEntity.Entity.Location}" +
                                           $@"-{reportEntity.Entity.Entity_Type}" +
                                           $@"-{reportEntity.Entity.Entity_Id}";
                    sortedReportEntities.Add(entitySortKey, reportEntity);
                }

                var consolidatedAccountListsByCategory = ConsolidatedAccount.GetSortedListsByCategory(consolidatedReport.ConsolidatedAccounts);

                //                                                          Consolidated_Account_Id    
                //                                                                     |      Ledger_Account_Id
                //                                                                     |               |          EntitySortKey
                //                                                                     |               |                |
                var accountDataByLedgerAccountsByConsolidatedAccount = new SortedList<int, SortedList<int, SortedList<string, AccountData>>>();

                foreach (var consolidatedAccountList in consolidatedAccountListsByCategory)
                    foreach (var consolidatedAccount in consolidatedAccountList.Value.Values)
                    {
                        SortedList<int, SortedList<string, AccountData>> accountDataByEntityByLedgerAccounts = null;
                        if (! accountDataByLedgerAccountsByConsolidatedAccount.ContainsKey(consolidatedAccount.Consolidated_Account_Id))
                        {
                            accountDataByEntityByLedgerAccounts = new SortedList<int, SortedList<string, AccountData>>();
                            accountDataByLedgerAccountsByConsolidatedAccount.Add(consolidatedAccount.Consolidated_Account_Id, accountDataByEntityByLedgerAccounts);
                        }

                        accountDataByEntityByLedgerAccounts = accountDataByLedgerAccountsByConsolidatedAccount[consolidatedAccount.Consolidated_Account_Id];

                        foreach (var ledgerAccount in ledgerAccounts)
                            if (ledgerAccount.Consolidated_Account_Id == consolidatedAccount.Consolidated_Account_Id)
                            {
                                SortedList<string, AccountData> accountDataByEntity = null;
                                if (! accountDataByEntityByLedgerAccounts.ContainsKey(ledgerAccount.Ledger_Account_Id))
                                {
                                    accountDataByEntity = new SortedList<string, AccountData>();
                                    accountDataByEntityByLedgerAccounts.Add(ledgerAccount.Ledger_Account_Id, accountDataByEntity);
                                }

                                accountDataByEntity = accountDataByEntityByLedgerAccounts[ledgerAccount.Ledger_Account_Id];

                                string entitySortKey = "";
                                foreach (var reportEntity in consolidatedReport.ReportEntities)
                                    foreach (var reportItem in reportEntity.ReportItems)
                                        foreach (var accountAmount in reportItem.AccountAmounts)
                                            if (accountAmount.Ledger_Account_Id == ledgerAccount.Ledger_Account_Id)
                                            {
                                                entitySortKey = $@"{reportEntity.Entity.SortOrder}" +
                                                                $@"-{reportEntity.Entity.Location}" +
                                                                $@"-{reportEntity.Entity.Entity_Type}" +
                                                                $@"-{reportEntity.Entity.Entity_Id}";
                                                accountDataByEntity.Add(entitySortKey, accountAmount);
                                            }
                            }
                    }

                var reportStart = sheet.Range("ReportStart").Cells[1, 1];
                int row = reportStart.Row-1;
                int column = reportStart.Column-1;
                int entitySortKeyRow = 0;
                int entityNameRow = 0;
                int entityStartColumn = -1;

                object[,] cellData = new object[5000, 104];

                // Write out the entity headers first
                entityStartColumn = reportStart.Column + 4 - 1;
                column = entityStartColumn;
                entityNameRow = reportStart.Row - 1 - 1;
                entitySortKeyRow = reportStart.Row - 2 - 1;
                foreach (var reportEntity in sortedReportEntities.Values)
                {
                    string entitySortKey = $@"{reportEntity.Entity.SortOrder}" +
                                           $@"-{reportEntity.Entity.Location}" +
                                           $@"-{reportEntity.Entity.Entity_Type}" +
                                           $@"-{reportEntity.Entity.Entity_Id}";
                    cellData[entitySortKeyRow, column] = entitySortKey;
                    if (reportEntity.Entity.Location.Contains(reportEntity.Entity.Entity_Type))
                        cellData[entityNameRow, column++] = reportEntity.Entity.Location;
                    else
                        cellData[entityNameRow, column++] = reportEntity.Entity.Location + " - " + reportEntity.Entity.Entity_Type;
                }

                row = reportStart.Row - 1;
                column = reportStart.Column - 1;

                int last_Consolidated_Account_Id = 0;

                foreach (var consolidatedPair in accountDataByLedgerAccountsByConsolidatedAccount)
                {
                    column = reportStart.Column - 1;

                    int consolidated_Account_Id = consolidatedPair.Key;
                    var consolidatedAccount = ConsolidatedAccount.GetConsolidatedAccountById(consolidated_Account_Id, consolidatedAccounts);
                    if (consolidated_Account_Id != last_Consolidated_Account_Id)
                    {
                        cellData[row++, column] = consolidatedAccount.Category;
                        last_Consolidated_Account_Id = consolidated_Account_Id;
                    }
                    column += 1;
                    cellData[row++, column++] = consolidatedAccount.Name;

                    int savedColumn = column;
                    foreach (var ledgerPair in consolidatedPair.Value)
                    {
                        column = savedColumn;

                        var ledgerAccount = LedgerAccount.GetLedgerAccountById(ledgerPair.Key, ledgerAccounts);
                        cellData[row, column++] = ledgerAccount.Number;
                        cellData[row, column++] = ledgerAccount.Name;

                        for (int col = entityStartColumn; col < (entityStartColumn + sortedReportEntities.Count); col++)
                            foreach (var amountPair in ledgerPair.Value)
                                if (amountPair.Key == (cellData[entitySortKeyRow, col] as string))
                                    cellData[row, col] = amountPair.Value.Value;

                        row += 1;
                    }
                }

                // Clear out the entity sort values...
                column = entityStartColumn;
                row = entitySortKeyRow;
                foreach (var reportEntity in sortedReportEntities.Values)
                    cellData[row, column++] = "";

                // Write the entire object array at once to the excel sheet...
                sheet.Range("A1:CZ500").Value2 = cellData;

                book.Save();
                book.Close();
                book.Dispose();

            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
            finally
            {
                excelApp.Quit();
                excelApp.Dispose();
            }
        }


    }
}
