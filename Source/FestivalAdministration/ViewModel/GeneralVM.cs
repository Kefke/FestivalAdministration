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
    }
}
