using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Main;

namespace SiPintar.Views.Main.DaftarPengguna
{
    public partial class DialogFormView : UserControl
    {
        private readonly DaftarPenggunaViewModel _viewModel;
        public DialogFormView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (DaftarPenggunaViewModel)DataContext;

            Title.Text = _viewModel.IsEdit ? "Koreksi " : "Tambah ";
            Title.Text += "Pengguna PDAM Pintar";

            OkButton.Content = _viewModel.IsEdit ? "Simpan" : "Tambah";

            CheckButton();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);

            MaskPassword1.Visibility = Visibility.Visible;
            UnmaskPassword1.Visibility = Visibility.Collapsed;
            PasswordBtn.Content = "Tampilkan";

            MaskPassword2.Visibility = Visibility.Visible;
            UnmaskPassword2.Visibility = Visibility.Collapsed;
            KonfirmasiPasswordBtn.Content = "Tampilkan";
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.FormData.IdRole = _viewModel.SelectedRole == null ? _viewModel.FormData.IdLoket : _viewModel.SelectedRole.IdRole;
            _viewModel.FormData.IdLoket = _viewModel.SelectedRole == null ? _viewModel.FormData.IdLoket : _viewModel.SelectedLoket.IdLoket;
            DialogHost.CloseDialogCommand.Execute(null, null);

            if (_viewModel.IsEdit)
                _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitEditUserCommand).ExecuteAsync(null));
            else
                _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitAddUserCommand).ExecuteAsync(null));
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => CheckButton();

        private void CheckButton()
        {
            if (_viewModel.IsEdit)
            {
                if (string.IsNullOrEmpty(Nama.Text) || string.IsNullOrEmpty(NamaUser.Text) || HakAkses.SelectedItem == null ||
                    string.IsNullOrEmpty(NoIdentitas.Text) || Status.SelectedItem == null)
                    OkButton.IsEnabled = false;
                else
                    OkButton.IsEnabled = true;
            }
            else
            {
                if (string.IsNullOrEmpty(Nama.Text) || string.IsNullOrEmpty(NamaUser.Text) || HakAkses.SelectedItem == null ||
                    string.IsNullOrEmpty(Password.Password) || string.IsNullOrEmpty(KonfirmasiPassword.Password) || !IsValidPassword() ||
                    string.IsNullOrEmpty(NoIdentitas.Text) || Status.SelectedItem == null)
                    OkButton.IsEnabled = false;
                else
                    OkButton.IsEnabled = true;
            }
        }

        private bool IsValidPassword()
        {
            bool result = false;

            if (!string.IsNullOrEmpty(Password.Password) && !string.IsNullOrEmpty(KonfirmasiPassword.Password) &&
                ValidateCombination(Password.Password) && Password.Password == KonfirmasiPassword.Password)
                result = true;

            return result;
        }

        private bool ValidateCombination(string input)
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");

            var isValidated = hasNumber.IsMatch(input) && hasUpperChar.IsMatch(input) && hasMinimum8Chars.IsMatch(input);

            InfoPassword.Foreground = !isValidated ? (SolidColorBrush)Application.Current.Resources["Modul"] : (SolidColorBrush)Application.Current.Resources["SuccessSofter"];

            return isValidated;
        }

        private void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var current = (PasswordBox)sender;

            if (current.Name == "Password" && _viewModel.FormData != null)
                _viewModel.FormData.Password = current.Password;

            switch (current.Name)
            {
                case "Password":
                    PasswordShow.Text = current.Password;
                    break;
                case "KonfirmasiPassword":
                    KonfirmasiPasswordShow.Text = current.Password;
                    break;
                default:
                    break;
            }

            CheckButton();
        }

        private void PasswordShow_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            var current = (TextBox)sender;

            if (current.Name == "PasswordShow" && _viewModel.FormData != null)
                _viewModel.FormData.Password = current.Text;

            switch (current.Name)
            {
                case "PasswordShow":
                    Password.Password = current.Text;
                    break;
                case "KonfirmasiPasswordShow":
                    KonfirmasiPassword.Password = current.Text;
                    break;
                default:
                    break;
            }

            CheckButton();
        }

        private void ShowPasswordBtn_Click(object sender, RoutedEventArgs e)
        {
            var current = (Button)sender;

            if (current.Name == "PasswordBtn")
            {
                MaskPassword1.Visibility = current.Content.ToString() == "Sembunyikan" ? Visibility.Visible : Visibility.Collapsed;
                UnmaskPassword1.Visibility = current.Content.ToString() == "Tampilkan" ? Visibility.Visible : Visibility.Collapsed;

                PasswordBtn.Content = current.Content.ToString() == "Tampilkan" ? "Sembunyikan" : "Tampilkan";
            }
            else if (current.Name == "KonfirmasiPasswordBtn")
            {
                MaskPassword2.Visibility = current.Content.ToString() == "Sembunyikan" ? Visibility.Visible : Visibility.Collapsed;
                UnmaskPassword2.Visibility = current.Content.ToString() == "Tampilkan" ? Visibility.Visible : Visibility.Collapsed;

                KonfirmasiPasswordBtn.Content = current.Content.ToString() == "Tampilkan" ? "Sembunyikan" : "Tampilkan";
            }
        }
    }
}
