using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using DevExpress.Export.Xl;
using DevExpress.XtraExport.Csv;
using DevExpress.Spreadsheet;

namespace XLExportExamples {
    public static class Pictures {

        static void InsertPicture(Stream stream, XlDocumentFormat documentFormat, string imagesPath) {
            #region #InsertPicture
            // Create an exporter instance
            IXlExporter exporter = XlExport.CreateExporter(documentFormat);

            // Create a new document
            using(IXlDocument document = exporter.CreateDocument(stream)) {
                document.Options.Culture = CultureInfo.CurrentCulture;

                // Create a worksheet
                using(IXlSheet sheet = document.CreateSheet()) {

                    // Insert a picture from a file and anchor it to cells
                    using(IXlPicture picture = sheet.CreatePicture()) {
                        picture.Image = Image.FromFile(Path.Combine(imagesPath, "image1.jpg"));
                        // Set two-cell anchor with "Move and size with cells" positioning
                        picture.SetTwoCellAnchor(new XlAnchorPoint(1, 1, 0, 0), new XlAnchorPoint(6, 11, 2, 15), XlAnchorType.TwoCell);
                    }
                }
            }

            #endregion #InsertPicture
        }

        static void StretchPicture(Stream stream, XlDocumentFormat documentFormat, string imagesPath) {
            #region #StretchPicture
            // Create an exporter instance
            IXlExporter exporter = XlExport.CreateExporter(documentFormat);

            // Create a new document
            using(IXlDocument document = exporter.CreateDocument(stream)) {
                document.Options.Culture = CultureInfo.CurrentCulture;

                // Create a worksheet
                using(IXlSheet sheet = document.CreateSheet()) {
                    sheet.SkipColumns(1);
                    using(IXlColumn column = sheet.CreateColumn()) {
                        column.WidthInPixels = 205;
                    }
                    sheet.SkipRows(1);
                    using(IXlRow row = sheet.CreateRow()) {
                        row.HeightInPixels = 154;
                    }

                    // Insert a picture from a file and stretch it to fill the cell B2
                    using(IXlPicture picture = sheet.CreatePicture()) {
                        picture.Image = Image.FromFile(Path.Combine(imagesPath, "image1.jpg"));
                        picture.StretchToCell(new XlCellPosition(1, 1)); // B2
                    }
                }
            }

            #endregion #StretchPicture
        }

        static void FitPicture(Stream stream, XlDocumentFormat documentFormat, string imagesPath) {
            #region #FitPicture
            // Create an exporter instance
            IXlExporter exporter = XlExport.CreateExporter(documentFormat);

            // Create a new document
            using(IXlDocument document = exporter.CreateDocument(stream)) {
                document.Options.Culture = CultureInfo.CurrentCulture;

                // Create a worksheet
                using(IXlSheet sheet = document.CreateSheet()) {
                    sheet.SkipColumns(1);
                    using(IXlColumn column = sheet.CreateColumn()) {
                        column.WidthInPixels = 300;
                    }
                    sheet.SkipRows(1);
                    using(IXlRow row = sheet.CreateRow()) {
                        row.HeightInPixels = 154;
                    }

                    // Insert a picture from a file to fit in the cell B2
                    using(IXlPicture picture = sheet.CreatePicture()) {
                        picture.Image = Image.FromFile(Path.Combine(imagesPath, "image1.jpg"));
                        picture.FitToCell(new XlCellPosition(1, 1), 300, 154, true);
                    }
                }
            }

            #endregion #FitPicture
        }

        static void PictureHyperlinkClick(Stream stream, XlDocumentFormat documentFormat, string imagesPath) {
            #region #HyperlinkClick
            // Create an exporter instance
            IXlExporter exporter = XlExport.CreateExporter(documentFormat);

            // Create a new document
            using(IXlDocument document = exporter.CreateDocument(stream)) {
                document.Options.Culture = CultureInfo.CurrentCulture;

                // Create a worksheet
                using(IXlSheet sheet = document.CreateSheet()) {

                    // Load a picture from a file and add a hyperlink to it
                    using(IXlPicture picture = sheet.CreatePicture()) {
                        picture.Image = Image.FromFile(Path.Combine(imagesPath, "DevExpress.png"));
                        picture.HyperlinkClick.TargetUri = "http://www.devexpress.com";
                        picture.HyperlinkClick.Tooltip = "Developer Express Inc.";
                        picture.SetTwoCellAnchor(new XlAnchorPoint(1, 1, 0, 0), new XlAnchorPoint(10, 5, 2, 15), XlAnchorType.TwoCell);
                    }
                }
            }

            #endregion #HyperlinkClick
        }

    }
}