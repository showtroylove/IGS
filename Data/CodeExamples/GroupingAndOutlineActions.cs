using System;
using System.Collections.Generic;
using System.Drawing;
using DevExpress.Spreadsheet;

namespace SpreadsheetExamples {
    public static class GroupingAndOutlineActions {

        static void GroupRows(IWorkbook workbook) {
            #region #GroupRows
            Worksheet worksheet = workbook.Worksheets["Grouping"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Group rows and collapse.
            worksheet.Rows.Group(2, 5, true);

            // Group rows.
            worksheet.Rows.Group(8, 11, false);

            #endregion #GroupRows
        }

        static void GroupColumns(IWorkbook workbook) {
            #region #GroupColumns
            Worksheet worksheet = workbook.Worksheets["Grouping"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Group columns.
            worksheet.Columns.Group(2, 5, false);

            #endregion #GroupColumns
        }

        static void UngroupRows(IWorkbook workbook) {
            #region #UngroupRows
            Worksheet worksheet = workbook.Worksheets["Grouping and outline"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Ungroup rows and expand.
            worksheet.Rows.UnGroup(2, 5, true);

            // Ungroup rows.
            worksheet.Rows.UnGroup(8, 11, false);
            worksheet.Rows.UnGroup(1, 12, false);

            #endregion #UngroupRows
        }

        static void UngroupColumns(IWorkbook workbook) {
            #region #UngroupColumns
            Worksheet worksheet = workbook.Worksheets["Grouping and outline"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Ungroup columns.
            worksheet.Columns.UnGroup(2, 5, false);

            #endregion #UngroupColumns
        }

        static void AutoOutline(IWorkbook workbook) {
            #region #AutoOutline
            Worksheet worksheet = workbook.Worksheets["Grouping"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Outline the data automatically.
            worksheet.AutoOutline();

            #endregion #AutoOutline
        }

        static void Subtotal(IWorkbook workbook) {
            #region #Subtotal
            Worksheet worksheet = workbook.Worksheets["Regional sales"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            Range range = worksheet["B3:D9"];
            List<int> subtotalColumnList = new List<int>();
            subtotalColumnList.Add(3);
            worksheet.Subtotal(range, 1, subtotalColumnList, 9, "Total");

            #endregion #Subtotal
        }
    }
}