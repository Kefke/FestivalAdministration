﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalAdministration.Models
{
    public class TimeSlot
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

        private Stage _stage;

        public Stage Stage
        {
            get { return _stage; }
            set { _stage = value; }
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

        public string TimeToString()
        {
            return "From " + TimeToString(StartDate) + " till " + TimeToString(EndDate);
        }

        static public string TimeToString(DateTime dt)
        {
            if (dt.Minute < 10) return dt.Hour + ":" + dt.Minute + "0";
            else return dt.Hour + ":" + dt.Minute;
        }

        public string DateToString()
        {
            //if (StartDate.Date == EndDate.Date)
                return DateToString(StartDate);
            //else return "From " + DateToString(StartDate) + " till " + DateToString(EndDate);
        }

        static public string DateToString(DateTime dt)
        {
            string result = dt.DayOfWeek + " " + dt.Day + " ";

            switch (dt.Month)
            {
                case 1:
                    result += "January";
                    break;
                case 2:
                    result += "February";
                    break;
                case 3:
                    result += "March";
                    break;
                case 4:
                    result += "April";
                    break;
                case 5:
                    result += "May";
                    break;
                case 6:
                    result += "June";
                    break;
                case 7:
                    result += "July";
                    break;
                case 8:
                    result += "August";
                    break;
                case 9:
                    result += "September";
                    break;
                case 10:
                    result += "October";
                    break;
                case 11:
                    result += "November";
                    break;
                case 12:
                    result += "December";
                    break;
                default:
                    return "";
            }

            result += " " + dt.Year;

            return result;
        }

        /*private static ObservableCollection<TimeSlot> _TimeSlots = null;

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

        public static int AddTimeSlot(TimeSlot timeSlot)
        {
            // If _TimeSlots is null, create the Observable Collection
            if (_TimeSlots == null) GetTimeSlots();

            try
            {
                // Add to db
                DbParameter param1 = Database.AddParameter("@bandid", timeSlot.BandID);
                DbParameter param2 = Database.AddParameter("@stageid", timeSlot.StageID);
                DbParameter param3 = Database.AddParameter("@startdate", timeSlot.StartDate);
                DbParameter param4 = Database.AddParameter("@enddate", timeSlot.EndDate);
                DbDataReader reader = Database.GetData("INSERT INTO TimeSlot(BandID, StageID, StartDate, EndDate) VALUES(@bandid, @stageid, @startdate, @enddate); SELECT LAST_INSERT_ID() AS ID;", param1, param2, param3, param4);
                foreach (DbDataRecord record in reader)
                {
                    // Get ID
                    if (DBNull.Value.Equals(record["ID"])) timeSlot.ID = -1;
                    else timeSlot.ID = Convert.ToInt32(record["ID"]);
                }

                _TimeSlots.Add(timeSlot);
                return timeSlot.ID;
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return -1;
        }

        public static void UpdateTimeSlot(TimeSlot timeSlot)
        {
            // If _TimeSlots is null, create the Observable Collection
            if (_TimeSlots == null) GetTimeSlots();

            try
            {
                // Update db
                DbParameter param1 = Database.AddParameter("@id", timeSlot.ID);
                DbParameter param2 = Database.AddParameter("@bandid", timeSlot.BandID);
                DbParameter param3 = Database.AddParameter("@stageid", timeSlot.StageID);
                DbParameter param4 = Database.AddParameter("@startdate", timeSlot.StartDate);
                DbParameter param5 = Database.AddParameter("@enddate", timeSlot.EndDate);
                int affectedRows = Database.ModifyData("UPDATE timeslot SET BandID = @bandid, StageID = @stageid, StartDate = @startdate, EndDate = @enddate WHERE id = @id", param1, param2, param3, param4, param5);
                if (affectedRows == 0) return;

                // Update _TimeSlots
                _TimeSlots[GetIndexFromID(timeSlot.ID)] = timeSlot;
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void DeleteTimeSlot(TimeSlot timeSlot)
        {
            // If _TimeSlots is null, create the Observable Collection
            if (_TimeSlots == null) GetTimeSlots();

            try
            {
                // Add to db
                DbParameter param = Database.AddParameter("@id", timeSlot.ID);
                int affectedRows = Database.ModifyData("DELETE FROM TimeSlot WHERE id = @id", param);
                if (affectedRows == 0) return;

                // Update _TimeSlots
                _TimeSlots.RemoveAt(GetIndexFromID(timeSlot.ID));
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static int GetIndexFromID(int id)
        {
            if (_TimeSlots == null) GetTimeSlots();
            for (int i = 0; i < _TimeSlots.Count; ++i)
            {
                if (_TimeSlots[i].ID == id) return i;
            }
            return -1;
        }*/
    }
}
