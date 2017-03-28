Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Spreadsheet
Imports DevExpress.Utils
Imports System.Globalization

Namespace SpreadsheetExamples
	Public NotInheritable Class CellActions
		Private Sub New()
		End Sub
		Private Shared Sub ChangeCellValue(ByVal workbook As IWorkbook)
'			#Region "#CellValue"
			Dim worksheet As Worksheet = workbook.Worksheets(0)

			' Add data of different types to cells.
			worksheet.Cells("B2").Value = DateTime.Now
			worksheet.Cells("B3").Value = Math.PI
			worksheet.Cells("B4").Value = "Have a nice day!"
			worksheet.Cells("B5").Value = CellValue.ErrorReference
			worksheet.Cells("B6").Value = True
			worksheet.Cells("B7").Value = Single.MaxValue
			worksheet.Cells("B8").Value = "a"c
			worksheet.Cells("B9").Value = Int32.MaxValue

			' Fill all cells of the range with 10.
			worksheet.Range("B12:C12").Value = 10

			worksheet.Cells("A2").Value = "dateTime"
			worksheet.Cells("A3").Value = "double"
			worksheet.Cells("A4").Value = "string"
			worksheet.Cells("A5").Value = "error constant"
			worksheet.Cells("A6").Value = "boolean"
			worksheet.Cells("A7").Value = "float"
			worksheet.Cells("A8").Value = "char"
			worksheet.Cells("A9").Value = "int32"
			worksheet.Cells("A12").Value = "fill range"

			Dim header As Range = worksheet.Range("A1:B1")
			header(0).Value = "Type"
			header(1).Value = "Value"
			header.ColumnWidthInCharacters = 25
			header.Style = workbook.Styles("Header")

			workbook.Options.Culture = CultureInfo.InvariantCulture
'			#End Region ' #CellValue
		End Sub

		Private Shared Sub AddHyperlink(ByVal workbook As IWorkbook)
'			#Region "#AddHyperlink"
			Dim worksheet As Worksheet = workbook.Worksheets(0)
			Dim worksheet2 As Worksheet = workbook.Worksheets(1)
			worksheet.Range("A:B").ColumnWidthInCharacters = 12

			' Create a hyperlink to a web page.
			Dim cell As Cell = worksheet.Cells("A1")
			worksheet.Hyperlinks.Add(cell, "http://www.devexpress.com/", True, "DevExpress")

			' Create a hyperlink to a cell range in a workbook.
			Dim range As Range = worksheet.Range("C3:D4")
			Dim cellHyperlink As Hyperlink = worksheet.Hyperlinks.Add(range, "Sheet2!B2:E7", False, "Select Range")
			cellHyperlink.TooltipText = "Click Me"

			worksheet2.Hyperlinks.Add(worksheet2.Range("C9:D9"), "Sheet1!A1", False, "Back to Sheet1")
'			#End Region ' #AddHyperlink
		End Sub

		Private Shared Sub CopyCellDataAndStyle(ByVal workbook As IWorkbook)

'			#Region "#CopyCell"
			Dim worksheet As Worksheet = workbook.Worksheets(0)
			Dim style As Style = workbook.Styles(BuiltInStyleId.Input)

			' Specify the content and formatting for a source cell.
			worksheet.Cells("A1").Value = "Source Cell"
			Dim sourceCell As Cell = worksheet.Cells("B1")
			sourceCell.Formula = "= PI()"
			sourceCell.NumberFormat = "0.0000"
			sourceCell.Style = style
			sourceCell.Font.Color = DXColor.Blue
			sourceCell.Font.Bold = True
			sourceCell.Borders.SetOutsideBorders(DXColor.Black, BorderLineStyle.Thin)
			' Copy all information from the source cell to the "B3" cell. 
			worksheet.Cells("A3").Value = "Copy All"
			worksheet.Cells("B3").CopyFrom(sourceCell)
			' Copy only the source cell content (e.g., text, numbers, formula calculated values) to the "B4" cell.
			worksheet.Cells("A4").Value = "Copy Values"
			worksheet.Cells("B4").CopyFrom(sourceCell, PasteSpecial.Values)
			' Copy the source cell content (e.g., text, numbers, formula calculated values) 
			' and number formats to the "B5" cell.
			worksheet.Cells("A5").Value = "Copy Values and Number Formats"
			worksheet.Cells("B5").CopyFrom(sourceCell, PasteSpecial.Values Or PasteSpecial.NumberFormats)
			' Copy only the formatting information from the source cell to the "B6" cell.
			worksheet.Cells("A6").Value = "Copy Formats"
			worksheet.Cells("B6").CopyFrom(sourceCell, PasteSpecial.Formats)
			' Copy all information from the source cell to the "B7" cell except for border settings.
			worksheet.Cells("A7").Value = "Copy All Except Borders"
			worksheet.Cells("B7").CopyFrom(sourceCell, PasteSpecial.All And (Not PasteSpecial.Borders))
			' Copy information only about borders from the source cell to the "B8" cell.
			worksheet.Cells("A8").Value = "Copy Borders"
			worksheet.Cells("B8").CopyFrom(sourceCell, PasteSpecial.Borders)

			worksheet.Columns("A").AutoFit()
			worksheet.Columns("B").AutoFit()
'			#End Region ' #CopyCell
		End Sub

		Private Shared Sub MergeAndSplitCells(ByVal workbook As IWorkbook)
'			#Region "#MergeCells"
			Dim worksheet As Worksheet = workbook.Worksheets(0)
			worksheet.Cells("A1").FillColor = DXColor.LightGray
			worksheet.Cells("B2").Value = "B2"
			worksheet.Cells("B2").FillColor = DXColor.LightGreen
			worksheet.Cells("C3").Value = "C3"
			worksheet.Cells("C3").FillColor = DXColor.LightGreen

			' Merge cells contained in the range.
			Dim range As Range = worksheet.Range("A1:C5")
			range.Merge()
			'range.UnMerge();
'			#End Region ' #MergeCells
		End Sub

	End Class
End Namespace
