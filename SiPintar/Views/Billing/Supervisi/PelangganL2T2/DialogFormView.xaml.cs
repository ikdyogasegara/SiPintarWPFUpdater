using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Views.Billing.Supervisi.PelangganL2T2
{
    public partial class DialogFormView : UserControl
    {
        private readonly PelangganL2T2ViewModel _viewModel;
        public DialogFormView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (PelangganL2T2ViewModel)DataContext;

            Title.Text = _viewModel.IsEdit ? "Koreksi " : "Tambah ";
            Title.Text += "Pelanggan L2T2";

            OkButton.Content = _viewModel.IsEdit ? "Perbarui" : "Simpan";
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

        private void CheckButton()
        {
            if (string.IsNullOrEmpty(NamaPelanggan.Text) || string.IsNullOrEmpty(NoL2T2.Text) ||
                string.IsNullOrEmpty(NoKtp.Text) || KodeTarif.SelectedItem == null ||
                string.IsNullOrEmpty(NoTelp.Text) || KodeRayon.SelectedItem == null ||
                string.IsNullOrEmpty(NoHp.Text) || KodeKelurahan.SelectedItem == null ||
                string.IsNullOrEmpty(Email.Text) || KodeKolektif.SelectedItem == null ||
                string.IsNullOrEmpty(Alamat.Text) || string.IsNullOrEmpty(NomorAir.Text) ||
                Status.SelectedItem == null || string.IsNullOrEmpty(Keterangan.Text) ||
                Flag.SelectedItem == null)
                OkButton.IsEnabled = false;
            else
                OkButton.IsEnabled = true;
        }

        private void OkButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);

            if (_viewModel.IsEdit)
                _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitEditCommand).ExecuteAsync(null));
            else
                _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitAddCommand).ExecuteAsync(null));
        }
    }
}
