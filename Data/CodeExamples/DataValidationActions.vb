Imports System
Imports System.Collections.Generic
Imports DevExpress.Spreadsheet

Namespace SpreadsheetExamples
	Public NotInheritable Class DataValidationActions

		Private Sub New()
		End Sub
		Private Shared Sub AddDataValidation(ByVal workbook As IWorkbook)
'			#Region "#AddDataValidation"
			Dim worksheet As Worksheet = workbook.Worksheets("Data validation sample")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Restrict data entry to values in a drop-down list
			worksheet.DataValidations.Add(worksheet("D3:D10"), DataValidationType.List, ValueObject.FromRange(worksheet("G3:G8").GetRangeWithAbsoluteReference()))

			' Restrict data entry to a number within limits
			worksheet.DataValidations.Add(worksheet("E3:E10"), DataValidationType.Decimal, DataValidationOperator.Between, 10, 40)

			' Restrict data entry to a 5-digit number
			worksheet.DataValidations.Add(worksheet("B3:B10"), DataValidationType.Custom, "=AND(ISNUMBER(B3),LEN(B3)=5)")
'			#End Region ' #AddDataValidation
		End Sub

		Private Shared Sub ChangeCriteria(ByVal workbook As IWorkbook)
'			#Region "#ChangeCriteria"
			Dim worksheet As Worksheet = workbook.Worksheets("Data validation sample")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Restrict data entry to a number within limits
			Dim validation As DataValidation = worksheet.DataValidations.Add(worksheet("E3:E10"), DataValidationType.Decimal, DataValidationOperator.Between, 10, 40)

			' Change operator and criteria
			validation.Operator = DataValidationOperator.GreaterThanOrEqual
			validation.Criteria = 20
			validation.Criteria2 = ValueObject.Empty
'			#End Region ' #ChangeCriteria
		End Sub

		Private Shared Sub UseUnionRange(ByVal workbook As IWorkbook)
'			#Region "#UseUnionRange"
			Dim worksheet As Worksheet = workbook.Worksheets("Data validation sample")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Create union range
			Dim range As Range = worksheet.Range.Union(worksheet("E3:E4"), worksheet("E6:E10"))
			' Restrict data entry to a number within limits
			worksheet.DataValidations.Add(range, DataValidationType.Decimal, DataValidationOperator.Between, 10, 40)
'			#End Region ' #UseUnionRange
		End Sub

		Private Shared Sub SetupInputMessage(ByVal workbook As IWorkbook)
'			#Region "#SetupInputMessage"
			Dim worksheet As Worksheet = workbook.Worksheets("Data validation sample")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Restrict data entry to a 5-digit number
			Dim validation As DataValidation = worksheet.DataValidations.Add(worksheet("B3:B10"), DataValidationType.Custom, "=AND(ISNUMBER(B3),LEN(B3)=5)")

			' Setup input message
			validation.InputTitle = "Employee Id"
			validation.InputMessage = "Please enter 5-digit number"
			validation.ShowInputMessage = True
'			#End Region ' #SetupInputMessage
		End Sub

		Private Shared Sub SetupErrorMessage(ByVal workbook As IWorkbook)
'			#Region "#SetupErrorMessage"
			Dim worksheet As Worksheet = workbook.Worksheets("Data validation sample")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Restrict data entry to a 5-digit number
			Dim validation As DataValidation = worksheet.DataValidations.Add(worksheet("B3:B10"), DataValidationType.Custom, "=AND(ISNUMBER(B3),LEN(B3)=5)")

			' Setup error message
			validation.ErrorTitle = "Wrong Employee Id"
			validation.ErrorMessage = "The value you entered is not valid. Use 5-digit number as employee id."
			validation.ShowErrorMessage = True
'			#End Region ' #SetupErrorMessage
		End Sub

		Private Shared Sub RemoveDataValidation(ByVal workbook As IWorkbook)
'			#Region "#RemoveDataValidation"
			Dim worksheet As Worksheet = workbook.Worksheets("Data validation sample")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Add data validation
			worksheet.DataValidations.Add(worksheet("D3:D10"), DataValidationType.List, ValueObject.FromRange(worksheet("G3:G8").GetRangeWithAbsoluteReference()))
			worksheet.DataValidations.Add(worksheet("E3:E10"), DataValidationType.Decimal, DataValidationOperator.Between, 10, 40)

			' Remove data validation by index
			worksheet.DataValidations.RemoveAt(1)
'			#End Region ' #RemoveDataValidation
		End Sub

		Private Shared Sub RemoveAllDataValidations(ByVal workbook As IWorkbook)
'			#Region "#RemoveAllDataValidations"
			Dim worksheet As Worksheet = workbook.Worksheets("Data validation sample")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Add data validation
			worksheet.DataValidations.Add(worksheet("D3:D10"), DataValidationType.List, ValueObject.FromRange(worksheet("G3:G8").GetRangeWithAbsoluteReference()))
			worksheet.DataValidations.Add(worksheet("E3:E10"), DataValidationType.Decimal, DataValidationOperator.Between, 10, 40)

			' Remove all data validations
			worksheet.DataValidations.Clear()
'			#End Region ' #RemoveAllDataValidations
		End Sub
	End Class
End Namespace