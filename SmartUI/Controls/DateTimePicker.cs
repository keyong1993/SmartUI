using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using System.Windows;
using System.Windows.Media;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SmartUI.Controls
{
    [System.ComponentModel.DefaultBindingProperty("Value")]

    public class DateTimePicker : Control
    {
        internal TextBox _textBox;
        private BlockManager _blockManager;
        Popup popup = null;


        public string WaterText
        {
            get { return (string)GetValue(WaterTextProperty); }
            set { SetValue(WaterTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WaterText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WaterTextProperty =
            DependencyProperty.Register("WaterText", typeof(string), typeof(DateTimePicker));



        public DateTime? SelectedDateTime
        {
            get { return (DateTime?)GetValue(SelectedDateTimeProperty); }
            set { SetValue(SelectedDateTimeProperty, value); }
        }

        public static readonly DependencyProperty SelectedDateTimeProperty =
            DependencyProperty.Register("SelectedDateTime", typeof(DateTime?), typeof(DateTimePicker), new PropertyMetadata(OnSelectedDateChanged));


        public DateTime? StartDateTime
        {
            get { return (DateTime?)GetValue(StartDateTimeProperty); }
            set { SetValue(StartDateTimeProperty, value); }
        }

        public static readonly DependencyProperty StartDateTimeProperty =
            DependencyProperty.Register("StartDateTime", typeof(DateTime?), typeof(DateTimePicker));

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            Console.WriteLine(e.Property.Name+","+e.NewValue);
            if (e.Property.Name == "StartDateTime")
            {
                if (StartDateTime != null && StartDateTime > SelectedDateTime)
                {
                    SelectedDateTime = StartDateTime.Value;
                }
            }
        }

        public DateTime? EndDateTime
        {
            get { return (DateTime?)GetValue(EndDateTimeProperty); }
            set { SetValue(EndDateTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EndDateTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EndDateTimeProperty =
            DependencyProperty.Register("EndDateTime", typeof(DateTime?), typeof(DateTimePicker));



        private static void OnSelectedDateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        public Visibility ShowDropDown
        {
            get { return (Visibility)GetValue(ShowDropDownProperty); }
            set { SetValue(ShowDropDownProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowDropDown.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowDropDownProperty =
            DependencyProperty.Register("ShowDropDown", typeof(Visibility), typeof(DateTimePicker), new PropertyMetadata(Visibility.Visible));

        public string CustomFormat
        {
            get { return (string)GetValue(CustomFormatProperty); }
            set { SetValue(CustomFormatProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CustomFormat.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CustomFormatProperty =
            DependencyProperty.Register("CustomFormat", typeof(string), typeof(DateTimePicker), new PropertyMetadata("yyyy-MM-dd HH:mm:ss"));


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this._textBox = (TextBox)this.Template.FindName("PART_TextBox", this);
            Button button = (Button)this.Template.FindName("PART_Button", this);
            popup = (Popup)this.Template.FindName("PART_Popup", this);
            CalendarTime ct = (CalendarTime)this.Template.FindName("ct", this);

            ct.SelectedDatesChanged -= Ct_SelectedDatesChanged;
            ct.SelectedDatesChanged += Ct_SelectedDatesChanged;
            button.Click -= Button_Click;
            button.Click += Button_Click;


            this.MouseWheel += new System.Windows.Input.MouseWheelEventHandler(Dameer_MouseWheel);
            this._textBox.GotFocus += new System.Windows.RoutedEventHandler(_textBox_GotFocus);
            this._textBox.PreviewMouseUp += new System.Windows.Input.MouseButtonEventHandler(_textBox_PreviewMouseUp);
            this._textBox.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(_textBox_PreviewKeyDown);
            this._blockManager = new BlockManager(this, CustomFormat);
        }

        private void Ct_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            //if(sender is CalendarTime calendarTime)
            //{
            //    if (calendarTime.SelectedDate is null)
            //        SelectedDateTime = null;
            //    else
            //        SelectedDateTime = calendarTime.SelectedDate;
            //}
            popup.IsOpen = false;
        }

        private void Ct_SelectedFinshed(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = false;
        }

        private void Ct_SelectedChanged(object sender, RoutedEventArgs e)
        {
            SelectedDateTime = Convert.ToDateTime(e.OriginalSource);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = true;
        }

        void Dameer_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            this._blockManager.Change(((e.Delta < 0) ? -1 : 1), true);
        }

        void _textBox_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            this._blockManager.ReSelect();
            popup.IsOpen = true;
        }

        void _textBox_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this._blockManager.ReSelect();
        }

        void _textBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            byte b = (byte)e.Key;
            if (e.Key == System.Windows.Input.Key.Left)
                this._blockManager.Left();
            else if (e.Key == System.Windows.Input.Key.Right)
                this._blockManager.Right();
            else if (e.Key == System.Windows.Input.Key.Up)
                this._blockManager.Change(1, true);
            else if (e.Key == System.Windows.Input.Key.Down)
                this._blockManager.Change(-1, true);
            if (b >= 34 && b <= 43)
                this._blockManager.ChangeValue(b - 34);
            if (!(e.Key == System.Windows.Input.Key.Tab))
                e.Handled = true;
        }
    }
    public class CalendarTime : Calendar
    {
        public DateTime? SelectedDateTime
        {
            get { return (DateTime?)GetValue(SelectedDateTimeProperty); }
            set { SetValue(SelectedDateTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedDateTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedDateTimeProperty =
            DependencyProperty.Register("SelectedDateTime", typeof(DateTime?), typeof(CalendarTime), new PropertyMetadata(null));

    }
    internal class BlockManager
    {
        internal DateTimePicker _dameer;
        private List<Block> _blocks;
        private string _format;
        private Block _selectedBlock;
        private int _selectedIndex;
        public event EventHandler NeglectProposed;
        private string[] _supportedFormats = new string[] {
                "yyyy", "MMMM", "dddd",
                "yyy", "MMM", "ddd",
                "yy", "MM", "dd",
                "y", "M", "d",
                "HH", "H", "hh", "h",
                "mm", "m",
                "ss", "s",
                "tt", "t",
                "fff", "ff", "f",
                "K", "g"};

        public BlockManager(DateTimePicker dameer, string format)
        {
            this._dameer = dameer;
            this._format = format;
            this._dameer.LostFocus += new RoutedEventHandler(_dameer_LostFocus);
            this._blocks = new List<Block>();
            this.InitBlocks();
        }

        private void InitBlocks()
        {
            foreach (string f in this._supportedFormats)
                this._blocks.AddRange(this.GetBlocks(f));
            this._blocks = this._blocks.OrderBy((a) => a.Index).ToList();
            this._selectedBlock = this._blocks[0];
            this.Render();
        }

        internal void Render()
        {
            int accum = 0;
            StringBuilder sb = new StringBuilder(this._format);
            foreach (Block b in this._blocks)
                b.Render(ref accum, sb);
            this._dameer._textBox.Text = this._format = sb.ToString();
            this.Select(this._selectedBlock);
        }

        private List<Block> GetBlocks(string pattern)
        {
            List<Block> bCol = new List<Block>();

            int index = -1;
            while ((index = this._format.IndexOf(pattern, ++index)) > -1)
                bCol.Add(new Block(this, pattern, index));
            this._format = this._format.Replace(pattern, (0).ToString().PadRight(pattern.Length, '0'));
            return bCol;
        }

        internal void ChangeValue(int p)
        {
            this._selectedBlock.Proposed = p;
            this.Change(this._selectedBlock.Proposed, false);
        }

        internal void Change(int value, bool upDown)
        {
            if (value < 0)
                return;
            this._dameer.SelectedDateTime = this._selectedBlock.Change(this._dameer.SelectedDateTime.Value, value, upDown);
            if (upDown)
                this.OnNeglectProposed();
            this.Render();
        }

        internal void Right()
        {
            if (this._selectedIndex + 1 < this._blocks.Count)
                this.Select(this._selectedIndex + 1);
        }

        internal void Left()
        {
            if (this._selectedIndex > 0)
                this.Select(this._selectedIndex - 1);
        }

        private void _dameer_LostFocus(object sender, RoutedEventArgs e)
        {
            //this.OnNeglectProposed();
        }

        protected virtual void OnNeglectProposed()
        {
            EventHandler temp = this.NeglectProposed;
            if (temp != null)
            {
                temp(this, EventArgs.Empty);
            }
        }

        internal void ReSelect()
        {
            foreach (Block b in this._blocks)
                if ((b.Index <= this._dameer._textBox.SelectionStart) && ((b.Index + b.Length) >= this._dameer._textBox.SelectionStart))
                { this.Select(b); return; }
            Block bb = this._blocks.Where((a) => a.Index < this._dameer._textBox.SelectionStart).LastOrDefault();
            if (bb == null) this.Select(0);
            else this.Select(bb);
        }

        private void Select(int blockIndex)
        {
            if (this._blocks.Count > blockIndex)
                this.Select(this._blocks[blockIndex]);
        }

        private void Select(Block block)
        {
            if (!(this._selectedBlock == block))
                this.OnNeglectProposed();
            this._selectedIndex = this._blocks.IndexOf(block);
            this._selectedBlock = block;
            this._dameer._textBox.Select(block.Index, block.Length);
        }
    }

    internal class Block
    {
        private BlockManager _blockManager;
        internal string Pattern { get; set; }
        internal int Index { get; set; }
        private int _length;
        internal int Length
        {
            get
            {
                return this._length;
            }
            set
            {
                this._length = value;
            }
        }
        private int _maxLength;
        private string _proposed;
        internal int Proposed
        {
            get
            {
                string p = this._proposed;
                return int.Parse(p.PadLeft(this.Length, '0'));
            }
            set
            {
                if (!(this._proposed == null) && this._proposed.Length >= this._maxLength)
                    this._proposed = value.ToString();
                else
                    this._proposed = string.Format("{0}{1}", this._proposed, value);
            }
        }

        public Block(BlockManager blockManager, string pattern, int index)
        {
            this._blockManager = blockManager;
            this._blockManager.NeglectProposed += new EventHandler(_blockManager_NeglectProposed);
            this.Pattern = pattern;
            this.Index = index;
            this.Length = this.Pattern.Length;
            this._maxLength = this.GetMaxLength(this.Pattern);
        }

        private int GetMaxLength(string p)
        {
            switch (p)
            {
                case "y":
                case "M":
                case "d":
                case "h":
                case "m":
                case "s":
                case "H":
                    return 2;
                case "yyy":
                    return 4;
                default:
                    return p.Length;
            }
        }

        private void _blockManager_NeglectProposed(object sender, EventArgs e)
        {
            this._proposed = null;
        }

        internal DateTime Change(DateTime dateTime, int value, bool upDown)
        {
            if (!upDown && !this.CanChange()) return dateTime;
            int y, m, d, h, n, s;
            y = dateTime.Year;
            m = dateTime.Month;
            d = dateTime.Day;
            h = dateTime.Hour;
            n = dateTime.Minute;
            s = dateTime.Second;

            if (this.Pattern.Contains("y"))
                y = ((upDown) ? dateTime.Year + value : value);
            else if (this.Pattern.Contains("M"))
                m = ((upDown) ? dateTime.Month + value : value);
            else if (this.Pattern.Contains("d"))
                d = ((upDown) ? dateTime.Day + value : value);
            else if (this.Pattern.Contains("h") || this.Pattern.Contains("H"))
                h = ((upDown) ? dateTime.Hour + value : value);
            else if (this.Pattern.Contains("m"))
                n = ((upDown) ? dateTime.Minute + value : value);
            else if (this.Pattern.Contains("s"))
                s = ((upDown) ? dateTime.Second + value : value);
            else if (this.Pattern.Contains("t"))
                h = ((h < 12) ? (h + 12) : (h - 12));

            if (y > 9999) y = 1;
            if (y < 1) y = 9999;
            if (m > 12) m = 1;
            if (m < 1) m = 12;
            if (d > DateTime.DaysInMonth(y, m)) d = 1;
            if (d < 1) d = DateTime.DaysInMonth(y, m);
            if (h > 23) h = 0;
            if (h < 0) h = 23;
            if (n > 59) n = 0;
            if (n < 0) n = 59;
            if (s > 59) s = 0;
            if (s < 0) s = 59;

            return new DateTime(y, m, d, h, n, s);
        }

        private bool CanChange()
        {
            switch (this.Pattern)
            {
                case "MMMM":
                case "dddd":
                case "MMM":
                case "ddd":
                case "g":
                    return false;
                default:
                    return true;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}", this.Pattern, this.Index);
        }

        internal void Render(ref int accum, StringBuilder sb)
        {
            if (this._blockManager._dameer.SelectedDateTime == null)
                return;
            this.Index += accum;
            DateTime dt = this._blockManager._dameer.SelectedDateTime.Value;
            string f = dt.ToString(this.Pattern + ",").TrimEnd(',');
            sb.Remove(this.Index, this.Length);
            sb.Insert(this.Index, f);
            accum += f.Length - this.Length;
            this._blockManager._dameer.SelectedDateTime = dt;
            this.Length = f.Length;
        }
    }
}
