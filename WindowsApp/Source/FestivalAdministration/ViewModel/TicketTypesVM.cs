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
    class TicketTypesVM : ObservableObject, IPage
    {
        public TicketTypesVM()
        {
            TicketTypes = TicketType.GetTicketTypes();

            SelectedType = new TicketType();
            Enabled = true;
            ShowEdit = "Hidden";
            ShowCancel = "Hidden";
            ShowSave = "Visible";
        }

        public string Name
        {
            get { return "TicketType Type"; }
        }

        private ObservableCollection<TicketType> _ticketTypes;

        public ObservableCollection<TicketType> TicketTypes
        {
            get { return _ticketTypes; }
            set { _ticketTypes = value; OnPropertyChanged("TicketTypes"); }
        }

        private bool _changeNotify;
        private TicketType _oldType;
        private TicketType _selectedType;

        public TicketType SelectedType
        {
            get { return _selectedType; }
            set
            {
                _selectedType = value;
                OnPropertyChanged("SelectedType");
                if (!_changeNotify)
                {
                    _changeNotify = !_changeNotify;
                    return;
                }
                SelectionChanged(this);
            }
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

        public ICommand AddTicketTypeCommand
        {
            get { return new RelayCommand<TicketTypesVM>(AddTicketType); }
        }

        private void AddTicketType(TicketTypesVM tickettypevm)
        {
            SelectedType = new TicketType();
            _oldType = null;
            tickettypevm.ShowEdit = "Hidden";
            tickettypevm.ShowCancel = "Visible";
            tickettypevm.ShowSave = "Visible";
            tickettypevm.Enabled = true;
        }

        public ICommand EditTicketTypeCommand
        {
            get { return new RelayCommand<TicketTypesVM>(EditTicketType); }
        }

        private void EditTicketType(TicketTypesVM tickettypevm)
        {
            tickettypevm.ShowEdit = "Hidden";
            tickettypevm.ShowCancel = "Visible";
            tickettypevm.ShowSave = "Visible";
            tickettypevm.Enabled = true;
        }

        public ICommand DeleteTicketTypeCommand
        {
            get { return new RelayCommand<TicketTypesVM>(DeleteTicketType); }
        }

        private void DeleteTicketType(TicketTypesVM tickettypevm)
        {
            if (SelectedType == null) return;

            TicketType.DeleteTicketType(SelectedType);

            SelectedType = new TicketType();
            Enabled = true;
            ShowEdit = "Hidden";
            ShowCancel = "Hidden";
            ShowSave = "Visible";
        }

        public ICommand SaveUpdateTicketTypeCommand
        {
            get { return new RelayCommand<TicketTypesVM>(SaveUpdateTicketType); }
        }

        private void SaveUpdateTicketType(TicketTypesVM tickettypevm)
        {
            // Save Changes
            if (_oldType == null)
            {
                // Insert into db
                SelectedType.ID = TicketType.AddTicketType(SelectedType);
            }
            else
            {
                // Update db
                TicketType.UpdateTicketType(SelectedType);
            }

            // Update GUI
            tickettypevm.ShowEdit = "Visible";
            tickettypevm.ShowCancel = "Hidden";
            tickettypevm.ShowSave = "Hidden";
            Enabled = false;
        }

        public ICommand CancelUpdateTicketTypeCommand
        {
            get { return new RelayCommand<TicketTypesVM>(CancelUpdateTicketType); }
        }

        private void CancelUpdateTicketType(TicketTypesVM tickettypevm)
        {
            // Reset person
            _changeNotify = false;
            SelectedType = _oldType;

            tickettypevm.ShowEdit = "Visible";
            tickettypevm.ShowCancel = "Hidden";
            tickettypevm.ShowSave = "Hidden";
            Enabled = false;
        }

        public ICommand SelectionChangedCommand
        {
            get { return new RelayCommand<TicketTypesVM>(SelectionChanged); }
        }

        private void SelectionChanged(TicketTypesVM tickettypevm)
        {
            if (SelectedType == null) return;
            if (ShowCancel == "Visible") CancelUpdateTicketType(this);

            _oldType = SelectedType.Copy();

            tickettypevm.ShowEdit = "Visible";
            tickettypevm.ShowCancel = "Hidden";
            tickettypevm.ShowSave = "Hidden";
            Enabled = false;
        }
        //************************
    }
}
