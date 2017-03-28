using System;
using System.Collections.Generic;
using DevExpress.Spreadsheet;

namespace SpreadsheetExamples {
    public static class DataValidationActions {

        static void AddDataValidation(IWorkbook workbook) {
            #region #AddDataValidation
            Worksheet worksheet = workbook.Worksheets["Data validation sample"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Restrict data entry to values in a drop-down list
            worksheet.DataValidations.Add(worksheet["D3:D10"], DataValidationType.List, ValueObject.FromRange(worksheet["G3:G8"].GetRangeWithAbsoluteReference()));

            // Restrict data entry to a number within limits
            worksheet.DataValidations.Add(worksheet["E3:E10"], DataValidationType.Decimal, DataValidationOperator.Between, 10, 40);

            // Restrict data entry to a 5-digit number
            worksheet.DataValidations.Add(worksheet["B3:B10"], DataValidationType.Custom, "=AND(ISNUMBER(B3),LEN(B3)=5)");
            #endregion #AddDataValidation
        }

        static void ChangeCriteria(IWorkbook workbook) {
            #region #ChangeCriteria
            Worksheet worksheet = workbook.Worksheets["Data validation sample"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Restrict data entry to a number within limits
            DataValidation validation = worksheet.DataValidations.Add(worksheet["E3:E10"], DataValidationType.Decimal, DataValidationOperator.Between, 10, 40);

            // Change operator and criteria
            validation.Operator = DataValidationOperator.GreaterThanOrEqual;
            validation.Criteria = 20;
            validation.Criteria2 = ValueObject.Empty;
            #endregion #ChangeCriteria
        }

        static void UseUnionRange(IWorkbook workbook) {
            #region #UseUnionRange
            Worksheet worksheet = workbook.Worksheets["Data validation sample"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Create union range
            Range range = worksheet.Range.Union(worksheet["E3:E4"], worksheet["E6:E10"]);
            // Restrict data entry to a number within limits
            worksheet.DataValidations.Add(range, DataValidationType.Decimal, DataValidationOperator.Between, 10, 40);
            #endregion #UseUnionRange
        }

        static void SetupInputMessage(IWorkbook workbook) {
            #region #SetupInputMessage
            Worksheet worksheet = workbook.Worksheets["Data validation sample"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Restrict data entry to a 5-digit number
            DataValidation validation = worksheet.DataValidations.Add(worksheet["B3:B10"], DataValidationType.Custom, "=AND(ISNUMBER(B3),LEN(B3)=5)");

            // Setup input message
            validation.InputTitle = "Employee Id";
            validation.InputMessage = "Please enter 5-digit number";
            validation.ShowInputMessage = true;
            #endregion #SetupInputMessage
        }

        static void SetupErrorMessage(IWorkbook workbook) {
            #region #SetupErrorMessage
            Worksheet worksheet = workbook.Worksheets["Data validation sample"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Restrict data entry to a 5-digit number
            DataValidation validation = worksheet.DataValidations.Add(worksheet["B3:B10"], DataValidationType.Custom, "=AND(ISNUMBER(B3),LEN(B3)=5)");

            // Setup error message
            validation.ErrorTitle = "Wrong Employee Id";
            validation.ErrorMessage = "The value you entered is not valid. Use 5-digit number as employee id.";
            validation.ShowErrorMessage = true;
            #endregion #SetupErrorMessage
        }

        static void RemoveDataValidation(IWorkbook workbook) {
            #region #RemoveDataValidation
            Worksheet worksheet = workbook.Worksheets["Data validation sample"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Add data validation
            worksheet.DataValidations.Add(worksheet["D3:D10"], DataValidationType.List, ValueObject.FromRange(worksheet["G3:G8"].GetRangeWithAbsoluteReference()));
            worksheet.DataValidations.Add(worksheet["E3:E10"], DataValidationType.Decimal, DataValidationOperator.Between, 10, 40);

            // Remove data validation by index
            worksheet.DataValidations.RemoveAt(1);
            #endregion #RemoveDataValidation
        }

        static void RemoveAllDataValidations(IWorkbook workbook) {
            #region #RemoveAllDataValidations
            Worksheet worksheet = workbook.Worksheets["Data validation sample"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Add data validation
            worksheet.DataValidations.Add(worksheet["D3:D10"], DataValidationType.List, ValueObject.FromRange(worksheet["G3:G8"].GetRangeWithAbsoluteReference()));
            worksheet.DataValidations.Add(worksheet["E3:E10"], DataValidationType.Decimal, DataValidationOperator.Between, 10, 40);

            // Remove all data validations
            worksheet.DataValidations.Clear();
            #endregion #RemoveAllDataValidations
        }
    }
}