using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using DevExpress.Export.Xl;
using DevExpress.Spreadsheet;

namespace XLExportExamples {
    public static class BasicActions {

        static void CreateDocument(Stream stream, XlDocumentFormat documentFormat) {
            #region #CreateDocument
            // Create an exporter instance
            IXlExporter exporter = XlExport.CreateExporter(documentFormat);

            // Create a new document and write it to the specified stream
            using(IXlDocument document = exporter.CreateDocument(stream)) {

                // Specify the document culture
                document.Options.Culture = CultureInfo.CurrentCulture;
            }

            #endregion #CreateDocument
        }

        static void CreateSheet(Stream stream, XlDocumentFormat documentFormat) {
            #region #CreateSheet
            // Create an exporter instance
            IXlExporter exporter = XlExport.CreateExporter(documentFormat);
            
            // Create a new document
            using(IXlDocument document = exporter.CreateDocument(stream)) {

                // Specify the document culture
                document.Options.Culture = CultureInfo.CurrentCulture;

                // Create a new worksheet under the specified name
                using(IXlSheet sheet = document.CreateSheet()) {
                    sheet.Name = "Sales report";
                }
            }

            #endregion #CreateSheet
        }

        static void CreateHiddenSheet(Stream stream, XlDocumentFormat documentFormat) {
            #region #CreateHiddenSheet
            // Create an exporter instance
            IXlExporter exporter = XlExport.CreateExporter(documentFormat);

            // Create a new document
            using(IXlDocument document = exporter.CreateDocument(stream)) {

                // Specify the document culture
                document.Options.Culture = CultureInfo.CurrentCulture;

                // Create the first worksheet
                using(IXlSheet sheet = document.CreateSheet()) {
                    sheet.Name = "Sales report";
                }

                // Create the second worksheet and setup its visibility
                using(IXlSheet sheet = document.CreateSheet()) {
                    sheet.Name = "Data";
                    sheet.VisibleState = XlSheetVisibleState.Hidden;
                }
            }

            #endregion #CreateHiddenSheet
        }

        static void CreateColumns(Stream stream, XlDocumentFormat documentFormat) {
            #region #CreateColumns
            // Create an exporter instance
            IXlExporter exporter = XlExport.CreateExporter(documentFormat);
            
            // Create a new document
            using(IXlDocument document = exporter.CreateDocument(stream)) {

                // Specify the document culture
                document.Options.Culture = CultureInfo.CurrentCulture;

                // Create a worksheet
                using(IXlSheet sheet = document.CreateSheet()) {

                    // Create the column A and set its width to 100 pixels
                    using(IXlColumn column = sheet.CreateColumn()) {
                        column.WidthInPixels = 100;
                    }

                    // Hide the column B in the worksheet
                    using(IXlColumn column = sheet.CreateColumn()) {
                        column.IsHidden = true;
                    }

                    // Create the column D and set its width to 24.5 characters
                    using(IXlColumn column = sheet.CreateColumn(3)) {
                        column.WidthInCharacters = 24.5f;
                    }
                }
            }

            #endregion #CreateColumns
        }

        static void CreateRows(Stream stream, XlDocumentFormat documentFormat) {
            #region #CreateRows
            // Create an exporter instance
            IXlExporter exporter = XlExport.CreateExporter(documentFormat);

            // Create a new document
            using(IXlDocument document = exporter.CreateDocument(stream)) {

                // Specify the document culture
                document.Options.Culture = CultureInfo.CurrentCulture;

                // Create a worksheet
                using(IXlSheet sheet = document.CreateSheet()) {

                    // Create the first row and set its height to 40 pixels
                    using(IXlRow row = sheet.CreateRow()) {
                        row.HeightInPixels = 40;
                    }

                    // Hide the third row in the worksheet
                    using(IXlRow row = sheet.CreateRow(2)) {
                        row.IsHidden = true;
                    }
                }
            }

            #endregion #CreateRows
        }

