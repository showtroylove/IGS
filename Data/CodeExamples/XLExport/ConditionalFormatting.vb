Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports DevExpress.Export.Xl
Imports DevExpress.Spreadsheet

Namespace XLExportExamples
	Public NotInheritable Class ConditionalFormatting

		Private Sub New()
		End Sub
		Private Shared Sub Average(ByVal stream As Stream, ByVal documentFormat As XlDocumentFormat)
'			#Region "#AverageRule"
			' Create an exporter instance
			Dim exporter As IXlExporter = XlExport.CreateExporter(documentFormat)

			' Create a new document
			Using document As IXlDocument = exporter.CreateDocument(stream)
				document.Options.Culture = CultureInfo.CurrentCulture

				' Create a worksheet
				Using sheet As IXlSheet = document.CreateSheet()

					' Generate data for the document
					For i As Integer = 0 To 10
						Using row As IXlRow = sheet.CreateRow()
							For j As Integer = 0 To 3
								Using cell As IXlCell = row.CreateCell()
									cell.Value = i + 1
								End Using
							Next j
						End Using
					Next i

					' Create an instance of the XlConditionalFormatting class
					Dim formatting As New XlConditionalFormatting()

					' Specify the cell range to which the conditional formatting rule should be applied (A1:A11)
					formatting.Ranges.Add(XlCellRange.FromLTRB(0, 0, 0, 10))

					' Create the rule highlighting values that are above the average in the cell range
					Dim rule As New XlCondFmtRuleAboveAverage()
					rule.Condition = XlCondFmtAverageCondition.Above
					rule.Formatting = XlCellFormatting.Bad
					formatting.Rules.Add(rule)

					' Add the specified format options to the worksheet collection of conditional formats
					sheet.ConditionalFormattings.Add(formatting)

					' Create an instance of the XlConditionalFormatting class
					formatting = New XlConditionalFormatting()

					' Specify the cell range to which the conditional formatting rule should be applied (B1:B11)
					formatting.Ranges.Add(XlCellRange.FromLTRB(1, 0, 1, 10))

					' Create the rule highlighting values that are above or equal to the average value in the cell range
					rule = New XlCondFmtRuleAboveAverage()
					rule.Condition = XlCondFmtAverageCondition.AboveOrEqual
					rule.Formatting = XlCellFormatting.Bad
					formatting.Rules.Add(rule)

					' Add the specified format options to the worksheet collection of conditional formats
					sheet.ConditionalFormattings.Add(formatting)

					' Create an instance of the XlConditionalFormatting class
					formatting = New XlConditionalFormatting()

					' Specify the cell range to which the conditional formatting rule should be applied (C1:C11)
					formatting.Ranges.Add(XlCellRange.FromLTRB(2, 0, 2, 10))

					' Create the rule highlighting values that are below the average in the cell range
					rule = New XlCondFmtRuleAboveAverage()
					rule.Condition = XlCondFmtAverageCondition.Below
					rule.Formatting = XlCellFormatting.Bad
					formatting.Rules.Add(rule)

					' Add the specified format options to the worksheet collection of conditional formats
					sheet.ConditionalFormattings.Add(formatting)

					' Create an instance of the XlConditionalFormatting class
					formatting = New XlConditionalFormatting()

					' Specify the cell range to which the conditional formatting rule should be applied (D1:D11)
					formatting.Ranges.Add(XlCellRange.FromLTRB(3, 0, 3, 10))

					' Create the rule highlighting values that are below or equal to the average in the cell range
					rule = New XlCondFmtRuleAboveAverage()
					rule.Condition = XlCondFmtAverageCondition.BelowOrEqual
					rule.Formatting = XlCellFormatting.Bad
					formatting.Rules.Add(rule)

					' Add the specified format options to the worksheet collection of conditional formats
					sheet.ConditionalFormattings.Add(formatting)
				End Using
			End Using

