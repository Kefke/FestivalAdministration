using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalAdministration.Models
{
    public class Ticket
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

        private int _TicketTypeID;

        public int TicketTypeID
        {
            get { return _TicketTypeID; }
            set { _TicketTypeID = value; }
        }

        private int _Amount;

        public int Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        private TicketType _ticketType;

        public TicketType TicketType
        {
            get { return _ticketType; }
            set { _ticketType = value; }
        }
        

        /*private static ObservableCollection<Ticket> _Tickets = null;

        public static ObservableCollection<Ticket> GetTickets()
        {
            // If _Tickets is null, create the Observable Collection
            if (_Tickets == null)
            {
                try
                {
                    // Create _Tickets
                    _Tickets = new ObservableCollection<Ticket>();

                    // Get data
                    DbDataReader reader = Database.GetData("SELECT * FROM ticket");
                    foreach (DbDataRecord record in reader)
                    {
                        // Create new Ticket
                        Ticket ticket = new Ticket();

                        // Get ID
                        if (DBNull.Value.Equals(record["ID"])) ticket.ID = -1;
                        else ticket.ID = Convert.ToInt32(record["ID"]);

                        // Get TicketHolder
                        if (DBNull.Value.Equals(record["TicketHolder"])) ticket.TicketHolder = "";
                        else ticket.TicketHolder = record["TicketHolder"].ToString();

                        // Get TicketHolderEmail
                        if (DBNull.Value.Equals(record["TicketHolderEmail"])) ticket.TicketHolderEmail = "";
                        else ticket.TicketHolderEmail = record["TicketHolderEmail"].ToString();

                        // Get TicketType
                        if (DBNull.Value.Equals(record["TicketType"])) ticket.TicketType = -1;
                        else ticket.TicketType = Convert.ToInt32(record["TicketType"].ToString());

                        // Get Amount
                        if (DBNull.Value.Equals(record["Amount"])) ticket.Amount = -1;
                        else ticket.Amount = Convert.ToInt32(record["Amount"].ToString());

                        // Add _Tickets
                        _Tickets.Add(ticket);
                    }
                }

                // Fail
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    // Clear Contactpersons
                    _Tickets.Clear();
                    _Tickets = null;
                }

            }

            // Return _Tickets
            return _Tickets;
        }

        public static int AddTicket(Ticket ticket)
        {
            // If _Tickets is null, create the Observable Collection
            if (_Tickets == null) GetTickets();

            try
            {
                // Add to db
                DbParameter param1 = Database.AddParameter("@ticketholder", ticket.TicketHolder);
                DbParameter param2 = Database.AddParameter("@ticketholderemail", ticket.TicketHolderEmail);
                DbParameter param3 = Database.AddParameter("@tickettype", ticket.TicketType);
                DbParameter param4 = Database.AddParameter("@amount", ticket.Amount);
                DbDataReader reader = Database.GetData("INSERT INTO ticket(TicketHolder, TicketHolderEmail, TicketType, Amount) VALUES(@ticketholder, @ticketholderemail, @tickettype, @amount); SELECT LAST_INSERT_ID() AS ID;", param1, param2, param3, param4);
                foreach (DbDataRecord record in reader)
                {
                    // Get ID
                    if (DBNull.Value.Equals(record["ID"])) ticket.ID = -1;
                    else ticket.ID = Convert.ToInt32(record["ID"]);
                }

                _Tickets.Add(ticket);
                return ticket.ID;
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return -1;
        }

        public static void UpdateTicket(Ticket ticket)
        {
            // If _Tickets is null, create the Observable Collection
            if (_Tickets == null) GetTickets();

            try
            {
                // Update db
                DbParameter param1 = Database.AddParameter("@id", ticket.ID);
                DbParameter param2 = Database.AddParameter("@ticketholder", ticket.TicketHolder);
                DbParameter param3 = Database.AddParameter("@ticketholderemail", ticket.TicketHolderEmail);
                DbParameter param4 = Database.AddParameter("@tickettype", ticket.TicketType);
                DbParameter param5 = Database.AddParameter("@amount", ticket.Amount);
                int affectedRows = Database.ModifyData("UPDATE ticket SET TicketHolder = @ticketholder, TicketHolderEmail = @ticketholderemail, TicketType = @tickettype, Amount = @amount WHERE id = @id", param1, param2, param3, param4, param5);
                if (affectedRows == 0) return;

                // Update _Tickets
                _Tickets[GetIndexByID(ticket.ID)] = ticket;
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void DeleteTicket(Ticket ticket)
        {
            // If _Tickets is null, create the Observable Collection
            if (_Tickets == null) GetTickets();

            try
            {
                // Add to db
                DbParameter param = Database.AddParameter("@id", ticket.ID);
                int affectedRows = Database.ModifyData("DELETE FROM ticket WHERE id = @id", param);
                if (affectedRows == 0) return;

                // Update _Tickets
                _Tickets.RemoveAt(GetIndexByID(ticket.ID));
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static int GetIndexByID(int id)
        {
            for (int i = 0; i < _Tickets.Count; ++i)
            {
                if (_Tickets[i].ID == id) return i;
            }
            return -1;
        }

        public Ticket Copy()
        {
            return new Ticket() { ID = this.ID, TicketHolder = this.TicketHolder, TicketHolderEmail = this.TicketHolderEmail, TicketType = this.TicketType, Amount = this.Amount };
        }*/
    }
}
