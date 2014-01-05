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
                    if (reader != null)
                        reader.Close();
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

        public static int AddContactpersonType(ContactpersonType type)
        {
            // If _ContactpersonType is null, create the Observable Collection
            if (_ContactpersonTypes == null) GetContactpersonTypes();

            try
            {
                // Add to db
                DbParameter param = Database.AddParameter("@name", type.Name);

                // Get ID from db
                DbDataReader reader = Database.GetData("INSERT INTO contacttype(name) VALUES(@name); SELECT LAST_INSERT_ID() AS ID;", param);
                foreach (DbDataRecord record in reader)
                {
                    // Get ID
                    if (DBNull.Value.Equals(record["ID"])) type.ID = -1;
                    else type.ID = Convert.ToInt32(record["ID"]);
                }
                if (reader != null)
                    reader.Close();

                _ContactpersonTypes.Add(type);
                return type.ID;
            }

            // Fail
            catch (Exception ex)
            { 
                Console.WriteLine(ex.Message);
            }
            return -1;
        }

        public static void UpdateContactpersonType(ContactpersonType type)
        {
            // If _ContactpersonType is null, create the Observable Collection
            if (_ContactpersonTypes == null) GetContactpersonTypes();

            try
            {
                // Update db
                DbParameter param1 = Database.AddParameter("@id", type.ID);
                DbParameter param2 = Database.AddParameter("@name", type.Name);
                int affectedRows = Database.ModifyData("UPDATE contacttype SET name = @name WHERE id = @id", param1, param2);
                if (affectedRows == 0) return;

                // Update _ContactpersonTypes
                _ContactpersonTypes[GetIndexByID(type.ID)] = type;
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void DeleteContactpersonType(ContactpersonType type)
        {
            // If _ContactpersonType is null, create the Observable Collection
            if (_ContactpersonTypes == null) GetContactpersonTypes();

            try
            {
                // Add to db
                DbParameter param = Database.AddParameter("@id", type.ID);
                int affectedRows = Database.ModifyData("DELETE FROM contacttype WHERE id = @id", param);
                if (affectedRows == 0) return;

                // Update _ContactpersonTypes
                _ContactpersonTypes.RemoveAt(GetIndexByID(type.ID));
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static string GetTypeByID(int id)
        {
            // If _ContactpersonType is null, create the Observable Collection
            if (_ContactpersonTypes == null) GetContactpersonTypes();

            foreach (ContactpersonType type in _ContactpersonTypes)
            {
                if (type.ID == id)
                    return type.Name;
            }

            return "";
        }

        public static int GetIndexByID(int id)
        {
            // If _ContactpersonType is null, create the Observable Collection
            if (_ContactpersonTypes == null) GetContactpersonTypes();

            for (int i = 0; i < _ContactpersonTypes.Count; ++i)
            {
                if (_ContactpersonTypes[i].ID == id)
                    return i;
            }

            return -1;
        }

        public ContactpersonType Copy()
        {
            return new ContactpersonType() { ID = this.ID, Name = this.Name };
        }
    }
}
