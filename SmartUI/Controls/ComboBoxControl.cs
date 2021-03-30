using System.Windows.Controls;

namespace SmartUI.Controls
{
    public class ComboBoxControl : ComboBox
    {
        private Border _border;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _border = GetTemplateChild("border") as Border;
            _border.MouseLeftButtonDown += Border_MouseLeftButtonDown;
            SelectionChanged += ComboBoxControl_SelectionChanged;
        }

        private void ComboBoxControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IsDropDownOpen = false;
        }

        private void Border_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            IsDropDownOpen = !IsDropDownOpen;
        }
    }
}
