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
    static public class ExcelLoader
    {
        static public SortedList<string, SortedList<string, string>> LoadExcelFile(string fullFilePath, string primaryKeyHeaderValue,
                                                                                   string sheetName = "", int firstRow = 1, int firstColumn = 1,
                                                                                   object[] extraCellValues = null, 
                                                                                   int maxRows = 10000, int maxCols = 104, int expectedValuesPerRow = -1)
        {
            SortedList<string, SortedList<string, string>> listOfLists = null;

            if (!System.IO.File.Exists(fullFilePath))
            {
                Legendary.UpdateStatus($"        File '{fullFilePath}' Does Not Exist!");
                return null;
            }

            listOfLists = ExcelLoader.LoadWorkbookIntoListOfLists(fullFilePath, primaryKeyHeaderValue, sheetName, firstRow, firstColumn, 
                                                                  extraCellValues, maxRows, maxCols, expectedValuesPerRow);
            if (listOfLists == null)
                return null;

            if ((listOfLists == null) || (listOfLists.Count <= 1))
                Legendary.UpdateStatus($"        File '{fullFilePath}' Didn't Contain Data");
            else
                Legendary.UpdateStatus($"        Loaded {listOfLists.Count - 1} Rows of Data");

            return listOfLists;

        }

        static public SortedList<string, SortedList<string, string>> LoadWorkbookIntoListOfLists(string excelFileFullPath, string primaryKeyHeaderValue, 
                                                                                                 string sheetName = "", int firstRow = 1, int firstColumn = 1,
                                                                                                 object[] extraCellValues = null, 
                                                                                                 int maxRows = 10000, int maxCols = 104, int expectedValuesPerRow = -1)
        {
            var excelApp = new Excel.Application();
            Excel.Workbook book = null;
            Excel.Worksheet sheet = null;

            primaryKeyHeaderValue = primaryKeyHeaderValue.ToUpper();

            var listOfLists = new SortedList<string, SortedList<string, string>>();
            try
            {
                if (!System.IO.File.Exists(excelFileFullPath))
                    throw new Exception($"ExcelLoader::LoadWorkbookIntoListOfLists::File {excelFileFullPath} doesn't Exist!!!\n");

                book = excelApp.Workbooks.Open(excelFileFullPath, false, true);
                sheet = ExcelHelper.GetWorksheet(book, sheetName, true);
                if (sheet == null)
                {
                    Legendary.UpdateStatus($"        No sheets found in {excelFileFullPath}");
                    return null;
                }

                if (extraCellValues != null)
                {
                    Legendary.UpdateStatus("        Reading Extra Cell Values");
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

                Legendary.UpdateStatus("        Reading All Values Into Memory");
                Excel.Range range = sheet.Range(sheet.Cells[1, 1], sheet.Cells[maxRows, maxCols]);
                object[,] sheetCells = (object[,])range.Value2;

                // Get headers and count columns...
                Legendary.UpdateStatus("        Getting Header Values");
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

                Legendary.UpdateStatus("        Getting Row Data");

                int valuesFound = 0;
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
                        tempList = new SortedList<string, string>();
                        valuesFound = 0;
                        for (col = firstColumn; col <= (firstColumn + colCount); col++)
                        {
                            if (sheetCells[row, col] == null)
                                cellValue = "";
                            else
                            {
                                valuesFound += 1;
                                cellValue = sheetCells[row, col].ToString();
                            }
                            tempList.Add(headerList[col], cellValue);
                        }

                        if ((string.Compare(tempList[primaryKeyHeaderValue], primaryKeyHeaderValue, true) != 0) && 
                            ((expectedValuesPerRow == -1) || (valuesFound >= expectedValuesPerRow)))
                        {
                            if (listOfLists.ContainsKey(tempList[primaryKeyHeaderValue]))
                                Legendary.UpdateStatus($"        Primary Key '{tempList[primaryKeyHeaderValue]}' Already Exists, so skipping!");
                            else
                                listOfLists.Add(tempList[primaryKeyHeaderValue], tempList);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
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
