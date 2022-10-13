using System.Windows;
using System.Windows.Controls;
using SiPintar.ViewModels.Akuntansi;

namespace SiPintar.Views.Akuntansi
{
    public partial class JurnalView : UserControl
    {
        public JurnalView()
        {
            InitializeComponent();
        }

        private void NavItemSelectedHandler(object sender, RoutedEventArgs e)
        {
            var _viewModel = (JurnalViewModel)DataContext;

            string Label = ((RadioButton)sender).Tag.ToString();
            if (_viewModel != null && Label != null)
                _viewModel.UpdatePage(Label);
        }
    }
}
