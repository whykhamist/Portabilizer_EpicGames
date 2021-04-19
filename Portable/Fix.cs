using Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Portable
{
    public class Fix : IFix
    {
        /// <summary>
        /// This method will be used in starting an application instead of the built in method of the Portabilizer.
        /// Throw a NotImplementedExemption to use the default method.
        /// </summary>
        /// <param name="executable"></param>
        /// <param name="args"></param>
        public void ExecuteApplication(string executable, string[] args)
        {
            string text = "";
            string text2 = null;
            foreach (string text3 in args.Skip(1))
            {
                string a = text3.Split('=')[0].ToLower();
                string text4 = text3.Split('=')[1].ToLower();
                if (a == "_runapp" && !string.IsNullOrWhiteSpace(text4))
                {
                    text2 = text4;
                }
                else
                {
                    text = text + text3 + " ";
                }
            }
            Process process = new Process();
            process.StartInfo.WorkingDirectory = Path.GetDirectoryName(executable);
            if (!string.IsNullOrWhiteSpace(text2))
            {
                process.StartInfo.FileName = executable;
                process.StartInfo.Arguments = "com.epicgames.launcher://apps/" + text2 + "?action=launch&silent=true";
            }
            else
            {
                process.StartInfo.Arguments = text;
                process.StartInfo.FileName = executable;
            }
            process.Start();
            process.WaitForExit();

            Fixers.ClearLogin();
        }

        /// <summary>
        /// This function will be executed BEFORE creating Symlinks and applying registry values.
        /// </summary>
        /// <param name="progress"></param>
        /// <param name="cancelToken"></param>
        /// <returns></returns>
        public async Task Pre(IProgress<FixProgress> progress = null, CancellationToken cancelToken = default)
        {
            var progression = new FixProgress
            {
                Progress = 100d
            };
            await Task.Delay(10, cancelToken);

            progress?.Report(progression);
        }

        /// <summary>
        /// This function will be executed AFTER creating Symlinks and applying registry values.
        /// </summary>
        /// <param name="progress"></param>
        /// <param name="cancelToken"></param>
        /// <returns></returns>
        public async Task Post(IProgress<FixProgress> progress = null, CancellationToken cancelToken = default)
        {
            progress?.Report(new FixProgress
            {
                Progress = 15d,
                StatusMessage = $"Collecting manifests information."
            });
            List<Manifest> manifests = Fixers.CollectManifests(Fixers.ManifestLocation);

            progress?.Report(new FixProgress
            {
                Progress = 20d,
                StatusMessage = $"Found {manifests.Count} file(s)."
            });

            double ctr = (manifests.Count * 100);
            double ctrFixed = 0d;

            foreach (Manifest manifest in manifests)
            {
                var fixedManifest = Fixers.FixManifests(manifest, Fixers.GamesFolder);
                fixedManifest.SaveManifest(Fixers.ManifestLocation);
                ctrFixed++;
                progress?.Report(new FixProgress
                {
                    Progress = (((100 * ctrFixed) / ctr) * 50) + 20d,
                    StatusMessage = manifest.AppName
                });
                await Task.Delay(10, cancelToken);
            }

            List<string> catalogs = Fixers.CollectCatalogsWithNoManifest();
            double ctrCats = (catalogs.Count * 100);
            double ctrBuilt = 0d;

            foreach(string catalog in catalogs)
            {
                MiniManifest mm = Fixers.CreateMinimalManifest(catalog);
                mm.SaveManifest(Fixers.ManifestLocation);
                ctrBuilt++;
                progress?.Report(new FixProgress
                {
                    Progress = (((100 * ctrBuilt) / ctrCats) * 20) + 70d,
                    StatusMessage = $"Built minimal manifest for {mm.MainGameAppName}."
                });
                await Task.Delay(10, cancelToken);
            }

            progress?.Report(new FixProgress
            {
                Progress = 100d,
                StatusMessage = "Done!"
            });
            await Task.Delay(10, cancelToken);
        }
    }
}
