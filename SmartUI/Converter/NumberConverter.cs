using System;
using System.Globalization;
using System.Windows.Data;

namespace SmartUI.Converter
{
    public class NumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string format = "F2";
            if (parameter != null)
                format = parameter.ToString();
            if (value != null && decimal.TryParse(value.ToString(), out decimal num))
                return num.ToString(format);
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
