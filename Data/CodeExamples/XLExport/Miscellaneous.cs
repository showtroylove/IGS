﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using DevExpress.Export.Xl;
using DevExpress.XtraExport.Csv;
using DevExpress.Spreadsheet;

namespace XLExportExamples {
    public static class Miscellaneous {

        static void Hyperlinks(Stream stream, XlDocumentFormat documentFormat) {
            #region #Hyperlinks
            // Create an exporter instance
            IXlExporter exporter = XlExport.CreateExporter(documentFormat);

            // Create a new document
            using(IXlDocument document = exporter.CreateDocument(stream)) {
                document.Options.Culture = CultureInfo.CurrentCulture;

                // Create a worksheet
                using(IXlSheet sheet = document.CreateSheet()) {
                    using(IXlColumn column = sheet.CreateColumn()) {
                        column.WidthInPixels = 300;
                    }

                    // Create a hyperlink to a cell in the current workbook
                    using(IXlRow row = sheet.CreateRow()) {
                        using(IXlCell cell = row.CreateCell()) {
                            cell.Value = "Local link";
                            cell.Formatting = XlCellFormatting.Hyperlink;
                            XlHyperlink hyperlink = new XlHyperlink();
                            hyperlink.Reference = new XlCellRange(new XlCellPosition(cell.ColumnIndex, cell.RowIndex));
                            hyperlink.TargetUri = "#Sheet1!C5";
                            sheet.Hyperlinks.Add(hyperlink);
                        }
                    }

                    // Create a hyperlink to a cell located in the external workbook
                    using(IXlRow row = sheet.CreateRow()) {
                        using(IXlCell cell = row.CreateCell()) {
                            cell.Value = "External file link";
                            cell.Formatting = XlCellFormatting.Hyperlink;
                            XlHyperlink hyperlink = new XlHyperlink();
                            hyperlink.Reference = new XlCellRange(new XlCellPosition(cell.ColumnIndex, cell.RowIndex));
                            hyperlink.TargetUri = "linked.xlsx#Sheet1!C5";
                            sheet.Hyperlinks.Add(hyperlink);
                        }
                    }

                    // Create a hyperlink to a web page
                    using(IXlRow row = sheet.CreateRow()) {
                        using(IXlCell cell = row.CreateCell()) {
                            cell.Value = "External uri";
                            cell.Formatting = XlCellFormatting.Hyperlink;
                            XlHyperlink hyperlink = new XlHyperlink();
                            hyperlink.Reference = new XlCellRange(new XlCellPosition(cell.ColumnIndex, cell.RowIndex));
                            hyperlink.TargetUri = "http://www.devexpress.com";
                            sheet.Hyperlinks.Add(hyperlink);
                        }
                    }
                }
            }

            #endregion #Hyperlinks
        }

        static void DocumentProperties(Stream stream, XlDocumentFormat documentFormat) {
            #region #DocumentProperties
            // Create an exporter instance
            IXlExporter exporter = XlExport.CreateExporter(documentFormat);

            // Create a new document
            using(IXlDocument document = exporter.CreateDocument(stream)) {
                document.Options.Culture = CultureInfo.CurrentCulture;

                // Set the built-in document properties
                document.Properties.Title = "Sample document";
                document.Properties.Subject = "XL Export API demo";
                document.Properties.Keywords = "XL export document generation";
                document.Properties.Description = "Generate through XL Export API";
                document.Properties.Category = "Spreadsheet";
                document.Properties.Company = "DevExpress Inc.";

                // Set the custom properties
                document.Properties.Custom["Product Suite"] = "Spreadsheet Document Automation";
                document.Properties.Custom["Revision"] = 5;
                document.Properties.Custom["Date Completed"] = DateTime.Now;
                document.Properties.Custom["Published"] = true;

                // Generate data for the document
                using(IXlSheet sheet = document.CreateSheet()) {
                    sheet.SkipRows(1);
                    using(IXlRow row = sheet.CreateRow()) {
                        row.SkipCells(1);
                        using(IXlCell cell = row.CreateCell()) {
                            cell.Value = "You can check exported document properties using File/Info/Advanced Properties dialog box.";
                        }
                    }
                }
            }

            #endregion #DocumentProperties
        }