        static void CreateCells(Stream stream, XlDocumentFormat documentFormat) {
            #region #CreateCells
            // Create an exporter instance
            IXlExporter exporter = XlExport.CreateExporter(documentFormat);

            // Create a new document
            using(IXlDocument document = exporter.CreateDocument(stream)) {

                // Specify the document culture
                document.Options.Culture = CultureInfo.CurrentCulture;

                // Create a worksheet
                using(IXlSheet sheet = document.CreateSheet()) {

                    // Create the column A and set its width
                    using(IXlColumn column = sheet.CreateColumn()) {
                        column.WidthInPixels = 150;
                    }

                    // Create the first row
                    using(IXlRow row = sheet.CreateRow()) {

                        // Create the cell A1 and set its value
                        using(IXlCell cell = row.CreateCell()) {
                            cell.Value = "Numeric value:";
                        }

                        // Create the cell B1 and assign the numeric value to it
                        using(IXlCell cell = row.CreateCell()) {
                            cell.Value = 123.45;
                        }
                    }

                    // Create the second row
                    using(IXlRow row = sheet.CreateRow()) {

                        // Create the cell A2 and set its valu
                        using(IXlCell cell = row.CreateCell()) {
                            cell.Value = "Text value:";
                        }

                        // Create the cell B2 and assign the text value to it
                        using(IXlCell cell = row.CreateCell()) {
                            cell.Value = "abc";
                        }
                    }

                    // Create the third row
                    using(IXlRow row = sheet.CreateRow()) {

                        // Create the cell A3 and set its value
                        using(IXlCell cell = row.CreateCell()) {
                            cell.Value = "Boolean value:";
                        }

                        // Create the cell B3 and assign the boolean value to it
                        using(IXlCell cell = row.CreateCell()) {
                            cell.Value = true;
                        }
                    }

                    // Create the fourth row
                    using(IXlRow row = sheet.CreateRow()) {

                        // Create the cell A4 and set its value
                        using(IXlCell cell = row.CreateCell()) {
                            cell.Value = "Error value:";
                        }

                        // Create the cell B4 and assign an error value to it
                        using(IXlCell cell = row.CreateCell()) {
                            cell.Value = XlVariantValue.ErrorName;
                        }
                    }
                }
            }

            #endregion #CreateCells
        }

        static void MergeCells(Stream stream, XlDocumentFormat documentFormat) {
            #region #MergeCells
            // Create an exporter instance
            IXlExporter exporter = XlExport.CreateExporter(documentFormat);

            // Create a new document
            using(IXlDocument document = exporter.CreateDocument(stream)) {
                document.Options.Culture = CultureInfo.CurrentCulture;

                // Create a worksheet
                using(IXlSheet sheet = document.CreateSheet()) {
                    
                    // Create the first row in the worksheet
                    using(IXlRow row = sheet.CreateRow()) {
                        // Create a cell
                        using(IXlCell cell = row.CreateCell()) {
                            // Set the cell value
                            cell.Value = "Merged cells in range A1:E1";
                            // Align the cell content
                            cell.ApplyFormatting(XlCellAlignment.FromHV(XlHorizontalAlignment.Center, XlVerticalAlignment.Center));
                        }
                    }
                    
                    // Create the second row in the worksheet
                    using(IXlRow row = sheet.CreateRow()) {
                        // Create a cell
                        using(IXlCell cell = row.CreateCell()) {
                            // Set the cell value
                            cell.Value = "Merged cells in range A2:A5";
                            // ALign the cell content
                            cell.ApplyFormatting(XlCellAlignment.FromHV(XlHorizontalAlignment.Center, XlVerticalAlignment.Center));
                            // Wrap the text within the cell
                            cell.Formatting.Alignment.WrapText = true;
                        }
                        // Create a cell
                        using(IXlCell cell = row.CreateCell()) {
                            // Set the cell value
                            cell.Value = "Merged cells in range B2:E5";
                            // Align the cell content
                            cell.ApplyFormatting(XlCellAlignment.FromHV(XlHorizontalAlignment.Center, XlVerticalAlignment.Center));
                        }
                    }

                    // Merge cells contained in the range A1:E1
                    sheet.MergedCells.Add(XlCellRange.FromLTRB(0, 0, 4, 0));

                    // Merge cells contained in the range A2:A5
                    sheet.MergedCells.Add(XlCellRange.FromLTRB(0, 1, 0, 4));

                    // Merge cells contained in the range B2:E5
                    sheet.MergedCells.Add(XlCellRange.FromLTRB(1, 1, 4, 4));
                }
            }

            #endregion #MergeCells
        }

    }
}