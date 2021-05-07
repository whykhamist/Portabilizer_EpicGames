using Settings.Core;
using Settings.MVVM.Model;
using Settings.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Settings.MVVM.View
{
    /// <summary>
    /// Interaction logic for PathsEditorView.xaml
    /// </summary>
    public partial class PathsEditorView : UserControl
    {
        //public static readonly DependencyProperty DataPathsProperty = DependencyProperty.Register
        //(
        //        "DataPaths",
        //        typeof(DataPathsModel),
        //        typeof(PathsEditorView),
        //        new PropertyMetadata(null)
        //);

        //public DataPathsModel DataPaths
        //{
        //    get { return (DataPathsModel)GetValue(DataPathsProperty); }
        //    set { SetValue(DataPathsProperty, value);
        //        PaathsEditorVM.DataPaths = value;
        //    }
        //}


        public PathsEditorView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddPathBtn.Focus();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = (e.Text != Helpers.CleanFileName(e.Text));
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var txtBox = ((TextBox)sender);
            txtBox.Text = Helpers.CleanFileName(txtBox.Text);
        }

        private void TextBox_Loaded(object sender, RoutedEventArgs e)
        {
            var txtBox = ((TextBox)sender);
            txtBox.Text = Helpers.CleanFileName(txtBox.Text);
        }
    }
}