        static void DocumentOptions(Stream stream, XlDocumentFormat documentFormat) {
            #region #DocumentOptions
            // Create an exporter instance
            IXlExporter exporter = XlExport.CreateExporter(documentFormat);

            // Create a new document
            using(IXlDocument document = exporter.CreateDocument(stream)) {
                document.Options.Culture = CultureInfo.CurrentCulture;

                // Create a worksheet
                using(IXlSheet sheet = document.CreateSheet()) {
                    using(IXlColumn column = sheet.CreateColumn()) {
                        column.WidthInPixels = 200;
                    }
                    using(IXlColumn column = sheet.CreateColumn()) {
                        column.Formatting = XlCellAlignment.FromHV(XlHorizontalAlignment.Center, XlVerticalAlignment.Bottom);
                    }

                    // Display the file format to which the document is exported
                    using(IXlRow row = sheet.CreateRow()) {
                        using(IXlCell cell = row.CreateCell()) {
                            cell.Value = "Document format:";
                        }
                        using(IXlCell cell = row.CreateCell()) {
                            cell.Value = document.Options.DocumentFormat.ToString().ToUpper();
                        }
                    }

                    // Display the maximum number of columns allowed by the output file format
                    using(IXlRow row = sheet.CreateRow()) {
                        using(IXlCell cell = row.CreateCell()) {
                            cell.Value = "Max column count:";
                        }
                        using(IXlCell cell = row.CreateCell()) {
                            cell.Value = document.Options.MaxColumnCount;
                        }
                    }

                    // Display the maximum number of rows allowed by the output file format
                    using(IXlRow row = sheet.CreateRow()) {
                        using(IXlCell cell = row.CreateCell()) {
                            cell.Value = "Max row count:";
                        }
                        using(IXlCell cell = row.CreateCell()) {
                            cell.Value = document.Options.MaxRowCount;
                        }
                    }

                    // Display whether the document can contain multiple worksheets
                    using(IXlRow row = sheet.CreateRow()) {
                        using(IXlCell cell = row.CreateCell()) {
                            cell.Value = "Supports document parts:";
                        }
                        using(IXlCell cell = row.CreateCell()) {
                            cell.Value = document.Options.SupportsDocumentParts;
                        }
                    }

                    // Display whether the document can contain formulas
                    using(IXlRow row = sheet.CreateRow()) {
                        using(IXlCell cell = row.CreateCell()) {
                            cell.Value = "Supports formulas:";
                        }
                        using(IXlCell cell = row.CreateCell()) {
                            cell.Value = document.Options.SupportsFormulas;
                        }
                    }

                    // Display whether the document supports grouping functionality
                    using(IXlRow row = sheet.CreateRow()) {
                        using(IXlCell cell = row.CreateCell()) {
                            cell.Value = "Supports outline/grouping:";
                        }
                        using(IXlCell cell = row.CreateCell()) {
                            cell.Value = document.Options.SupportsOutlineGrouping;
                        }
                    }
                }
            }

            #endregion #DocumentOptions
        }

        static void CsvExportOptions(Stream stream, XlDocumentFormat documentFormat) {
            #region #CsvOptions
            // Create an exporter instance
            IXlExporter exporter = XlExport.CreateExporter(documentFormat);

            // Create a new document
            using(IXlDocument document = exporter.CreateDocument(stream)) {
                document.Options.Culture = CultureInfo.CurrentCulture;

                // Specify options for exporting the document to CSV format
                CsvDataAwareExporterOptions csvOptions = document.Options as CsvDataAwareExporterOptions;
                if(csvOptions != null) {
                    // Set the encoding of the text-based file to which the workbook is exported
                    csvOptions.Encoding = Encoding.UTF8;
                    // Write a preamble that specifies the encoding used
                    csvOptions.WritePreamble = true;
                    // Convert a cell value to a string by using the current culture's format string (instead of specified cell number formats) 
                    csvOptions.UseCellNumberFormat = false;
                    // Insert the newline character after the last row of the resulting text
                    csvOptions.NewlineAfterLastRow = true;
                }

                // Generate data for the document
                using(IXlSheet sheet = document.CreateSheet()) {
                    using(IXlRow row = sheet.CreateRow()) {
                        using(IXlCell cell = row.CreateCell()) {
                            cell.Value = "Product";
                        }
                        for(int i = 0; i < 4; i++) {
                            using(IXlCell cell = row.CreateCell()) {
                                cell.Value = string.Format("Q{0}", i + 1);
                            }
                        }
                    }
                    string[] products = new string[] { 
                        "Camembert Pierrot", "Gorgonzola Telino", "Mascarpone Fabioli", "Mozzarella di Giovanni",
                        "Gnocchi di nonna Alice", "Gudbrandsdalsost", "Gustaf's Knäckebröd", "Queso Cabrales",
                        "Queso Manchego La Pastora", "Raclette Courdavault", "Singaporean Hokkien Fried Mee", "Wimmers gute Semmelknödel"
                    };
                    Random random = new Random();
                    for(int i = 0; i < 12; i++) {
                        using(IXlRow row = sheet.CreateRow()) {
                            using(IXlCell cell = row.CreateCell()) {
                                cell.Value = products[i];
                            }
                            for(int j = 0; j < 4; j++) {
                                using(IXlCell cell = row.CreateCell()) {
                                    cell.Value = Math.Round(random.NextDouble() * 2000 + 3000);
                                }
                            }
                        }
                    }
                }
            }

            #endregion #CsvOptions
        }

    }
}