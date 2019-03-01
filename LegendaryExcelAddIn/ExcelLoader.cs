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
    static public class ExcelLoader
    {
        static public SortedList<string, SortedList<string, string>> LoadExcelFile(string fullFilePath, string primaryKeyHeaderValue,
                                                                                   string sheetName = "", int firstRow = 1, int firstColumn = 1,
                                                                                   object[] extraCellValues = null)
        {
            SortedList<string, SortedList<string, string>> listOfLists = null;

            if (!System.IO.File.Exists(fullFilePath))
            {
                LegendaryConstants.UpdateStatus($"File '{fullFilePath}' Does Not Exist!");
                return null;
            }

            listOfLists = ExcelLoader.LoadWorkbookIntoListOfLists(fullFilePath, primaryKeyHeaderValue, sheetName, firstRow, firstColumn, extraCellValues);
            if (listOfLists == null)
                return null;

            if ((listOfLists == null) || (listOfLists.Count <= 1))
                LegendaryConstants.UpdateStatus($"File '{fullFilePath}' Didn't Contain Data");
            else
                LegendaryConstants.UpdateStatus($"Loaded {listOfLists.Count - 1} Rows of Data");

            return listOfLists;

        }

        static public SortedList<string, SortedList<string, string>> LoadWorkbookIntoListOfLists(string excelFileFullPath, string primaryKeyHeaderValue, 
                                                                                                 string sheetName = "", int firstRow = 1, int firstColumn = 1,
                                                                                                 object[] extraCellValues = null)
        {
            var excelApp = new Excel.Application();
            Excel.Workbook book = null;
            Excel.Worksheet sheet = null;

            primaryKeyHeaderValue = primaryKeyHeaderValue.ToUpper();

            var listOfLists = new SortedList<string, SortedList<string, string>>();
            try
            {
              //  excelApp.Visible = true;

                if (!System.IO.File.Exists(excelFileFullPath))
                    throw new Exception($"ExcelLoader::LoadWorkbookIntoListOfLists::File {excelFileFullPath} doesn't Exist!!!\n");

                book = excelApp.Workbooks.Open(excelFileFullPath, false, true);
                sheet = ExcelHelper.GetWorksheet(book, sheetName, true);
                if (sheet == null)
                {
                    LegendaryConstants.UpdateStatus($"No sheets found in {excelFileFullPath}");
                    return null;
                }

                if (extraCellValues != null)
                {
                    LegendaryConstants.UpdateStatus("   Reading Extra Cell Values");
                    for (int index = 0; index < extraCellValues.Length; index++)
                        extraCellValues[index] = (sheet.Range(extraCellValues[index]).Cells[1, 1].Value2 as string);
                }

                int colCount = 0;
                int colBlankCount = 0;
                int colBlankCountInRow = 0;
                int row = firstRow;
                int col = firstColumn - 1;
                bool done = false;
                string cellValue = "";
                int primaryKeyHeaderIndex = 0;

                Excel.Range range = sheet.Range(sheet.Cells[1, 1], sheet.Cells[10000, 1000]);
                object[,] sheetCells = (object[,])range.Value2;

                // Get headers and count columns...
                LegendaryConstants.UpdateStatus("   Reading Header Values");
                var headerList = new SortedList<int, string>();
                var tempList = new SortedList<string, string>();
                while (! done)
                {
                    col += 1;
                    if (sheetCells[row, col] == null)
                    {
                        colBlankCount += 1;
                        cellValue = $"Blank~{colBlankCount.ToString()}~";
                        colBlankCountInRow += 1;
                        if (colBlankCountInRow > 4)
                        {
                            done = true;
                            colCount = col - colBlankCountInRow - firstColumn;
                        }
                    }
                    else
                    {
                        colBlankCountInRow = 0;
                        cellValue = sheetCells[row, col].ToString();
                        if (string.Compare(cellValue, primaryKeyHeaderValue, true) == 0)
                            primaryKeyHeaderIndex = col;
                    }

                    string fieldKey = cellValue.ToUpper().Trim();
                    while (tempList.ContainsKey(fieldKey))
                    {
                        fieldKey = fieldKey + "~";
                        cellValue = cellValue + "~";
                    }

                    tempList.Add(fieldKey, cellValue);
                    headerList.Add(col, cellValue.ToUpper().Trim());
                }

                listOfLists.Add("HEADER", tempList);

                LegendaryConstants.UpdateStatus("   Reading Row Data");

                done = false;
                int rowBlankCount = 0;
                while (!done)
                {
                    row += 1;
                    if (sheetCells[row, firstColumn] == null)
                    {
                        rowBlankCount += 1;
                        if (rowBlankCount >= 5)
                            done = true;
                    }
                    else
                    {
                        //LegendaryConstants.UpdateStatus(".", false);
                        tempList = new SortedList<string, string>();
                        string rowPrimaryKey = $"Row~{row}~";
                        for (col = firstColumn; col <= (firstColumn + colCount); col++)
                        {
                            if (sheetCells[row, col] == null)
                                cellValue = "";
                            else
                                cellValue = sheetCells[row, col].ToString();
                            tempList.Add(headerList[col], cellValue);
                        }
                        if (listOfLists.ContainsKey(tempList[primaryKeyHeaderValue]))
                            LegendaryConstants.UpdateStatus($"Primary Key '{tempList[primaryKeyHeaderValue]}' Already Exists, so skipping!");
                        else
                            listOfLists.Add(tempList[primaryKeyHeaderValue], tempList);
                    }
                }

            }
            catch (Exception ex)
            {
                LegendaryConstants.UpdateStatusFromException(ex);
            }
            finally
            {
                book.Close(false);
                excelApp.Quit();
                excelApp.Dispose();
            }

            return listOfLists;
        }
    }
}
