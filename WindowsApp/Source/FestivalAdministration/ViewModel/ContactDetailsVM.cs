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
    class ContactDetailsVM: ObservableObject, IPage
    {
        public ContactDetailsVM()
        {
            Contacts = Contactperson.GetContactpersons();
            ContactTypes = ContactpersonType.GetContactpersonTypes();

            SelectedPerson = new Contactperson();
            Enabled = true;
            ShowEdit = "Hidden";
            ShowCancel = "Hidden";
            ShowSave = "Visible";
        }

        public string Name
        {
            get { return "Contact Detail"; }
        }

        private ObservableCollection<ContactpersonType> _contactTypes;

        public ObservableCollection<ContactpersonType> ContactTypes
        {
            get { return _contactTypes; }
            set { _contactTypes = value; OnPropertyChanged("ContactTypes"); }
        }

        private ObservableCollection<Contactperson> _contacts;

        public ObservableCollection<Contactperson> Contacts
        {
            get { return _contacts; }
            set { _contacts = value; OnPropertyChanged("Contacts"); }
        }

        private bool _changeNotify;
        private Contactperson _oldPerson;
        private Contactperson _selectedPerson;

        public Contactperson SelectedPerson
        {
            get { return _selectedPerson; }
            set { 
                _selectedPerson = value; 
                OnPropertyChanged("SelectedPerson");
                if (!_changeNotify)
                {
                    _changeNotify = !_changeNotify;
                    return;
                }
                SelectionChanged(this); }
        }

        private string _searchName;

        public string SearchName
        {
            get { return _searchName; }
            set { _searchName = value; OnPropertyChanged("SearchName"); }
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

        public ICommand AddContactCommand
        {
            get { return new RelayCommand<ContactDetailsVM>(AddContact); }
        }

        private void AddContact(ContactDetailsVM contactvm)
        {
            SelectedPerson = new Contactperson();
            _oldPerson = null;
            contactvm.ShowEdit = "Hidden";
            contactvm.ShowCancel = "Visible";
            contactvm.ShowSave = "Visible";
            contactvm.Enabled = true;
        }

        public ICommand EditContactCommand
        {
            get { return new RelayCommand<ContactDetailsVM>(EditContact); }
        }

        private void EditContact(ContactDetailsVM contactvm)
        {
            contactvm.ShowEdit = "Hidden";
            contactvm.ShowCancel = "Visible";
            contactvm.ShowSave = "Visible";
            contactvm.Enabled = true;
        }

        public ICommand SearchContactCommand
        {
            get { return new RelayCommand<ContactDetailsVM>(SearchContact); }
        }

        private void SearchContact(ContactDetailsVM contactvm)
        {
            CancelUpdateContact(this);
            Contacts = Contactperson.SearchContactpersons(SearchName);
        }

        public ICommand DeleteContactCommand
        {
            get { return new RelayCommand<ContactDetailsVM>(DeleteContact); }
        }

        private void DeleteContact(ContactDetailsVM contactvm)
        {
            if (SelectedPerson == null) return;

            Contactperson.DeleteContactperson(SelectedPerson);

            SelectedPerson = new Contactperson();
            Enabled = true;
            ShowEdit = "Hidden";
            ShowCancel = "Hidden";
            ShowSave = "Visible";
        }

        public ICommand SaveUpdateContactCommand
        {
            get { return new RelayCommand<ContactDetailsVM>(SaveUpdateContact); }
        }

        private void SaveUpdateContact(ContactDetailsVM contactvm)
        {
            // Save Changes
            if (_oldPerson == null)
            {
                // Insert into db
                SelectedPerson.ID = Contactperson.AddContactperson(SelectedPerson);
            }
            else
            {
                // Update db
                Contactperson.UpdateContactperson(SelectedPerson);
            }

            // Update GUI
            contactvm.ShowEdit = "Visible";
            contactvm.ShowCancel = "Hidden";
            contactvm.ShowSave = "Hidden";
            Enabled = false;
        }

        public ICommand CancelUpdateContactCommand
        {
            get { return new RelayCommand<ContactDetailsVM>(CancelUpdateContact); }
        }

        private void CancelUpdateContact(ContactDetailsVM contactvm)
        {
            // Reset person
            _changeNotify = false;
            SelectedPerson = _oldPerson;

            contactvm.ShowEdit = "Visible";
            contactvm.ShowCancel = "Hidden";
            contactvm.ShowSave = "Hidden";
            Enabled = false;
        }

        public ICommand SelectionChangedCommand
        {
            get { return new RelayCommand<ContactDetailsVM>(SelectionChanged); }
        }

        private void SelectionChanged(ContactDetailsVM contactvm)
        {
            if (SelectedPerson == null) return;
            if (ShowCancel == "Visible") CancelUpdateContact(this);

            _oldPerson = SelectedPerson.Copy();

            contactvm.ShowEdit = "Visible";
            contactvm.ShowCancel = "Hidden";
            contactvm.ShowSave = "Hidden";
            Enabled = false;
        }
        //************************
    }
}
