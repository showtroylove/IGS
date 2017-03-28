using System;
using System.Collections.Generic;
using System.Drawing;
using DevExpress.Spreadsheet;

namespace SpreadsheetExamples {
    public static class AutoFilterActions {

        static void ApplyFilter(IWorkbook workbook) {
            #region #ApplyFilter
            Worksheet worksheet = workbook.Worksheets["Regional sales"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Apply AutoFilter to a worksheet range.
            Range range = worksheet["B2:D9"];
            worksheet.AutoFilter.Apply(range);

            #endregion #ApplyFilter
        }

        static void SortBySingleColumn(IWorkbook workbook) {
            #region #SortBySingleColumn
            Worksheet worksheet = workbook.Worksheets["Regional sales"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Apply AutoFilter to a worksheet range.
            Range range = worksheet["B2:D9"];
            worksheet.AutoFilter.Apply(range);

            // Sort in descending order by the first column.
            worksheet.AutoFilter.SortState.Sort(0, true);

            #endregion #SortBySingleColumn
        }

        static void SortByMultipleColumns(IWorkbook workbook) {
            #region #SortByMultipleColumns
            Worksheet worksheet = workbook.Worksheets["Regional sales"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Apply AutoFilter to a worksheet range.
            Range range = worksheet["B2:D9"];
            worksheet.AutoFilter.Apply(range);

            // Sort in descending order by the first and third columns.
            List<SortCondition> sortConditions = new List<SortCondition>();
            sortConditions.Add(new SortCondition(0, true));
            sortConditions.Add(new SortCondition(2, true));
            worksheet.AutoFilter.SortState.Sort(sortConditions);

            #endregion #SortByMultipleColumns
        }

        static void FilterByCondition(IWorkbook workbook) {
            #region #FilterByCondition
            Worksheet worksheet = workbook.Worksheets["Regional sales"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Apply AutoFilter to a worksheet range.
            Range range = worksheet["B2:D9"];
            worksheet.AutoFilter.Apply(range);

            // Filter the data based on the condition SALES > 5000.
            worksheet.AutoFilter.Columns[2].ApplyCustomFilter(5000, FilterComparisonOperator.GreaterThan);

            #endregion #FilterByCondition
        }

        static void FilterByValue(IWorkbook workbook) {
            #region #FilterByValue
            Worksheet worksheet = workbook.Worksheets["Regional sales"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Apply AutoFilter to a worksheet range.
            Range range = worksheet["B2:D9"];
            worksheet.AutoFilter.Apply(range);

            // Filter the data by a specific value.
            worksheet.AutoFilter.Columns[1].ApplyFilterCriteria("Mozzarella di Giovanni");

            #endregion #FilterByValue
        }

        static void FilterByValues(IWorkbook workbook) {
            #region #FilterByValues
            Worksheet worksheet = workbook.Worksheets["Regional sales"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Apply AutoFilter to worksheet range
            Range range = worksheet["B2:D9"];
            worksheet.AutoFilter.Apply(range);

            // Filter the data by an array of values.
            worksheet.AutoFilter.Columns[1].ApplyFilterCriteria(new CellValue[] { "Mozzarella di Giovanni", "Gorgonzola Telino" });

            #endregion #FilterByValues
        }

        static void DynamicFilter(IWorkbook workbook) {
            #region #DynamicFilter
            Worksheet worksheet = workbook.Worksheets["Regional sales"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Apply AutoFilter to a worksheet range.
            Range range = worksheet["B2:D9"];
            worksheet.AutoFilter.Apply(range);

            // Filter the data using dynamic criteria.
            worksheet.AutoFilter.Columns[2].ApplyDynamicFilter(DynamicFilterType.AboveAverage);

            #endregion #DynamicFilter
        }

        static void ReapplyFilter(IWorkbook workbook) {
            #region #ReapplyFilter
            Worksheet worksheet = workbook.Worksheets["Regional sales"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Apply AutoFilter to a worksheet range.
            Range range = worksheet["B2:D9"];
            worksheet.AutoFilter.Apply(range);

            // Filter the data based on the condition SALES > 5000.
            worksheet.AutoFilter.Columns[2].ApplyCustomFilter(5000, FilterComparisonOperator.GreaterThan);

            // Change the data and reapply AutoFilter.
            worksheet["D3"].Value = 5000;
            worksheet.AutoFilter.ReApply();

            #endregion #ReapplyFilter
        }

        static void ClearFilter(IWorkbook workbook) {
            #region #ClearFilter
            Worksheet worksheet = workbook.Worksheets["Regional sales"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Apply AutoFilter to a worksheet range.
            Range range = worksheet["B2:D9"];
            worksheet.AutoFilter.Apply(range);

            // Filter the data based on the condition SALES > 5000.
            worksheet.AutoFilter.Columns[2].ApplyCustomFilter(5000, FilterComparisonOperator.GreaterThan);

            // Clear the filter.
            worksheet.AutoFilter.Clear();

            #endregion #ClearFilter
        }

        static void DisableFilter(IWorkbook workbook) {
            #region #DisableFilter
            Worksheet worksheet = workbook.Worksheets["Regional sales"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Apply AutoFilter to a worksheet range.
            Range range = worksheet["B2:D9"];
            worksheet.AutoFilter.Apply(range);

            // Disable AutoFilter for the worksheet.
            worksheet.AutoFilter.Disable();

            #endregion #DisableFilter
        }
    }
}