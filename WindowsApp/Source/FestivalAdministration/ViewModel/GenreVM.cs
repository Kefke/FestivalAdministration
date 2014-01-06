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
    class GenreVM: ObservableObject, IPage
    {
        public GenreVM()
        {
            Genres = Genre.GetGenres();

            SelectedGenre = new Genre();
            Enabled = true;
            ShowEdit = "Hidden";
            ShowCancel = "Hidden";
            ShowSave = "Visible";
        }

        //************************
        // Parameters

        public string Name
        {
            get { return "Genres"; }
        }
        
        private ObservableCollection<Genre> _genres;

        public ObservableCollection<Genre> Genres
        {
            get { return _genres; }
            set { _genres = value; OnPropertyChanged("Genres"); }
        }

        private bool _changeNotify;
        private Genre _oldGenre;
        private Genre _selectedGenre;

        public Genre SelectedGenre
        {
            get { return _selectedGenre; }
            set
            {
                _selectedGenre = value;
                OnPropertyChanged("SelectedGenre");
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

        public ICommand AddGenreCommand
        {
            get { return new RelayCommand<GenreVM>(AddGenre); }
        }

        private void AddGenre(GenreVM genrevm)
        {
            SelectedGenre = new Genre();
            _oldGenre = null;
            genrevm.ShowEdit = "Hidden";
            genrevm.ShowCancel = "Visible";
            genrevm.ShowSave = "Visible";
            genrevm.Enabled = true;
        }

        public ICommand EditGenreCommand
        {
            get { return new RelayCommand<GenreVM>(EditGenre); }
        }

        private void EditGenre(GenreVM genrevm)
        {
            genrevm.ShowEdit = "Hidden";
            genrevm.ShowCancel = "Visible";
            genrevm.ShowSave = "Visible";
            genrevm.Enabled = true;
        }

        public ICommand DeleteGenreCommand
        {
            get { return new RelayCommand<GenreVM>(DeleteGenre); }
        }

        private void DeleteGenre(GenreVM genrevm)
        {
            if (SelectedGenre == null) return;

            Genre.DeleteGenre(SelectedGenre);

            SelectedGenre = new Genre();
            Enabled = true;
            ShowEdit = "Hidden";
            ShowCancel = "Hidden";
            ShowSave = "Visible";
        }

        public ICommand SaveUpdateGenreCommand
        {
            get { return new RelayCommand<GenreVM>(SaveUpdateGenre); }
        }

        private void SaveUpdateGenre(GenreVM genrevm)
        {
            if (!SelectedGenre.IsValid())
                return;
            // Save Changes
            if (_oldGenre == null)
            {
                // Insert into db
                SelectedGenre.ID = Genre.AddGenre(SelectedGenre);
            }
            else
            {
                // Update db
                Genre.UpdateGenre(SelectedGenre);
            }

            // Update GUI
            genrevm.ShowEdit = "Visible";
            genrevm.ShowCancel = "Hidden";
            genrevm.ShowSave = "Hidden";
            Enabled = false;
        }

        public ICommand CancelUpdateGenreCommand
        {
            get { return new RelayCommand<GenreVM>(CancelUpdateGenre); }
        }

        private void CancelUpdateGenre(GenreVM genrevm)
        {
            // Reset person
            _changeNotify = false;
            SelectedGenre = _oldGenre;

            genrevm.ShowEdit = "Visible";
            genrevm.ShowCancel = "Hidden";
            genrevm.ShowSave = "Hidden";
            Enabled = false;
        }

        public ICommand SelectionChangedCommand
        {
            get { return new RelayCommand<GenreVM>(SelectionChanged); }
        }

        private void SelectionChanged(GenreVM genrevm)
        {
            if (SelectedGenre == null) return;
            if (ShowCancel == "Visible") CancelUpdateGenre(this);
            _oldGenre = SelectedGenre.Copy();

            genrevm.ShowEdit = "Visible";
            genrevm.ShowCancel = "Hidden";
            genrevm.ShowSave = "Hidden";
            Enabled = false;
        }
        //************************
    }
}
