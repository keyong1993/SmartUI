using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SmartUI.Controls
{
    public class SmartWindow : Window
    {
        private TextBlock _title;

        public SmartWindow()
        {
            CommandBindings.Add(new CommandBinding(SystemCommands.MinimizeWindowCommand, (a, b) => { WindowState = WindowState.Minimized; }));
            CommandBindings.Add(new CommandBinding(SystemCommands.MaximizeWindowCommand, (a, b) => { WindowState = WindowState.Maximized; }));
            CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand, (a, b) => { Close(); }));
            CommandBindings.Add(new CommandBinding(SystemCommands.RestoreWindowCommand, (a, b) => { WindowState = WindowState.Normal; }));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _title = GetTemplateChild("title") as TextBlock;
            if (_title != null)
            {
                _title.MouseLeftButtonDown += _title_MouseLeftButtonDown;
            }
        }

        private void _title_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && e.ClickCount == 1)
            {
                this.DragMove();
            }
            else if (e.ClickCount == 2)
            {
                if (WindowState == WindowState.Maximized)
                    WindowState = WindowState.Normal;
                else if (WindowState == WindowState.Normal)
                    WindowState = WindowState.Maximized;
            }
        }
    }
}
