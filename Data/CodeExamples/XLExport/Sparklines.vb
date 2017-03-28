Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Text
Imports DevExpress.Export.Xl
Imports DevExpress.XtraExport.Csv
Imports DevExpress.Spreadsheet

Namespace XLExportExamples
	Public NotInheritable Class Sparklines

		Private Sub New()
		End Sub
		Private Shared Sub AddSparklinesGroup(ByVal stream As Stream, ByVal documentFormat As XlDocumentFormat, ByVal imagesPath As String)
'			#Region "#AddSparklinesGroup"
			' Create an exporter instance
			Dim exporter As IXlExporter = XlExport.CreateExporter(documentFormat)

			' Create a new document
			Using document As IXlDocument = exporter.CreateDocument(stream)
				document.Options.Culture = CultureInfo.CurrentCulture

				' Create a worksheet
				Using sheet As IXlSheet = document.CreateSheet()

					' Create worksheet columns and set their widths
					Using column As IXlColumn = sheet.CreateColumn()
						column.WidthInPixels = 200
					End Using
					For i As Integer = 0 To 4
						Using column As IXlColumn = sheet.CreateColumn()
							column.WidthInPixels = 100
							column.ApplyFormatting(CType("_([$$-409]* #,##0.00_);_([$$-409]* \(#,##0.00\);_([$$-409]* ""-""??_);_(@_)", XlNumberFormat))
						End Using
					Next i

					' Specify formatting settings for cells containing data
					Dim rowFormatting As New XlCellFormatting()
					rowFormatting.Font = XlFont.CustomFont("Century Gothic", 12.0)

					' Specify formatting settings for the header row
					Dim headerRowFormatting As New XlCellFormatting()
					headerRowFormatting.Font = XlFont.CustomFont("Century Gothic", 12.0)
					headerRowFormatting.Font.Bold = True
					headerRowFormatting.Font.Color = XlColor.FromTheme(XlThemeColor.Light1, 0.0)
					headerRowFormatting.Fill = XlFill.SolidFill(XlColor.FromTheme(XlThemeColor.Accent1, 0.0))

					Dim columnNames() As String = { "Product", "Q1", "Q2", "Q3", "Q4" }

					Using row As IXlRow = sheet.CreateRow()
						row.BulkCells(columnNames, headerRowFormatting)
					End Using

					' Generate data for the document
					Dim random As New Random()
					Dim products() As String = { "HD Video Player", "SuperLED 42", "SuperLED 50", "DesktopLED 19", "DesktopLED 21", "Projector Plus HD" }

					For Each product As String In products
						Using row As IXlRow = sheet.CreateRow()
							Using cell As IXlCell = row.CreateCell()
								cell.Value = product
								cell.ApplyFormatting(rowFormatting)
							End Using
							For j As Integer = 0 To 3
								Using cell As IXlCell = row.CreateCell()
									cell.Value = Math.Round(random.NextDouble() * 2000 + 3000)
									cell.ApplyFormatting(rowFormatting)
								End Using
							Next j
						End Using
					Next product

					' Create a group of line sparklines
					Dim group As New XlSparklineGroup(XlCellRange.FromLTRB(1, 1, 4, 6), XlCellRange.FromLTRB(5, 1, 5, 6))
					' Set the sparkline weight to 1.25pt
					group.LineWeight = 1.25
					' Display data markers on the sparklines
					group.DisplayMarkers = True
					sheet.SparklineGroups.Add(group)

				End Using
			End Using

'			#End Region ' #AddSparklinesGroup
		End Sub

		Private Shared Sub AddSparklines(ByVal stream As Stream, ByVal documentFormat As XlDocumentFormat, ByVal imagesPath As String)
