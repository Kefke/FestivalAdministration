using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace FestivalAdministration.Models.DAL
{
    public class TimeSlotSQLRepository
    {
        public static List<TimeSlot> GetTimeSlotFromBand(int bandID)
        {
            List<TimeSlot> times = new List<TimeSlot>();
            try
            {
                // Get data
                DbParameter param = Database.AddParameter("@bandid", bandID);
                DbDataReader reader = Database.GetData("SELECT * FROM timeslot WHERE BandID = @bandID ORDER BY startdate", param);
                foreach (DbDataRecord record in reader)
                {
                    // Create new TimeSlot
                    TimeSlot time = new TimeSlot();

                    // Get ID
                    if (DBNull.Value.Equals(record["ID"])) time.ID = -1;
                    else time.ID = Convert.ToInt32(record["ID"]);

                    // Get BandID
                    if (DBNull.Value.Equals(record["BandID"])) time.BandID = -1;
                    else time.BandID = Convert.ToInt32(record["BandID"]);

                    // Get StageID
                    if (DBNull.Value.Equals(record["StageID"])) time.StageID = -1;
                    else time.StageID = Convert.ToInt32(record["StageID"]);

                    // Get StartDate
                    if (DBNull.Value.Equals(record["StartDate"])) time.StartDate = new DateTime();
                    else time.StartDate = Convert.ToDateTime(record["StartDate"]);

                    // Get EndDate
                    if (DBNull.Value.Equals(record["EndDate"])) time.EndDate = new DateTime();
                    else time.EndDate = Convert.ToDateTime(record["EndDate"]);

                    time.Stage = StageSQLRepository.GetStage(time.StageID);

                    // Add TimeSlot
                    times.Add(time);
                }
                reader.Close();
                return times;
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