using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalAdministration.Model
{
    class Ticket
    {
        private int _ID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private string _TicketHolder;

        public string TicketHolder
        {
            get { return _TicketHolder; }
            set { _TicketHolder = value; }
        }

        private string _TicketHolderEmail;

        public string TicketHolderEmail
        {
            get { return _TicketHolderEmail; }
            set { _TicketHolderEmail = value; }
        }

        private TicketType _TicketType;

        public TicketType TicketType
        {
            get { return _TicketType; }
            set { _TicketType = value; }
        }

        private int _Amount;

        public int Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        
    }
}
