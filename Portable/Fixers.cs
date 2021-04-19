using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using Systems;

namespace Portable
{
    internal static class Fixers
    {
        public static string ManifestLocation
        {
            get
            {
                return Path.Combine(
                    Environment.ExpandEnvironmentVariables("%ProgramData%"),
                    "Epic", "EpicGamesLauncher", "Data", "Manifests");
            }
        }

        public static string GamesFolder
        {
            get
            {
                return new DirectoryInfo("Games").FullName;
            }
        }

        public static string SettingsLocation
        {
            get
            {
                return Path.Combine(Environment.ExpandEnvironmentVariables("%localappdata%"), "EpicGamesLauncher", "Saved", "Config", "Windows", "GameUserSettings.ini");
            }
        }

        private static JsonSerializer JsonSerializer
        {
            get
            {
                JsonSerializer jsonSerializer = new JsonSerializer
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    Formatting = Formatting.Indented
                };
                jsonSerializer.Converters.Add(new JavaScriptDateTimeConverter());
                return jsonSerializer;
            }
        }

        public static List<Manifest> CollectManifests(string manifestLocation)
        {
            DirectoryInfo Location = new DirectoryInfo(manifestLocation);
            List<Manifest> manifests = new List<Manifest>();
            foreach (FileInfo fi in Location.GetFiles("*.item"))
            {
                manifests.Add(ReadManifestFile(fi.FullName));
            }
            return manifests;
        }

        public static List<string> CollectCatalogsWithNoManifest()
        {
            List<string> output = new List<string>();

            DirectoryInfo[] gamesFolderInfo = new DirectoryInfo(GamesFolder).GetDirectories();
            foreach (DirectoryInfo di in gamesFolderInfo)
            {
                string tmp = Path.Combine(di.FullName, ".egstore");
                if (Directory.Exists(tmp))
                {
                    FileInfo[] catalogFiles = di.GetFiles(Path.Combine(".egstore", "*.mancpn"));
                    if (catalogFiles.Length > 0)
                    {
                        foreach (FileInfo catFile in catalogFiles)
                        {
                            if (!CatalogHasManifest(catFile.Name.Replace(".mancpn", "")))
                            {
                                output.Add(catFile.FullName);
                            }
                        }
                    }
                }
            }

            return output;
        }

        /// <summary>
        /// Check if Catalog name (without extension) has a corresponding Manifest file
        /// </summary>
        /// <param name="catalogFileName"></param>
        /// <returns></returns>
        public static bool CatalogHasManifest(string catalogFileName)
        {
            return File.Exists(Path.Combine(ManifestLocation, $"{catalogFileName}.item"));
        }

        public static MiniManifest CreateMinimalManifest(string catalogFilePath)
        {
            FileInfo CatalogFile = new FileInfo(catalogFilePath);
            Catalog catalog = ReadCatalog(CatalogFile.FullName);

            MiniManifest output = new MiniManifest
            {
                FormatVersion = catalog.FormatVersion,
                ManifestLocation = CatalogFile.Directory.FullName,
                AppName = catalog.AppName,
                BuildLabel = "Live",
                CatalogItemId = catalog.CatalogItemId,
                CatalogNamespace = catalog.CatalogNamespace,
                InstallationGuid = Path.GetFileNameWithoutExtension(CatalogFile.Name),
                InstallLocation = CatalogFile.Directory.Parent.FullName,
                StagingLocation = Path.Combine(CatalogFile.Directory.FullName, "bps"),
                MainGameAppName = catalog.AppName
            };

            return output;
        }

        public static void SaveManifest(this MiniManifest manifest, string manifestLocation)
        {
            SaveManifestObj(manifest, Path.Combine(manifestLocation, $"{manifest.InstallationGuid}.item"));
        }

        public static void SaveManifest(this Manifest manifest, string manifestLocation)
        {
            SaveManifestObj(manifest, Path.Combine(manifestLocation, $"{manifest.InstallationGuid}.item"));
        }

        private static void SaveManifestObj(object manifest, string fileName)
        {
            using StreamWriter textWriter = new StreamWriter(fileName);
            using JsonWriter jsonWriter = new JsonTextWriter(textWriter);
            JsonSerializer.Serialize(jsonWriter, manifest);
        }

        public static Manifest FixManifests(this Manifest epicManifest, string gamesDir)
        {
            if ((!(epicManifest.InstallLocation != Path.Combine(gamesDir, epicManifest.MandatoryAppFolderName)) ||
                !File.Exists(Path.Combine(gamesDir, epicManifest.MandatoryAppFolderName, ".egstore", $"{epicManifest.InstallationGuid}.mancpn"))) &&
                !epicManifest.bIsIncompleteInstall)
            {
                return epicManifest;
            }
            epicManifest.bIsIncompleteInstall = false;
            epicManifest.ManifestLocation = Path.Combine(gamesDir, epicManifest.MandatoryAppFolderName) + "/.egstore";
            epicManifest.InstallLocation = Path.Combine(gamesDir, epicManifest.MandatoryAppFolderName);
            epicManifest.StagingLocation = Path.Combine(gamesDir, epicManifest.MandatoryAppFolderName) + "/.egstore/bps";

            return epicManifest;
        }

        public static Manifest ReadManifestFile(string itemFile)
        {
            string value = File.ReadAllText(itemFile);
            Manifest epicManifest = JsonConvert.DeserializeObject<Manifest>(value);
            return epicManifest;
        }

        public static Catalog ReadCatalog(string catalogFilePath)
        {
            string catalogString = File.ReadAllText(catalogFilePath);
            var catalog = JsonConvert.DeserializeObject<Catalog>(catalogString);
            return catalog;
        }

        public static void ClearLogin()
        {
            IniFile iniFile = new IniFile(SettingsLocation);
            if (iniFile.Read("Enable", "RememberMe") == "False")
            {
                iniFile.Write("Data", "W3siUmVnaW9uIjoiUHJvZCIsIkVtYWlsIjoiIiwiTmFtZSI6IiIsIkxhc3ROYW1lIjoiIiwiRGlzcGxheU5hbWUiOiIiLCJUb2tlbiI6IiIsImJIYXNQYXNzd29yZEF1dGgiOmZhbHNlfV0=", "RememberMe");
            }
        }
    }
}
