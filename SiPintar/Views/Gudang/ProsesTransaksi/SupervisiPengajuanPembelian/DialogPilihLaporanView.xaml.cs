using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Views.Gudang.ProsesTransaksi.SupervisiPengajuanPembelian
{
    /// <summary>
    /// Interaction logic for DialogPilihLaporanView.xaml
    /// </summary>
    public partial class DialogPilihLaporanView : UserControl
    {
        private readonly SupervisiPengajuanPembelianViewModel Vm;
        private readonly ObservableCollection<KeyValuePair<string, string>> DaftarJenisLaporan = new ObservableCollection<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("OrderPembelian", "OP (Order Pembelian)"),
            new KeyValuePair<string, string>("SuratPerjanjianKerja", "SPK (Surat Perjanjian Kerja)")
        };

        public DialogPilihLaporanView(object dataContext)
        {
            InitializeComponent();
            Vm = dataContext as SupervisiPengajuanPembelianViewModel;
            DataContext = Vm;
            PreviewKeyUp += DialogPilihLaporanView_PreviewKeyUp;
            JenisLaporan.ItemsSource = DaftarJenisLaporan;
            BtnSubmit.IsEnabled = JenisLaporan.SelectedItem != null;
        }

        private void DialogPilihLaporanView_PreviewKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Escape)
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
        }

        private void BtnSubmit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            switch (((KeyValuePair<string, string>)JenisLaporan.SelectedItem).Key)
            {
                case "OrderPembelian":
                    Vm.OnOpenLaporanOrderPembelianCommand.Execute(null);
                    break;
                case "SuratPerjanjianKerja":
                    Vm.OnOpenLaporanSuratPerjanjianKerjaCommand.Execute(null);
                    break;
                default:
                    break;
            }
        }

        private void JenisLaporan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BtnSubmit.IsEnabled = JenisLaporan.SelectedItem != null;
        }
    }
}
