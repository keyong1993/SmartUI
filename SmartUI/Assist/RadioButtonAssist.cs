using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SmartUI.Assist
{
    public class RadioButtonAssist
    {
        public static bool GetIsDynamicCornerRadius(RadioButton radio) => (bool)radio.GetValue(IsDynamicCornerRadiusProperty);
        public static void SetIsDynamicCornerRadius(RadioButton radio, bool value) => radio.SetValue(IsDynamicCornerRadiusProperty, value);
        public static readonly DependencyProperty IsDynamicCornerRadiusProperty
       = DependencyProperty.RegisterAttached("IsDynamicCornerRadius", typeof(bool), typeof(RadioButtonAssist), new PropertyMetadata(false, IsDynamicCornerRadiusChanged));

        private static void IsDynamicCornerRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is RadioButton radio && e.NewValue is bool flag && flag)
            {
                radio.Loaded -= Radio_Loaded;
                radio.Loaded += Radio_Loaded;
            }
        }

        private static void Radio_Loaded(object sender, RoutedEventArgs e)
        {
            if (!(sender is RadioButton radio))
                return;
            string groupName = radio.GroupName;
            if (!(radio.Parent is Panel panel))
                return;
            int index = 0;
            RadioButton preRadio = default;
            foreach (var item in panel.Children)
            {
                if (item is RadioButton itemRadio && itemRadio.GroupName.Equals(groupName))
                {
                    preRadio = itemRadio;
                    if (index == 0)
                    {
                        Border border = itemRadio.Template.FindName("border", itemRadio) as Border;
                        if (border != null)
                        {
                            border.CornerRadius = new CornerRadius(4, 0, 0, 4);
                            border.BorderThickness = new Thickness(1, 1, 0, 1);
                        }
                    }
                    index++;
                }
            }
            if (preRadio != default)
            {
                Border border = preRadio.Template.FindName("border", preRadio) as Border;
                if (border != null)
                {
                    border.CornerRadius = new CornerRadius(0, 4, 4, 0);
                    border.BorderThickness = new Thickness(0, 1, 1, 1);
                }
            }
        }
    }
}
