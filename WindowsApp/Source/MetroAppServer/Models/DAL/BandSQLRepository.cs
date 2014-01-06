using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace MetroAppServer.Models.DAL
{
    public class BandSQLRepository
    {
        public static List<Band> GetBandsOrderTimeSlot()
        {
            try
            {
                List<Band> bands = new List<Band>();

                // Get data
                DbDataReader reader = Database.GetData("SELECT b.ID, b.Name, b.Picture, ts.ID AS TimeSlotID, ts.BandID, ts.StageID, ts.StartDate, ts.EndDate, s.ID AS Stage_ID, s.Name AS StageName FROM band AS b INNER JOIN timeslot AS ts ON b.ID = ts.bandID INNER JOIN stage AS s ON ts.stageID = s.ID ORDER BY StageName DESC, Name DESC, StartDate DESC");
                
                foreach (DbDataRecord record in reader)
                {
                    Band band = new Band();

                    // Get ID
                    if (DBNull.Value.Equals(record["ID"])) band.ID = -1;
                    else band.ID = Convert.ToInt32(record["ID"]);

                    // Get Name
                    if (DBNull.Value.Equals(record["Name"])) band.Name = "";
                    else band.Name = record["Name"].ToString();

                    // Get Picture
                    if (DBNull.Value.Equals(record["Picture"])) band.Picture = null;
                    else band.Picture = (byte[])record["Picture"];

                    // Get Genres
                    band.Genres = GenreSQLRepository.GetGenresFromBand(band.ID);

                    //Get Timeslot

                    TimeSlot ts = new TimeSlot();

                    // Get TimeSlotID
                    if (DBNull.Value.Equals(record["TimeSlotID"])) ts.ID = -1;
                    else ts.ID = Convert.ToInt32(record["TimeSlotID"]);

                    // Get BandID
                    if (DBNull.Value.Equals(record["BandID"])) ts.BandID = -1;
                    else ts.BandID = Convert.ToInt32(record["BandID"]);

                    // Get StageID
                    if (DBNull.Value.Equals(record["StageID"])) ts.StageID = -1;
                    else ts.StageID = Convert.ToInt32(record["StageID"]);

                    // Get StartDate
                    if (DBNull.Value.Equals(record["StartDate"])) ts.StartDate = new DateTime();
                    else ts.StartDate = Convert.ToDateTime(record["StartDate"]);

                    // Get EndDate
                    if (DBNull.Value.Equals(record["EndDate"])) ts.EndDate = new DateTime();
                    else ts.EndDate = Convert.ToDateTime(record["EndDate"]);

                    // Get Stage
                    Stage s = new Stage();

                    // Get Stage_ID
                    if (DBNull.Value.Equals(record["Stage_ID"])) s.ID = -1;
                    else s.ID = Convert.ToInt32(record["Stage_ID"]);

                    // Get Name
                    if (DBNull.Value.Equals(record["StageName"])) s.Name = "";
                    else s.Name = record["StageName"].ToString();

                    ts.Stage = s;

                    band.TimeSlots = new List<TimeSlot>();
                    band.TimeSlots.Add(ts);

                    bands.Add(band);
                }
                reader.Close();

                return bands;
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public static List<Band> GetBandsOrderedByGenre()
        {
            try
            {
                List<Band> bands = new List<Band>();
                // Get data
                DbDataReader reader = Database.GetData("SELECT b.ID, b.Name, b.Picture, g.ID AS GenreID, g.Name AS GenreName FROM band AS b INNER JOIN bandgenre AS bg ON b.ID = bg.bandID INNER JOIN genre AS g ON bg.genreID = g.ID ORDER BY g.Name ASC, b.Name ASC");
                
                foreach (DbDataRecord record in reader)
                {
                    Band band = new Band();

                    // Get ID
                    if (DBNull.Value.Equals(record["ID"])) band.ID = -1;
                    else band.ID = Convert.ToInt32(record["ID"]);

                    // Get Name
                    if (DBNull.Value.Equals(record["Name"])) band.Name = "";
                    else band.Name = record["Name"].ToString();

                    // Get Picture
                    if (DBNull.Value.Equals(record["Picture"])) band.Picture = null;
                    else band.Picture = (byte[])record["Picture"];

                    // Get Genres
                    Genre genre = new Genre();
                    if (DBNull.Value.Equals(record["GenreID"])) genre.ID = -1;
                    else genre.ID = (int)record["GenreID"];

                    if (DBNull.Value.Equals(record["GenreName"])) genre.Name = "";
                    else genre.Name = record["GenreName"].ToString();

                    band.Genres = new List<Genre>() { genre };

                    bands.Add(band);
                }
                reader.Close();

                return bands;
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