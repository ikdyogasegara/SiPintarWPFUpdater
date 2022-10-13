using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Views.Gudang.ProsesTransaksi.SupervisiPengajuanPembelian
{
    /// <summary>
    /// Interaction logic for DialogLaporanOrderPembelianView.xaml
    /// </summary>
    public partial class DialogLaporanOrderPembelianView : UserControl
    {
        private readonly SupervisiPengajuanPembelianViewModel Vm;
        private readonly ObservableCollection<KeyValuePair<string, string>> DaftarJenisLaporan = new ObservableCollection<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("OrderPembelian", "OP (Order Pembelian)"),
            new KeyValuePair<string, string>("SuratPerjanjianKerja", "SPK (Surat Perjanjian Kerja)")
        };

        public DialogLaporanOrderPembelianView(object dataContext)
        {
            InitializeComponent();
            Vm = dataContext as SupervisiPengajuanPembelianViewModel;
            DataContext = Vm;
            Vm.OrderPembelian = null;
            PreviewKeyUp += DialogLaporanOrderPembelianView_PreviewKeyUp;
            JenisLaporan.ItemsSource = DaftarJenisLaporan;
            JenisLaporan.SelectedItem = DaftarJenisLaporan.FirstOrDefault(x => x.Key == "OrderPembelian");
            TanggalLaporan.SelectedDate = Vm.SelectedData.Order_Pembelian_Tgl_Laporan ?? System.DateTime.Now;
            TanggalVoucher.SelectedDate = Vm.SelectedData.Tgl_Voucher ?? System.DateTime.Now;
            Vm.NoOrderPembelian = Vm.SelectedData.Order_Pembelian_Nomor;
            Rating.Value = Vm.SelectedData.Order_Pembelian_Rating_Suplier ?? 0;
            Suplier.SelectedItem = Vm.DataSuplier.FirstOrDefault(x => x.IdSuplier == Vm.SelectedData.Order_Pembelian_IdSuplier);
            CheckSubmit();
        }

        private void DialogLaporanOrderPembelianView_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
        }

        private void CheckSubmit()
        {
            BtnSubmit.IsEnabled = TanggalLaporan.SelectedDate != null && TanggalVoucher.SelectedDate != null && !string.IsNullOrWhiteSpace(NomorOrderPembelian.Text)
                && Suplier.SelectedItem != null && Rating.Value > 0;
        }

        private void SelectedDateChanged(object sender, SelectionChangedEventArgs e) => CheckSubmit();
        private void Suplier_SelectionChanged(object sender, SelectionChangedEventArgs e) => CheckSubmit();
        private void Rating_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            RatingText.Text = $"(Bintang {Rating.Value})";
            CheckSubmit();
        }
        private void NomorOrderPembelian_TextChanged(object sender, TextChangedEventArgs e) => CheckSubmit();
        private void BtnSubmit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var ParamSend = new ParamPengajuanPembelianBarangOPDto()
            {
                IdPengajuanPembelian = Vm.SelectedData.IdPengajuanPembelian,
                Order_Pembelian_Tgl_Laporan = TanggalLaporan.SelectedDate,
                Tgl_Voucher = TanggalVoucher.SelectedDate,
                Order_Pembelian_IdSuplier = ((MasterSuplierDto)Suplier.SelectedItem).IdSuplier,
                Order_Pembelian_Nomor = NomorOrderPembelian.Text,
                Order_Pembelian_Rating_Suplier = Rating.Value
            };
            Vm.OnOpenConfirmationOrderPembelianCommand.Execute(ParamSend);
        }
    }
}
