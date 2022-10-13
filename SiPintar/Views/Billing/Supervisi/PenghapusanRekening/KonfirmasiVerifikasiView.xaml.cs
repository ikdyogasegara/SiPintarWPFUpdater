using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Views.Billing.Supervisi.PenghapusanRekening
{
    /// <summary>
    /// Interaction logic for KonfirmasiVerifikasiView.xaml
    /// </summary>
    public partial class KonfirmasiVerifikasiView : UserControl
    {
        private readonly PenghapusanRekeningViewModel _viewModel;
        public KonfirmasiVerifikasiView(PenghapusanRekeningViewModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (PenghapusanRekeningViewModel)DataContext;
        }

        private void VerifikasiPassword(object sender, RoutedEventArgs e)
        {
            _viewModel.Password = TxtPassword.Password;
            DialogHost.CloseDialogCommand.Execute(null, null);

            _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitVerifikasiHapusRekeningCommand).ExecuteAsync(null));
        }

        private void TxtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordPlaceholder.Visibility = !string.IsNullOrWhiteSpace(TxtPassword.Password) ? Visibility.Hidden : Visibility.Visible;
        }
    }
}