'			#End Region ' #AverageRule
		End Sub

		Private Shared Sub CellIs(ByVal stream As Stream, ByVal documentFormat As XlDocumentFormat)
'			#Region "#CellIsRule"
			' Create an exporter instance
			Dim exporter As IXlExporter = XlExport.CreateExporter(documentFormat, New XlFormulaParser())

			' Create a new document
			Using document As IXlDocument = exporter.CreateDocument(stream)
				document.Options.Culture = CultureInfo.CurrentCulture

				' Create a worksheet
				Using sheet As IXlSheet = document.CreateSheet()

					' Generate data for the document
					For i As Integer = 0 To 10
						Using row As IXlRow = sheet.CreateRow()
							Using cell As IXlCell = row.CreateCell()
								cell.Value = i + 1
							End Using
							Using cell As IXlCell = row.CreateCell()
								cell.Value = 12 - i
							End Using
						End Using
					Next i

					' Create an instance of the XlConditionalFormatting class
					Dim formatting As New XlConditionalFormatting()

					' Specify the cell range to which the conditional formatting rules should be applied (A1:A11)
					formatting.Ranges.Add(XlCellRange.FromLTRB(0, 0, 0, 10))

					' Create the rule to highlight cells whose values are less than 5
					Dim rule As New XlCondFmtRuleCellIs()
					rule.Operator = XlCondFmtOperator.LessThan
					rule.Formatting = XlCellFormatting.Bad
					rule.Value = 5
					formatting.Rules.Add(rule)

					' Create the rule to highlight cells whose values are between 5 and 8
					rule = New XlCondFmtRuleCellIs()
					rule.Operator = XlCondFmtOperator.Between
					rule.Formatting = XlCellFormatting.Neutral
					rule.Value = 5
					rule.SecondValue = 8
					formatting.Rules.Add(rule)

					' Create the rule to highlight cells whose values are greater than 8
					rule = New XlCondFmtRuleCellIs()
					rule.Operator = XlCondFmtOperator.GreaterThan
					rule.Formatting = XlCellFormatting.Good
					rule.Value = 8
					formatting.Rules.Add(rule)

					' Add the specified format options to the worksheet collection of conditional formats
					sheet.ConditionalFormattings.Add(formatting)

					' Create an instance of the XlConditionalFormatting class
					formatting = New XlConditionalFormatting()

					' Specify the cell range to which the conditional formatting rule should be applied (B1:B11)
					formatting.Ranges.Add(XlCellRange.FromLTRB(1, 0, 1, 10))

					' Create the rule to highlight cells whose values are greater than a value calculated by a formula
					rule = New XlCondFmtRuleCellIs()
					rule.Operator = XlCondFmtOperator.GreaterThan
					rule.Formatting = XlCellFormatting.Bad
					rule.Value = "=$A1+3"
					formatting.Rules.Add(rule)

					' Add the specified format options to the worksheet collection of conditional formats
					sheet.ConditionalFormattings.Add(formatting)
				End Using
			End Using

'			#End Region ' #CellIsRule
		End Sub

		Private Shared Sub Blanks(ByVal stream As Stream, ByVal documentFormat As XlDocumentFormat)
'			#Region "#BlanksRule"
			' Create an exporter instance
			Dim exporter As IXlExporter = XlExport.CreateExporter(documentFormat)

			' Create a new document
			Using document As IXlDocument = exporter.CreateDocument(stream)
				document.Options.Culture = CultureInfo.CurrentCulture

				' Create a worksheet
				Using sheet As IXlSheet = document.CreateSheet()

					' Generate data for the document
					For i As Integer = 0 To 9
						Using row As IXlRow = sheet.CreateRow()
							Using cell As IXlCell = row.CreateCell()
								If (i Mod 2) = 0 Then
									cell.Value = i + 1
								End If
							End Using
						End Using
					Next i

					' Create an instance of the XlConditionalFormatting class 
					Dim formatting As New XlConditionalFormatting()

					' Specify the cell range to which the conditional formatting rules should be applied (A1:A10)
					formatting.Ranges.Add(XlCellRange.FromLTRB(0, 0, 0, 9))

					' Create the rule to highlight blank cells in the range
					Dim rule As New XlCondFmtRuleBlanks(True)
					rule.Formatting = XlCellFormatting.Bad
					formatting.Rules.Add(rule)

					' Create the rule to highlight non-blank cells in the range
					rule = New XlCondFmtRuleBlanks(False)
					rule.Formatting = XlCellFormatting.Good
					formatting.Rules.Add(rule)

					' Add the specified format options to the worksheet collection of conditional formats
					sheet.ConditionalFormattings.Add(formatting)
				End Using
			End Using

