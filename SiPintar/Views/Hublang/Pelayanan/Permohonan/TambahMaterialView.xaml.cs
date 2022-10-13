using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Views.Hublang.Pelayanan.Permohonan
{
    public partial class TambahMaterialView : UserControl
    {
        private readonly PermohonanViewModel _viewModel;
        private readonly MasterMaterialDto _selectedMaterial;
        private readonly bool? _flagBiayaDibebankanKePdam;
        private readonly bool? _flagDialihkanKeVendor;

        public TambahMaterialView(PermohonanViewModel dataContext, bool? flagBiayaDibebankanKePdam = false, bool? flagDialihkanKeVendor = false)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (PermohonanViewModel)DataContext;
            _selectedMaterial = _viewModel.SelectedDaftarMaterial;
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
            if (_viewModel.SelectedPaketRabPipaPersil != null && _selectedMaterial != null && Qty.Text != "")
            {
                _viewModel.SelectedDaftarMaterial = _selectedMaterial;

                decimal? persenPpnMaterial = _viewModel.SelectedPaketRabPipaPersil.PpnMaterial ?? 0;
                var persenKeuntungan = _viewModel.SelectedPaketRabPipaPersil.PersentaseKeuntungan ?? 0;
                var persenJasaDariBahan = _viewModel.SelectedPaketRabPipaPersil.PersentaseJasaDariBahan ?? 0;

                var qty = DecimalValidationHelper.FormatStringIdToDecimal(Qty.Text);

                var tipe = "Material";

                if (CheckTambahan.IsChecked == true)
                {
                    tipe = "Material Tambahan";
                    persenPpnMaterial = _viewModel.SelectedPaketRabPipaPersil.PpnMaterialTambahan ?? 0;
                }

                decimal? ppn = persenPpnMaterial / 100 * _viewModel.SelectedDaftarMaterial.HargaJual * qty;
                decimal? keuntungan = persenKeuntungan / 100 * (_viewModel.SelectedDaftarMaterial.HargaJual * qty);

                decimal? jasaDariBahan = 0;

                if (CheckTidakDikenakanJasaBahan.IsChecked == false)
                {
                    jasaDariBahan = persenJasaDariBahan / 100 * (_viewModel.SelectedDaftarMaterial.HargaJual * qty);
                }

                if (_viewModel.TipeRab == "Pipa Persil")
                {
                    _viewModel.DetailRabPipaPersil.Add(new DetailRabWpf
                    {
                        Kode = _viewModel.SelectedDaftarMaterial.KodeMaterial,
                        Tipe = tipe,
                        Uraian = _viewModel.SelectedDaftarMaterial.NamaMaterial,
                        HargaSatuan = _viewModel.SelectedDaftarMaterial.HargaJual,
                        Satuan = _viewModel.SelectedDaftarMaterial.Satuan,
                        Qty = qty,
                        Jumlah = _viewModel.SelectedDaftarMaterial.HargaJual * qty,
                        Ppn = ppn,
                        Keuntungan = keuntungan,
                        JasaDariBahan = jasaDariBahan,
                        Total = (_viewModel.SelectedDaftarMaterial.HargaJual * qty) + ppn + keuntungan + jasaDariBahan,
                        FlagDialihkanKevendor = _flagDialihkanKeVendor,
                        FlagBiayaDibebankanKePdam = _flagBiayaDibebankanKePdam
                    });
                }
                else if (_viewModel.TipeRab == "Pipa Distribusi")
                {
                    _viewModel.DetailRabPipaDistribusi.Add(new DetailRabWpf
                    {
                        Kode = _viewModel.SelectedDaftarMaterial.KodeMaterial,
                        Tipe = tipe,
                        Uraian = _viewModel.SelectedDaftarMaterial.NamaMaterial,
                        HargaSatuan = _viewModel.SelectedDaftarMaterial.HargaJual,
                        Satuan = _viewModel.SelectedDaftarMaterial.Satuan,
                        Qty = qty,
                        Jumlah = _viewModel.SelectedDaftarMaterial.HargaJual * qty,
                        Ppn = ppn,
                        Keuntungan = keuntungan,
                        JasaDariBahan = jasaDariBahan,
                        Total = (_viewModel.SelectedDaftarMaterial.HargaJual * qty) + ppn + keuntungan + jasaDariBahan,
                        FlagDialihkanKevendor = _flagDialihkanKeVendor,
                        FlagBiayaDibebankanKePdam = _flagBiayaDibebankanKePdam
                    });
                }

                _viewModel.OnHitungRabCommand.Execute(null);
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
        }
    }
}
