Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports DevExpress.Export.Xl
Imports DevExpress.Spreadsheet

Namespace XLExportExamples
	Public NotInheritable Class BasicActions

		Private Sub New()
		End Sub
		Private Shared Sub CreateDocument(ByVal stream As Stream, ByVal documentFormat As XlDocumentFormat)
'			#Region "#CreateDocument"
			' Create an exporter instance
			Dim exporter As IXlExporter = XlExport.CreateExporter(documentFormat)

			' Create a new document and write it to the specified stream
			Using document As IXlDocument = exporter.CreateDocument(stream)

				' Specify the document culture
				document.Options.Culture = CultureInfo.CurrentCulture
			End Using

'			#End Region ' #CreateDocument
		End Sub

		Private Shared Sub CreateSheet(ByVal stream As Stream, ByVal documentFormat As XlDocumentFormat)
'			#Region "#CreateSheet"
			' Create an exporter instance
			Dim exporter As IXlExporter = XlExport.CreateExporter(documentFormat)

			' Create a new document
			Using document As IXlDocument = exporter.CreateDocument(stream)

				' Specify the document culture
				document.Options.Culture = CultureInfo.CurrentCulture

				' Create a new worksheet under the specified name
				Using sheet As IXlSheet = document.CreateSheet()
					sheet.Name = "Sales report"
				End Using
			End Using

'			#End Region ' #CreateSheet
		End Sub

		Private Shared Sub CreateHiddenSheet(ByVal stream As Stream, ByVal documentFormat As XlDocumentFormat)
'			#Region "#CreateHiddenSheet"
			' Create an exporter instance
			Dim exporter As IXlExporter = XlExport.CreateExporter(documentFormat)

			' Create a new document
			Using document As IXlDocument = exporter.CreateDocument(stream)

				' Specify the document culture
				document.Options.Culture = CultureInfo.CurrentCulture

				' Create the first worksheet
				Using sheet As IXlSheet = document.CreateSheet()
					sheet.Name = "Sales report"
				End Using

				' Create the second worksheet and setup its visibility
				Using sheet As IXlSheet = document.CreateSheet()
					sheet.Name = "Data"
					sheet.VisibleState = XlSheetVisibleState.Hidden
				End Using
			End Using

'			#End Region ' #CreateHiddenSheet
		End Sub

		Private Shared Sub CreateColumns(ByVal stream As Stream, ByVal documentFormat As XlDocumentFormat)
'			#Region "#CreateColumns"
			' Create an exporter instance
			Dim exporter As IXlExporter = XlExport.CreateExporter(documentFormat)

			' Create a new document
			Using document As IXlDocument = exporter.CreateDocument(stream)

				' Specify the document culture
				document.Options.Culture = CultureInfo.CurrentCulture

				' Create a worksheet
				Using sheet As IXlSheet = document.CreateSheet()

					' Create the column A and set its width to 100 pixels
					Using column As IXlColumn = sheet.CreateColumn()
						column.WidthInPixels = 100
					End Using

					' Hide the column B in the worksheet
					Using column As IXlColumn = sheet.CreateColumn()
						column.IsHidden = True
					End Using

					' Create the column D and set its width to 24.5 characters
					Using column As IXlColumn = sheet.CreateColumn(3)
						column.WidthInCharacters = 24.5f
					End Using
				End Using
			End Using

'			#End Region ' #CreateColumns
		End Sub

		Private Shared Sub CreateRows(ByVal stream As Stream, ByVal documentFormat As XlDocumentFormat)
'			#Region "#CreateRows"
			' Create an exporter instance
			Dim exporter As IXlExporter = XlExport.CreateExporter(documentFormat)

			' Create a new document
			Using document As IXlDocument = exporter.CreateDocument(stream)

				' Specify the document culture
				document.Options.Culture = CultureInfo.CurrentCulture

				' Create a worksheet
				Using sheet As IXlSheet = document.CreateSheet()

					' Create the first row and set its height to 40 pixels
					Using row As IXlRow = sheet.CreateRow()
						row.HeightInPixels = 40
					End Using

					' Hide the third row in the worksheet
					Using row As IXlRow = sheet.CreateRow(2)
						row.IsHidden = True
					End Using
				End Using
			End Using

