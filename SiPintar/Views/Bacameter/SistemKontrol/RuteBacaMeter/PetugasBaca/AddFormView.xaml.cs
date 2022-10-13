using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Bacameter.SistemKontrol.RuteBacaMeter;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Views.Bacameter.SistemKontrol.RuteBacaMeter.PetugasBaca
{
    public partial class AddFormView : UserControl
    {
        private readonly PetugasBacaViewModel ViewModel;

        public AddFormView(object dataContext)
        {
            InitializeComponent();
            ViewModel = dataContext as PetugasBacaViewModel;
            DataContext = ViewModel;

            PreviewKeyUp += new KeyEventHandler(HandleEsc);

            ToleransiMinus.Text = "0";
            ToleransiPlus.Text = "0";
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape && !ViewModel.IsLoadingForm)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            var _viewModel = (PetugasBacaViewModel)DataContext;

            if (_viewModel.SelectedRayon != null)
                Proses();
            else
                ShowAlert();
        }

        private void Proses()
        {
            var _viewModel = (PetugasBacaViewModel)DataContext;
            _ = ((AsyncCommandBase)_viewModel.OnSubmitAddFormCommand).ExecuteAsync(null);
        }

        private void ShowAlert()
        {
            _ = DialogHost.Show(new DialogGlobalView(
                    "Form Tidak Lengkap",
                    "Mohon Pilih Rayon.",
                    "warning",
                    "Oke",
                    false,
                    "bacameter"
                ), "RuteBacaMeterSubDialog");
        }

        private void Subtract_Click(object sender, RoutedEventArgs e)
        {
            var current = ((Button)sender).Tag.ToString();

            string value;
            switch (current)
            {
                case "SubtractMinus":
                    value = string.IsNullOrEmpty(ToleransiMinus.Text) ? "0" : ToleransiMinus.Text;
                    ToleransiMinus.Text = Convert.ToInt32(value) > 0 ? (Convert.ToInt32(value) - 1).ToString() : value;
                    ViewModel.ToleransiMinus = Convert.ToInt32(ToleransiMinus.Text);
                    break;
                case "SubtractPlus":
                    value = string.IsNullOrEmpty(ToleransiPlus.Text) ? "0" : ToleransiPlus.Text;
                    ToleransiPlus.Text = Convert.ToInt32(value) > 0 ? (Convert.ToInt32(value) - 1).ToString() : value;
                    ViewModel.ToleransiPlus = Convert.ToInt32(ToleransiPlus.Text);
                    break;
                default:
                    break;
            }

            CheckTanggal();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var current = ((Button)sender).Tag.ToString();

            string value;
            bool infoAllowed;
            switch (current)
            {
                case "AddMinus":
                    value = string.IsNullOrEmpty(ToleransiMinus.Text) ? "0" : ToleransiMinus.Text;
                    infoAllowed = Convert.ToInt32(AwalInfo.Text) > 1;
                    ToleransiMinus.Text = Convert.ToInt32(value) < 31 && infoAllowed ? (Convert.ToInt32(value) + 1).ToString() : value;
                    ViewModel.ToleransiMinus = Convert.ToInt32(ToleransiMinus.Text);
                    break;
                case "AddPlus":
                    value = string.IsNullOrEmpty(ToleransiPlus.Text) ? "0" : ToleransiPlus.Text;
                    infoAllowed = Convert.ToInt32(AkhirInfo.Text) < 31;
                    ToleransiPlus.Text = Convert.ToInt32(value) < 31 && infoAllowed ? (Convert.ToInt32(value) + 1).ToString() : value;
                    ViewModel.ToleransiPlus = Convert.ToInt32(ToleransiPlus.Text);
                    break;
                default:
                    break;
            }

            CheckTanggal();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void CheckTanggal()
        {
            if (TglBaca.SelectedItem == null || string.IsNullOrEmpty(ToleransiMinus.Text) || string.IsNullOrEmpty(ToleransiPlus.Text))
            {
                InfoSection.Visibility = Visibility.Collapsed;
                return;
            }

            AwalInfo.Text = (ViewModel.TanggalBacaForm.Value.Key - Convert.ToInt32(ToleransiMinus.Text)).ToString();
            AkhirInfo.Text = (ViewModel.TanggalBacaForm.Value.Key + Convert.ToInt32(ToleransiPlus.Text)).ToString();

            InfoSection.Visibility = Visibility.Visible;
        }

        private void Toleransi_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            CheckTanggal();
        }

        private void TglBaca_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckTanggal();
        }
    }
}
