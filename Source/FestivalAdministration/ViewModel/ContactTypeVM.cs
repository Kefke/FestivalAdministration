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
    class ContactTypeVM: ObservableObject, IPage
    {
        public ContactTypeVM()
        {
            ContactTypes = ContactpersonType.GetContactpersonTypes();

            SelectedType = new ContactpersonType();
            Enabled = true;
            ShowEdit = "Hidden";
            ShowCancel = "Hidden";
            ShowSave = "Visible";
        }

        //************************
        // Parameters

        public string Name
        {
            get { return "Contact Types"; }
        }
        
        private ObservableCollection<ContactpersonType> _contactTypes;

        public ObservableCollection<ContactpersonType> ContactTypes
        {
            get { return _contactTypes; }
            set { _contactTypes = value; OnPropertyChanged("ContactTypes"); }
        }

        private bool _changeNotify;
        private ContactpersonType _oldType;
        private ContactpersonType _selectedType;

        public ContactpersonType SelectedType
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

        public ICommand AddContactTypeCommand
        {
            get { return new RelayCommand<ContactTypeVM>(AddContactType); }
        }

        private void AddContactType(ContactTypeVM contacttypevm)
        {
            SelectedType = new ContactpersonType();
            _oldType = null;
            contacttypevm.ShowEdit = "Hidden";
            contacttypevm.ShowCancel = "Visible";
            contacttypevm.ShowSave = "Visible";
            contacttypevm.Enabled = true;
        }

        public ICommand EditContactTypeCommand
        {
            get { return new RelayCommand<ContactTypeVM>(EditContactType); }
        }

        private void EditContactType(ContactTypeVM contacttypevm)
        {
            contacttypevm.ShowEdit = "Hidden";
            contacttypevm.ShowCancel = "Visible";
            contacttypevm.ShowSave = "Visible";
            contacttypevm.Enabled = true;
        }

        public ICommand DeleteContactTypeCommand
        {
            get { return new RelayCommand<ContactTypeVM>(DeleteContactType); }
        }

        private void DeleteContactType(ContactTypeVM contacttypevm)
        {
            if (SelectedType == null) return;

            ContactpersonType.DeleteContactpersonType(SelectedType);

            SelectedType = new ContactpersonType();
            Enabled = true;
            ShowEdit = "Hidden";
            ShowCancel = "Hidden";
            ShowSave = "Visible";
        }

        public ICommand SaveUpdateContactTypeCommand
        {
            get { return new RelayCommand<ContactTypeVM>(SaveUpdateContactType); }
        }

        private void SaveUpdateContactType(ContactTypeVM contacttypevm)
        {
            // Save Changes
            if (_oldType == null)
            {
                // Insert into db
                SelectedType.ID = ContactpersonType.AddContactpersonType(SelectedType);
            }
            else
            {
                // Update db
                ContactpersonType.UpdateContactpersonType(SelectedType);
            }

            // Update GUI
            contacttypevm.ShowEdit = "Visible";
            contacttypevm.ShowCancel = "Hidden";
            contacttypevm.ShowSave = "Hidden";
            Enabled = false;
        }

        public ICommand CancelUpdateContactTypeCommand
        {
            get { return new RelayCommand<ContactTypeVM>(CancelUpdateContactType); }
        }

        private void CancelUpdateContactType(ContactTypeVM contacttypevm)
        {
            // Reset person
            _changeNotify = false;
            SelectedType = _oldType;

            contacttypevm.ShowEdit = "Visible";
            contacttypevm.ShowCancel = "Hidden";
            contacttypevm.ShowSave = "Hidden";
            Enabled = false;
        }

        public ICommand SelectionChangedCommand
        {
            get { return new RelayCommand<ContactTypeVM>(SelectionChanged); }
        }

        private void SelectionChanged(ContactTypeVM contacttypevm)
        {
            if (SelectedType == null) return;
            if (ShowCancel == "Visible") CancelUpdateContactType(this);
            _oldType = SelectedType.Copy();

            contacttypevm.ShowEdit = "Visible";
            contacttypevm.ShowCancel = "Hidden";
            contacttypevm.ShowSave = "Hidden";
            Enabled = false;
        }
        //************************
    }
}
