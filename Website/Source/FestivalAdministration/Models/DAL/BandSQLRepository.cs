using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace FestivalAdministration.Models.DAL
{
    public class BandSQLRepository
    {
        public static List<Band> GetBandsOrderName(bool asc)
        {
            List<Band> bands = new List<Band>();

            try
            {
                // Get data
                DbDataReader reader;
                if (asc) reader = Database.GetData("SELECT * FROM band ORDER BY Name ASC");
                else reader = Database.GetData("SELECT * FROM band ORDER BY Name DESC");
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

        public static List<Band> GetBandsOrderTimeSlot(bool stage, bool asc)
        {
            List<Band> bands = new List<Band>();

            if (asc)
            {
                GetBandsWithTimeSlot(ref bands, stage, asc);
                GetBandsWithoutTimeSlot(ref bands, asc);
            } else {
                GetBandsWithoutTimeSlot(ref bands, asc);
                GetBandsWithTimeSlot(ref bands, stage, asc);
            }

            return bands;
        }

        private static void GetBandsWithTimeSlot(ref List<Band> bands, bool stage, bool asc)
        {
            try
            {
                // Get data
                DbDataReader reader;
                if (stage)
                {
                    if (asc) reader = Database.GetData("SELECT b.ID, b.Name, b.Twitter, b.Facebook, b.Picture, b.Description, ts.ID AS TimeSlotID, ts.BandID, ts.StageID, ts.StartDate, ts.EndDate, s.ID AS Stage_ID, s.Name AS StageName FROM band AS b INNER JOIN timeslot AS ts ON b.ID = ts.bandID INNER JOIN stage AS s ON ts.stageID = s.ID ORDER BY StageName ASC, Name ASC, StartDate ASC");
                    else reader = Database.GetData("SELECT b.ID, b.Name, b.Twitter, b.Facebook, b.Picture, b.Description, ts.ID AS TimeSlotID, ts.BandID, ts.StageID, ts.StartDate, ts.EndDate, s.ID AS Stage_ID, s.Name AS StageName FROM band AS b INNER JOIN timeslot AS ts ON b.ID = ts.bandID INNER JOIN stage AS s ON ts.stageID = s.ID ORDER BY StageName DESC, Name DESC, StartDate DESC");
                } else {
                    if (asc) reader = Database.GetData("SELECT b.ID, b.Name, b.Twitter, b.Facebook, b.Picture, b.Description, ts.ID AS TimeSlotID, ts.BandID, ts.StageID, ts.StartDate, ts.EndDate, s.ID AS Stage_ID, s.Name AS StageName FROM band AS b INNER JOIN timeslot AS ts ON b.ID = ts.bandID INNER JOIN stage AS s ON ts.stageID = s.ID ORDER BY StartDate ASC, Name ASC, StageName ASC");
                    else reader = Database.GetData("SELECT b.ID, b.Name, b.Twitter, b.Facebook, b.Picture, b.Description, ts.ID AS TimeSlotID, ts.BandID, ts.StageID, ts.StartDate, ts.EndDate, s.ID AS Stage_ID, s.Name AS StageName FROM band AS b INNER JOIN timeslot AS ts ON b.ID = ts.bandID INNER JOIN stage AS s ON ts.stageID = s.ID ORDER BY StartDate DESC, Name DESC, StageName DESC");
                }
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
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void GetBandsWithoutTimeSlot(ref List<Band> bands, bool asc)
        {
            try
            {
                // Get data
                DbDataReader reader;
                if (asc) reader = Database.GetData("SELECT b.ID, b.Name, b.Twitter, b.Facebook, b.Picture, b.Description FROM band AS b LEFT OUTER JOIN timeslot AS ts ON b.ID = ts.bandID WHERE ts.bandID IS NULL ORDER BY Name ASC");
                else reader = Database.GetData("SELECT b.ID, b.Name, b.Twitter, b.Facebook, b.Picture, b.Description FROM band AS b LEFT OUTER JOIN timeslot AS ts ON b.ID = ts.bandID WHERE ts.bandID IS NULL ORDER BY Name DESC");
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
                    band.TimeSlots = null;

                    bands.Add(band);
                }
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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