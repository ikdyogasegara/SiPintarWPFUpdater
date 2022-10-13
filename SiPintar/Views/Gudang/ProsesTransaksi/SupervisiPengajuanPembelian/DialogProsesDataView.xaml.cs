using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Views.Gudang.ProsesTransaksi.SupervisiPengajuanPembelian
{
    /// <summary>
    /// Interaction logic for DialogProsesDataView.xaml
    /// </summary>
    public partial class DialogProsesDataView : UserControl
    {
        public readonly SupervisiPengajuanPembelianViewModel Vm;
        public DialogProsesDataView(object dataContext)
        {
            InitializeComponent();
            Vm = dataContext as SupervisiPengajuanPembelianViewModel;
            DataContext = Vm;
            Vm.ProsesData = null;
            PreviewKeyUp += DialogProsesDataView_PreviewKeyUp;
            NomorDpb.Text = Vm.SelectedData.NomorPengajuanPembelian;
            Checksubmit();
        }

        private void DialogProsesDataView_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
        }
        private void Checksubmit()
        {
            BtnSubmit.IsEnabled = !string.IsNullOrWhiteSpace(NomorDpb.Text) && !string.IsNullOrWhiteSpace(NomorLpb.Text)
                && PeriodePosting.SelectedItem != null && PeriodeVoucher.SelectedItem != null && !string.IsNullOrWhiteSpace(SuratJalan.Text);
        }
        private void TextChanged(object sender, TextChangedEventArgs e) => Checksubmit();
        private void SelectionChanged(object sender, SelectionChangedEventArgs e) => Checksubmit();

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            var Param = new ParamPengajuanPembelianBarangProsesKePersediaanDto()
            {
                IdGudang = Vm.SelectedData.IdGudang,
                IdPengajuanPembelian = Vm.SelectedData.IdPengajuanPembelian,
                IdPeriode = ((MasterPeriodeGudangDto)PeriodePosting.SelectedItem).IdPeriode,
                IdPeriode_Voucher = ((MasterPeriodeGudangDto)PeriodeVoucher.SelectedItem).IdPeriode,
                NomorSuratJalan = SuratJalan.Text,
                NomorLpb = NomorLpb.Text,
                WaktuBarangMasuk = DateTime.Now
            };
            _ = ((AsyncCommandBase)Vm.OnOpenConfirmationSubmitProsesDataCommand).ExecuteAsync(Param);
        }
    }
}
