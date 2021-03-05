using SmartUI.Common.Enum;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartUI.Demo
{
    public class MainWindowModel : INotifyPropertyChanged
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

        public MainWindowModel()
        {
            PackIconKind[] array =(PackIconKind[]) Enum.GetValues(typeof(PackIconKind));
            Icons = new ObservableCollection<PackIconKind>(array);
        }



        protected void RaisePropertyChanged(string propertyName)
        {
            var method = PropertyChanged;
            if (method != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
