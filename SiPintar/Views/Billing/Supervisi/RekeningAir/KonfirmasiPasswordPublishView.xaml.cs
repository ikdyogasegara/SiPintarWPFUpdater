using System.Windows;
using System.Windows.Controls;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Views.Billing.Supervisi.RekeningAir
{
    public partial class KonfirmasiPasswordPublishView : UserControl
    {
        private readonly RekeningAirViewModel _viewModel;
        public KonfirmasiPasswordPublishView(RekeningAirViewModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (RekeningAirViewModel)DataContext;
        }

        private void VerifikasiPassword(object sender, RoutedEventArgs e)
        {
            _viewModel.Password = TxtPassword.Password;
        }

        private void TxtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordPlaceholder.Visibility = !string.IsNullOrWhiteSpace(TxtPassword.Password) ? Visibility.Hidden : Visibility.Visible;
        }
    }
}
