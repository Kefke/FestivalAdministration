using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalAdministration.Model
{
    class BandGenre
    {
        private int _ID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private int _bandID;

        public int BandID
        {
            get { return _bandID; }
            set { _bandID = value; }
        }

        private int _genreID;

        public int GenreID
        {
            get { return _genreID; }
            set { _genreID = value; }
        }

        public static ObservableCollection<BandGenre> GetBandGenres(int bandID)
        {
            try
            {
                // Create _bandgenres
                ObservableCollection<BandGenre>  result = new ObservableCollection<BandGenre>();

                // Get data
                DbParameter param = Database.AddParameter("@bandid", bandID);
                DbDataReader reader = Database.GetData("SELECT * FROM bandgenre WHERE BandID = @bandid;", param);
                foreach (DbDataRecord record in reader)
                {
                    // Create new BandGenre
                    BandGenre bandgenre = new BandGenre();

                    // Get ID
                    if (DBNull.Value.Equals(record["ID"])) bandgenre.ID = -1;
                    else bandgenre.ID = Convert.ToInt32(record["ID"]);

                    // Get BandID
                    if (DBNull.Value.Equals(record["BandID"])) bandgenre.BandID = -1;
                    else bandgenre.BandID = Convert.ToInt32(record["BandID"]);

                    // Get GenreID
                    if (DBNull.Value.Equals(record["GenreID"])) bandgenre.GenreID = -1;
                    else bandgenre.GenreID = Convert.ToInt32(record["GenreID"]);

                    // Add BandGenre
                    result.Add(bandgenre);
                }
                return result;
            }

                // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return new ObservableCollection<BandGenre>();
            }
        }

        public static int AddBandGenre(BandGenre bandgenre)
        {
            try
            {
                // Add to db
                DbParameter param1 = Database.AddParameter("@bandid", bandgenre.BandID);
                DbParameter param2 = Database.AddParameter("@genreid", bandgenre.GenreID);
                DbDataReader reader = Database.GetData("INSERT INTO bandgenre(BandID, GenreID) VALUES(@bandid, @genreid); SELECT LAST_INSERT_ID() AS ID;", param1, param2);
                foreach (DbDataRecord record in reader)
                {
                    // Get ID
                    if (DBNull.Value.Equals(record["ID"])) bandgenre.ID = -1;
                    else bandgenre.ID = Convert.ToInt32(record["ID"]);
                }
                return bandgenre.ID;
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return -1;
        }

        public static void DeleteBandGenre(BandGenre bandGenre)
        {
           try
            {
                // Add to db
                DbParameter param = Database.AddParameter("@id", bandGenre.ID);
                int affectedRows = Database.ModifyData("DELETE FROM bandgenre WHERE id = @id", param);
                if (affectedRows == 0) return;
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /*private static ObservableCollection<BandGenre> _bandgenres = null;

        public static ObservableCollection<BandGenre> GetBandGenres()
        {
            // If _bandgenres is null, create the Observable Collection
            if (_bandgenres == null)
            {
                try
                {
                    // Create _bandgenres
                    _bandgenres = new ObservableCollection<BandGenre>();

                    // Get data
                    DbDataReader reader = Database.GetData("SELECT * FROM bandgenre");
                    foreach (DbDataRecord record in reader)
                    {
                        // Create new BandGenre
                        BandGenre bandgenre = new BandGenre();

                        // Get ID
                        if (DBNull.Value.Equals(record["ID"])) bandgenre.ID = -1;
                        else bandgenre.ID = Convert.ToInt32(record["ID"]);

                        // Get BandID
                        if (DBNull.Value.Equals(record["BandID"])) bandgenre.BandID = -1;
                        else bandgenre.BandID = Convert.ToInt32(record["BandID"]);

                        // Get GenreID
                        if (DBNull.Value.Equals(record["GenreID"])) bandgenre.GenreID = -1;
                        else bandgenre.GenreID = Convert.ToInt32(record["GenreID"]);

                        // Add BandGenre
                        _bandgenres.Add(bandgenre);
                    }
                }

                // Fail
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    // Clear BandGenres
                    _bandgenres.Clear();
                    _bandgenres = null;
                }

            }

            // Return _bandgenres
            return _bandgenres;
        }

        public static void AddBandGenre(int bandID, int genreID)
        {
            // If _BandGenre is null, create the Observable Collection
            if (_bandgenres == null) GetBandGenres();

            try
            {
                // Add to db
                DbParameter param1 = Database.AddParameter("@bandid", bandID);
                DbParameter param2 = Database.AddParameter("@genreid", genreID);
                int affectedRows = Database.ModifyData("INSERT INTO bandgenre(BandID, GenreID) VALUES(@bandid, @genreid)", param1, param2);
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

                _bandgenres.Add(new BandGenre() { ID = id, BandID = bandID, GenreID = genreID});
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void UpdateBandGenre(int index, int bandID, int genreID)
        {
            // If _BandGenre is null, create the Observable Collection
            if (_bandgenres == null) GetBandGenres();

            try
            {
                // Update db
                DbParameter param1 = Database.AddParameter("@id", _bandgenres[index].ID);
                DbParameter param2 = Database.AddParameter("@bandid", bandID);
                DbParameter param3 = Database.AddParameter("@genreid", genreID);
                int affectedRows = Database.ModifyData("UPDATE bandgenre SET BandID = @bandid, GenreID = @genreid WHERE id = @id", param1, param2, param3);
                if (affectedRows == 0) return;

                // Update _bandgenres
                _bandgenres[index].BandID = bandID;
                _bandgenres[index].GenreID = genreID;
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void DeleteBandGenre(int index)
        {
            // If _BandGenre is null, create the Observable Collection
            if (_bandgenres == null) GetBandGenres();

            // Only execute if index is valid
            if (_bandgenres.Count <= index) return;

            try
            {
                // Add to db
                DbParameter param = Database.AddParameter("@id", _bandgenres[index].ID);
                int affectedRows = Database.ModifyData("DELETE FROM bandgenre WHERE id = @id", param);
                if (affectedRows == 0) return;

                // Update _bandgenres
                _bandgenres.RemoveAt(index);
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        */
    }
}
