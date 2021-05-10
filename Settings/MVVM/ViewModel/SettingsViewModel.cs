using Settings.MVVM.Model;
using System;
using System.Collections.Generic;
using Configuration;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Converters;
using Settings.Core;
using Settings.Core.IOService;
using Settings.Core.DialogService;

namespace Settings.MVVM.ViewModel
{
    public class SettingsViewModel : ObservableObject
    {
        private Config UserConfig { get;set; }
        private static Config PortableConfig { get { return new Portable.Configuration(); } }
        private readonly IDialogService dialogService;

        #region Public Properties

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

        public RelayCommand BrowseExecCmd { get; set; }

        public RelayCommand BrowseDataFolderCmd { get; set; }

        public RelayCommand OpenDPEditorCmd { get; set; }

        #endregion

        public SettingsViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;
            UserConfig = new Config();

            if (File.Exists(Constants.ConfigFileName))
            {
                UserConfig = ConfigReader.Read(Constants.ConfigFileName);
                var tmp = UserConfig.DataPaths;
                UserConfig = PortableConfig.Merge(UserConfig);
                //UserConfig.DataPaths = tmp;
            }

            SaveConfig();

            BrowseExecCmd = new RelayCommand(o => BrowseExec());
            BrowseDataFolderCmd = new RelayCommand(o => BrowseDataFolder());
            OpenDPEditorCmd = new RelayCommand(o => OpenDataPathsEditor());
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

        private void BrowseExec()
        {
            string output = Helpers.OpenDialog(OpenDialogType.File, "Browse for executable file.", Environment.CurrentDirectory);
            if (!string.IsNullOrWhiteSpace(output))
            {
                Executable = output.Replace($"{Environment.CurrentDirectory}", "");
            }
        }

        private void BrowseDataFolder()
        {
            string output = Helpers.OpenDialog(OpenDialogType.Folder, "Browse for Data Folder.", Environment.CurrentDirectory);
            if (!string.IsNullOrWhiteSpace(output))
            {
                DataFolder = output.Replace( $"{Environment.CurrentDirectory}", "");
            }
        }

        private void OpenDataPathsEditor()
        {
            var vm = new DataPathsEditorViewModel(DataPaths);
            bool? result = this.dialogService.ShowDialog(vm);

            if (result.HasValue && result.Value)
            {
                DataPaths = Helpers.EditorDPToConfigDP(vm.DataPaths);
            }
        }
    }
}
