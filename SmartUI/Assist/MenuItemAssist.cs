using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SmartUI.Assist
{
    public class MenuItemAssist
    {
        public static bool GetMainMenuSelect(DependencyObject obj)
        {
            return (bool)obj.GetValue(MainMenuSelectProperty);
        }

        public static void SetMainMenuSelect(DependencyObject obj, bool value)
        {
            obj.SetValue(MainMenuSelectProperty, value);
        }

        public static readonly DependencyProperty MainMenuSelectProperty =
            DependencyProperty.RegisterAttached("MainMenuSelect", typeof(bool), typeof(MenuItemAssist), new PropertyMetadata(false, MainMenuSelectChanged));

        private static void MainMenuSelectChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is MenuItem menuItem && e.NewValue is bool flag))
                return;
            menuItem.Click += MenuItem_Click;
        }

        private static void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (!(sender is MenuItem item))
                return;
            item.IsChecked = true;
            while (true)
            {
                if (item.Role == MenuItemRole.TopLevelHeader || item.Role == MenuItemRole.TopLevelItem)
                {
                    if(item.Parent is Menu menu)
                    {
                        foreach (var obj in menu.Items)
                        {
                            if(obj is MenuItem mainItem&&!mainItem.Equals(item))
                            {
                                mainItem.IsChecked = false;
                            }
                        }
                    }
                    break;
                }
                if (item.Parent is MenuItem parent)
                {
                    parent.IsChecked = item.IsChecked;
                    item = parent;
                }
                else
                {
                    break;
                }
            }
            e.Handled = true;
        }
    }
}
