using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace FestivalAdministration.Models.DAL
{
    public class TicketTypeSQLRepository
    {
        public static List<TicketType> GetTicketTypes()
        {
            List<TicketType> ticketTypes = new List<TicketType>();
            try
            {
                // Get data
                DbDataReader reader = Database.GetData("SELECT * FROM tickettype");
                foreach (DbDataRecord record in reader)
                {
                    // Create new TicketType
                    TicketType type = new TicketType();

                    // Get ID
                    if (DBNull.Value.Equals(record["ID"])) type.ID = -1;
                    else type.ID = Convert.ToInt32(record["ID"]);

                    // Get Name
                    if (DBNull.Value.Equals(record["Name"])) type.Name = "";
                    else type.Name = record["Name"].ToString();

                    // Get Price
                    if (DBNull.Value.Equals(record["Price"])) type.Price = -1;
                    else type.Price = Convert.ToDouble(record["Price"].ToString());

                    // Get AvailableTickets
                    if (DBNull.Value.Equals(record["AvailableTickets"])) type.AvailableTickets = -1;
                    else type.AvailableTickets = Convert.ToInt32(record["AvailableTickets"].ToString());

                    // Get TicketsLeft
                    if (DBNull.Value.Equals(record["TicketsLeft"])) type.TicketsLeft = -1;
                    else type.TicketsLeft = Convert.ToInt32(record["TicketsLeft"].ToString());

                    // Add TicketType
                    ticketTypes.Add(type);
                }
                return ticketTypes;
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public static TicketType GetTicketType(int ID)
        {
            try
            {
                // Create new TicketType
                TicketType type = new TicketType();

                // Get data
                DbParameter param = Database.AddParameter("@id", ID);
                DbDataReader reader = Database.GetData("SELECT * FROM tickettype WHERE ID = @id", param);
                foreach (DbDataRecord record in reader)
                {
                    // Get ID
                    if (DBNull.Value.Equals(record["ID"])) type.ID = -1;
                    else type.ID = Convert.ToInt32(record["ID"]);

                    // Get Name
                    if (DBNull.Value.Equals(record["Name"])) type.Name = "";
                    else type.Name = record["Name"].ToString();

                    // Get Price
                    if (DBNull.Value.Equals(record["Price"])) type.Price = -1;
                    else type.Price = Convert.ToDouble(record["Price"].ToString());

                    // Get AvailableTickets
                    if (DBNull.Value.Equals(record["AvailableTickets"])) type.AvailableTickets = -1;
                    else type.AvailableTickets = Convert.ToInt32(record["AvailableTickets"].ToString());

                    // Get TicketsLeft
                    if (DBNull.Value.Equals(record["TicketsLeft"])) type.TicketsLeft = -1;
                    else type.TicketsLeft = Convert.ToInt32(record["TicketsLeft"].ToString());

                }
                return type;
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public static void UpdateTicketTypeLeft(int ticketTypeID, int left)
        {
            try
            {
                // Update db
                DbParameter param1 = Database.AddParameter("@id", ticketTypeID);
                DbParameter param2 = Database.AddParameter("@left", left);
                int affectedRows = Database.ModifyData("UPDATE tickettype SET TicketsLeft = @left WHERE id = @id", param1, param2);
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}