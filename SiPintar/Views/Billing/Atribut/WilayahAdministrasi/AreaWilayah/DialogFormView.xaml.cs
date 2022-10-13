using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Billing.Atribut.WilayahAdministrasi;

namespace SiPintar.Views.Billing.Atribut.WilayahAdministrasi.AreaWilayah
{
    public partial class DialogFormView : UserControl
    {
        private readonly string _section;
        private readonly AreaWilayahViewModel _viewModel;
        public DialogFormView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;

            _viewModel = (AreaWilayahViewModel)DataContext;
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
            Title.Text += _section == "wilayah" ? "Wilayah" : (_section == "area" ? "Area" : "Rayon");

            LabelKode.Text += _section == "wilayah" ? " Wilayah" : (_section == "area" ? " Area" : " Rayon");
            LabelNama.Text += _section == "wilayah" ? " Wilayah" : (_section == "area" ? " Area" : " Rayon");
            PlaceholderKode.Text += " " + _section;
            PlaceholderNama.Text += " " + _section;

            if (_section == "area" || _section == "rayon")
            {
                LabelKodeParent.Text = _section == "area" ? " Wilayah" : " Area";
                LabelNamaParent.Text += _section == "area" ? " Wilayah" : " Area";

                KodeParent.Text = _section == "area"
                    ? _viewModel.SelectedWilayah.KodeWilayah
                    : _viewModel.SelectedArea.KodeArea;
                NamaParent.Text = _section == "area"
                    ? _viewModel.SelectedWilayah.NamaWilayah
                    : _viewModel.SelectedArea.NamaArea;

            }

            ParentSection.Visibility = _section == "area" || _section == "rayon" ? Visibility.Visible : Visibility.Collapsed;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);

            _ = _viewModel.IsEdit ? Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitEditCommand).ExecuteAsync(null)) : Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitAddCommand).ExecuteAsync(null));
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

        private void CheckButton()
        {
            if (!string.IsNullOrEmpty(Kode.Text) && !string.IsNullOrEmpty(Nama.Text) && notif.Text == "")
                OkButton.IsEnabled = true;
            else
                OkButton.IsEnabled = false;
        }

        private void Kode_TextChanged(object sender, TextChangedEventArgs e)
        {
            switch (_section)
            {
                case "wilayah":
                    notif.Text = _viewModel.WilayahList.FirstOrDefault(i => i.KodeWilayah == Kode.Text) != null ? notif.Text = "Kode telah digunakan" : "";
                    break;

                case "area":
                    notif.Text = _viewModel.AreaList.FirstOrDefault(i => i.KodeArea == Kode.Text) != null ? notif.Text = "Kode telah digunakan" : "";
                    break;

                case "rayon":
                    notif.Text = _viewModel.RayonList.FirstOrDefault(i => i.KodeRayon == Kode.Text) != null ? notif.Text = "Kode telah digunakan" : "";
                    break;
            }
        }

        private void Nama_TextChanged(object sender, TextChangedEventArgs e)
        {
            switch (_section)
            {
                case "wilayah":
                    notif.Text = _viewModel.WilayahList.FirstOrDefault(i => i.NamaWilayah == Kode.Text) != null ? notif.Text = "Nama telah digunakan" : "";
                    break;

                case "area":
                    notif.Text = _viewModel.AreaList.FirstOrDefault(i => i.NamaArea == Kode.Text) != null ? notif.Text = "Nama telah digunakan" : "";
                    break;

                case "rayon":
                    notif.Text = _viewModel.RayonList.FirstOrDefault(i => i.NamaRayon == Kode.Text) != null ? notif.Text = "Nama telah digunakan" : "";
                    break;
            }
        }
    }
}