'			#End Region ' #BlanksRule
		End Sub

		Private Shared Sub Duplicates(ByVal stream As Stream, ByVal documentFormat As XlDocumentFormat)
'			#Region "#DuplicatesRule"
			' Create an exporter instance
			Dim exporter As IXlExporter = XlExport.CreateExporter(documentFormat)

			' Create a new document
			Using document As IXlDocument = exporter.CreateDocument(stream)
				document.Options.Culture = CultureInfo.CurrentCulture

				' Create a worksheet
				Using sheet As IXlSheet = document.CreateSheet()

					' Generate data for the document
					For i As Integer = 0 To 10
						Using row As IXlRow = sheet.CreateRow()
							For j As Integer = 0 To 3
								Using cell As IXlCell = row.CreateCell()
									cell.Value = cell.ColumnIndex * cell.RowIndex + cell.RowIndex + 1
								End Using
							Next j
						End Using
					Next i

					' Create an instance of the XlConditionalFormatting class
					Dim formatting As New XlConditionalFormatting()

					' Specify the cell range to which the conditional formatting rules should be applied (A1:D11)
					formatting.Ranges.Add(XlCellRange.FromLTRB(0, 0, 3, 10))

					' Create the rule to identify duplicate values in the cell range
					formatting.Rules.Add(New XlCondFmtRuleDuplicates() With {.Formatting = XlCellFormatting.Bad})

					' Create the rule to identify unique values in the cell range
					formatting.Rules.Add(New XlCondFmtRuleUnique() With {.Formatting = XlCellFormatting.Good})

					' Add the specified format options to the worksheet collection of conditional formats
					sheet.ConditionalFormattings.Add(formatting)
				End Using
			End Using

'			#End Region ' #DuplicatesRule
		End Sub

		Private Shared Sub Expression(ByVal stream As Stream, ByVal documentFormat As XlDocumentFormat)
