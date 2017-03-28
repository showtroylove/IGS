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
	Public NotInheritable Class Miscellaneous

		Private Sub New()
		End Sub
		Private Shared Sub Hyperlinks(ByVal stream As Stream, ByVal documentFormat As XlDocumentFormat)
'			#Region "#Hyperlinks"
			' Create an exporter instance
			Dim exporter As IXlExporter = XlExport.CreateExporter(documentFormat)

			' Create a new document
			Using document As IXlDocument = exporter.CreateDocument(stream)
				document.Options.Culture = CultureInfo.CurrentCulture

				' Create a worksheet
				Using sheet As IXlSheet = document.CreateSheet()
					Using column As IXlColumn = sheet.CreateColumn()
						column.WidthInPixels = 300
					End Using

					' Create a hyperlink to a cell in the current workbook
					Using row As IXlRow = sheet.CreateRow()
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Local link"
							cell.Formatting = XlCellFormatting.Hyperlink
							Dim hyperlink As New XlHyperlink()
							hyperlink.Reference = New XlCellRange(New XlCellPosition(cell.ColumnIndex, cell.RowIndex))
							hyperlink.TargetUri = "#Sheet1!C5"
							sheet.Hyperlinks.Add(hyperlink)
						End Using
					End Using

					' Create a hyperlink to a cell located in the external workbook
					Using row As IXlRow = sheet.CreateRow()
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "External file link"
							cell.Formatting = XlCellFormatting.Hyperlink
							Dim hyperlink As New XlHyperlink()
							hyperlink.Reference = New XlCellRange(New XlCellPosition(cell.ColumnIndex, cell.RowIndex))
							hyperlink.TargetUri = "linked.xlsx#Sheet1!C5"
							sheet.Hyperlinks.Add(hyperlink)
						End Using
					End Using

					' Create a hyperlink to a web page
					Using row As IXlRow = sheet.CreateRow()
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "External uri"
							cell.Formatting = XlCellFormatting.Hyperlink
							Dim hyperlink As New XlHyperlink()
							hyperlink.Reference = New XlCellRange(New XlCellPosition(cell.ColumnIndex, cell.RowIndex))
							hyperlink.TargetUri = "http://www.devexpress.com"
							sheet.Hyperlinks.Add(hyperlink)
						End Using
					End Using
				End Using
			End Using

'			#End Region ' #Hyperlinks
		End Sub

		Private Shared Sub DocumentProperties(ByVal stream As Stream, ByVal documentFormat As XlDocumentFormat)
'			#Region "#DocumentProperties"
			' Create an exporter instance
			Dim exporter As IXlExporter = XlExport.CreateExporter(documentFormat)

			' Create a new document
			Using document As IXlDocument = exporter.CreateDocument(stream)
				document.Options.Culture = CultureInfo.CurrentCulture

				' Set the built-in document properties
				document.Properties.Title = "Sample document"
				document.Properties.Subject = "XL Export API demo"
				document.Properties.Keywords = "XL export document generation"
				document.Properties.Description = "Generate through XL Export API"
				document.Properties.Category = "Spreadsheet"
				document.Properties.Company = "DevExpress Inc."

				' Set the custom properties
				document.Properties.Custom("Product Suite") = "Spreadsheet Document Automation"
				document.Properties.Custom("Revision") = 5
				document.Properties.Custom("Date Completed") = DateTime.Now
				document.Properties.Custom("Published") = True

				' Generate data for the document
				Using sheet As IXlSheet = document.CreateSheet()
					sheet.SkipRows(1)
					Using row As IXlRow = sheet.CreateRow()
						row.SkipCells(1)
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "You can check exported document properties using File/Info/Advanced Properties dialog box."
						End Using
					End Using
				End Using
			End Using

'			#End Region ' #DocumentProperties
		End Sub

		Private Shared Sub DocumentOptions(ByVal stream As Stream, ByVal documentFormat As XlDocumentFormat)
