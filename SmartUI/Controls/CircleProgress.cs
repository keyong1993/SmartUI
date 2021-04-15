using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SmartUI.Controls
{
    public class CircleProgress : FrameworkElement
    {
        /// <summary>
        /// 半径
        /// </summary>
        public double Radius
        {
            get { return (double)GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }

        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register(nameof(Radius), typeof(double), typeof(CircleProgress), new PropertyMetadata(0));


        /// <summary>
        /// 起始角度
        /// 默认90°，及下方开始绘制
        /// </summary>
        public double StartAngle
        {
            get { return (double)GetValue(StartAngleProperty); }
            set { SetValue(StartAngleProperty, value); }
        }

        public static readonly DependencyProperty StartAngleProperty =
            DependencyProperty.Register(nameof(StartAngle), typeof(double), typeof(CircleProgress), new PropertyMetadata(90));

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(double), typeof(CircleProgress));

        private static object ValueChanged(DependencyObject d, object baseValue)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// 显示在进度条中心的控件
        /// </summary>
        public ContentControl Content
        {
            get { return (ContentControl)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register(nameof(Content), typeof(ContentControl), typeof(CircleProgress), new PropertyMetadata(null));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

        }

    }
}
