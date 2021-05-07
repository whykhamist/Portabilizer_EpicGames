using Settings.Core;
using Settings.Core.IOService;
using Settings.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Settings.MVVM.ViewModel
{
    public class PathsEditorViewModel : ObservableObject
    {
        private DataPathsModel _DataPaths;
        private string _StatusMessage;

        public bool PathsHasEmpty
        {
            get
            {
                return (DataPaths.Paths == null || DataPaths.Paths.Any(o => (string.IsNullOrWhiteSpace(o.FolderName) && string.IsNullOrWhiteSpace(o.Path))));
            }
        }

        public DataPathsModel DataPaths
        {
            get { return _DataPaths; }
            set
            {
                _DataPaths = value;
                OnPropertyChanged("DataPaths");
            }
        }

        public string StatusMessage
        {
            get { return _StatusMessage; }
            set
            {
                _StatusMessage = value;
                OnPropertyChanged("StatusMessage");
            }
        }

        public RelayCommand AddPathCmd { get { return new RelayCommand(o => AddPath()); } }

        public RelayCommand RemovePathCmd { get { return new RelayCommand(RemovePath); } }

        public RelayCommand BrowsePathCmd { get { return new RelayCommand(BrowsePath); } }

        public PathsEditorViewModel(DataPathsModel dataPaths)
        {
            var dp = dataPaths;
            if (dp != null && dp.Paths == null)
            {
                dp.Paths = new ObservableCollection<PathsModel>();
            }
            DataPaths = dp ?? new DataPathsModel { Paths = new ObservableCollection<PathsModel>() };
        }

        //public PathsEditorViewModel()
        //{
        //    DataPaths = new DataPathsModel {
        //        Paths = new ObservableCollection<PathsModel>()
        //    };
        //}

        private void AddPath()
        {
            if (!PathsHasEmpty)
            {
                DataPaths.Paths.Add(new PathsModel());
            }
        }

        private void RemovePath(object path)
        {
            if (path != null && path is PathsModel _path)
            {
                DataPaths.Paths.Remove(_path);
            }
        }

        private void BrowsePath(object path)
        {
            StatusMessage = string.Empty;
            if (path != null && path is PathsModel _path)
            {
                IOService fileDialog = new OpenFolderDIalogService();
                string output = fileDialog.OpenDialog($"Browse for a data path.");

                if (!string.IsNullOrWhiteSpace(output))
                {
                    if (!IsPathUnique(output))
                    {
                        StatusMessage = $"Path already Exists!";
                        return;
                    }
                    var newPath = new PathsModel
                    {
                        Path = output
                    };

                    if (string.IsNullOrWhiteSpace(_path.FolderName))
                    {
                        string[] breakDown = output.Split(@"\");
                        if (breakDown.Length > 0)
                        {
                            newPath.FolderName = UniquifyFolderName(breakDown[^1]);
                        }
                    }
                    else
                    {
                        newPath.FolderName = _path.FolderName;
                    }
                    DataPaths.Paths[DataPaths.Paths.IndexOf(_path)] = newPath;
                }
            }
        }

        private bool IsPathUnique(string path)
        {
            return !DataPaths.Paths.Any(o => o.Path == path);
        }

        private string UniquifyFolderName(string folderName, int folderCount = 0)
        {
            string output = folderName;
            string tmpName = folderName;
            if(folderCount > 0)
            {
                tmpName = $"{folderName}({folderCount})";
            }

            if(DataPaths.Paths.Any(o => o.FolderName == tmpName))
            {
                folderCount++;
                output = UniquifyFolderName(output, folderCount);
            }
            else
            {
                output = tmpName;
            }

            return output;
        }


    }
}
