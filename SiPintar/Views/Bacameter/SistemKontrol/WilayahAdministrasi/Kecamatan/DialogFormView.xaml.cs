using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Bacameter.SistemKontrol.WilayahAdministrasi;

namespace SiPintar.Views.Bacameter.SistemKontrol.WilayahAdministrasi.Kecamatan
{
    public partial class DialogFormView : UserControl
    {
        private readonly string section;
        private readonly KecamatanViewModel viewModel;
        public DialogFormView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;

            viewModel = (KecamatanViewModel)DataContext;
            section = viewModel.Section;

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
                viewModel.KodeForm = Kode.Text;
                viewModel.NamaForm = Nama.Text;

                Submit_Click(null, null);
            }
        }

        private void InitData()
        {
            Title.Text = viewModel.IsEdit ? "Koreksi " : "Tambah ";
            Title.Text += "Data ";
            Title.Text += section == "cabang" ? "Cabang" : (section == "kecamatan" ? "Kecamatan" : "Kelurahan");

            LabelKode.Text += section == "cabang" ? " Cabang" : (section == "kecamatan" ? " Kecamatan" : " Kelurahan");
            LabelNama.Text += section == "cabang" ? " Cabang" : (section == "kecamatan" ? " Kecamatan" : " Kelurahan");
            PlaceholderKode.Text += " " + section;
            PlaceholderNama.Text += " " + section;

            if (section == "kecamatan" || section == "kelurahan")
            {
                LabelKodeParent.Text = section == "kecamatan" ? " Cabang" : " Kecamatan";
                LabelNamaParent.Text += section == "kecamatan" ? " Cabang" : " Kecamatan";

                KodeParent.Text = section == "kecamatan"
                    ? viewModel.SelectedCabang.KodeCabang
                    : viewModel.SelectedKecamatan.KodeKecamatan;
                NamaParent.Text = section == "kecamatan"
                    ? viewModel.SelectedCabang.NamaCabang
                    : viewModel.SelectedKecamatan.NamaKecamatan;

                KelurahanSection.Visibility = (section == "kelurahan") ? Visibility.Visible : Visibility.Collapsed;
            }

            ParentSection.Visibility = section == "kecamatan" || section == "kelurahan" ? Visibility.Visible : Visibility.Collapsed;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);

            if (viewModel.IsEdit)
                _ = Task.Run(() => ((AsyncCommandBase)viewModel.OnSubmitEditCommand).ExecuteAsync(null));
            else
                _ = Task.Run(() => ((AsyncCommandBase)viewModel.OnSubmitAddCommand).ExecuteAsync(null));
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

        private void Loket_SelectionChanged(object sender, SelectionChangedEventArgs e) => CheckButton();

        private void CheckButton()
        {
            if (!viewModel.IsEdit && (string.IsNullOrEmpty(Kode.Text) || string.IsNullOrEmpty(Nama.Text) || (string.IsNullOrEmpty(JmlJiwa.Text) && section == "kelurahan") || notif.Text != ""))
                OkButton.IsEnabled = false;
            else
                OkButton.IsEnabled = true;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
            CheckButton();
        }

        private void Kode_TextChanged(object sender, TextChangedEventArgs e)
        {
            string notifikasi = null;

            notifikasi = section switch
            {
                "cabang" => viewModel.CabangList.FirstOrDefault(i => i.KodeCabang == Kode.Text) != null ? notif.Text = "Kode telah digunakan" : notif.Text = "",
                "kecamatan" => viewModel.KecamatanList.FirstOrDefault(i => i.KodeKecamatan == Kode.Text) != null ? notif.Text = "Kode telah digunakan" : notif.Text = "",
                "kelurahan" => viewModel.KelurahanList.FirstOrDefault(i => i.KodeKelurahan == Kode.Text) != null ? notif.Text = "Kode telah digunakan" : notif.Text = "",
                _ => viewModel.CabangList.FirstOrDefault(i => i.KodeCabang == Kode.Text) != null ? notif.Text = "Kode telah digunakan" : notif.Text = "",
            };
            if (notifikasi != "" && !viewModel.IsEdit)
                notif.Visibility = Visibility.Visible;
            else
                notif.Visibility = Visibility.Collapsed;
        }
    }
}
