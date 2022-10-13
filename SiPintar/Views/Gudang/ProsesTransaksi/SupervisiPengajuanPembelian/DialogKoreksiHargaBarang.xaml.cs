using System.Linq;
using System.Windows.Controls;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Views.Gudang.ProsesTransaksi.SupervisiPengajuanPembelian
{
    /// <summary>
    /// Interaction logic for DialogKoreksiHargaBarang.xaml
    /// </summary>
    public partial class DialogKoreksiHargaBarang : UserControl
    {
        private readonly SupervisiPengajuanPembelianViewModel Vm;

        public DialogKoreksiHargaBarang(object dataContext)
        {
            InitializeComponent();
            Vm = dataContext as SupervisiPengajuanPembelianViewModel;
            DataContext = Vm;

            TotalQty.GotFocus += DecimalValidationHelper.Object_GotFocus;
            TotalQty.LostFocus += DecimalValidationHelper.Object_LostFocus;
            TotalQty.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;

            HargaSetelahPpn.GotFocus += DecimalValidationHelper.Object_GotFocus;
            HargaSetelahPpn.LostFocus += DecimalValidationHelper.Object_LostFocus;
            HargaSetelahPpn.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;

            Kategori.SelectedItem = Vm.DataKategoriBarangMasuk?.FirstOrDefault(x => x.IdKategoriBarangMasuk == Vm.SelectedDataDetail.IdKategoriBarangMasuk);
            TotalQty.Text = DecimalValidationHelper.FormatDecimalToStringId(Vm.SelectedDataDetail.Qty);
            HargaSetelahPpn.Text = DecimalValidationHelper.FormatDecimalToStringId(Vm.SelectedDataDetail.Harga);


            CheckSubmit();
        }

        private void TextBlock_TextChanged(object sender, TextChangedEventArgs e) => CheckSubmit();

        private void CheckSubmit()
        {
            BtnSubmit.IsEnabled = DecimalValidationHelper.FormatStringIdToDecimal(HargaSetelahPpn.Text) > 0 &&
                DecimalValidationHelper.FormatStringIdToDecimal(TotalQty.Text) > 0 && Kategori.SelectedItem != null;
        }

        private void BtnSubmit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var Param = new ParamPengajuanPembelianBarangTentukanHargaDto()
            {
                IdBarang = Vm.SelectedDataDetail.IdBarang,
                IdKategoriBarangMasuk = ((MasterKategoriBarangMasukDto)Kategori.SelectedItem).IdKategoriBarangMasuk,
                Qty = DecimalValidationHelper.FormatStringIdToDecimal(TotalQty.Text),
                Harga = DecimalValidationHelper.FormatStringIdToDecimal(HargaSetelahPpn.Text)
            };
            Vm.OnSubmitKoreksiHargaCommand.Execute(Param);
        }
    }
}
