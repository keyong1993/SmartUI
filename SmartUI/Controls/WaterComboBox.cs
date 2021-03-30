using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SmartUI.Controls
{
    public class WaterComboBox : ComboBox
    {
        public AlignmentX AlignmentX
        {
            get { return (AlignmentX)GetValue(AlignmentXProperty); }
            set { SetValue(AlignmentXProperty, value); }
        }

        public static readonly DependencyProperty AlignmentXProperty =
            DependencyProperty.Register(nameof(AlignmentX), typeof(AlignmentX), typeof(WaterComboBox), new PropertyMetadata(AlignmentX.Left));


        public string WaterText
        {
            get { return (string)GetValue(WaterTextProperty); }
            set { SetValue(WaterTextProperty, value); }
        }

        public static readonly DependencyProperty WaterTextProperty =
            DependencyProperty.Register(nameof(WaterText), typeof(string), typeof(WaterComboBox));


    }
}
