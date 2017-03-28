Imports System
Imports System.Drawing
Imports DevExpress.Spreadsheet
Imports System.Collections.Generic
Imports Formatting = DevExpress.Spreadsheet.Formatting
Imports DevExpress.Utils

Namespace SpreadsheetExamples
	Public NotInheritable Class ConditionalFormatting
		Private Sub New()
		End Sub
		Private Shared Sub AddAverageConditionalFormatting(ByVal workbook As IWorkbook)
'			#Region "#AverageConditionalFormatting"
			Dim worksheet As Worksheet = workbook.Worksheets("cfBooks")
			workbook.Worksheets.ActiveWorksheet = worksheet
			Dim conditionalFormattings As ConditionalFormattingCollection = worksheet.ConditionalFormattings
			' Create the rule highlighting values that are above the average in cells D5 through D18.  
			Dim cfRule1 As AverageConditionalFormatting = conditionalFormattings.AddAverageConditionalFormatting(worksheet("$D$5:$D$18"), ConditionalFormattingAverageCondition.AboveOrEqual)
			' Specify formatting options to be applied to cells if the condition is true.
			' Set the background color to yellow.
			cfRule1.Formatting.Fill.BackgroundColor = Color.FromArgb(255, &HFA, &HF7, &HAA)
			' Set the font color to red.
			cfRule1.Formatting.Font.Color = Color.Red
			' Create the rule highlighting values that are one standard deviation below the mean in cells E5 through E18.
			Dim cfRule2 As AverageConditionalFormatting = conditionalFormattings.AddAverageConditionalFormatting(worksheet("$E$5:$E$18"), ConditionalFormattingAverageCondition.BelowOrEqual, 1)
			' Specify formatting options to be applied to cells if the conditions is true.
			' Set the background color to light-green.
			cfRule2.Formatting.Fill.BackgroundColor = Color.FromArgb(255, &H9F, &HFB, &H69)
			' Set the font color to blue-violet.
			cfRule2.Formatting.Font.Color = Color.BlueViolet

			' Add an explanation to the created rule.
			worksheet("B2").Value = "In the report below determine cost values that are above the average in the first quarter and one standard deviation below the average in the second quarter."
			worksheet.Visible = True
'			#End Region ' #AverageConditionalFormatting
		End Sub

		Private Shared Sub AddRangeConditionalFormatting(ByVal workbook As IWorkbook)
'			#Region "#RangeConditionalFormatting"
			Dim worksheet As Worksheet = workbook.Worksheets("cfBooks")
			workbook.Worksheets.ActiveWorksheet = worksheet
			' Create the rule to identify values below 7 and above 19 in cells G5 through G18.
			Dim cfRule As RangeConditionalFormatting = worksheet.ConditionalFormattings.AddRangeConditionalFormatting(worksheet("$G$5:$G$18"), ConditionalFormattingRangeCondition.Outside, "7", "19")
			' Specify formatting options to be applied to cells if the condition is true.
			' Set the background color to yellow.
			cfRule.Formatting.Fill.BackgroundColor = Color.FromArgb(255, &HFA, &HF7, &HAA)
			' Set the font color to red.
			cfRule.Formatting.Font.Color = Color.Red

			' Add an explanation to the created rule.
			worksheet("B2").Value = "In the report below identify price values that are less than $7 and greater than $19."
			worksheet.Visible = True
'			#End Region ' #RangeConditionalFormatting
		End Sub

		Private Shared Sub AddRankConditionalFormatting(ByVal workbook As IWorkbook)
'			#Region "#RankConditionalFormatting"
			Dim worksheet As Worksheet = workbook.Worksheets("cfBooks")
			workbook.Worksheets.ActiveWorksheet = worksheet
			' Create the rule to identify top three values in cells G5 through G18.
			Dim cfRule As RankConditionalFormatting = worksheet.ConditionalFormattings.AddRankConditionalFormatting(worksheet("$G$5:$G$18"), ConditionalFormattingRankCondition.TopByRank, 3)
			' Specify formatting options to be applied to cells if the condition is true.
			' Set the background color to dark orchid.
			cfRule.Formatting.Fill.BackgroundColor = Color.DarkOrchid
			' Set the outline borders.
			cfRule.Formatting.Borders.SetOutsideBorders(Color.Black, BorderLineStyle.Thin)
			' Set the font color to white.
			cfRule.Formatting.Font.Color = Color.White

			' Add an explanation to the created rule.
			worksheet("B2").Value = "In the report below identify the top three price values."
			worksheet.Visible = True
