using System;
using DevExpress.Spreadsheet;
using System.Drawing;
using DevExpress.Utils;

namespace SpreadsheetExamples {
    public static class FormulaActions {
        static void UseConstantsAndCalculationOperatorsInFormulas(IWorkbook workbook) {
            #region #ConstantsAndCalculationOperators
            Worksheet worksheet = workbook.Worksheets[0];
            
            // Use constants and calculation operators in a formula.
            workbook.Worksheets[0].Cells["B2"].Formula = "= (1+5)*6";

            Range header = worksheet.Range["A1:B1"];
            header[0].Value = "Formula";
            header[1].Value = "Value";
            header.FillColor = Color.LightGray;
            header.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
            
            worksheet["A2"].Value = "'" + worksheet["B2"].Formula;
            #endregion #ConstantsAndCalculationOperators
        }

        public static void R1C1ReferencesInFormulas(IWorkbook workbook) {
            #region #R1C1ReferencesInFormulas
            Worksheet worksheet = workbook.Worksheets[0];

            // Switch on the R1C1 reference style in a workbook.
            workbook.DocumentSettings.R1C1ReferenceStyle = true;

            // Specify a formula with relative R1C1 references in cell D2
            // to add values contained in cells A2 through A11.
            worksheet.Cells["D2"].Formula = "=SUM(RC[-3]:R[9]C[-3])";

            // Specify a formula with absolute R1C1 references 
            // to add values contained in cells A2 through A11.
            worksheet.Cells["D3"].Formula = "=SUM(R2C1:R11C1)";

            // Provide static data.
            worksheet.Cells["A1"].Value = "Data";
            worksheet.Range.Parse("R2C1:R11C1", ReferenceStyle.UseDocumentSettings).Formula = "=ROW() - 1";

            worksheet.Cells["B1"].ColumnWidthInCharacters = 30;
            worksheet.Cells["B1"].Value = "Cell Reference Style";
            worksheet.Cells["B2"].Value = "Relative R1C1 Cell Reference";
            worksheet.Cells["B3"].Value = "Absolute R1C1 Cell Reference";

            worksheet.Cells["C1"].ColumnWidthInCharacters = 25;
            worksheet.Cells["C1"].Value = "Formula";
            worksheet.Cells["C2"].Value = "=SUM(RC[-3]:R[9]C[-3])";
            worksheet.Cells["C3"].Value = "=SUM(R2C1:R11C1)";

            worksheet.Cells["D1"].ColumnWidthInCharacters = 25;
            worksheet.Cells["D1"].Value = "Value";
            
            worksheet.Range.Parse("R1C1:R1C4", ReferenceStyle.UseDocumentSettings).FillColor = Color.LightGray;
            worksheet.Range.Parse("R1C1:R11C4", ReferenceStyle.UseDocumentSettings).Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
            #endregion #R1C1ReferencesInFormulas
        }

