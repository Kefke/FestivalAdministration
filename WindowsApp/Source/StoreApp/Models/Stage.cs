using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Models
{
    public class Stage: StoreApp.Common.BindableBase
    {
        private int _ID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; OnPropertyChanged("ID"); }
        }

        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; OnPropertyChanged("Name"); }
        }

        private ObservableCollection<TimeSlot> _timeslots;

        public ObservableCollection<TimeSlot> TimeSlots
        {
            get { return _timeslots; }
            set { _timeslots = value; OnPropertyChanged("TimeSlots"); }
        }
        
    }
}
