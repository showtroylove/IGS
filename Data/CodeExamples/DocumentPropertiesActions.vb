Imports System
Imports System.Collections.Generic
Imports DevExpress.Spreadsheet

Namespace SpreadsheetExamples
	Public NotInheritable Class DocumentPropertiesActions

		Private Sub New()
		End Sub
		Private Shared Sub BuiltInProperties(ByVal workbook As IWorkbook)
'			#Region "#Built-inProperties"
			Dim worksheet As Worksheet = workbook.Worksheets(0)
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Set the built-in document properties.
			workbook.DocumentProperties.Title = "Spreadsheet API: document properties demo"
			workbook.DocumentProperties.Description = "How to manage document properties through Spreadsheet API."
			workbook.DocumentProperties.Keywords = "Spreadsheet, API, demo, properties, OLEProps"
			workbook.DocumentProperties.Company = "Developer Express Inc."

			' Get the built-in document properties.
			worksheet("B2").Value = "Title:"
			worksheet("C2").Value = workbook.DocumentProperties.Title
			worksheet("B3").Value = "Description:"
			worksheet("C3").Value = workbook.DocumentProperties.Description
			worksheet("B4").Value = "Keywords:"
			worksheet("C4").Value = workbook.DocumentProperties.Keywords
			worksheet("B5").Value = "Company:"
			worksheet("C5").Value = workbook.DocumentProperties.Company

			worksheet.Columns(0).WidthInCharacters = 2
			worksheet.Columns.AutoFit(1, 2)

'			#End Region ' #Built-inProperties
		End Sub

		Private Shared Sub CustomProperties(ByVal workbook As IWorkbook)
'			#Region "#CustomProperties"
			Dim worksheet As Worksheet = workbook.Worksheets(0)
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Set the custom document properties.
			workbook.DocumentProperties.Custom("Checked by") = "Mike Hamilton"
			workbook.DocumentProperties.Custom("Revision") = 3
			workbook.DocumentProperties.Custom("Completed") = True
			workbook.DocumentProperties.Custom("Published") = DateTime.Now

			' Enumerate and get the custom document properties.
			Dim customPropertiesNames As IEnumerable(Of String) = workbook.DocumentProperties.Custom.Names
			Dim rowIndex As Integer = 1
			For Each propertyName As String In customPropertiesNames
				worksheet(rowIndex, 1).Value = propertyName & ":"
				worksheet(rowIndex, 2).Value = workbook.DocumentProperties.Custom(propertyName)
				If worksheet(rowIndex, 2).Value.IsDateTime Then
					worksheet(rowIndex, 2).NumberFormat = "[$-409]m/d/yyyy h:mm AM/PM"
				End If
				rowIndex += 1
			Next propertyName

			' Remove a custom document property.
			workbook.DocumentProperties.Custom("Published") = Nothing

			' Remove all custom document properties.
			workbook.DocumentProperties.Custom.Clear()

			worksheet.Columns(0).WidthInCharacters = 2
			worksheet.Columns.AutoFit(1, 2)

'			#End Region ' #CustomProperties
		End Sub
	End Class
End Namespace