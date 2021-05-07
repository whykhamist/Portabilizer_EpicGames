﻿using Settings.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Settings.Core
{
    public static class Helpers
    {
        public static string CleanFileName(string fileName)
        {
            return Path.GetInvalidFileNameChars().Aggregate(fileName, (current, c) => current.Replace(c.ToString(), string.Empty));
        }

        public static ObservableCollection<DataPathsModel> ConfigDPToEditorDP(List<Configuration.DataPaths> ConfDP)
        {
            var tmpDP = new ObservableCollection<DataPathsModel>();
            foreach (Configuration.DataPaths dp in ConfDP)
            {
                tmpDP.Add(new DataPathsModel
                {
                    GroupName = dp.GroupName,
                    Paths = DictionaryPathstoOCPathsModel(dp.Paths)
                });
            }
            return tmpDP;
        }

        public static List<Configuration.DataPaths> EditorDPToConfigDP(ObservableCollection<DataPathsModel> dataPath)
        {
            var tmpDP = new List<Configuration.DataPaths>();

            foreach(var data in dataPath)
            {
                tmpDP.Add(new Configuration.DataPaths
                {
                    GroupName = data.GroupName,
                    Paths = OCPathsModelToDictionaryPaths(data.Paths)
                });
            }


            return tmpDP;
        }

        public static ObservableCollection<PathsModel> DictionaryPathstoOCPathsModel(Dictionary<string, string> confPaths)
        {
            var tmpPaths = new ObservableCollection<PathsModel>();
            foreach (KeyValuePair<string, string> kvp in confPaths)
            {
                tmpPaths.Add(new PathsModel
                {
                    FolderName = kvp.Key,
                    Path = kvp.Value
                });
            }
            return tmpPaths;
        }

        public static Dictionary<string, string> OCPathsModelToDictionaryPaths(ObservableCollection<PathsModel> pathsModel)
        {
            var tmpPaths = new Dictionary<string, string>();
            foreach(var data in pathsModel)
            {
                tmpPaths.Add(data.FolderName, data.Path);
            }
            return tmpPaths;
        }
    }
}