'			#Region "#AddSparklines"
			' Create an exporter instance
			Dim exporter As IXlExporter = XlExport.CreateExporter(documentFormat)

			' Create a new document
			Using document As IXlDocument = exporter.CreateDocument(stream)
				document.Options.Culture = CultureInfo.CurrentCulture

				' Create a worksheet
				Using sheet As IXlSheet = document.CreateSheet()
					' Create worksheet columns and set their widths
					Using column As IXlColumn = sheet.CreateColumn()
						column.WidthInPixels = 200
					End Using
					For i As Integer = 0 To 4
						Using column As IXlColumn = sheet.CreateColumn()
							column.WidthInPixels = 100
							column.ApplyFormatting(CType("_([$$-409]* #,##0.00_);_([$$-409]* \(#,##0.00\);_([$$-409]* ""-""??_);_(@_)", XlNumberFormat))
						End Using
					Next i

					' Specify formatting settings for cells containing data
					Dim rowFormatting As New XlCellFormatting()
					rowFormatting.Font = XlFont.CustomFont("Century Gothic", 12.0)

					' Specify formatting settings for the header row
					Dim headerRowFormatting As New XlCellFormatting()
					headerRowFormatting.Font = XlFont.CustomFont("Century Gothic", 12.0)
					headerRowFormatting.Font.Bold = True
					headerRowFormatting.Font.Color = XlColor.FromTheme(XlThemeColor.Light1, 0.0)
					headerRowFormatting.Fill = XlFill.SolidFill(XlColor.FromTheme(XlThemeColor.Accent1, 0.0))

					' Create the header row
					Dim columnNames() As String = { "Product", "Q1", "Q2", "Q3", "Q4" }

					Using row As IXlRow = sheet.CreateRow()
						row.BulkCells(columnNames, headerRowFormatting)
					End Using

					' Create a group of line sparklines
					Dim group As New XlSparklineGroup()
					' Set the sparkline color
					group.ColorSeries = XlColor.FromTheme(XlThemeColor.Accent1, -0.2)
					' Set the sparkline weight to 1.25pt
					group.LineWeight = 1.25
					sheet.SparklineGroups.Add(group)

					' Generate data for the document
					Dim random As New Random()
					Dim products() As String = { "HD Video Player", "SuperLED 42", "SuperLED 50", "DesktopLED 19", "DesktopLED 21", "Projector Plus HD" }

					For Each product As String In products
						Using row As IXlRow = sheet.CreateRow()
							Using cell As IXlCell = row.CreateCell()
								cell.Value = product
								cell.ApplyFormatting(rowFormatting)
							End Using
							For j As Integer = 0 To 3
								Using cell As IXlCell = row.CreateCell()
									cell.Value = Math.Round(random.NextDouble() * 2000 + 3000)
									cell.ApplyFormatting(rowFormatting)
								End Using
							Next j

							' Add one more sparkline to the existing group
							Dim rowIndex As Integer = row.RowIndex
							group.Sparklines.Add(New XlSparkline(XlCellRange.FromLTRB(1, rowIndex, 4, rowIndex), XlCellRange.FromLTRB(5, rowIndex, 5, rowIndex)))
						End Using
					Next product
				End Using
			End Using

'			#End Region ' #AddSparklines
		End Sub

		Private Shared Sub AdjustScaling(ByVal stream As Stream, ByVal documentFormat As XlDocumentFormat, ByVal imagesPath As String)
