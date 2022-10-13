using System.Windows.Controls;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Views.Gudang.ProsesTransaksi.PengajuanPengeluaranBarang
{
    /// <summary>
    /// Interaction logic for DialogProsesDataView.xaml
    /// </summary>
    public partial class DialogProsesDataView : UserControl
    {
        private readonly TransaksiBarangKeluarViewModel _vm;

        public DialogProsesDataView(object dataContext)
        {
            InitializeComponent();
            _vm = dataContext as TransaksiBarangKeluarViewModel;
            DataContext = _vm;
            PreviewKeyUp += DialogProsesDataView_PreviewKeyUp;
            CheckSubmit();
        }

        private void DialogProsesDataView_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
        }

        private void CheckSubmit()
        {
            BtnSubmit.IsEnabled = !string.IsNullOrWhiteSpace(_vm.FormProses.NomorBpp) && _vm.FormProses.IdPeriode.HasValue;
        }

        private void Periode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox cb && cb.SelectedItem != null)
            {
                _vm.FormProses.IdPeriode = ((MasterPeriodeGudangDto)cb.SelectedItem).IdPeriode;
                CheckSubmit();
            }
        }

        private void NoBpp_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckSubmit();
        }
    }
}
