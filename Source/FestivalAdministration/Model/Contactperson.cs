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

        public static void AddContactperson(string name, string company, int function, string street, string city, string tel, string email, string extra)
        {
            // If _Contactperson is null, create the Observable Collection
            if (_Contactpersons == null) GetContactpersons();

            try
            {
                // Add to db
                DbParameter param1 = Database.AddParameter("@name", name);
                DbParameter param2 = Database.AddParameter("@company", company);
                DbParameter param3 = Database.AddParameter("@function", function);
                DbParameter param4 = Database.AddParameter("@street", street);
                DbParameter param5 = Database.AddParameter("@city", city);
                DbParameter param6 = Database.AddParameter("@tel", tel);
                DbParameter param7 = Database.AddParameter("@email", email);
                DbParameter param8 = Database.AddParameter("@extra", extra);
                int affectedRows = Database.ModifyData("INSERT INTO contact(Name, Company, Function, Street, City, Tel, Email, Extra) VALUES(@name, @company, @function, @street, @city, @tel, @email, @extra)", param1, param2, param3, param4, param5, param6, param7, param8);
                if (affectedRows == 0) return;

                // Get ID from db
                int id = 0;
                DbDataReader reader = Database.GetData("SELECT ID FROM contact WHERE name = @name AND company = @company AND function = @function AND street = @street AND city = @city AND tel = @tel AND email = @email AND extra = @extra)", param1, param2, param3, param4, param5, param6, param7, param8);
                foreach (DbDataRecord record in reader)
                {
                    // Get ID
                    if (DBNull.Value.Equals(record["ID"])) id = -1;
                    else id = Convert.ToInt32(record["ID"]);
                }

                _Contactpersons.Add(new Contactperson() { ID = id, Name = name, Company = company, Job = function, Street = street, City = city, Phone = tel, Email = email, Extra = extra });
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void UpdateContactperson(int index, string newname, string newcompany, int newfunction, string newstreet, string newcity, string newtel, string newemail, string newextra)
        {
            // If _Contactperson is null, create the Observable Collection
            if (_Contactpersons == null) GetContactpersons();

            try
            {
                // Update db
                DbParameter param1 = Database.AddParameter("@id", _Contactpersons[index].ID);
                DbParameter param2 = Database.AddParameter("@name", newname);
                DbParameter param3 = Database.AddParameter("@company", newcompany);
                DbParameter param4 = Database.AddParameter("@function", newfunction);
                DbParameter param5 = Database.AddParameter("@street", newstreet);
                DbParameter param6 = Database.AddParameter("@city", newcity);
                DbParameter param7 = Database.AddParameter("@tel", newtel);
                DbParameter param8 = Database.AddParameter("@email", newemail);
                DbParameter param9 = Database.AddParameter("@extra", newextra);
                int affectedRows = Database.ModifyData("UPDATE contact SET name = @name, company = @company, function = @function, street = @street, city = @city, tel = @tel, email = @email, extra = @extra WHERE id = @id", param1, param2, param3, param4, param5, param6, param7, param8, param9);
                if (affectedRows == 0) return;

                // Update _Contactperson
                _Contactpersons[index].Name = newname;
                _Contactpersons[index].Company = newcompany;
                _Contactpersons[index].Job = newfunction;
                _Contactpersons[index].Street = newstreet;
                _Contactpersons[index].City = newcity;
                _Contactpersons[index].Phone = newtel;
                _Contactpersons[index].Email = newemail;
                _Contactpersons[index].Extra = newextra;
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void DeleteContactperson(int index)
        {
            // If _Contactperson is null, create the Observable Collection
            if (_Contactpersons == null) GetContactpersons();

            // Only execute if index is valid
            if (_Contactpersons.Count <= index) return;

            try
            {
                // Add to db
                DbParameter param = Database.AddParameter("@id", _Contactpersons[index].ID);
                int affectedRows = Database.ModifyData("DELETE FROM contact WHERE id = @id", param);
                if (affectedRows == 0) return;

                // Update _Contactperson
                _Contactpersons.RemoveAt(index);
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
