Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports DevExpress.Export.Xl
Imports DevExpress.XtraExport.Csv
Imports DevExpress.Spreadsheet

Namespace XLExportExamples
	Public NotInheritable Class CellFormatting

		Private Sub New()
		End Sub
		Private Shared Sub PredefinedFormatting(ByVal stream As Stream, ByVal documentFormat As XlDocumentFormat)
'			#Region "#PredefinedFormatting"
			' Create an exporter instance
			Dim exporter As IXlExporter = XlExport.CreateExporter(documentFormat)

			' Create a new document
			Using document As IXlDocument = exporter.CreateDocument(stream)
				document.Options.Culture = CultureInfo.CurrentCulture
				' Create a worksheet
				Using sheet As IXlSheet = document.CreateSheet()

					' Create six successive columns and set their widths
					For i As Integer = 0 To 5
						Using column As IXlColumn = sheet.CreateColumn()
							column.WidthInPixels = 100
						End Using
					Next i

					' Specify the "Good, Bad and Neutral" formatting category
					Using row As IXlRow = sheet.CreateRow()
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Good, Bad, Neutral"
						End Using
					End Using
					Using row As IXlRow = sheet.CreateRow()
						' Create a cell with the default "Normal" formatting
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Normal"
						End Using
						' Create a cell and apply the "Bad" predefined formatting to it
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Bad"
							cell.Formatting = XlCellFormatting.Bad
						End Using
						' Create a cell and apply the "Good" predefined formatting to it
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Good"
							cell.Formatting = XlCellFormatting.Good
						End Using
						' Create a cell and apply the "Neutral" predefined formatting to it
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Neutral"
							cell.Formatting = XlCellFormatting.Neutral
						End Using
					End Using

					sheet.SkipRows(1)

					' Specify the "Data and Model" formatting category
					Using row As IXlRow = sheet.CreateRow()
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Data and Model"
						End Using
					End Using
					Using row As IXlRow = sheet.CreateRow()
						' Create a cell and apply the "Calculation" predefined formatting to it
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Calculation"
							cell.Formatting = XlCellFormatting.Calculation
						End Using
						' Create a cell and apply the "Check Cell" predefined formatting to it
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Check Cell"
							cell.Formatting = XlCellFormatting.CheckCell
						End Using
						' Create a cell and apply the "Explanatory..." predefined formatting to it
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Explanatory"
							cell.Formatting = XlCellFormatting.Explanatory
						End Using
						' Create a cell and apply the "Input" predefined formatting to it
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Input"
							cell.Formatting = XlCellFormatting.Input
						End Using
						' Create a cell and apply the "Linked Cell" predefined formatting to it
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Linked Cell"
							cell.Formatting = XlCellFormatting.LinkedCell
						End Using
						' Create a cell and apply the "Note" predefined formatting to it
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Note"
							cell.Formatting = XlCellFormatting.Note
						End Using
					End Using
					Using row As IXlRow = sheet.CreateRow()
						' Create a cell and apply the "Output" predefined formatting to it
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Output"
							cell.Formatting = XlCellFormatting.Output
						End Using
						' Create a cell and apply the "Warning Text" predefined formatting to it
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Warning Text"
							cell.Formatting = XlCellFormatting.WarningText
						End Using
					End Using

					sheet.SkipRows(1)

					' Specify the "Titles and Headings" formatting category
					Using row As IXlRow = sheet.CreateRow()
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Titles and Headings"
						End Using
					End Using
					Using row As IXlRow = sheet.CreateRow()
						' Create a cell and apply the "Heading 1" predefined formatting to it
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Heading1"
							cell.Formatting = XlCellFormatting.Heading1
						End Using
						' Create a cell and apply the "Heading 2" predefined formatting to it
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Heading2"
							cell.Formatting = XlCellFormatting.Heading2
						End Using
						' Create a cell and apply the "Heading 3" predefined formatting to it
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Heading3"
							cell.Formatting = XlCellFormatting.Heading3
						End Using
						' Create a cell and apply the "Heading 4" predefined formatting to it
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Heading4"
							cell.Formatting = XlCellFormatting.Heading4
						End Using
						' Create a cell and apply the "Title" predefined formatting to it
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Title"
							cell.Formatting = XlCellFormatting.Title
						End Using
						' Create a cell and apply the "Total" predefined formatting to it
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Total"
							cell.Formatting = XlCellFormatting.Total
						End Using
					End Using
				End Using
			End Using

