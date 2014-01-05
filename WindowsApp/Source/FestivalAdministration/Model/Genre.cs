using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalAdministration.Model
{
    class Genre
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

        private static ObservableCollection<Genre> _genres = null;

        public static ObservableCollection<Genre> GetGenres()
        {
            // If _genres is null, create the Observable Collection
            if (_genres == null)
            {
                try
                {
                    // Create _genres
                    _genres = new ObservableCollection<Genre>();

                    // Get data
                    DbDataReader reader = Database.GetData("SELECT * FROM genre");
                    foreach (DbDataRecord record in reader)
                    {
                        // Create new Genre
                        Genre genre = new Genre();

                        // Get ID
                        if (DBNull.Value.Equals(record["ID"])) genre.ID = -1;
                        else genre.ID = Convert.ToInt32(record["ID"]);

                        // Get Name
                        if (DBNull.Value.Equals(record["Name"])) genre.Name = "";
                        else genre.Name = record["Name"].ToString();

                        // Add Genre
                        _genres.Add(genre);
                    }
                    if (reader != null)
                        reader.Close();
                }

                // Fail
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    // Clear Genres
                    _genres.Clear();
                    _genres = null;
                }

            }

            // Return _genres
            return _genres;
        }

        public static int AddGenre(Genre genre)
        {
            // If _Genre is null, create the Observable Collection
            if (_genres == null) GetGenres();

            try
            {
                // Add to db
                DbParameter param = Database.AddParameter("@name", genre.Name);
                DbDataReader reader = Database.GetData("INSERT INTO genre(name) VALUES(@name); SELECT LAST_INSERT_ID() AS ID;", param);
                foreach (DbDataRecord record in reader)
                {
                    // Get ID
                    if (DBNull.Value.Equals(record["ID"])) genre.ID = -1;
                    else genre.ID = Convert.ToInt32(record["ID"]);
                }
                if (reader != null)
                    reader.Close();

                _genres.Add(genre);
                return genre.ID;
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return -1;
        }

        public static void UpdateGenre(Genre genre)
        {
            // If _Genre is null, create the Observable Collection
            if (_genres == null) GetGenres();

            try
            {
                // Update db
                DbParameter param1 = Database.AddParameter("@id", genre.ID);
                DbParameter param2 = Database.AddParameter("@name", genre.Name);
                int affectedRows = Database.ModifyData("UPDATE genre SET name = @name WHERE id = @id", param1, param2);
                if (affectedRows == 0) return;

                // Update _genres
                _genres[GetIndexByID(genre.ID)] = genre;
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void DeleteGenre(Genre genre)
        {
            // If _Genre is null, create the Observable Collection
            if (_genres == null) GetGenres();

            try
            {
                // Add to db
                DbParameter param = Database.AddParameter("@id", genre.ID);
                int affectedRows = Database.ModifyData("DELETE FROM genre WHERE id = @id", param);
                if (affectedRows == 0) return;

                // Update _genres
                _genres.RemoveAt(GetIndexByID(genre.ID));
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static string GetTypeByID(int id)
        {
            // If _Genre is null, create the Observable Collection
            if (_genres == null) GetGenres();

            foreach (Genre type in _genres)
            {
                if (type.ID == id)
                    return type.Name;
            }

            return "";
        }

        public static int GetIndexByID(int id)
        {
            // If _Genre is null, create the Observable Collection
            if (_genres == null) GetGenres();

            for (int i = 0; i < _genres.Count; ++i)
            {
                if (_genres[i].ID == id)
                    return i;
            }

            return -1;
        }

        public Genre Copy()
        {
            return new Genre() { ID = this.ID, Name = this.Name };
        }
    }
}
