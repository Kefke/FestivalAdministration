using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace FestivalAdministration.Models.DAL
{
    public class GenreSQLRepository
    {
        public static List<Genre> GetGenresFromBand(int bandID)
        {
            List<Genre> genres = new List<Genre>();
            try
            {
                // Get data
                DbParameter param = Database.AddParameter("@bandid", bandID);
                DbDataReader reader = Database.GetData("SELECT genre.ID, genre.Name FROM genre INNER JOIN bandgenre ON genre.ID = bandgenre.GenreID WHERE bandgenre.BandID = @bandID", param);
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
                    genres.Add(genre);
                }
                reader.Close();
                return genres;
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }
    }
}