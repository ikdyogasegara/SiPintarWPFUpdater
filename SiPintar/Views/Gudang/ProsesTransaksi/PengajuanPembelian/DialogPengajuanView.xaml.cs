using System.Windows;
using System.Windows.Controls;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Views.Gudang.ProsesTransaksi.PengajuanPembelian
{
    /// <summary>
    /// Interaction logic for DialogPengajuanView.xaml
    /// </summary>
    public partial class DialogPengajuanView : UserControl
    {
        private readonly PengajuanPembelianViewModel _vm;
        public DialogPengajuanView(PengajuanPembelianViewModel dataContext)
        {
            _vm = dataContext;
            DataContext = _vm;
            InitializeComponent();

            Title.Text = (_vm.IsView ? "Rincian" : "Tambah") + " Pengajuan Pembelian";
            if (_vm.IsView)
            {
                CanvasControl.IsHitTestVisible = false;
                BtnCancel.Content = "Kembali";
                BtnSubmit.Visibility = Visibility.Collapsed;
                BtnAdd.Visibility = Visibility.Collapsed;
                BtnDel.Visibility = Visibility.Collapsed;
            }
        }
    }
}
