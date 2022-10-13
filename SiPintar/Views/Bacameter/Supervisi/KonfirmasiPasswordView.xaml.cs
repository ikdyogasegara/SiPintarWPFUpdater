using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Bacameter;

namespace SiPintar.Views.Bacameter.Supervisi
{
    public partial class KonfirmasiPasswordView : UserControl
    {
        private readonly SupervisiViewModel _viewModel;
        private readonly bool _isVerifikasi;
        private readonly bool _ignoreCheckbox;
        public KonfirmasiPasswordView(SupervisiViewModel dataContext, bool IsVerifikasi, bool IgnoreCheckbox = true)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (SupervisiViewModel)DataContext;

            _isVerifikasi = IsVerifikasi;
            _ignoreCheckbox = IgnoreCheckbox;

            Section.Text = IsVerifikasi ? "Verifikasi" : "Unverifikasi";
        }

        private void VerifikasiPassword(object sender = null, RoutedEventArgs e = null)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);

            _viewModel.KonfirmasiPassword = TxtPassword.Password;

            if (_isVerifikasi)
                _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitVerifikasiCommand).ExecuteAsync(_ignoreCheckbox));
            else
                _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitUnverifikasiCommand).ExecuteAsync(_ignoreCheckbox));
        }

        private void TxtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordPlaceholder.Visibility = !string.IsNullOrWhiteSpace(TxtPassword.Password) ? Visibility.Hidden : Visibility.Visible;
        }

        private void TxtPassword_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Return:
                    {
                        if (!string.IsNullOrWhiteSpace(TxtPassword.Password))
                        {
                            VerifikasiPassword();
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
