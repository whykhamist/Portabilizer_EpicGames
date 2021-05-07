using System.Windows;

namespace Settings.Core.DialogService
{
    public interface IDialogService
    {
        void Register<TViewModel, TView>() where TViewModel : IDialogRequestClose
                                           where TView : IDialog;

        bool? ShowDialog<TViewModel>(TViewModel viewModel) where TViewModel : IDialogRequestClose;

        bool? ShowDialog<TViewModel>(TViewModel viewModel, Window owner = null) where TViewModel : IDialogRequestClose;
    }
}