'			#End Region ' #CreateRows
		End Sub

		Private Shared Sub CreateCells(ByVal stream As Stream, ByVal documentFormat As XlDocumentFormat)
'			#Region "#CreateCells"
			' Create an exporter instance
			Dim exporter As IXlExporter = XlExport.CreateExporter(documentFormat)

			' Create a new document
			Using document As IXlDocument = exporter.CreateDocument(stream)

				' Specify the document culture
				document.Options.Culture = CultureInfo.CurrentCulture

				' Create a worksheet
				Using sheet As IXlSheet = document.CreateSheet()

					' Create the column A and set its width
					Using column As IXlColumn = sheet.CreateColumn()
						column.WidthInPixels = 150
					End Using

					' Create the first row
					Using row As IXlRow = sheet.CreateRow()

						' Create the cell A1 and set its value
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Numeric value:"
						End Using

						' Create the cell B1 and assign the numeric value to it
						Using cell As IXlCell = row.CreateCell()
							cell.Value = 123.45
						End Using
					End Using

					' Create the second row
					Using row As IXlRow = sheet.CreateRow()

						' Create the cell A2 and set its valu
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Text value:"
						End Using

						' Create the cell B2 and assign the text value to it
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "abc"
						End Using
					End Using

					' Create the third row
					Using row As IXlRow = sheet.CreateRow()

						' Create the cell A3 and set its value
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Boolean value:"
						End Using

						' Create the cell B3 and assign the boolean value to it
						Using cell As IXlCell = row.CreateCell()
							cell.Value = True
						End Using
					End Using

					' Create the fourth row
					Using row As IXlRow = sheet.CreateRow()

						' Create the cell A4 and set its value
						Using cell As IXlCell = row.CreateCell()
							cell.Value = "Error value:"
						End Using

						' Create the cell B4 and assign an error value to it
						Using cell As IXlCell = row.CreateCell()
							cell.Value = XlVariantValue.ErrorName
						End Using
					End Using
				End Using
			End Using

'			#End Region ' #CreateCells
		End Sub

		Private Shared Sub MergeCells(ByVal stream As Stream, ByVal documentFormat As XlDocumentFormat)
'			#Region "#MergeCells"
			' Create an exporter instance
			Dim exporter As IXlExporter = XlExport.CreateExporter(documentFormat)

			' Create a new document
			Using document As IXlDocument = exporter.CreateDocument(stream)
				document.Options.Culture = CultureInfo.CurrentCulture

				' Create a worksheet
				Using sheet As IXlSheet = document.CreateSheet()

					' Create the first row in the worksheet
					Using row As IXlRow = sheet.CreateRow()
						' Create a cell
						Using cell As IXlCell = row.CreateCell()
							' Set the cell value
							cell.Value = "Merged cells in range A1:E1"
							' Align the cell content
							cell.ApplyFormatting(XlCellAlignment.FromHV(XlHorizontalAlignment.Center, XlVerticalAlignment.Center))
						End Using
					End Using

					' Create the second row in the worksheet
					Using row As IXlRow = sheet.CreateRow()
						' Create a cell
						Using cell As IXlCell = row.CreateCell()
							' Set the cell value
							cell.Value = "Merged cells in range A2:A5"
							' ALign the cell content
							cell.ApplyFormatting(XlCellAlignment.FromHV(XlHorizontalAlignment.Center, XlVerticalAlignment.Center))
							' Wrap the text within the cell
							cell.Formatting.Alignment.WrapText = True
						End Using
						' Create a cell
						Using cell As IXlCell = row.CreateCell()
							' Set the cell value
							cell.Value = "Merged cells in range B2:E5"
							' Align the cell content
							cell.ApplyFormatting(XlCellAlignment.FromHV(XlHorizontalAlignment.Center, XlVerticalAlignment.Center))
						End Using
					End Using

					' Merge cells contained in the range A1:E1
					sheet.MergedCells.Add(XlCellRange.FromLTRB(0, 0, 4, 0))

					' Merge cells contained in the range A2:A5
					sheet.MergedCells.Add(XlCellRange.FromLTRB(0, 1, 0, 4))

					' Merge cells contained in the range B2:E5
					sheet.MergedCells.Add(XlCellRange.FromLTRB(1, 1, 4, 4))
				End Using
			End Using

'			#End Region ' #MergeCells
		End Sub

	End Class
End Namespace