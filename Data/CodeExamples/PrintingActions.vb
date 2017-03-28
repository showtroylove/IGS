Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Spreadsheet
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraPrinting.Control

Namespace SpreadsheetExamples
	Public NotInheritable Class PrintingActions

		Private Sub New()
		End Sub
		Private Shared Sub Print(ByVal workbook As IWorkbook)
'			#Region "WorksheetPrintOptions"
			Dim worksheet As Worksheet = workbook.Worksheets(0)
			worksheet.Cells("A1").Value = "Printing Example"
			' Access an object providing print options.
			Dim printOptions As WorksheetPrintOptions = workbook.Worksheets(0).PrintOptions

			' TODO
'			#End Region ' WorksheetPrintOptions

'			#Region "PrintWorksheet"
			Dim firstSheet As Worksheet = workbook.Worksheets(0)
			Dim table As Table = firstSheet.Tables.Add(firstSheet("A1:H30"), False)
			table.Style = workbook.TableStyles(BuiltInTableStyleId.TableStyleLight14)
			table.ShowTotals = True
			table.Columns(0).TotalRowLabel = "Total"
'			#End Region ' PrintWorksheet
		End Sub
		Private Shared Sub Print2(ByVal workbook As IWorkbook)
'			#Region "#PrintWorkbook"

			Dim firstSheet As Worksheet = workbook.Worksheets(0)
			Dim table As Table = firstSheet.Tables.Add(firstSheet("A1:H30"), False)
			table.Style = workbook.TableStyles(BuiltInTableStyleId.TableStyleMedium14)
			table.ShowTotals = True
			table.Columns(0).TotalRowLabel = "Total"

			Dim secondSheet As Worksheet = workbook.Worksheets(1)
			Dim table2 As Table = secondSheet.Tables.Add(secondSheet("A1:H30"), False)
			table2.Style = workbook.TableStyles(BuiltInTableStyleId.TableStyleDark4)
			table2.ShowTotals = True
			table2.Columns(0).TotalRowLabel = "Total"

			' Create printing components.
			Dim printControl As New PrintControl()
			Dim printingSystem As New PrintingSystem()
			Dim link As New PrintableComponentLink()

			' Assign a workbook to be printed by the link.
			link.Component = workbook
			' Add the link to the printing system's collection of links.
			printingSystem.Links.Add(link)
			' Assign the PrintingSystem to the PrintControl.
			printControl.PrintingSystem = printingSystem

			' Show the Print Preview for the workbook.
			link.ShowPreview()
'			#End Region ' #PrintWorkbook
		End Sub
	End Class
End Namespace
