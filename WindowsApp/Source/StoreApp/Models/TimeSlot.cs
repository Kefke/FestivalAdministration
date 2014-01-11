using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Models
{
    public class TimeSlot: StoreApp.Common.BindableBase
    {
        private int _ID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; OnPropertyChanged("ID"); }
        }

        private Band _band;

        public Band Band
        {
            get { return _band; }
            set { _band = value; OnPropertyChanged("Band"); }
        }

        private Stage _stage;

        public Stage Stage
        {
            get { return _stage; }
            set { _stage = value; OnPropertyChanged("Stage"); }
        }
        
        private DateTime _startDate;

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; OnPropertyChanged("StartDate"); }
        }

        private DateTime _endDate;

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; OnPropertyChanged("EndDate"); }
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
            return DateToString(StartDate);
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
    }
}
