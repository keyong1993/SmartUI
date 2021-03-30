using SmartUI.Base;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SmartUI.Controls
{
    public class Cascader : ComboBox
    {
        private const string ITEMSPANEL = "Pack_items";
        private StackPanel _itemsPanle;
        private Border _border;

        public new ObservableCollection<CascaderItem> ItemsSource
        {
            get { return (ObservableCollection<CascaderItem>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static new readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(nameof(ItemsSource), typeof(ObservableCollection<CascaderItem>), typeof(Cascader), new PropertyMetadata(default, ItemSourceChanged));

        private static void ItemSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is Cascader cascader && e.NewValue is ObservableCollection<CascaderItem> source))
            {
                return;
            }
            cascader.RaiseItems(source);
            if (!string.IsNullOrEmpty(cascader.Text))
                cascader.ResetByText();
        }

        public new string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static new readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(Cascader), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, TextChanged));

        private static void TextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Cascader control = d as Cascader;
            if (control.IsDropDownOpen)
                return;
            control.ResetByText();
        }

        private void ResetByText()
        {
            string text = Text;
            int index = 0;
            if (ItemsSource?.Count > 0 && !string.IsNullOrWhiteSpace(text))
            {
                ObservableCollection<CascaderItem> items = ItemsSource;
                CascaderItem selectItem = null;
                do
                {
                    foreach (var item in items)
                    {
                        item.ParentTree = selectItem;
                        if (text.StartsWith(item.DisplayName))
                        {
                            item.IsChecked = true;
                            text = text.Substring(item.DisplayName.Length).TrimStart('/');
                            break;
                        }
                    }
                    selectItem = items.FirstOrDefault(p => p.IsChecked);
                    if (_itemsPanle.Children.Count <= index)
                    {
                        RaiseItems(items);
                    }
                    else
                    {
                        (_itemsPanle.Children[index] as ListBox).ItemsSource = items;
                    }
                    index++;
                    items = selectItem?.Children;
                } while (items != null);
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _itemsPanle = GetTemplateChild(ITEMSPANEL) as StackPanel;
            if (_itemsPanle != null)
                RaiseItems(ItemsSource);
            _border = GetTemplateChild("border") as Border;
            _border.MouseLeftButtonDown += Border_MouseLeftButtonDown;
            if (!string.IsNullOrEmpty(Text))
            {
                ResetByText();
                if (_itemsPanle.Children.Count > 0)
                    (_itemsPanle.Children[_itemsPanle.Children.Count - 1] as ListBox).BorderThickness = new Thickness(0);
                if (_itemsPanle.Children.Count > 1)
                    (_itemsPanle.Children[_itemsPanle.Children.Count - 2] as ListBox).BorderThickness = new Thickness(0, 0, 1, 0);
            }
        }

        private void RaiseItems(ObservableCollection<CascaderItem> source)
        {
            if (_itemsPanle is null || source is null || source.Count == 0)
                return;
            ListBox control = new ListBox() { ItemsSource = source };
            control.SelectionChanged += Control_SelectionChanged;
            _itemsPanle.Children.Add(control);
        }

        private void Control_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListBox list)
            {
                if (e.RemovedItems.Count > 0)
                    (e.RemovedItems[0] as CascaderItem).IsChecked = false;
                int index = _itemsPanle.Children.IndexOf(list);
                if (e.AddedItems.Count == 0)
                    return;
                CascaderItem model = e.AddedItems[0] as CascaderItem;
                model.IsChecked = true;
                if (model.Children is null || model.Children.Count == 0)
                {
                    if (index + 1 < _itemsPanle.Children.Count)
                        _itemsPanle.Children.RemoveRange(index + 1, _itemsPanle.Children.Count - index - 1);
                    this.Text = GetParentNames(model);
                    this.SelectedItem = model;
                    IsDropDownOpen = false;
                }
                else
                {
                    IsDropDownOpen = true;
                    foreach (var item in model.Children)
                    {
                        item.ParentTree = model;
                    }
                    if (_itemsPanle.Children.Count == index + 1)
                    {
                        RaiseItems(model.Children);
                    }
                    else if (_itemsPanle.Children.Count - 1 > index)
                    {
                        (_itemsPanle.Children[index + 1] as ListBox).ItemsSource = model.Children;
                        if (index + 2 < _itemsPanle.Children.Count)
                            _itemsPanle.Children.RemoveRange(index + 2, _itemsPanle.Children.Count - index - 2);
                    }
                }
                if (_itemsPanle.Children.Count > 0)
                    (_itemsPanle.Children[_itemsPanle.Children.Count - 1] as ListBox).BorderThickness = new Thickness(0);
                if (_itemsPanle.Children.Count > 1)
                    (_itemsPanle.Children[_itemsPanle.Children.Count - 2] as ListBox).BorderThickness = new Thickness(0, 0, 1, 0);
            }
        }

        private void Border_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            IsDropDownOpen = !IsDropDownOpen;
        }

        private string GetParentNames(CascaderItem model)
        {
            if (model is null)
                return string.Empty;
            string text = model.DisplayName;
            CascaderItem tree = model;
            do
            {
                tree = tree.ParentTree;
                text = tree.DisplayName + "/" + text;
            } while (tree.ParentTree != null);
            return text;
        }
    }
}
