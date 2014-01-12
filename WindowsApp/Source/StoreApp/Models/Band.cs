using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace StoreApp.Models
{
    public class Band: StoreApp.Common.BindableBase
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

        private string _Twitter;

        public string Twitter
        {
            get { return _Twitter; }
            set { _Twitter = value; OnPropertyChanged("Twitter"); }
        }

        private string _Facebook;

        public string Facebook
        {
            get { return _Facebook; }
            set { _Facebook = value; OnPropertyChanged("Facebook"); }
        }

        private ImageSource _Picture;

        public ImageSource Picture
        {
            get { return _Picture; }
            set { _Picture = value; OnPropertyChanged("Picture"); }
        }

        private string _Description;

        public string Description
        {
            get { return _Description; }
            set { _Description = value; OnPropertyChanged("Description"); }
        }

        private ObservableCollection<Genre> _genres;

        public ObservableCollection<Genre> Genres
        {
            get { return _genres; }
            set { _genres = value; OnPropertyChanged("Genres"); }
        }

        private ObservableCollection<TimeSlot> _timeslots;

        public ObservableCollection<TimeSlot> TimeSlots
        {
            get { return _timeslots; }
            set { _timeslots = value; OnPropertyChanged("TimeSlots"); }
        }
    }
}
