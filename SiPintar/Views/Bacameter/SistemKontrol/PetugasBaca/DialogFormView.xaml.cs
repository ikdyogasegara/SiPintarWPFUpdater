using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Bacameter.SistemKontrol;

namespace SiPintar.Views.Bacameter.SistemKontrol.PetugasBaca
{
    public partial class DialogFormView : UserControl
    {
        private readonly PetugasBacaViewModel viewModel;
        public DialogFormView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;

            viewModel = (PetugasBacaViewModel)DataContext;

            Title.Text = viewModel.IsEdit ? "Koreksi " : "Tambah ";
            Title.Text += "Data Petugas Baca";

            MaskPassword1.Visibility = Visibility.Visible;
            UnmaskPassword1.Visibility = Visibility.Collapsed;
            PasswordBtn.Content = "Tampilkan";

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
            PreviewKeyUp += new KeyEventHandler(HandleEnter);

            CheckButton();
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void HandleEnter(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Enter) &&
                !string.IsNullOrEmpty(Kode.Text) && !string.IsNullOrEmpty(Nama.Text) &&
                JenisPembaca.SelectedItem != null && !string.IsNullOrEmpty(NoHp.Text) &&
                !string.IsNullOrEmpty(TglLahir.Text) && !string.IsNullOrEmpty(TglMulaiKerja.Text) &&
                !string.IsNullOrEmpty(Alamat.Text) && Status.SelectedItem != null &&
                !string.IsNullOrEmpty(NamaPengguna.Text) && !string.IsNullOrEmpty(Password.Password) && notif.Text == ""
            )
            {
                viewModel.FormData.KodePetugasBaca = Kode.Text;
                viewModel.FormData.PetugasBaca = Nama.Text;
                viewModel.FormData.JenisPembaca = ((KeyValuePair<int, string>)JenisPembaca.SelectedItem).Value;
                viewModel.FormData.NoHp = NoHp.Text;
                viewModel.FormData.TglLahir = DateTime.Parse(TglLahir.Text);
                viewModel.FormData.TglMulaiKerja = DateTime.Parse(TglMulaiKerja.Text);
                viewModel.FormData.Alamat = Alamat.Text;
                viewModel.FormData.Keterangan = Keterangan.Text;
                viewModel.FormData.Status = ((KeyValuePair<int, string>)Status.SelectedItem).Key == 1;
                viewModel.FormData.NamaUser = NamaPengguna.Text;

                Submit_Click(null, null);
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);

            if (viewModel.IsEdit)
                _ = Task.Run(() => ((AsyncCommandBase)viewModel.OnSubmitEditFormCommand).ExecuteAsync(null));
            else
                _ = Task.Run(() => ((AsyncCommandBase)viewModel.OnSubmitAddFormCommand).ExecuteAsync(null));
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

        private void Combo_SelectionChanged(object sender, SelectionChangedEventArgs e) => CheckButton();

        private void CheckButton()
        {
            if (string.IsNullOrEmpty(Kode.Text) || string.IsNullOrEmpty(Nama.Text) ||
                JenisPembaca.SelectedItem == null || string.IsNullOrEmpty(NoHp.Text) ||
                string.IsNullOrEmpty(TglLahir.Text) || string.IsNullOrEmpty(TglMulaiKerja.Text) ||
                string.IsNullOrEmpty(Alamat.Text) || Status.SelectedItem == null ||
                string.IsNullOrEmpty(NamaPengguna.Text) || notif.Text != "" || !IsValidPassword())
                OkButton.IsEnabled = false;
            else
                OkButton.IsEnabled = true;
        }

        private bool IsValidPassword()
        {
            bool result = false;

            if (viewModel.IsEdit || (!string.IsNullOrEmpty(Password.Password) && ValidateCombination(Password.Password)))
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

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
            CheckButton();
        }

        private void Kode_TextChanged(object sender, TextChangedEventArgs e)
        {
            string notifikasi = null;

            notifikasi = viewModel.DataList.FirstOrDefault(i => i.KodePetugasBaca == Kode.Text) != null ? notif.Text = "Kode telah digunakan" : notif.Text = "";

            if (notifikasi != "" && !viewModel.IsEdit)
                notif.Visibility = Visibility.Visible;
            else
                notif.Visibility = Visibility.Collapsed;
        }

        private void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var current = (PasswordBox)sender;

            if (current.Name == "Password" && viewModel.FormData != null)
                viewModel.PasswordForm = current.Password;

            PasswordShow.Text = current.Password;

            CheckButton();
        }

        private void PasswordShow_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            var current = (TextBox)sender;

            if (current.Name == "PasswordShow" && viewModel.FormData != null)
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
