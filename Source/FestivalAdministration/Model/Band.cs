using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalAdministration.Model
{
    class Band
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

        private string _Twitter;

        public string Twitter
        {
            get { return _Twitter; }
            set { _Twitter = value; }
        }

        private string _Facebook;

        public string Facebook
        {
            get { return _Facebook; }
            set { _Facebook = value; }
        }

        private byte[] _Picture;

        public byte[] Picture
        {
            get { return _Picture; }
            set { _Picture = value; }
        }

        private string _Description;

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        private static ObservableCollection<Band> _Bands = null;

        public static ObservableCollection<Band> GetBands()
        {
            // If _Band is null, create the Observable Collection
            if (_Bands == null)
            {
                try
                {
                    // Create _Band
                    _Bands = new ObservableCollection<Band>();

                    // Get data
                    DbDataReader reader = Database.GetData("SELECT * FROM band");
                    foreach (DbDataRecord record in reader)
                    {
                        // Create new Band
                        Band band = new Band();

                        // Get ID
                        if (DBNull.Value.Equals(record["ID"])) band.ID = -1;
                        else band.ID = Convert.ToInt32(record["ID"]);

                        // Get Name
                        if (DBNull.Value.Equals(record["Name"])) band.Name = "";
                        else band.Name = record["Name"].ToString();

                        // Get Twitter
                        if (DBNull.Value.Equals(record["Twitter"])) band.Twitter = "";
                        else band.Twitter = record["Twitter"].ToString();

                        // Get Facebook
                        if (DBNull.Value.Equals(record["Facebook"])) band.Facebook = "";
                        else band.Facebook = record["Facebook"].ToString();

                        // Get Picture
                        if (DBNull.Value.Equals(record["Picture"])) band.Picture = null;
                        else band.Picture = (byte[])record["Picture"];

                        // Get Description
                        if (DBNull.Value.Equals(record["Description"])) band.Description = "";
                        else band.Description = record["Description"].ToString();

                        // Add Band
                        _Bands.Add(band);
                    }
                }

                // Fail
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    // Clear Bands
                    _Bands.Clear();
                    _Bands = null;
                }

            }

            // Return _Band
            return _Bands;
        }

        public static void AddBand(string name, string twitter, string facebook, byte[] picture, string description)
        {
            // If _Band is null, create the Observable Collection
            if (_Bands == null) GetBands();

            try
            {
                // Add to db
                DbParameter param1 = Database.AddParameter("@name", name);
                DbParameter param2 = Database.AddParameter("@twitter", twitter);
                DbParameter param3 = Database.AddParameter("@facebook", facebook);
                DbParameter param4 = Database.AddParameter("@picture", picture);
                DbParameter param5 = Database.AddParameter("@description", description);
                int affectedRows = Database.ModifyData("INSERT INTO band(Name, Twitter, Facebook, Picture, Description) VALUES(@name, @twitter, @facebook, @picture, @description)", param1, param2, param3, param4, param5);
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

                _Bands.Add(new Band() { ID = id, Name = name, Twitter = twitter, Facebook = facebook, Picture = picture, Description = description });
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void UpdateBand(int index, string name, string twitter, string facebook, byte[] picture, string description)
        {
            // If _Band is null, create the Observable Collection
            if (_Bands == null) GetBands();

            try
            {
                // Update db
                DbParameter param1 = Database.AddParameter("@id", _Bands[index].ID);
                DbParameter param2 = Database.AddParameter("@name", name);
                DbParameter param3 = Database.AddParameter("@twitter", twitter);
                DbParameter param4 = Database.AddParameter("@facebook", facebook);
                DbParameter param5 = Database.AddParameter("@picture", picture);
                DbParameter param6 = Database.AddParameter("@description", description);
                int affectedRows = Database.ModifyData("UPDATE band SET Name = @name, Twitter = @twitter, Facebook = @facebook, Picture = @picture, Description = @description WHERE id = @id", param1, param2, param3, param4, param5, param6);
                if (affectedRows == 0) return;

                // Update _Band
                _Bands[index].Name = name;
                _Bands[index].Twitter = twitter;
                _Bands[index].Facebook = facebook;
                _Bands[index].Picture = picture;
                _Bands[index].Description= description;
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void DeleteBand(int index)
        {
            // If _Band is null, create the Observable Collection
            if (_Bands == null) GetBands();

            // Only execute if index is valid
            if (_Bands.Count <= index) return;

            try
            {
                // Add to db
                DbParameter param = Database.AddParameter("@id", _Bands[index].ID);
                int affectedRows = Database.ModifyData("DELETE FROM band WHERE id = @id", param);
                if (affectedRows == 0) return;

                // Update _Band
                _Bands.RemoveAt(index);
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