'			#Region "#ExpressionRule"
			' Create an exporter instance
			Dim exporter As IXlExporter = XlExport.CreateExporter(documentFormat, New XlFormulaParser())

			' Create a new document
			Using document As IXlDocument = exporter.CreateDocument(stream)
				document.Options.Culture = CultureInfo.CurrentCulture

				' Create a worksheet
				Using sheet As IXlSheet = document.CreateSheet()

					' Generate data for the document
					Dim width() As Integer = { 80, 150, 90 }
					For i As Integer = 0 To 2
						Using column As IXlColumn = sheet.CreateColumn()
							column.WidthInPixels = width(i)
							If i = 2 Then
								column.Formatting = New XlCellFormatting()
								column.Formatting.NumberFormat = "[$$-409] #,##0.00"
							End If
						End Using
					Next i
					Dim columnNames() As String = { "Account ID", "User Name", "Balance" }
					Using row As IXlRow = sheet.CreateRow()
						Dim headerRowFormatting As New XlCellFormatting()
						headerRowFormatting.Font = XlFont.BodyFont()
						headerRowFormatting.Font.Bold = True
						headerRowFormatting.Border = New XlBorder()
						headerRowFormatting.Border.BottomColor = Color.Black
						headerRowFormatting.Border.BottomLineStyle = XlBorderLineStyle.Thin
						For i As Integer = 0 To 2
							Using cell As IXlCell = row.CreateCell()
								cell.Value = columnNames(i)
								cell.ApplyFormatting(headerRowFormatting)
							End Using
						Next i
					End Using
					Dim accountIds() As String = { "A105", "A114", "B013", "C231", "D101", "D105" }
					Dim users() As String = { "Berry Dafoe", "Chris Cadwell", "Esta Mangold", "Liam Bell", "Simon Newman", "Wendy Underwood" }
					Dim balance() As Integer = { 155, 250, 48, 350, -15, 10 }
					For i As Integer = 0 To 5
						Using row As IXlRow = sheet.CreateRow()
							Using cell As IXlCell = row.CreateCell()
								cell.Value = accountIds(i)
							End Using
							Using cell As IXlCell = row.CreateCell()
								cell.Value = users(i)
							End Using
							Using cell As IXlCell = row.CreateCell()
								cell.Value = balance(i)
							End Using
						End Using
					Next i

					' Create an instance of the XlConditionalFormatting class 
					Dim formatting As New XlConditionalFormatting()

					' Specify the cell range to which the conditional formatting rules should be applied (A2:C7)
					formatting.Ranges.Add(XlCellRange.FromLTRB(0, 1, 2, 6))

					' Create the rule that uses a formula to highlight cells if a value in the column "C" is greater than 0 and less than 50
					Dim rule As New XlCondFmtRuleExpression("AND($C2>0,$C2<50)")
					rule.Formatting = XlFill.SolidFill(Color.FromArgb(&Hff, &Hff, &Hcc))
					formatting.Rules.Add(rule)

					' Create the rule that uses a formula to highlight cells if a value in the column "C" is less than or equal to 0 
					rule = New XlCondFmtRuleExpression("$C2<=0")
					rule.Formatting = XlCellFormatting.Bad
					formatting.Rules.Add(rule)

					' Add the specified format options to the worksheet collection of conditional formats
					sheet.ConditionalFormattings.Add(formatting)
				End Using
			End Using

'			#End Region ' #ExpressionRule
		End Sub

		Private Shared Sub SpecificText(ByVal stream As Stream, ByVal documentFormat As XlDocumentFormat)
'			#Region "#SpecificTextRule"
			' Create an exporter instance
			Dim exporter As IXlExporter = XlExport.CreateExporter(documentFormat)

			' Create a new document
			Using document As IXlDocument = exporter.CreateDocument(stream)
				document.Options.Culture = CultureInfo.CurrentCulture

				' Create a worksheet
				Using sheet As IXlSheet = document.CreateSheet()

					' Generate data for the document
					Dim width() As Integer = { 250, 180, 100 }
					For i As Integer = 0 To 2
						Using column As IXlColumn = sheet.CreateColumn()
							column.WidthInPixels = width(i)
							If i = 2 Then
								column.Formatting = New XlCellFormatting()
								column.Formatting.NumberFormat = "_([$$-409]* #,##0.00_);_([$$-409]* \(#,##0.00\);_([$$-409]* ""-""??_);_(@_)"
							End If
						End Using
					Next i
					Dim columnNames() As String = { "Product", "Delivery", "Sales" }
					Using row As IXlRow = sheet.CreateRow()
						Dim headerRowFormatting As New XlCellFormatting()
						headerRowFormatting.Font = XlFont.BodyFont()
						headerRowFormatting.Font.Bold = True
						headerRowFormatting.Border = New XlBorder()
						headerRowFormatting.Border.BottomColor = Color.Black
						headerRowFormatting.Border.BottomLineStyle = XlBorderLineStyle.Thin
						For i As Integer = 0 To 2
							Using cell As IXlCell = row.CreateCell()
								cell.Value = columnNames(i)
								cell.ApplyFormatting(headerRowFormatting)
							End Using
						Next i
					End Using
					Dim products() As String = { "Camembert Pierrot", "Gorgonzola Telino", "Mascarpone Fabioli", "Mozzarella di Giovanni", "Queso Cabrales", "Raclette Courdavault" }
					Dim deliveries() As String = { "USA", "Worldwide", "USA", "Ships worldwide", "Worldwide except EU", "EU" }
					Dim sales() As Integer = { 15500, 20250, 12634, 35010, 15234, 10050 }
					For i As Integer = 0 To 5
						Using row As IXlRow = sheet.CreateRow()
							Using cell As IXlCell = row.CreateCell()
								cell.Value = products(i)
							End Using
							Using cell As IXlCell = row.CreateCell()
								cell.Value = deliveries(i)
							End Using
							Using cell As IXlCell = row.CreateCell()
								cell.Value = sales(i)
							End Using
						End Using
					Next i

					' Create an instance of the XlConditionalFormatting class 
					Dim formatting As New XlConditionalFormatting()

					' Specify the cell range to which the conditional formatting rule should be applied (B2:B7)
					formatting.Ranges.Add(XlCellRange.FromLTRB(1, 1, 1, 6))

					' Create the rule to highlight cells that contain the given text
					Dim rule As New XlCondFmtRuleSpecificText(XlCondFmtSpecificTextType.Contains, "worldwide")
					rule.Formatting = XlCellFormatting.Neutral
					formatting.Rules.Add(rule)

					' Add the specified format options to the worksheet collection of conditional formats
					sheet.ConditionalFormattings.Add(formatting)
				End Using
			End Using

