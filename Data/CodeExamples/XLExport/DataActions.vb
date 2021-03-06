Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports DevExpress.Export.Xl
Imports DevExpress.Spreadsheet

Namespace XLExportExamples
	Public NotInheritable Class DataActions

		Private Sub New()
		End Sub
		Private Shared Sub AutoFilter(ByVal stream As Stream, ByVal documentFormat As XlDocumentFormat)
'			#Region "#AutoFilter"
			' Create an exporter instance
			Dim exporter As IXlExporter = XlExport.CreateExporter(documentFormat)

			' Create a new document
			Using document As IXlDocument = exporter.CreateDocument(stream)
				document.Options.Culture = CultureInfo.CurrentCulture

				' Create a worksheet
				Using sheet As IXlSheet = document.CreateSheet()

					' Create worksheet columns, set their widths and number formats
					Using column As IXlColumn = sheet.CreateColumn()
						column.WidthInPixels = 100
					End Using
					Using column As IXlColumn = sheet.CreateColumn()
						column.WidthInPixels = 250
					End Using
					Using column As IXlColumn = sheet.CreateColumn()
						column.WidthInPixels = 100
						column.Formatting = New XlCellFormatting()
						column.Formatting.NumberFormat = "_([$$-409]* #,##0.00_);_([$$-409]* \(#,##0.00\);_([$$-409]* ""-""??_);_(@_)"
					End Using

					' Specify formatting settings for cells containing data
					Dim rowFormatting As New XlCellFormatting()
					rowFormatting.Font = New XlFont()
					rowFormatting.Font.Name = "Century Gothic"
					rowFormatting.Font.SchemeStyle = XlFontSchemeStyles.None

					' Specify formatting settings for the header row
					Dim headerRowFormatting As New XlCellFormatting()
					headerRowFormatting.CopyFrom(rowFormatting)
					headerRowFormatting.Font.Bold = True
					headerRowFormatting.Font.Color = XlColor.FromTheme(XlThemeColor.Light1, 0.0)
					headerRowFormatting.Fill = XlFill.SolidFill(XlColor.FromTheme(XlThemeColor.Accent2, 0.0))

					' Generate the header row
					Using row As IXlRow = sheet.CreateRow()
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Region"
							cell.ApplyFormatting(headerRowFormatting)
						End Using
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Product"
							cell.ApplyFormatting(headerRowFormatting)
						End Using
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Sales"
							cell.ApplyFormatting(headerRowFormatting)
						End Using
					End Using

					' Generate data for the document
					Dim products() As String = { "Camembert Pierrot", "Gorgonzola Telino", "Mascarpone Fabioli", "Mozzarella di Giovanni" }
					Dim amount() As Integer = { 6750, 4500, 3550, 4250, 5500, 6250, 5325, 4235 }
					For i As Integer = 0 To 7
						Using row As IXlRow = sheet.CreateRow()
							Using cell As IXlCell = row.CreateCell()
								cell.Value = If((i < 4), "East", "West")
								cell.ApplyFormatting(rowFormatting)
							End Using
							Using cell As IXlCell = row.CreateCell()
								cell.Value = products(i Mod 4)
								cell.ApplyFormatting(rowFormatting)
							End Using
							Using cell As IXlCell = row.CreateCell()
								cell.Value = amount(i)
								cell.ApplyFormatting(rowFormatting)
							End Using
						End Using
					Next i

					' Enable filtering for the data range
					sheet.AutoFilterRange = sheet.DataRange
				End Using
			End Using

'			#End Region ' #AutoFilter
		End Sub

		Private Shared Sub OutlineGrouping(ByVal stream As Stream, ByVal documentFormat As XlDocumentFormat)
