using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace SmartUI.Assist
{
    public class PopupAssist
    {
        public static bool GetIsFollowTarget(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsFollowTargetProperty);
        }

        public static void SetIsFollowTarget(DependencyObject obj, bool value)
        {
            obj.SetValue(IsFollowTargetProperty, value);
        }

        public static readonly DependencyProperty IsFollowTargetProperty =
            DependencyProperty.RegisterAttached("IsFollowTarget", typeof(bool), typeof(PopupAssist), new PropertyMetadata(false, IsFollowTargetChanged));

        private static void IsFollowTargetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Popup popup && e.NewValue is bool flag)
            {
                if (flag)
                {
                    //popup.PlacementTarget.
                }
            }
        }
    }
}
