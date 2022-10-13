using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Views.Gudang.ProsesTransaksi.DaftarBarangMasuk
{
    /// <summary>
    /// Interaction logic for DialogSetTanggalVoucher.xaml
    /// </summary>
    public partial class DialogSetTanggalVoucher : UserControl
    {
        private readonly DaftarBarangMasukViewModel _vm;
        public DialogSetTanggalVoucher(object dataContext)
        {
            _vm = dataContext as DaftarBarangMasukViewModel;
            DataContext = _vm;
            InitializeComponent();

            PreviewKeyUp += DialogSetTanggalVoucher_PreviewKeyUp;
        }

        private void DialogSetTanggalVoucher_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
        }
    }
}
