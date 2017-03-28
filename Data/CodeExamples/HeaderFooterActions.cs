using System;
using System.IO;
using DevExpress.Spreadsheet;

namespace SpreadsheetExamples {
    public static class HeaderFooterActions {
        static void AddHeaderFooter(IWorkbook workbook) {
            #region #AddHeaderFooter
            Worksheet worksheet = workbook.Worksheets["HeaderFooter"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            WorksheetHeaderFooterOptions headerFooter = worksheet.HeaderFooterOptions;

            // Add headers to first page
            headerFooter.DifferentFirst = true;
            headerFooter.FirstHeader.Left = "File path: " + HeaderFooterCode.WorkbookFilePath + HeaderFooterCode.WorkbookFileName + ".xlsx";
            headerFooter.FirstHeader.Right = "Total number of pages: " + HeaderFooterCode.PageTotal;

            // Add footers to first page, using FromLCR method
            string leftFooter = "Current date: " + HeaderFooterCode.Date;
            string centerFooter = "Current time: " + HeaderFooterCode.Time;
            string rightFooter = "First page";
            headerFooter.FirstFooter.FromLCR(leftFooter, centerFooter, rightFooter);

            // Add header to even pages
            headerFooter.DifferentOddEven = true;
            headerFooter.EvenHeader.Right = "This page number is even: " + HeaderFooterCode.PageNumber;

            // Add footer to odd pages, using FromString method
            string oddPageFooter = HeaderFooterCode.RightSection + "This page number is odd: " + HeaderFooterCode.PageNumber;
            headerFooter.OddFooter.FromString(oddPageFooter);
            #endregion #AddHeaderFooter
        }

        static void AddPicture(IWorkbook workbook, string rootPath) {
            #region #AddPicture
            Worksheet worksheet = workbook.Worksheets["HeaderFooter"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            WorksheetHeaderFooter oddHeaderFooter = worksheet.HeaderFooterOptions.OddHeader;
            
            // Add a picture to center header
            string filePath = Path.Combine(rootPath + "\\DevExpress.png");
            oddHeaderFooter.AddPicture(filePath, HeaderFooterSection.Center);

            // Change width to fit picture
            oddHeaderFooter.CenterPicture.Width = 500;
            #endregion #AddPicture
        }

        static void RemovePicture(IWorkbook workbook, string rootPath) {
            #region #RemovePicture
            Worksheet worksheet = workbook.Worksheets["HeaderFooter"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            WorksheetHeaderFooter oddHeaderFooter = worksheet.HeaderFooterOptions.OddHeader;

            // Add a picture to center header
            string filePath = Path.Combine(rootPath + "\\DevExpress.png");
            oddHeaderFooter.AddPicture(filePath, HeaderFooterSection.Center);
            oddHeaderFooter.CenterPicture.Width = 500;

            // Remove picture from center header
            oddHeaderFooter.RemovePicture(HeaderFooterSection.Center);
            #endregion #RemovePicture
        }

        static void FormatPicture(IWorkbook workbook, string rootPath) {
            #region #FormatPicture
            Worksheet worksheet = workbook.Worksheets["HeaderFooter"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            WorksheetHeaderFooter oddHeaderFooter = worksheet.HeaderFooterOptions.OddHeader;

            // Add a picture to center header
            string filePath = Path.Combine(rootPath + "\\DevExpress.png");
            HeaderFooterPicture picture = oddHeaderFooter.AddPicture(filePath, HeaderFooterSection.Center);

            // Change sizes
            picture.LockAspectRatio = false;
            picture.Width = 500;
            picture.Height = 80;

            // Apply crop
            picture.CropLeft = 10;
            picture.CropRight = 2100;
            picture.CropTop = 10;
            picture.CropBottom = 50;
            #endregion #FormatPicture
        }
    }
}
