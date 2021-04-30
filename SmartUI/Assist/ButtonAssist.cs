using SmartUI.Common.Enum;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

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


        #region ClickAnimation

        public static bool GetClickAnimation(DependencyObject obj) => (bool)obj.GetValue(ClickAnimationProperty);

        public static void SetClickAnimation(DependencyObject obj, bool value) => obj.SetValue(ClickAnimationProperty, value);

        public static readonly DependencyProperty ClickAnimationProperty =
            DependencyProperty.RegisterAttached("ClickAnimation", typeof(bool), typeof(ButtonAssist), new PropertyMetadata(false, ClickAnimationChnaged));



        private static void ClickAnimationChnaged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ButtonBase button && e.NewValue is bool vaule)
            {
                if (!vaule)
                    button.MouseLeftButtonDown -= Button_MouseDown;
                else
                {
                    button.Loaded += (a, b) =>
                    {
                        Border root = button.Template.FindName("border", button) as Border;
                        Grid grid = button.Template.FindName("root", button) as Grid;
                        if (button.Template.FindName("path", button) is null && root != null)
                        {
                            Path path = new Path() { Name = "path" };
                            path.Fill = Brushes.Black;
                            EllipseGeometry ellipse = new EllipseGeometry();
                            //ellipse.RadiusX = 0;
                            //Binding binding = new Binding() { Path = new PropertyPath("RadiusX"), Source = ellipse };

                            //ellipse.set(EllipseGeometry.RadiusYProperty, binding);
                            path.Data = ellipse;
                            root.Child = path;
                            root.ClipToBounds = true;
                            path.ClipToBounds = true;
                            button.OnApplyTemplate();
                            grid.MouseLeftButtonDown += Button_MouseDown;
                        }
                    };

                }
            }
        }

        private static void Button_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is Grid grid)
            {
                Point point = e.GetPosition(grid);
                Path path = FindPathControl(grid);
                if (path is null)
                    return;
                DoubleAnimation radiusXAnimation = new DoubleAnimation(0, grid.ActualWidth, new Duration(TimeSpan.FromSeconds(0.5)), FillBehavior.Stop);
                path.Data.BeginAnimation(EllipseGeometry.RadiusXProperty, radiusXAnimation);
                DoubleAnimation radiusYAnimation = new DoubleAnimation(0, grid.ActualWidth, new Duration(TimeSpan.FromSeconds(0.5)), FillBehavior.Stop);
                path.Data.BeginAnimation(EllipseGeometry.RadiusYProperty, radiusYAnimation);
                (path.Data as EllipseGeometry).Center = point;

                DoubleAnimation opacityAnimation = new DoubleAnimation(0.3, 0, new Duration(TimeSpan.FromSeconds(0.4)), FillBehavior.HoldEnd);
                path.BeginAnimation(Path.OpacityProperty, opacityAnimation);
            }
        }

        private static Path FindPathControl(UIElement element)
        {
            object FindByBorder(Border border)
            {
                if (border.Child is Path path)
                    return path;
                else if (border.Child is Border border1)
                {
                    return FindByBorder(border1);
                }
                else if (border.Child is Grid grid)
                {
                    return FindByPanel(grid);
                }
                return null;
            }

            object FindByPanel(Panel panel)
            {
                if (panel is null || panel.Children.Count == 0)
                    return null;
                foreach (var item in panel.Children)
                {
                    if (item is Border border) {
                        object obj = FindByBorder(border);
                        if (obj != null)
                            return obj;
                    }
                    else if (item is Panel panel1)
                    {
                        object obj = FindByPanel(panel1);
                        if (obj != null)
                            return obj;
                    }
                }
                return null;
            }
            object obj = null;
            if (element is Border border)
                obj = FindByBorder(border);
            else if (element is Panel panel)
                obj = FindByPanel(panel);
            else if (element is Path path)
                return path;
            return obj as Path;
        }
        #endregion
    }
}
