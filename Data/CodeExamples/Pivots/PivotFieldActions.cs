using System;
using System.Collections.Generic;
using System.Drawing;
using DevExpress.Spreadsheet;

namespace SpreadsheetExamples {
    public static class PivotFieldActions {

        static void AddFieldToAxis(IWorkbook workbook) {
            #region #Add to Axis
            Worksheet sourceWorksheet = workbook.Worksheets["Data1"];
            Worksheet worksheet = workbook.Worksheets.Add();
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Create a pivot table
            PivotTable pivotTable = worksheet.PivotTables.Add(sourceWorksheet["A1:D41"], worksheet["B2"]);

            // Add the "Product" field to the row axis area
            pivotTable.RowFields.Add(pivotTable.Fields["Product"]);
            // Add the "Category" field to the column axis area
            pivotTable.ColumnFields.Add(pivotTable.Fields["Category"]);
            // Add the "Sales" field to the data area and specify the custom field name
            PivotDataField dataField = pivotTable.DataFields.Add(pivotTable.Fields["Sales"], "Sales(Sum)");
            // Specify the number format for the "Sales" field
            dataField.NumberFormat = @"_([$$-409]* #,##0.00_);_([$$-409]* (#,##0.00);_([$$-409]* "" - ""??_);_(@_)";
            // Add the "Region" field to the filter area
            pivotTable.PageFields.Add(pivotTable.Fields["Region"]);

            #endregion #Add to Axis
        }

        static void InsertFieldToAxis(IWorkbook workbook) {
            #region #Insert into Axis
            Worksheet worksheet = workbook.Worksheets["Report1"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Access the pivot table by its name in the collection
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];

            // Insert the "Region" field at the top of the row axis area
            pivotTable.RowFields.Insert(0, pivotTable.Fields["Region"]);

            #endregion #Insert into Axis
        }

        static void MoveFieldToAxis(IWorkbook workbook) {
            #region #Move to Axis
            Worksheet worksheet = workbook.Worksheets["Report1"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Access the pivot table by its name in the collection
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];

            // Move the "Region" field to the column axis area
            pivotTable.ColumnFields.Add(pivotTable.Fields["Region"]);

            #endregion #Move to Axis
        }

        static void MoveFieldUp(IWorkbook workbook) {
            #region #Move Up
            Worksheet worksheet = workbook.Worksheets["Report3"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Access the pivot table by its name in the collection
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];

            // Move the "Category" field one position up in the row area
            pivotTable.RowFields["Category"].MoveUp();

            #endregion #Move Up
        }

        static void MoveFieldDown(IWorkbook workbook) {
            #region #Move Down
            Worksheet worksheet = workbook.Worksheets["Report3"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Access the pivot table by its name in the collection
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];

            // Move the "Region" field one position down in the row area
            pivotTable.RowFields["Region"].MoveDown();

            #endregion #Move Down
        }
        
        static void RemoveFieldFromAxis(IWorkbook workbook) {
            #region #Remove from Axis
            Worksheet worksheet = workbook.Worksheets["Report1"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Access the pivot table by its name in the collection
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];

            // Remove the "Product" field from the row axis area
            pivotTable.RowFields.Remove(pivotTable.RowFields["Product"]);

            #endregion #Remove from Axis
        }

        static void SortFieldItems(IWorkbook workbook) {
            #region #Sort Field Items
            Worksheet worksheet = workbook.Worksheets["Report1"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Access the pivot table by its name in the collection
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];
            // Access the pivot field by its name in the collection
            PivotField field = pivotTable.Fields["Product"];
            // Sort items in the "Product" field 
            field.SortType = PivotFieldSortType.Ascending;
            #endregion #Sort Field Items
        }

        static void SortFieldItemsByDataField(IWorkbook workbook) {
            #region #Sort Field Items by Data Field
            Worksheet worksheet = workbook.Worksheets["Report1"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Access the pivot table by its name in the collection
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];
            // Access the pivot field by its name in the collection
            PivotField field = pivotTable.Fields["Product"];
            // Sort items in the "Product" field by "Sum of Sales"
            field.SortItems(PivotFieldSortType.Ascending, 0);
            #endregion #Sort Field Items by Data Field
        }

    }
}