'			#Region "#Group/Outline"
			' Create an exporter instance
			Dim exporter As IXlExporter = XlExport.CreateExporter(documentFormat)

			' Create a new document
			Using document As IXlDocument = exporter.CreateDocument(stream)
				document.Options.Culture = CultureInfo.CurrentCulture

				' Create a worksheet
				Using sheet As IXlSheet = document.CreateSheet()

					' Specify the summary row and summary column location for the grouped data
					sheet.OutlineProperties.SummaryBelow = True
					sheet.OutlineProperties.SummaryRight = True

					' Create the column "A" and set its width
					Using column As IXlColumn = sheet.CreateColumn()
						column.WidthInPixels = 200
					End Using

					' Begin to group worksheet columns starting from the column "B" to the column "E"
					sheet.BeginGroup(False)

					' Create four successive columns ("B", "C", "D" and "E") and set the specific number format for their cells
					For i As Integer = 0 To 3
						Using column As IXlColumn = sheet.CreateColumn()
							column.WidthInPixels = 100
							column.Formatting = New XlCellFormatting()
							column.Formatting.NumberFormat = "_([$$-409]* #,##0.00_);_([$$-409]* \(#,##0.00\);_([$$-409]* ""-""??_);_(@_)"
						End Using
					Next i

					' Finalize the group creation
					sheet.EndGroup()

					' Create the column "F", adjust its width and set the specific number format for its cells
					Using column As IXlColumn = sheet.CreateColumn()
						column.WidthInPixels = 100
						column.Formatting = New XlCellFormatting()
						column.Formatting.NumberFormat = "_([$$-409]* #,##0.00_);_([$$-409]* \(#,##0.00\);_([$$-409]* ""-""??_);_(@_)"
					End Using

					' Specify formatting settings for cells containing data
					Dim rowFormatting As New XlCellFormatting()
					rowFormatting.Font = XlFont.BodyFont()
					rowFormatting.Fill = XlFill.SolidFill(XlColor.FromTheme(XlThemeColor.Light1, 0.0))

					' Specify formatting settings for the header rows
					Dim headerRowFormatting As New XlCellFormatting()
					headerRowFormatting.Font = XlFont.BodyFont()
					headerRowFormatting.Font.Bold = True
					headerRowFormatting.Font.Color = XlColor.FromTheme(XlThemeColor.Light1, 0.0)
					headerRowFormatting.Fill = XlFill.SolidFill(XlColor.FromTheme(XlThemeColor.Accent1, 0.0))

					' Specify formatting settings for the total rows
					Dim totalRowFormatting As New XlCellFormatting()
					totalRowFormatting.Font = XlFont.BodyFont()
					totalRowFormatting.Font.Bold = True
					totalRowFormatting.Fill = XlFill.SolidFill(XlColor.FromTheme(XlThemeColor.Light2, 0.0))

					' Specify formatting settings for the grand total row
					Dim grandTotalRowFormatting As New XlCellFormatting()
					grandTotalRowFormatting.Font = XlFont.BodyFont()
					grandTotalRowFormatting.Font.Bold = True
					grandTotalRowFormatting.Fill = XlFill.SolidFill(XlColor.FromTheme(XlThemeColor.Light2, -0.2))

					' Generate data for the document
					Dim random As New Random()
					Dim products() As String = { "Camembert Pierrot", "Gorgonzola Telino", "Mascarpone Fabioli", "Mozzarella di Giovanni" }

					' Begin to group worksheet rows (create the outer group of rows)
					sheet.BeginGroup(False)
					For p As Integer = 0 To 1
						' Generate the header row
						Using row As IXlRow = sheet.CreateRow()
							Using cell As IXlCell = row.CreateCell()
								cell.Value = If((p = 0), "East", "West")
								cell.ApplyFormatting(headerRowFormatting)
								cell.Formatting.Fill = XlFill.SolidFill(XlColor.FromTheme(XlThemeColor.Accent2, 0.0))
							End Using
							For i As Integer = 0 To 3
								Using cell As IXlCell = row.CreateCell()
									cell.Value = String.Format("Q{0}", i + 1)
									cell.ApplyFormatting(headerRowFormatting)
									cell.ApplyFormatting(XlCellAlignment.FromHV(XlHorizontalAlignment.Right, XlVerticalAlignment.Bottom))
								End Using
							Next i
							Using cell As IXlCell = row.CreateCell()
								cell.Value = "Yearly total"
								cell.ApplyFormatting(headerRowFormatting)
								cell.ApplyFormatting(XlCellAlignment.FromHV(XlHorizontalAlignment.Right, XlVerticalAlignment.Bottom))
							End Using
						End Using

						' Create and group data rows (create the inner group of rows containing sales data for the specific region)
						sheet.BeginGroup(False)
						For i As Integer = 0 To 3
							Using row As IXlRow = sheet.CreateRow()
								Using cell As IXlCell = row.CreateCell()
									cell.Value = products(i)
									cell.ApplyFormatting(rowFormatting)
									cell.Formatting.Fill = XlFill.SolidFill(XlColor.FromTheme(XlThemeColor.Accent2, 0.8))
								End Using
								For j As Integer = 0 To 3
									Using cell As IXlCell = row.CreateCell()
										cell.Value = Math.Round(random.NextDouble() * 2000 + 3000)
										cell.ApplyFormatting(rowFormatting)
									End Using
								Next j
								Using cell As IXlCell = row.CreateCell()
									cell.SetFormula(XlFunc.Sum(XlCellRange.FromLTRB(1, row.RowIndex, 4, row.RowIndex)))
									cell.ApplyFormatting(rowFormatting)
									cell.ApplyFormatting(XlFill.SolidFill(XlColor.FromTheme(XlThemeColor.Light2, 0.0)))
								End Using
							End Using
						Next i
						' Finalize the group creation
						sheet.EndGroup()

						' Create the total row
						Using row As IXlRow = sheet.CreateRow()
							Using cell As IXlCell = row.CreateCell()
								cell.Value = "Total"
								cell.ApplyFormatting(totalRowFormatting)
								cell.Formatting.Fill = XlFill.SolidFill(XlColor.FromTheme(XlThemeColor.Accent2, 0.6))
							End Using
							For j As Integer = 0 To 4
								Using cell As IXlCell = row.CreateCell()
									cell.SetFormula(XlFunc.Subtotal(XlCellRange.FromLTRB(j + 1, row.RowIndex - 4, j + 1, row.RowIndex - 1), XlSummary.Sum, False))
									cell.ApplyFormatting(totalRowFormatting)
								End Using
							Next j
						End Using
					Next p
					' Finalize the group creation
					sheet.EndGroup()

					' Create the grand total row
					Using row As IXlRow = sheet.CreateRow()
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Grand total"
							cell.ApplyFormatting(grandTotalRowFormatting)
							cell.Formatting.Fill = XlFill.SolidFill(XlColor.FromTheme(XlThemeColor.Accent2, 0.4))
						End Using
						For j As Integer = 0 To 4
							Using cell As IXlCell = row.CreateCell()
								cell.SetFormula(XlFunc.Subtotal(XlCellRange.FromLTRB(j + 1, 1, j + 1, row.RowIndex - 1), XlSummary.Sum, False))
								cell.ApplyFormatting(grandTotalRowFormatting)
							End Using
						Next j
					End Using
				End Using
			End Using

