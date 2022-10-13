using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Billing.Atribut.WilayahAdministrasi;

namespace SiPintar.Views.Billing.Atribut.WilayahAdministrasi.Kelurahan
{
    public partial class DialogFormView : UserControl
    {
        private readonly string _section;
        private readonly KelurahanViewModel _viewModel;
        public DialogFormView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;

            _viewModel = (KelurahanViewModel)DataContext;
            _section = _viewModel.Section;

            InitData();
            CheckButton();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
            PreviewKeyUp += new KeyEventHandler(HandleEnter);
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
                _viewModel.KodeForm = Kode.Text;
                _viewModel.NamaForm = Nama.Text;

                Submit_Click(null, null);
            }
        }

        private void InitData()
        {
            Title.Text = _viewModel.IsEdit ? "Koreksi " : "Tambah ";
            Title.Text += "Data ";
            Title.Text += _section == "cabang" ? "Cabang" : (_section == "kecamatan" ? "Kecamatan" : "Kelurahan");

            LabelKode.Text += _section == "cabang" ? " Cabang" : (_section == "kecamatan" ? " Kecamatan" : " Kelurahan");
            LabelNama.Text += _section == "cabang" ? " Cabang" : (_section == "kecamatan" ? " Kecamatan" : " Kelurahan");
            PlaceholderKode.Text += " " + _section;
            PlaceholderNama.Text += " " + _section;

            if (_section == "kecamatan" || _section == "kelurahan")
            {
                LabelKodeParent.Text = _section == "kecamatan" ? " Cabang" : " Kecamatan";
                LabelNamaParent.Text += _section == "kecamatan" ? " Cabang" : " Kecamatan";

                KodeParent.Text = _section == "kecamatan"
                    ? _viewModel.SelectedCabang.KodeCabang
                    : _viewModel.SelectedKecamatan.KodeKecamatan;
                NamaParent.Text = _section == "kecamatan"
                    ? _viewModel.SelectedCabang.NamaCabang
                    : _viewModel.SelectedKecamatan.NamaKecamatan;

                KelurahanSection.Visibility = (_section == "kelurahan") ? Visibility.Visible : Visibility.Collapsed;
            }

            ParentSection.Visibility = _section == "kecamatan" || _section == "kelurahan" ? Visibility.Visible : Visibility.Collapsed;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);

            _ = _viewModel.IsEdit ? Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitEditCommand).ExecuteAsync(null)) : Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitAddCommand).ExecuteAsync(null));
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

        private void CheckButton()
        {
            if (_section == "kelurahan")
            {
                if (!string.IsNullOrEmpty(Kode.Text) && !string.IsNullOrEmpty(Nama.Text) && !string.IsNullOrEmpty(JmlJiwa.Text) && notif.Text == "")
                    OkButton.IsEnabled = true;
                else
                    OkButton.IsEnabled = false;
            }
            else
            {
                if (!string.IsNullOrEmpty(Kode.Text) && !string.IsNullOrEmpty(Nama.Text) && notif.Text == "")
                    OkButton.IsEnabled = true;
                else
                    OkButton.IsEnabled = false;
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Kode_TextChanged(object sender, TextChangedEventArgs e)
        {
            switch (_section)
            {
                case "cabang":
                    notif.Text = _viewModel.CabangList.FirstOrDefault(i => i.KodeCabang == Kode.Text) != null ? notif.Text = "Kode telah digunakan" : "";
                    break;

                case "kecamatan":
                    notif.Text = _viewModel.KecamatanList.FirstOrDefault(i => i.KodeKecamatan == Kode.Text) != null ? notif.Text = "Kode telah digunakan" : "";
                    break;

                case "kelurahan":
                    notif.Text = _viewModel.KelurahanList.FirstOrDefault(i => i.KodeKelurahan == Kode.Text) != null ? notif.Text = "Kode telah digunakan" : "";
                    break;
            }
        }

        private void Nama_TextChanged(object sender, TextChangedEventArgs e)
        {
            switch (_section)
            {
                case "cabang":
                    notif.Text = _viewModel.CabangList.FirstOrDefault(i => i.NamaCabang == Nama.Text) != null ? notif.Text = "Nama telah digunakan" : "";
                    break;

                case "kecamatan":
                    notif.Text = _viewModel.KecamatanList.FirstOrDefault(i => i.NamaKecamatan == Nama.Text) != null ? notif.Text = "Nama telah digunakan" : "";
                    break;

                case "kelurahan":
                    notif.Text = _viewModel.KelurahanList.FirstOrDefault(i => i.NamaKelurahan == Nama.Text) != null ? notif.Text = "Nama telah digunakan" : "";
                    break;
            }
        }
    }
}
