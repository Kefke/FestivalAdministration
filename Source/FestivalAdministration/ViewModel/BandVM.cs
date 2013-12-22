using FestivalAdministration.Converter;
using FestivalAdministration.Model;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FestivalAdministration.ViewModel
{
    class BandVM: ObservableObject, IPage
    {
        public BandVM()
        {
            Bands = Band.GetBands();

            SelectedBand = new Band();
            Enabled = true;
            ShowEdit = "Hidden";
            ShowCancel = "Hidden";
            ShowSave = "Visible";
        }

        //************************
        // Parameters

        public string Name
        {
            get { return "Band Detail"; }
        }

        private ObservableCollection<Band> _bands;

        public ObservableCollection<Band> Bands
        {
            get { return _bands; }
            set { _bands = value; OnPropertyChanged("ContactTypes"); }
        }

        private bool _changeNotify;
        private Band _oldBand;
        private Band _selectedBand;

        public Band SelectedBand
        {
            get { return _selectedBand; }
            set
            {
                _selectedBand = value;
                OnPropertyChanged("SelectedBand");
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

        public ICommand AddBandCommand
        {
            get { return new RelayCommand<BandVM>(AddBand); }
        }

        private void AddBand(BandVM bandvm)
        {
            SelectedBand = new Band();
            _oldBand = null;
            bandvm.ShowEdit = "Hidden";
            bandvm.ShowCancel = "Visible";
            bandvm.ShowSave = "Visible";
            bandvm.Enabled = true;
        }

        public ICommand EditBandCommand
        {
            get { return new RelayCommand<BandVM>(EditBand); }
        }

        private void EditBand(BandVM bandvm)
        {
            bandvm.ShowEdit = "Hidden";
            bandvm.ShowCancel = "Visible";
            bandvm.ShowSave = "Visible";
            bandvm.Enabled = true;
        }

        public ICommand DeleteBandCommand
        {
            get { return new RelayCommand<BandVM>(DeleteBand); }
        }

        private void DeleteBand(BandVM bandvm)
        {
            if (SelectedBand == null) return;

            Band.DeleteBand(SelectedBand);

            SelectedBand = new Band();
            Enabled = true;
            ShowEdit = "Hidden";
            ShowCancel = "Hidden";
            ShowSave = "Visible";
        }

        public ICommand SaveUpdateBandCommand
        {
            get { return new RelayCommand<BandVM>(SaveUpdateBand); }
        }

        private void SaveUpdateBand(BandVM bandvm)
        {
            // Save Changes
            if (_oldBand == null)
            {
                // Insert into db
                SelectedBand.ID = Band.AddBand(SelectedBand);
            }
            else
            {
                // Update db
                Band.UpdateBand(SelectedBand);
            }

            // Update GUI
            bandvm.ShowEdit = "Visible";
            bandvm.ShowCancel = "Hidden";
            bandvm.ShowSave = "Hidden";
            Enabled = false;
        }

        public ICommand CancelUpdateBandCommand
        {
            get { return new RelayCommand<BandVM>(CancelUpdateBand); }
        }

        private void CancelUpdateBand(BandVM bandvm)
        {
            // Reset person
            _changeNotify = false;
            SelectedBand = _oldBand;

            bandvm.ShowEdit = "Visible";
            bandvm.ShowCancel = "Hidden";
            bandvm.ShowSave = "Hidden";
            Enabled = false;
        }

        public ICommand SelectionChangedCommand
        {
            get { return new RelayCommand<BandVM>(SelectionChanged); }
        }

        private void SelectionChanged(BandVM bandvm)
        {
            if (SelectedBand == null) return;
            if (ShowCancel == "Visible") CancelUpdateBand(this);
            _oldBand = SelectedBand.Copy();

            bandvm.ShowEdit = "Visible";
            bandvm.ShowCancel = "Hidden";
            bandvm.ShowSave = "Hidden";
            Enabled = false;
        }

        public ICommand BrowseCommand
        {
            get { return new RelayCommand<BandVM>(Browse); }
        }

        private void Browse(BandVM bandvm)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "Image files (*.jpg, *.jpeg, *.png, *.gif)|*.jpg; *.jpeg; *.png; *.gif" ;

            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                ImagePathToByteArrayConverter converter = new ImagePathToByteArrayConverter();
                SelectedBand.Picture = (byte[]) converter.Convert(dlg.FileName, null, null, null);
                OnPropertyChanged("SelectedBand");
            }
        }
        //************************
    }
}
