using Settings.Core.DialogService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Settings.MVVM.View
{
    /// <summary>
    /// Interaction logic for DataPathsEditorView.xaml
    /// </summary>
    public partial class DataPathsEditorView : Window, IDialog
    {
        public DataPathsEditorView()
        {
            InitializeComponent();
            this.Owner = Application.Current.MainWindow;
        }

        private void HeaderGrid_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) { DragMove(); }
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DataPathEditorWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
