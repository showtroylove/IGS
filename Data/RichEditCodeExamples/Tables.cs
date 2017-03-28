using System;
using DevExpress.Utils;
using DevExpress.XtraRichEdit.API.Native;

namespace RichEditExamples {
    public static class Tables {
        static void CreateTable(Document document) {
            #region #Create Table
            document.Tables.Create(document.CaretPosition, 5, 5);
            #endregion #Create Table
        }
        static void ChangeTableStyle(Document document) {
            #region #Table Style
            Table table = document.Tables.Create(document.CaretPosition, 5, 5);
            table.Style.TableBorders.Left.LineColor = DXColor.Coral;
            table.Style.TableBorders.Left.LineStyle = TableBorderLineStyle.Double;
            table.Style.TableBorders.Left.LineThickness = 5;

            table.Style.TableBorders.Top.LineColor = DXColor.Coral;
            table.Style.TableBorders.Top.LineStyle = TableBorderLineStyle.Double;
            table.Style.TableBorders.Top.LineThickness = 5;

            table.Style.TableBorders.Right.LineColor = DXColor.Coral;
            table.Style.TableBorders.Right.LineStyle = TableBorderLineStyle.Double;
            table.Style.TableBorders.Right.LineThickness = 5;

            table.Style.TableBorders.Bottom.LineColor = DXColor.Coral;
            table.Style.TableBorders.Bottom.LineStyle = TableBorderLineStyle.Double;
            table.Style.TableBorders.Bottom.LineThickness = 5;

            table.Style.TableBorders.InsideHorizontalBorder.LineColor = DXColor.Coral;
            table.Style.TableBorders.InsideVerticalBorder.LineColor = DXColor.Coral;
            #endregion #Table Style
        }
        static void ChangeRowStyle(Document document) {
            #region #Row Style
            Table table = document.Tables.Create(document.CaretPosition, 5, 5);
            table.Rows[0].Height = 100;
            for(int i = 0; i < table.Rows[0].Cells.Count; i++) {
                table.Rows[0].Cells[i].BackgroundColor = DXColor.Gray;
                table.Rows[0].Cells[i].PreferredWidthType = WidthType.Fixed;
                table.Rows[0].Cells[i].PreferredWidth = 150;
            }
            #endregion #Row Style
        }
        static void ChangeColumnStyle(Document document) {
            #region #Column Style
            Table table = document.Tables.Create(document.CaretPosition, 5, 5);
            table[0, 0].PreferredWidthType = WidthType.Fixed;
            table[0, 0].PreferredWidth = 400;
            for(int i = 0; i < table.Rows.Count; i++) {
                table[i, 0].BackgroundColor = DXColor.Maroon;
            }
            #endregion #Column Style
        }
        static void ChangeCellStyle(Document document) {
            #region #Cell Style
            Table table = document.Tables.Create(document.CaretPosition, 5, 5);
            table[2, 2].PreferredWidthType = WidthType.Fixed;
            table[2, 2].PreferredWidth = 400;
            table[2, 2].Height = 400;
            table[2, 2].BackgroundColor = DXColor.RosyBrown;
            #endregion #Cell Style
        }
    }
}
