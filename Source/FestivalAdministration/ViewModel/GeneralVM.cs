using FestivalAdministration.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalAdministration.ViewModel
{
    class GeneralVM : ObservableObject, IPage
    {
        public GeneralVM()
        {
            ContactTypes = ContactpersonType.GetContactpersonTypes();
            if (Festival.GetFestivals() != null) 
                Festival = Festival.GetFestivals()[0];
        }

        public string Name
        {
            get { return "General"; }
        }

        private ObservableCollection<ContactpersonType> _contactTypes;

        public ObservableCollection<ContactpersonType> ContactTypes
        {
            get { return _contactTypes; }
            set { _contactTypes = value; OnPropertyChanged("ContactTypes"); }
        }

        private Festival _Festival;

        public Festival Festival
        {
            get { return _Festival; }
            set { _Festival = value; }
        }
        
    }
}
