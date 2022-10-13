using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.PostingKeuangan;

namespace SiPintar.Views.Akuntansi.PostingKeuangan.PenerimaanLainnya
{
    /// <summary>
    /// Interaction logic for DialogFormView.xaml
    /// </summary>
    public partial class DialogFormView : UserControl
    {
        private readonly PenerimaanLainnyaViewModel _viewModel;

        public DialogFormView(PenerimaanLainnyaViewModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (PenerimaanLainnyaViewModel)DataContext;

            InitView();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void InitView()
        {
            Title.Text = _viewModel.IsAdd ? "Tambah" : "Koreksi";
            Title.Text += " Data Penerimaan Lainnya";
            OkButton.Content = _viewModel.IsAdd ? "Tambah" : "Simpan";
            RdNormal.IsChecked = _viewModel.IsAdd || _viewModel.SelectedData?.Kategori == 0;
            RdPindahBuku.IsChecked = _viewModel.SelectedData?.Kategori == 1;

            Nominal.Text = DecimalValidationHelper.FormatDecimalToStringId(_viewModel.IsAdd ? 0 : _viewModel.SelectedData?.JumlahNominal);
            Nominal.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            Nominal.GotFocus += DecimalValidationHelper.Object_GotFocus;
            Nominal.LostFocus += DecimalValidationHelper.Object_LostFocus;

            TanggalDPL_SelectedDateChanged();
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
            if (TanggalDPL.SelectedDate == null)
                OkButton.IsEnabled = false;
            else if (KodeWilayah.SelectedItem == null)
                OkButton.IsEnabled = false;
            else if (KodeDebet.SelectedItem == null)
                OkButton.IsEnabled = false;
            else if (string.IsNullOrWhiteSpace(Uraian.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrWhiteSpace(Nominal.Text))
                OkButton.IsEnabled = false;
            else if (IsPeriodeTutupBuku())
                OkButton.IsEnabled = false;
            else
                OkButton.IsEnabled = true;
        }

        private void TanggalDPL_SelectedDateChanged(object sender = null, SelectionChangedEventArgs e = null)
        {
            ErrorTanggalDPL.Visibility = System.Windows.Visibility.Collapsed;

            if (IsPeriodeTutupBuku())
            {
                ErrorTanggalDPL.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                _viewModel.NotransForm = UniqueNumberHelper.Generate("dpl", _viewModel.WaktuInputForm, GetNewId());
            }

            CheckButton();
        }

        private bool IsPeriodeTutupBuku()
        {
            return _viewModel.PeriodeTutupBukuList
                .FirstOrDefault(x => x.KodePeriode.ToString() == _viewModel.WaktuInputForm.ToString("yyyyMM", null)) != null;
        }

        private int GetNewId()
        {
            var newId = 1;
            var bulan = int.Parse(_viewModel.WaktuInputForm.ToString("MM", null), null);
            var selectedDate = _viewModel.WaktuInputForm.ToString($"/{NumberToRomanHelper.getRomanNumber(bulan)}/yyyy", null);
            var data = _viewModel.DataList
                .Where(x => x.NomorTransaksi.Contains(selectedDate, StringComparison.Ordinal))
                .OrderByDescending(x => x.NomorTransaksi);

            if (data.Any())
                newId += int.Parse(data.First().NomorTransaksi[^1].ToString(null), null);

            return newId;
        }

        private void OkButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!_viewModel.IsAdd)
            {
                _viewModel.PenerimaanLainnyaForm.IdDaftarPenerimaanKas = _viewModel.SelectedData.IdDaftarPenerimaanKas;
                _viewModel.PenerimaanLainnyaForm.IdPeriode = _viewModel.SelectedData.IdPeriode;
            }
            else
            {
                _viewModel.PenerimaanLainnyaForm.IdPeriode = _viewModel.PeriodeList
                    .Where(x => x.KodePeriode.ToString() == _viewModel.WaktuInputForm.ToString("yyyyMM", null))
                    .Select(x => x.IdPeriode).FirstOrDefault();
                _viewModel.PenerimaanLainnyaForm.NomorTransaksi = _viewModel.NotransForm;
            }

            _viewModel.PenerimaanLainnyaForm.WaktuInput = _viewModel.WaktuInputForm;
            _viewModel.PenerimaanLainnyaForm.Kategori = RdNormal?.IsChecked == true ? 0 : 1;
            _viewModel.PenerimaanLainnyaForm.IdWilayah = _viewModel.SelectedWilayah.IdWilayah;
            _viewModel.PenerimaanLainnyaForm.IdPerkiraan3Debet = _viewModel.SelectedDebet.IdPerkiraan3;
            _viewModel.PenerimaanLainnyaForm.IdPerkiraan3Kredit = _viewModel.SelectedKredit.IdPerkiraan3;
            _viewModel.PenerimaanLainnyaForm.Uraian = _viewModel.UraianForm;
            _viewModel.PenerimaanLainnyaForm.JumlahNominal = DecimalValidationHelper.FormatStringIdToDecimal(Nominal.Text);

            _viewModel.OnSubmitFormCommand.Execute(null);
        }
    }
}
