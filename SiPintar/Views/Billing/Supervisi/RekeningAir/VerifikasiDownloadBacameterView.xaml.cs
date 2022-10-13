using System.Windows;
using System.Windows.Controls;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Views.Billing.Supervisi.RekeningAir
{
    /// <summary>
    /// Interaction logic for VerifikasiDownloadBacameterView.xaml
    /// </summary>
    public partial class VerifikasiDownloadBacameterView : UserControl
    {
        private readonly RekeningAirViewModel _viewModel;
        public VerifikasiDownloadBacameterView(RekeningAirViewModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (RekeningAirViewModel)DataContext;
        }

        private void CekLangsungPublish_Checked(object sender, RoutedEventArgs e)
        {
            _viewModel.IsLangsungPublish = true;
        }

        private void CekLangsungPublish_Unchecked(object sender, RoutedEventArgs e)
        {
            _viewModel.IsLangsungPublish = false;
        }

    }
}
