Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports DevExpress.Spreadsheet

Namespace SpreadsheetExamples
	Public NotInheritable Class AutoFilterActions

		Private Sub New()
		End Sub
		Private Shared Sub ApplyFilter(ByVal workbook As IWorkbook)
'			#Region "#ApplyFilter"
			Dim worksheet As Worksheet = workbook.Worksheets("Regional sales")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Apply AutoFilter to a worksheet range.
			Dim range As Range = worksheet("B2:D9")
			worksheet.AutoFilter.Apply(range)

'			#End Region ' #ApplyFilter
		End Sub

		Private Shared Sub SortBySingleColumn(ByVal workbook As IWorkbook)
'			#Region "#SortBySingleColumn"
			Dim worksheet As Worksheet = workbook.Worksheets("Regional sales")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Apply AutoFilter to a worksheet range.
			Dim range As Range = worksheet("B2:D9")
			worksheet.AutoFilter.Apply(range)

			' Sort in descending order by the first column.
			worksheet.AutoFilter.SortState.Sort(0, True)

'			#End Region ' #SortBySingleColumn
		End Sub

		Private Shared Sub SortByMultipleColumns(ByVal workbook As IWorkbook)
'			#Region "#SortByMultipleColumns"
			Dim worksheet As Worksheet = workbook.Worksheets("Regional sales")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Apply AutoFilter to a worksheet range.
			Dim range As Range = worksheet("B2:D9")
			worksheet.AutoFilter.Apply(range)

			' Sort in descending order by the first and third columns.
			Dim sortConditions As New List(Of SortCondition)()
			sortConditions.Add(New SortCondition(0, True))
			sortConditions.Add(New SortCondition(2, True))
			worksheet.AutoFilter.SortState.Sort(sortConditions)

'			#End Region ' #SortByMultipleColumns
		End Sub

		Private Shared Sub FilterByCondition(ByVal workbook As IWorkbook)
'			#Region "#FilterByCondition"
			Dim worksheet As Worksheet = workbook.Worksheets("Regional sales")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Apply AutoFilter to a worksheet range.
			Dim range As Range = worksheet("B2:D9")
			worksheet.AutoFilter.Apply(range)

			' Filter the data based on the condition SALES > 5000.
			worksheet.AutoFilter.Columns(2).ApplyCustomFilter(5000, FilterComparisonOperator.GreaterThan)

'			#End Region ' #FilterByCondition
		End Sub

		Private Shared Sub FilterByValue(ByVal workbook As IWorkbook)
'			#Region "#FilterByValue"
			Dim worksheet As Worksheet = workbook.Worksheets("Regional sales")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Apply AutoFilter to a worksheet range.
			Dim range As Range = worksheet("B2:D9")
			worksheet.AutoFilter.Apply(range)

			' Filter the data by a specific value.
			worksheet.AutoFilter.Columns(1).ApplyFilterCriteria("Mozzarella di Giovanni")

'			#End Region ' #FilterByValue
		End Sub

		Private Shared Sub FilterByValues(ByVal workbook As IWorkbook)
'			#Region "#FilterByValues"
			Dim worksheet As Worksheet = workbook.Worksheets("Regional sales")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Apply AutoFilter to worksheet range
			Dim range As Range = worksheet("B2:D9")
			worksheet.AutoFilter.Apply(range)

			' Filter the data by an array of values.
			worksheet.AutoFilter.Columns(1).ApplyFilterCriteria(New CellValue() { "Mozzarella di Giovanni", "Gorgonzola Telino" })

'			#End Region ' #FilterByValues
		End Sub

		Private Shared Sub DynamicFilter(ByVal workbook As IWorkbook)
'			#Region "#DynamicFilter"
			Dim worksheet As Worksheet = workbook.Worksheets("Regional sales")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Apply AutoFilter to a worksheet range.
			Dim range As Range = worksheet("B2:D9")
			worksheet.AutoFilter.Apply(range)

			' Filter the data using dynamic criteria.
			worksheet.AutoFilter.Columns(2).ApplyDynamicFilter(DynamicFilterType.AboveAverage)

'			#End Region ' #DynamicFilter
		End Sub

		Private Shared Sub ReapplyFilter(ByVal workbook As IWorkbook)
'			#Region "#ReapplyFilter"
			Dim worksheet As Worksheet = workbook.Worksheets("Regional sales")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Apply AutoFilter to a worksheet range.
			Dim range As Range = worksheet("B2:D9")
			worksheet.AutoFilter.Apply(range)

			' Filter the data based on the condition SALES > 5000.
			worksheet.AutoFilter.Columns(2).ApplyCustomFilter(5000, FilterComparisonOperator.GreaterThan)

			' Change the data and reapply AutoFilter.
			worksheet("D3").Value = 5000
			worksheet.AutoFilter.ReApply()

'			#End Region ' #ReapplyFilter
		End Sub

		Private Shared Sub ClearFilter(ByVal workbook As IWorkbook)
'			#Region "#ClearFilter"
			Dim worksheet As Worksheet = workbook.Worksheets("Regional sales")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Apply AutoFilter to a worksheet range.
			Dim range As Range = worksheet("B2:D9")
			worksheet.AutoFilter.Apply(range)

			' Filter the data based on the condition SALES > 5000.
			worksheet.AutoFilter.Columns(2).ApplyCustomFilter(5000, FilterComparisonOperator.GreaterThan)

			' Clear the filter.
			worksheet.AutoFilter.Clear()

'			#End Region ' #ClearFilter
		End Sub

		Private Shared Sub DisableFilter(ByVal workbook As IWorkbook)
'			#Region "#DisableFilter"
			Dim worksheet As Worksheet = workbook.Worksheets("Regional sales")
			workbook.Worksheets.ActiveWorksheet = worksheet

			' Apply AutoFilter to a worksheet range.
			Dim range As Range = worksheet("B2:D9")
			worksheet.AutoFilter.Apply(range)

			' Disable AutoFilter for the worksheet.
			worksheet.AutoFilter.Disable()

'			#End Region ' #DisableFilter
		End Sub
	End Class
End Namespace