'			#End Region ' #PredefinedFormatting
		End Sub

		Private Shared Sub ThemedFormatting(ByVal stream As Stream, ByVal documentFormat As XlDocumentFormat)
'			#Region "#ThemedFormatting"
			' Create an exporter instance
			Dim exporter As IXlExporter = XlExport.CreateExporter(documentFormat)

			' Create a new document
			Using document As IXlDocument = exporter.CreateDocument(stream)
				document.Options.Culture = CultureInfo.CurrentCulture
				' Create a worksheet
				Using sheet As IXlSheet = document.CreateSheet()

					' Create six successive columns and set their widths
					For i As Integer = 0 To 5
						Using column As IXlColumn = sheet.CreateColumn()
							column.WidthInPixels = 100
						End Using
					Next i

					' Specify an array that stores six accent colors of the document theme
					Dim themeColors() As XlThemeColor = { XlThemeColor.Accent1, XlThemeColor.Accent2, XlThemeColor.Accent3, XlThemeColor.Accent4, XlThemeColor.Accent5, XlThemeColor.Accent6 }

					' Specify the "20% - AccentN" themed cell formatting
					Using row As IXlRow = sheet.CreateRow()
						For i As Integer = 0 To 5
							Using cell As IXlCell = row.CreateCell()
								cell.Value = String.Format("Accent{0} 20%", i + 1)
								' Apply the themed formatting to the cell using one of the predefined accent colors lightened by 80%
								cell.Formatting = XlCellFormatting.Themed(themeColors(i), 0.8)
							End Using
						Next i
					End Using

					' Specify the "40% - AccentN" themed cell formatting
					Using row As IXlRow = sheet.CreateRow()
						For i As Integer = 0 To 5
							Using cell As IXlCell = row.CreateCell()
								cell.Value = String.Format("Accent{0} 40%", i + 1)
								' Apply the themed formatting to the cell using one of the predefined accent colors lightened by 60%
								cell.Formatting = XlCellFormatting.Themed(themeColors(i), 0.6)
							End Using
						Next i
					End Using

					' Specify the "60% - AccentN" themed cell formatting
					Using row As IXlRow = sheet.CreateRow()
						For i As Integer = 0 To 5
							Using cell As IXlCell = row.CreateCell()
								cell.Value = String.Format("Accent{0} 60%", i + 1)
								' Apply the themed formatting to the cell using one of the predefined accent colors lightened by 40%
								cell.Formatting = XlCellFormatting.Themed(themeColors(i), 0.4)
							End Using
						Next i
					End Using

					' Specify the "AccentN" themed cell formatting
					Using row As IXlRow = sheet.CreateRow()
						For i As Integer = 0 To 5
							Using cell As IXlCell = row.CreateCell()
								cell.Value = String.Format("Accent{0}", i + 1)
								' Apply the themed formatting to the cell using one of the predefined accent colors
								cell.Formatting = XlCellFormatting.Themed(themeColors(i), 0.0)
							End Using
						Next i
					End Using
				End Using
			End Using

'			#End Region ' #ThemedFormatting
		End Sub

		Private Shared Sub Alignment(ByVal stream As Stream, ByVal documentFormat As XlDocumentFormat)