'			#End Region ' #SpecificTextRule
		End Sub

		Private Shared Sub TimePeriod(ByVal stream As Stream, ByVal documentFormat As XlDocumentFormat)
'			#Region "#TimePeriodRule"
			' Create an exporter instance
			Dim exporter As IXlExporter = XlExport.CreateExporter(documentFormat)

			' Create a new document
			Using document As IXlDocument = exporter.CreateDocument(stream)
				document.Options.Culture = CultureInfo.CurrentCulture

				' Create a worksheet
				Using sheet As IXlSheet = document.CreateSheet()

					' Generate data for the document
					Using column As IXlColumn = sheet.CreateColumn()
						column.WidthInPixels = 100
						column.ApplyFormatting(XlNumberFormat.ShortDate)
					End Using
					For i As Integer = 0 To 9
						Using row As IXlRow = sheet.CreateRow()
							Using cell As IXlCell = row.CreateCell()
								cell.Value = DateTime.Now.AddDays(row.RowIndex - 5)
							End Using
						End Using
					Next i

					' Create an instance of the XlConditionalFormatting class 
					Dim formatting As New XlConditionalFormatting()

					' Specify the cell range to which the conditional formatting rules should be applied (A1:A10)
					formatting.Ranges.Add(XlCellRange.FromLTRB(0, 0, 0, 9))

					' Create the rule to highlight yesterday's dates in the cell range
					Dim rule As New XlCondFmtRuleTimePeriod()
					rule.TimePeriod = XlCondFmtTimePeriod.Yesterday
					rule.Formatting = XlCellFormatting.Bad
					formatting.Rules.Add(rule)

					' Create the rule to highlight today's dates in the cell range
					rule = New XlCondFmtRuleTimePeriod()
					rule.TimePeriod = XlCondFmtTimePeriod.Today
					rule.Formatting = XlCellFormatting.Good
					formatting.Rules.Add(rule)

					' Create the rule to highlight tomorrows's dates in the cell range
					rule = New XlCondFmtRuleTimePeriod()
					rule.TimePeriod = XlCondFmtTimePeriod.Tomorrow
					rule.Formatting = XlCellFormatting.Neutral
					formatting.Rules.Add(rule)

					' Add the specified format options to the worksheet collection of conditional formats
					sheet.ConditionalFormattings.Add(formatting)
				End Using
			End Using

'			#End Region ' #TimePeriodRule
		End Sub

		Private Shared Sub Top10(ByVal stream As Stream, ByVal documentFormat As XlDocumentFormat)
