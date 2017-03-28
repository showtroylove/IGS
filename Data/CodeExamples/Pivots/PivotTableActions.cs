using System;
using System.Collections.Generic;
using System.Drawing;
using DevExpress.Spreadsheet;

namespace SpreadsheetExamples {
    public static class PivotTableActions {

        static void CreatePivotTableFromRange(IWorkbook workbook) {
            #region #Create from Range
            Worksheet sourceWorksheet = workbook.Worksheets["Data1"];
            Worksheet worksheet = workbook.Worksheets.Add();
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Create a pivot table using the cell range "A1:D41" as the data source
            PivotTable pivotTable = worksheet.PivotTables.Add(sourceWorksheet["A1:D41"], worksheet["B2"]);

            // Add the "Category" field to the row axis area
            pivotTable.RowFields.Add(pivotTable.Fields["Category"]);
            // Add the "Product" field to the row axis area
            pivotTable.RowFields.Add(pivotTable.Fields["Product"]);
            // Add the "Sales" field to the data area
            pivotTable.DataFields.Add(pivotTable.Fields["Sales"]);

            // Set the default style for the pivot table
            pivotTable.Style = workbook.TableStyles.DefaultPivotStyle;

            #endregion #Create from Range
        }

        static void CreatePivotTableFromCache(IWorkbook workbook) {
            #region #Create from PivotCache
            Worksheet worksheet = workbook.Worksheets.Add();
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Create a pivot table based on the specified PivotTable cache
            PivotCache cache = workbook.Worksheets["Report1"].PivotTables["PivotTable1"].Cache;
            PivotTable pivotTable = worksheet.PivotTables.Add(cache, worksheet["B2"]);

            // Add the "Category" field to the row axis area
            pivotTable.RowFields.Add(pivotTable.Fields["Category"]);
            // Add the "Product" field to the row axis area
            pivotTable.RowFields.Add(pivotTable.Fields["Product"]);
            // Add the "Sales" field to the data area
            pivotTable.DataFields.Add(pivotTable.Fields["Sales"]);

            // Set the default style for the pivot table
            pivotTable.Style = workbook.TableStyles.DefaultPivotStyle;

            #endregion #Create from PivotCache
        }

        static void RemovePivotTable(IWorkbook workbook) {
            #region #Remove Table
            Worksheet worksheet = workbook.Worksheets["Report1"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Access the pivot table by its name in the collection
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];
            // Remove the pivot table from the collection
            worksheet.PivotTables.Remove(pivotTable);

            #endregion #Remove Table
        }
        static void ChangePivotTableLocation(IWorkbook workbook) {
            #region #Change Location
            Worksheet worksheet = workbook.Worksheets["Report1"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Change pivot table location
            worksheet.PivotTables["PivotTable1"].MoveTo(worksheet["A7"]);

            #endregion #Change Location
        }
        static void MovePivotTableToWorksheet(IWorkbook workbook) {
            #region #Move to Worksheet
            Worksheet worksheet = workbook.Worksheets["Report1"];

            // Create new worksheet
            Worksheet targetWorksheet = workbook.Worksheets.Add();

            // Access the pivot table by its name in the collection
            // and move it to the new worksheet
            worksheet.PivotTables["PivotTable1"].MoveTo(targetWorksheet["B2"]);
            // Refresh the moved pivot table
            targetWorksheet.PivotTables["PivotTable1"].Cache.Refresh();
            workbook.Worksheets.ActiveWorksheet = targetWorksheet;
            #endregion #Move to Worksheet
        }

        static void ChangePivotTableDataSource(IWorkbook workbook) {
            #region #Change DataSource
            Worksheet worksheet = workbook.Worksheets["Report1"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Access the pivot table by its name in the collection
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];
            Worksheet sourceWorksheet = workbook.Worksheets["Data2"];
            // Change the data source of the pivot table
            pivotTable.ChangeDataSource(sourceWorksheet["A1:H6367"]);

            // Add the "State" field to the row axis area
            pivotTable.RowFields.Add(pivotTable.Fields["State"]);
            // Add the "Yearly Earnings" field to the data area
            PivotDataField dataField = pivotTable.DataFields.Add(pivotTable.Fields["Yearly Earnings"]);
            dataField.SummarizeValuesBy = PivotDataConsolidationFunction.Average;

            #endregion #Change DataSource
        }

        static void ClearPivotTable(IWorkbook workbook) {
            #region #Clear Table
            Worksheet worksheet = workbook.Worksheets["Report1"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Clear the pivot table
            worksheet.PivotTables["PivotTable1"].Clear();

            #endregion #Clear Table
        }

        static void ChangeBehaviorOptions(IWorkbook workbook) {
            #region #Change Behavior Options
            Worksheet worksheet = workbook.Worksheets["Report1"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            worksheet.Columns["B"].WidthInCharacters = 40;

            // Access the pivot table by its name in the collection
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];

            // Restrict an end-user's ability to modify the pivot table
            PivotBehaviorOptions behaviorOptions = pivotTable.Behavior;
            behaviorOptions.AutoFitColumns = false;
            behaviorOptions.EnableFieldList = false;

            // Refresh pivot table
            pivotTable.Cache.Refresh();

            #endregion #Change Behavior Options
        }
    }
}