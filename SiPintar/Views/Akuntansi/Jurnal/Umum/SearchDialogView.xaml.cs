using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Akuntansi.Jurnal;

namespace SiPintar.Views.Akuntansi.Jurnal.Umum
{
    /// <summary>
    /// Interaction logic for SearchDialogView.xaml
    /// </summary>
    public partial class SearchDialogView : UserControl
    {
        private readonly UmumViewModel _viewModel;

        public SearchDialogView(UmumViewModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (UmumViewModel)DataContext;

            CheckButton();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void DataGridKodePerkiraan_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {

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

        private void ButtonKembali_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
            _viewModel.OnOpenTransaksiAddFormCommand.Execute(null);
        }

    }
}