'			#Region "#AdjustScaling"
			' Create an exporter instance
			Dim exporter As IXlExporter = XlExport.CreateExporter(documentFormat)

			' Create a new document
			Using document As IXlDocument = exporter.CreateDocument(stream)
				document.Options.Culture = CultureInfo.CurrentCulture

				' Create a worksheet
				Using sheet As IXlSheet = document.CreateSheet()
					' Create worksheet columns and set their widths
					Using column As IXlColumn = sheet.CreateColumn()
						column.WidthInPixels = 200
					End Using
					For i As Integer = 0 To 4
						Using column As IXlColumn = sheet.CreateColumn()
							column.WidthInPixels = 100
							column.ApplyFormatting(CType("_([$$-409]* #,##0.00_);_([$$-409]* \(#,##0.00\);_([$$-409]* ""-""??_);_(@_)", XlNumberFormat))
						End Using
					Next i

					' Specify formatting settings for cells containing data
					Dim rowFormatting As New XlCellFormatting()
					rowFormatting.Font = XlFont.CustomFont("Century Gothic", 12.0)

					' Specify formatting settings for the header row
					Dim headerRowFormatting As New XlCellFormatting()
					headerRowFormatting.Font = XlFont.CustomFont("Century Gothic", 12.0)
					headerRowFormatting.Font.Bold = True
					headerRowFormatting.Font.Color = XlColor.FromTheme(XlThemeColor.Light1, 0.0)
					headerRowFormatting.Fill = XlFill.SolidFill(XlColor.FromTheme(XlThemeColor.Accent1, 0.0))

					' Create the header row
					Dim columnNames() As String = { "Product", "Q1", "Q2", "Q3", "Q4" }

					Using row As IXlRow = sheet.CreateRow()
						row.BulkCells(columnNames, headerRowFormatting)
					End Using

					' Create data rows
					Dim random As New Random()
					Dim products() As String = { "HD Video Player", "SuperLED 42", "SuperLED 50", "DesktopLED 19", "DesktopLED 21", "Projector Plus HD" }

					For Each product As String In products
						Using row As IXlRow = sheet.CreateRow()
							Using cell As IXlCell = row.CreateCell()
								cell.Value = product
								cell.ApplyFormatting(rowFormatting)
							End Using
							For j As Integer = 0 To 3
								Using cell As IXlCell = row.CreateCell()
									cell.Value = Math.Round(random.NextDouble() * 2000 + 1500)
									cell.ApplyFormatting(rowFormatting)
								End Using
							Next j
						End Using
					Next product

					' Create a sparkline group
					Dim group As New XlSparklineGroup(XlCellRange.FromLTRB(1, 1, 4, 6), XlCellRange.FromLTRB(5, 1, 5, 6))
					' Set the sparkline color
					group.ColorSeries = XlColor.FromTheme(XlThemeColor.Accent1, 0.0)
					' Change the sparkline group type to "Column"
					group.SparklineType = XlSparklineType.Column
					' Set the custom minimum value for the vertical axis
					group.MinScaling = XlSparklineAxisScaling.Custom
					group.ManualMin = 1000.0
					' Set the automatic maximum value for all sparklines in the group
					group.MaxScaling = XlSparklineAxisScaling.Group
					sheet.SparklineGroups.Add(group)

				End Using
			End Using

'			#End Region ' #AdjustScaling
		End Sub

		Private Shared Sub HighlightValues(ByVal stream As Stream, ByVal documentFormat As XlDocumentFormat, ByVal imagesPath As String)
'			#Region "#HighlightValues"
			' Create an exporter instance
			Dim exporter As IXlExporter = XlExport.CreateExporter(documentFormat)

			' Create a new document
			Using document As IXlDocument = exporter.CreateDocument(stream)
				document.Options.Culture = CultureInfo.CurrentCulture

				' Create a worksheet
				Using sheet As IXlSheet = document.CreateSheet()
					' Create worksheet columns and set their widths
					Using column As IXlColumn = sheet.CreateColumn()
						column.WidthInPixels = 200
					End Using
					For i As Integer = 0 To 8
						Using column As IXlColumn = sheet.CreateColumn()
							column.WidthInPixels = 100
							column.ApplyFormatting(CType("_([$$-409]* #,##0.00_);_([$$-409]* \(#,##0.00\);_([$$-409]* ""-""??_);_(@_)", XlNumberFormat))
						End Using
					Next i

					' Specify formatting settings for cells containing data
					Dim rowFormatting As New XlCellFormatting()
					rowFormatting.Font = XlFont.CustomFont("Century Gothic", 12.0)

					' Specify formatting settings for the header row
					Dim headerRowFormatting As XlCellFormatting = rowFormatting.Clone()
					headerRowFormatting.Font.Bold = True
					headerRowFormatting.Font.Color = XlColor.FromTheme(XlThemeColor.Light1, 0.0)
					headerRowFormatting.Fill = XlFill.SolidFill(XlColor.FromTheme(XlThemeColor.Accent1, 0.0))

					' Create the header row
					Dim columnNames() As String = { "State", "Q1'13", "Q2'13", "Q3'13", "Q4'13", "Q1'14", "Q2'14", "Q3'14", "Q4'14" }

					Using row As IXlRow = sheet.CreateRow()
						row.BulkCells(columnNames, headerRowFormatting)
					End Using

					' Create data rows
					Dim random As New Random()
					Dim products() As String = { "Alabama", "Arizona", "California", "Colorado", "Connecticut", "Florida" }

					For Each product As String In products
						Using row As IXlRow = sheet.CreateRow()
							Using cell As IXlCell = row.CreateCell()
								cell.Value = product
								cell.ApplyFormatting(rowFormatting)
							End Using
							For j As Integer = 0 To 7
								Using cell As IXlCell = row.CreateCell()
									cell.Value = Math.Round((random.NextDouble() + 0.5) * 2000 * Math.Sign(random.NextDouble() - 0.4))
									cell.ApplyFormatting(rowFormatting)
								End Using
							Next j
						End Using
					Next product

					' Create a sparkline group
					Dim group As New XlSparklineGroup(XlCellRange.FromLTRB(1, 1, 8, 6), XlCellRange.FromLTRB(9, 1, 9, 6))
					' Change the sparkline group type to "Column"
					group.SparklineType = XlSparklineType.Column
					' Set the series color
					group.ColorSeries = XlColor.FromTheme(XlThemeColor.Accent1, 0.0)
					' Set the color for negative points on sparklines
					group.ColorNegative = XlColor.FromTheme(XlThemeColor.Accent2, 0.0)
					' Set the color for the highest points on sparklines
					group.ColorHigh = XlColor.FromTheme(XlThemeColor.Accent6, 0.0)
					' Highlight the highest and negative points on each sparkline in the group
					group.HighlightNegative = True
					group.HighlightHighest = True
					sheet.SparklineGroups.Add(group)

				End Using
			End Using

