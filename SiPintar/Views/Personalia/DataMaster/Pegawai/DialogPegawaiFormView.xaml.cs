using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Personalia.DataMaster;

namespace SiPintar.Views.Personalia.DataMaster.Pegawai
{
    public partial class DialogPegawaiFormView : UserControl
    {
        private readonly PegawaiViewModel _viewModel;
        public DialogPegawaiFormView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (PegawaiViewModel)DataContext;

            CheckButton();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

        private void CheckForm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox cbForm)
            {
                if (cbForm.Name == "StatusPegawai")
                {
                    if ((cbForm.SelectedValue as int?) == 1)
                        _viewModel.IsStatusPegawaiTetap = true;
                    else
                        _viewModel.IsStatusPegawaiTetap = false;
                }
                if (cbForm.Name == "Departemen")
                {
                    _viewModel.DivisiCopyList = new ObservableCollection<MasterDivisiDto>(_viewModel.DivisiList.Where(d => d.IdDepartemen == (cbForm.SelectedValue as int?)));
                }
            }

            CheckButton();
        }

        private void RadioButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;

            if (radioButton.GroupName == "FlagKawin")
                _viewModel.FormData.FlagKawin = radioButton.Name == "FlagKawinTrue";
            else if (radioButton.GroupName == "JenisKelamin")
                _viewModel.FormData.IdJenisKelamin = radioButton.Name == "JenisKelamin1" ? 1 : 2;

            CheckButton();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void CheckButton()
        {
            if (_viewModel.FormCurrentPage == 1)
            {
                if (string.IsNullOrEmpty(Nik.Text))
                    OkButton.IsEnabled = false;
                else if (string.IsNullOrEmpty(Pin.Text))
                    OkButton.IsEnabled = false;
                else if (string.IsNullOrEmpty(NamaPegawai.Text))
                    OkButton.IsEnabled = false;
                else if (string.IsNullOrEmpty(TempatLahir.Text))
                    OkButton.IsEnabled = false;
                else if (TglLahir.SelectedDate == null)
                    OkButton.IsEnabled = false;
                else if (string.IsNullOrEmpty(NoPenduduk.Text))
                    OkButton.IsEnabled = false;
                else if (JenisKelamin1.IsChecked == null && JenisKelamin2.IsChecked == null)
                    OkButton.IsEnabled = false;
                else if (Pendidikan.SelectedValue == null)
                    OkButton.IsEnabled = false;
                else if (string.IsNullOrEmpty(TahunLulus.Text))
                    OkButton.IsEnabled = false;
                else if (Agama.SelectedValue == null)
                    OkButton.IsEnabled = false;
                else if (string.IsNullOrEmpty(Alamat.Text))
                    OkButton.IsEnabled = false;
                else if (string.IsNullOrEmpty(AlamatKtp.Text))
                    OkButton.IsEnabled = false;
                else if (FlagKawinTrue.IsChecked == null && FlagKawinFalse.IsChecked == null)
                    OkButton.IsEnabled = false;
                else if (FlagKawinTrue.IsChecked == true && TglKawin.SelectedDate == null)
                    OkButton.IsEnabled = false;
                else
                    OkButton.IsEnabled = true;
            }
            else if (_viewModel.FormCurrentPage == 2)
            {
                if (StatusPegawai.SelectedValue == null)
                    OkButton.IsEnabled = false;
                else if (Jabatan.SelectedValue == null)
                    OkButton.IsEnabled = false;
                else if (Departemen.SelectedValue == null)
                    OkButton.IsEnabled = false;
                else if (TglMasuk.SelectedDate == null)
                    OkButton.IsEnabled = false;
                else if (Tgl80.SelectedDate == null)
                    OkButton.IsEnabled = false;
                else if ((int?)StatusPegawai.SelectedValue == 1 && Tgl100.SelectedDate == null)
                    OkButton.IsEnabled = false;
                else if (Divisi.SelectedValue == null)
                    OkButton.IsEnabled = false;
                else if (string.IsNullOrEmpty(Tugas.Text))
                    OkButton.IsEnabled = false;
                else if (AreaKerja.SelectedValue == null)
                    OkButton.IsEnabled = false;
                else if (TmtJabatan.SelectedDate == null)
                    OkButton.IsEnabled = false;
                else if (GolonganPegawai.SelectedValue == null)
                    OkButton.IsEnabled = false;
                else if (TmtGolongan.SelectedDate == null)
                    OkButton.IsEnabled = false;
                else if ((int?)StatusPegawai.SelectedValue == 1 && TglBerkalaSebelumnya.SelectedDate == null)
                    OkButton.IsEnabled = false;
                else if ((int?)StatusPegawai.SelectedValue == 1 && string.IsNullOrEmpty(SkBerkalaSebelumnya.Text))
                    OkButton.IsEnabled = false;
                else if ((int?)StatusPegawai.SelectedValue == 1 && TglPangkatSebelumnya.SelectedDate == null)
                    OkButton.IsEnabled = false;
                else if ((int?)StatusPegawai.SelectedValue == 1 && string.IsNullOrEmpty(SkPangkatSebelumnya.Text))
                    OkButton.IsEnabled = false;
                else if ((int?)StatusPegawai.SelectedValue == 1 && TglBerkala.SelectedDate == null)
                    OkButton.IsEnabled = false;
                else if ((int?)StatusPegawai.SelectedValue == 1 && TglPangkat.SelectedDate == null)
                    OkButton.IsEnabled = false;
                else
                    OkButton.IsEnabled = true;
            }
            else
                OkButton.IsEnabled = true;
        }


    }
}
