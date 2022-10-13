using System;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shell;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.PostingKeuangan;

namespace SiPintar.Views.Akuntansi.PostingKeuangan.PengeluaranLainnya
{
    /// <summary>
    /// Interaction logic for DialogFormView.xaml
    /// </summary>
    public partial class DialogFormView : UserControl
    {
        private readonly PengeluaranLainnyaViewModel _viewModel;

        public DialogFormView(PengeluaranLainnyaViewModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (PengeluaranLainnyaViewModel)DataContext;

            InitView();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }


        private void InitView()
        {
            Title.Text = _viewModel.IsAdd ? "Tambah" : "Koreksi";
            Title.Text += " Data Pengeluaran Lainnya";
            OkButton.Content = _viewModel.IsAdd ? "Tambah" : "Simpan";
            RadioNormal.Tag = _viewModel.PengeluaranLainnyaForm?.Kategori == 0;
            RadioPindahBuku.Tag = _viewModel.PengeluaranLainnyaForm?.Kategori == 1;
            Nominal.Text = DecimalValidationHelper.FormatDecimalToStringId(_viewModel.PengeluaranLainnyaForm?.JumlahNominal);

            Nominal.GotFocus += DecimalValidationHelper.Object_GotFocus;
            Nominal.LostFocus += DecimalValidationHelper.Object_LostFocus;

            Nominal.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;

            CheckButton();
        }


        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

        private void CheckButton()
        {

            if (_viewModel.PeriodeTutupBukuList.FirstOrDefault(x => x.KodePeriode.ToString() == _viewModel.PengeluaranLainnyaForm.WaktuInput.ToString("yyyyMM", null)) != null)
            {
                ValidasiPeriode.Visibility = System.Windows.Visibility.Visible;
                OkButton.IsEnabled = false;
            }
            else if (string.IsNullOrEmpty(KodeWilayah.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(KodeDebet.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(KodeKredit.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(Penjelasan.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(Nominal.Text))
                OkButton.IsEnabled = false;
            else
                OkButton.IsEnabled = true;

        }

        private void TanggalDBL_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ValidasiPeriode.Visibility = System.Windows.Visibility.Collapsed;
            CheckButton();
            if (!_viewModel.IsAdd)
                return;

            var urutan = _viewModel.DataList.Where(x => x.WaktuInput.ToString("yyyy-MM", null).Contains(_viewModel.PengeluaranLainnyaForm.WaktuInput.ToString("yyyy-MM", null), StringComparison.Ordinal)).Count() + 1;

            NomorDBL.Text = UniqueNumberHelper.Generate("dbl", _viewModel.PengeluaranLainnyaForm.WaktuInput, urutan);
            _viewModel.PengeluaranLainnyaForm.NomorTransaksi = NomorDBL.Text;
        }

        private void OkButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!_viewModel.IsAdd)
                _viewModel.PengeluaranLainnyaForm.IdDaftarBiayaKas = _viewModel.SelectedData.IdDaftarBiayaKas;

            _viewModel.PengeluaranLainnyaForm.IdPeriode = _viewModel.PeriodeList?
                .FirstOrDefault(x => Convert.ToString(x.KodePeriode, CultureInfo.InvariantCulture) == _viewModel.PengeluaranLainnyaForm.WaktuInput.ToString("yyyyMM", CultureInfo.CurrentCulture))?.IdPeriode ?? 0;
            _viewModel.PengeluaranLainnyaForm.KodePeriode = _viewModel.PeriodeList?
                .FirstOrDefault(x => Convert.ToString(x.KodePeriode, CultureInfo.InvariantCulture) == _viewModel.PengeluaranLainnyaForm.WaktuInput.ToString("yyyyMM", CultureInfo.CurrentCulture))?.KodePeriode ?? 0;
            _viewModel.PengeluaranLainnyaForm.NamaPeriode = _viewModel.PeriodeList?
                .FirstOrDefault(x => Convert.ToString(x.KodePeriode, CultureInfo.InvariantCulture) == _viewModel.PengeluaranLainnyaForm.WaktuInput.ToString("yyyyMM", CultureInfo.CurrentCulture))?.NamaPeriode ?? "";
            _viewModel.PengeluaranLainnyaForm.Kategori = RadioNormal?.IsChecked == true ? 0 : 1;
            _viewModel.PengeluaranLainnyaForm.IdWilayah = _viewModel.SelectedWilayah.IdWilayah;
            _viewModel.PengeluaranLainnyaForm.SumberData = 2;
            _viewModel.PengeluaranLainnyaForm.IdJenisHutang = 1; // ini masih static, api belum ada
            _viewModel.PengeluaranLainnyaForm.IdPerkiraan3Debet = _viewModel.SelectedDebet.IdPerkiraan3;
            _viewModel.PengeluaranLainnyaForm.IdPerkiraan3Kredit = _viewModel.SelectedKredit.IdPerkiraan3;
            _viewModel.PengeluaranLainnyaForm.Uraian = Penjelasan.Text;
            _viewModel.PengeluaranLainnyaForm.JumlahNominal = DecimalValidationHelper.FormatStringIdToDecimal(Nominal.Text);

            _viewModel.OnSubmitFormCommand.Execute(null);
        }

        private void JenisHutang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckButton();
        }

        private void RadioNormal_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CheckButton();
        }

    }
}
