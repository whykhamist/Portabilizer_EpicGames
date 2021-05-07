using System;

namespace Settings.Core.IOService
{
    public interface IOService
    {
        string OpenDialog(string title, string defaultPath = null);
    }
}
