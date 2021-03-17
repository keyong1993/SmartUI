using SmartUI.Common.Enum;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace SmartUI.Assist
{
    public class ButtonAssist
    {
        public static PackIconKind GetIcon(Button button) => (PackIconKind)button.GetValue(IconProperty);
        public static void SetIcon(Button button, PackIconKind icon) => button.SetValue(IconProperty, icon);

        public static readonly DependencyProperty IconProperty
       = DependencyProperty.RegisterAttached("Icon", typeof(PackIconKind), typeof(ButtonAssist));

        public static bool GetIsDynamicCornerRadius(RadioButton radio) => (bool)radio.GetValue(IsDynamicCornerRadiusProperty);
        public static void SetIsDynamicCornerRadius(RadioButton radio, bool value) => radio.SetValue(IsDynamicCornerRadiusProperty, value);
        public static readonly DependencyProperty IsDynamicCornerRadiusProperty
       = DependencyProperty.RegisterAttached("IsDynamicCornerRadius", typeof(bool), typeof(ButtonAssist), new PropertyMetadata(false, IsDynamicCornerRadiusChanged));

        private static void IsDynamicCornerRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ButtonBase button && e.NewValue is bool flag && flag)
            {
                button.Loaded -= Radio_Loaded;
                button.Loaded += Radio_Loaded;
            }
        }

        private static void Radio_Loaded(object sender, RoutedEventArgs e)
        {
            if (!(sender is ButtonBase button))
                return;
            string groupName = string.Empty;
            if (button is RadioButton radio) 
                groupName = radio.GroupName;
            if (!(button.Parent is Panel panel))
                return;
            int index = 0;
            ButtonBase preButton = default;
            foreach (var item in panel.Children)
            {
                ButtonBase currentItem = default;
                if (item is RadioButton itemRadio && itemRadio.GroupName.Equals(groupName))
                {
                    currentItem = itemRadio;
                }
                else if (item is CheckBox check)
                {
                    currentItem = check;
                }
                if (currentItem != default)
                {
                    preButton = currentItem;
                    if (index == 0)
                    {
                        Border border = currentItem.Template.FindName("border", currentItem) as Border;
                        if (border != null)
                        {
                            border.CornerRadius = new CornerRadius(4, 0, 0, 4);
                            border.BorderThickness = new Thickness(1, 1, 0, 1);
                        }
                    }
                    index++;
                }
            }
            if (preButton != default)
            {
                Border border = preButton.Template.FindName("border", preButton) as Border;
                if (border != null)
                {
                    border.CornerRadius = new CornerRadius(0, 4, 4, 0);
                    border.BorderThickness = new Thickness(1, 1, 1, 1);
                }
            }
        }
    }
}
