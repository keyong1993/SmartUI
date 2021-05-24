using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SmartUI.Controls
{
    public class NoticeControl : Grid
    {
        static ResourceDictionary style = new ResourceDictionary() { Source = new Uri("SmartUI;component/Themes/NoticeStyle.xaml", UriKind.Relative) };
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
            WaitShow(icon, message, -1, millisecond);
        }

        public int WaitShow(NoticeType icon, string message, double progress = -1, int maxWaitTime = int.MaxValue)
        {
            NoticeItemModel model = new NoticeItemModel(icon, message, maxWaitTime, progress);
            Items.Insert(0, model);
            itemsControl.ItemsSource = Items;
            return model.Key;
        }

        public void ChangeProgress(int key, double progress)
        {
            NoticeItemModel model = Items.FirstOrDefault(p => p.Key.Equals(key));
            model.Progress = progress;
        }

        public bool Close(int key)
        {
            NoticeItemModel current = Items.FirstOrDefault(p => p.Key == key);
            if (current is null)
                return false;
            Dispatcher.Invoke((Action)delegate ()
            {
                Items.Remove(current);
            });

            return true;
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

    public class NoticeItemModel : INotifyPropertyChanged
    {
        private static int index = 0;

        public event PropertyChangedEventHandler PropertyChanged;

        public int Key { get; set; }

        public string Icon { get; private set; }

        public string Message { get; private set; }

        private double progress;
        public double Progress
        {
            get => progress;
            set { progress = value; RaisePropertyChanged(nameof(Progress)); }
        }

        public int Millisecond { get; private set; }

        public DateTime CreateTime { get; set; }

        public NoticeItemModel(NoticeType icon, string message, int millisecond, double progress = -1)
        {
            Icon = icon switch
            {
                NoticeType.Ok => "/SmartUI;component/Themes/Images/u301.png",
                NoticeType.Error => "/SmartUI;component/Themes/Images/u304.png",
                _ => "/SmartUI;component/Themes/Images/u196.png",
            };
            Message = message.Length > 15 ? message.Substring(0, 15) + "..." : message;
            Millisecond = millisecond;
            Progress = progress;
            CreateTime = DateTime.Now;
            Key = index++;
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            var method = PropertyChanged;
            if (method != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public enum NoticeType
    {
        Info,
        Ok,
        Error
    }
}