'			#Region "#Alignment"
			' Create an exporter instance
			Dim exporter As IXlExporter = XlExport.CreateExporter(documentFormat)

			' Create a new document
			Using document As IXlDocument = exporter.CreateDocument(stream)
				document.Options.Culture = CultureInfo.CurrentCulture
				' Create a worksheet
				Using sheet As IXlSheet = document.CreateSheet()

					' Create three successive columns and set their widths
					For i As Integer = 0 To 2
						Using column As IXlColumn = sheet.CreateColumn()
							column.WidthInPixels = 72
						End Using
					Next i

					' Create the first row in the worksheet
					Using row As IXlRow = sheet.CreateRow()

						' Set the row height
						row.HeightInPixels = 40

						' Create the first cell in the row
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "text"
							' Specify the horizontal and vertical alignment of the cell content (Left/Top)
							cell.ApplyFormatting(XlCellAlignment.FromHV(XlHorizontalAlignment.Left, XlVerticalAlignment.Top))
						End Using

						' Create the second cell in the row
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "text"
							' Specify the horizontal and vertical alignment of the cell content (Center/Top)
							cell.ApplyFormatting(XlCellAlignment.FromHV(XlHorizontalAlignment.Center, XlVerticalAlignment.Top))
						End Using

						' Create the third cell in the row
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "text"
							' Specify the horizontal and vertical alignment of the cell content (Right/Top)
							cell.ApplyFormatting(XlCellAlignment.FromHV(XlHorizontalAlignment.Right, XlVerticalAlignment.Top))
						End Using
					End Using

					' Create the second row in the worksheet
					Using row As IXlRow = sheet.CreateRow()

						' Set the row height
						row.HeightInPixels = 40

						' Create the first cell in the row
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "text"
							' Specify the horizontal and vertical alignment of the cell content (Left/Center)
							cell.ApplyFormatting(XlCellAlignment.FromHV(XlHorizontalAlignment.Left, XlVerticalAlignment.Center))
						End Using

						' Create the second cell in the row
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "text"
							' Specify the horizontal and vertical alignment of the cell content (Center/Center)
							cell.ApplyFormatting(XlCellAlignment.FromHV(XlHorizontalAlignment.Center, XlVerticalAlignment.Center))
						End Using

						' Create the third cell in the row
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "text"
							' Specify the horizontal and vertical alignment of the cell content (Right/Center)
							cell.ApplyFormatting(XlCellAlignment.FromHV(XlHorizontalAlignment.Right, XlVerticalAlignment.Center))
						End Using
					End Using

					' Create the third row in the worksheet
					Using row As IXlRow = sheet.CreateRow()

						' Set the row height
						row.HeightInPixels = 40

						' Create the first cell in the row
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "text"
							' Specify the horizontal and vertical alignment of the cell content (Left/Bottom)
							cell.ApplyFormatting(XlCellAlignment.FromHV(XlHorizontalAlignment.Left, XlVerticalAlignment.Bottom))
						End Using

						' Create the second cell in the row
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "text"
							' Specify the horizontal and vertical alignment of the cell content (Center/Bottom)
							cell.ApplyFormatting(XlCellAlignment.FromHV(XlHorizontalAlignment.Center, XlVerticalAlignment.Bottom))
						End Using

						' Create the third cell in the row
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "text"
							' Specify the horizontal and vertical alignment of the cell content (Right/Bottom)
							cell.ApplyFormatting(XlCellAlignment.FromHV(XlHorizontalAlignment.Right, XlVerticalAlignment.Bottom))
						End Using
					End Using

					sheet.SkipRows(1)

					' Create the fifth row in the worksheet
					Using row As IXlRow = sheet.CreateRow()

						' Create the first cell in the row
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "The WrapText property is applied to wrap the text within a cell"
							' Wrap the text within the cell
							cell.Formatting = New XlCellAlignment() With {.WrapText = True}
						End Using

						' Create the second cell in the row
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Indented text"
							' Set the indentation of the cell content
							cell.Formatting = New XlCellAlignment() With {.Indent = 2}
						End Using

						' Create the third cell in the row
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Rotated text"
							' Rotate the text within the cell
							cell.Formatting = New XlCellAlignment() With {.TextRotation = 90}
						End Using
					End Using
				End Using
			End Using

'			#End Region ' #Alignment
		End Sub

		Private Shared Sub Borders(ByVal stream As Stream, ByVal documentFormat As XlDocumentFormat)
