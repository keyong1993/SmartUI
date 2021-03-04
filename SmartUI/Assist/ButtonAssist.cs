using SmartUI.Common.Enum;
using System.Windows;
using System.Windows.Controls;

namespace SmartUI.Assist
{
    public class ButtonAssist
    {
        public static PackIconKind GetIcon(Button button) => (PackIconKind)button.GetValue(IconProperty);
        public static void SetIcon(Button button, PackIconKind icon) => button.SetValue(IconProperty, icon);

        public static readonly DependencyProperty IconProperty
       = DependencyProperty.RegisterAttached("Icon", typeof(PackIconKind), typeof(ButtonAssist));
    }
}