'			#Region "#Top/BottomRules"
			' Create an exporter instance
			Dim exporter As IXlExporter = XlExport.CreateExporter(documentFormat)

			' Create a new document
			Using document As IXlDocument = exporter.CreateDocument(stream)
				document.Options.Culture = CultureInfo.CurrentCulture

				' Create a worksheet
				Using sheet As IXlSheet = document.CreateSheet()

					' Generate data for the document
					For i As Integer = 0 To 9
						Using row As IXlRow = sheet.CreateRow()
							For j As Integer = 0 To 3
								Using cell As IXlCell = row.CreateCell()
									cell.Value = cell.ColumnIndex * 4 + cell.RowIndex + 1
								End Using
							Next j
						End Using
					Next i

					' Create an instance of the XlConditionalFormatting class 
					Dim formatting As New XlConditionalFormatting()

					' Specify the cell range to which the conditional formatting rules should be applied (A1:D10)
					formatting.Ranges.Add(XlCellRange.FromLTRB(0, 0, 3, 9))

					' Create the rule to identify bottom 10 values in the cell range
					Dim rule As New XlCondFmtRuleTop10()
					rule.Bottom = True
					rule.Formatting = XlCellFormatting.Bad
					formatting.Rules.Add(rule)

					' Create the rule to identify top 10 values in the cell range
					rule = New XlCondFmtRuleTop10()
					rule.Formatting = XlCellFormatting.Good
					formatting.Rules.Add(rule)

					' Add the specified format options to the worksheet collection of conditional formats
					sheet.ConditionalFormattings.Add(formatting)
				End Using
			End Using

'			#End Region ' #Top/BottomRules
		End Sub

		Private Shared Sub DataBar(ByVal stream As Stream, ByVal documentFormat As XlDocumentFormat)
'			#Region "#DataBarRule"
			' Create an exporter instance
			Dim exporter As IXlExporter = XlExport.CreateExporter(documentFormat)

			' Create a new document
			Using document As IXlDocument = exporter.CreateDocument(stream)
				document.Options.Culture = CultureInfo.CurrentCulture

				' Create a worksheet
				Using sheet As IXlSheet = document.CreateSheet()

					' Generate data for the document
					For i As Integer = 0 To 2
						Using column As IXlColumn = sheet.CreateColumn()
							column.WidthInPixels = 100
						End Using
					Next i
					For i As Integer = 0 To 10
						Using row As IXlRow = sheet.CreateRow()
							For j As Integer = 0 To 2
								Using cell As IXlCell = row.CreateCell()
									Dim rowIndex As Integer = cell.RowIndex
									Dim columnIndex As Integer = cell.ColumnIndex
									If columnIndex = 0 Then
										cell.Value = rowIndex + 1
									ElseIf columnIndex = 1 Then
										cell.Value = rowIndex - 5
									Else
										cell.Value = If((rowIndex < 5), rowIndex + 1, 11 - rowIndex)
									End If
								End Using
							Next j
						End Using
					Next i

					' Create an instance of the XlConditionalFormatting class 
					Dim formatting As New XlConditionalFormatting()

					' Specify the cell range to which the conditional formatting rule should be applied (A1:A11)
					formatting.Ranges.Add(XlCellRange.FromLTRB(0, 0, 0, 10))

					' Create the rule to compare values in the cell range using data bars with specified fill
					Dim rule As New XlCondFmtRuleDataBar()
					rule.FillColor = XlColor.FromTheme(XlThemeColor.Accent1, 0.2)
					rule.GradientFill = False
					formatting.Rules.Add(rule)

					' Add the specified format options to the worksheet collection of conditional formats
					sheet.ConditionalFormattings.Add(formatting)

					' Create an instance of the XlConditionalFormatting class 
					formatting = New XlConditionalFormatting()

					' Specify the cell range to which the conditional formatting rule should be applied (B1:B11).
					formatting.Ranges.Add(XlCellRange.FromLTRB(1, 0, 1, 10))

					' Create the rule to compare values in the cell range using data bars with specified fill, border and axis parameters
					rule = New XlCondFmtRuleDataBar()
					rule.FillColor = Color.Green
					rule.BorderColor = Color.Green
					rule.AxisColor = Color.Brown
					rule.GradientFill = True
					formatting.Rules.Add(rule)

					' Add the specified format options to the worksheet collection of conditional formats
					sheet.ConditionalFormattings.Add(formatting)

					' Create an instance of the XlConditionalFormatting class 
					formatting = New XlConditionalFormatting()

					' Specify the cell range to which the conditional formatting rule should be applied (C1:C11)
					formatting.Ranges.Add(XlCellRange.FromLTRB(2, 0, 2, 10))

					' Create the rule to compare values in the cell range using data bars with specified length, value and direction parameters
					rule = New XlCondFmtRuleDataBar()
					rule.FillColor = XlColor.FromTheme(XlThemeColor.Accent4, 0.2)
					rule.MinLength = 10
					rule.MaxLength = 90
					rule.MinValue.ObjectType = XlCondFmtValueObjectType.Number
					rule.MinValue.Value = 3
					rule.Direction = XlDataBarDirection.RightToLeft
					rule.ShowValues = False
					formatting.Rules.Add(rule)

					' Add the specified format options to the worksheet collection of conditional formats
					sheet.ConditionalFormattings.Add(formatting)
				End Using
			End Using