'			#End Region ' #RankConditionalFormatting
		End Sub

		Private Shared Sub AddTextConditionalFormatting(ByVal workbook As IWorkbook)
'			#Region "#TextConditionalFormatting"
			Dim worksheet As Worksheet = workbook.Worksheets("cfBooks")
			workbook.Worksheets.ActiveWorksheet = worksheet
			' Create the rule to highlight values with the given text string in cells B5 through B18.
			Dim cfRule As TextConditionalFormatting = worksheet.ConditionalFormattings.AddTextConditionalFormatting(worksheet("$B$5:$B$18"), ConditionalFormattingTextCondition.Contains, "Bradbury")
			' Specify formatting options to be applied to cells if the condition is true.
			' Set the background color to pink.
			cfRule.Formatting.Fill.BackgroundColor = Color.FromArgb(255, &HE1, &H95, &HC2)

			' Add an explanation to the created rule.
			worksheet("B2").Value = "Quickly find books written by Ray Bradbury in the report below."
			worksheet.Visible = True
'			#End Region ' #TextConditionalFormatting
		End Sub


		Private Shared Sub AddSpecialConditionalFormatting(ByVal workbook As IWorkbook)
'			#Region "#SpecialConditionalFormatting"
			Dim worksheet As Worksheet = workbook.Worksheets("cfBooks")
			workbook.Worksheets.ActiveWorksheet = worksheet
			' Create the rule to identify unique values in cells B5 through B18.
			Dim cfRule As SpecialConditionalFormatting = worksheet.ConditionalFormattings.AddSpecialConditionalFormatting(worksheet("$B$5:$B$18"), ConditionalFormattingSpecialCondition.ContainUniqueValue)
			' Specify formatting options to be applied to cells if the condition is true.
			' Set the background color to yellow.
			cfRule.Formatting.Fill.BackgroundColor = Color.FromArgb(255, &HFA, &HF7, &HAA)

			' Add an explanation to the created rule.
			worksheet("B2").Value = "Quickly identify unique values in the list of authors."
			worksheet.Visible = True
'			#End Region ' #SpecialConditionalFormatting
		End Sub


		Private Shared Sub AddTimePeriodConditionalFormatting(ByVal workbook As IWorkbook)
'			#Region "#TimePeriodConditionalFormatting"
			workbook.Calculate()
			Dim worksheet As Worksheet = workbook.Worksheets("cfTasks")
			workbook.Worksheets.ActiveWorksheet = worksheet
			' Create the rule to highlight today's dates in cells C5 through C9.
			Dim cfRule As TimePeriodConditionalFormatting = worksheet.ConditionalFormattings.AddTimePeriodConditionalFormatting(worksheet("$C$5:$C$9"), ConditionalFormattingTimePeriod.Today)
			' Specify formatting options to be applied to cells if the condition is true.
			' Set the background color to pink.
			cfRule.Formatting.Fill.BackgroundColor = Color.FromArgb(255, &HF2, &HAE, &HE3)

			' Add an explanation to the created rule.
			worksheet("B2").Value = "Determine the today's task in the TO DO list."
			worksheet.Visible = True
'			#End Region ' #TimePeriodConditionalFormatting
		End Sub


		Private Shared Sub AddExpressionConditionalFormatting(ByVal workbook As IWorkbook)
