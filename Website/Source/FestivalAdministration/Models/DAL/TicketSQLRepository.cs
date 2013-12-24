﻿using System;
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
                    if (DBNull.Value.Equals(record["TicketType"])) ticket.TicketTypeID = -1;
                    else ticket.TicketTypeID = Convert.ToInt32(record["TicketType"].ToString());

                    // Get Amount
                    if (DBNull.Value.Equals(record["Amount"])) ticket.Amount = -1;
                    else ticket.Amount = Convert.ToInt32(record["Amount"].ToString());

                    // Get TicketType
                    TicketTypeSQLRepository.GetTicketType(ticket.TicketTypeID);

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
                    TicketTypeSQLRepository.GetTicketType(ticket.TicketTypeID);

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
    }
}