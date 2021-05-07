using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Settings.Core.IOService
{
    public class OpenFileDialogService : IOService
    {
        public string OpenDialog(string title, string defaultPath = null)
        {
            string output = string.Empty;
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.FileName = defaultPath;
            OFD.Title = title;

            OFD.InitialDirectory = Environment.CurrentDirectory;
            if (OFD.ShowDialog() == true)
            {
                output = OFD.FileName;
            }
            return output;
        }
    }
}
