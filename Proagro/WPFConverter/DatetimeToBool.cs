using System;
using System.Globalization;
using System.Windows.Data;

namespace Proagro.WPFConverter
{
    public class DatetimeToBool : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((bool)value) ? DateTime.Now as DateTime? : null;
        }
    }
}
