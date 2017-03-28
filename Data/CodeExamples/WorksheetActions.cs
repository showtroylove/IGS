using System;
using DevExpress.Spreadsheet;

namespace SpreadsheetExamples {

    public static class WorksheetActions {
        static void AssignActiveWorksheet(IWorkbook workbook) {
            #region ActiveWorksheet
            // Set the second worksheet under the "Sheet2" name as active.
            workbook.Worksheets.ActiveWorksheet = workbook.Worksheets["Sheet2"];
            #endregion ActiveWorksheet
        }

        static void AddWorksheet(IWorkbook workbook) {
            #region AddWorksheet
            // Add a new worksheet to the workbook. The worksheet will be inserted into the end of the existing worksheet collection
            // under the name "SheetN", where N is a number following the largest number used in worksheet names in the previously existing collection.
            workbook.Worksheets.Add();

            // Add a new worksheet under the specified name.
            workbook.Worksheets.Add().Name = "TestSheet1";

            workbook.Worksheets.Add("TestSheet2");

            // Add a new workbook to the specified position in the collection of worksheets.
            workbook.Worksheets.Insert(1, "TestSheet3");

            workbook.Worksheets.Insert(3);

            #endregion AddWorksheet
        }

        static void RemoveWorksheet(IWorkbook workbook) {
            #region DeleteWorksheet
            // By default, a new IWorkbook object is created with three worksheets ("Sheet1", "Sheet2", "Sheet3").
            // Delete the second default worksheet under the "Sheet2" name from the workbook.
            workbook.Worksheets.Remove(workbook.Worksheets["Sheet2"]);

            // Delete the first worksheet using its index in the collection of worksheets.
            workbook.Worksheets.RemoveAt(0);

            Worksheet lastWorksheet = workbook.Worksheets.ActiveWorksheet;
            Range range = lastWorksheet.Range["A1:B3"];
            range[0].Value = "Sheets: ";
            range[1].Value = workbook.Worksheets.Count;
            range[2].Value = "Name:";
            range[3].Value = lastWorksheet.Name;

            #endregion DeleteWorksheet
        }

        static void RenameWorksheet(IWorkbook workbook) {
            #region RenameWorksheet
            Worksheet sheet2 = workbook.Worksheets[1];
            // Change the name of the second worksheet in the collection of worksheets.
            sheet2.Name = "Renamed Sheet";
            #endregion RenameWorksheet
        }

        static void CopyWorksheetWithinWorkbook(IWorkbook workbook) {
            // TODO
        }

        static void CopyWorksheetBetweenWorkbooks(IWorkbook workbook) {
            // TODO
        }

        static void MoveWorksheet(IWorkbook workbook) {
            #region MoveWorksheet
            // Move the first worksheet to the position of the last worksheet within the workbook.
            workbook.Worksheets[0].Move(workbook.Worksheets.Count - 1);
            #endregion MoveWorksheet
        }

        static void ShowHideWorksheet(IWorkbook workbook) {
            #region ShowHideWorksheet
            // Hide the worksheet under the "Sheet2" name and prevent end-users from unhiding it via Excel interface.
            // To make this worksheet visible again, use the Worksheet.Visible property.
            workbook.Worksheets["Sheet2"].VisibilityType = WorksheetVisibilityType.VeryHidden;

            // Hide the worksheet under the "Sheet3" name. 
            // In this state a worksheet can be unhidden via Excel interface.
            workbook.Worksheets["Sheet3"].Visible = false;
            #endregion ShowHideWorksheet
        }

        static void ShowHideGridlines(IWorkbook workbook) {
            #region ShowHideGridlines
            // Hide gridlines on the first worksheet.
            workbook.Worksheets[0].ActiveView.ShowGridlines = false;
            #endregion ShowHideGridlines
        }

        static void ShowHideHeadings(IWorkbook workbook) {
            #region ShowHideHeadings
            // Hide row and column headings on the first worksheet.
            workbook.Worksheets[0].ActiveView.ShowHeadings = false;
            #endregion ShowHideHeadings
        }

        static void SetPageOrientation(IWorkbook workbook) {
            #region PageOrientation
            // Set the page orientation to Landscape.
            workbook.Worksheets[0].ActiveView.Orientation = PageOrientation.Landscape;

            #endregion PageOrientation
        }

        static void SetPageMargins(IWorkbook workbook) {
            #region PageMargins
            // Select a unit of measure used within the workbook.
            workbook.Unit = DevExpress.Office.DocumentUnit.Centimeter;

            // Access page margins.
            Margins pageMargins = workbook.Worksheets[0].ActiveView.Margins;

            // Specify page margins.
            pageMargins.Left = 2;
            pageMargins.Top = 3;
            pageMargins.Right = 1;
            pageMargins.Bottom = 2;
            
            // Specify header and footer margins.
            pageMargins.Header = 2;
            pageMargins.Footer = 1;
            #endregion PageMargins
        }

        static void SetPaperSize(IWorkbook workbook) {
            #region PaperSize
            // Select the page's paper size.
            workbook.Worksheets[0].ActiveView.PaperKind = System.Drawing.Printing.PaperKind.A4;
            #endregion PaperSize
        }

        static void ZoomWorksheet(IWorkbook workbook) {
            #region WorksheetZoom
            // Zoom out the worksheet view. 
            workbook.Worksheets[0].ActiveView.Zoom = 50;

            #endregion WorksheetZoom
        }

    }
}
