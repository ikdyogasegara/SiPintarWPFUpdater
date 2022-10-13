using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Loket;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Views.Loket.Angsuran.BuatAngsuran
{
    public partial class FormSambunganBaruView : UserControl
    {
        private readonly AngsuranViewModel ViewModel;

        public FormSambunganBaruView(object dataContext)
        {
            InitializeComponent();
            ViewModel = dataContext as AngsuranViewModel;
            DataContext = ViewModel;

            PreviewKeyUp += new KeyEventHandler(HandleEsc);

            ViewModel.CurrentStep = 1;
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void Kembali_Click(object sender, RoutedEventArgs e)
        {
            var _viewModel = (AngsuranViewModel)DataContext;

            if (_viewModel.CurrentStep == 1)
                DialogHost.CloseDialogCommand.Execute(null, null);
            else if (_viewModel.CurrentStep == 2)
                _viewModel.CurrentStep = 1;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            var _viewModel = (AngsuranViewModel)DataContext;

            if (_viewModel.CurrentStep == 1 && _viewModel.SelectedPelanggan != null)
            {
                _viewModel.CurrentStep = 2;

                _ = ((AsyncCommandBase)_viewModel.GetTagihanDetailCommand).ExecuteAsync(null);
            }
            else if (_viewModel.CurrentStep == 2)
            {

                if (ViewModel.JumlahDpMinForm > Convert.ToDecimal(UangMuka.Text))
                {
                    Application.Current.Dispatcher.Invoke(delegate
                    {
                        _ = DialogHost.Show(new DialogGlobalView("Tambah Angsuran Sambungan Baru", "Jumlah uang muka harus lebih besar dari DP Minimal  !", "warning"), "AngsuranSubDialog");
                    });
                    return;
                }

                Proses();
            }
        }

        private void Proses()
        {
            DialogHost.CloseDialogCommand.Execute(null, null);

            var _viewModel = (AngsuranViewModel)DataContext;
            _ = ((AsyncCommandBase)_viewModel.OnSubmitAddSambunganBaruCommand).ExecuteAsync(null);
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            ViewModel.UangMukaForm = Convert.ToDecimal(UangMuka.Text);
            ViewModel.TerminForm = Convert.ToInt32(Termin.Text);
            ViewModel.TglTerminForm = Convert.ToDateTime(Tgl.Text);

            if (string.IsNullOrWhiteSpace(NoHp.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrWhiteSpace(NoTelp.Text))
                OkButton.IsEnabled = false;
            else
            {
                OkButton.IsEnabled = true;
            }
        }

        private void CheckFormSetp2_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            ViewModel.UangMukaForm = UangMuka.Text != "" ? Convert.ToDecimal(UangMuka.Text) : 0;
            ViewModel.TerminForm = Termin.Text != "" ? Convert.ToInt32(Termin.Text) : 0;
            ViewModel.TglTerminForm = Tgl.Text != "" ? Convert.ToDateTime(Tgl.Text) : DateTime.Now;

            if (string.IsNullOrWhiteSpace(UangMuka.Text) || UangMuka.Text == "0")
                OkButton.IsEnabled = false;
            else if (string.IsNullOrWhiteSpace(Termin.Text) || Termin.Text == "0")
                OkButton.IsEnabled = false;
            else if (string.IsNullOrWhiteSpace(Tgl.Text))
                OkButton.IsEnabled = false;
            else
            {
                OkButton.IsEnabled = true;
                _ = ((AsyncCommandBase)ViewModel.GetAngsuranNonAirKalkulasiCommand).ExecuteAsync(null);
            }

            ViewModel.SisaAngsur = ViewModel.SelectedRekeningNonAir.Total - Convert.ToDecimal(UangMuka.Text) ?? 0;
        }

        private void Select_Click(object sender = null, System.Windows.RoutedEventArgs e = null)
        {

            if (string.IsNullOrWhiteSpace(NoHp.Text) || string.IsNullOrWhiteSpace(NoTelp.Text))
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    _ = DialogHost.Show(new DialogGlobalView("Tambah Angsuran Sambungan Baru", "Pastikan 'No.Hp' dan 'No Telp' tidak boleh kosong !", "warning"), "AngsuranSubDialog");
                });
                return;
            }


            var item = DataGridContent.SelectedItem;
            var _viewModel = (AngsuranViewModel)DataContext;

            _viewModel.CurrentStep = 2;
            OkButton.IsEnabled = false;
            //_viewModel.JumlahDpMinForm = _viewModel.SelectedRekeningNonAir.Total * 10 / 100 ?? 0;

            _ = ((AsyncCommandBase)_viewModel.GetAngsuranNonAirKalkulasiCommand).ExecuteAsync(null);
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
