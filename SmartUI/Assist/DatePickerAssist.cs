using System;
using System.Windows;
using System.Windows.Controls;

namespace SmartUI.Assist
{
    public class DatePickerAssist
    {
        public static bool GetAutoClose(DependencyObject obj)
        {
            return (bool)obj.GetValue(AutoCloseProperty);
        }

        public static void SetAutoClose(DependencyObject obj, bool value)
        {
            obj.SetValue(AutoCloseProperty, value);
        }

        public static readonly DependencyProperty AutoCloseProperty =
            DependencyProperty.RegisterAttached("AutoClose", typeof(bool), typeof(DatePickerAssist), new PropertyMetadata(false, AutoCloseChanged));

        private static void AutoCloseChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DatePicker picker && e.NewValue is bool flag)
            {
                picker.SelectedDateChanged += Picker_SelectedDateChanged;
            }
        }

        private static void Picker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is DatePicker picker && GetAutoClose(picker))
            {
                picker.IsDropDownOpen = false;
            }
        }
    }
}
