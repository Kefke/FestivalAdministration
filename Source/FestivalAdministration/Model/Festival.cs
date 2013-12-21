using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalAdministration.Model
{
    class Festival
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

        private string _Street;

        public string Street
        {
            get { return _Street; }
            set { _Street = value; }
        }

        private string _City;

        public string City
        {
            get { return _City; }
            set { _City = value; }
        }

        private static ObservableCollection<Festival> _Festivals = null;

        public static ObservableCollection<Festival> GetFestivals()
        {
            // If _Festivals is null, create the Observable Collection
            if (_Festivals == null)
            {
                try
                {
                    // Create _Festivals
                    _Festivals = new ObservableCollection<Festival>();

                    // Get data
                    DbDataReader reader = Database.GetData("SELECT * FROM festival");
                    foreach (DbDataRecord record in reader)
                    {
                        // Create new Festival
                        Festival festival = new Festival();

                        // Get ID
                        if (DBNull.Value.Equals(record["ID"])) festival.ID = -1;
                        else festival.ID = Convert.ToInt32(record["ID"]);

                        // Get Name
                        if (DBNull.Value.Equals(record["Name"])) festival.Name = "";
                        else festival.Name = record["Name"].ToString();

                        // Get Street
                        if (DBNull.Value.Equals(record["Street"])) festival.Street = "";
                        else festival.Street = record["Street"].ToString();

                        // Get City
                        if (DBNull.Value.Equals(record["City"])) festival.City = "";
                        else festival.City = record["City"].ToString();

                        // Add Festival
                        _Festivals.Add(festival);
                    }
                }

                // Fail
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    // Clear Festivals
                    _Festivals.Clear();
                    _Festivals = null;
                }

            }

            // Return Festivals
            return _Festivals;
        }

        public static void AddFestival(Festival festival)
        {
            // If _Festivals is null, create the Observable Collection
            if (_Festivals == null) GetFestivals();

            try
            {
                // Add to db
                DbParameter param1 = Database.AddParameter("@name", festival.Name);
                DbParameter param2 = Database.AddParameter("@street", festival.Street);
                DbParameter param3 = Database.AddParameter("@city", festival.City);
                int affectedRows = Database.ModifyData("INSERT INTO festival(Name, Street, City) VALUES(@name, @street, @city)", param1, param2, param3);
                if (affectedRows == 0) return;

                // Get ID from db
                DbDataReader reader = Database.GetData("SELECT LAST_INSERT_ID() AS ID");
                foreach (DbDataRecord record in reader)
                {
                    // Get ID
                    if (DBNull.Value.Equals(record["ID"])) festival.ID = -1;
                    else festival.ID = Convert.ToInt32(record["ID"]);
                }

                _Festivals.Add(festival);
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void UpdateFestival(Festival festival)
        {
            // If _Festivals is null, create the Observable Collection
            if (_Festivals == null) GetFestivals();

            try
            {
                // Update db
                DbParameter param1 = Database.AddParameter("@id", festival.ID);
                DbParameter param2 = Database.AddParameter("@name", festival.Name);
                DbParameter param3 = Database.AddParameter("@street", festival.Street);
                DbParameter param4 = Database.AddParameter("@city", festival.City);
                int affectedRows = Database.ModifyData("UPDATE festival SET name = @name, street = @street, city = @city WHERE id = @id", param1, param2, param3, param4);
                if (affectedRows == 0) return;

                // Update _Festivals
                for (int i = 0; i < _Festivals.Count; ++i)
                {
                    if (_Festivals[i].ID == festival.ID)
                    {
                        _Festivals[i] = festival;
                        i = _Festivals.Count;
                    }
                }
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /*public static void DeleteFestival(int index)
        {
            // If _Festivals is null, create the Observable Collection
            if (_Festivals == null) GetFestivals();

            // Only execute if index is valid
            if (_Festivals.Count <= index) return;

            try
            {
                // Add to db
                DbParameter param = Database.AddParameter("@id", _Festivals[index].ID);
                int affectedRows = Database.ModifyData("DELETE FROM festival WHERE id = @id", param);
                if (affectedRows == 0) return;

                // Update _Festivals
                _Festivals.RemoveAt(index);
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }*/
    }
}