'			#Region "#ExpressionConditionalFormatting"
			Dim worksheet As Worksheet = workbook.Worksheets("cfBooks")
			workbook.Worksheets.ActiveWorksheet = worksheet
			' Create the rule to identify values that are above the average in cells G5 through G18.
			Dim cfRule As ExpressionConditionalFormatting = worksheet.ConditionalFormattings.AddExpressionConditionalFormatting(worksheet("$G$5:$G$18"), ConditionalFormattingExpressionCondition.GreaterThan, "=AVERAGE($G$5:$G$18)")
			' Specify formatting options to be applied to cells if the condition is true.
			' Set the background color to yellow.
			cfRule.Formatting.Fill.BackgroundColor = Color.FromArgb(255, &HFA, &HF7, &HAA)
			' Set the font color to red.
			cfRule.Formatting.Font.Color = Color.Red

			' Add an explanation to the created rule.
			worksheet("B2").Value = "In the report below identify price values that are above the average."
			worksheet.Visible = True
'			#End Region ' #ExpressionConditionalFormatting
		End Sub


		Private Shared Sub AddFormulaExpressionConditionalFormatting(ByVal workbook As IWorkbook)
'			#Region "#FormulaExpressionConditionalFormatting"
			Dim worksheet As Worksheet = workbook.Worksheets("cfBooks")
			workbook.Worksheets.ActiveWorksheet = worksheet
			' Create the rule to shade alternate rows without applying a new style.
			Dim cfRule As FormulaExpressionConditionalFormatting = worksheet.ConditionalFormattings.AddFormulaExpressionConditionalFormatting(worksheet.Range("$B$5:$H$18"), "=MOD(ROW(),2)=1")
			' Specify formatting options to be applied to cells if the condition is true.
			' Set the background color to light blue.
			cfRule.Formatting.Fill.BackgroundColor = Color.FromArgb(255, &HBC, &HDA, &HF7)

			' Add an explanation to the created rule.
			worksheet("B2").Value = "Shade alternate rows in light blue without applying a new style."
			worksheet.Visible = True
'			#End Region ' #FormulaExpressionConditionalFormatting
		End Sub


		Private Shared Sub AddColorScale2ConditionalFormatting(ByVal workbook As IWorkbook)
'			#Region "#ColorScale2ConditionalFormatting"
			Dim worksheet As Worksheet = workbook.Worksheets("cfBooks")
			workbook.Worksheets.ActiveWorksheet = worksheet
			' Set the minimum threshold to 0%.
            Dim minPoint As ConditionalFormattingValue = worksheet.ConditionalFormattings.CreateValue(ConditionalFormattingValueType.Percent, "0")
			' Set the maximum threshold to 100%.
            Dim maxPoint As ConditionalFormattingValue = worksheet.ConditionalFormattings.CreateValue(ConditionalFormattingValueType.Percent, "100")
			' Create the two-color scale rule to differentiate low and high values in cells D5 through E18. Blue represents the lower values and yellow represents the higher values. 
			Dim cfRule As ColorScale2ConditionalFormatting = worksheet.ConditionalFormattings.AddColorScale2ConditionalFormatting(worksheet.Range("$D$5:$E$18"), minPoint, Color.FromArgb(255, &H9D, &HE9, &HFA), maxPoint, Color.FromArgb(255, &HFF, &HF6, &HA9))

			' Add an explanation to the created rule.
			worksheet("B2").Value = "Examine cost distribution using a gradation of two colors. Blue represents the lower values and yellow represents the higher values."
			worksheet.Visible = True
'			#End Region ' #ColorScale2ConditionalFormatting
		End Sub


		Private Shared Sub AddColorScale2ConditionalFormatting_Extremum(ByVal workbook As IWorkbook)