'			#Region "#Borders"
			' Specify a two-dimensional array that stores possible line styles for a border
			Dim lineStyles(,) As XlBorderLineStyle = { { XlBorderLineStyle.Thin, XlBorderLineStyle.Medium, XlBorderLineStyle.Thick, XlBorderLineStyle.Double }, { XlBorderLineStyle.Dotted, XlBorderLineStyle.Dashed, XlBorderLineStyle.DashDot, XlBorderLineStyle.DashDotDot }, { XlBorderLineStyle.SlantDashDot, XlBorderLineStyle.MediumDashed, XlBorderLineStyle.MediumDashDot, XlBorderLineStyle.MediumDashDotDot } }

			' Create an exporter instance
			Dim exporter As IXlExporter = XlExport.CreateExporter(documentFormat)

			' Create a new document
			Using document As IXlDocument = exporter.CreateDocument(stream)
				document.Options.Culture = CultureInfo.CurrentCulture

				' Create a worksheet
				Using sheet As IXlSheet = document.CreateSheet()
					For i As Integer = 0 To 2
						sheet.SkipRows(1)

						' Create a worksheet row
						Using row As IXlRow = sheet.CreateRow()
							For j As Integer = 0 To 3
								' Skip one cell
								row.SkipCells(1)

								' Create a new cell in the row
								Using cell As IXlCell = row.CreateCell()
									' Set outside borders for the created cell using a particular line style from the lineStyles array
									cell.ApplyFormatting(XlBorder.OutlineBorders(Color.Black, lineStyles(i, j)))
								End Using
							Next j
						End Using
					Next i
				End Using
			End Using

'			#End Region ' #Borders
		End Sub

		Private Shared Sub Fill(ByVal stream As Stream, ByVal documentFormat As XlDocumentFormat)
'			#Region "#Fill"
			' Create an exporter instance
			Dim exporter As IXlExporter = XlExport.CreateExporter(documentFormat)

			' Create a new document
			Using document As IXlDocument = exporter.CreateDocument(stream)
				document.Options.Culture = CultureInfo.CurrentCulture
				' Create a worksheet
				Using sheet As IXlSheet = document.CreateSheet()

					Using row As IXlRow = sheet.CreateRow()
						Using cell As IXlCell = row.CreateCell()
							' Fill the cell background using the predefined color
							cell.ApplyFormatting(XlFill.SolidFill(Color.Beige))
						End Using
						Using cell As IXlCell = row.CreateCell()
							' Fill the cell background using the custom RGB color
							cell.ApplyFormatting(XlFill.SolidFill(Color.FromArgb(&Hff, &H99, &H66)))
						End Using
						Using cell As IXlCell = row.CreateCell()
							' Fill the cell background using the theme color
							cell.ApplyFormatting(XlFill.SolidFill(XlColor.FromTheme(XlThemeColor.Accent3, 0.4)))
						End Using
					End Using

					sheet.SkipRows(1)
					Using row As IXlRow = sheet.CreateRow()
						Using cell As IXlCell = row.CreateCell()
							' Specify the cell background pattern using predefined colors
							cell.ApplyFormatting(XlFill.PatternFill(XlPatternType.DarkDown, Color.Red, Color.White))
						End Using
						Using cell As IXlCell = row.CreateCell()
							' Specify the cell background pattern using custom RGB colors
							cell.ApplyFormatting(XlFill.PatternFill(XlPatternType.DarkTrellis, Color.FromArgb(&Hff, &Hff, &H66), Color.FromArgb(&H66, &H99, &Hff)))
						End Using
						Using cell As IXlCell = row.CreateCell()
							' Specify the cell background pattern using theme colors
							cell.ApplyFormatting(XlFill.PatternFill(XlPatternType.LightHorizontal, XlColor.FromTheme(XlThemeColor.Accent1, 0.2), XlColor.FromTheme(XlThemeColor.Light2, 0.0)))
						End Using
					End Using
				End Using
			End Using

'			#End Region ' #Fill
		End Sub

		Private Shared Sub Font(ByVal stream As Stream, ByVal documentFormat As XlDocumentFormat)
