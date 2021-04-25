using System;
using Configuration;
using System.Collections.Generic;

namespace Settings.MVVM.Model
{
    sealed class ConfigurationModel : IConfiguration
    {
        private string _title;
        private int _width;
        private int _height;
        private bool _clearSymlinks;
        private string _executable;
        private string _dataFolder;
        private List<DataPaths> _dataPaths;

        public string Title { get => _title; set => _title = value; }
        public int Width { get => _width; set => _width = value; }
        public int Height { get => _height; set => _height = value; }
        public bool ClearSymlinks { get => _clearSymlinks; set => _clearSymlinks = value; }
        public string Executable { get => _executable; set => _executable = value; }
        public string DataFolder { get => _dataFolder; set => _dataFolder = value; }
        public List<DataPaths> DataPaths { get => _dataPaths; set => _dataPaths = value; }

    }
}