'			#Region "#ColorScale2ConditionalFormatting_Extremum"
			Dim worksheet As Worksheet = workbook.Worksheets("cfBooks")
			workbook.Worksheets.ActiveWorksheet = worksheet
			' Automatically set the minimum threshold to the lowest value in a range of cells.
            Dim minPoint As ConditionalFormattingValue = worksheet.ConditionalFormattings.CreateValue(ConditionalFormattingValueType.MinMax)
            ' Automatically set the maximum threshold to the highest value in a range of cells.
            Dim maxPoint As ConditionalFormattingValue = worksheet.ConditionalFormattings.CreateValue(ConditionalFormattingValueType.MinMax)
			' Create the two-color scale rule to differentiate low and high values in cells D5 through E18. Blue represents the lower values and yellow represents the higher values. 
			Dim cfRule As ColorScale2ConditionalFormatting = worksheet.ConditionalFormattings.AddColorScale2ConditionalFormatting(worksheet.Range("$D$5:$E$18"), minPoint, Color.FromArgb(255, &H9D, &HE9, &HFA), maxPoint, Color.FromArgb(255, &HFF, &HF6, &HA9))

			' Add an explanation to the created rule.
			worksheet("B2").Value = "Examine cost distribution using a gradation of two colors. Blue represents the lower values and yellow represents the higher values."
			worksheet.Visible = True
'			#End Region ' #ColorScale2ConditionalFormatting_Extremum
		End Sub


		Private Shared Sub AddColorScale3ConditionalFormatting(ByVal workbook As IWorkbook)
'			#Region "#ColorScale3ConditionalFormatting"
			Dim worksheet As Worksheet = workbook.Worksheets("cfBooks")
			workbook.Worksheets.ActiveWorksheet = worksheet
			' Set the minimum threshold to the lowest value in the range of cells using the MIN() formula.
            Dim minPoint As ConditionalFormattingValue = worksheet.ConditionalFormattings.CreateValue(ConditionalFormattingValueType.Formula, "=MIN($E$5:$F$18)")
			' Set the midpoint threshold to the 50th percentile.
            Dim midPoint As ConditionalFormattingValue = worksheet.ConditionalFormattings.CreateValue(ConditionalFormattingValueType.Percentile, "50")
			' Set the maximum threshold to the highest value in the range of cells using the MAX() formula.
            Dim maxPoint As ConditionalFormattingValue = worksheet.ConditionalFormattings.CreateValue(ConditionalFormattingValueType.Number, "=MAX($E$5:$F$18)")
			' Create the three-color scale rule to determine how values in cells D5 through E18 vary. Red represents the lower values, yellow represents the medium values and sky blue represents the higher values.
			Dim cfRule As ColorScale3ConditionalFormatting = worksheet.ConditionalFormattings.AddColorScale3ConditionalFormatting(worksheet.Range("$D$5:$E$18"), minPoint, Color.Red, midPoint, Color.Yellow, maxPoint, Color.SkyBlue)

			' Add an explanation to the created rule.
			worksheet("B2").Value = "Examine cost distribution using a gradation of three colors. Red represents the lower values, yellow represents the medium values and sky blue represents the higher values."
			worksheet.Visible = True
'			#End Region ' #ColorScale3ConditionalFormatting
		End Sub

		Private Shared Sub AddDataBarConditionalFormatting(ByVal workbook As IWorkbook)
