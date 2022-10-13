using System.Windows.Controls;
using SiPintar.ViewModels.Gudang.Pengolahan;

namespace SiPintar.Views.Gudang.Pengolahan.PenyesuaianStock
{
    /// <summary>
    /// Interaction logic for DialogFormView.xaml
    /// </summary>
    public partial class DialogFormView : UserControl
    {
        private readonly PenyesuaianStockViewModel _vm;
        public DialogFormView(PenyesuaianStockViewModel dataCtx)
        {
            DataContext = _vm = dataCtx;
            InitializeComponent();
        }
    }
}
