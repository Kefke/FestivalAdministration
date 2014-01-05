using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalAdministration.Model
{
    class Contactperson
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

        private string _Company;

        public string Company
        {
            get { return _Company; }
            set { _Company = value; }
        }

        private int _Job;

        public int Job
        {
            get { return _Job; }
            set { _Job = value; }
        }

        private string _City;

        public string City
        {
            get { return _City; }
            set { _City = value; }
        }

        private string _Street;

        public string Street
        {
            get { return _Street; }
            set { _Street = value; }
        }

        private string _Phone;

        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }

        private string _Email;

        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        private string _Extra;

        public string Extra
        {
            get { return _Extra; }
            set { _Extra = value; }
        }

        private static ObservableCollection<Contactperson> _Contactpersons = null;

        public static ObservableCollection<Contactperson> GetContactpersons()
        {
            // If _Contactperson is null, create the Observable Collection
            if (_Contactpersons == null)
            {
                try
                {
                    // Create _Contactperson
                    _Contactpersons = new ObservableCollection<Contactperson>();

                    // Get data
                    DbDataReader reader = Database.GetData("SELECT * FROM contact");
                    foreach (DbDataRecord record in reader)
                    {
                        // Create new Contactperson
                        Contactperson contact = new Contactperson();

                        // Get ID
                        if (DBNull.Value.Equals(record["ID"])) contact.ID = -1;
                        else contact.ID = Convert.ToInt32(record["ID"]);

                        // Get Name
                        if (DBNull.Value.Equals(record["Name"])) contact.Name = "";
                        else contact.Name = record["Name"].ToString();

                        // Get Company
                        if (DBNull.Value.Equals(record["Company"])) contact.Company = "";
                        else contact.Company = record["Company"].ToString();

                        // Get Function
                        if (DBNull.Value.Equals(record["Function"])) contact.Job = -1;
                        else contact.Job = Convert.ToInt32(record["Function"].ToString());

                        // Get Street
                        if (DBNull.Value.Equals(record["Street"])) contact.Street = "";
                        else contact.Street = record["Street"].ToString();

                        // Get City
                        if (DBNull.Value.Equals(record["City"])) contact.City = "";
                        else contact.City = record["City"].ToString();

                        // Get Tel
                        if (DBNull.Value.Equals(record["Tel"])) contact.Phone = "";
                        else contact.Phone = record["Tel"].ToString();

                        // Get Email
                        if (DBNull.Value.Equals(record["Email"])) contact.Email = "";
                        else contact.Email = record["Email"].ToString();

                        // Get Extra
                        if (DBNull.Value.Equals(record["Extra"])) contact.Extra = "";
                        else contact.Extra = record["Extra"].ToString();

                        // Add Contactperson
                        _Contactpersons.Add(contact);
                    }
                    if (reader != null)
                        reader.Close();
                }

                // Fail
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    // Clear Contactpersons
                    _Contactpersons.Clear();
                    _Contactpersons = null;
                }

            }

            // Return _Contactperson
            return _Contactpersons;
        }

        public static ObservableCollection<Contactperson> SearchContactpersons(string name)
        {
            try
            {
                // Create _Contactperson
                ObservableCollection<Contactperson> results = new ObservableCollection<Contactperson>();

                // Get data
                DbParameter param = Database.AddParameter("@name", "%"+name+"%");
                DbDataReader reader = Database.GetData("SELECT * FROM contact WHERE Name LIKE @name", param);
                foreach (DbDataRecord record in reader)
                {
                    // Create new Contactperson
                    Contactperson contact = new Contactperson();

                    // Get ID
                    if (DBNull.Value.Equals(record["ID"])) contact.ID = -1;
                    else contact.ID = Convert.ToInt32(record["ID"]);

                    // Get Name
                    if (DBNull.Value.Equals(record["Name"])) contact.Name = "";
                    else contact.Name = record["Name"].ToString();

                    // Get Company
                    if (DBNull.Value.Equals(record["Company"])) contact.Company = "";
                    else contact.Company = record["Company"].ToString();

                    // Get Function
                    if (DBNull.Value.Equals(record["Function"])) contact.Job = -1;
                    else contact.Job = Convert.ToInt32(record["Function"].ToString());

                    // Get Street
                    if (DBNull.Value.Equals(record["Street"])) contact.Street = "";
                    else contact.Street = record["Street"].ToString();

                    // Get City
                    if (DBNull.Value.Equals(record["City"])) contact.City = "";
                    else contact.City = record["City"].ToString();

                    // Get Tel
                    if (DBNull.Value.Equals(record["Tel"])) contact.Phone = "";
                    else contact.Phone = record["Tel"].ToString();

                    // Get Email
                    if (DBNull.Value.Equals(record["Email"])) contact.Email = "";
                    else contact.Email = record["Email"].ToString();

                    // Get Extra
                    if (DBNull.Value.Equals(record["Extra"])) contact.Extra = "";
                    else contact.Extra = record["Extra"].ToString();

                    // Add Contactperson
                    results.Add(contact);
                }
                if (reader != null)
                    reader.Close();

                return results;
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public static int AddContactperson(Contactperson person)
        {
            // If _Contactperson is null, create the Observable Collection
            if (_Contactpersons == null) GetContactpersons();

            try
            {
                // Add to db
                DbParameter param1 = Database.AddParameter("@name", person.Name);
                DbParameter param2 = Database.AddParameter("@company", person.Company);
                DbParameter param3 = Database.AddParameter("@function", person.Job);
                DbParameter param4 = Database.AddParameter("@street", person.Street);
                DbParameter param5 = Database.AddParameter("@city", person.City);
                DbParameter param6 = Database.AddParameter("@tel", person.Phone);
                DbParameter param7 = Database.AddParameter("@email", person.Email);
                DbParameter param8 = Database.AddParameter("@extra", person.Extra);
                
                DbDataReader reader = Database.GetData("INSERT INTO contact(Name, Company, Function, Street, City, Tel, Email, Extra) VALUES(@name, @company, @function, @street, @city, @tel, @email, @extra); SELECT LAST_INSERT_ID() AS ID;", param1, param2, param3, param4, param5, param6, param7, param8);
                foreach (DbDataRecord record in reader)
                {
                    // Get ID
                    if (DBNull.Value.Equals(record["ID"])) person.ID = -1;
                    else person.ID = Convert.ToInt32(record["ID"]);
                }
                if (reader != null)
                    reader.Close();

                _Contactpersons.Add(person);
                return person.ID;
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        public static void UpdateContactperson(Contactperson person)
        {
            // If _Contactperson is null, create the Observable Collection
            if (_Contactpersons == null) GetContactpersons();

            try
            {
                // Update db
                DbParameter param1 = Database.AddParameter("@id", person.ID);
                DbParameter param2 = Database.AddParameter("@name", person.Name);
                DbParameter param3 = Database.AddParameter("@company", person.Company);
                DbParameter param4 = Database.AddParameter("@function", person.Job);
                DbParameter param5 = Database.AddParameter("@street", person.Street);
                DbParameter param6 = Database.AddParameter("@city", person.City);
                DbParameter param7 = Database.AddParameter("@tel", person.Phone);
                DbParameter param8 = Database.AddParameter("@email", person.Email);
                DbParameter param9 = Database.AddParameter("@extra", person.Extra);
                int affectedRows = Database.ModifyData("UPDATE contact SET name = @name, company = @company, function = @function, street = @street, city = @city, tel = @tel, email = @email, extra = @extra WHERE id = @id", param1, param2, param3, param4, param5, param6, param7, param8, param9);
                if (affectedRows == 0) return;

                // Update _Contactperson
                _Contactpersons[GetIndexByID(person.ID)] = person;
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void DeleteContactperson(Contactperson person)
        {
            // If _Contactperson is null, create the Observable Collection
            if (_Contactpersons == null) GetContactpersons();

            try
            {
                // Add to db
                DbParameter param = Database.AddParameter("@id", person.ID);
                int affectedRows = Database.ModifyData("DELETE FROM contact WHERE id = @id", param);
                if (affectedRows == 0) return;

                // Update _Contactperson
                _Contactpersons.RemoveAt(GetIndexByID(person.ID));
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static int GetIndexByID(int id)
        {
            for (int i = 0; i < _Contactpersons.Count; ++i)
            {
                if (_Contactpersons[i].ID == id) return i;
            }
            return -1;
        }

        public Contactperson Copy()
        {
            return new Contactperson() { ID = this.ID, Name = this.Name, Company = this.Company, Job = this.Job, Street = this.Street, City = this.City, Phone = this.Phone, Email = this.Email, Extra = this.Extra};
        }
    }
}

