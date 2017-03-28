using System;
using DevExpress.Utils;
using DevExpress.XtraRichEdit.API.Native;

namespace RichEditExamples {
    public static class SpecialFeatires {
        static void CreateHyperlink(Document document) {
            #region #Hyperlinks
            document.AppendText("Main website: ");
            DocumentRange range = document.AppendText("DevExpress");
            document.Hyperlinks.Create(range);
            document.Hyperlinks[0].Target = "_blank";
            document.Hyperlinks[0].NavigateUri = "http://www.devexpress.com/";
            #endregion #Hyperlinks
        }
        static void CreateField(Document document) {
            #region #Fields
            document.Fields.Create(document.Range.Start, "DATE");
            document.Fields[0].Update();
            #endregion #Fields
        }
    }
}