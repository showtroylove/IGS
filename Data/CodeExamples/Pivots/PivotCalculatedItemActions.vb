Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports DevExpress.Spreadsheet

Namespace SpreadsheetExamples
	Public NotInheritable Class PivotCalculatedItemActions

		Private Sub New()
		End Sub
		Shared Sub AddCalculatedItem(ByVal workbook As IWorkbook)
'			#Region "#Add Calculated Item"
			Dim worksheet As Worksheet = workbook.Worksheets("Report10")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Access the pivot table by its name in the collection
			Dim pivotTable As PivotTable = worksheet.PivotTables("PivotTable1")

			' Access the pivot field by its name in the collection
			Dim field As PivotField = pivotTable.Fields("State")

			' Add calculated items to the "State" field
			field.CalculatedItems.Add("=Arizona+California+Colorado", "West Total")
			field.CalculatedItems.Add("=Illinois+Kansas+Wisconsin", "Midwest Total")
'			#End Region ' #Add Calculated Item
		End Sub
		
		Private Shared Sub RemoveCalculatedItem(ByVal workbook As IWorkbook)
'			#Region "#Remove Calculated Item"
			Dim worksheet As Worksheet = workbook.Worksheets("Report7")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Access the pivot table by its name in the collection
			Dim pivotTable As PivotTable = worksheet.PivotTables("PivotTable1")

			' Access the pivot field by its name in the collection
			Dim field As PivotField = pivotTable.Fields("Customer")

			'Remove the calculated item by its index from the collection
			field.CalculatedItems.RemoveAt(0)
'			#End Region ' #Remove Calculated Item
		End Sub

		Private Shared Sub ModifyCalculatedItem(ByVal workbook As IWorkbook)
'			#Region "#Modify Calculated Item"
			Dim worksheet As Worksheet = workbook.Worksheets("Report7")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Access the pivot table by its name in the collection
			Dim pivotTable As PivotTable = worksheet.PivotTables("PivotTable1")

			' Access the pivot field by its name in the collection
			Dim field As PivotField = pivotTable.Fields("Customer")

			' Access the calculated item by its index in the collection
			Dim item As PivotItem = field.CalculatedItems(0)

			'Change the formula for the calculated item
			item.Formula = "='Big Foods'*115%"

'			#End Region ' #Modify Calculated Item
		End Sub
	End Class
End Namespace