'			#End Region ' #HighlightValues
		End Sub

		Private Shared Sub DisplayXAxis(ByVal stream As Stream, ByVal documentFormat As XlDocumentFormat, ByVal imagesPath As String)
'			#Region "#DisplayXAxis"
			' Create an exporter instance
			Dim exporter As IXlExporter = XlExport.CreateExporter(documentFormat)

			' Create a new document
			Using document As IXlDocument = exporter.CreateDocument(stream)
				document.Options.Culture = CultureInfo.CurrentCulture

				' Create a worksheet
				Using sheet As IXlSheet = document.CreateSheet()
					' Create worksheet columns and set their widths
					Using column As IXlColumn = sheet.CreateColumn()
						column.WidthInPixels = 200
					End Using
					For i As Integer = 0 To 8
						Using column As IXlColumn = sheet.CreateColumn()
							column.WidthInPixels = 100
							column.ApplyFormatting(CType("_([$$-409]* #,##0.00_);_([$$-409]* \(#,##0.00\);_([$$-409]* ""-""??_);_(@_)", XlNumberFormat))
						End Using
					Next i

					' Specify formatting settings for cells containing data
					Dim rowFormatting As New XlCellFormatting()
					rowFormatting.Font = XlFont.CustomFont("Century Gothic", 12.0)

					' Specify formatting settings for the header row
					Dim headerRowFormatting As XlCellFormatting = rowFormatting.Clone()
					headerRowFormatting.Font.Bold = True
					headerRowFormatting.Font.Color = XlColor.FromTheme(XlThemeColor.Light1, 0.0)
					headerRowFormatting.Fill = XlFill.SolidFill(XlColor.FromTheme(XlThemeColor.Accent1, 0.0))

					' Create the header row
					Dim columnNames() As String = { "State", "Q1'13", "Q2'13", "Q3'13", "Q4'13", "Q1'14", "Q2'14", "Q3'14", "Q4'14" }

					Using row As IXlRow = sheet.CreateRow()
						row.BulkCells(columnNames, headerRowFormatting)
					End Using

					' Create data rows
					Dim random As New Random()
					Dim products() As String = { "Alabama", "Arizona", "California", "Colorado", "Connecticut", "Florida" }

					For Each product As String In products
						Using row As IXlRow = sheet.CreateRow()
							Using cell As IXlCell = row.CreateCell()
								cell.Value = product
								cell.ApplyFormatting(rowFormatting)
							End Using
							For j As Integer = 0 To 7
								Using cell As IXlCell = row.CreateCell()
									cell.Value = Math.Round((random.NextDouble() + 0.5) * 2000 * Math.Sign(random.NextDouble() - 0.4))
									cell.ApplyFormatting(rowFormatting)
								End Using
							Next j
						End Using
					Next product

					' Create a sparkline group
					Dim group As New XlSparklineGroup(XlCellRange.FromLTRB(1, 1, 8, 6), XlCellRange.FromLTRB(9, 1, 9, 6))
					' Change the sparkline group type to "Column"
					group.SparklineType = XlSparklineType.Column
					' Display the horizontal axis
					group.DisplayXAxis = True
					' Set the series color
					group.ColorSeries = XlColor.FromTheme(XlThemeColor.Accent1, 0.0)
					' Highlight negative points on each sparkline in the group
					group.ColorNegative = XlColor.FromTheme(XlThemeColor.Accent2, 0.0)
					group.HighlightNegative = True
					sheet.SparklineGroups.Add(group)

				End Using
			End Using

