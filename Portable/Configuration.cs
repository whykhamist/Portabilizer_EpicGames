using Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace Portable
{
    internal class Configuration : IConfiguration
    {
        public string Title { get; set; } = "Epic Games Portable";
        public int Width { get; set; } = 512;
        public int Height { get; set; } = 115;
        public bool ClearSymlinks { get; set; } = true;
        public string Executable { get ; set; } = @$"Launcher\Portal\Binaries\Win{ (Environment.Is64BitOperatingSystem ? "64" : "32") }\EpicGamesLauncher.exe";
        public string DataFolder { get ; set; }
        public List<DataPaths> DataPaths { get ; set; } = new List<DataPaths>() {
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
