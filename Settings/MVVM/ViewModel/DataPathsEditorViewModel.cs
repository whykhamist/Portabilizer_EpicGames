using Settings.Core;
using Settings.Core.DialogService;
using Settings.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Settings.MVVM.ViewModel
{
    public class DataPathsEditorViewModel : ObservableObject, IDialogRequestClose
    {

        private ObservableCollection<DataPathsModel> _DataPaths;

        public ObservableCollection<DataPathsModel> DataPaths
        {
            get { return _DataPaths; }
            set { _DataPaths = value;
                OnPropertyChanged("DataPaths");
            }
        }

        private object _PathEditorView;

        public object PathEditorView
        {
            get { return _PathEditorView; }
            set
            {
                _PathEditorView = value;
                OnPropertyChanged("PathEditorView");
            }
        }


        #region Public Event Handlers

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        #endregion

        #region Public Properties

        public RelayCommand OkCommand { get; }
        public RelayCommand CancelCommand { get; }
        public RelayCommand EditPathEditorCmd { get { return new RelayCommand(EditPaths); } }

        #endregion

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


        }

        private void EditPaths(object DP)
        {
            if (DP != null && DP is DataPathsModel _dp)
            {
                PathEditorView = new PathsEditorViewModel(_dp);
            }
        }

    }
}
