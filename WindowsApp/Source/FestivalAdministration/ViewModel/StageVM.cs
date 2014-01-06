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
    class StageVM: ObservableObject, IPage
    {
        public StageVM()
        {
            Stages = Stage.GetStages();

            SelectedStage = new Stage();
            Enabled = true;
            ShowEdit = "Hidden";
            ShowCancel = "Hidden";
            ShowSave = "Visible";
        }

        //************************
        // Parameters

        public string Name
        {
            get { return "Stages"; }
        }
        
        private ObservableCollection<Stage> _contactTypes;

        public ObservableCollection<Stage> Stages
        {
            get { return _contactTypes; }
            set { _contactTypes = value; OnPropertyChanged("Stages"); }
        }

        private bool _changeNotify;
        private Stage _oldType;
        private Stage _selectedType;

        public Stage SelectedStage
        {
            get { return _selectedType; }
            set
            {
                _selectedType = value;
                OnPropertyChanged("SelectedStage");
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

        public ICommand AddStageCommand
        {
            get { return new RelayCommand<StageVM>(AddStage); }
        }

        private void AddStage(StageVM stagevm)
        {
            SelectedStage = new Stage();
            _oldType = null;
            stagevm.ShowEdit = "Hidden";
            stagevm.ShowCancel = "Visible";
            stagevm.ShowSave = "Visible";
            stagevm.Enabled = true;
        }

        public ICommand EditStageCommand
        {
            get { return new RelayCommand<StageVM>(EditStage); }
        }

        private void EditStage(StageVM stagevm)
        {
            stagevm.ShowEdit = "Hidden";
            stagevm.ShowCancel = "Visible";
            stagevm.ShowSave = "Visible";
            stagevm.Enabled = true;
        }

        public ICommand DeleteStageCommand
        {
            get { return new RelayCommand<StageVM>(DeleteStage); }
        }

        private void DeleteStage(StageVM stagevm)
        {
            if (SelectedStage == null) return;

            Stage.DeleteStage(SelectedStage);

            SelectedStage = new Stage();
            Enabled = true;
            ShowEdit = "Hidden";
            ShowCancel = "Hidden";
            ShowSave = "Visible";
        }

        public ICommand SaveUpdateStageCommand
        {
            get { return new RelayCommand<StageVM>(SaveUpdateStage); }
        }

        private void SaveUpdateStage(StageVM stagevm)
        {
            if (!SelectedStage.IsValid())
                return;
            // Save Changes
            if (_oldType == null)
            {
                // Insert into db
                SelectedStage.ID = Stage.AddStage(SelectedStage);
            }
            else
            {
                // Update db
                Stage.UpdateStage(SelectedStage);
            }

            // Update GUI
            stagevm.ShowEdit = "Visible";
            stagevm.ShowCancel = "Hidden";
            stagevm.ShowSave = "Hidden";
            Enabled = false;
        }

        public ICommand CancelUpdateStageCommand
        {
            get { return new RelayCommand<StageVM>(CancelUpdateStage); }
        }

        private void CancelUpdateStage(StageVM stagevm)
        {
            // Reset person
            _changeNotify = false;
            SelectedStage = _oldType;

            stagevm.ShowEdit = "Visible";
            stagevm.ShowCancel = "Hidden";
            stagevm.ShowSave = "Hidden";
            Enabled = false;
        }

        public ICommand SelectionChangedCommand
        {
            get { return new RelayCommand<StageVM>(SelectionChanged); }
        }

        private void SelectionChanged(StageVM stagevm)
        {
            if (SelectedStage == null) return;
            if (ShowCancel == "Visible") CancelUpdateStage(this);
            _oldType = SelectedStage.Copy();

            stagevm.ShowEdit = "Visible";
            stagevm.ShowCancel = "Hidden";
            stagevm.ShowSave = "Hidden";
            Enabled = false;
        }
        //************************
    }
}
