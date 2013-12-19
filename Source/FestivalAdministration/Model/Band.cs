using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalAdministration.Model
{
    class Band
    {
        private int _ID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private ObservableCollection<Genre> _Genres;

        public ObservableCollection<Genre> Genres
        {
            get { return _Genres; }
            set { _Genres = value; }
        }

        private Contactperson _Contact;

        public Contactperson Contact
        {
            get { return _Contact; }
            set { _Contact = value; }
        }

        private string _Twitter;

        public string Twitter
        {
            get { return _Twitter; }
            set { _Twitter = value; }
        }

        private string _Facebook;

        public string Facebook
        {
            get { return _Facebook; }
            set { _Facebook = value; }
        }

        private string _Picture;

        public string Picture
        {
            get { return _Picture; }
            set { _Picture = value; }
        }

        private string _Description;

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
    }
}
