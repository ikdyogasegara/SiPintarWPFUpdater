using System.Windows;
using System.Windows.Controls;
using SiPintar.ViewModels.Personalia;

namespace SiPintar.Views.Personalia
{
    public partial class GajiPokokView : UserControl
    {
        public GajiPokokView()
        {
            InitializeComponent();
        }

        private void NavItemSelectedHandler(object sender, RoutedEventArgs e)
        {
            var _viewModel = (GajiPokokViewModel)DataContext;

            string Label = ((RadioButton)sender).Tag.ToString();
            if (_viewModel != null && Label != null)
                _viewModel.UpdatePage(Label);
        }
    }
}
