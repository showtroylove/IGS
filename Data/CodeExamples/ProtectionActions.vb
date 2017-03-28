Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Spreadsheet
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraPrinting.Control
Imports System.Drawing

Namespace SpreadsheetExamples
	Public NotInheritable Class ProtectionActions
		Private Sub New()
		End Sub
		Private Shared Sub ProtectWorkbook(ByVal workbook As IWorkbook)
'			#Region "#ProtectWorkbook"
			Dim worksheet As Worksheet = workbook.Worksheets("ProtectionSample")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Protect workbook structure with password.
			If (Not workbook.IsProtected) Then
				workbook.Protect("password", True, False)
			End If

			' Add an explanation to the created rule.
			worksheet("B2").Value = "Workbook structure is now protected by password. " & Constants.vbLf & " You cannot add, move or delete sheets until protection is removed."
			worksheet.Visible = True
'			#End Region ' #ProtectWorkbook
		End Sub
		Private Shared Sub UnprotectWorkbook(ByVal workbook As IWorkbook)
'			#Region "#UnprotectWorkbook"
			Dim worksheet As Worksheet = workbook.Worksheets("ProtectionSample")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Unprotect workbook with password.
			If workbook.IsProtected Then
				workbook.Unprotect("password")
			End If

			' Add an explanation to the created rule.
			worksheet("B2").Value = "Workbook is unprotected. Sheets can be added, moved or deleted now."
			worksheet.Visible = True
'			#End Region ' #UnprotectWorkbook
		End Sub
		Private Shared Sub ProtectWorksheet(ByVal workbook As IWorkbook)
'			#Region "#ProtectWorksheet"
			Dim worksheet As Worksheet = workbook.Worksheets("ProtectionSample")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Protect worksheet from editing with password.
			If (Not worksheet.IsProtected) Then
				worksheet.Protect("password", WorksheetProtectionPermissions.Default)
			End If

			' Add an explanation to the created rule.
			worksheet("B2").Value = "Worksheet is now protected by password. " & Constants.vbLf & " You cannot edit or format cells until protection is removed." & Constants.vbLf & "To remove protection, right click currently open sheet name in sheet tab," & Constants.vbLf & "select ""Unprotect Sheet"" and enter ""password""."
			worksheet.Visible = True
'			#End Region ' #ProtectWorksheet
		End Sub
		Private Shared Sub UnprotectWorksheet(ByVal workbook As IWorkbook)
'			#Region "#UnprotectWorksheet"
			Dim worksheet As Worksheet = workbook.Worksheets("ProtectionSample")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Unprotect worksheet with password.
			If worksheet.IsProtected Then
				worksheet.Unprotect("password")
			End If

			' Add an explanation to the created rule.
			worksheet("B2").Value = "Worksheet is unprotected. You can edit and format cells now."
			worksheet.Visible = True
'			#End Region
		End Sub
		Private Shared Sub ProtectRange(ByVal workbook As IWorkbook)
'			#Region "#ProtectRange"
			Dim worksheet As Worksheet = workbook.Worksheets("ProtectionSample")
			workbook.Worksheets.ActiveWorksheet = worksheet
			worksheet("B2:J5").Borders.SetOutsideBorders(Color.Red, BorderLineStyle.Thin)

			' Protect range from editing with password.
			Dim protectedRange As ProtectedRange = worksheet.ProtectedRanges.Add("My Range", worksheet("B2:J5"))
			Dim permission As New EditRangePermission()
			permission.UserName = "John"
			permission.DomainName = "MyDomain"
			permission.Deny = False
			protectedRange.SecurityDescriptor = protectedRange.CreateSecurityDescriptor(New EditRangePermission() { permission })
			protectedRange.SetPassword("letmeedit")

			If (Not worksheet.IsProtected) Then
				worksheet.Protect("password", WorksheetProtectionPermissions.Default)
			End If

			' Add an explanation to the created rule.
			worksheet("B2").Value = "This cell range is now protected by password. " & Constants.vbLf & " You cannot edit or format it until protection is removed." & Constants.vbLf & "To remove protection, double click the range and enter ""letmeedit""."
			worksheet.Visible = True
'			#End Region ' #ProtectRange
		End Sub
	End Class
End Namespace