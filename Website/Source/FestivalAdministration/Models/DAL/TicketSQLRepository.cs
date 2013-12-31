using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace FestivalAdministration.Models.DAL
{
    public class TicketSQLRepository
    {
        public static List<Ticket> GetTickets()
        {
            List<Ticket> Tickets = new List<Ticket>();

            try
            {
                // Get data
                DbDataReader reader = Database.GetData("SELECT * FROM ticket ORDER BY TicketHolder");
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
                    if (DBNull.Value.Equals(record["TicketType"])) ticket.TicketTypeID = -1;
                    else ticket.TicketTypeID = Convert.ToInt32(record["TicketType"].ToString());

                    // Get Amount
                    if (DBNull.Value.Equals(record["Amount"])) ticket.Amount = -1;
                    else ticket.Amount = Convert.ToInt32(record["Amount"].ToString());

                    // Get TicketType
                    ticket.TicketType = TicketTypeSQLRepository.GetTicketType(ticket.TicketTypeID);

                    // Add _Tickets
                    Tickets.Add(ticket);
                }
                return Tickets;
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public static List<Ticket> GetTickets(string email)
        {
            List<Ticket> tickets = new List<Ticket>();

            try
            {
                // Get data
                DbParameter param = Database.AddParameter("@email", email);
                DbDataReader reader = Database.GetData("SELECT * FROM ticket WHERE TicketHolderEmail = @email", param);
                foreach (DbDataRecord record in reader)
                {
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
                    if (DBNull.Value.Equals(record["TicketType"])) ticket.TicketTypeID = -1;
                    else ticket.TicketTypeID = Convert.ToInt32(record["TicketType"].ToString());

                    // Get Amount
                    if (DBNull.Value.Equals(record["Amount"])) ticket.Amount = -1;
                    else ticket.Amount = Convert.ToInt32(record["Amount"].ToString());

                    // Get TicketType
                    ticket.TicketType = TicketTypeSQLRepository.GetTicketType(ticket.TicketTypeID);

                    tickets.Add(ticket);
                }
                return tickets;
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public static void AddTicket(Ticket ticket)
        {
            try
            {
                // Add to db
                DbParameter param1 = Database.AddParameter("@ticketholder", ticket.TicketHolder);
                DbParameter param2 = Database.AddParameter("@ticketholderemail", ticket.TicketHolderEmail);
                DbParameter param3 = Database.AddParameter("@tickettype", ticket.TicketTypeID);
                DbParameter param4 = Database.AddParameter("@amount", ticket.Amount);
                
                Database.ModifyData("INSERT INTO ticket(TicketHolder, TicketHolderEmail, TicketType, Amount) VALUES(@ticketholder, @ticketholderemail, @tickettype, @amount)", param1, param2, param3, param4);
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void UpdateTicket(Ticket ticket)
        {
           try
            {
                // Update db
                DbParameter param1 = Database.AddParameter("@id", ticket.ID);
                DbParameter param2 = Database.AddParameter("@ticketholder", ticket.TicketHolder);
                DbParameter param3 = Database.AddParameter("@ticketholderemail", ticket.TicketHolderEmail);
                DbParameter param4 = Database.AddParameter("@tickettype", ticket.TicketTypeID);
                DbParameter param5 = Database.AddParameter("@amount", ticket.Amount);
                Database.ModifyData("UPDATE ticket SET TicketHolder = @ticketholder, TicketHolderEmail = @ticketholderemail, TicketType = @tickettype, Amount = @amount WHERE id = @id", param1, param2, param3, param4, param5);
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static int GetNumberOfTicketsByType(int ticketTypeID)
        {
            try
            {
                // Get data
                DbParameter param = Database.AddParameter("@ticketType", ticketTypeID);
                DbDataReader reader = Database.GetData("SELECT SUM(Amount) AS Amount FROM Ticket WHERE TicketType = @ticketType", param);
                foreach (DbDataRecord record in reader)
                {
                    // Get Amount
                    if (DBNull.Value.Equals(record["Amount"])) return 0;
                    else return Convert.ToInt32(record["Amount"].ToString());
                }
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

        public static void UpdateEmail(string oldemail, string newemail)
        {
            if (oldemail == newemail) return;
            List<Ticket> tickets = GetTickets(oldemail);
            foreach (Ticket ticket in tickets)
            {
                ticket.TicketHolderEmail = newemail;
                UpdateTicket(ticket);
            }
        }
    }
}