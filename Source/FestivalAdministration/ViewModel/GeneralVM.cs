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
    class GeneralVM : ObservableObject, IPage
    {
        public GeneralVM()
        {
            if (Festival.GetFestivals() != null)
            {
                if (Festival.GetFestivals().Count > 0)
                {
                    Festival = Festival.GetFestivals()[0];
                    _OldFestival = new Festival() {ID = Festival.ID, Name = Festival.Name, Street = Festival.Street, City = Festival.City};

                    // There's data to display, so disable the textboxes and show edit button
                    ShowEdit = "Visible";
                    ShowCancel = "Hidden";
                    ShowSave = "Hidden";
                    Enabled = false;

                    return;
                }
            }

            _OldFestival = null;
            // Else enable the textboxes and show the save button 
            ShowEdit = "Hidden";
            ShowCancel = "Hidden";
            ShowSave = "Visible";
            Enabled = true;
        }

        //************************
        // Properties
        public string Name
        {
            get { return "General"; }
        }

        private Festival _Festival;
        private Festival _OldFestival;

        public Festival Festival
        {
            get { return _Festival; }
            set { _Festival = value; OnPropertyChanged("Festival"); }
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
        public ICommand EditFestivalCommand
        {
            get { return new RelayCommand<GeneralVM>(EditFestival); }
        }

        private void EditFestival(GeneralVM generalvm)
        {
            generalvm.ShowEdit = "Hidden";
            generalvm.ShowCancel = "Visible";
            generalvm.ShowSave = "Visible";
            generalvm.Enabled = true;
        }

        public ICommand SaveUpdateFestivalCommand
        {
            get { return new RelayCommand<GeneralVM>(SaveUpdateFestival); }
        }

        private void SaveUpdateFestival(GeneralVM generalvm)
        {
            // Save Changes
            if (_OldFestival == null)
            {
                // Insert into db
                Festival.AddFestival(Festival);
                if (Festival.GetFestivals() != null)
                {
                    if (Festival.GetFestivals().Count > 0)
                    {
                        Festival = Festival.GetFestivals()[0];
                        _OldFestival = Festival;
                    }
                }
            } else {
                // Update db
                Festival.UpdateFestival(Festival);
                _OldFestival = new Festival() { ID = Festival.ID, Name = Festival.Name, Street = Festival.Street, City = Festival.City };
            }
            // Update GUI
            generalvm.ShowEdit = "Visible";
            generalvm.ShowCancel = "Hidden";
            generalvm.ShowSave = "Hidden";
            Enabled = false;
        }

        public ICommand CancelUpdateFestivalCommand
        {
            get { return new RelayCommand<GeneralVM>(CancelUpdateFestival); }
        }

        private void CancelUpdateFestival(GeneralVM generalvm)
        {
            // Reset festival
            Festival = _OldFestival;//new Festival() { ID = _OldFestival.ID, Name = _OldFestival.Name, Street = _OldFestival.Street, City = _OldFestival.City};

            generalvm.ShowEdit = "Visible";
            generalvm.ShowCancel = "Hidden";
            generalvm.ShowSave = "Hidden";
            Enabled = false;
        }
        //************************
    }
}
