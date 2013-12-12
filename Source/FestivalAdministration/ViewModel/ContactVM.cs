using FestivalAdministration.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalAdministration.ViewModel
{
    class ContactVM : ObservableObject, IPage
    {
        public ContactVM()
        {
            ContactTypes = ContactpersonType.GetContactpersonTypes();
        }

        public string Name
        {
            get { return "Contact"; }
        }

        private ObservableCollection<ContactpersonType> _contactTypes;

        public ObservableCollection<ContactpersonType> ContactTypes
        {
            get { return _contactTypes; }
            set { _contactTypes = value; OnPropertyChanged("ContactTypes"); }
        }
        
    }
}