'			#Region "#DocumentOptions"
			' Create an exporter instance
			Dim exporter As IXlExporter = XlExport.CreateExporter(documentFormat)

			' Create a new document
			Using document As IXlDocument = exporter.CreateDocument(stream)
				document.Options.Culture = CultureInfo.CurrentCulture

				' Create a worksheet
				Using sheet As IXlSheet = document.CreateSheet()
					Using column As IXlColumn = sheet.CreateColumn()
						column.WidthInPixels = 200
					End Using
					Using column As IXlColumn = sheet.CreateColumn()
						column.Formatting = XlCellAlignment.FromHV(XlHorizontalAlignment.Center, XlVerticalAlignment.Bottom)
					End Using

					' Display the file format to which the document is exported
					Using row As IXlRow = sheet.CreateRow()
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Document format:"
						End Using
						Using cell As IXlCell = row.CreateCell()
							cell.Value = document.Options.DocumentFormat.ToString().ToUpper()
						End Using
					End Using

					' Display the maximum number of columns allowed by the output file format
					Using row As IXlRow = sheet.CreateRow()
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Max column count:"
						End Using
						Using cell As IXlCell = row.CreateCell()
							cell.Value = document.Options.MaxColumnCount
						End Using
					End Using

					' Display the maximum number of rows allowed by the output file format
					Using row As IXlRow = sheet.CreateRow()
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Max row count:"
						End Using
						Using cell As IXlCell = row.CreateCell()
							cell.Value = document.Options.MaxRowCount
						End Using
					End Using

					' Display whether the document can contain multiple worksheets
					Using row As IXlRow = sheet.CreateRow()
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Supports document parts:"
						End Using
						Using cell As IXlCell = row.CreateCell()
							cell.Value = document.Options.SupportsDocumentParts
						End Using
					End Using

					' Display whether the document can contain formulas
					Using row As IXlRow = sheet.CreateRow()
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Supports formulas:"
						End Using
						Using cell As IXlCell = row.CreateCell()
							cell.Value = document.Options.SupportsFormulas
						End Using
					End Using

					' Display whether the document supports grouping functionality
					Using row As IXlRow = sheet.CreateRow()
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Supports outline/grouping:"
						End Using
						Using cell As IXlCell = row.CreateCell()
							cell.Value = document.Options.SupportsOutlineGrouping
						End Using
					End Using
				End Using
			End Using

'			#End Region ' #DocumentOptions
		End Sub

		Private Shared Sub CsvExportOptions(ByVal stream As Stream, ByVal documentFormat As XlDocumentFormat)
'			#Region "#CsvOptions"
			' Create an exporter instance
			Dim exporter As IXlExporter = XlExport.CreateExporter(documentFormat)

			' Create a new document
			Using document As IXlDocument = exporter.CreateDocument(stream)
				document.Options.Culture = CultureInfo.CurrentCulture

				' Specify options for exporting the document to CSV format
				Dim csvOptions As CsvDataAwareExporterOptions = TryCast(document.Options, CsvDataAwareExporterOptions)
				If csvOptions IsNot Nothing Then
					' Set the encoding of the text-based file to which the workbook is exported
					csvOptions.Encoding = Encoding.UTF8
					' Write a preamble that specifies the encoding used
					csvOptions.WritePreamble = True
					' Convert a cell value to a string by using the current culture's format string (instead of specified cell number formats) 
					csvOptions.UseCellNumberFormat = False
					' Insert the newline character after the last row of the resulting text
					csvOptions.NewlineAfterLastRow = True
				End If

				' Generate data for the document
				Using sheet As IXlSheet = document.CreateSheet()
					Using row As IXlRow = sheet.CreateRow()
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Product"
						End Using
						For i As Integer = 0 To 3
							Using cell As IXlCell = row.CreateCell()
								cell.Value = String.Format("Q{0}", i + 1)
							End Using
						Next i
					End Using
					Dim products() As String = { "Camembert Pierrot", "Gorgonzola Telino", "Mascarpone Fabioli", "Mozzarella di Giovanni", "Gnocchi di nonna Alice", "Gudbrandsdalsost", "Gustaf's Knackebrod", "Queso Cabrales", "Queso Manchego La Pastora", "Raclette Courdavault", "Singaporean Hokkien Fried Mee", "Wimmers gute Semmelknodel" }
					Dim random As New Random()
					For i As Integer = 0 To 11
						Using row As IXlRow = sheet.CreateRow()
							Using cell As IXlCell = row.CreateCell()
								cell.Value = products(i)
							End Using
							For j As Integer = 0 To 3
								Using cell As IXlCell = row.CreateCell()
									cell.Value = Math.Round(random.NextDouble() * 2000 + 3000)
								End Using
							Next j
						End Using
					Next i
				End Using
			End Using

'			#End Region ' #CsvOptions
		End Sub

	End Class
End Namespace