'			#End Region ' #DataBarRule
		End Sub

		Private Shared Sub IconSet(ByVal stream As Stream, ByVal documentFormat As XlDocumentFormat)
'			#Region "#IconSetRule"
			' Create an exporter instance
			Dim exporter As IXlExporter = XlExport.CreateExporter(documentFormat)

			' Create a new document
			Using document As IXlDocument = exporter.CreateDocument(stream)
				document.Options.Culture = CultureInfo.CurrentCulture

				' Create a worksheet
				Using sheet As IXlSheet = document.CreateSheet()

					' Generate data for the document
					For i As Integer = 0 To 10
						Using row As IXlRow = sheet.CreateRow()
							For j As Integer = 0 To 3
								Using cell As IXlCell = row.CreateCell()
									If cell.ColumnIndex Mod 2 = 0 Then
										cell.Value = cell.RowIndex + 1
									Else
										cell.Value = cell.RowIndex - 5
									End If
								End Using
							Next j
						End Using
					Next i

					' Create an instance of the XlConditionalFormatting class
					Dim formatting As New XlConditionalFormatting()

					' Specify the cell range to which the conditional formatting rule should be applied (A1:A11)
					formatting.Ranges.Add(XlCellRange.FromLTRB(0, 0, 0, 10))

					' Create the rule to apply a specific icon from the "3 Arrows" icon set to each cell in the range based on its value
					Dim rule As New XlCondFmtRuleIconSet()
					rule.IconSetType = XlCondFmtIconSetType.Arrows3
					rule.Priority = 1
					formatting.Rules.Add(rule)

					' Add the specified format options to the worksheet collection of conditional formats
					sheet.ConditionalFormattings.Add(formatting)

					' Create an instance of the XlConditionalFormatting class
					formatting = New XlConditionalFormatting()

					' Specify the cell range to which the conditional formatting rule should be applied (B1:B11)
					formatting.Ranges.Add(XlCellRange.FromLTRB(1, 0, 1, 10))

					' Create the rule to apply a specific icon from the "3 Flags" icon set to each cell in the range based on its value
					rule = New XlCondFmtRuleIconSet()
					rule.IconSetType = XlCondFmtIconSetType.Flags3
					rule.Priority = 2
					formatting.Rules.Add(rule)

					' Add the specified format options to the worksheet collection of conditional formats
					sheet.ConditionalFormattings.Add(formatting)

					' Create an instance of the XlConditionalFormatting class
					formatting = New XlConditionalFormatting()

					' Specify the cell range to which the conditional formatting rule should be applied (C1:C11)
					formatting.Ranges.Add(XlCellRange.FromLTRB(2, 0, 2, 10))

					' Create the rule to apply a specific icon from the "5 Ratings" icon set to each cell in the range based on its value
					rule = New XlCondFmtRuleIconSet()
					rule.IconSetType = XlCondFmtIconSetType.Rating5
					rule.ShowValues = False
					rule.Priority = 3
					formatting.Rules.Add(rule)

					' Add the specified format options to the worksheet collection of conditional formats
					sheet.ConditionalFormattings.Add(formatting)

					' Create an instance of the XlConditionalFormatting class
					formatting = New XlConditionalFormatting()

					' Specify the cell range to which the conditional formatting rule should be applied (D1:D11)
					formatting.Ranges.Add(XlCellRange.FromLTRB(3, 0, 3, 10))

					' Create the rule to apply a specific icon from the "4 Traffic Lights" icon set to each cell in the range based on its value
					rule = New XlCondFmtRuleIconSet()
					rule.IconSetType = XlCondFmtIconSetType.TrafficLights4
					rule.Reverse = True
					rule.Priority = 4
					formatting.Rules.Add(rule)

					' Add the specified format options to the worksheet collection of conditional formats
					sheet.ConditionalFormattings.Add(formatting)
				End Using
			End Using

