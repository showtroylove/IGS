using System;
using System.Collections.Generic;
using DevExpress.Spreadsheet;

namespace SpreadsheetExamples {
    public static class PivotFormattingActions {

        static void ChangeStylePivotTable(IWorkbook workbook) {
            #region #Set Style
            Worksheet worksheet = workbook.Worksheets["Report1"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Access the pivot table by its name in the collection
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];
            // Set the pivot table style
            pivotTable.Style = workbook.TableStyles[BuiltInPivotStyleId.PivotStyleDark7];

            #endregion #Set Style
        }


        static void BandedColumns(IWorkbook workbook) {
            #region #Banded Columns
            Worksheet worksheet = workbook.Worksheets["Report4"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Access the pivot table by its name in the collection
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];
            // Apply the banded column formatting to the pivot table 
            pivotTable.BandedColumns = true;

            #endregion #Banded Columns
        }

        static void BandedRows(IWorkbook workbook) {
            #region #Banded Rows
            Worksheet worksheet = workbook.Worksheets["Report4"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Access the pivot table by its name in the collection
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];
            // Apply the banded row formatting to the pivot table 
            pivotTable.BandedRows = true;

            #endregion #Banded Rows
        }

        static void ShowColumnHeaders(IWorkbook workbook) {
            #region #Column Headers
            Worksheet worksheet = workbook.Worksheets["Report1"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Access the pivot table by its name in the collection
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];
            // Remove formatting from column headers
            pivotTable.ShowColumnHeaders = false;
            
            #endregion #Column Headers
        }

        static void ShowRowHeaders(IWorkbook workbook) {
            #region #Row Headers
            Worksheet worksheet = workbook.Worksheets["Report1"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Access the pivot table by its name in the collection
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];
            // Add the "Region" field to the column axis area 
            pivotTable.ColumnFields.Add(pivotTable.Fields["Region"]);

            // Remove formatting from row headers
            pivotTable.ShowRowHeaders = false;

            #endregion #Row Headers
        }
    }
}