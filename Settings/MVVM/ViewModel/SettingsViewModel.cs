using Settings.Core;
using Settings.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Configuration;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Converters;
using Microsoft.Win32;
using WinForms = System.Windows.Forms;

namespace Settings.MVVM.ViewModel
{
    public class SettingsViewModel : ObservableObject
    {
        private Config _configuration
        { get;set; }

        public RelayCommand BrowseExecutable { get; set; }
        public RelayCommand BrowseDataFolder { get; set; }

        public string Title { get
            {
                return _configuration.Title;
            }
            set
            {
                _configuration.Title = value;
                SaveConfig();
                OnPropertyChanged("Title");
            }
        }

        public string Executable
        {
            get
            {
                return _configuration.Executable;
            }
            set
            {
                _configuration.Executable = value;
                SaveConfig();
                OnPropertyChanged("Executable");
            }
        }

        public string DataFolder
        {
            get
            {
                return _configuration.DataFolder;
            }
            set
            {
                _configuration.DataFolder = value;
                SaveConfig();
                OnPropertyChanged("DataFolder");
            }
        }

        public int Width
        {
            get
            {
                return _configuration.Width < 0 ? 510 : _configuration.Width;
            }
            set
            {
                _configuration.Width = (value < 0) ? 510 : value;
                SaveConfig();
                OnPropertyChanged("Width");
            }
        }

        public int Height
        {
            get
            {
                return _configuration.Height < 0 ? 110 : _configuration.Height;
            }
            set
            {
                _configuration.Height = (value < 0) ? 110 : value;
                SaveConfig();
                OnPropertyChanged("Height");
            }
        }

        public bool ClearSymlinks
        {
            get
            {
                return _configuration.ClearSymlinks;
            }
            set
            {
                _configuration.ClearSymlinks = value;
                SaveConfig();
                OnPropertyChanged("ClearSymlinks");
            }
        }

        public List<DataPaths> DataPaths
        {
            get
            {
                return _configuration.DataPaths;
            }
            set
            {
                _configuration.DataPaths = value;
                SaveConfig();
                OnPropertyChanged("DataPaths");
            }
        }

        public SettingsViewModel()
        {
            _configuration = new Portable.Configuration();

            if (File.Exists(Constants.ConfigFileName))
            {
                _configuration = _configuration.Merge(ConfigReader.Read(Constants.ConfigFileName));
            }
            SaveConfig();

            BrowseExecutable = new RelayCommand(o => { BrowseExecutableFunc(); });
            BrowseDataFolder = new RelayCommand(o => { BrowseDataFolderFunc(); });
        }

        private void SaveConfig()
        {
            JsonSerializer jsonSerializer = new JsonSerializer();
            jsonSerializer.Converters.Add(new JavaScriptDateTimeConverter());
            jsonSerializer.NullValueHandling = NullValueHandling.Ignore;
            jsonSerializer.Formatting = Formatting.Indented;
            using (StreamWriter textWriter = new StreamWriter(Constants.ConfigFileName))
            {
                using (JsonWriter jsonWriter = new JsonTextWriter(textWriter))
                {
                    jsonSerializer.Serialize(jsonWriter, _configuration);
                }
            }
        }

        private void BrowseExecutableFunc()
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.Title = "Browse for Executable";

            OFD.InitialDirectory = Environment.CurrentDirectory;
            if (OFD.ShowDialog() == true)
            {
                Executable = OFD.FileName.Replace($@"{Environment.CurrentDirectory}\", "");
            }
        }

        private void BrowseDataFolderFunc()
        {
            WinForms.FolderBrowserDialog FBD = new WinForms.FolderBrowserDialog();

            FBD.ShowNewFolderButton = true;
            FBD.SelectedPath = Environment.CurrentDirectory;
            if (FBD.ShowDialog() == WinForms.DialogResult.OK)
            {
                DataFolder = FBD.SelectedPath.Replace(@$"{Environment.CurrentDirectory}\", "");
            }
        }

    }
}