'			#End Region ' #IconSetRule
		End Sub

		Private Shared Sub ColorScale(ByVal stream As Stream, ByVal documentFormat As XlDocumentFormat)
'			#Region "#ColorScaleRule"
			' Create an exporter instance
			Dim exporter As IXlExporter = XlExport.CreateExporter(documentFormat)

			' Create a new document
			Using document As IXlDocument = exporter.CreateDocument(stream)
				document.Options.Culture = CultureInfo.CurrentCulture

				' Create a worksheet
				Using sheet As IXlSheet = document.CreateSheet()

					' Generate data for the document
					For i As Integer = 0 To 10
						Using row As IXlRow = sheet.CreateRow()
							For j As Integer = 0 To 3
								Using cell As IXlCell = row.CreateCell()
									cell.Value = cell.RowIndex + 1
								End Using
							Next j
						End Using
					Next i

					' Create an instance of the XlConditionalFormatting class
					Dim formatting As New XlConditionalFormatting()

					' Specify cell ranges to which the conditional formatting rule should be applied (A1:A11 and C1:C11)
					formatting.Ranges.Add(XlCellRange.FromLTRB(0, 0, 0, 10))
					formatting.Ranges.Add(XlCellRange.FromLTRB(2, 0, 2, 10))

					' Create the default three-color scale rule to differentiate low, medium and high values in cell ranges
					Dim rule As New XlCondFmtRuleColorScale()
					formatting.Rules.Add(rule)

					' Add the specified format options to the worksheet collection of conditional formats
					sheet.ConditionalFormattings.Add(formatting)

					' Create an instance of the XlConditionalFormatting class
					formatting = New XlConditionalFormatting()

					' Specify cell ranges to which the conditional formatting rule should be applied (B1:B11 and D1:D11)
					formatting.Ranges.Add(XlCellRange.FromLTRB(1, 0, 1, 10)) ' B1:B11
					formatting.Ranges.Add(XlCellRange.FromLTRB(3, 0, 3, 10)) ' D1:D11

					' Create the two-color scale rule to differentiate low and high values in cell ranges 
					rule = New XlCondFmtRuleColorScale()
					rule.ColorScaleType = XlCondFmtColorScaleType.ColorScale2
					rule.MinColor = XlColor.FromTheme(XlThemeColor.Light1, 0.0)
					rule.MaxColor = XlColor.FromTheme(XlThemeColor.Accent1, 0.5)
					formatting.Rules.Add(rule)

					' Add the specified format options to the worksheet collection of conditional formats
					sheet.ConditionalFormattings.Add(formatting)
				End Using
			End Using

'			#End Region ' #ColorScaleRule
		End Sub

	End Class
End Namespace