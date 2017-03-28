using System;
using System.Globalization;
using System.Windows.Data;

namespace DevExpress.DevAV {
    public class PictureConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var picture = value as Picture;
            return picture?.Data;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            var data = value as byte[];
            return data == null ? null : new Picture { Data = data };
        }
    }
}
