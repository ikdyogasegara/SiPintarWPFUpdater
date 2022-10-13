using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Akuntansi.Voucher;

namespace SiPintar.Views.Akuntansi.Voucher.PembayaranPembatalan
{
    /// <summary>
    /// Interaction logic for DialogPembatalanFormView.xaml
    /// </summary>
    public partial class DialogPembatalanFormView : UserControl
    {
        private readonly PembayaranPembatalanViewModel _viewModel;

        public DialogPembatalanFormView(PembayaranPembatalanViewModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (PembayaranPembatalanViewModel)DataContext;

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void DataGridVoucherTerbayar_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {

        }

    }
}
