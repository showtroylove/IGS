using System;
using DevExpress.Spreadsheet;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Control;

namespace SpreadsheetExamples {
    public static class PrintingActions {

        static void Print(IWorkbook workbook) {
            #region WorksheetPrintOptions
            Worksheet worksheet = workbook.Worksheets[0];
            worksheet.Cells["A1"].Value = "Printing Example";
            // Access an object providing print options.
            WorksheetPrintOptions printOptions = workbook.Worksheets[0].PrintOptions;
    
            // TODO
            #endregion WorksheetPrintOptions

            #region PrintWorksheet
            Worksheet firstSheet = workbook.Worksheets[0];
            Table table = firstSheet.Tables.Add(firstSheet["A1:H30"], false);
            table.Style = workbook.TableStyles[BuiltInTableStyleId.TableStyleLight14];
            table.ShowTotals = true;
            table.Columns[0].TotalRowLabel = "Total";
            #endregion PrintWorksheet
        }
        static void Print2(IWorkbook workbook) {
            #region #PrintWorkbook

            Worksheet firstSheet = workbook.Worksheets[0];
            Table table = firstSheet.Tables.Add(firstSheet["A1:H30"], false);
            table.Style = workbook.TableStyles[BuiltInTableStyleId.TableStyleMedium14];
            table.ShowTotals = true;
            table.Columns[0].TotalRowLabel = "Total";

            Worksheet secondSheet = workbook.Worksheets[1];
            Table table2 = secondSheet.Tables.Add(secondSheet["A1:H30"], false);
            table2.Style = workbook.TableStyles[BuiltInTableStyleId.TableStyleDark4];
            table2.ShowTotals = true;
            table2.Columns[0].TotalRowLabel = "Total";

            // Create printing components.
            PrintControl printControl = new PrintControl();
            PrintingSystem printingSystem = new PrintingSystem();
            PrintableComponentLink link = new PrintableComponentLink();

            // Assign a workbook to be printed by the link.
            link.Component = workbook;
            // Add the link to the printing system's collection of links.
            printingSystem.Links.Add(link);
            // Assign the PrintingSystem to the PrintControl.
            printControl.PrintingSystem = printingSystem;

            // Show the Print Preview for the workbook.
            link.ShowPreview();
            #endregion #PrintWorkbook
        }
    }
}
