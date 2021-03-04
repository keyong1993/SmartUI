using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using SmartUI.Common.Enum;

namespace DiningReservation.UI.Converter
{
    public class DialogButtonConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is DialogButton dialog) || parameter is null)
                return Visibility.Collapsed;
            if (dialog == DialogButton.OkCancel)
                return Visibility.Visible;
            if (Enum.TryParse<DialogButton>(parameter.ToString(), out DialogButton selected))
            {
                return dialog == selected ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
