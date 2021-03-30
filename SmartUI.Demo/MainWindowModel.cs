using SmartUI.Base;
using SmartUI.Common.Enum;
using SmartUI.Demo.Common;
using SmartUI.Demo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SmartUI.Demo
{
    public class MainWindowModel : BaseModel
    {
        private ObservableCollection<PackIconKind> icons;

        public ObservableCollection<PackIconKind> Icons
        {
            get => icons;
            set
            {
                icons = value;
                RaisePropertyChanged(nameof(Icons));
            }
        }

        private ICommand _loadMoreIconCommand;
        public ICommand LoadMoreIconCommand
        {
            get => _loadMoreIconCommand;
            set
            {
                _loadMoreIconCommand = value;
                RaisePropertyChanged(nameof(LoadMoreIconCommand));
            }
        }

        private ICommand _stateCommand;

        public ICommand StateCommand
        {
            get => _stateCommand;
            set
            {
                _stateCommand = value;
                RaisePropertyChanged(nameof(StateCommand));
            }
        }

        private string _state = "Button";
        public string State
        {
            get => _state;
            set
            {
                _state = value;
                RaisePropertyChanged(nameof(State));
            }
        }

        private ObservableCollection<DataGridItemModel> _dataGridSource;
        public ObservableCollection<DataGridItemModel> DataGridSource
        {
            get => _dataGridSource;
            set
            {
                _dataGridSource = value;
                RaisePropertyChanged(nameof(DataGridSource));
            }
        }

        private int _iconsPageIndex = 1;
        private int _pageSize = 100;
        private PackIconKind[] _iconsArray;

        public MainWindowModel()
        {
            _iconsArray = (PackIconKind[])Enum.GetValues(typeof(PackIconKind));
            Icons = new ObservableCollection<PackIconKind>(_iconsArray.Take(_iconsPageIndex * _pageSize));
            LoadMoreIconCommand = new RelayCommand(new Action<object>(LoadMoreIcon));
            StateCommand = new RelayCommand(new Action<object>(StateChanged));
            DataGridSource = new ObservableCollection<DataGridItemModel>();
            for (int i = 0; i < 10; i++)
            {
                DataGridItemModel model = new DataGridItemModel()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "王小虎",
                    Desc = "王小虎是一个三好青年",
                    Date = DateTime.Now
                };
                DataGridSource.Add(model);
            }
        }

        private void LoadMoreIcon(object obj)
        {
            _iconsArray.Skip((_iconsPageIndex - 1) * _pageSize).Take(_pageSize).ToList().ForEach(p => { Icons.Add(p); });
        }

        private void StateChanged(object obj)
        {
            if (!(obj is string state))
                return;
            State = state;
        }

        public ObservableCollection<CascaderItem> CascaderItemSource { get; set; } = new ObservableCollection<CascaderItem>()
        {
            new CascaderItem()
            {
                DisplayName="选项一",Children=new ObservableCollection<CascaderItem>(){
                    new CascaderItem(){DisplayName="子项一",Children=new ObservableCollection<CascaderItem>(){
                        new CascaderItem(){DisplayName="子子项一"} ,
                        new CascaderItem(){DisplayName="子子项二"} ,
                        new CascaderItem(){DisplayName="子子项三"} ,
                        new CascaderItem(){DisplayName="子子项四"} ,
                        new CascaderItem(){DisplayName="子子项五"} }
                    },
                    new CascaderItem()
                    {
                        DisplayName = "子项二",Children = new ObservableCollection<CascaderItem>(){
                                new CascaderItem(){DisplayName="子子项一"},
                                new CascaderItem()
                                {
                                    DisplayName = "子项三",Children = new ObservableCollection<CascaderItem>(){
                                     new CascaderItem(){DisplayName="子子项一"} }}
                                }
                    }
                },
            },
            new CascaderItem()
            {
                DisplayName = "选项二",
                Children = new ObservableCollection<CascaderItem>(){
                new CascaderItem(){DisplayName="子项二",Children=new ObservableCollection<CascaderItem>(){
                    new CascaderItem(){DisplayName="子子项二"} }
                } }
            }
        };

    }
}
