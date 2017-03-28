using System;
using DevExpress.Spreadsheet;
using DevExpress.Utils;
using System.Drawing;
using System.Globalization;
using System.Collections.Generic;


namespace SpreadsheetExamples {
    public static class SearchActions {
        static void SimpleSearch(IWorkbook workbook) {
            #region #SimpleSearch
            Worksheet worksheet = workbook.Worksheets["SearchSample"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            worksheet.Visible = true;

            // Find and highlight cells, containing text "the".
            IEnumerable<Cell> foundCells = worksheet.Search("the");
            foreach (Cell cell in foundCells)
                cell.Fill.BackgroundColor = Color.Yellow;
            #endregion #SimpleSearch
        }

        static void SearchWithOptions(IWorkbook workbook) {
            #region #SearchWithOptions
            Worksheet worksheet = workbook.Worksheets["SearchSample"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            worksheet.Visible = true;

            // Find and highlight cells, containing case-sensitive text "The".
            SearchOptions options = new SearchOptions();
            options.MatchCase = true;
            IEnumerable<Cell> foundCells = worksheet.Search("The", options);
            foreach (Cell cell in foundCells)
                cell.Fill.BackgroundColor = Color.Yellow;
            #endregion #SearchWithOptions
        }
    }
}
