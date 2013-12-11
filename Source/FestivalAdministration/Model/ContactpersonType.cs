using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalAdministration.Model
{
    class ContactpersonType
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

        private static ObservableCollection<ContactpersonType> _ContactpersonTypes = null;

        public static ObservableCollection<ContactpersonType> GetContactpersonTypes()
        {
            // If _ContactpersonType is null, create the Observable Collection
            if (_ContactpersonTypes == null)
            {
                try
                {
                    // Create _ContactpersonType
                    _ContactpersonTypes = new ObservableCollection<ContactpersonType>();

                    // Get data
                    DbDataReader reader = Database.GetData("SELECT * FROM contacttype");
                    foreach (DbDataRecord record in reader)
                    {
                        // Create new ContactpersonType
                        ContactpersonType type = new ContactpersonType();

                        // Get ID
                        if (DBNull.Value.Equals(record["ID"])) type.ID = -1;
                        else type.ID = Convert.ToInt32(record["ID"]);

                        // Get Name
                        if (DBNull.Value.Equals(record["Name"])) type.Name = "";
                        else type.Name = record["Name"].ToString();

                        // Add ContactpersonType
                        _ContactpersonTypes.Add(type);
                    }
                }

                // Fail
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    // Clear ContactpersonTypes
                    _ContactpersonTypes.Clear();
                    _ContactpersonTypes = null;
                }

            }

            // Return _ContactpersonType
            return _ContactpersonTypes;
        }

        public static void AddContactpersonType(string name)
        {
            // If _ContactpersonType is null, create the Observable Collection
            if (_ContactpersonTypes == null) GetContactpersonTypes();

            try
            {
                // Add to db
                DbParameter param = Database.AddParameter("@name", name);
                int affectedRows = Database.ModifyData("INSERT INTO contacttype(name) VALUES(@name)", param);
                if (affectedRows == 0) return;

                // Get ID from db
                int id = 0;
                DbDataReader reader = Database.GetData("SELECT ID FROM contacttype WHERE name = @name", param);
                foreach (DbDataRecord record in reader)
                {
                    // Get ID
                    if (DBNull.Value.Equals(record["ID"])) id = -1;
                    else id = Convert.ToInt32(record["ID"]);
                }

                _ContactpersonTypes.Add(new ContactpersonType() { ID = id, Name = name });
            }

            // Fail
            catch (Exception ex)
            { 
                Console.WriteLine(ex.Message);
            }
        }

        public static void UpdateContactpersonType(int index, string newname)
        {
            // If _ContactpersonType is null, create the Observable Collection
            if (_ContactpersonTypes == null) GetContactpersonTypes();

            try
            {
                // Update db
                DbParameter param1 = Database.AddParameter("@id", _ContactpersonTypes[index].ID);
                DbParameter param2 = Database.AddParameter("@name", newname);
                int affectedRows = Database.ModifyData("UPDATE contacttype SET name = @name WHERE id = @id", param1, param2);
                if (affectedRows == 0) return;

                // Update _ContactpersonTypes
                _ContactpersonTypes[index].Name = newname;
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void DeleteContactpersonType(int index)
        {
            // If _ContactpersonType is null, create the Observable Collection
            if (_ContactpersonTypes == null) GetContactpersonTypes();

            // Only execute if index is valid
            if (_ContactpersonTypes.Count <= index) return;

            try
            {
                // Add to db
                DbParameter param = Database.AddParameter("@id", _ContactpersonTypes[index].ID);
                int affectedRows = Database.ModifyData("DELETE FROM contacttype WHERE id = @id", param);
                if (affectedRows == 0) return;

                // Update _ContactpersonTypes
                _ContactpersonTypes.RemoveAt(index);
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
