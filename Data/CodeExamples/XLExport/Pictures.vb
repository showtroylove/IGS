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
	Public NotInheritable Class Pictures

		Private Sub New()
		End Sub
		Private Shared Sub InsertPicture(ByVal stream As Stream, ByVal documentFormat As XlDocumentFormat, ByVal imagesPath As String)
'			#Region "#InsertPicture"
			' Create an exporter instance
			Dim exporter As IXlExporter = XlExport.CreateExporter(documentFormat)

			' Create a new document
			Using document As IXlDocument = exporter.CreateDocument(stream)
				document.Options.Culture = CultureInfo.CurrentCulture

				' Create a worksheet
				Using sheet As IXlSheet = document.CreateSheet()

					' Insert a picture from a file and anchor it to cells
					Using picture As IXlPicture = sheet.CreatePicture()
						picture.Image = Image.FromFile(Path.Combine(imagesPath, "image1.jpg"))
						' Set two-cell anchor with "Move and size with cells" positioning
						picture.SetTwoCellAnchor(New XlAnchorPoint(1, 1, 0, 0), New XlAnchorPoint(6, 11, 2, 15), XlAnchorType.TwoCell)
					End Using
				End Using
			End Using

'			#End Region ' #InsertPicture
		End Sub

		Private Shared Sub StretchPicture(ByVal stream As Stream, ByVal documentFormat As XlDocumentFormat, ByVal imagesPath As String)
'			#Region "#StretchPicture"
			' Create an exporter instance
			Dim exporter As IXlExporter = XlExport.CreateExporter(documentFormat)

			' Create a new document
			Using document As IXlDocument = exporter.CreateDocument(stream)
				document.Options.Culture = CultureInfo.CurrentCulture

				' Create a worksheet
				Using sheet As IXlSheet = document.CreateSheet()
					sheet.SkipColumns(1)
					Using column As IXlColumn = sheet.CreateColumn()
						column.WidthInPixels = 205
					End Using
					sheet.SkipRows(1)
					Using row As IXlRow = sheet.CreateRow()
						row.HeightInPixels = 154
					End Using

					' Insert a picture from a file and stretch it to fill the cell B2
					Using picture As IXlPicture = sheet.CreatePicture()
						picture.Image = Image.FromFile(Path.Combine(imagesPath, "image1.jpg"))
						picture.StretchToCell(New XlCellPosition(1, 1)) ' B2
					End Using
				End Using
			End Using

'			#End Region ' #StretchPicture
		End Sub

		Private Shared Sub FitPicture(ByVal stream As Stream, ByVal documentFormat As XlDocumentFormat, ByVal imagesPath As String)
'			#Region "#FitPicture"
			' Create an exporter instance
			Dim exporter As IXlExporter = XlExport.CreateExporter(documentFormat)

			' Create a new document
			Using document As IXlDocument = exporter.CreateDocument(stream)
				document.Options.Culture = CultureInfo.CurrentCulture

				' Create a worksheet
				Using sheet As IXlSheet = document.CreateSheet()
					sheet.SkipColumns(1)
					Using column As IXlColumn = sheet.CreateColumn()
						column.WidthInPixels = 300
					End Using
					sheet.SkipRows(1)
					Using row As IXlRow = sheet.CreateRow()
						row.HeightInPixels = 154
					End Using

					' Insert a picture from a file to fit in the cell B2
					Using picture As IXlPicture = sheet.CreatePicture()
						picture.Image = Image.FromFile(Path.Combine(imagesPath, "image1.jpg"))
						picture.FitToCell(New XlCellPosition(1, 1), 300, 154, True)
					End Using
				End Using
			End Using

'			#End Region ' #FitPicture
		End Sub

		Private Shared Sub PictureHyperlinkClick(ByVal stream As Stream, ByVal documentFormat As XlDocumentFormat, ByVal imagesPath As String)
'			#Region "#HyperlinkClick"
			' Create an exporter instance
			Dim exporter As IXlExporter = XlExport.CreateExporter(documentFormat)

			' Create a new document
			Using document As IXlDocument = exporter.CreateDocument(stream)
				document.Options.Culture = CultureInfo.CurrentCulture

				' Create a worksheet
				Using sheet As IXlSheet = document.CreateSheet()

					' Load a picture from a file and add a hyperlink to it
					Using picture As IXlPicture = sheet.CreatePicture()
						picture.Image = Image.FromFile(Path.Combine(imagesPath, "DevExpress.png"))
						picture.HyperlinkClick.TargetUri = "http://www.devexpress.com"
						picture.HyperlinkClick.Tooltip = "Developer Express Inc."
						picture.SetTwoCellAnchor(New XlAnchorPoint(1, 1, 0, 0), New XlAnchorPoint(10, 5, 2, 15), XlAnchorType.TwoCell)
					End Using
				End Using
			End Using

'			#End Region ' #HyperlinkClick
		End Sub

	End Class
End Namespace