'			#End Region ' #Group/Outline
		End Sub

		Private Shared Sub DataValidations(ByVal stream As Stream, ByVal documentFormat As XlDocumentFormat)
'			#Region "#DataValidation"
			' Create an exporter instance
			Dim exporter As IXlExporter = XlExport.CreateExporter(documentFormat, New XlFormulaParser())

			' Create a new document
			Using document As IXlDocument = exporter.CreateDocument(stream)
				document.Options.Culture = CultureInfo.CurrentCulture

				' Create a worksheet
				Using sheet As IXlSheet = document.CreateSheet()

					' Create worksheet columns and set their widths
					Using column As IXlColumn = sheet.CreateColumn()
						column.WidthInPixels = 110
						column.Formatting = XlCellAlignment.FromHV(XlHorizontalAlignment.Left, XlVerticalAlignment.Bottom)
					End Using
					Using column As IXlColumn = sheet.CreateColumn()
						column.WidthInPixels = 190
					End Using
					For i As Integer = 0 To 1
						Using column As IXlColumn = sheet.CreateColumn()
							column.WidthInPixels = 90
							column.Formatting = New XlCellFormatting()
							column.Formatting.NumberFormat = "_([$$-409]* #,##0.00_);_([$$-409]* \(#,##0.00\);_([$$-409]* ""-""??_);_(@_)"
						End Using
					Next i
					Using column As IXlColumn = sheet.CreateColumn()
						column.WidthInPixels = 130
					End Using

					sheet.SkipColumns(1)
					Using column As IXlColumn = sheet.CreateColumn()
						column.WidthInPixels = 130
					End Using

					' Specify formatting settings for cells containing data
					Dim rowFormatting As New XlCellFormatting()
					rowFormatting.Font = New XlFont()
					rowFormatting.Font.Name = "Century Gothic"
					rowFormatting.Font.SchemeStyle = XlFontSchemeStyles.None

					' Specify formatting settings for the header row
					Dim headerRowFormatting As New XlCellFormatting()
					headerRowFormatting.CopyFrom(rowFormatting)
					headerRowFormatting.Font.Bold = True
					headerRowFormatting.Font.Color = XlColor.FromTheme(XlThemeColor.Light1, 0.0)
					headerRowFormatting.Fill = XlFill.SolidFill(XlColor.FromTheme(XlThemeColor.Accent2, 0.0))

					' Generate the header row
					Using row As IXlRow = sheet.CreateRow()
						Dim columnNames() As String = { "Employee ID", "Employee name", "Salary", "Bonus", "Department" }
						row.BulkCells(columnNames, headerRowFormatting)
						row.SkipCells(1)
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Departments"
							cell.ApplyFormatting(headerRowFormatting)
						End Using
					End Using

					' Generate data for the document
					Dim id() As Integer = { 10115, 10709, 10401, 10204 }
					Dim name() As String = { "Augusta Delono", "Chris Cadwell", "Frank Diamond", "Simon Newman" }
					Dim salary() As Integer = { 1100, 2000, 1750, 1250 }
					Dim bonus() As Integer = { 50, 180, 100, 80 }
					Dim deptid() As Integer = { 0, 2, 3, 3 }
					Dim department() As String = { "Accounting", "IT", "Management", "Manufacturing" }
					For i As Integer = 0 To 3
						Using row As IXlRow = sheet.CreateRow()
							Using cell As IXlCell = row.CreateCell()
								cell.Value = id(i)
								cell.ApplyFormatting(rowFormatting)
							End Using
							Using cell As IXlCell = row.CreateCell()
								cell.Value = name(i)
								cell.ApplyFormatting(rowFormatting)
							End Using
							Using cell As IXlCell = row.CreateCell()
								cell.Value = salary(i)
								cell.ApplyFormatting(rowFormatting)
							End Using
							Using cell As IXlCell = row.CreateCell()
								cell.Value = bonus(i)
								cell.ApplyFormatting(rowFormatting)
							End Using
							Using cell As IXlCell = row.CreateCell()
								cell.Value = department(deptid(i))
								cell.ApplyFormatting(rowFormatting)
							End Using
							row.SkipCells(1)
							Using cell As IXlCell = row.CreateCell()
								cell.Value = department(i)
								cell.ApplyFormatting(rowFormatting)
							End Using
						End Using
					Next i

					' Apply data validation to cells

					' Restrict data entry in the range A2:A5 to a 5-digit number
					Dim validation As New XlDataValidation()
					validation.Ranges.Add(XlCellRange.FromLTRB(0, 1, 0, 4)) ' A2:A5
					validation.Type = XlDataValidationType.Custom
					validation.Criteria1 = "=AND(ISNUMBER(A2),LEN(A2)=5)"
					sheet.DataValidations.Add(validation)

					' Restrict data entry in the cell range C2:C5 to a whole number between 600 and 2000
					validation = New XlDataValidation()
					validation.Ranges.Add(XlCellRange.FromLTRB(2, 1, 2, 4)) ' C2:C5
					validation.Type = XlDataValidationType.Whole
					validation.Operator = XlDataValidationOperator.Between
					validation.Criteria1 = 600
					validation.Criteria2 = 2000
					' Setup the error message
					validation.ErrorMessage = "Salary can be set in the range $600-$2000."
					validation.ErrorTitle = "Warning"
					validation.ErrorStyle = XlDataValidationErrorStyle.Warning
					' Setup the input prompt
					validation.InputPrompt = "Please enter whole number in range 600...2000"
					validation.PromptTitle = "Salary"
					validation.ShowErrorMessage = True
					validation.ShowInputMessage = True
					sheet.DataValidations.Add(validation)

					' Restrict data entry in the cell range D2:D5 to a decimal number within the specified limits
					' Bonus cannot be greater than 10% of the salary
					validation = New XlDataValidation()
					validation.Ranges.Add(XlCellRange.FromLTRB(3, 1, 3, 4)) ' D2:D5
					validation.Type = XlDataValidationType.Decimal
					validation.Operator = XlDataValidationOperator.Between
					validation.Criteria1 = 0
					' Use a formula to specify the validation criterion
					validation.Criteria2 = "=C2*0.1"
					' Setup the error message
					validation.ErrorMessage = "Bonus cannot be greater than 10% of the salary."
					validation.ErrorTitle = "Information"
					validation.ErrorStyle = XlDataValidationErrorStyle.Information
					validation.ShowErrorMessage = True
					sheet.DataValidations.Add(validation)

					' Restrict data entry in the cell range E2:E5 to values in a drop-down list obtained from the cells G2:G5
					validation = New XlDataValidation()
					validation.Ranges.Add(XlCellRange.FromLTRB(4, 1, 4, 4)) ' E2:E5
					validation.Type = XlDataValidationType.List
					validation.Criteria1 = XlCellRange.FromLTRB(6, 1, 6, 4).AsAbsolute() ' $G$2:$G$5
					sheet.DataValidations.Add(validation)
				End Using
			End Using

'			#End Region ' #DataValidation
		End Sub

	End Class
End Namespace