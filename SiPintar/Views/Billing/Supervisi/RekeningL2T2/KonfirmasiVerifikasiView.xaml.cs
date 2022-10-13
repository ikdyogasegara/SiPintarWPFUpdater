using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Views.Billing.Supervisi.RekeningL2T2
{
    public partial class KonfirmasiVerifikasiView : UserControl
    {
        private readonly RekeningL2T2ViewModel _viewModel;
        public KonfirmasiVerifikasiView(RekeningL2T2ViewModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (RekeningL2T2ViewModel)DataContext;

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void VerifikasiPassword(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);

            _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitPublishSemuaCommand).ExecuteAsync(TxtPassword.Password));
        }

        private void TxtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordPlaceholder.Visibility = !string.IsNullOrWhiteSpace(TxtPassword.Password) ? Visibility.Hidden : Visibility.Visible;

            _viewModel.ConfirmationPasswordForm = TxtPassword.Password;
        }
    }
}
