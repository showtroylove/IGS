using System;
using DevExpress.Spreadsheet;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Control;
using System.Drawing;

namespace SpreadsheetExamples {
    public static class ProtectionActions {
        static void ProtectWorkbook(IWorkbook workbook) {
            #region #ProtectWorkbook
            Worksheet worksheet = workbook.Worksheets["ProtectionSample"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Protect workbook structure (prevents users from adding or deleting worksheets
            // or from displaying hidden worksheets) with the password.
            if (!workbook.IsProtected)
                workbook.Protect("password", true, false);
            // Add a note.
            worksheet["B2"].Value = "Workbook structure is now protected by password. \n You cannot add, move or delete sheets until protection is removed.";
            worksheet.Visible = true;
            #endregion #ProtectWorkbook
        }
        static void UnprotectWorkbook(IWorkbook workbook) {
            #region #UnprotectWorkbook
            Worksheet worksheet = workbook.Worksheets["ProtectionSample"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Unprotect the workbook using a password.
            if (workbook.IsProtected)
                workbook.Unprotect("password");
            // Add a note.
            worksheet["B2"].Value = "Workbook is unprotected. Sheets can be added, moved or deleted now.";
            worksheet.Visible = true;
            #endregion #UnprotectWorkbook
        }
        static void ProtectWorksheet(IWorkbook workbook) {
            #region #ProtectWorksheet
            Worksheet worksheet = workbook.Worksheets["ProtectionSample"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Protect the worksheet. Prevent users from making changes to worksheet elements.
            if (!worksheet.IsProtected)
                worksheet.Protect("password", WorksheetProtectionPermissions.Default);
            // Add a note.
            worksheet["B2"].Value = "Worksheet is now protected by password. \n You cannot edit or format cells until protection is removed." +
                                    "\nTo remove protection, right click currently open sheet name in sheet tab," +
                                    "\nselect \"Unprotect Sheet\" and enter \"password\".";
            worksheet.Visible = true;
            #endregion #ProtectWorksheet
        }
        static void UnprotectWorksheet(IWorkbook workbook) {
            #region #UnprotectWorksheet
            Worksheet worksheet = workbook.Worksheets["ProtectionSample"];
            workbook.Worksheets.ActiveWorksheet = worksheet;

            // Unprotect the worksheet using a  password.
            if (worksheet.IsProtected)
                worksheet.Unprotect("password");
            // Add a note.
            worksheet["B2"].Value = "Worksheet is unprotected. You can edit and format cells now.";
            worksheet.Visible = true;
            #endregion
        }
        static void ProtectRange(IWorkbook workbook) {
            #region #ProtectRange
            Worksheet worksheet = workbook.Worksheets["ProtectionSample"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            worksheet["B2:J5"].Borders.SetOutsideBorders(Color.Red, BorderLineStyle.Thin);

            // Give specific user permission to edit a range in a protected worksheet.
            ProtectedRange protectedRange = worksheet.ProtectedRanges.Add("My Range", worksheet["B2:J5"]);
            EditRangePermission permission = new EditRangePermission();
            permission.UserName = "John";
            permission.DomainName = "MyDomain";
            permission.Deny = false;
            protectedRange.SecurityDescriptor = protectedRange.CreateSecurityDescriptor(new EditRangePermission[] { permission });
            protectedRange.SetPassword("letmeedit");
            // Protect a worksheet.
            if (!worksheet.IsProtected)
                worksheet.Protect("password", WorksheetProtectionPermissions.Default);
            // Add a note.
            worksheet["B2"].Value = "This cell range is now protected by password. \n You cannot edit or format it until protection is removed." +
                                    "\nTo remove protection, double click the range and enter \"letmeedit\".";
            worksheet.Visible = true;
            #endregion #ProtectRange
        }
    }
}