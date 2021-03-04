using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SmartUI.Controls
{
    public class NoticeControl : Grid
    {
        static ResourceDictionary style = new ResourceDictionary() { Source = new Uri("DiningReservation.UI;component/Themes/NoticeStyle.xaml", UriKind.Relative) };
        private ObservableCollection<NoticeItemModel> Items = new ObservableCollection<NoticeItemModel>();
        private ItemsControl itemsControl;
        Timer timer;
        public NoticeControl()
        {
            itemsControl = new ItemsControl();
            itemsControl.ItemsSource = Items;
            itemsControl.Style = (Style)style["itemsControlStyle"];
            itemsControl.VerticalAlignment = VerticalAlignment.Top;
            itemsControl.HorizontalAlignment = HorizontalAlignment.Center;
            this.Children.Add(itemsControl);
            timer = new Timer();
            timer.Interval = 500;
            timer.Elapsed += new ElapsedEventHandler(TimerElapsed);
            timer.Start();
        }

        public void Show(NoticeType icon, string message, int millisecond)
        {
            Items.Insert(0, new NoticeItemModel(icon, message, millisecond));
            itemsControl.ItemsSource = Items;
        }

        private void TimerElapsed(object sender, ElapsedEventArgs args)
        {
            if (Items.Count == 0)
                return;
            Dispatcher.InvokeAsync((Action)delegate ()
            {
                for (int i = Items.Count - 1; i >= 0; i--)
                {
                    if (Items[i].CreateTime.AddMilliseconds(Items[i].Millisecond) <= DateTime.Now && Items.Count > i)
                    {
                        Items.RemoveAt(i);
                    }
                }
            });
        }
    }

    public class NoticeItemModel
    {
        public string Icon { get; private set; }

        public string Message { get; private set; }

        public int Millisecond { get; private set; }

        public DateTime CreateTime { get; set; }

        public NoticeItemModel(NoticeType icon, string message, int millisecond)
        {
            switch (icon)
            {
                case NoticeType.Ok:
                    Icon = "/DiningReservation.UI;component/Images/u301.png";
                    break;
                case NoticeType.Error:
                    Icon = "/DiningReservation.UI;component/Images/u304.png";
                    break;
                case NoticeType.Info:
                default:
                    Icon = "/DiningReservation.UI;component/Images/u196.png";
                    break;
            }
            Message = message.Length > 15 ? message.Substring(0, 15) + "..." : message;
            Millisecond = millisecond;
            CreateTime = DateTime.Now;
        }
    }

    public enum NoticeType
    {
        Info,
        Ok,
        Error
    }
}
