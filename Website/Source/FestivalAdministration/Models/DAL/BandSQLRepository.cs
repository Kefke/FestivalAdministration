using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace FestivalAdministration.Models.DAL
{
    public class BandSQLRepository
    {
        public static List<Band> GetBands()
        {
            List<Band> bands = new List<Band>();

            try
            {
                // Get data
                DbDataReader reader = Database.GetData("SELECT * FROM band");
                foreach (DbDataRecord record in reader)
                {
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

                    // Get Genres
                    band.Genres = GenreSQLRepository.GetGenresFromBand(band.ID);

                    //Get Timeslots
                    band.TimeSlots = TimeSlotSQLRepository.GetTimeSlotFromBand(band.ID);

                    bands.Add(band);
                }
                
                return bands;
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public static Band GetBand(int ID)
        {
            try
            {
                // Get data
                DbParameter param = Database.AddParameter("@id", ID);
                DbDataReader reader = Database.GetData("SELECT * FROM band WHERE ID = @id", param);
                foreach (DbDataRecord record in reader)
                {
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

                    // Get Genres
                    band.Genres = GenreSQLRepository.GetGenresFromBand(ID);

                    //Get Timeslots
                    band.TimeSlots = TimeSlotSQLRepository.GetTimeSlotFromBand(band.ID);

                    return band;
                }
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