Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports DevExpress.Spreadsheet

Namespace SpreadsheetExamples
	Public NotInheritable Class PivotTableActions

		Private Sub New()
		End Sub
		Private Shared Sub CreatePivotTableFromRange(ByVal workbook As IWorkbook)
'			#Region "#Create from Range"
			Dim sourceWorksheet As Worksheet = workbook.Worksheets("Data1")
			Dim worksheet As Worksheet = workbook.Worksheets.Add()
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Create a pivot table using the cell range "A1:D41" as the data source
			Dim pivotTable As PivotTable = worksheet.PivotTables.Add(sourceWorksheet("A1:D41"), worksheet("B2"))

			' Add the "Category" field to the row axis area
			pivotTable.RowFields.Add(pivotTable.Fields("Category"))
			' Add the "Product" field to the row axis area
			pivotTable.RowFields.Add(pivotTable.Fields("Product"))
			' Add the "Sales" field to the data area
			pivotTable.DataFields.Add(pivotTable.Fields("Sales"))

			' Set the default style for the pivot table
			pivotTable.Style = workbook.TableStyles.DefaultPivotStyle

'			#End Region ' #Create from Range
		End Sub

		Private Shared Sub CreatePivotTableFromCache(ByVal workbook As IWorkbook)
'			#Region "#Create from PivotCache"
			Dim worksheet As Worksheet = workbook.Worksheets.Add()
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Create a pivot table based on the specified PivotTable cache
			Dim cache As PivotCache = workbook.Worksheets("Report1").PivotTables("PivotTable1").Cache
			Dim pivotTable As PivotTable = worksheet.PivotTables.Add(cache, worksheet("B2"))

			' Add the "Category" field to the row axis area
			pivotTable.RowFields.Add(pivotTable.Fields("Category"))
			' Add the "Product" field to the row axis area
			pivotTable.RowFields.Add(pivotTable.Fields("Product"))
			' Add the "Sales" field to the data area
			pivotTable.DataFields.Add(pivotTable.Fields("Sales"))

			' Set the default style for the pivot table
			pivotTable.Style = workbook.TableStyles.DefaultPivotStyle

'			#End Region ' #Create from PivotCache
		End Sub

		Private Shared Sub RemovePivotTable(ByVal workbook As IWorkbook)
'			#Region "#Remove Table"
			Dim worksheet As Worksheet = workbook.Worksheets("Report1")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Access the pivot table by its name in the collection
			Dim pivotTable As PivotTable = worksheet.PivotTables("PivotTable1")
			' Remove the pivot table from the collection
			worksheet.PivotTables.Remove(pivotTable)

'			#End Region ' #Remove Table
		End Sub
		Private Shared Sub ChangePivotTableLocation(ByVal workbook As IWorkbook)
'			#Region "#Change Location"
			Dim worksheet As Worksheet = workbook.Worksheets("Report1")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Change pivot table location
			worksheet.PivotTables("PivotTable1").MoveTo(worksheet("A7"))

'			#End Region ' #Change Location
		End Sub
		Private Shared Sub MovePivotTableToWorksheet(ByVal workbook As IWorkbook)
'			#Region "#Move to Worksheet"
			Dim worksheet As Worksheet = workbook.Worksheets("Report1")

			' Create new worksheet
			Dim targetWorksheet As Worksheet = workbook.Worksheets.Add()

			' Access the pivot table by its name in the collection
			' and move it to the new worksheet
			worksheet.PivotTables("PivotTable1").MoveTo(targetWorksheet("B2"))
			' Refresh the moved pivot table
			targetWorksheet.PivotTables("PivotTable1").Cache.Refresh()
			workbook.Worksheets.ActiveWorksheet = targetWorksheet
			
'			#End Region ' #Move to Worksheet
		End Sub

		Private Shared Sub ChangePivotTableDataSource(ByVal workbook As IWorkbook)
'			#Region "#Change DataSource"
			Dim worksheet As Worksheet = workbook.Worksheets("Report1")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Access the pivot table by its name in the collection
			Dim pivotTable As PivotTable = worksheet.PivotTables("PivotTable1")
			Dim sourceWorksheet As Worksheet = workbook.Worksheets("Data2")
			' Change the data source of the pivot table
			pivotTable.ChangeDataSource(sourceWorksheet("A1:H6367"))

			' Add the "State" field to the row axis area
			pivotTable.RowFields.Add(pivotTable.Fields("State"))
			' Add the "Yearly Earnings" field to the data area
			Dim dataField As PivotDataField = pivotTable.DataFields.Add(pivotTable.Fields("Yearly Earnings"))
			dataField.SummarizeValuesBy = PivotDataConsolidationFunction.Average

'			#End Region ' #Change DataSource
		End Sub

		Private Shared Sub ClearPivotTable(ByVal workbook As IWorkbook)
'			#Region "#Clear Table"
			Dim worksheet As Worksheet = workbook.Worksheets("Report1")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Clear the pivot table
			worksheet.PivotTables("PivotTable1").Clear()

'			#End Region ' #Clear Table
		End Sub

		Private Shared Sub ChangeBehaviorOptions(ByVal workbook As IWorkbook)
'			#Region "#Change Behavior Options"
			Dim worksheet As Worksheet = workbook.Worksheets("Report1")
			workbook.Worksheets.ActiveWorksheet = worksheet
			worksheet.Columns("B").WidthInCharacters = 40

			' Access the pivot table by its name in the collection
			Dim pivotTable As PivotTable = worksheet.PivotTables("PivotTable1")

			' Restrict an end-user's ability to modify the pivot table
			Dim behaviorOptions As PivotBehaviorOptions = pivotTable.Behavior
			behaviorOptions.AutoFitColumns = False
			behaviorOptions.EnableFieldList = False

			' Refresh pivot table
			pivotTable.Cache.Refresh()

'			#End Region ' #Change Behavior Options
		End Sub
	End Class
End Namespace