Imports System
Imports System.IO
Imports DevExpress.Spreadsheet

Namespace SpreadsheetExamples
	Public NotInheritable Class HeaderFooterActions
		Private Sub New()
		End Sub
		Private Shared Sub AddHeaderFooter(ByVal workbook As IWorkbook)
'			#Region "#AddHeaderFooter"
			Dim worksheet As Worksheet = workbook.Worksheets("HeaderFooter")
			workbook.Worksheets.ActiveWorksheet = worksheet
			Dim headerFooter As WorksheetHeaderFooterOptions = worksheet.HeaderFooterOptions

			' Add headers to first page
			headerFooter.DifferentFirst = True
			headerFooter.FirstHeader.Left = "File path: " & HeaderFooterCode.WorkbookFilePath + HeaderFooterCode.WorkbookFileName & ".xlsx"
			headerFooter.FirstHeader.Right = "Total number of pages: " & HeaderFooterCode.PageTotal

			' Add footers to first page, using FromLCR method
			Dim leftFooter As String = "Current date: " & HeaderFooterCode.Date
			Dim centerFooter As String = "Current time: " & HeaderFooterCode.Time
			Dim rightFooter As String = "First page"
			headerFooter.FirstFooter.FromLCR(leftFooter, centerFooter, rightFooter)

			' Add header to even pages
			headerFooter.DifferentOddEven = True
			headerFooter.EvenHeader.Right = "This page number is even: " & HeaderFooterCode.PageNumber

			' Add footer to odd pages, using FromString method
			Dim oddPageFooter As String = HeaderFooterCode.RightSection & "This page number is odd: " & HeaderFooterCode.PageNumber
			headerFooter.OddFooter.FromString(oddPageFooter)
'			#End Region ' #AddHeaderFooter
		End Sub

		Private Shared Sub AddPicture(ByVal workbook As IWorkbook, ByVal rootPath As String)
'			#Region "#AddPicture"
			Dim worksheet As Worksheet = workbook.Worksheets("HeaderFooter")
			workbook.Worksheets.ActiveWorksheet = worksheet
			Dim oddHeaderFooter As WorksheetHeaderFooter = worksheet.HeaderFooterOptions.OddHeader

			' Add a picture to center header
			Dim filePath As String = Path.Combine(rootPath & "\DevExpress.png")
			oddHeaderFooter.AddPicture(filePath, HeaderFooterSection.Center)

			' Change width to fit picture
			oddHeaderFooter.CenterPicture.Width = 500
'			#End Region ' #AddPicture
		End Sub

		Private Shared Sub RemovePicture(ByVal workbook As IWorkbook, ByVal rootPath As String)
'			#Region "#RemovePicture"
			Dim worksheet As Worksheet = workbook.Worksheets("HeaderFooter")
			workbook.Worksheets.ActiveWorksheet = worksheet
			Dim oddHeaderFooter As WorksheetHeaderFooter = worksheet.HeaderFooterOptions.OddHeader

			' Add a picture to center header
			Dim filePath As String = Path.Combine(rootPath & "\DevExpress.png")
			oddHeaderFooter.AddPicture(filePath, HeaderFooterSection.Center)
			oddHeaderFooter.CenterPicture.Width = 500

			' Remove picture from center header
			oddHeaderFooter.RemovePicture(HeaderFooterSection.Center)
'			#End Region ' #RemovePicture
		End Sub

		Private Shared Sub FormatPicture(ByVal workbook As IWorkbook, ByVal rootPath As String)
'			#Region "#FormatPicture"
			Dim worksheet As Worksheet = workbook.Worksheets("HeaderFooter")
			workbook.Worksheets.ActiveWorksheet = worksheet
			Dim oddHeaderFooter As WorksheetHeaderFooter = worksheet.HeaderFooterOptions.OddHeader

			' Add a picture to center header
			Dim filePath As String = Path.Combine(rootPath & "\DevExpress.png")
			Dim picture As HeaderFooterPicture = oddHeaderFooter.AddPicture(filePath, HeaderFooterSection.Center)

			' Change sizes
			picture.LockAspectRatio = False
			picture.Width = 500
			picture.Height = 80

			' Apply crop
			picture.CropLeft = 10
			picture.CropRight = 2100
			picture.CropTop = 10
			picture.CropBottom = 50
'			#End Region ' #FormatPicture
		End Sub
	End Class
End Namespace