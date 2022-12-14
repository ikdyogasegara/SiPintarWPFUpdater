using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Personalia.GajiPokok;

namespace SiPintar.Views.Personalia.GajiPokok.DewanPengawas
{
    public partial class DialogFormView : UserControl
    {
        private readonly DewanPengawasViewModel _viewModel;
        public DialogFormView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (DewanPengawasViewModel)DataContext;

            Title.Text = ((DewanPengawasViewModel)DataContext).IsEdit ? "Koreksi " : "Tambah ";
            Title.Text += "Gaji Dewan Pengawas";

            CheckButton();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                CloseDialog();
        }

        private void CloseButton_Click(object sender, System.Windows.RoutedEventArgs e) => CloseDialog();

        private void CloseDialog()
        {
            _viewModel.IsBerdasarkanGajiPegawai = false;
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();
        private void CheckForm_SelectionChanged(object sender, SelectionChangedEventArgs e) => CheckButton();

        private void CheckButton()
        {

        }

    }
}
