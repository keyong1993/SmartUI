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
        private Label _title;
        private Border _titleControl;

        public SmartWindow()
        {
            CommandBindings.Add(new CommandBinding(SystemCommands.MinimizeWindowCommand, (a, b) => { WindowState = WindowState.Minimized; }));
            CommandBindings.Add(new CommandBinding(SystemCommands.MaximizeWindowCommand, (a, b) => { WindowState = WindowState.Maximized; }));
            CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand, (a, b) => { Close(); }));
            CommandBindings.Add(new CommandBinding(SystemCommands.RestoreWindowCommand, (a, b) => { WindowState = WindowState.Normal; }));
        }

        public FrameworkElement TitleControl
        {
            get { return (FrameworkElement)GetValue(TitleControlProperty); }
            set { SetValue(TitleControlProperty, value); }
        }

        public static readonly DependencyProperty TitleControlProperty =
            DependencyProperty.Register(nameof(TitleControl), typeof(FrameworkElement), typeof(SmartWindow), new PropertyMetadata(null));


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();            
            if (TitleControl != null)
            {
                _titleControl = (Border)GetTemplateChild("titleControl");
                _titleControl.Child = TitleControl;
                _titleControl.MouseLeftButtonDown += TitleMouseLeftButtonDown;
            }
            else
            {
                _title = GetTemplateChild("title") as Label;
                if (_title != null)
                {
                    _title.MouseLeftButtonDown += TitleMouseLeftButtonDown;
                }
            }
        }

        private void TitleMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
