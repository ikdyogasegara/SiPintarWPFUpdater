using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Bacameter.SistemKontrol.TarifGolongan;

namespace SiPintar.Views.Bacameter.SistemKontrol.TarifGolongan.Golongan
{
    /// <summary>
    /// Interaction logic for DialogFormView.xaml
    /// </summary>
    public partial class DialogFormView : UserControl
    {
        private readonly GolonganViewModel _viewModel;
        public DialogFormView(GolonganViewModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (GolonganViewModel)DataContext;

            Title.Text = ((GolonganViewModel)DataContext).IsEdit ? "Koreksi " : "Tambah ";
            Title.Text += "Data Golongan";
            OkButton.Content = ((GolonganViewModel)DataContext).IsEdit ? "Simpan " : "Tambah ";

            CheckButton();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);

        }


        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void HandleEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                _viewModel.OnSubmitCommand.Execute(null);
            }
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

        private void CheckButton()
        {
            if (string.IsNullOrEmpty(KodeGolongan.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(NamaGolongan.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(Kategori.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(BatasAwal1.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(BatasAwal2.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(BatasAwal3.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(BatasAwal4.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(BatasAwal5.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(BatasAkhir1.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(BatasAkhir2.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(BatasAkhir3.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(BatasAkhir4.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(BatasAkhir5.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(Tarif1.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(Tarif2.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(Tarif3.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(Tarif4.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(Tarif5.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(DendaTg1.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(DendaTg2.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(DendaTg3.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(DendaTg4.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(DendaTgPerBulan.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(Administrasi.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(Pemeliharaan.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(Pelayanan.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(Retribusi.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(RetribusiPakai.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(MinimalDenda.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(Ppn.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(DendaPakai0.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(AirLimbah.Text))
                OkButton.IsEnabled = false;
            else if (StatusGolongan.SelectedIndex == -1)
                OkButton.IsEnabled = false;
            else
                OkButton.IsEnabled = true;

        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void OkButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);

            _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitCommand).ExecuteAsync(null));
        }
        private void StatusGolongan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string textCombo = "";
            if (textCombo != StatusGolongan.Text && _viewModel.IsEdit)
                OkButton.IsEnabled = true;
        }
    }
}