'			#Region "#Font"
			' Create an exporter instance
			Dim exporter As IXlExporter = XlExport.CreateExporter(documentFormat)

			' Create a new document
			Using document As IXlDocument = exporter.CreateDocument(stream)
				document.Options.Culture = CultureInfo.CurrentCulture
				' Create a worksheet
				Using sheet As IXlSheet = document.CreateSheet()
					' Create five successive columns and set their widths
					For i As Integer = 0 To 4
						Using column As IXlColumn = sheet.CreateColumn()
							column.WidthInPixels = 100
						End Using
					Next i

					' Create the first row
					Using row As IXlRow = sheet.CreateRow()
						' Create the cell A1
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Body font"
							' Apply the theme body font to the cell content
							cell.ApplyFormatting(XlFont.BodyFont())
						End Using
						' Create the cell B1
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Headings font"
							' Apply the theme heading font to the cell content
							cell.ApplyFormatting(XlFont.HeadingsFont())
						End Using
						' Create the cell C1
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Custom font"
							' Specify the custom font attributes
							Dim font As New XlFont()
							font.Name = "Century Gothic"
							font.SchemeStyle = XlFontSchemeStyles.None
							' Apply the custom font to the cell content
							cell.ApplyFormatting(font)
						End Using
					End Using

					' Create an array that stores different values of font size
					Dim fontSizes() As Integer = { 11, 14, 18, 24, 36 }
					' Skip one row in the worksheet
					sheet.SkipRows(1)

					' Create the third row
					Using row As IXlRow = sheet.CreateRow()

						' Create five successive cells (A3:E3) with different font sizes
						For i As Integer = 0 To 4
							Using cell As IXlCell = row.CreateCell()
								' Set the cell value that displays the applied font size
								cell.Value = String.Format("{0}pt", fontSizes(i))
								' Create a font instance of the specified size
								Dim font As New XlFont()
								font.Size = fontSizes(i)
								' Apply font settings to the cell content
								cell.ApplyFormatting(font)
							End Using
						Next i
					End Using

					' Skip one row in the worksheet
					sheet.SkipRows(1)

					' Create the fifth row
					Using row As IXlRow = sheet.CreateRow()
						' Create the cell A5
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Red"
							' Create a font instance and set its color
							Dim font As New XlFont() With {.Color = Color.Red}
							' Apply the font color to the cell content
							cell.ApplyFormatting(font)
						End Using
						' Create the cell B5
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Bold"
							' Create a font instance and set its style to bold
							Dim font As New XlFont() With {.Bold = True}
							' Apply the font style to the cell content
							cell.ApplyFormatting(font)
						End Using
						' Create the cell C5
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Italic"
							' Create a font instance and set its style to italic
							Dim font As New XlFont() With {.Italic = True}
							' Italicize the cell text
							cell.ApplyFormatting(font)
						End Using
						' Create the cell D5
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Underline"
							' Create a font instance and set the underline type to double
							Dim font As New XlFont() With {.Underline = XlUnderlineType.Double}
							' Underline the cell text
							cell.ApplyFormatting(font)
						End Using
						' Create the cell E5
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "StrikeThrough"
							' Create a font instance and turn the strikethrough formatting on
							Dim font As New XlFont() With {.StrikeThrough = True}
							' Strike the cell text through
							cell.ApplyFormatting(font)
						End Using
					End Using
				End Using
			End Using

'			#End Region ' #Font
		End Sub

		Private Shared Sub NumberFormat(ByVal stream As Stream, ByVal documentFormat As XlDocumentFormat)
