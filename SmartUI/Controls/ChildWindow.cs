using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SmartUI.Controls
{

    [TemplatePart(Name = PART_WindowRoot, Type = typeof(Grid))]
    [TemplatePart(Name = PART_Root, Type = typeof(Grid))]
    public class ChildWindow : Window
    {
        private const string PART_WindowRoot = "PART_WindowRoot";
        private const string PART_Root = "PART_Root";
        private Button _closeBtn;
        private Border _title;


        public bool ShowCloseButton
        {
            get { return (bool)GetValue(ShowCloseButtonProperty); }
            set { SetValue(ShowCloseButtonProperty, value); }
        }

        public static readonly DependencyProperty ShowCloseButtonProperty =
            DependencyProperty.Register(nameof(ShowCloseButton), typeof(bool), typeof(ChildWindow), new PropertyMetadata(true, (d, e) =>
             {
                 if (d is ChildWindow window && e.NewValue is bool flag)
                     window._closeBtn.Visibility = flag ? Visibility.Visible : Visibility.Collapsed;
             }));

        public static readonly RoutedEvent CloseButtonClickedEvent = EventManager.RegisterRoutedEvent("CloseButtonClicked", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ChildWindow));
        public event RoutedEventHandler CloseButtonClicked
        {
            add
            {
                AddHandler(CloseButtonClickedEvent, value);
            }
            remove
            {
                RemoveHandler(CloseButtonClickedEvent, value);
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _closeBtn = this.GetTemplateChild("closeBtn") as Button;
            if (_closeBtn != null)
            {
                _closeBtn.Visibility = ShowCloseButton ? Visibility.Visible : Visibility.Collapsed;
                _closeBtn.Click += CloseBtn_Click;
            }
                
            _title = this.GetTemplateChild("borderTitle") as Border;
            if (_title != null)
            {
                _title.MouseLeftButtonDown += Title_MouseLeftButtonDown;
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            RoutedEventArgs args = new RoutedEventArgs(CloseButtonClickedEvent, this);
            this.RaiseEvent(args);
            this.Close();           
        }

        private void Title_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
    }
}