        static void UseNamesInFormulas(IWorkbook workbook) {
            #region #NamesInFormulas
            Worksheet worksheet = workbook.Worksheets[0];

            // Access the "A2:C5" range of cells in the worksheet.
            Range range = worksheet["A2:C5"];

            // Specify the name for the created range.
            range.Name = "myRange";

            // Create a formula that sums up the values of all cells included in the specified named range.
            worksheet.Cells["F3"].Formula = "= SUM(myRange)";

            Range dataRangeHeader = worksheet.Range["A1:C1"];
            worksheet.MergeCells(dataRangeHeader);
            dataRangeHeader.Value = "myRange:";
            dataRangeHeader.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
            dataRangeHeader.FillColor = Color.LightGray;

            worksheet["A2:C5"].Value = 2;
            worksheet["A2:C5"].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
            worksheet["A2:C5"].Borders.SetOutsideBorders(Color.LightBlue, BorderLineStyle.Medium);

            Range sumHeader = worksheet.Range["E1:F1"];
            worksheet.MergeCells(sumHeader);
            sumHeader.Value = "Sum:";
            sumHeader.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
            sumHeader.FillColor = Color.LightGray;

            worksheet.Range["E2:F2"].ColumnWidthInCharacters = 15;
            worksheet.Cells["E2"].Value = "Formula:";
            worksheet.Cells["E3"].Value = "Value:";
            worksheet.Cells["F2"].Value = "= SUM(myRange)";
            #endregion #NamesInFormulas
        }
        static void CreateNamedFormulas(IWorkbook workbook) {
            #region #CreateNamedFormulas
            Worksheet worksheet1 = workbook.Worksheets["Sheet1"];
            Worksheet worksheet2 = workbook.Worksheets["Sheet2"];

            // Create a name for a formula that sums up the values of all cells included in the "A1:C3" range of the "Sheet1" worksheet. 
            // The scope of this name will be limited by the "Sheet1" worksheet.
            worksheet1.DefinedNames.Add("Range_Sum", "=SUM(Sheet1!$A$1:$C$3)");

            // Create a name for a formula that doubles the value resulting from the "Range_Sum" named formula and
            // make this name available within the entire workbook.
            workbook.DefinedNames.Add("Range_DoubleSum", "=2*Sheet1!Range_Sum");

            // Create formulas that use other formulas with the specified names.
            worksheet2.Cells["C2"].Formula = "=Sheet1!Range_Sum";
            worksheet2.Cells["C3"].Formula = "=Range_DoubleSum";
            worksheet2.Cells["C4"].Formula = "=Range_DoubleSum + 100";

            workbook.Worksheets.ActiveWorksheet = workbook.Worksheets["Sheet2"];

            worksheet1.Cells["A1"].Value = 2;
            worksheet1.Cells["B2"].Value = 3;
            worksheet1.Cells["C3"].Value = 4;

            worksheet2["A1:C1"].FillColor = Color.LightGray;
            worksheet2["A1:C1"].ColumnWidthInCharacters = 25;

            worksheet2.Cells["A1"].Value = "Formula Name";
            worksheet2.Cells["B1"].Value = "Formula";
            worksheet2.Cells["C1"].Value = "Formula Result";

            worksheet2.Cells["A2"].Value = "Range_Sum";
            worksheet2.Cells["A3"].Value = "Range_DoubleSum";
            worksheet2.Cells["A4"].Value = "-";

            worksheet2.Cells["B2"].Value = "'=SUM(Sheet1!$A$1:$C$3)";
            worksheet2.Cells["B3"].Value = "'=2*Sheet1!Range_Sum";
            worksheet2.Cells["B4"].Value = "'=Range_DoubleSum + 100";

            worksheet2["A:C"].AutoFitColumns();
            #endregion #CreateNamedFormulas
        }
        static void UseFunctionsInFormulas(IWorkbook workbook) {
            #region #FunctionsInFormulas

            Worksheet worksheet = workbook.Worksheets[0];
            // Provide static data.
            worksheet.Cells["A2"].Value = 15;
            worksheet.Range["A3:A5"].Value = 3;
            worksheet.Cells["A6"].Value = 20;
            worksheet.Cells["A7"].Value = 15.12345;

            // If a number in cell A2 is less than 10, the formula returns "Normal" 
            // and this text is displayed in cell B2. Otherwise, cell B2 displays "Excess".
            worksheet.Cells["C2"].FormulaInvariant = @"= IF(A2<10, ""Normal"", ""Excess"")";

            // Calculate an average value of cell values of the "A2:A7" range.
            worksheet.Cells["C3"].Formula = "=AVERAGE(A2:A7)";

            // Add values contained in cells A3 through A5, then add the value contained in cell A6, 
            // and then add 100 to that result.
            worksheet.Cells["C4"].FormulaInvariant = "=SUM(A3:A5,A6,100)";

            // Use a nested function in a formula. 
            // Round the sum of the values contained in cells A6 and A7 to two decimal places.
            worksheet.Cells["C5"].FormulaInvariant = "=ROUND(SUM(A6,A7),2)";

            // Add the current date to cell B6.
            worksheet.Cells["C6"].Formula = "= Today()";
            worksheet.Cells["C6"].NumberFormat = "m/d/yy";

            // Convert the specified text to uppercase.
            worksheet.Cells["C7"].Formula = @"=UPPER(""formula"")";

            
            Range header = worksheet.Range["A1:C1"];
            header[0].Value = "Data:";
            header[1].Value = "Formula text:";
            header[2].Value = "Formula result:";
            header.Style = workbook.Styles["Header"];
            worksheet["A1:A7"].Alignment.Indent = 1;

            Range rangeWithFormulas = worksheet.Range["C2:C7"];
            Range rangeWithFormulaDescription = worksheet.Range["B2:B7"];

            for (int index = rangeWithFormulas.RowCount - 1; index >= 0; index--) {
                rangeWithFormulaDescription[index].Value = "'" + rangeWithFormulas[index].Formula;
            }
            worksheet.Range["B1:C7"].AutoFitColumns();
            #endregion #FunctionsInFormulas
        }

        static void CreateSharedAndArrayFormulas(IWorkbook workbook) {
            #region #SharedAndArrayFormulas
            Worksheet worksheet = workbook.Worksheets[0];
            worksheet.Cells["A2"].Value = 1;

            // Use the shared formula in the "A3:A11" range of cells.
            worksheet["A3:A11"].Formula = "=SUM(A2+1)";

            // Use the shared formula in the "B2:B11" range of cells.
            worksheet["B2:B11"].Formula = "=A2+2";

            // Create an array formula that multiplies values contained in the cell range A2 through A11
            // by the corresponding cells in the range B2 through B11, 
            // and displays the results in cells C2 through C13.
            worksheet["C2:C13"].ArrayFormula = "=A2:A11*B2:B11";

            // Create an array formula that multiplies values contained in the cell range C2 through C11 by 2
            // and displays the results in cells D2 through D11.
            worksheet["D2:D11"].ArrayFormula = "=C2:C11*2";

            // Create an array formula that multiplies values contained in the cell range B2 through D10, 
            // adds the results, and displays the total sum in cell D12.
            worksheet.Range["D12"].ArrayFormula = "=SUM(B2:B11*C2:C11*D2:D11)";

            // Re-dimension an array formula range:
            // delete the array formula and create a new range with the same formula.
            if (worksheet.Cells["C13"].HasArrayFormula)
            {
                string af = worksheet.Cells["C13"].ArrayFormula;
                worksheet.Cells["C13"].GetArrayFormulaRange().ArrayFormula = string.Empty;
                worksheet["C2:C11"].ArrayFormula = af;
            }

            Range header = worksheet["A1:D1"];
            header.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
            header.FillColor = Color.LightGray;
            header.Font.Bold = true;
            header[0].Value = "Use Shared Formulas:";
            header[2].Value = "Use Array Formulas:";

            worksheet.MergeCells(worksheet.Range["A1:B1"]);
            worksheet.MergeCells(worksheet.Range["C1:D1"]);
            header.ColumnWidthInCharacters = 10;
            #endregion #SharedAndArrayFormulas
        }
    }
}
