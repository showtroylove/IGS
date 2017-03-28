Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Spreadsheet
Imports System.Drawing

Namespace SpreadsheetExamples
	Public NotInheritable Class RowAndColumnActions
		Private Sub New()
		End Sub
		Private Shared Sub InsertRows(ByVal workbook As IWorkbook)
'			#Region "#InsertRows"
			Dim worksheet As Worksheet = workbook.Worksheets(0)

			' Populate cells with data.
			For i As Integer = 0 To 9
				worksheet.Cells(i, 0).Value = i + 1
				worksheet.Cells(0, i).Value = i + 1
			Next i

			' Insert a new row into the worksheet at the 3rd position.
			worksheet.Rows.Insert(2)

			' Insert five rows into the worksheet at the 8th position.
			worksheet.Rows.Insert(7, 5)
'			#End Region ' #InsertRows
		End Sub
		Private Shared Sub InsertColumns(ByVal workbook As IWorkbook)
'			#Region "#InsertColumns"
			Dim worksheet As Worksheet = workbook.Worksheets(0)

			' Populate cells with data.
			For i As Integer = 0 To 9
				worksheet.Cells(i, 0).Value = i + 1
				worksheet.Cells(0, i).Value = i + 1
			Next i
			' Insert a new column into the worksheet at the 5th position.
			worksheet.Columns.Insert(4)

			' Insert three columns into the worksheet at the 7th position.
			worksheet.Columns.Insert(6, 3)
'			#End Region ' #InsertColumns
		End Sub

		Private Shared Sub DeleteRowsColumns(ByVal workbook As IWorkbook)
' 			#Region "#DeleteRows"
			Dim worksheet As Worksheet = workbook.Worksheets("Sheet1")

			' Fill cells with data.
			For i As Integer = 0 To 14
				worksheet.Cells(i, 0).Value = i + 1
				worksheet.Cells(0, i).Value = i + 1
			Next i

			' Delete the 2nd row from the worksheet.
			worksheet.Rows.Remove(1)

			' Delete the 3rd row from the worksheet.
			worksheet.Rows(2).Delete()

			' Delete three rows from the worksheet starting from the 10th row.
			workbook.Worksheets(0).Rows.Remove(9, 3)
'			#End Region ' #DeleteRows

'			#Region "#DeleteRowsBasedOnCondition"

			Dim worksheet1 As Worksheet = workbook.Worksheets("Sheet1")
			' Create a function specifying the condition to remove worksheet rows.
			Dim rowRemovalCondition As Func(Of Integer, Boolean) = Function(x) worksheet1.Cells(x, 0).Value.NumericValue > 3.0 AndAlso worksheet1.Cells(x, 0).Value.NumericValue < 14.0

			' Fill cells with data.
			For i As Integer = 0 To 14
				worksheet1.Cells(i, 0).Value = i + 1
				worksheet1.Cells(0, i).Value = i + 1
			Next i

			' Delete all rows that meet the specified condition.
			'worksheet1.Rows.Remove(rowRemovalCondition);


			' Delete rows that meet the specified condition starting from the 7th row.
			worksheet1.Rows.Remove(7, rowRemovalCondition)

			' Delete rows that meet the specified condition starting from the 5th row to the 14th row.
			'worksheet1.Rows.Remove(5, 14, rowRemovalCondition);


'			#End Region ' #DeleteRowsBasedOnCondition

'			#Region "#DeleteColumns"
			Dim worksheet2 As Worksheet = workbook.Worksheets("Sheet1")

			' Fill cells with data.
			For i As Integer = 0 To 14
				worksheet2.Cells(i, 0).Value = i + 1
				worksheet2.Cells(0, i).Value = i + 1
			Next i
			' Delete the 2nd column from the worksheet.
			worksheet2.Columns.Remove(1)

			' Delete the 3rd column from the worksheet.
			worksheet2.Columns(2).Delete()

			' Delete three columns from the worksheet starting from the 10th column.
			workbook.Worksheets(0).Columns.Remove(9, 3)
'			#End Region ' #DeleteColumns

'			#Region "#DeleteColumnsBasedOnCondition"

			Dim sheet As Worksheet = workbook.Worksheets("Sheet1")
			' Create a function specifying the condition to remove worksheet columns.
			Dim columnRemovalCondition As Func(Of Integer, Boolean) = Function(x) sheet.Cells(0, x).Value.NumericValue > 3.0 AndAlso sheet.Cells(0, x).Value.NumericValue < 14.0

			' Fill cells with data.
			For i As Integer = 0 To 14
				sheet.Cells(i, 0).Value = i + 1
				sheet.Cells(0, i).Value = i + 1
			Next i

			' Delete all columns that meet the specified condition.
			'sheet.Columns.Remove(columnRemovalCondition);


			' Delete columns that meet the specified condition starting from the 7th column.
			sheet.Columns.Remove(7, columnRemovalCondition)

			' Delete columns that meet the specified condition starting from the 5th column to the 14th column.
			'sheet.Columns.Remove(5, 14, columnRemovalCondition);


