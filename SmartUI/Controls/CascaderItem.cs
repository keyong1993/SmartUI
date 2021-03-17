using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SmartUI.Base
{
    public class CascaderItem : ContentControl
    {

        public string DisplayName
        {
            get { return (string)GetValue(DisplayNameProperty); }
            set { SetValue(DisplayNameProperty, value); }
        }

        public static readonly DependencyProperty DisplayNameProperty =
            DependencyProperty.Register(nameof(DisplayName), typeof(string), typeof(CascaderItem));



        public object Data
        {
            get { return (object)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register(nameof(Data), typeof(object), typeof(CascaderItem));




        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register(nameof(IsChecked), typeof(bool), typeof(CascaderItem), new PropertyMetadata(false));



        public ObservableCollection<CascaderItem> Children
        {
            get { return (ObservableCollection<CascaderItem>)GetValue(ChildrenProperty); }
            set { SetValue(ChildrenProperty, value); }
        }

        public static readonly DependencyProperty ChildrenProperty =
            DependencyProperty.Register(nameof(Children), typeof(ObservableCollection<CascaderItem>), typeof(CascaderItem));

        internal CascaderItem ParentTree { get; set; }


    }
}
