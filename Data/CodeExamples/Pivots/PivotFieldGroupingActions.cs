using System.Collections.Generic;
using DevExpress.Spreadsheet;

namespace SpreadsheetExamples {
    public static class PivotFieldGroupingActions {

        static void GroupFieldItems(IWorkbook workbook) {
            #region #Group Field Items
            Worksheet worksheet = workbook.Worksheets["Report11"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Access the pivot table by its name in the collection.
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];
            // Access the "State" field by its name in the collection.
            PivotField field = pivotTable.Fields["State"];
            // Add the "State" field to the column axis area.
            pivotTable.ColumnFields.Add(field);

            // Group the first three items in the field.
            IEnumerable<int> items = new List<int>() { 0, 1, 2 };
            field.GroupItems(items);
            // Access the created grouped field by its index in the field collection.
            int groupedFieldIndex = pivotTable.Fields.Count - 1;
            PivotField groupedField = pivotTable.Fields[groupedFieldIndex];
            // Set the grouped item caption to "West".
            groupedField.Items[0].Caption = "West";
            #endregion #Group Field Items
        }

        static void GroupFieldByDates(IWorkbook workbook) {
            #region #Group Field by Dates
            Worksheet worksheet = workbook.Worksheets["Report8"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Access the pivot table by its name in the collection
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];
            // Access the "DATE" field by its name in the collection
            PivotField field = pivotTable.Fields["DATE"];
            // Group field items by quarters and months
            field.GroupItems(PivotFieldGroupByType.Quarters | PivotFieldGroupByType.Months);
            #endregion #Group Field by Dates
        }

        static void GroupFieldByNumericRanges(IWorkbook workbook) {
            #region #Group Field by Numeric Ranges
            Worksheet worksheet = workbook.Worksheets["Report9"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Access the pivot table by its name in the collection
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];
            // Access the "Usual Hours Worked" field by its name in the collection
            PivotField field = pivotTable.Fields["Usual Hours Worked"];
            // Group field items from 0 to 150 by 10
            field.GroupItems(0, 150, 10, PivotFieldGroupByType.NumericRanges);
            #endregion #Group Field by Numeric Ranges
        }

        static void UngroupSpecificItem(IWorkbook workbook) {
            #region #Ungroup Specific Item
            Worksheet worksheet = workbook.Worksheets["Report11"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Access the pivot table by its name in the collection
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];
            // Access the "State" field by its name in the collection.
            PivotField field = pivotTable.Fields["State"];
            // Add the "State" field to the column axis area.
            pivotTable.ColumnFields.Add(field);

            // Group the first three items in the field.
            IEnumerable<int> items = new List<int>() { 0, 1, 2 };
            field.GroupItems(items);
            // Access the created grouped field by its index in the field collection.
            int groupedFieldIndex = pivotTable.Fields.Count - 1;
            PivotField groupedField = pivotTable.Fields[groupedFieldIndex];
            // Set the grouped item caption to "West".
            groupedField.Items[0].Caption = "West";

            // Group the remaining field items.
            items = new List<int>() { 3, 4, 5 };
            field.GroupItems(items);
            // Set the grouped item caption to "Midwest"
            groupedField.Items[1].Caption = "Midwest";

            // Ungroup the "West" item.
            items = new List<int> { 0 };
            groupedField.UngroupItems(items);
            #endregion #Ungroup Specific Item
        }

        static void UngroupFieldItems(IWorkbook workbook) {
            #region #Ungroup Field Items
            Worksheet worksheet = workbook.Worksheets["Report8"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Access the pivot table by its name in the collection.
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];
            // Access the "DATE" field by its name in the collection.
            PivotField field = pivotTable.Fields["DATE"];
            // Group field items by days.
            field.GroupItems(field.GroupingInfo.DefaultStartValue, field.GroupingInfo.DefaultEndValue, 50, PivotFieldGroupByType.Days);
            // Ungroup field items.
            field.UngroupItems();
            #endregion #Ungroup Field Items
        }
    }
}