Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Spreadsheet

Namespace SpreadsheetExamples

	Public NotInheritable Class WorksheetActions
		Private Sub New()
		End Sub
		Private Shared Sub AssignActiveWorksheet(ByVal workbook As IWorkbook)
'			#Region "ActiveWorksheet"
			' Set the second worksheet under the "Sheet2" name as active.
			workbook.Worksheets.ActiveWorksheet = workbook.Worksheets("Sheet2")
'			#End Region ' ActiveWorksheet
		End Sub

		Private Shared Sub AddWorksheet(ByVal workbook As IWorkbook)
'			#Region "AddWorksheet"
			' Add a new worksheet to the workbook. The worksheet will be inserted into the end of the existing worksheet collection
			' under the name "SheetN", where N is a number following the largest number used in worksheet names in the previously existing collection.
			workbook.Worksheets.Add()

			' Add a new worksheet under the specified name.
			workbook.Worksheets.Add().Name = "TestSheet1"

			workbook.Worksheets.Add("TestSheet2")

			' Add a new workbook to the specified position in the collection of worksheets.
			workbook.Worksheets.Insert(1, "TestSheet3")

			workbook.Worksheets.Insert(3)

'			#End Region ' AddWorksheet
		End Sub

		Private Shared Sub RemoveWorksheet(ByVal workbook As IWorkbook)
'			#Region "DeleteWorksheet"
			' By default, a new IWorkbook object is created with three worksheets ("Sheet1", "Sheet2", "Sheet3").
			' Delete the second default worksheet under the "Sheet2" name from the workbook.
			workbook.Worksheets.Remove(workbook.Worksheets("Sheet2"))

			' Delete the first worksheet using its index in the collection of worksheets.
			workbook.Worksheets.RemoveAt(0)

			Dim lastWorksheet As Worksheet = workbook.Worksheets.ActiveWorksheet
			Dim range As Range = lastWorksheet.Range("A1:B3")
			range(0).Value = "Sheets: "
			range(1).Value = workbook.Worksheets.Count
			range(2).Value = "Name:"
			range(3).Value = lastWorksheet.Name

'			#End Region ' DeleteWorksheet
		End Sub

		Private Shared Sub RenameWorksheet(ByVal workbook As IWorkbook)
'			#Region "RenameWorksheet"
			Dim sheet2 As Worksheet = workbook.Worksheets(1)
			' Change the name of the second worksheet in the collection of worksheets.
			sheet2.Name = "Renamed Sheet"
'			#End Region ' RenameWorksheet
		End Sub

		Private Shared Sub CopyWorksheetWithinWorkbook(ByVal workbook As IWorkbook)
			' TODO
		End Sub

		Private Shared Sub CopyWorksheetBetweenWorkbooks(ByVal workbook As IWorkbook)
			' TODO
		End Sub

		Private Shared Sub MoveWorksheet(ByVal workbook As IWorkbook)
'			#Region "MoveWorksheet"
			' Move the first worksheet to the position of the last worksheet within the workbook.
			workbook.Worksheets(0).Move(workbook.Worksheets.Count - 1)
'			#End Region ' MoveWorksheet
		End Sub

		Private Shared Sub ShowHideWorksheet(ByVal workbook As IWorkbook)
'			#Region "ShowHideWorksheet"
			' Hide the worksheet under the "Sheet2" name and prevent end-users from unhiding it via Excel interface.
			' To make this worksheet visible again, use the Worksheet.Visible property.
			workbook.Worksheets("Sheet2").VisibilityType = WorksheetVisibilityType.VeryHidden

			' Hide the worksheet under the "Sheet3" name. 
			' In this state a worksheet can be unhidden via Excel interface.
			workbook.Worksheets("Sheet3").Visible = False
'			#End Region ' ShowHideWorksheet
		End Sub

		Private Shared Sub ShowHideGridlines(ByVal workbook As IWorkbook)
'			#Region "ShowHideGridlines"
			' Hide gridlines on the first worksheet.
			workbook.Worksheets(0).ActiveView.ShowGridlines = False
'			#End Region ' ShowHideGridlines
		End Sub

		Private Shared Sub ShowHideHeadings(ByVal workbook As IWorkbook)
'			#Region "ShowHideHeadings"
			' Hide row and column headings on the first worksheet.
			workbook.Worksheets(0).ActiveView.ShowHeadings = False
'			#End Region ' ShowHideHeadings
		End Sub

		Private Shared Sub SetPageOrientation(ByVal workbook As IWorkbook)
'			#Region "PageOrientation"
			' Set the page orientation to Landscape.
			workbook.Worksheets(0).ActiveView.Orientation = PageOrientation.Landscape

'			#End Region ' PageOrientation
		End Sub

		Private Shared Sub SetPageMargins(ByVal workbook As IWorkbook)
'			#Region "PageMargins"
			' Select a unit of measure used within the workbook.
			workbook.Unit = DevExpress.Office.DocumentUnit.Centimeter

			' Access page margins.
			Dim pageMargins As Margins = workbook.Worksheets(0).ActiveView.Margins

			' Specify page margins.
			pageMargins.Left = 2
			pageMargins.Top = 3
			pageMargins.Right = 1
			pageMargins.Bottom = 2

			' Specify header and footer margins.
			pageMargins.Header = 2
			pageMargins.Footer = 1
'			#End Region ' PageMargins
		End Sub

		Private Shared Sub SetPaperSize(ByVal workbook As IWorkbook)
'			#Region "PaperSize"
			' Select the page's paper size.
			workbook.Worksheets(0).ActiveView.PaperKind = System.Drawing.Printing.PaperKind.A4
'			#End Region ' PaperSize
		End Sub

		Private Shared Sub ZoomWorksheet(ByVal workbook As IWorkbook)
'			#Region "WorksheetZoom"
			' Zoom out the worksheet view. 
			workbook.Worksheets(0).ActiveView.Zoom = 50

'			#End Region ' WorksheetZoom
		End Sub

	End Class
End Namespace
