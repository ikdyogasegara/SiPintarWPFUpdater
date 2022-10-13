using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Views.Billing.Supervisi.RekeningLimbah
{
    public partial class KonfirmasiVerifikasiView : UserControl
    {
        private readonly RekeningLimbahViewModel _viewModel;
        public KonfirmasiVerifikasiView(RekeningLimbahViewModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (RekeningLimbahViewModel)DataContext;

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
