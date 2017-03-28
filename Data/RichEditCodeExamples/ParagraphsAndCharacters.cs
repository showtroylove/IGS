using System;
using DevExpress.Utils;
using DevExpress.XtraRichEdit.API.Native;

namespace RichEditExamples {
    public static class ParagraphsAndCharactersActions {
        static void ChangeSelection(Document document) {
            #region #Selections
            document.AppendText("Some text.");
            DocumentRange textRange = document.CreateRange(2, 5);
            CharacterProperties cp = document.BeginUpdateCharacters(textRange);
            cp.BackColor = System.Drawing.Color.FromArgb(180, 201, 233);
            document.EndUpdateCharacters(cp);
            #endregion #Selections
        }
        static void ChangeCharacterStyle(Document document) {
            #region #CharacterStyle
            DocumentRange textRange = document.AppendText("Some text.");
            CharacterProperties cp = document.BeginUpdateCharacters(textRange);
            cp.Bold = true;
            cp.FontSize = 16;
            cp.FontName = "Arial";
            cp.ForeColor = DXColor.Red;
            cp.BackColor = DXColor.LightGreen;
            document.EndUpdateCharacters(cp);
            #endregion #CharacterStyle
        }
        static void CreateParagraph(Document document) {
            #region #Paragraphs
            document.AppendText("First paragraph.");
            document.Paragraphs.Append();
            document.AppendText("Second paragraph.");
            #endregion #Paragraphs
        }
        static void ChangeParagraphStyle(Document document) {
            #region #ParagraphStyle
            document.AppendText("Title");
            document.Paragraphs.Append();
            document.Paragraphs[0].BackColor = DXColor.Yellow;
            document.Paragraphs[0].Alignment = ParagraphAlignment.Center;
            document.AppendText("Some text.");
            document.Paragraphs[1].BackColor = DXColor.YellowGreen;
            #endregion #ParagraphStyle
        }
        static void CreateNumberingList(Document document) {
            #region #Numbering List
            document.BeginUpdate();
            //Describe the pattern used for bulleted list.
            //Specify parameters used to represent each list level, up to the eighth level.
            document.AbstractNumberingLists.Add();

            // Create a numbering list. It is based on a previously defined abstract list with ID = 0.
            document.NumberingLists.Add(0);

            //The main purpose of the Guard class is to validate parameters passed into a method. 
            //Methods exposed by the Guard class are designed to throw exceptions if a parameter being checked does not pass validation.
            Guard.Equals(document.NumberingLists[document.NumberingLists.Count - 1].Index, document.NumberingLists.Count - 1);

            // Append list items
            document.AppendText("One");
            document.Paragraphs.Append();
            Paragraph paragraph = document.Paragraphs[0];
            paragraph.ListIndex = 0;

            document.AppendText("Two");
            document.Paragraphs.Append();
            paragraph = document.Paragraphs[1];
            paragraph.ListIndex = 0;

            document.AppendText("Three");
            document.Paragraphs.Append();
            paragraph = document.Paragraphs[2];
            paragraph.ListIndex = 0;

            document.EndUpdate();
            #endregion #Numbering List
        }
        static void ChangeNumberingListStyle(Document document) {
            #region #Numbering List Style
            document.BeginUpdate();
            //Describe the pattern used for bulleted list.
            //Specify parameters used to represent each list level, up to the eighth level.
            AbstractNumberingList list = document.AbstractNumberingLists.Add();
            document.AbstractNumberingLists.Add();
            list.NumberingType = NumberingType.Bullet;

            ListLevel level = list.Levels[0];
            level.ParagraphProperties.LeftIndent = 150;
            level.ParagraphProperties.FirstLineIndentType = ParagraphFirstLineIndent.Hanging;
            level.ParagraphProperties.FirstLineIndent = 75;
            level.CharacterProperties.FontName = "Symbol";
            level.DisplayFormatString = new string('\u00B7', 1);

            // Create a numbering list. It is based on a previously defined abstract list with ID = 0.
            document.NumberingLists.Add(0);

            //The main purpose of the Guard class is to validate parameters passed into a method. 
            //Methods exposed by the Guard class are designed to throw exceptions if a parameter being checked does not pass validation.
            Guard.Equals(document.NumberingLists[document.NumberingLists.Count - 1].Index, document.NumberingLists.Count - 1);

            // Append list items
            document.AppendText("One");
            document.Paragraphs.Append();
            Paragraph paragraph = document.Paragraphs[0];
            paragraph.ListIndex = 0;
            paragraph.ListLevel = 0;

            document.AppendText("OneOne");
            document.Paragraphs.Append();
            paragraph = document.Paragraphs[1];
            paragraph.ListIndex = 0;
            paragraph.ListLevel = 1;

            document.AppendText("Two");
            document.Paragraphs.Append();
            paragraph = document.Paragraphs[2];
            paragraph.ListIndex = 0;
            paragraph.ListLevel = 0;

            document.EndUpdate();
            #endregion #Numbering List Style
        }
    }
}
