using System.Collections.ObjectModel;

namespace Settings.MVVM.Model
{
    public class DataPathsModel
    {
        public string GroupName { get; set; }

        public ObservableCollection<PathsModel> Paths { get; set; }

        public bool IsFirst { get; set; }
    }
}
