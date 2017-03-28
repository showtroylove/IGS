using System;
using System.Collections.Generic;
using DevExpress.Spreadsheet;

namespace SpreadsheetExamples {
    public static class DocumentPropertiesActions {

        static void BuiltInProperties(IWorkbook workbook) {
            #region #Built-inProperties
            Worksheet worksheet = workbook.Worksheets[0];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Set the built-in document properties.
            workbook.DocumentProperties.Title = "Spreadsheet API: document properties demo";
            workbook.DocumentProperties.Description = "How to manage document properties through Spreadsheet API.";
            workbook.DocumentProperties.Keywords = "Spreadsheet, API, demo, properties, OLEProps";
            workbook.DocumentProperties.Company = "Developer Express Inc.";

            // Get the built-in document properties.
            worksheet["B2"].Value = "Title:";
            worksheet["C2"].Value = workbook.DocumentProperties.Title;
            worksheet["B3"].Value = "Description:";
            worksheet["C3"].Value = workbook.DocumentProperties.Description;
            worksheet["B4"].Value = "Keywords:";
            worksheet["C4"].Value = workbook.DocumentProperties.Keywords;
            worksheet["B5"].Value = "Company:";
            worksheet["C5"].Value = workbook.DocumentProperties.Company;

            worksheet.Columns[0].WidthInCharacters = 2;
            worksheet.Columns.AutoFit(1, 2);

            #endregion #Built-inProperties
        }

        static void CustomProperties(IWorkbook workbook) {
            #region #CustomProperties
            Worksheet worksheet = workbook.Worksheets[0];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Set the custom document properties.
            workbook.DocumentProperties.Custom["Checked by"] = "Mike Hamilton";
            workbook.DocumentProperties.Custom["Revision"] = 3;
            workbook.DocumentProperties.Custom["Completed"] = true;
            workbook.DocumentProperties.Custom["Published"] = DateTime.Now;

            // Enumerate and get the custom document properties.
            IEnumerable<string> customPropertiesNames = workbook.DocumentProperties.Custom.Names;
            int rowIndex = 1;
            foreach(string propertyName in customPropertiesNames) {
                worksheet[rowIndex, 1].Value = propertyName + ":";
                worksheet[rowIndex, 2].Value = workbook.DocumentProperties.Custom[propertyName];
                if(worksheet[rowIndex, 2].Value.IsDateTime)
                    worksheet[rowIndex, 2].NumberFormat = "[$-409]m/d/yyyy h:mm AM/PM";
                rowIndex++;
            }

            // Remove a custom document property.
            workbook.DocumentProperties.Custom["Published"] = null;

            // Remove all custom document properties.
            workbook.DocumentProperties.Custom.Clear();

            worksheet.Columns[0].WidthInCharacters = 2;
            worksheet.Columns.AutoFit(1, 2);

            #endregion #CustomProperties
        }
    }
}