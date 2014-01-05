using FestivalAdministration.Model;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FestivalAdministration.ViewModel
{
    class TimeSlotVM: ObservableObject, IPage
    {
        public TimeSlotVM()
        {
            Bands = Band.GetBands();
            Stages = Stage.GetStages();
            Timeslots = TimeSlot.GetTimeSlots();

            SelectedSlot = new TimeSlot();

            Hours = new ObservableCollection<int>();
            for (int i = 0; i < 24; ++i)
                Hours.Add(i);

            Minutes = new ObservableCollection<int>();
            for (int i = 0; i < 60; i += 5)
                Minutes.Add(i);

            CancelUpdateSlot(this);

            SelectedSlot = new TimeSlot();
            StartDate = DateTime.Now;

            _oldSlot = null;

            Enabled = true;
            ShowEdit = "Hidden";
            ShowCancel = "Hidden";
            ShowSave = "Visible";
        }

        public string Name
        {
            get { return "Line-up Detail"; }
        }

        private ObservableCollection<Band> _bands;

        public ObservableCollection<Band> Bands
        {
            get { return _bands; }
            set { _bands = value; OnPropertyChanged("Bands"); }
        }

        private ObservableCollection<Stage> _stages;

        public ObservableCollection<Stage> Stages
        {
            get { return _stages; }
            set { _stages = value; OnPropertyChanged("Stages"); }
        }

        private ObservableCollection<TimeSlot> _timeslots;

        public ObservableCollection<TimeSlot> Timeslots
        {
            get { return _timeslots; }
            set { _timeslots = value; OnPropertyChanged("Timeslots"); }
        }

        private bool _changeNotify;
        private TimeSlot _oldSlot;
        private TimeSlot _selectedSlot;

        public TimeSlot SelectedSlot
        {
            get { return _selectedSlot; }
            set {
                _selectedSlot = value;

                // Reset Date
                if (SelectedSlot == null)
                {
                    StartDate = DateTime.Now;
                    EndDate = DateTime.Now;
                } else {
                    StartDate = SelectedSlot.StartDate;
                    EndDate = SelectedSlot.EndDate;
                    Duration = EndDate - StartDate;
                }

                OnPropertyChanged("SelectedSlot");
                if (!_changeNotify)
                {
                    _changeNotify = !_changeNotify;
                    return;
                }
                SelectionChanged(this); 
            }
        }

        private ObservableCollection<int> _hours;

        public ObservableCollection<int> Hours
        {
            get { return _hours; }
            set { _hours = value; }
        }

        private ObservableCollection<int> _minutes;

        public ObservableCollection<int> Minutes
        {
            get { return _minutes; }
            set { _minutes = value; }
        }

        private TimeSpan _duration;

        public TimeSpan Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }

        private DateTime _startDate;

        public DateTime StartDate
        {
            get { return _startDate; }
            set { 
                // Set Start Date
                _startDate = value; 

                // Update Hours and Minutes
                StartHour = StartDate.Hour;
                StartMinute = StartDate.Minute;

                //Update EndDate if StartDate is bigger than EndDate
                if (EndDate <= StartDate)
                    EndDate = StartDate.AddMinutes(50);

                OnPropertyChanged("StartDate"); 
            }
        }

        private DateTime _endDate;

        public DateTime EndDate
        {
            get { return _endDate; }
            set { 
                // Set End Date
                _endDate = value; 

                // Update Hours and Minutes
                EndHour = EndDate.Hour;
                EndMinute = EndDate.Minute;

                // Don't let the EndDate become smaller than the StartDate
                if (EndDate < StartDate)
                    EndDate = StartDate;

                OnPropertyChanged("EndDate"); 
            }
        }

        private int _startHour;

        public int StartHour
        {
            get { return _startHour; }
            set { _startHour = value; OnPropertyChanged("StartHour"); }
        }

        private int _endHour;

        public int EndHour
        {
            get { return _endHour; }
            set { _endHour = value; OnPropertyChanged("EndHour"); }
        }        

        private int _startMinute;

        public int StartMinute
        {
            get { return _startMinute; }
            set { _startMinute = value; OnPropertyChanged("StartMinute"); }
        }

        private int _endMinute;

        public int EndMinute
        {
            get { return _endMinute; }
            set { _endMinute = value; OnPropertyChanged("EndMinute"); }
        }

        private string _showedit;

        public string ShowEdit
        {
            get { return _showedit; }
            set { _showedit = value; OnPropertyChanged("ShowEdit"); }
        }

        private string _ShowCancel;

        public string ShowCancel
        {
            get { return _ShowCancel; }
            set { _ShowCancel = value; OnPropertyChanged("ShowCancel"); }
        }

        private string _ShowSave;

        public string ShowSave
        {
            get { return _ShowSave; }
            set { _ShowSave = value; OnPropertyChanged("ShowSave"); }
        }

        private bool _Enabled;

        public bool Enabled
        {
            get { return _Enabled; }
            set { _Enabled = value; OnPropertyChanged("Enabled"); }
        }
        //************************

        //************************
        // Commands
        public ICommand AddSlotCommand
        {
            get { return new RelayCommand<TimeSlotVM>(AddSlot); }
        }

        private void AddSlot(TimeSlotVM timeslotVM)
        {
            SelectedSlot = new TimeSlot();

            StartDate = DateTime.Now;
            EndDate = StartDate;

            _oldSlot = null;
            timeslotVM.ShowEdit = "Hidden";
            timeslotVM.ShowCancel = "Visible";
            timeslotVM.ShowSave = "Visible";
            timeslotVM.Enabled = true;
        }

        public ICommand EditSlotCommand
        {
            get { return new RelayCommand<TimeSlotVM>(EditSlot); }
        }

        private void EditSlot(TimeSlotVM timeslotVM)
        {
            timeslotVM.ShowEdit = "Hidden";
            timeslotVM.ShowCancel = "Visible";
            timeslotVM.ShowSave = "Visible";
            timeslotVM.Enabled = true;
        }

        public ICommand DeleteSlotCommand
        {
            get { return new RelayCommand<TimeSlotVM>(DeleteSlot); }
        }

        private void DeleteSlot(TimeSlotVM timeslotVM)
        {
            if (SelectedSlot == null) return;

            TimeSlot.DeleteTimeSlot(SelectedSlot);

            SelectedSlot = new TimeSlot();
            Enabled = true;
            ShowEdit = "Hidden";
            ShowCancel = "Hidden";
            ShowSave = "Visible";
        }

        public ICommand SaveUpdateSlotCommand
        {
            get { return new RelayCommand<TimeSlotVM>(SaveUpdateSlot); }
        }

        private void SaveUpdateSlot(TimeSlotVM timeslotVM)
        {
            SelectedSlot.StartDate = new DateTime(StartDate.Year, StartDate.Month, StartDate.Day, StartHour, StartMinute, 0);
            SelectedSlot.EndDate = new DateTime(EndDate.Year, EndDate.Month, EndDate.Day, EndHour, EndMinute, 0);

            // Save Changes
            if (_oldSlot == null)
            {
                // Insert into db
                SelectedSlot.ID = TimeSlot.AddTimeSlot(SelectedSlot);
            }
            else
            {
                // Update db
                TimeSlot.UpdateTimeSlot(SelectedSlot);
            }

            // Update GUI
            timeslotVM.ShowEdit = "Visible";
            timeslotVM.ShowCancel = "Hidden";
            timeslotVM.ShowSave = "Hidden";
            Enabled = false;
        }

        public ICommand CancelUpdateSlotCommand
        {
            get { return new RelayCommand<TimeSlotVM>(CancelUpdateSlot); }
        }

        private void CancelUpdateSlot(TimeSlotVM slotvm)
        {
            // Reset TimeSlot
            _changeNotify = false;
            SelectedSlot = _oldSlot;

            slotvm.ShowEdit = "Visible";
            slotvm.ShowCancel = "Hidden";
            slotvm.ShowSave = "Hidden";
            Enabled = false;
        }

        public ICommand SelectionChangedCommand
        {
            get { return new RelayCommand<TimeSlotVM>(SelectionChanged); }
        }

        private void SelectionChanged(TimeSlotVM slotvm)
        {
            if (SelectedSlot == null) return;
            if (ShowCancel == "Visible") CancelUpdateSlot(this);
            _oldSlot = SelectedSlot.Copy();

            slotvm.ShowEdit = "Visible";
            slotvm.ShowCancel = "Hidden";
            slotvm.ShowSave = "Hidden";
            Enabled = false;
        }
        //************************        
    }
}
