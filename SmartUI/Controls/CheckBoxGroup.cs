using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SmartUI.Controls
{
    public class CheckBoxGroup : WrapPanel
    {
        public CheckBoxGroup()
        {
            Orientation = Orientation.Horizontal;
        }

        public bool CanAllChecked
        {
            get { return (bool)GetValue(CanAllCheckedProperty); }
            set { SetValue(CanAllCheckedProperty, value); }
        }

        public static readonly DependencyProperty CanAllCheckedProperty =
            DependencyProperty.Register(nameof(CanAllChecked), typeof(bool), typeof(CheckBoxGroup), new PropertyMetadata(false));



        public bool AllChecked
        {
            get { return (bool)GetValue(AllCheckedProperty); }
            set { SetValue(AllCheckedProperty, value); }
        }

        public static readonly DependencyProperty AllCheckedProperty =
            DependencyProperty.Register(nameof(AllChecked), typeof(bool), typeof(CheckBoxGroup), new PropertyMetadata(false));

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(CheckBoxGroup), new PropertyMetadata(default));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (Children.Count == 0 || CornerRadius == default)
                return;
            int index = 0;
            CheckBox preCheckBox = default;
            foreach (var item in Children)
            {
                if (item is CheckBox check)
                {
                    if (index == 0)
                    {
                        preCheckBox = check;
                        Border border = check.Template.FindName("border", check) as Border;
                        if (border != null)
                        {
                            border.CornerRadius = new CornerRadius(CornerRadius.TopLeft, 0, 0, CornerRadius.BottomLeft);
                            border.BorderThickness = new Thickness(1, 1, 0, 1);
                        }
                    }
                }
            }
            if (preCheckBox != default)
            {
                Border border = preCheckBox.Template.FindName("border", preCheckBox) as Border;
                if (border != null)
                {
                    border.CornerRadius = new CornerRadius(0, CornerRadius.TopRight, CornerRadius.BottomRight, 0);
                    border.BorderThickness = new Thickness(0, 1, 1, 1);
                }
            }
        }
    }
}
