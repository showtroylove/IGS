using System;
using System.Drawing;
using DevExpress.Spreadsheet;
using System.Collections.Generic;
using Formatting = DevExpress.Spreadsheet.Formatting;
using DevExpress.Utils;

namespace SpreadsheetExamples {
    public static class ConditionalFormatting {
        static void AddAverageConditionalFormatting(IWorkbook workbook) {
            #region #AverageConditionalFormatting
            Worksheet worksheet = workbook.Worksheets["cfBooks"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            ConditionalFormattingCollection conditionalFormattings = worksheet.ConditionalFormattings;
            // Create the rule highlighting values that are above the average in cells D5 through D18.  
            AverageConditionalFormatting cfRule1 = conditionalFormattings.AddAverageConditionalFormatting(worksheet["$D$5:$D$18"], ConditionalFormattingAverageCondition.AboveOrEqual);
            // Specify formatting options to be applied to cells if the condition is true.
            // Set the background color to yellow.
            cfRule1.Formatting.Fill.BackgroundColor = Color.FromArgb(255, 0xFA, 0xF7, 0xAA);
            // Set the font color to red.
            cfRule1.Formatting.Font.Color = Color.Red;
            // Create the rule highlighting values that are one standard deviation below the mean in cells E5 through E18.
            AverageConditionalFormatting cfRule2 = conditionalFormattings.AddAverageConditionalFormatting(worksheet["$E$5:$E$18"], ConditionalFormattingAverageCondition.BelowOrEqual, 1);
            // Specify formatting options to be applied to cells if the conditions is true.
            // Set the background color to light-green.
            cfRule2.Formatting.Fill.BackgroundColor = Color.FromArgb(255, 0x9F, 0xFB, 0x69);
            // Set the font color to blue-violet.
            cfRule2.Formatting.Font.Color = Color.BlueViolet;

            // Add an explanation to the created rule.
            worksheet["B2"].Value = "In the report below determine cost values that are above the average in the first quarter and one standard deviation below the average in the second quarter.";
            worksheet.Visible = true;
            #endregion #AverageConditionalFormatting
        }

        static void AddRangeConditionalFormatting(IWorkbook workbook) {
            #region #RangeConditionalFormatting
            Worksheet worksheet = workbook.Worksheets["cfBooks"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            // Create the rule to identify values below 7 and above 19 in cells G5 through G18.
            RangeConditionalFormatting cfRule = worksheet.ConditionalFormattings.AddRangeConditionalFormatting(worksheet["$G$5:$G$18"], ConditionalFormattingRangeCondition.Outside, "7", "19");
            // Specify formatting options to be applied to cells if the condition is true.
            // Set the background color to yellow.
            cfRule.Formatting.Fill.BackgroundColor = Color.FromArgb(255, 0xFA, 0xF7, 0xAA);
            // Set the font color to red.
            cfRule.Formatting.Font.Color = Color.Red;

            // Add an explanation to the created rule.
            worksheet["B2"].Value = "In the report below identify price values that are less than $7 and greater than $19.";
            worksheet.Visible = true;
            #endregion #RangeConditionalFormatting
        }

        static void AddRankConditionalFormatting(IWorkbook workbook) {
            #region #RankConditionalFormatting
            Worksheet worksheet = workbook.Worksheets["cfBooks"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            // Create the rule to identify top three values in cells G5 through G18.
            RankConditionalFormatting cfRule = worksheet.ConditionalFormattings.AddRankConditionalFormatting(worksheet["$G$5:$G$18"], ConditionalFormattingRankCondition.TopByRank, 3);
            // Specify formatting options to be applied to cells if the condition is true.
            // Set the background color to dark orchid.
            cfRule.Formatting.Fill.BackgroundColor = Color.DarkOrchid;
            // Set the outline borders.
            cfRule.Formatting.Borders.SetOutsideBorders(Color.Black, BorderLineStyle.Thin);
            // Set the font color to white.
            cfRule.Formatting.Font.Color = Color.White;

            // Add an explanation to the created rule.
            worksheet["B2"].Value = "In the report below identify the top three price values.";
            worksheet.Visible = true;
            #endregion #RankConditionalFormatting
        }

        static void AddTextConditionalFormatting(IWorkbook workbook) {
            #region #TextConditionalFormatting
            Worksheet worksheet = workbook.Worksheets["cfBooks"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            // Create the rule to highlight values with the given text string in cells B5 through B18.
            TextConditionalFormatting cfRule = worksheet.ConditionalFormattings.AddTextConditionalFormatting(worksheet["$B$5:$B$18"], ConditionalFormattingTextCondition.Contains, "Bradbury");
            // Specify formatting options to be applied to cells if the condition is true.
            // Set the background color to pink.
            cfRule.Formatting.Fill.BackgroundColor = Color.FromArgb(255, 0xE1, 0x95, 0xC2);

            // Add an explanation to the created rule.
            worksheet["B2"].Value = "Quickly find books written by Ray Bradbury in the report below.";
            worksheet.Visible = true;
            #endregion #TextConditionalFormatting
        }


        static void AddSpecialConditionalFormatting(IWorkbook workbook) {
            #region #SpecialConditionalFormatting
            Worksheet worksheet = workbook.Worksheets["cfBooks"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            // Create the rule to identify unique values in cells B5 through B18.
            SpecialConditionalFormatting cfRule = worksheet.ConditionalFormattings.AddSpecialConditionalFormatting(worksheet["$B$5:$B$18"], ConditionalFormattingSpecialCondition.ContainUniqueValue);
            // Specify formatting options to be applied to cells if the condition is true.
            // Set the background color to yellow.
            cfRule.Formatting.Fill.BackgroundColor = Color.FromArgb(255, 0xFA, 0xF7, 0xAA);

            // Add an explanation to the created rule.
            worksheet["B2"].Value = "Quickly identify unique values in the list of authors.";
            worksheet.Visible = true;
            #endregion #SpecialConditionalFormatting
        }


        static void AddTimePeriodConditionalFormatting(IWorkbook workbook) {
            #region #TimePeriodConditionalFormatting
            workbook.Calculate();
            Worksheet worksheet = workbook.Worksheets["cfTasks"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            // Create the rule to highlight today's dates in cells C5 through C9.
            TimePeriodConditionalFormatting cfRule = worksheet.ConditionalFormattings.AddTimePeriodConditionalFormatting(worksheet["$C$5:$C$9"], ConditionalFormattingTimePeriod.Today);
            // Specify formatting options to be applied to cells if the condition is true.
            // Set the background color to pink.
            cfRule.Formatting.Fill.BackgroundColor = Color.FromArgb(255, 0xF2, 0xAE, 0xE3);

            // Add an explanation to the created rule.
            worksheet["B2"].Value = "Determine the today's task in the TO DO list.";
            worksheet.Visible = true;
            #endregion #TimePeriodConditionalFormatting
        }


        static void AddExpressionConditionalFormatting(IWorkbook workbook) {
            #region #ExpressionConditionalFormatting
            Worksheet worksheet = workbook.Worksheets["cfBooks"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            // Create the rule to identify values that are above the average in cells G5 through G18.
            ExpressionConditionalFormatting cfRule = worksheet.ConditionalFormattings.AddExpressionConditionalFormatting(worksheet["$G$5:$G$18"], ConditionalFormattingExpressionCondition.GreaterThan, "=AVERAGE($G$5:$G$18)");
            // Specify formatting options to be applied to cells if the condition is true.
            // Set the background color to yellow.
            cfRule.Formatting.Fill.BackgroundColor = Color.FromArgb(255, 0xFA, 0xF7, 0xAA);
            // Set the font color to red.
            cfRule.Formatting.Font.Color = Color.Red;

            // Add an explanation to the created rule.
            worksheet["B2"].Value = "In the report below identify price values that are above the average.";
            worksheet.Visible = true;
            #endregion #ExpressionConditionalFormatting
        }


        static void AddFormulaExpressionConditionalFormatting(IWorkbook workbook) {
            #region #FormulaExpressionConditionalFormatting
            Worksheet worksheet = workbook.Worksheets["cfBooks"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            // Create the rule to shade alternate rows without applying a new style.
            FormulaExpressionConditionalFormatting cfRule = worksheet.ConditionalFormattings.AddFormulaExpressionConditionalFormatting(worksheet.Range["$B$5:$H$18"], "=MOD(ROW(),2)=1");
            // Specify formatting options to be applied to cells if the condition is true.
            // Set the background color to light blue.
            cfRule.Formatting.Fill.BackgroundColor = Color.FromArgb(255, 0xBC, 0xDA, 0xF7);

            // Add an explanation to the created rule.
            worksheet["B2"].Value = "Shade alternate rows in light blue without applying a new style.";
            worksheet.Visible = true;
            #endregion #FormulaExpressionConditionalFormatting
        }


        static void AddColorScale2ConditionalFormatting(IWorkbook workbook) {
            #region #ColorScale2ConditionalFormatting
            Worksheet worksheet = workbook.Worksheets["cfBooks"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            // Set the minimum threshold to 0%.
            ConditionalFormattingValue minPoint = worksheet.ConditionalFormattings.CreateValue(ConditionalFormattingValueType.Percent, "0");
            // Set the maximum threshold to 100%.
            ConditionalFormattingValue maxPoint = worksheet.ConditionalFormattings.CreateValue(ConditionalFormattingValueType.Percent, "100");
            // Create the two-color scale rule to differentiate low and high values in cells D5 through E18. Blue represents the lower values and yellow represents the higher values. 
            ColorScale2ConditionalFormatting cfRule = worksheet.ConditionalFormattings.AddColorScale2ConditionalFormatting(worksheet.Range["$D$5:$E$18"], minPoint, Color.FromArgb(255, 0x9D, 0xE9, 0xFA), maxPoint, Color.FromArgb(255, 0xFF, 0xF6, 0xA9));

            // Add an explanation to the created rule.
            worksheet["B2"].Value = "Examine cost distribution using a gradation of two colors. Blue represents the lower values and yellow represents the higher values.";
            worksheet.Visible = true;
            #endregion #ColorScale2ConditionalFormatting
        }


        static void AddColorScale2ConditionalFormatting_Extremum(IWorkbook workbook) {
            #region #ColorScale2ConditionalFormatting_Extremum
            Worksheet worksheet = workbook.Worksheets["cfBooks"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            // Automatically set the minimum threshold to the lowest value in a range of cells.
            ConditionalFormattingValue minPoint = worksheet.ConditionalFormattings.CreateValue(ConditionalFormattingValueType.MinMax);
            // Automatically set the maximum threshold to the highest value in a range of cells.
            ConditionalFormattingValue maxPoint = worksheet.ConditionalFormattings.CreateValue(ConditionalFormattingValueType.MinMax);
            // Create the two-color scale rule to differentiate low and high values in cells D5 through E18. Blue represents the lower values and yellow represents the higher values. 
            ColorScale2ConditionalFormatting cfRule = worksheet.ConditionalFormattings.AddColorScale2ConditionalFormatting(worksheet.Range["$D$5:$E$18"], minPoint, Color.FromArgb(255, 0x9D, 0xE9, 0xFA), maxPoint, Color.FromArgb(255, 0xFF, 0xF6, 0xA9));

            // Add an explanation to the created rule.
            worksheet["B2"].Value = "Examine cost distribution using a gradation of two colors. Blue represents the lower values and yellow represents the higher values.";
            worksheet.Visible = true;
            #endregion #ColorScale2ConditionalFormatting_Extremum
        }


        static void AddColorScale3ConditionalFormatting(IWorkbook workbook) {
            #region #ColorScale3ConditionalFormatting
            Worksheet worksheet = workbook.Worksheets["cfBooks"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            // Set the minimum threshold to the lowest value in the range of cells using the MIN() formula.
            ConditionalFormattingValue minPoint = worksheet.ConditionalFormattings.CreateValue(ConditionalFormattingValueType.Formula, "=MIN($E$5:$F$18)");
            // Set the midpoint threshold to the 50th percentile.
            ConditionalFormattingValue midPoint = worksheet.ConditionalFormattings.CreateValue(ConditionalFormattingValueType.Percentile, "50");
            // Set the maximum threshold to the highest value in the range of cells using the MAX() formula.
            ConditionalFormattingValue maxPoint = worksheet.ConditionalFormattings.CreateValue(ConditionalFormattingValueType.Number, "=MAX($E$5:$F$18)");
            // Create the three-color scale rule to determine how values in cells D5 through E18 vary. Red represents the lower values, yellow represents the medium values and sky blue represents the higher values.
            ColorScale3ConditionalFormatting cfRule = worksheet.ConditionalFormattings.AddColorScale3ConditionalFormatting(worksheet.Range["$D$5:$E$18"], minPoint, Color.Red, midPoint, Color.Yellow, maxPoint, Color.SkyBlue);

            // Add an explanation to the created rule.
            worksheet["B2"].Value = "Examine cost distribution using a gradation of three colors. Red represents the lower values, yellow represents the medium values and sky blue represents the higher values.";
            worksheet.Visible = true;
            #endregion #ColorScale3ConditionalFormatting
        }

        static void AddDataBarConditionalFormatting(IWorkbook workbook) {
            #region #DataBarConditionalFormatting
            Worksheet worksheet = workbook.Worksheets["cfBooks"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            // Automatically set the value corresponding to the shortest bar to the lowest value in a range.
            ConditionalFormattingValue lowBound1 = worksheet.ConditionalFormattings.CreateValue(ConditionalFormattingValueType.MinMax);
            // Automatically set the value corresponding to the longest bar to the highest value in a range.
            ConditionalFormattingValue highBound1 = worksheet.ConditionalFormattings.CreateValue(ConditionalFormattingValueType.MinMax);
            // Create the rule to compare values in cells F5 through F18 using data bars. 
            DataBarConditionalFormatting cfRule1 = worksheet.ConditionalFormattings.AddDataBarConditionalFormatting(worksheet.Range["$F$5:$F$18"], lowBound1, highBound1, Color.Green);
            // Set the positive bar border color to green.
            cfRule1.BorderColor = Color.Green;
            // Set the negative bar color to red.
            cfRule1.NegativeBarColor = Color.Red;
            // Set the negative bar border color to red.
            cfRule1.NegativeBarBorderColor = Color.Red;
            // Set the axis position to display the axis in the middle of the cell.
            cfRule1.AxisPosition = ConditionalFormattingDataBarAxisPosition.Middle;
            // Set the axis color to dark blue.
            cfRule1.AxisColor = Color.DarkBlue;

            // Set the value corresponding to the shortest bar to 0 percent.
            ConditionalFormattingValue lowBound2 = worksheet.ConditionalFormattings.CreateValue(ConditionalFormattingValueType.Percent, "0");
            // Set the value corresponding to the longest bar to 100 percent.
            ConditionalFormattingValue highBound2 = worksheet.ConditionalFormattings.CreateValue(ConditionalFormattingValueType.Percent, "100");
            // Create the rule to compare values in cells H5 through H18 using data bars.  
            DataBarConditionalFormatting cfRule2 = worksheet.ConditionalFormattings.AddDataBarConditionalFormatting(worksheet.Range["$H$5:$H$18"], lowBound2, highBound2, Color.SkyBlue);
            // Set the data bar border color to sky blue.
            cfRule2.BorderColor = Color.SkyBlue;
            // Specify the solid fill type.
            cfRule2.GradientFill = false;
            // Hide values of cells to which the rule is applied.
            cfRule2.ShowValue = false;

            // Add an explanation to the created rule.
            worksheet["B2"].Value = "Compare values in the \"Cost Trend\" and \"Markup\" columns using data bars.";
            worksheet.Visible = true;
            #endregion #DataBarConditionalFormatting
        }

        static void AddIconSetConditionalFormatting(IWorkbook workbook) {
            #region #IconSetConditionalFormatting
            Worksheet worksheet = workbook.Worksheets["cfBooks"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            // Set the first threshold to the lowest value in the range of cells using the MIN() formula.
            ConditionalFormattingIconSetValue minPoint = worksheet.ConditionalFormattings.CreateIconSetValue(ConditionalFormattingValueType.Formula, "=MIN($F$5:$F$18)", ConditionalFormattingValueOperator.GreaterOrEqual);
            // Set the second threshold to 0.
            ConditionalFormattingIconSetValue midPoint = worksheet.ConditionalFormattings.CreateIconSetValue(ConditionalFormattingValueType.Number, "0", ConditionalFormattingValueOperator.GreaterOrEqual);
            // Set the third threshold to 0.01.
            ConditionalFormattingIconSetValue maxPoint = worksheet.ConditionalFormattings.CreateIconSetValue(ConditionalFormattingValueType.Number, "0.01", ConditionalFormattingValueOperator.GreaterOrEqual);
            // Create the rule to apply a specific icon from the three arrows icon set to each cell in the range E4:E17 based on its value.  
            IconSetConditionalFormatting cfRule = worksheet.ConditionalFormattings.AddIconSetConditionalFormatting(worksheet.Range["$F$5:$F$18"], IconSetType.Arrows3, new ConditionalFormattingIconSetValue[] { minPoint, midPoint, maxPoint });
            // Specify the custom icon to be displayed if the second condition is true. 
            // To do this, set the IconSetConditionalFormatting.IsCustom property to true.
            cfRule.IsCustom = true;
            // Initialize the ConditionalFormattingCustomIcon object.
            ConditionalFormattingCustomIcon cfCustomIcon = new ConditionalFormattingCustomIcon();
            // Specify the icon set where you wish to get the icon. 
            cfCustomIcon.IconSet = IconSetType.TrafficLights13;
            // Specify the index of the desired icon in the set.
            cfCustomIcon.IconIndex = 1;
            // Add the custom icon at the specified position in the initial icon set.
            cfRule.SetCustomIcon(1, cfCustomIcon);
            // Hide values of cells to which the rule is applied.
            cfRule.ShowValue = false;

            // Add an explanation to the created rule.
            worksheet["B2"].Value = "In the report below identify upward and downward cost trends.";
            worksheet.Visible = true;
            #endregion #IconSetConditionalFormatting
        }
    }
}