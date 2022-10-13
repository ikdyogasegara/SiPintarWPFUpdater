using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Personalia.Tunjangan;

namespace SiPintar.Views.Personalia.Tunjangan.TunjanganBulanan
{
    public partial class DialogFormView : UserControl
    {
        private readonly TunjanganBulananViewModel _viewModel;
        public DialogFormView(object dataContext)
        {
            InitializeComponent();
            _viewModel = dataContext as TunjanganBulananViewModel;
            DataContext = _viewModel;

            Title.Text = ((TunjanganBulananViewModel)DataContext).IsEdit ? "Koreksi " : "Tambah ";
            Title.Text += "Tunjangan Non Rutin";

            CheckButton();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void RadioButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;

            if (radioButton.GroupName == "FlagAbsensi")
                _viewModel.FormFlagAbsensi = radioButton.Name == "FlagAbsensiTrue";
            else if (radioButton.GroupName == "FlagSemuaPegawai")
                _viewModel.FormFlagSemuaPegawai = radioButton.Name == "FlagSemuaPegawaiTrue";

            CheckButton();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();
        private void CheckForm_SelectionChanged(object sender, SelectionChangedEventArgs e) => CheckButton();
        private void CheckBox_Click(object sender, System.Windows.RoutedEventArgs e) => CheckButton();

        private void CheckForm_TextChanged(object sender, TextChangedEventArgs e) => CheckButton();
        private void CheckButton()
        {
            if (_viewModel.FormCurrentPage == 1)
            {
                NextButton.IsEnabled = true;
                if (JenisTunjangan.SelectedValue == null)
                    NextButton.IsEnabled = false;
                if (FlagAbsensiTrue.IsChecked == null && FlagAbsensiFalse.IsChecked == null)
                    NextButton.IsEnabled = false;
                if (FlagAbsensiTrue.IsChecked == true && UpahKehadiran.SelectedValue == null)
                    NextButton.IsEnabled = false;
                if (FlagAbsensiFalse.IsChecked == true && string.IsNullOrEmpty(Nominal.Text))
                    NextButton.IsEnabled = false;
                if (FlagPersen.SelectedIndex == 1)
                {
                    if (PersenDari.SelectedValue == null)
                        NextButton.IsEnabled = false;
                    else if (NominalMinCheck.IsChecked == true && string.IsNullOrEmpty(NominalMin.Text))
                        NextButton.IsEnabled = false;
                    else if (NominalMaxCheck.IsChecked == true && string.IsNullOrEmpty(NominalMax.Text))
                        NextButton.IsEnabled = false;
                }
                if (string.IsNullOrEmpty(Keterangan.Text))
                    NextButton.IsEnabled = false;
                if (FlagPotongPph.SelectedIndex == 1)
                {
                    if (FlagPersenPph.SelectedValue == null)
                        NextButton.IsEnabled = false;
                    else if (string.IsNullOrEmpty(NominalPph.Text))
                        NextButton.IsEnabled = false;
                }
            }
            else if (_viewModel.FormCurrentPage == 2)
            {
                OkButton.IsEnabled = true;
                if (FlagSemuaPegawaiTrue.IsChecked == null && FlagSemuaPegawaiFalse.IsChecked == null)
                    OkButton.IsEnabled = false;
                else if (FlagSemuaPegawaiFalse.IsChecked == true)
                {
                    if (AgamaCheck.IsChecked != true && DivisiCheck.IsChecked != true &&
                        JabatanCheck.IsChecked != true && PendidikanCheck.IsChecked != true &&
                        AreaKerjaCheck.IsChecked != true && GolonganPegawaiCheck.IsChecked != true &&
                        JenisKelaminCheck.IsChecked != true && TipeKeluargaCheck.IsChecked != true &&
                        DepartemenCheck.IsChecked != true && MkgCheck.IsChecked != true &&
                        PegawaiCheck.IsChecked != true && StatusPegawaiCheck.IsChecked != true)
                        OkButton.IsEnabled = false;
                    if (AgamaCheck.IsChecked == true && Agama.SelectedValue == null)
                        OkButton.IsEnabled = false;
                    if (DivisiCheck.IsChecked == true && Divisi.SelectedValue == null)
                        OkButton.IsEnabled = false;
                    if (JabatanCheck.IsChecked == true && Jabatan.SelectedValue == null)
                        OkButton.IsEnabled = false;
                    if (PendidikanCheck.IsChecked == true && Pendidikan.SelectedValue == null)
                        OkButton.IsEnabled = false;
                    if (AreaKerjaCheck.IsChecked == true && AreaKerja.SelectedValue == null)
                        OkButton.IsEnabled = false;
                    if (GolonganPegawaiCheck.IsChecked == true && GolonganPegawai.SelectedValue == null)
                        OkButton.IsEnabled = false;
                    if (JenisKelaminCheck.IsChecked == true && JenisKelamin.SelectedValue == null)
                        OkButton.IsEnabled = false;
                    if (TipeKeluargaCheck.IsChecked == true && TipeKeluarga.SelectedValue == null)
                        OkButton.IsEnabled = false;
                    if (DepartemenCheck.IsChecked == true && Departemen.SelectedValue == null)
                        OkButton.IsEnabled = false;
                    if (MkgCheck.IsChecked == true && string.IsNullOrEmpty(Mkg.Text))
                        OkButton.IsEnabled = false;
                    if (PegawaiCheck.IsChecked == true && string.IsNullOrEmpty(NoPegawai.Text))
                        OkButton.IsEnabled = false;
                    if (StatusPegawaiCheck.IsChecked == true && StatusPegawai.SelectedValue == null)
                        OkButton.IsEnabled = false;
                }
            }
        }
    }
}
