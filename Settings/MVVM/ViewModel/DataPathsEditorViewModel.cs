using Settings.Core;
using Settings.Core.DialogService;
using Settings.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Settings.MVVM.ViewModel
{
    public class DataPathsEditorViewModel : ObservableObject, IDialogRequestClose
    {
        #region Privte variables

        private ObservableCollection<DataPathsModel> _DataPaths;

        private object _PathEditorView;

        private DataPathsModel CurrentDP = null;

        #endregion


        #region Public Event Handlers

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        #endregion

        #region Public Properties

        public RelayCommand OkCommand { get; }

        public RelayCommand CancelCommand { get; }

        public RelayCommand EditPathEditorCmd { get { return new RelayCommand(EditPaths); } }

        public RelayCommand AddDataPathsCmd { get { return new RelayCommand(o => AddDataPaths()); } }

        public RelayCommand DeletePathEditorCmd { get { return new RelayCommand(DeleteDataPath); } }

        public ObservableCollection<DataPathsModel> DataPaths
        {
            get { return _DataPaths; }
            set
            {
                _DataPaths = value;
                OnPropertyChanged("DataPaths");
            }
        }

        public object PathEditorView
        {
            get { return _PathEditorView; }
            set
            {
                _PathEditorView = value;
                OnPropertyChanged("PathEditorView");
            }
        }

        #endregion

        #region Contstructor

        //public DataPathsEditorViewModel()
        //{
        //    OkCommand = new RelayCommand(o => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true)));
        //    CancelCommand = new RelayCommand(o => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(false)));
        //    DataPaths = new ObservableCollection<DataPathsModel>() {
        //        new DataPathsModel
        //        {
        //            GroupName = "TestName"
        //        },
        //        new DataPathsModel
        //        {
        //            GroupName = "TestName2"
        //        }
        //    };
        //}

        public DataPathsEditorViewModel(List<Configuration.DataPaths> ConfDP)
        {
            OkCommand = new RelayCommand(o => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true)));
            CancelCommand = new RelayCommand(o => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(false)));
            DataPaths = Helpers.ConfigDPToEditorDP(ConfDP);

            EditPaths(DataPaths.FirstOrDefault(o => o.IsFirst));
        }

        #endregion

        #region Private Methods

        private void EditPaths(object DP)
        {
            if (DP != null && DP is DataPathsModel _dp)
            {
                CurrentDP = _dp;
                PathEditorView = new PathsEditorViewModel(_dp);
            }
        }

        private void AddDataPaths()
        {
            DataPaths.Add(new DataPathsModel { GroupName = $"Group{DataPaths.Count + 1}", Paths = new ObservableCollection<PathsModel>() });
        }

        private void DeleteDataPath(object DP)
        {
            if (DP != null && DP is DataPathsModel _dp)
            {
                if (CurrentDP == _dp)
                {
                    PathEditorView = null;
                }
                DataPaths.Remove(_dp);
            }
        }

        #endregion

    }
}
