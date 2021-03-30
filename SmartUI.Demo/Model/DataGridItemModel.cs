using System;

namespace SmartUI.Demo.Model
{
    public class DataGridItemModel : BaseModel
    {
        private string _id;
        public string Id { get => _id; set { _id = value; RaisePropertyChanged(nameof(Id)); } }

        private string _name;
        public string Name { get => _name; set { _name = value; RaisePropertyChanged(nameof(Name)); } }

        private string _desc;
        public string Desc { get => _desc; set { _desc = value; RaisePropertyChanged(nameof(Desc)); } }

        private DateTime _date;
        public DateTime Date { get => _date; set { _date = value; RaisePropertyChanged(nameof(Date)); } }
    }
}
