using System;
using DevExpress.Spreadsheet;
using DevExpress.Utils;
using System.Globalization;
using System.Collections.Generic;

namespace SpreadsheetExamples {
    public static class SortActions {
        static void SimpleSort(IWorkbook workbook) {
            #region #SimpleSort
            Worksheet worksheet = workbook.Worksheets[0];

            // Fill in the range with data.
            worksheet.Cells["A2"].Value = "Ray Bradbury";
            worksheet.Cells["A3"].Value = "F. Scott Fitzgerald";
            worksheet.Cells["A4"].Value = "Harper Lee";
            worksheet.Cells["A5"].Value = "Ernest Hemingway";
            worksheet.Cells["A6"].Value = "J.D. Salinger";
            worksheet.Cells["A7"].Value = "Gene Wolfe";

            // Sort the range in ascending order.
            Range range = worksheet.Range["A2:A7"];
            worksheet.Sort(range);

            // Create a heading.
            Range header = worksheet.Range["A1"];
            header[0].Value = "Authors in ascending order";
            header.ColumnWidthInCharacters = 28;
            header.Style = workbook.Styles["Header"];
            #endregion #SimpleSort
        }

        static void DescendingOrder(IWorkbook workbook) {
            #region #DescendingOrder
            Worksheet worksheet = workbook.Worksheets[0];

            // Fill in the range with data.
            worksheet.Cells["A2"].Value = "Ray Bradbury";
            worksheet.Cells["A3"].Value = "F. Scott Fitzgerald";
            worksheet.Cells["A4"].Value = "Harper Lee";
            worksheet.Cells["A5"].Value = "Ernest Hemingway";
            worksheet.Cells["A6"].Value = "J.D. Salinger";
            worksheet.Cells["A7"].Value = "Gene Wolfe";

            // Sort the range in descending order.
            Range range = worksheet.Range["A2:A7"];
            worksheet.Sort(range, false);

            // Create a heading.
            Range header = worksheet.Range["A1"];
            header[0].Value = "Authors in descending order";
            header.ColumnWidthInCharacters = 28;
            header.Style = workbook.Styles["Header"];
            #endregion #DescendingOrder
        }

        static void SelectComparer(IWorkbook workbook) {
            #region #SelectComparer
            Worksheet worksheet = workbook.Worksheets[0];

            // Fill in the range with data.
            worksheet.Cells["A2"].Value = 0.7;
            worksheet.Cells["A3"].Value = 0.45;
            worksheet.Cells["A4"].Value = 0.53;
            worksheet.Cells["A5"].Value = 0.33;
            worksheet.Cells["A6"].Value = 0.99;
            worksheet.Cells["A7"].Value = 0.62;

            // Specify a built-in comparer. 
            IComparer<CellValue> comparer = worksheet.Comparers.Descending;

            // Sort values using the comparer.
            Range range = worksheet.Range["A2:A7"];
            worksheet.Sort(range, 0, comparer);

            // Create a heading.
            Range header = worksheet.Range["A1"];
            header[0].Value = "Values sorted by selected comparer";
            header.ColumnWidthInCharacters = 40;
            header.Style = workbook.Styles["Header"];
            #endregion #SelectComparer
        }

        static void SortBySpecifiedColumn(IWorkbook workbook) {
            #region #SortBySpecifiedColumn
            Worksheet worksheet = workbook.Worksheets["SortSample"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Sort by a column with offset = 6  within the range being sorted.
            // Use ascending order.
            Range range = worksheet.Range["B5:H18"];
            worksheet.Sort(range, 6);

            worksheet["B2"].Value = "Table is sorted by markup column using ascending order.";
            worksheet.Visible = true;
            #endregion #SortBySpecifiedColumn
        }

        static void SortByMultipleColumns(IWorkbook workbook) {
            #region #SortByMultipleColumns
            Worksheet worksheet = workbook.Worksheets["SortSample"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Create sorting fields.
            List<SortField> fields = new List<SortField>();
            
            // First sorting field. Author column (offset = 0) will be sorted using ascending order.
            SortField sortField1 = new SortField();
            sortField1.ColumnOffset = 0;
            sortField1.Comparer = worksheet.Comparers.Ascending;
            fields.Add(sortField1);

            // Second sorting field. Title column (offset = 1) will be sorted using ascending order.
            SortField sortField2 = new SortField();
            sortField2.ColumnOffset = 1;
            sortField2.Comparer = worksheet.Comparers.Ascending;
            fields.Add(sortField2);

            // Sort the range by sorting fields.
            Range range = worksheet.Range["B5:H18"];
            worksheet.Sort(range, fields);

            // Add a note.
            worksheet["B2"].Value = "Table is sorted by two columns: by author, then by title in ascending order.";
            worksheet.Visible = true;
            #endregion #SortByMultipleColumns
        }
    }
}
