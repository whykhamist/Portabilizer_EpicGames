using Settings.Core.DialogService;
using Settings.MVVM.View;
using Settings.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Settings
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IDialogService dialogService = new DialogService(MainWindow);

            dialogService.Register<DataPathsEditorViewModel, DataPathsEditorView>();

            var viewModel = new MainViewModel(dialogService);
            var view = new MainWindow { DataContext = viewModel };

            view.ShowDialog();
        }
    }
}
