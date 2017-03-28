Imports System
Imports DevExpress.Spreadsheet
Imports DevExpress.Utils
Imports System.Drawing
Imports System.Globalization
Imports System.Collections.Generic


Namespace SpreadsheetExamples
	Public NotInheritable Class SearchActions
		Private Sub New()
		End Sub
		Private Shared Sub SimpleSearch(ByVal workbook As IWorkbook)
'			#Region "#SimpleSearch"
			Dim worksheet As Worksheet = workbook.Worksheets("SearchSample")
			workbook.Worksheets.ActiveWorksheet = worksheet
			worksheet.Visible = True

			' Find and highlight cells, containing text "the".
			Dim foundCells As IEnumerable(Of Cell) = worksheet.Search("the")
			For Each cell As Cell In foundCells
				cell.Fill.BackgroundColor = Color.Yellow
			Next cell
'			#End Region ' #SimpleSearch
		End Sub

		Private Shared Sub SearchWithOptions(ByVal workbook As IWorkbook)
'			#Region "#SearchWithOptions"
			Dim worksheet As Worksheet = workbook.Worksheets("SearchSample")
			workbook.Worksheets.ActiveWorksheet = worksheet
			worksheet.Visible = True

			' Find and highlight cells, containing case-sensitive text "The".
			Dim options As New SearchOptions()
			options.MatchCase = True
			Dim foundCells As IEnumerable(Of Cell) = worksheet.Search("The", options)
			For Each cell As Cell In foundCells
				cell.Fill.BackgroundColor = Color.Yellow
			Next cell
'			#End Region ' #SearchWithOptions
		End Sub
	End Class
End Namespace