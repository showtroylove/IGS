Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports DevExpress.Spreadsheet

Namespace SpreadsheetExamples
	Public NotInheritable Class GroupingAndOutlineActions

		Private Sub New()
		End Sub
		Private Shared Sub GroupRows(ByVal workbook As IWorkbook)
'			#Region "#GroupRows"
			Dim worksheet As Worksheet = workbook.Worksheets("Grouping")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Group rows and collapse.
			worksheet.Rows.Group(2, 5, True)

			' Group rows.
			worksheet.Rows.Group(8, 11, False)

'			#End Region ' #GroupRows
		End Sub

		Private Shared Sub GroupColumns(ByVal workbook As IWorkbook)
'			#Region "#GroupColumns"
			Dim worksheet As Worksheet = workbook.Worksheets("Grouping")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Group columns.
			worksheet.Columns.Group(2, 5, False)

'			#End Region ' #GroupColumns
		End Sub

		Private Shared Sub UngroupRows(ByVal workbook As IWorkbook)
'			#Region "#UngroupRows"
			Dim worksheet As Worksheet = workbook.Worksheets("Grouping and outline")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Ungroup rows and expand.
			worksheet.Rows.UnGroup(2, 5, True)

			' Ungroup rows.
			worksheet.Rows.UnGroup(8, 11, False)
			worksheet.Rows.UnGroup(1, 12, False)

'			#End Region ' #UngroupRows
		End Sub

		Private Shared Sub UngroupColumns(ByVal workbook As IWorkbook)
'			#Region "#UngroupColumns"
			Dim worksheet As Worksheet = workbook.Worksheets("Grouping and outline")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Ungroup columns.
			worksheet.Columns.UnGroup(2, 5, False)

'			#End Region ' #UngroupColumns
		End Sub

		Private Shared Sub AutoOutline(ByVal workbook As IWorkbook)
'			#Region "#AutoOutline"
			Dim worksheet As Worksheet = workbook.Worksheets("Grouping")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Outline the data automatically.
			worksheet.AutoOutline()

'			#End Region ' #AutoOutline
		End Sub

		Private Shared Sub Subtotal(ByVal workbook As IWorkbook)
'			#Region "#Subtotal"
			Dim worksheet As Worksheet = workbook.Worksheets("Regional sales")
			workbook.Worksheets.ActiveWorksheet = worksheet

			Dim range As Range = worksheet("B3:D9")
			Dim subtotalColumnList As New List(Of Integer)()
			subtotalColumnList.Add(3)
			worksheet.Subtotal(range, 1, subtotalColumnList, 9, "Total")

'			#End Region ' #Subtotal
		End Sub
	End Class
End Namespace