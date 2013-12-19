using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalAdministration.Model
{
    class TimeSlot
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

        private int _stageID;

        public int StageID
        {
            get { return _stageID; }
            set { _stageID = value; }
        }
        

        private DateTime _startDate;

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        private DateTime _endDate;

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        private static ObservableCollection<TimeSlot> _TimeSlots = null;

        public static ObservableCollection<TimeSlot> GetTimeSlots()
        {
            // If _TimeSlots is null, create the Observable Collection
            if (_TimeSlots == null)
            {
                try
                {
                    // Create _Contactperson
                    _TimeSlots = new ObservableCollection<TimeSlot>();

                    // Get data
                    DbDataReader reader = Database.GetData("SELECT * FROM timeslot");
                    foreach (DbDataRecord record in reader)
                    {
                        // Create new TimeSlot
                        TimeSlot timeslot = new TimeSlot();

                        // Get ID
                        if (DBNull.Value.Equals(record["ID"])) timeslot.ID = -1;
                        else timeslot.ID = Convert.ToInt32(record["ID"]);

                        // Get BandID
                        if (DBNull.Value.Equals(record["BandID"])) timeslot.BandID = -1;
                        else timeslot.BandID = Convert.ToInt32(record["BandID"]);

                        // Get StageID
                        if (DBNull.Value.Equals(record["StageID"])) timeslot.StageID = -1;
                        else timeslot.StageID = Convert.ToInt32(record["StageID"]);

                        // Get StartDate
                        if (DBNull.Value.Equals(record["StartDate"])) timeslot.StartDate = new DateTime();
                        else timeslot.StartDate = Convert.ToDateTime(record["StartDate"]);

                        // Get EndDate
                        if (DBNull.Value.Equals(record["EndDate"])) timeslot.EndDate = new DateTime();
                        else timeslot.EndDate = Convert.ToDateTime(record["EndDate"]);

                        // Add TimeSlot
                        _TimeSlots.Add(timeslot);
                    }
                }

                // Fail
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    // Clear TimeSlots
                    _TimeSlots.Clear();
                    _TimeSlots = null;
                }

            }

            // Return _TimeSlots
            return _TimeSlots;
        }

        public static void AddTimeSlot(int bandid, int stageid, DateTime startdate, DateTime enddate)
        {
            // If _TimeSlots is null, create the Observable Collection
            if (_TimeSlots == null) GetTimeSlots();

            try
            {
                // Add to db
                DbParameter param1 = Database.AddParameter("@bandid", bandid);
                DbParameter param2 = Database.AddParameter("@stageid", stageid);
                DbParameter param3 = Database.AddParameter("@startdate", startdate);
                DbParameter param4 = Database.AddParameter("@enddate", enddate);
                int affectedRows = Database.ModifyData("INSERT INTO TimeSlot(BandID, StageID, StartDate, EndDate) VALUES(@bandid, @stageid, @startdate, @enddate)", param1, param2, param3, param4);
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

                _TimeSlots.Add(new TimeSlot() { ID = id, BandID = bandid, StageID = stageid, StartDate = startdate, EndDate = enddate });
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void UpdateTimeSlot(int index, int bandid, int stageid, DateTime startdate, DateTime enddate)
        {
            // If _TimeSlots is null, create the Observable Collection
            if (_TimeSlots == null) GetTimeSlots();

            try
            {
                // Update db
                DbParameter param1 = Database.AddParameter("@id", _TimeSlots[index].ID);
                DbParameter param2 = Database.AddParameter("@bandid", bandid);
                DbParameter param3 = Database.AddParameter("@stageid", stageid);
                DbParameter param4 = Database.AddParameter("@startdate", startdate);
                DbParameter param5 = Database.AddParameter("@enddate", enddate);
                int affectedRows = Database.ModifyData("UPDATE timeslot SET BandID = @bandid, StageID = @stageid, StartDate = @startdate, EndDate = @enddate WHERE id = @id", param1, param2, param3, param4, param5);
                if (affectedRows == 0) return;

                // Update _TimeSlots
                _TimeSlots[index].BandID = bandid;
                _TimeSlots[index].StageID = stageid;
                _TimeSlots[index].StartDate = startdate;
                _TimeSlots[index].EndDate = enddate;
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void DeleteTimeSlot(int index)
        {
            // If _TimeSlots is null, create the Observable Collection
            if (_TimeSlots == null) GetTimeSlots();

            // Only execute if index is valid
            if (_TimeSlots.Count <= index) return;

            try
            {
                // Add to db
                DbParameter param = Database.AddParameter("@id", _TimeSlots[index].ID);
                int affectedRows = Database.ModifyData("DELETE FROM TimeSlot WHERE id = @id", param);
                if (affectedRows == 0) return;

                // Update _TimeSlots
                _TimeSlots.RemoveAt(index);
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
