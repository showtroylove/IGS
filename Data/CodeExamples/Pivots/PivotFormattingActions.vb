Imports System
Imports System.Collections.Generic
Imports DevExpress.Spreadsheet

Namespace SpreadsheetExamples
	Public NotInheritable Class PivotFormattingActions

		Private Sub New()
		End Sub
		Private Shared Sub ChangeStylePivotTable(ByVal workbook As IWorkbook)
'			#Region "#Set Style"
			Dim worksheet As Worksheet = workbook.Worksheets("Report1")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Access the pivot table by its name in the collection
			Dim pivotTable As PivotTable = worksheet.PivotTables("PivotTable1")
			' Set the pivot table style
			pivotTable.Style = workbook.TableStyles(BuiltInPivotStyleId.PivotStyleDark7)

'			#End Region ' #Set Style
		End Sub


		Private Shared Sub BandedColumns(ByVal workbook As IWorkbook)
'			#Region "#Banded Columns"
			Dim worksheet As Worksheet = workbook.Worksheets("Report4")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Access the pivot table by its name in the collection
			Dim pivotTable As PivotTable = worksheet.PivotTables("PivotTable1")
			' Apply the banded column formatting to the pivot table 
			pivotTable.BandedColumns = True

'			#End Region ' #Banded Columns
		End Sub

		Private Shared Sub BandedRows(ByVal workbook As IWorkbook)
'			#Region "#Banded Rows"
			Dim worksheet As Worksheet = workbook.Worksheets("Report4")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Access the pivot table by its name in the collection
			Dim pivotTable As PivotTable = worksheet.PivotTables("PivotTable1")
			' Apply the banded row formatting to the pivot table 
			pivotTable.BandedRows = True

'			#End Region ' #Banded Rows
		End Sub

		Private Shared Sub ShowColumnHeaders(ByVal workbook As IWorkbook)
'			#Region "#Column Headers"
			Dim worksheet As Worksheet = workbook.Worksheets("Report1")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Access the pivot table by its name in the collection
			Dim pivotTable As PivotTable = worksheet.PivotTables("PivotTable1")
			' Remove formatting from column headers
			pivotTable.ShowColumnHeaders = False

'			#End Region ' #Column Headers
		End Sub

		Private Shared Sub ShowRowHeaders(ByVal workbook As IWorkbook)
'			#Region "#Row Headers"
			Dim worksheet As Worksheet = workbook.Worksheets("Report1")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Access the pivot table by its name in the collection
			Dim pivotTable As PivotTable = worksheet.PivotTables("PivotTable1")
			' Add the "Region" field to the column axis area 
			pivotTable.ColumnFields.Add(pivotTable.Fields("Region"))

			' Remove formatting from row headers
			pivotTable.ShowRowHeaders = False

'			#End Region ' #Row Headers
		End Sub
	End Class
End Namespace