'			#Region "#NumberFormat"
			' Create an exporter instance
			Dim exporter As IXlExporter = XlExport.CreateExporter(documentFormat)

			' Create a new document
			Using document As IXlDocument = exporter.CreateDocument(stream)
				document.Options.Culture = CultureInfo.CurrentCulture
				' Specify options for exporting the document to CSV format
				Dim csvOptions As CsvDataAwareExporterOptions = TryCast(document.Options, CsvDataAwareExporterOptions)
				If csvOptions IsNot Nothing Then
					csvOptions.WritePreamble = True
				End If

				' Create a worksheet
				Using sheet As IXlSheet = document.CreateSheet()
					' Create six successive columns and set their widths
					For i As Integer = 0 To 5
						Using column As IXlColumn = sheet.CreateColumn()
							column.WidthInPixels = 180
						End Using
					Next i

					' Create the header row for the "Excel number formats" category
					Using row As IXlRow = sheet.CreateRow()
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Excel number formats"
							cell.Formatting = XlCellFormatting.Heading4
						End Using
					End Using

					' Use the predefined Excel number formats to display data in cells
					Using row As IXlRow = sheet.CreateRow()
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Predefined formats:"
						End Using
						Using cell As IXlCell = row.CreateCell()
							' Display 123.456 as 123.46
							cell.Value = 123.456
							cell.Formatting = XlNumberFormat.Number2
						End Using
						Using cell As IXlCell = row.CreateCell()
							' Display 12345 as 12,345
							cell.Value = 12345
							cell.Formatting = XlNumberFormat.NumberWithThousandSeparator
						End Using
						Using cell As IXlCell = row.CreateCell()
							' Display 0.33 as 33%.
							cell.Value = 0.33
							cell.Formatting = XlNumberFormat.Percentage
						End Using
						Using cell As IXlCell = row.CreateCell()
							' Display the current date as "mm-dd-yy" 
							cell.Value = DateTime.Now
							cell.Formatting = XlNumberFormat.ShortDate
						End Using
						Using cell As IXlCell = row.CreateCell()
							' Display the current time as "h:mm AM/PM"
							cell.Value = DateTime.Now
							cell.Formatting = XlNumberFormat.ShortTime12
						End Using
					End Using

					' Use custom number formats to display data in cells
					Using row As IXlRow = sheet.CreateRow()
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Custom formats:"
						End Using
						Using cell As IXlCell = row.CreateCell()
							' Display 4310.45 as $4,310.45
							cell.Value = 4310.45
							cell.Formatting = New XlCellFormatting()
							cell.Formatting.NumberFormat = "_([$$-409]* #,##0.00_);_([$$-409]* \(#,##0.00\);_([$$-409]* ""-""??_);_(@_)"
						End Using
						Using cell As IXlCell = row.CreateCell()
							' Display 3426.75 as €3,426.75
							cell.Value = 3426.75
							cell.Formatting = New XlCellFormatting()
							cell.Formatting.NumberFormat = "_-[$€-2] * #,##0.00_-;-[$€-2] * #,##0.00_-;_-[$€-2] * "" - ""??_-;_-@_-"
						End Using
						Using cell As IXlCell = row.CreateCell()
							' Display 0.333 as 33.3%
							cell.Value = 0.333
							cell.Formatting = New XlCellFormatting()
							cell.Formatting.NumberFormat = "0.0%"
						End Using
						Using cell As IXlCell = row.CreateCell()
							' Apply the custom number format to the date value
							' Display days as Sunday–Saturday, months as January–December, days as 1–31 and years as 1900–9999
							cell.Value = DateTime.Now
							cell.Formatting = New XlCellFormatting()
							cell.Formatting.NumberFormat = "dddd, mmmm d, yyyy"
						End Using
						Using cell As IXlCell = row.CreateCell()
							' Display 0.6234 as 341/547
							cell.Value = 0.6234
							cell.Formatting = New XlCellFormatting()
							cell.Formatting.NumberFormat = "# ???/???"
						End Using
					End Using

					sheet.SkipRows(1)

					' Create the header row for the ".NET number formats" category.
					Using row As IXlRow = sheet.CreateRow()
						Using cell As IXlCell = row.CreateCell()
							cell.Value = ".NET number formats"
							cell.Formatting = XlCellFormatting.Heading4
						End Using
					End Using

					' Use the standard .NET-style format strings to display data in cells
					Using row As IXlRow = sheet.CreateRow()
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Standard formats:"
						End Using
						Using cell As IXlCell = row.CreateCell()
							' Display 123.45 as 123
							cell.Value = 123.45
							cell.Formatting = XlCellFormatting.FromNetFormat("D", False)
						End Using
						Using cell As IXlCell = row.CreateCell()
							' Display 12345 as 1.234500E+004
							cell.Value = 12345
							cell.Formatting = XlCellFormatting.FromNetFormat("E", False)
						End Using
						Using cell As IXlCell = row.CreateCell()
							' Display 0.33 as 33.00%
							cell.Value = 0.33
							cell.Formatting = XlCellFormatting.FromNetFormat("P", False)
						End Using
						Using cell As IXlCell = row.CreateCell()
							' Display the current date using the short date pattern
							cell.Value = DateTime.Now
							cell.Formatting = XlCellFormatting.FromNetFormat("d", True)
						End Using
						Using cell As IXlCell = row.CreateCell()
							' Display the current time using the short time pattern
							cell.Value = DateTime.Now
							cell.Formatting = XlCellFormatting.FromNetFormat("t", True)
						End Using
					End Using

					' Use custom format strings to display data in cells
					Using row As IXlRow = sheet.CreateRow()
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Custom formats:"
						End Using
						Using cell As IXlCell = row.CreateCell()
							' Display 123.456 as 123.46 
							cell.Value = 123.456
							cell.Formatting = XlCellFormatting.FromNetFormat("#0.00", False)
						End Using
						Using cell As IXlCell = row.CreateCell()
							' Display 12345 as 1.235E+04
							cell.Value = 12345
							cell.Formatting = XlCellFormatting.FromNetFormat("0.0##e+00", False)
						End Using
						Using cell As IXlCell = row.CreateCell()
							' Display 0.333 as Max=33.3%
							cell.Value = 0.333
							cell.Formatting = XlCellFormatting.FromNetFormat("Max={0:#.0%}", False)
						End Using
						Using cell As IXlCell = row.CreateCell()
							' Apply the custom format string to the current date
							' Display days as 01–31, months as 01-12 and years as a four-digit number 
							cell.Value = DateTime.Now
							cell.Formatting = XlCellFormatting.FromNetFormat("dd-MM-yyyy", True)
						End Using
						Using cell As IXlCell = row.CreateCell()
							' Apply the custom format string to the current time
							' Display hours as 01-12, minutes as 00-59, and add the AM/PM designator
							cell.Value = DateTime.Now
							cell.Formatting = XlCellFormatting.FromNetFormat("hh:mm tt", True)
						End Using
					End Using
				End Using
			End Using

