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
        private Config UserConfig { get;set; }
        private static Config PortableConfig { get { return new Portable.Configuration(); } }

        public RelayCommand BrowseExecutable { get; set; }
        public RelayCommand BrowseDataFolder { get; set; }

        public string Title { get
            {
                return UserConfig.Title;
            }
            set
            {
                UserConfig.Title = value;
                SaveConfig();
                OnPropertyChanged("Title");
            }
        }

        public string Executable
        {
            get
            {
                return UserConfig.Executable;
            }
            set
            {
                UserConfig.Executable = value;
                SaveConfig();
                OnPropertyChanged("Executable");
            }
        }

        public string DataFolder
        {
            get
            {
                return UserConfig.DataFolder;
            }
            set
            {
                UserConfig.DataFolder = value;
                SaveConfig();
                OnPropertyChanged("DataFolder");
            }
        }

        public int Width
        {
            get
            {
                return UserConfig.Width < 0 ? 510 : UserConfig.Width;
            }
            set
            {
                UserConfig.Width = (value < 0) ? 510 : value;
                SaveConfig();
                OnPropertyChanged("Width");
            }
        }

        public int Height
        {
            get
            {
                return UserConfig.Height < 0 ? 110 : UserConfig.Height;
            }
            set
            {
                UserConfig.Height = (value < 0) ? 110 : value;
                SaveConfig();
                OnPropertyChanged("Height");
            }
        }

        public bool ClearSymlinks
        {
            get
            {
                return UserConfig.ClearSymlinks;
            }
            set
            {
                UserConfig.ClearSymlinks = value;
                SaveConfig();
                OnPropertyChanged("ClearSymlinks");
            }
        }

        public List<DataPaths> DataPaths
        {
            get
            {
                return UserConfig.DataPaths;
            }
            set
            {
                UserConfig.DataPaths = value;
                SaveConfig();
                OnPropertyChanged("DataPaths");
            }
        }

        public List<DataPaths> PluginDataPaths
        {
            get
            {
                return PortableConfig.DataPaths;
            }
        }

        public SettingsViewModel()
        {
            UserConfig = new Config();

            if (File.Exists(Constants.ConfigFileName))
            {
                UserConfig = ConfigReader.Read(Constants.ConfigFileName);
                var tmp = UserConfig.DataPaths;
                UserConfig = PortableConfig.Merge(UserConfig);
                UserConfig.DataPaths = tmp;
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
                    jsonSerializer.Serialize(jsonWriter, UserConfig);
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
            var FBD = new WinForms.FolderBrowserDialog
            {
                ShowNewFolderButton = true,
                SelectedPath = Environment.CurrentDirectory
            };

            if (FBD.ShowDialog() == WinForms.DialogResult.OK)
            {
                DataFolder = FBD.SelectedPath.Replace(@$"{Environment.CurrentDirectory}\", "");
            }
        }

    }
}