'			#End Region ' #DisplayXAxis
		End Sub

		Private Shared Sub SetupDateRange(ByVal stream As Stream, ByVal documentFormat As XlDocumentFormat, ByVal imagesPath As String)
'			#Region "#SetupDateRange"
			' Create an exporter instance
			Dim exporter As IXlExporter = XlExport.CreateExporter(documentFormat)

			' Create a new document
			Using document As IXlDocument = exporter.CreateDocument(stream)
				document.Options.Culture = CultureInfo.CurrentCulture

				' Create a worksheet
				Using sheet As IXlSheet = document.CreateSheet()
					' Create worksheet columns and set their widths
					Using column As IXlColumn = sheet.CreateColumn()
						column.WidthInPixels = 200
					End Using
					For i As Integer = 0 To 4
						Using column As IXlColumn = sheet.CreateColumn()
							column.WidthInPixels = 100
							column.ApplyFormatting(CType("_([$$-409]* #,##0.00_);_([$$-409]* \(#,##0.00\);_([$$-409]* ""-""??_);_(@_)", XlNumberFormat))
						End Using
					Next i

					' Specify formatting settings for cells containing data
					Dim rowFormatting As New XlCellFormatting()
					rowFormatting.Font = XlFont.CustomFont("Century Gothic", 12.0)

					' Specify formatting settings for the header row
					Dim headerRowFormatting As New XlCellFormatting()
					headerRowFormatting.Font = XlFont.CustomFont("Century Gothic", 12.0)
					headerRowFormatting.Font.Bold = True
					headerRowFormatting.Font.Color = XlColor.FromTheme(XlThemeColor.Light1, 0.0)
					headerRowFormatting.Fill = XlFill.SolidFill(XlColor.FromTheme(XlThemeColor.Accent1, 0.0))
					headerRowFormatting.NumberFormat = XlNumberFormat.ShortDate

					' Create the header row
					Dim headerValues() As Object = { "Product", New DateTime(2015, 10, 1), New DateTime(2015, 10, 10), New DateTime(2015, 10, 15), New DateTime(2015, 10, 25) }

					Using row As IXlRow = sheet.CreateRow()
						row.BulkCells(headerValues, headerRowFormatting)
					End Using

					' Create data rows
					Dim random As New Random()
					Dim products() As String = { "HD Video Player", "SuperLED 42", "SuperLED 50", "DesktopLED 19", "DesktopLED 21", "Projector Plus HD" }

					For Each product As String In products
						Using row As IXlRow = sheet.CreateRow()
							Using cell As IXlCell = row.CreateCell()
								cell.Value = product
								cell.ApplyFormatting(rowFormatting)
							End Using
							For j As Integer = 0 To 3
								Using cell As IXlCell = row.CreateCell()
									cell.Value = Math.Round(random.NextDouble() * 2000 + 3000)
									cell.ApplyFormatting(rowFormatting)
								End Using
							Next j
						End Using
					Next product

					' Create a group of line sparklines
					Dim group As New XlSparklineGroup(XlCellRange.Parse("B2:E7"), XlCellRange.Parse("F2:F7"))
					' Specify the date range for the sparkline group 
					group.DateRange = XlCellRange.Parse("B1:E1")
					' Set the sparkline weight to 1.25pt
					group.LineWeight = 1.25
					' Display data markers on the sparklines
					group.DisplayMarkers = True
					sheet.SparklineGroups.Add(group)

				End Using
			End Using

'			#End Region ' #SetupDateRange
		End Sub

	End Class
End Namespace