using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Akuntansi.Voucher;

namespace SiPintar.Views.Akuntansi.Voucher.PembayaranPembatalan
{
    /// <summary>
    /// Interaction logic for DialogPembayaranFormView.xaml
    /// </summary>
    public partial class DialogPembayaranFormView : UserControl
    {
        private readonly PembayaranPembatalanViewModel _viewModel;

        public DialogPembayaranFormView(PembayaranPembatalanViewModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (PembayaranPembatalanViewModel)DataContext;

            CheckButton();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

        private void CheckButton()
        {
            //if (string.IsNullOrEmpty(KodeMaterial.Text))
            //    OkButton.IsEnabled = false;
            //else if (string.IsNullOrEmpty(NamaMaterial.Text))
            //    OkButton.IsEnabled = false;
            //else if (string.IsNullOrEmpty(Satuan.Text))
            //    OkButton.IsEnabled = false;
            //else if (string.IsNullOrEmpty(HargaJual.Text))
            //    OkButton.IsEnabled = false;
            //else
            //    OkButton.IsEnabled = true;

        }
    }
}
