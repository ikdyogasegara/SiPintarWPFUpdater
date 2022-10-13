using System.Windows;
using System.Windows.Controls;
using SiPintar.ViewModels.Akuntansi;

namespace SiPintar.Views.Akuntansi
{
    /// <summary>
    /// Interaction logic for PenyusutanView.xaml
    /// </summary>
    public partial class PenyusutanView : UserControl
    {
        public PenyusutanView()
        {
            InitializeComponent();
        }

        private void NavItemSelectedHandler(object sender, RoutedEventArgs e)
        {
            var _viewModel = (PenyusutanViewModel)DataContext;

            string Label = ((RadioButton)sender).Tag.ToString();
            if (_viewModel != null && Label != null)
                _viewModel.UpdatePage(Label);
        }
    }
}