'			#Region "#DataBarConditionalFormatting"
			Dim worksheet As Worksheet = workbook.Worksheets("cfBooks")
			workbook.Worksheets.ActiveWorksheet = worksheet
			' Automatically set the value corresponding to the shortest bar to the lowest value in a range.
            Dim lowBound1 As ConditionalFormattingValue = worksheet.ConditionalFormattings.CreateValue(ConditionalFormattingValueType.MinMax)
			' Automatically set the value corresponding to the longest bar to the highest value in a range.
            Dim highBound1 As ConditionalFormattingValue = worksheet.ConditionalFormattings.CreateValue(ConditionalFormattingValueType.MinMax)
			' Create the rule to compare values in cells F5 through F18 using data bars. 
			Dim cfRule1 As DataBarConditionalFormatting = worksheet.ConditionalFormattings.AddDataBarConditionalFormatting(worksheet.Range("$F$5:$F$18"), lowBound1, highBound1, Color.Green)
			' Set the positive bar border color to green.
			cfRule1.BorderColor = Color.Green
			' Set the negative bar color to red.
			cfRule1.NegativeBarColor = Color.Red
			' Set the negative bar border color to red.
			cfRule1.NegativeBarBorderColor = Color.Red
			' Set the axis position to display the axis in the middle of the cell.
			cfRule1.AxisPosition = ConditionalFormattingDataBarAxisPosition.Middle
			' Set the axis color to dark blue.
			cfRule1.AxisColor = Color.DarkBlue

			' Set the value corresponding to the shortest bar to 0 percent.
            Dim lowBound2 As ConditionalFormattingValue = worksheet.ConditionalFormattings.CreateValue(ConditionalFormattingValueType.Percent, "0")
			' Set the value corresponding to the longest bar to 100 percent.
            Dim highBound2 As ConditionalFormattingValue = worksheet.ConditionalFormattings.CreateValue(ConditionalFormattingValueType.Percent, "100")
			' Create the rule to compare values in cells H5 through H18 using data bars.  
			Dim cfRule2 As DataBarConditionalFormatting = worksheet.ConditionalFormattings.AddDataBarConditionalFormatting(worksheet.Range("$H$5:$H$18"), lowBound2, highBound2, Color.SkyBlue)
			' Set the data bar border color to sky blue.
			cfRule2.BorderColor = Color.SkyBlue
			' Specify the solid fill type.
			cfRule2.GradientFill = False
			' Hide values of cells to which the rule is applied.
			cfRule2.ShowValue = False

			' Add an explanation to the created rule.
			worksheet("B2").Value = "Compare values in the ""Cost Trend"" and ""Markup"" columns using data bars."
			worksheet.Visible = True
'			#End Region ' #DataBarConditionalFormatting
		End Sub

		Private Shared Sub AddIconSetConditionalFormatting(ByVal workbook As IWorkbook)
'			#Region "#IconSetConditionalFormatting"
			Dim worksheet As Worksheet = workbook.Worksheets("cfBooks")
			workbook.Worksheets.ActiveWorksheet = worksheet
			' Set the first threshold to the lowest value in the range of cells using the MIN() formula.
            Dim minPoint As ConditionalFormattingIconSetValue = worksheet.ConditionalFormattings.CreateIconSetValue(ConditionalFormattingValueType.Formula, "=MIN($F$5:$F$18)", ConditionalFormattingValueOperator.GreaterOrEqual)
			' Set the second threshold to 0.
            Dim midPoint As ConditionalFormattingIconSetValue = worksheet.ConditionalFormattings.CreateIconSetValue(ConditionalFormattingValueType.Number, "0", ConditionalFormattingValueOperator.GreaterOrEqual)
			' Set the third threshold to 0.01.
            Dim maxPoint As ConditionalFormattingIconSetValue = worksheet.ConditionalFormattings.CreateIconSetValue(ConditionalFormattingValueType.Number, "0.01", ConditionalFormattingValueOperator.GreaterOrEqual)
			' Create the rule to apply a specific icon from the three arrows icon set to each cell in the range E4:E17 based on its value.  
            Dim cfRule As IconSetConditionalFormatting = worksheet.ConditionalFormattings.AddIconSetConditionalFormatting(worksheet.Range("$F$5:$F$18"), IconSetType.Arrows3, New ConditionalFormattingIconSetValue() {minPoint, midPoint, maxPoint})
			' Specify the custom icon to be displayed if the second condition is true. 
			' To do this, set the IconSetConditionalFormatting.IsCustom property to true.
			cfRule.IsCustom = True
			' Initialize the ConditionalFormattingCustomIcon object.
			Dim cfCustomIcon As New ConditionalFormattingCustomIcon()
			' Specify the icon set where you wish to get the icon. 
			cfCustomIcon.IconSet = IconSetType.TrafficLights13
			' Specify the index of the desired icon in the set.
			cfCustomIcon.IconIndex = 1
			' Add the custom icon at the specified position in the initial icon set.
			cfRule.SetCustomIcon(1, cfCustomIcon)
			' Hide values of cells to which the rule is applied.
			cfRule.ShowValue = False

			' Add an explanation to the created rule.
			worksheet("B2").Value = "In the report below identify upward and downward cost trends."
			worksheet.Visible = True
'			#End Region ' #IconSetConditionalFormatting
		End Sub
	End Class
End Namespace