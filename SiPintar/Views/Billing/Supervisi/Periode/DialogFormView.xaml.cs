using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Views.Billing.Supervisi.Periode
{
    public partial class DialogFormView : UserControl
    {
        private readonly PeriodeViewModel _viewModel;
        public DialogFormView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (PeriodeViewModel)DataContext;

            Title.Text = _viewModel.IsEdit ? "Koreksi Tanggal Denda" : "Buat Periode DRD baru";
            OkButton.Content = _viewModel.IsEdit ? "Simpan" : "Buat Periode";
            TglMulaiTagih.IsEnabled = !_viewModel.IsEdit;

            GenerateTanggal();
            CheckButton();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);

            // CheckRayon.Checked += CheckFilterChange;
            CheckRayon.Unchecked += (sender, args) => { _viewModel.SelectedRayon = null; };

            // CheckWilayah.Checked += CheckFilterChange;
            CheckWilayah.Unchecked += (sender, args) => { _viewModel.SelectedWilayah = null; };

            ComboRayon.SelectionChanged += ComboFilterChanged;
            ComboWilayah.SelectionChanged += ComboFilterChanged;
        }

        private void ComboFilterChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine("Combo");
            GetFilterData();
        }

        private void CheckFilterChange(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Check");
            GetFilterData();
        }

        private void GetFilterData()
        {
            _viewModel.OnGetTglDendaCommand.Execute(null);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);

            if (!_viewModel.IsEdit)
                _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitAddCommand).ExecuteAsync(null));
            else
                _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitEditCommand).ExecuteAsync(null));
        }

        private void Bulan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GenerateTanggal();
            CheckButton();
        }

        private void Tahun_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GenerateTanggal();
            CheckButton();
        }

        private void GenerateTanggal()
        {
            if (_viewModel.IsEdit)
                return;

            DateTime? TglMulaiTagih = null;
            DateTime? TglDenda1 = null;
            DateTime? TglDenda2 = null;
            DateTime? TglDenda3 = null;
            DateTime? TglDenda4 = null;
            DateTime? TglDendaPerBulan = null;

            if (Bulan.SelectedItem != null && Tahun.SelectedItem != null)
            {
                var dateTime = new DateTime(Convert.ToInt32(_viewModel.TahunForm.Key, null), Convert.ToInt32(_viewModel.BulanForm.Key, null), 1);

                TglMulaiTagih = dateTime.AddMonths(1);
                TglDenda1 = dateTime;
                TglDenda2 = TglDenda1?.AddMonths(1);
                TglDenda3 = TglDenda2?.AddMonths(1);
                TglDenda4 = TglDenda3?.AddMonths(1);
                TglDendaPerBulan = TglDenda1;
            }

            if (_viewModel != null)
            {
                _viewModel.TglMulaiTagihForm = TglMulaiTagih;
                _viewModel.TglDenda1Form = TglDenda1;
                _viewModel.TglDenda2Form = TglDenda2;
                _viewModel.TglDenda3Form = TglDenda3;
                _viewModel.TglDenda4Form = TglDenda4;
                _viewModel.TglMulaiDendaForm = TglDendaPerBulan;
            }
        }

        private void CheckButton()
        {
            if (Bulan.SelectedItem == null || Tahun.SelectedItem == null ||
                string.IsNullOrEmpty(TglMulaiTagih.Text) ||
                string.IsNullOrEmpty(Tgl1.Text) || string.IsNullOrEmpty(Tgl2.Text) ||
                string.IsNullOrEmpty(Tgl3.Text) || string.IsNullOrEmpty(Tgl4.Text) ||
                Agreement.IsChecked == null || (bool)!Agreement.IsChecked)
                OkButton.IsEnabled = false;
            else
                OkButton.IsEnabled = true;
        }

        private void CheckboxButton_Checked(object sender, RoutedEventArgs e) => CheckButton();

        private void Tgl_SelectionChanged(object sender, SelectionChangedEventArgs e) => CheckButton();
    }
}
