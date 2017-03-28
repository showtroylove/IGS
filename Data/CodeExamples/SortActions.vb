Imports System
Imports DevExpress.Spreadsheet
Imports DevExpress.Utils
Imports System.Globalization
Imports System.Collections.Generic

Namespace SpreadsheetExamples
	Public NotInheritable Class SortActions
		Private Sub New()
		End Sub
		Private Shared Sub SimpleSort(ByVal workbook As IWorkbook)
'			#Region "#SimpleSort"
			Dim worksheet As Worksheet = workbook.Worksheets(0)

			' Setup cells.
			worksheet.Cells("A2").Value = "Ray Bradbury"
			worksheet.Cells("A3").Value = "F. Scott Fitzgerald"
			worksheet.Cells("A4").Value = "Harper Lee"
			worksheet.Cells("A5").Value = "Ernest Hemingway"
			worksheet.Cells("A6").Value = "J.D. Salinger"
			worksheet.Cells("A7").Value = "Gene Wolfe"

			' Sort authors in ascending order.
			Dim range As Range = worksheet.Range("A2:A7")
			worksheet.Sort(range)

			' Setup header.
			Dim header As Range = worksheet.Range("A1")
			header(0).Value = "Authors in ascending order"
			header.ColumnWidthInCharacters = 28
			header.Style = workbook.Styles("Header")
'			#End Region ' #SimpleSort
		End Sub

		Private Shared Sub DescendingOrder(ByVal workbook As IWorkbook)
'			#Region "#DescendingOrder"
			Dim worksheet As Worksheet = workbook.Worksheets(0)

			' Setup cells.
			worksheet.Cells("A2").Value = "Ray Bradbury"
			worksheet.Cells("A3").Value = "F. Scott Fitzgerald"
			worksheet.Cells("A4").Value = "Harper Lee"
			worksheet.Cells("A5").Value = "Ernest Hemingway"
			worksheet.Cells("A6").Value = "J.D. Salinger"
			worksheet.Cells("A7").Value = "Gene Wolfe"

			' Sort authors in descending order.
			Dim range As Range = worksheet.Range("A2:A7")
			worksheet.Sort(range, False)

			' Setup header.
			Dim header As Range = worksheet.Range("A1")
			header(0).Value = "Authors in descending order"
			header.ColumnWidthInCharacters = 28
			header.Style = workbook.Styles("Header")
'			#End Region ' #DescendingOrder
		End Sub

		Private Shared Sub SelectComparer(ByVal workbook As IWorkbook)
'			#Region "#SelectComparer"
			Dim worksheet As Worksheet = workbook.Worksheets(0)

			' Setup cells.
			worksheet.Cells("A2").Value = 0.7
			worksheet.Cells("A3").Value = 0.45
			worksheet.Cells("A4").Value = 0.53
			worksheet.Cells("A5").Value = 0.33
			worksheet.Cells("A6").Value = 0.99
			worksheet.Cells("A7").Value = 0.62

			' Select comparer. 
			Dim comparer As IComparer(Of CellValue) = worksheet.Comparers.Descending

			' Sort values using selected comparer.
			Dim range As Range = worksheet.Range("A2:A7")
			worksheet.Sort(range, 0, comparer)

			' Setup header.
			Dim header As Range = worksheet.Range("A1")
			header(0).Value = "Values sorted by selected comparer"
			header.ColumnWidthInCharacters = 40
			header.Style = workbook.Styles("Header")
'			#End Region ' #SelectComparer
		End Sub

		Private Shared Sub SortBySpecifiedColumn(ByVal workbook As IWorkbook)
'			#Region "#SortBySpecifiedColumn"
			Dim worksheet As Worksheet = workbook.Worksheets("SortSample")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Table is sorted by markup coulmn (index = 6) using ascending order.
			Dim range As Range = worksheet.Range("B5:H18")
			worksheet.Sort(range, 6)

			' Add an explanation to the created rule.
			worksheet("B2").Value = "Table is sorted by markup column using ascending order."
			worksheet.Visible = True
'			#End Region ' #SortBySpecifiedColumn
		End Sub

		Private Shared Sub SortByMultipleColumns(ByVal workbook As IWorkbook)
'			#Region "#SortByMultipleColumns"
			Dim worksheet As Worksheet = workbook.Worksheets("SortSample")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Create sorting fields.
			Dim fields As New List(Of SortField)()

			' Setup first sort field. Author column (index = 0) will be sorted using ascending order.
			Dim sortField1 As New SortField()
			sortField1.ColumnOffset = 0
			sortField1.Comparer = worksheet.Comparers.Ascending
			fields.Add(sortField1)

			' Setup second sort field. Title column (index = 1) will be sorted using ascending order.
			Dim sortField2 As New SortField()
			sortField2.ColumnOffset = 1
			sortField2.Comparer = worksheet.Comparers.Ascending
			fields.Add(sortField2)

			' Execute sort command for whole table.
			Dim range As Range = worksheet.Range("B5:H18")
			worksheet.Sort(range, fields)

			' Add an explanation to the created rule.
			worksheet("B2").Value = "Table is sorted by two columns: author and then title in ascending order."
			worksheet.Visible = True
'			#End Region ' #SortByMultipleColumns
		End Sub
	End Class
End Namespace