using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Views.Hublang.Pelayanan.Permohonan
{
    public partial class TambahOngkosView : UserControl
    {
        private readonly PermohonanViewModel _viewModel;
        private readonly MasterOngkosDto _selectedOngkos;
        private readonly bool? _flagBiayaDibebankanKePdam;
        private readonly bool? _flagDialihkanKeVendor;

        public TambahOngkosView(PermohonanViewModel dataContext, bool? flagBiayaDibebankanKePdam = false, bool? flagDialihkanKeVendor = false)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (PermohonanViewModel)DataContext;
            _selectedOngkos = _viewModel.SelectedDaftarOngkos;
            _flagBiayaDibebankanKePdam = flagBiayaDibebankanKePdam;
            _flagDialihkanKeVendor = flagDialihkanKeVendor;
            CheckSubmitStatus();
            InputValidation();
            PreviewKeyUp += new KeyEventHandler(HandleKey);
        }

        private void HandleKey(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && BtnSimpan.IsEnabled == true)
            {
                BtnSimpan_Click(null, null);
            }
        }

        private void InputValidation()
        {
            try
            {
                Qty.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
                Qty.GotFocus += DecimalValidationHelper.Object_GotFocus;
                Qty.LostFocus += DecimalValidationHelper.Object_LostFocus;
            }
            catch
            {
                Qty.Text = "0";
            }
        }

        private void ChildInput_PreviewKeyUp(object sender, KeyEventArgs e) => CheckSubmitStatus();

        private void CheckSubmitStatus()
        {
            if (DecimalValidationHelper.FormatStringIdToDecimal(Qty.Text) > 0)
            {
                BtnSimpan.IsEnabled = true;
            }
            else
            {
                BtnSimpan.IsEnabled = false;
            }
        }

        private void BtnSimpan_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedPaketRabPipaPersil != null && _selectedOngkos != null && Qty.Text != "")
            {
                _viewModel.SelectedDaftarOngkos = _selectedOngkos;

                var persenPpnOngkos = _viewModel.SelectedPaketRabPipaPersil.PpnOngkos ?? 0;
                var persenKeuntungan = _viewModel.SelectedPaketRabPipaPersil.PersentaseKeuntungan ?? 0;

                var qty = DecimalValidationHelper.FormatStringIdToDecimal(Qty.Text);

                var tipe = "Ongkos";

                if (CheckTambahan.IsChecked == true)
                {
                    tipe = "Ongkos Tambahan";
                    persenPpnOngkos = _viewModel.SelectedPaketRabPipaPersil.PpnOngkosTambahan ?? 0;
                }
                decimal? ppn = persenPpnOngkos / 100 * (_viewModel.SelectedDaftarOngkos.Biaya * qty);
                decimal? keuntungan = persenKeuntungan / 100 * (_viewModel.SelectedDaftarOngkos.Biaya * qty);
                decimal? jasaDariBahan = 0;

                if (_viewModel.TipeRab == "Pipa Persil")
                {
                    _viewModel.DetailRabPipaPersil.Add(new DetailRabWpf
                    {
                        Kode = _viewModel.SelectedDaftarOngkos.KodeOngkos,
                        Tipe = tipe,
                        Uraian = _viewModel.SelectedDaftarOngkos.NamaOngkos,
                        HargaSatuan = _viewModel.SelectedDaftarOngkos.Biaya,
                        Satuan = _viewModel.SelectedDaftarOngkos.Satuan,
                        Qty = qty,
                        Jumlah = _viewModel.SelectedDaftarOngkos.Biaya * qty,
                        Ppn = ppn,
                        Keuntungan = keuntungan,
                        JasaDariBahan = jasaDariBahan,
                        PostBiaya = _viewModel.SelectedDaftarOngkos.PostBiaya,
                        Kelompok = _viewModel.SelectedDaftarOngkos.Kelompok,
                        Total = (_viewModel.SelectedDaftarOngkos.Biaya * qty) + ppn + keuntungan + jasaDariBahan,
                        FlagDialihkanKevendor = false, // ongkos ga bisa ditannggung vendor
                        FlagBiayaDibebankanKePdam = _flagBiayaDibebankanKePdam
                    });
                }
                else if (_viewModel.TipeRab == "Pipa Distribusi")
                {
                    _viewModel.DetailRabPipaDistribusi.Add(new DetailRabWpf
                    {
                        Kode = _viewModel.SelectedDaftarOngkos.KodeOngkos,
                        Tipe = tipe,
                        Uraian = _viewModel.SelectedDaftarOngkos.NamaOngkos,
                        HargaSatuan = _viewModel.SelectedDaftarOngkos.Biaya,
                        Satuan = _viewModel.SelectedDaftarOngkos.Satuan,
                        Qty = qty,
                        Jumlah = _viewModel.SelectedDaftarOngkos.Biaya * qty,
                        Ppn = ppn,
                        Keuntungan = keuntungan,
                        JasaDariBahan = jasaDariBahan,
                        PostBiaya = _viewModel.SelectedDaftarOngkos.PostBiaya,
                        Kelompok = _viewModel.SelectedDaftarOngkos.Kelompok,
                        Total = (_viewModel.SelectedDaftarOngkos.Biaya * qty) + ppn + keuntungan + jasaDariBahan,
                        FlagDialihkanKevendor = false,// ongkos ga bisa ditannggung vendor
                        FlagBiayaDibebankanKePdam = _flagBiayaDibebankanKePdam
                    });
                }

                _viewModel.OnHitungRabCommand.Execute(null);
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
        }
    }
}
