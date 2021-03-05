using System;
using System.Globalization;
using System.Windows.Data;
using SmartUI.Common;

namespace SmartUI.UI.Converter
{
    public class TimestampConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value!=null)
            {
                string tim = value.ToString();
                DateTime dateTime= DateTimeHelper.ToDateTime(long.Parse(tim));
                return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
            return string.Empty;
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