'			#End Region ' #NumberFormat
		End Sub

		Private Shared Sub RichText(ByVal stream As Stream, ByVal documentFormat As XlDocumentFormat)
'			#Region "#RichText"
			' Create an exporter instance
			Dim exporter As IXlExporter = XlExport.CreateExporter(documentFormat)

			' Create a new document
			Using document As IXlDocument = exporter.CreateDocument(stream)
				document.Options.Culture = CultureInfo.CurrentCulture

				' Create a new worksheet
				Using sheet As IXlSheet = document.CreateSheet()

					' Create the first row
					Using row As IXlRow = sheet.CreateRow()

						' Create the cell A1
						Using cell As IXlCell = row.CreateCell()

							' Create an XlRichTextString instance
							Dim richText As New XlRichTextString()

							' Add text runs to the collection and set their fonts
							richText.Runs.Add(New XlRichTextRun("Lorem ", XlFont.CustomFont("Arial", 14.0, XlColor.FromArgb(&Hff, &H00, &H00))))
							richText.Runs.Add(New XlRichTextRun("ipsum", XlFont.CustomFont("Cambria", 11.0, XlColor.FromTheme(XlThemeColor.Accent1, 0.0))))
							richText.Runs.Add(New XlRichTextRun(" dolor ", XlFont.CustomFont("Calibri", 15.0, XlColor.FromTheme(XlThemeColor.Accent2, -0.2))))
							richText.Runs.Add(New XlRichTextRun("sit amet", XlFont.BodyFont()))
							Dim font As XlFont = XlFont.BodyFont()
							font.Script = XlScriptType.Superscript
							richText.Runs.Add(New XlRichTextRun("2", font))

							' Add the rich formatted text to the cell
							cell.SetRichText(richText)
						End Using
					End Using
				End Using
			End Using

'			#End Region ' #RichText
		End Sub
	End Class
End Namespace