﻿using FestivalAdministration.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalAdministration.ViewModel
{
    class CustomerDetailVM: ObservableObject, IPage
    {
        public CustomerDetailVM()
        {
            TicketTypes = TicketType.GetTicketTypes();
            Tickets = Ticket.GetTickets();
        }

        public string Name
        {
            get { return "Customer Detail"; }
        }

        private ObservableCollection<TicketType> _ticketTypes;

        public ObservableCollection<TicketType> TicketTypes
        {
            get { return _ticketTypes; }
            set { _ticketTypes = value; OnPropertyChanged("TicketTypes"); }
        }

        private ObservableCollection<Ticket> _tickets;

        public ObservableCollection<Ticket> Tickets
        {
            get { return _tickets; }
            set { _tickets = value; OnPropertyChanged("Tickets"); }
        }
    }
}
