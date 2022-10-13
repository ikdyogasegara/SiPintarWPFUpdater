using System;
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
    /// Interaction logic for DialogLaporanSuratPerjanjianKerjaView.xaml
    /// </summary>
    public partial class DialogLaporanSuratPerjanjianKerjaView : UserControl
    {
        private readonly SupervisiPengajuanPembelianViewModel Vm;
        private readonly ObservableCollection<KeyValuePair<string, string>> DaftarJenisLaporan = new ObservableCollection<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("OrderPembelian", "OP (Order Pembelian)"),
            new KeyValuePair<string, string>("SuratPerjanjianKerja", "SPK (Surat Perjanjian Kerja)")
        };

        public DialogLaporanSuratPerjanjianKerjaView(object dataContext)
        {
            InitializeComponent();
            Vm = dataContext as SupervisiPengajuanPembelianViewModel;
            DataContext = Vm;
            Vm.Spk = null;
            PreviewKeyUp += DialogLaporanOrderPembelianView_PreviewKeyUp;
            JenisLaporan.ItemsSource = DaftarJenisLaporan;
            JenisLaporan.SelectedItem = DaftarJenisLaporan.FirstOrDefault(x => x.Key == "SuratPerjanjianKerja");
            TanggalLaporan.SelectedDate = Vm.SelectedData.Surat_Perjanjian_Tgl_Laporan ?? DateTime.Now;
            TanggalVoucher.SelectedDate = Vm.SelectedData.Tgl_Voucher ?? DateTime.Now;
            Vm.NoSpk = Vm.SelectedData.Surat_Perjanjian_Nomor;
            Rating.Value = Vm.SelectedData.Surat_Perjanjian_Rating_Suplier ?? 0;
            Suplier.SelectedItem = Vm.DataSuplier.FirstOrDefault(x => x.IdSuplier == Vm.SelectedData.Surat_Perjanjian_IdSuplier);
            NomorBA.Text = Vm.SelectedData.Surat_Perjanjian_Berita_Acara;
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
            BtnSubmit.IsEnabled = TanggalLaporan.SelectedDate != null && TanggalVoucher.SelectedDate != null && !string.IsNullOrWhiteSpace(NomorSpk.Text)
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

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            var ParamSend = new ParamPengajuanPembelianBarangSuratPerjanjianDto()
            {
                IdPengajuanPembelian = Vm.SelectedData.IdPengajuanPembelian,
                Surat_Perjanjian_Tgl_Laporan = TanggalLaporan.SelectedDate,
                Tgl_Voucher = TanggalVoucher.SelectedDate,
                Surat_Perjanjian_IdSuplier = ((MasterSuplierDto)Suplier.SelectedItem).IdSuplier,
                Surat_Perjanjian_Nomor = NomorSpk.Text,
                Surat_Perjanjian_Rating_Suplier = Rating.Value,
                Surat_Perjanjian_Berita_Acara = NomorBA.Text
            };
            Vm.OnOpenConfirmationSpkCommand.Execute(ParamSend);
        }
    }
}