'			#End Region ' #DeleteColumnsBasedOnCondition		
		End Sub

		Private Shared Sub CopyRowsColumns(ByVal workbook As IWorkbook)
'			#Region "CopyRows"

'			#End Region ' CopyRows

'			#Region "CopyColumns"

'			#End Region ' CopyColumns
		End Sub

		Private Shared Sub ShowHideRowsColumns(ByVal workbook As IWorkbook)
'			#Region "ShowHideRowsColumns"
			Dim worksheet As Worksheet = workbook.Worksheets(0)

			' Hide the 8th row of the worksheet.
			worksheet.Rows(7).Visible = False

			' Hide the 4th column of the worksheet.
			worksheet.Columns(3).Visible = False

			' Populate cells with data.
			For i As Integer = 0 To 9
				worksheet.Cells(i, 0).Value = i + 1
				worksheet.Cells(0, i).Value = i + 1
			Next i
'			#End Region ' ShowHideRowsColumns
		End Sub

		Private Shared Sub SpecifyRowsHeightColumnsWidth(ByVal workbook As IWorkbook)
'			#Region "#RowHeightAndColumnWidth"
			Dim worksheet As Worksheet = workbook.Worksheets(0)

			' Set the height of the 2nd row to 50 points
			workbook.Unit = DevExpress.Office.DocumentUnit.Point
			worksheet.Rows(1).Height = 50

			' Set the height of the row that contains the "C3" cell to 1 inches.
			workbook.Unit = DevExpress.Office.DocumentUnit.Inch
			worksheet.Cells("C3").RowHeight = 1

			' Set the height of the 4th row to the height of the 3rd row.
			worksheet.Rows("4").Height = worksheet.Rows("2").Height

			' Set the default row height to 30 points.
			workbook.Unit = DevExpress.Office.DocumentUnit.Point
			worksheet.DefaultRowHeight = 30

			' Set the "B" column width to 15 characters of the default font that is specified by the Normal style.
			worksheet.Columns("B").WidthInCharacters = 15

			' Set the "C" column width to 15 millimeters.
			workbook.Unit = DevExpress.Office.DocumentUnit.Millimeter
			worksheet.Columns("C").Width = 15

			' Set the width of the column that contains the "E15" cell to 100 points.
			workbook.Unit = DevExpress.Office.DocumentUnit.Point
			worksheet.Cells("E15").ColumnWidth = 100

			' Set the width of all columns that contain the "F4:H7" cell range (the "F", "G" and "H" columns) to 70 points.
			worksheet.Range("F4:H7").ColumnWidth = 70

			' Set the "J" column width to the "B" column width value.
			worksheet.Columns("J").Width = worksheet.Columns("B").Width

			' Copy the "C" column width value and assign it to the "K" column width.
			'worksheet.Columns["K"].CopyFrom(worksheet.Columns["C"], PasteSpecial.ColumnWidths);

			' Set the default column width to 40 pixels.
			worksheet.DefaultColumnWidthInPixels = 40
			worksheet.Range("B1:J1").Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center
			worksheet.Cells("B1").Value = "15 characters"
			worksheet.Cells("C1").Value = "15 mm"
			worksheet.Cells("E1").Value = "100 pt"
			worksheet.Cells("F1").Value = "70 pt"
			worksheet.Cells("G1").Value = "70 pt"
			worksheet.Cells("H1").Value = "70 pt"
			worksheet.Cells("J1").Value = "15 characters"
			'worksheet.Cells["K1"].Value = "15 mm";

			worksheet.Cells("A2").Value = "50 pt"
			worksheet.Cells("A3").Value = "1"""
			worksheet.Cells("A4").Value = "50 pt"
			Dim range As Range = worksheet.Range("A2:A5")
			Dim rowHeightValues As Formatting = range.BeginUpdateFormatting()
			rowHeightValues.Alignment.RotationAngle = 90
			rowHeightValues.Alignment.Vertical = SpreadsheetVerticalAlignment.Center
			rowHeightValues.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center
			range.EndUpdateFormatting(rowHeightValues)

'			#End Region ' RowHeightAndColumnWidth
		End Sub
	End Class
End Namespace
