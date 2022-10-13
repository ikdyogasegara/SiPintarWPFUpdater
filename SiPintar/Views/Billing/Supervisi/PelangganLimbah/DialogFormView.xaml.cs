using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Views.Billing.Supervisi.PelangganLimbah
{
    public partial class DialogFormView : UserControl
    {
        private readonly PelangganLimbahViewModel _viewModel;
        public DialogFormView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (PelangganLimbahViewModel)DataContext;

            Title.Text = _viewModel.IsEdit ? "Koreksi " : "Tambah ";
            Title.Text += "Pelanggan Limbah";

            OkButton.Content = _viewModel.IsEdit ? "Perbarui" : "Simpan";

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
            if (string.IsNullOrEmpty(NamaPelanggan.Text) || string.IsNullOrEmpty(NoLimbah.Text) ||
                string.IsNullOrEmpty(NoKtp.Text) || KodeTarif.SelectedIndex < 0 ||
                string.IsNullOrEmpty(NoTelp.Text) || KodeRayon.SelectedIndex < 0 ||
                string.IsNullOrEmpty(NoHp.Text) || KodeKelurahan.SelectedIndex < 0 ||
                string.IsNullOrEmpty(Email.Text) || KodeKolektif.SelectedIndex < 0 ||
                string.IsNullOrEmpty(Alamat.Text) || PelangganAir.SelectedIndex < 0 ||
                Status.SelectedItem == null || Flag.SelectedItem == null)
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

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => CheckButton();

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
            CheckButton();
        }
    }
}
