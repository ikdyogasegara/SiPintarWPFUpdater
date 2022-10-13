using System;
using System.Diagnostics;
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
    public partial class FormTunggakanView : UserControl
    {
        private readonly AngsuranViewModel _vm;

        public FormTunggakanView(object dataContext)
        {
            InitializeComponent();
            _vm = dataContext as AngsuranViewModel;
            DataContext = _vm;

            PreviewKeyUp += new KeyEventHandler(HandleEsc);

            _vm.CurrentStep = 1;
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
            {
                DpMax.Text = "0";
                NoHp.Text = "";
                NoTelp.Text = "";
                _viewModel.CurrentStep = 1;
            }
            else
            {
                _viewModel.CurrentStep = 2;
                CheckForm_PreviewKeyUp(null, null);
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            var _viewModel = (AngsuranViewModel)DataContext;

            if (_viewModel.CurrentStep == 1)
            {
                _viewModel.CurrentStep = 2;
                _ = ((AsyncCommandBase)_viewModel.GetTagihanDetailCommand).ExecuteAsync(null);
                CheckFormSetp2_PreviewKeyUp(null, null);

            }
            else if (_viewModel.CurrentStep == 2)
            {
                UangMuka.Text = "0";
                Termin.Text = "0";
                _vm.UangMukaForm = 0;
                _vm.SisaAngsur = 0;
                _vm.AngsuranKalkulasi = null;
                _vm.AngsuranKalkulasiGroup = null;
                OkButton.IsEnabled = false;
                _viewModel.CurrentStep = 3;
                bool IsPiutangSelected = _viewModel.RekeningAirList.FirstOrDefault(i => i.IsSelected == true) != null;
                if (!IsPiutangSelected)
                {
                    ShowAlert();
                    return;
                }
                if (_vm.RekeningAirList != null)
                {
                    _vm.SisaAngsur = _vm.RekeningAirList.Where(x => x.IsSelected).Sum(x => x.Total ?? 0) - _vm.UangMukaForm;
                }
            }
            else if (_viewModel.CurrentStep == 3)
            {
                Proses();
            }
        }

        private void Proses()
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
            _ = ((AsyncCommandBase)_vm.OnSubmitAddTunggakanCommand).ExecuteAsync(null);
        }

        private void ShowAlert()
        {
            _ = DialogHost.Show(new DialogGlobalView(
                    "Form Tidak Lengkap",
                    "Mohon Lengkapi Form.",
                    "warning",
                    "Oke",
                    false,
                    "loket"
                ), "AngsuranSubDialog");
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (sender is TextBox txb)
            {
                if (txb.Name == "DpMax")
                {
                    _vm.DpMaxForm = string.IsNullOrWhiteSpace(DpMax.Text) ? decimal.Zero : Convert.ToDecimal(DpMax.Text);
                    if (_vm.DpMaxForm > 50)
                    {
                        _vm.DpMaxForm = 50;
                    }
                    _vm.MaxDpUpdate();
                }
            }
            _vm.UangMukaForm = Convert.ToDecimal(UangMuka.Text);
            _vm.TerminForm = Convert.ToInt32(Termin.Text);
            _vm.TglTerminForm = Convert.ToDateTime(Tgl.Text);


            if (DpMax.Text == "0" || string.IsNullOrWhiteSpace(DpMax.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrWhiteSpace(NoHp.Text))
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
            _vm.UangMukaForm = UangMuka.Text != "" ? Convert.ToDecimal(UangMuka.Text) : 0;
            _vm.TerminForm = Termin.Text != "" ? Convert.ToInt32(Termin.Text) : 0;
            _vm.TglTerminForm = Tgl.Text != "" ? Convert.ToDateTime(Tgl.SelectedDate) : DateTime.Now;
            if (string.IsNullOrWhiteSpace(UangMuka.Text) || UangMuka.Text == "0")
                OkButton.IsEnabled = false;
            else if (string.IsNullOrWhiteSpace(Termin.Text) || Termin.Text == "0")
                OkButton.IsEnabled = false;
            else if (string.IsNullOrWhiteSpace(Tgl.Text))
                OkButton.IsEnabled = false;
            else
            {
                OkButton.IsEnabled = true;
                _ = ((AsyncCommandBase)_vm.GetAngsuranKalkulasiCommand).ExecuteAsync(null);
            }

            NotifDpKurang.Visibility = _vm.UangMukaForm < _vm.JumlahDpMinForm ? Visibility.Visible : Visibility.Collapsed;
            if (_vm.RekeningAirList != null)
            {
                _vm.SisaAngsur = _vm.RekeningAirList.Where(x => x.IsSelected).Sum(x => x.Total ?? 0) - _vm.UangMukaForm;
            }
            else
            {
                _vm.SisaAngsur = 0;
            }
        }

        private void Select_Click(object sender = null, System.Windows.RoutedEventArgs e = null)
        {
            var item = DataGridContent.SelectedItem;
            var _viewModel = (AngsuranViewModel)DataContext;

            _viewModel.SelectedPelanggan = item as MasterPelangganAirDto;
            _viewModel.CurrentStep = 2;
            OkButton.IsEnabled = false;

            _ = ((AsyncCommandBase)_viewModel.GetTagihanDetailCommand).ExecuteAsync(null);

            _vm.UangMukaForm = 0;
            _vm.TerminForm = 0;
            _vm.TglTerminForm = DateTime.Now;
            CheckFormSetp2_PreviewKeyUp(null, null);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (_vm != null)
            {
                _vm.CheckUpdate();
            }
        }

        private void DpMax_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex(@"^[0-9]+$");
            if (sender is TextBox txb)
            {
                if (!regex.IsMatch(txb.Text + e.Text))
                {
                    e.Handled = true;
                }
            }
        }
    }
}
