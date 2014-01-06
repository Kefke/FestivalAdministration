using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FestivalAdministration.Model
{
    public class Band : INotifyPropertyChanged, IDataErrorInfo
    {
        private int _ID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private string _Name;

        [Required(ErrorMessage = "The Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The Name has to be between 3 and 50 characters")]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _Twitter;

        [Required(ErrorMessage = "Twitter URL is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Twitter URL has to be between 3 and 50 characters")]
        [Url(ErrorMessage="Please insert an URL")]
        public string Twitter
        {
            get { return _Twitter; }
            set { _Twitter = value; }
        }

        private string _Facebook;

        [Required(ErrorMessage = "Facebook URL is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Facebook URL has to be between 3 and 50 characters")]
        [Url(ErrorMessage = "Please insert an URL")]
        public string Facebook
        {
            get { return _Facebook; }
            set { _Facebook = value; }
        }

        private byte[] _Picture;

        [Required(ErrorMessage = "Picture is required")]
        public byte[] Picture
        {
            get { return _Picture; }
            set { _Picture = value; }
        }

        private string _Description;

        [StringLength(2000, MinimumLength = 0, ErrorMessage = "The description is maximum 2000 characters")]
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
                    if (reader != null)
                        reader.Close();
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

        public static int AddBand(Band band)
        {
            // If _Band is null, create the Observable Collection
            if (_Bands == null) GetBands();

            try
            {
                // Add to db
                DbParameter param1 = Database.AddParameter("@name", band.Name);
                DbParameter param2 = Database.AddParameter("@twitter", band.Twitter);
                DbParameter param3 = Database.AddParameter("@facebook", band.Facebook);
                DbParameter param4 = Database.AddParameter("@picture", band.Picture);
                DbParameter param5 = Database.AddParameter("@description", band.Description);

                DbDataReader reader = Database.GetData("INSERT INTO band(Name, Twitter, Facebook, Picture, Description) VALUES(@name, @twitter, @facebook, @picture, @description); SELECT LAST_INSERT_ID() AS ID;", param1, param2, param3, param4, param5);
                foreach (DbDataRecord record in reader)
                {
                    // Get ID
                    if (DBNull.Value.Equals(record["ID"])) band.ID = -1;
                    else band.ID = Convert.ToInt32(record["ID"]);
                }
                if (reader != null)
                    reader.Close();

                _Bands.Add(band);
                return band.ID;
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return -1;
        }

        public static void UpdateBand(Band band)
        {
            // If _Band is null, create the Observable Collection
            if (_Bands == null) GetBands();

            try
            {
                // Update db
                DbParameter param1 = Database.AddParameter("@id", band.ID);
                DbParameter param2 = Database.AddParameter("@name", band.Name);
                DbParameter param3 = Database.AddParameter("@twitter", band.Twitter);
                DbParameter param4 = Database.AddParameter("@facebook", band.Facebook);
                DbParameter param5 = Database.AddParameter("@picture", band.Picture);
                DbParameter param6 = Database.AddParameter("@description", band.Description);
                int affectedRows = Database.ModifyData("UPDATE band SET Name = @name, Twitter = @twitter, Facebook = @facebook, Picture = @picture, Description = @description WHERE id = @id", param1, param2, param3, param4, param5, param6);
                if (affectedRows == 0) return;

                // Update _Band
                _Bands[GetIndexByID(band.ID)] = band;
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void DeleteBand(Band band)
        {
            // If _Band is null, create the Observable Collection
            if (_Bands == null) GetBands();

            try
            {
                // Add to db
                DbParameter param = Database.AddParameter("@id", band.ID);
                int affectedRows = Database.ModifyData("DELETE FROM band WHERE id = @id", param);
                if (affectedRows == 0) return;

                // Update _Band
                _Bands.RemoveAt(GetIndexByID(band.ID));
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static string GetBandById(int id)
        {
            for (int i = 0; i < _Bands.Count; ++i)
            {
                if (_Bands[i].ID == id) return _Bands[i].Name;
            }
            return "";
        }

        public static int GetIndexByID(int id)
        {
            for (int i = 0; i < _Bands.Count; ++i)
            {
                if (_Bands[i].ID == id) return i;
            }
            return -1;
        }

        public Band Copy()
        {
            return new Band() { ID = this.ID, Name = this.Name, Twitter = this.Twitter, Facebook = this.Facebook, Picture = this.Picture, Description = this.Description };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public string Error
        {
            get { return null; }
        }

        public string this[string columnName]
        {
            get
            {
                try
                {
                    object value = this.GetType().GetProperty(columnName).GetValue(this);
                    Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = columnName });
                }
                catch (ValidationException ex)
                {
                    return ex.Message;
                }
                return String.Empty;
            }
        }

        public bool IsValid()
        {
            return Validator.TryValidateObject(this, new ValidationContext(this, null, null), null, true);
        }
    }
}
