using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;

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
    }
}
