using Settings.Core;
using Settings.Core.DialogService;

namespace Settings.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        private readonly IDialogService dialogService;

        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand SettingsViewCommand { get; set; }
        public RelayCommand EpicGamesViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public SettingsViewModel SettingsVM { get; set; }
        public EpicGamesViewModel EpicGamesVM { get; set; }


        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }


        public MainViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;

            HomeVM = new HomeViewModel();
            SettingsVM = new SettingsViewModel(this.dialogService);
            EpicGamesVM = new EpicGamesViewModel();

            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
            });

            SettingsViewCommand = new RelayCommand(o =>
            {
                CurrentView = SettingsVM;
            });

            EpicGamesViewCommand = new RelayCommand(o =>
            {
                CurrentView = EpicGamesVM;
            });
        }
    }
}
