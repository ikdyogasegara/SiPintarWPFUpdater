using System.Windows.Controls;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Views.Gudang.ProsesTransaksi.DaftarBarangKeluar
{
    /// <summary>
    /// Interaction logic for DialogVerifikasiBarangKeluarView.xaml
    /// </summary>
    public partial class DialogVerifikasiBarangKeluarView : UserControl
    {
        private readonly DaftarBarangKeluarViewModel _vm;
        public DialogVerifikasiBarangKeluarView(object dataContext)
        {
            InitializeComponent();
            var _vm = dataContext as DaftarBarangKeluarViewModel;
            DataContext = _vm;
        }
    }
}
