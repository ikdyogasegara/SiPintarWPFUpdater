using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Billing.Atribut.RuteBacaMeter;

namespace SiPintar.Views.Billing.Atribut.RuteBacaMeter.PerPetugasBaca
{
    public partial class EditFormView : UserControl
    {
        private readonly PerPetugasBacaViewModel _viewModel;

        public EditFormView(object dataContext)
        {
            InitializeComponent();
            _viewModel = dataContext as PerPetugasBacaViewModel;
            DataContext = _viewModel;

            PreviewKeyUp += new KeyEventHandler(HandleEsc);

            ToleransiMinus.Text = _viewModel.ToleransiMinus.ToString();
            ToleransiPlus.Text = _viewModel.ToleransiPlus.ToString();

            CheckTanggal();
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape && !_viewModel.IsLoadingForm)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            _ = ((AsyncCommandBase)_viewModel.OnSubmitEditFormCommand).ExecuteAsync(null);
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
                    _viewModel.ToleransiMinus = Convert.ToInt32(ToleransiMinus.Text);
                    break;
                case "SubtractPlus":
                    value = string.IsNullOrEmpty(ToleransiPlus.Text) ? "0" : ToleransiPlus.Text;
                    ToleransiPlus.Text = Convert.ToInt32(value) > 0 ? (Convert.ToInt32(value) - 1).ToString() : value;
                    _viewModel.ToleransiPlus = Convert.ToInt32(ToleransiPlus.Text);
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
                    _viewModel.ToleransiMinus = Convert.ToInt32(ToleransiMinus.Text);
                    break;
                case "AddPlus":
                    value = string.IsNullOrEmpty(ToleransiPlus.Text) ? "0" : ToleransiPlus.Text;
                    infoAllowed = Convert.ToInt32(AkhirInfo.Text) < 31;
                    ToleransiPlus.Text = Convert.ToInt32(value) < 31 && infoAllowed ? (Convert.ToInt32(value) + 1).ToString() : value;
                    _viewModel.ToleransiPlus = Convert.ToInt32(ToleransiPlus.Text);
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

            AwalInfo.Text = (_viewModel.TanggalBacaForm.Value.Key - Convert.ToInt32(ToleransiMinus.Text)).ToString();
            AkhirInfo.Text = (_viewModel.TanggalBacaForm.Value.Key + Convert.ToInt32(ToleransiPlus.Text)).ToString();

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
