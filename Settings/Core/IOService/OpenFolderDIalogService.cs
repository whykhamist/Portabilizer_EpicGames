using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinForms = System.Windows.Forms;

namespace Settings.Core.IOService
{
    public class OpenFolderDIalogService : IOService
    {
        public string OpenDialog(string title, string defaultPath = null)
        {
            string output = string.Empty;
            var FBD = new WinForms.FolderBrowserDialog
            {
                UseDescriptionForTitle = true,
                Description = title,
                ShowNewFolderButton = true,
                SelectedPath = defaultPath
            };

            if (FBD.ShowDialog() == WinForms.DialogResult.OK)
            {
                output = FBD.SelectedPath;
            }
            return output;
        }
    }
}
