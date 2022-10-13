using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Views.Gudang.ProsesTransaksi.DaftarBarangMasuk
{
    /// <summary>
    /// Interaction logic for DialogTambahBarang.xaml
    /// </summary>
    public partial class DialogKoreksiTambahBarang : UserControl
    {
        private readonly DaftarBarangMasukViewModel _vm;
        private readonly bool _isEdit;
        private MasterBarangDto selectedBarang { get; set; }
        private bool BtnSimpanEnabled
        {
            get
            {
                return Kategori.SelectedItem != null && Tanggal.SelectedDate.HasValue;
            }
        }

        public DialogKoreksiTambahBarang(object dataContext, bool isEdit = false)
        {
            InitializeComponent();
            _vm = dataContext as DaftarBarangMasukViewModel;
            _isEdit = isEdit;
            DataContext = _vm;

            BtnSimpan.IsEnabled = BtnSimpanEnabled;
            if (isEdit) { SetupEdit(); }

            PreviewKeyUp += DialogKoreksiTambahBarang_PreviewKeyUp;
            Price.GotFocus += DecimalValidationHelper.Object_GotFocus;
            Price.LostFocus += DecimalValidationHelper.Object_LostFocus;
            Qty.GotFocus += DecimalValidationHelper.Object_GotFocus;
            Qty.LostFocus += DecimalValidationHelper.Object_LostFocus;

            Price.TextChanged += TextChanged;
            Qty.TextChanged += TextChanged;
            Kategori.SelectionChanged += Kategori_SelectionChanged;
            Tanggal.SelectedDateChanged += Tanggal_SelectedDateChanged;
            BtnSimpan.Click += BtnSimpan_Click;
        }

        private void BtnSimpan_Click(object sender, RoutedEventArgs e)
        {
            if (!_isEdit)
            {
                _vm.OnTambahBarangCommand.Execute(new Dictionary<string, dynamic>()
                {
                    { "IdBarangMasuk" , _vm.SelectedDaftarBarangMasuk.IdBarangMasuk },
                    { "IdBarang" , selectedBarang.IdBarang },
                    { "IdKategoriBarangMasuk" , ((MasterKategoriBarangMasukDto)Kategori.SelectedItem).IdKategoriBarangMasuk },
                    { "Qty" , DecimalValidationHelper.FormatStringIdToDecimal(Qty.Text) },
                    { "Harga_Tanpa_Ppn" , DecimalValidationHelper.FormatStringIdToDecimal(Price.Text) },
                    { "WaktuDiterima" , Tanggal.SelectedDate.Value }
                });
            }
            else
            {
                _vm.OnKoreksiBarangCommand.Execute(new Dictionary<string, dynamic>()
                {
                    { "IdBarangMasuk" , _vm.SelectedDaftarBarangMasuk.IdBarangMasuk },
                    { "IdBarang" , _vm.SelectedDaftarBarangMasukDetail.IdBarang },
                    { "IdKategoriBarangMasuk" , ((MasterKategoriBarangMasukDto)Kategori.SelectedItem).IdKategoriBarangMasuk },
                    { "Qty" , DecimalValidationHelper.FormatStringIdToDecimal(Qty.Text) },
                    { "Harga_Tanpa_Ppn" , DecimalValidationHelper.FormatStringIdToDecimal(Price.Text) },
                });
            }
        }

        private void Tanggal_SelectedDateChanged(object sender, SelectionChangedEventArgs e) => CheckSubmit();

        private void Kategori_SelectionChanged(object sender, SelectionChangedEventArgs e) => CheckSubmit();
        private void TextChanged(object sender, TextChangedEventArgs e) => CheckSubmit();

        private void DialogKoreksiTambahBarang_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F3 && !_isEdit)
            {
                _ = OpenPilihBarangAsync();
            }
        }

        private async Task OpenPilihBarangAsync()
        {
            await DialogHelper.ShowCustomDialogViewAsync(false, true, "DaftarBarangMasukTambahDialog", () => new DialogPilihBarangMaterialView(SetData, _vm.RestApi));
        }

        public bool SetData(MasterBarangDto param)
        {
            selectedBarang = param;
            NamaBarang.Text = selectedBarang.NamaBarang;
            KodeBarang.Text = selectedBarang.KodeBarang;
            Price.Text = DecimalValidationHelper.FormatDecimalToStringId(decimal.Zero);
            Qty.Text = DecimalValidationHelper.FormatDecimalToStringId(1);
            CheckSubmit();
            return true;
        }


        public void SetupEdit()
        {
            Title.Text = "Koreksi Barang";
            NamaBarang.Text = _vm.SelectedDaftarBarangMasukDetail.NamaBarang;
            KodeBarang.Text = _vm.SelectedDaftarBarangMasukDetail.KodeBarang;
            Qty.Text = DecimalValidationHelper.FormatDecimalToStringId(_vm.SelectedDaftarBarangMasukDetail.Qty);
            Price.Text = DecimalValidationHelper.FormatDecimalToStringId(_vm.SelectedDaftarBarangMasukDetail.Harga_Tanpa_Ppn);
            Kategori.SelectedItem = _vm.KategoriBarangMasuk.FirstOrDefault(x => x.IdKategoriBarangMasuk == _vm.SelectedDaftarBarangMasukDetail.IdKategoriBarangMasuk);
            Tanggal.SelectedDate = _vm.SelectedDaftarBarangMasukDetail.WaktuDiterima;
            BtnCari.Visibility = Visibility.Collapsed;
            CheckSubmit();
        }

        private void CariClick(object sender, RoutedEventArgs e)
        {
            _ = OpenPilihBarangAsync();
        }

        private void CheckSubmit()
        {
            BtnSimpan.IsEnabled = (selectedBarang != null || _isEdit) && DecimalValidationHelper.FormatStringIdToDecimal(Qty.Text) >= 0 && DecimalValidationHelper.FormatStringIdToDecimal(Qty.Text) > 0 &&
                Kategori.SelectedItem != null && Tanggal.SelectedDate.HasValue;
        }
    }
}
