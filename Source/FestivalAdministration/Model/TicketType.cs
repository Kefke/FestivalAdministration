using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalAdministration.Model
{
    class TicketType
    {
        private int _ID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private double _Price;

        public double Price
        {
            get { return _Price; }
            set { _Price = value; }
        }

        private int _AvailableTickets;

        public int AvailableTickets
        {
            get { return _AvailableTickets; }
            set { _AvailableTickets = value; }
        }

        private int _TicketsLeft;

        public int TicketsLeft
        {
            get { return _TicketsLeft; }
            set { _TicketsLeft = value; }
        }

        private static ObservableCollection<TicketType> _TicketTypes = null;

        public static ObservableCollection<TicketType> GetTicketTypes()
        {
            // If _TicketTypes is null, create the Observable Collection
            if (_TicketTypes == null)
            {
                try
                {
                    // Create _Contactperson
                    _TicketTypes = new ObservableCollection<TicketType>();

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
                        _TicketTypes.Add(type);
                    }
                }

                // Fail
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    // Clear TicketTypes
                    _TicketTypes.Clear();
                    _TicketTypes = null;
                }

            }

            // Return _TicketTypes
            return _TicketTypes;
        }

        public static void AddTicketType(string name, float price, int quantity)
        {
            // If _TicketTypes is null, create the Observable Collection
            if (_TicketTypes == null) GetTicketTypes();

            try
            {
                // Add to db
                DbParameter param1 = Database.AddParameter("@name", name);
                DbParameter param2 = Database.AddParameter("@price", price);
                DbParameter param3 = Database.AddParameter("@quantity", quantity);
                int affectedRows = Database.ModifyData("INSERT INTO tickettype(Name, Price, AvailableTickets, TicketsLeft) VALUES(@name, @price, @quantity, @quantity)", param1, param2, param3);
                if (affectedRows == 0) return;

                // Get ID from db
                int id = 0;
                DbDataReader reader = Database.GetData("SELECT LAST_INSERT_ID() AS ID");
                foreach (DbDataRecord record in reader)
                {
                    // Get ID
                    if (DBNull.Value.Equals(record["ID"])) id = -1;
                    else id = Convert.ToInt32(record["ID"]);
                }

                _TicketTypes.Add(new TicketType() { ID = id, Name = name, Price = price, AvailableTickets = quantity, TicketsLeft = quantity });
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void UpdateTicketType(int index, string newname, float newprice, int newquantity, int newticketsleft)
        {
            // If _TicketTypes is null, create the Observable Collection
            if (_TicketTypes == null) GetTicketTypes();

            try
            {
                // Update db
                DbParameter param1 = Database.AddParameter("@id", _TicketTypes[index].ID);
                DbParameter param2 = Database.AddParameter("@name", newname);
                DbParameter param3 = Database.AddParameter("@price", newprice);
                DbParameter param4 = Database.AddParameter("@quantity", newquantity);
                DbParameter param5 = Database.AddParameter("@available", newticketsleft);
                int affectedRows = Database.ModifyData("UPDATE tickettype SET Name = @name, Price = @price, AvailableTickets = @quantity, TicketsLeft = @available WHERE id = @id", param1, param2, param3, param4, param5);
                if (affectedRows == 0) return;

                // Update _TicketTypes
                _TicketTypes[index].Name = newname;
                _TicketTypes[index].Price = newprice;
                _TicketTypes[index].AvailableTickets = newquantity;
                _TicketTypes[index].TicketsLeft = newticketsleft;
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void DeleteTicketType(int index)
        {
            // If _TicketTypes is null, create the Observable Collection
            if (_TicketTypes == null) GetTicketTypes();

            // Only execute if index is valid
            if (_TicketTypes.Count <= index) return;

            try
            {
                // Add to db
                DbParameter param = Database.AddParameter("@id", _TicketTypes[index].ID);
                int affectedRows = Database.ModifyData("DELETE FROM tickettype WHERE id = @id", param);
                if (affectedRows == 0) return;

                // Update _TicketTypes
                _TicketTypes.RemoveAt(index);
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static string GetTypeByID(int id)
        {
            // If _TicketTypes is null, create the Observable Collection
            if (_TicketTypes == null) GetTicketTypes();

            foreach (TicketType type in _TicketTypes)
            {
                if (type.ID == id)
                    return type.Name;
            }

            return "";
        }

        public static int GetIndexByID(int id)
        {
            // If _TicketTypes is null, create the Observable Collection
            if (_TicketTypes == null) GetTicketTypes();

            for (int i = 0; i < _TicketTypes.Count; ++i)
            {
                if (_TicketTypes[i].ID == id) return i;
            }
            return -1;
        }
    }
}
