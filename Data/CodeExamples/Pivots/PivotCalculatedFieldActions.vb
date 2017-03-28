Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports DevExpress.Spreadsheet

Namespace SpreadsheetExamples
	Public NotInheritable Class PivotCalculatedFieldActions

		Private Sub New()
		End Sub
		Private Shared Sub AddCalculatedField(ByVal workbook As IWorkbook)
'			#Region "#Add Calculated Field"
			Dim worksheet As Worksheet = workbook.Worksheets("Report1")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Access the pivot table by its name in the collection
			Dim pivotTable As PivotTable = worksheet.PivotTables("PivotTable1")
			' Create a calculated field based on data in the "Sales" field
			Dim field As PivotField = pivotTable.CalculatedFields.Add("=Sales*10%", "Sales Tax")
			' Add the calculated field to the data area and specify the custom field name
			Dim dataField As PivotDataField = pivotTable.DataFields.Add(field, "Total Tax")
			' Specify the number format for the data field
			dataField.NumberFormat = "_([$$-409]* #,##0.00_);_([$$-409]* (#,##0.00);_([$$-409]* "" - ""??_);_(@_)"
'			#End Region ' #Add Calculated Field
		End Sub

		Private Shared Sub RemoveCalculatedField(ByVal workbook As IWorkbook)
'			#Region "#Remove Calculated Field"
			Dim worksheet As Worksheet = workbook.Worksheets("Report1")
			workbook.Worksheets.ActiveWorksheet = worksheet
			' Access the pivot table by its name in the collection
			Dim pivotTable As PivotTable = worksheet.PivotTables("PivotTable1")
			' Create a calculated field based on data in the "Sales" field
			Dim field As PivotField = pivotTable.CalculatedFields.Add("=Sales*10%", "Sales Tax")
			' Add the calculated field to the data area
			pivotTable.DataFields.Add(field)
			'Remove the calculated field
			pivotTable.CalculatedFields.RemoveAt(0)
'			#End Region ' #Remove Calculated Field
		End Sub

		Private Shared Sub ModifyCalculatedField(ByVal workbook As IWorkbook)
'			#Region "#Modify Calculated Field"
			Dim worksheet As Worksheet = workbook.Worksheets("Report1")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Access the pivot table by its name in the collection
			Dim pivotTable As PivotTable = worksheet.PivotTables("PivotTable1")
			' Create a calculated field based on data in the "Sales" field
			pivotTable.CalculatedFields.Add("=Sales*10%", "Sales Tax Rate 10")
			' Access the calculated field by its name in the collection
			Dim field As PivotField = pivotTable.CalculatedFields("Sales Tax Rate 10")
			'Change the formula for the calculated field
			field.Formula = "=Sales*15%"
			'Change the calculated field name
			field.Name = "Sales Tax Rate 15"
			'Add the calculated field to the data area and specify the custom field name
			Dim dataField As PivotDataField = pivotTable.DataFields.Add(field, "Total Tax")
			' Specify the number format for the data field
			dataField.NumberFormat = "_([$$-409]* #,##0.00_);_([$$-409]* (#,##0.00);_([$$-409]* "" - ""??_);_(@_)"
'			#End Region ' #Modify Calculated Field
		End Sub
	End Class
End Namespace