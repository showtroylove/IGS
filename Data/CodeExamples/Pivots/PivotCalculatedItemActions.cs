using DevExpress.Spreadsheet;

namespace SpreadsheetExamples {
    public static class PivotCalculatedItemActions {

        static void AddCalculatedItem(IWorkbook workbook) {
            #region #Add Calculated Item
            Worksheet worksheet = workbook.Worksheets["Report10"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Access the pivot table by its name in the collection
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];

            // Access the pivot field by its name in the collection
            PivotField field = pivotTable.Fields["State"];

            // Add calculated items to the "State" field
            field.CalculatedItems.Add("=Arizona+California+Colorado", "West Total");
            field.CalculatedItems.Add("=Illinois+Kansas+Wisconsin", "Midwest Total");
            #endregion #Add Calculated Item
        }

        static void RemoveCalculatedItem(IWorkbook workbook) {
            #region #Remove Calculated Item
            Worksheet worksheet = workbook.Worksheets["Report7"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Access the pivot table by its name in the collection
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];

            // Access the pivot field by its name in the collection
            PivotField field = pivotTable.Fields["Customer"];

            //Remove the calculated item by its index from the collection
            field.CalculatedItems.RemoveAt(0);
            #endregion #Remove Calculated Item
        }

        static void ModifyCalculatedItem(IWorkbook workbook) {
            #region #Modify Calculated Item
            Worksheet worksheet = workbook.Worksheets["Report7"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Access the pivot table by its name in the collection
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];

            // Access the pivot field by its name in the collection
            PivotField field = pivotTable.Fields["Customer"];

            // Access the calculated item by its index in the collection
            PivotItem item = field.CalculatedItems[0];

            //Change the formula for the calculated item
            item.Formula = "='Big Foods'*115%";
            #endregion #Modify Calculated Item
        }
    }
}