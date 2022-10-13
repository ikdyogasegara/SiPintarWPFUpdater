using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Views.Gudang.ProsesTransaksi.PengajuanPengeluaranBarang
{
    /// <summary>
    /// Interaction logic for DialogKoreksiDataView.xaml
    /// </summary>
    public partial class DialogKoreksiDataView : UserControl
    {
        private readonly TransaksiBarangKeluarViewModel _vm;
        public DialogKoreksiDataView(object dataContext)
        {
            InitializeComponent();
            _vm = dataContext as TransaksiBarangKeluarViewModel;
            DataContext = _vm;
            PreviewKeyUp += DialogKoreksiDataView_PreviewKeyUp;
            KeteranganTitle.Text = _vm.SelectedData.DiGunakanUntuk;
        }

        private void DialogKoreksiDataView_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(KeteranganTitle.Text))
            {
                var param = new Dictionary<string, dynamic>()
                {
                    { "IdPengajuanPengeluaran", _vm.SelectedData.IdPengajuanPengeluaran },
                    { "IdGudang", _vm.SelectedGudang.IdGudang },
                    { "IdBagianMemintaBarang", _vm.SelectedBagianMemintaBarang.IdBagianMemintaBarang },
                    { "IdKategoriBarangKeluar", _vm.SelectedKategori.IdKategoriBarangKeluar },
                    { "DiGunakanUntuk", KeteranganTitle.Text.Trim() },
                };

                _vm.OnSubmitKoreksiDataPengajuanCommand.Execute(param);

            }
        }
    }
}
