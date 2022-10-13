using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Bacameter.SistemKontrol;

namespace SiPintar.Views.Bacameter.SistemKontrol.PengaturanPutstamp
{
    public partial class ManualPDAView : UserControl
    {
        private readonly PengaturanPutstampViewModel viewModel;
        public ManualPDAView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;

            viewModel = (PengaturanPutstampViewModel)DataContext;

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
            PreviewKeyUp += new KeyEventHandler(HandleEnter);

            CheckButton();

            MaskPassword1.Visibility = Visibility.Visible;
            UnmaskPassword1.Visibility = Visibility.Collapsed;
            PasswordBtn.Content = "Tampilkan";
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void HandleEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && !string.IsNullOrEmpty(Password.Password))
            {
                viewModel.PasswordForm = Password.Password;
                Submit_Click(null, null);
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
            viewModel.OnOpenConfirmationPDACommand.Execute(null);
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

        private void CheckButton()
        {
            if (string.IsNullOrEmpty(Password.Password))
                OkButton.IsEnabled = false;
            else
                OkButton.IsEnabled = true;
        }

        private void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var current = (PasswordBox)sender;
            viewModel.PasswordForm = current.Password;

            PasswordShow.Text = current.Password;

            CheckButton();
        }

        private void PasswordShow_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            var current = (TextBox)sender;
            viewModel.PasswordForm = current.Text;

            Password.Password = current.Text;

            CheckButton();
        }

        private void ShowPasswordBtn_Click(object sender, RoutedEventArgs e)
        {
            var current = (Button)sender;
            MaskPassword1.Visibility = current.Content.ToString() == "Sembunyikan" ? Visibility.Visible : Visibility.Collapsed;
            UnmaskPassword1.Visibility = current.Content.ToString() == "Tampilkan" ? Visibility.Visible : Visibility.Collapsed;

            PasswordBtn.Content = current.Content.ToString() == "Tampilkan" ? "Sembunyikan" : "Tampilkan";
        }
    }
}
