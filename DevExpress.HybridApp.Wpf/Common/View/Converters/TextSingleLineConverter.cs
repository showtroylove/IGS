using System;
using System.Globalization;
using System.Windows.Data;

namespace DevExpress.DevAV.Common.View {
    public class TextSingleLineConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var text = value as string;
            return text == null ? null : text.Replace(Environment.NewLine, " ");
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
}
