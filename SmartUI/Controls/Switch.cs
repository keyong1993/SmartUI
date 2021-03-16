using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace SmartUI.Controls
{
    public class Switch : ToggleButton
    {
        public string CheckedText
        {
            get { return (string)GetValue(CheckedTextProperty); }
            set { SetValue(CheckedTextProperty, value); }
        }

        public static readonly DependencyProperty CheckedTextProperty =
            DependencyProperty.Register(nameof(CheckedText), typeof(string), typeof(Switch), new PropertyMetadata(null));



        public string UnCheckText
        {
            get { return (string)GetValue(UnCheckTextProperty); }
            set { SetValue(UnCheckTextProperty, value); }
        }

        public static readonly DependencyProperty UnCheckTextProperty =
            DependencyProperty.Register(nameof(UnCheckText), typeof(string), typeof(Switch), new PropertyMetadata(null));




    }
}
