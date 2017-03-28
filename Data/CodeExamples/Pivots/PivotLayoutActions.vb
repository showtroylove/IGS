Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports DevExpress.Spreadsheet

Namespace SpreadsheetExamples
	Public NotInheritable Class PivotLayoutActions

		Private Sub New()
		End Sub
		Private Shared Sub ColumnGrandTotals(ByVal workbook As IWorkbook)
'			#Region "#Column Grand Totals"
			Dim worksheet As Worksheet = workbook.Worksheets("Report1")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Access the pivot table by its name in the collection
			Dim pivotTable As PivotTable = worksheet.PivotTables("PivotTable1")
			' Add the "Region" field to the column axis area 
			pivotTable.ColumnFields.Add(pivotTable.Fields("Region"))

			' Hide grand totals for columns
			pivotTable.Layout.ShowColumnGrandTotals = False

'			#End Region ' #Column Grand Totals
		End Sub

		Private Shared Sub RowGrandTotals(ByVal workbook As IWorkbook)
'			#Region "#Row Grand Totals"
			Dim worksheet As Worksheet = workbook.Worksheets("Report1")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Access the pivot table by its name in the collection
			Dim pivotTable As PivotTable = worksheet.PivotTables("PivotTable1")
			' Add the "Region" field to the column axis area. 
			pivotTable.ColumnFields.Add(pivotTable.Fields("Region"))
			' Hide grand totals for rows
			pivotTable.Layout.ShowRowGrandTotals = False

'			#End Region ' #Row Grand Totals
		End Sub

		Private Shared Sub DataOnRows(ByVal workbook As IWorkbook)
'			#Region "#Multiple data fields"
			Dim worksheet As Worksheet = workbook.Worksheets("Report2")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Access the pivot table by its name in the collection
			Dim pivotTable As PivotTable = worksheet.PivotTables("PivotTable1")
			' Display value fields in separate columns
			pivotTable.Layout.DataOnRows = False

'			#End Region ' #Multiple data fields
		End Sub
		Private Shared Sub MergeTitles(ByVal workbook As IWorkbook)
'			#Region "#Merge Titles"
			Dim worksheet As Worksheet = workbook.Worksheets("Report4")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Access the pivot table by its name in the collection
			Dim pivotTable As PivotTable = worksheet.PivotTables("PivotTable1")
			' Display the pivot table in the tabular form
			pivotTable.Layout.SetReportLayout(PivotReportLayout.Tabular)
			' Merge and center cells with labels 
			pivotTable.Layout.MergeTitles = True

'			#End Region ' #Merge Titles
		End Sub

		Private Shared Sub ShowAllSubtotals(ByVal workbook As IWorkbook)
'			#Region "#Show All Subtotals"
			Dim worksheet As Worksheet = workbook.Worksheets("Report1")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Access the pivot table by its name in the collection
			Dim pivotTable As PivotTable = worksheet.PivotTables("PivotTable1")
			' Show all subtotals at the top of each group
			pivotTable.Layout.ShowAllSubtotals(True)

'			#End Region ' #Show All Subtotals
		End Sub

		Private Shared Sub HideAllSubtotals(ByVal workbook As IWorkbook)
'			#Region "#Hide All Subtotals"
			Dim worksheet As Worksheet = workbook.Worksheets("Report1")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Access the pivot table by its name in the collection
			Dim pivotTable As PivotTable = worksheet.PivotTables("PivotTable1")
			' Hide subtotals at the top of each group
			pivotTable.Layout.HideAllSubtotals()

'			#End Region ' #Hide All Subtotals
		End Sub

		Private Shared Sub SetCompactReportLayout(ByVal workbook As IWorkbook)
'			#Region "#Compact Report Layout"
			Dim worksheet As Worksheet = workbook.Worksheets("Report1")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Access the pivot table by its name in the collection
			Dim pivotTable As PivotTable = worksheet.PivotTables("PivotTable1")
			' Display the pivot table in the compact form
			pivotTable.Layout.SetReportLayout(PivotReportLayout.Compact)

'			#End Region ' #Compact Report Layout
		End Sub

		Private Shared Sub SetOutlineReportLayout(ByVal workbook As IWorkbook)
'			#Region "#Outline Report Layout"
			Dim worksheet As Worksheet = workbook.Worksheets("Report1")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Access the pivot table by its name in the collection
			Dim pivotTable As PivotTable = worksheet.PivotTables("PivotTable1")
			' Display the pivot table in the outline form
			pivotTable.Layout.SetReportLayout(PivotReportLayout.Outline)

'			#End Region ' #Outline Report Layout
		End Sub

		Private Shared Sub SetTabularReportLayout(ByVal workbook As IWorkbook)
'			#Region "#Tabular Report Layout"
			Dim worksheet As Worksheet = workbook.Worksheets("Report1")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Access the pivot table by its name in the collection
			Dim pivotTable As PivotTable = worksheet.PivotTables("PivotTable1")
			' Display the pivot table in the tabular form
			pivotTable.Layout.SetReportLayout(PivotReportLayout.Tabular)

'			#End Region ' #Tabular Report Layout
		End Sub

		Private Shared Sub RepeatAllItemLabels(ByVal workbook As IWorkbook)
'			#Region "#Repeat All Item Labels"
			Dim worksheet As Worksheet = workbook.Worksheets("Report5")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Access the pivot table by its name in the collection
			Dim pivotTable As PivotTable = worksheet.PivotTables("PivotTable1")
			' Display repeated column labels
			pivotTable.Layout.RepeatAllItemLabels(True)

'			#End Region ' #Repeat All Item Labels
		End Sub

		Private Shared Sub InsertBlankRows(ByVal workbook As IWorkbook)
'			#Region "#Insert Blank Rows"
			Dim worksheet As Worksheet = workbook.Worksheets("Report1")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Access the pivot table by its name in the collection
			Dim pivotTable As PivotTable = worksheet.PivotTables("PivotTable1")
			' Insert a blank row after each group of items
			pivotTable.Layout.InsertBlankRows()

'			#End Region ' #Insert Blank Rows
		End Sub

		Private Shared Sub RemoveBlankRows(ByVal workbook As IWorkbook)
'			#Region "#Remove Blank Rows"
			Dim worksheet As Worksheet = workbook.Worksheets("Report1")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Access the pivot table by its name in the collection
			Dim pivotTable As PivotTable = worksheet.PivotTables("PivotTable1")
			' Insert a blank row after each group of items
			pivotTable.Layout.InsertBlankRows()

			' Remove a blank row after each group of items
			pivotTable.Layout.RemoveBlankRows()

'			#End Region ' #Remove Blank Rows
		End Sub
	End Class
End Namespace