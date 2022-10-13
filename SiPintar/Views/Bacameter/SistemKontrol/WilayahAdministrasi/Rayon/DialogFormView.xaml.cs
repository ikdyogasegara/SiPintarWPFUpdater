using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Bacameter.SistemKontrol.WilayahAdministrasi;

namespace SiPintar.Views.Bacameter.SistemKontrol.WilayahAdministrasi.Rayon
{
    public partial class DialogFormView : UserControl
    {
        private readonly string section;
        private readonly RayonViewModel viewModel;
        public DialogFormView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;

            viewModel = (RayonViewModel)DataContext;
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
            Title.Text += section == "wilayah" ? "Wilayah" : (section == "area" ? "Area" : "Rayon");

            LabelKode.Text += section == "wilayah" ? " Wilayah" : (section == "area" ? " Area" : " Rayon");
            LabelNama.Text += section == "wilayah" ? " Wilayah" : (section == "area" ? " Area" : " Rayon");
            PlaceholderKode.Text += " " + section;
            PlaceholderNama.Text += " " + section;

            if (section == "area" || section == "rayon")
            {
                LabelKodeParent.Text = section == "area" ? " Wilayah" : " Area";
                LabelNamaParent.Text += section == "area" ? " Wilayah" : " Area";

                KodeParent.Text = section == "area"
                    ? viewModel.SelectedWilayah.KodeWilayah
                    : viewModel.SelectedArea.KodeArea;
                NamaParent.Text = section == "area"
                    ? viewModel.SelectedWilayah.NamaWilayah
                    : viewModel.SelectedArea.NamaArea;

            }

            ParentSection.Visibility = section == "area" || section == "rayon" ? Visibility.Visible : Visibility.Collapsed;
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
            if ((!viewModel.IsEdit && (string.IsNullOrEmpty(Kode.Text) || string.IsNullOrEmpty(Nama.Text))) || notif.Text != "")
                OkButton.IsEnabled = false;
            else
                OkButton.IsEnabled = true;
        }

        private void Kode_TextChanged(object sender, TextChangedEventArgs e)
        {
            string notifikasi = null;

            notifikasi = section switch
            {
                "wilayah" => viewModel.WilayahList.FirstOrDefault(i => i.KodeWilayah == Kode.Text) != null ? notif.Text = "Kode telah digunakan" : notif.Text = "",
                "area" => viewModel.AreaList.FirstOrDefault(i => i.KodeArea == Kode.Text) != null ? notif.Text = "Kode telah digunakan" : notif.Text = "",
                "rayon" => viewModel.RayonList.FirstOrDefault(i => i.KodeRayon == Kode.Text) != null ? notif.Text = "Kode telah digunakan" : notif.Text = "",
                _ => viewModel.WilayahList.FirstOrDefault(i => i.KodeWilayah == Kode.Text) != null ? notif.Text = "Kode telah digunakan" : notif.Text = "",
            };
            if (notifikasi != "" && !viewModel.IsEdit)
                notif.Visibility = Visibility.Visible;
            else
                notif.Visibility = Visibility.Collapsed;
        }
    }
}
