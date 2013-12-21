using FestivalAdministration.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalAdministration.ViewModel
{
    class TicketTypesVM : ObservableObject, IPage
    {
        public TicketTypesVM()
        {
            TicketTypes = TicketType.GetTicketTypes();
        }

        public string Name
        {
            get { return "Ticket Type"; }
        }

        private ObservableCollection<TicketType> _ticketTypes;

        public ObservableCollection<TicketType> TicketTypes
        {
            get { return _ticketTypes; }
            set { _ticketTypes = value; OnPropertyChanged("TicketTypes"); }
        }
    }
}
