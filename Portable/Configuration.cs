using Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace Portable
{
    public class Configuration : Config, IConfiguration
    {
        public Configuration() { }
        public new string Title { get; set; } = "Epic Games Portable";
        public new int Width { get; set; } = 512;
        public new int Height { get; set; } = 115;
        public new bool ClearSymlinks { get; set; } = false;
        public new string Executable { get ; set; } = @$"Launcher\Portal\Binaries\Win{ (Environment.Is64BitOperatingSystem ? "64" : "32") }\EpicGamesLauncher.exe";
        public new string DataFolder { get ; set; }
        public new List<DataPaths> DataPaths { get ; set; } = new List<DataPaths>() {
            new DataPaths
            {
                GroupName = "EpicGames",
                Paths = new Dictionary<string, string> {
                    { "Epic", Path.Combine(Environment.ExpandEnvironmentVariables("%ProgramData%"), "Epic") },
                    { "EpicGamesLauncher", Path.Combine(Environment.ExpandEnvironmentVariables("%LocalAppData%"), "EpicGamesLauncher") } }
            }
        };
    }
}
