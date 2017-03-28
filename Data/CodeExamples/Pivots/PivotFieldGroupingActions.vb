Imports System.Collections.Generic
Imports DevExpress.Spreadsheet

Namespace SpreadsheetExamples
	Public NotInheritable Class PivotFieldGroupingActions
	
		Private Sub New()
		End Sub
		Private Shared Sub GroupFieldItems(ByVal workbook As IWorkbook)
'			#Region "#Group Field Items"
			Dim worksheet As Worksheet = workbook.Worksheets("Report11")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Access the pivot table by its name in the collection.
			Dim pivotTable As PivotTable = worksheet.PivotTables("PivotTable1")
			' Access the "State" field by its name in the collection.
			Dim field As PivotField = pivotTable.Fields("State")
			' Add the "State" field to the column axis area.
			pivotTable.ColumnFields.Add(field)

			' Group the first three items in the field.
			Dim items As IEnumerable(Of Integer) = New List(Of Integer) (New Integer() {0, 1, 2})
			field.GroupItems(items)
			' Access the created grouped field by its index in the field collection.
			Dim groupedFieldIndex As Integer = pivotTable.Fields.Count - 1
			Dim groupedField As PivotField = pivotTable.Fields(groupedFieldIndex)
			' Set the grouped item caption to "West".
			groupedField.Items(0).Caption = "West"
'			#End Region ' #Group Field Items
		End Sub

		Private Shared Sub GroupFieldByDates(ByVal workbook As IWorkbook)
'			#Region "#Group Field by Dates"
			Dim worksheet As Worksheet = workbook.Worksheets("Report8")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Access the pivot table by its name in the collection
			Dim pivotTable As PivotTable = worksheet.PivotTables("PivotTable1")
			' Access the "DATE" field by its name in the collection
			Dim field As PivotField = pivotTable.Fields("DATE")
			' Group field items by quarters and months
			field.GroupItems(PivotFieldGroupByType.Quarters Or PivotFieldGroupByType.Months)
'			#End Region ' #Group Field by Dates
		End Sub

		Private Shared Sub GroupFieldByNumericRanges(ByVal workbook As IWorkbook)
'			#Region "#Group Field by Numeric Ranges"
			Dim worksheet As Worksheet = workbook.Worksheets("Report9")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Access the pivot table by its name in the collection
			Dim pivotTable As PivotTable = worksheet.PivotTables("PivotTable1")
			' Access the "Usual Hours Worked" field by its name in the collection
			Dim field As PivotField = pivotTable.Fields("Usual Hours Worked")
			' Group field items from 0 to 150 by 10
			field.GroupItems(0, 150, 10, PivotFieldGroupByType.NumericRanges)
'			#End Region ' #Group Field by Numeric Ranges
		End Sub

		Shared Sub UngroupSpecificItem(ByVal workbook As IWorkbook)
'			#Region "#Ungroup Specific Item"
			Dim worksheet As Worksheet = workbook.Worksheets("Report11")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Access the pivot table by its name in the collection
			Dim pivotTable As PivotTable = worksheet.PivotTables("PivotTable1")
			' Access the "State" field by its name in the collection.
			Dim field As PivotField = pivotTable.Fields("State")
			' Add the "State" field to the column axis area.
			pivotTable.ColumnFields.Add(field)

			' Group the first three items in the field.
			Dim items As IEnumerable(Of Integer) = New List(Of Integer) (New Integer() {0, 1, 2})
			field.GroupItems(items)
			' Access the created grouped field by its index in the field collection.
			Dim groupedFieldIndex As Integer = pivotTable.Fields.Count - 1
			Dim groupedField As PivotField = pivotTable.Fields(groupedFieldIndex)
			' Set the grouped item caption to "West".
			groupedField.Items(0).Caption = "West"

			' Group the remaining field items.
			items = New List(Of Integer) (New Integer() {3, 4, 5})
			field.GroupItems(items)
			' Set the grouped item caption to "Midwest"
			groupedField.Items(1).Caption = "Midwest"

			' Ungroup the "West" item.
			items = New List(Of Integer) (New Integer() {0})
			groupedField.UngroupItems(items)
'			#End Region ' #Ungroup Specific Item
		End Sub

		Shared Sub UngroupFieldItems(ByVal workbook As IWorkbook)
'			#Region "#Ungroup Field Items"
			Dim worksheet As Worksheet = workbook.Worksheets("Report8")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Access the pivot table by its name in the collection.
			Dim pivotTable As PivotTable = worksheet.PivotTables("PivotTable1")
			' Access the "DATE" field by its name in the collection.
			Dim field As PivotField = pivotTable.Fields("DATE")
			' Group field items by days.
			field.GroupItems(field.GroupingInfo.DefaultStartValue, field.GroupingInfo.DefaultEndValue, 50, PivotFieldGroupByType.Days)
			' Ungroup field items.
			field.UngroupItems()
'			#End Region ' #Ungroup Field Items
		End Sub
	End Class
End Namespace
