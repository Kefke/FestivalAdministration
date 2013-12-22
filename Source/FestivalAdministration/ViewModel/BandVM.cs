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
            Genres = Genre.GetGenres();

            SelectedBand = new Band();

            SelectedBandGenres = new ObservableCollection<BandGenre>();
            AddGenre(this);

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

        private ObservableCollection<Genre> _genres;

        public ObservableCollection<Genre> Genres
        {
            get { return _genres; }
            set { _genres = value; OnPropertyChanged("Genres"); }
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

        private ObservableCollection<BandGenre> _selectedBandGenres;
        private ObservableCollection<BandGenre> _oldBandGenres;

        public ObservableCollection<BandGenre> SelectedBandGenres
        {
            get { return _selectedBandGenres; }
            set { _selectedBandGenres = value; OnPropertyChanged("SelectedBandGenres");}
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

            SelectedBandGenres = new ObservableCollection<BandGenre>();
            AddGenre(this);

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

            // Delete the previous genres
            foreach (BandGenre bg in _oldBandGenres)
            {
                BandGenre.DeleteBandGenre(bg);
            }

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
            int bandID = SelectedBand.ID;
            // Save Changes
            if (_oldBand == null)
            {
                // Insert into db
                bandID = Band.AddBand(SelectedBand);
            }
            else
            {
                // Update db
                Band.UpdateBand(SelectedBand);
            }

            // Check valid genre parameters
            ObservableCollection<BandGenre> bandGenreResults = new ObservableCollection<BandGenre>();
            foreach (BandGenre bg in SelectedBandGenres)
            {
                bool allreadyExists = false;
                foreach (BandGenre bgr in bandGenreResults)
                {
                    if (bgr.GenreID == bg.GenreID || bgr.GenreID == -1) allreadyExists = true;
                }

                if (!allreadyExists)
                {
                    bg.BandID = bandID;
                    bandGenreResults.Add(bg);
                }
            }

            // Delete the previous genres
            if (_oldBandGenres != null)
            {
                foreach (BandGenre bg in _oldBandGenres)
                {
                    BandGenre.DeleteBandGenre(bg);
                }
            }

            // Add the new band genres
            foreach (BandGenre bgr in bandGenreResults)
            {
                bgr.ID = BandGenre.AddBandGenre(bgr);
            }

            SelectedBandGenres = bandGenreResults;

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
            // Reset band
            _changeNotify = false;
            SelectedBand = _oldBand;

            SelectedBandGenres = BandGenre.GetBandGenres(SelectedBand.ID);

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

            SelectedBandGenres = BandGenre.GetBandGenres(SelectedBand.ID);
            _oldBandGenres = BandGenre.GetBandGenres(SelectedBand.ID);

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
            dlg.Filter = "Image files (*.jpg, *.jpeg, *.png, *.gif)|*.jpg; *.jpeg; *.png; *.gif";

            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                ImagePathToByteArrayConverter converter = new ImagePathToByteArrayConverter();
                SelectedBand.Picture = (byte[])converter.Convert(dlg.FileName, null, null, null);
                OnPropertyChanged("SelectedBand");
            }
        }

        public ICommand AddGenreCommand
        {
            get { return new RelayCommand<BandVM>(AddGenre); }
        }

        private void AddGenre(BandVM bandvm)
        {
            if (SelectedBandGenres == null) return;
            if (SelectedBandGenres.Count > 5) return;

            SelectedBandGenres.Add(new BandGenre() { ID = -1, BandID = -1, GenreID = -1 });
            OnPropertyChanged("SelectedBandGenres");
        }
        //************************
    }
}
