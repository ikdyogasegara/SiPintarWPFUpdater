using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Akuntansi.PostingKeuangan;

namespace SiPintar.Views.Akuntansi.PostingKeuangan.PengeluaranLainnya
{
    /// <summary>
    /// Interaction logic for SettingTableFormView.xaml
    /// </summary>
    public partial class SettingTableFormView : UserControl
    {
        private readonly PengeluaranLainnyaViewModel _viewModel;
        public SettingTableFormView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (PengeluaranLainnyaViewModel)DataContext;

            CheckButton();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);

            var param = new Dictionary<string, bool?>
            {
                { "NomorTransaksi", NomorTransaksi.IsChecked },
                { "KodeWilayah", KodeWilayah.IsChecked },
                { "NamaWilayah", NamaWilayah.IsChecked },
                { "KodePerkiraanDebet", KodePerkiraanDebet.IsChecked },
                { "NamaPerkiraanDebet", NamaPerkiraanDebet.IsChecked },
                { "KodePerkiraanKredit", KodePerkiraanKredit.IsChecked },
                { "NamaPerkiraanKredit", NamaPerkiraanKredit.IsChecked },
                { "Uraian", Uraian.IsChecked },
                { "JumlahNominal", JumlahNominal.IsChecked },
                { "TanggalTerima", TanggalTerima.IsChecked }
            };

            _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitSettingTableCommand).ExecuteAsync(param));
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

        private void CheckButton()
        {
            bool IsSelected = CheckSelected();

            if (IsSelected)
                OkButton.IsEnabled = true;
            else
                OkButton.IsEnabled = false;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e) => CheckButton();

        private bool CheckSelected()
        {
            bool IsSelected = false;

            if (NomorTransaksi.IsChecked == true)
                IsSelected = true;
            if (KodeWilayah.IsChecked == true)
                IsSelected = true;
            if (NamaWilayah.IsChecked == true)
                IsSelected = true;
            if (KodePerkiraanDebet.IsChecked == true)
                IsSelected = true;
            if (NamaPerkiraanDebet.IsChecked == true)
                IsSelected = true;
            if (KodePerkiraanKredit.IsChecked == true)
                IsSelected = true;
            if (NamaPerkiraanKredit.IsChecked == true)
                IsSelected = true;
            if (Uraian.IsChecked == true)
                IsSelected = true;
            if (JumlahNominal.IsChecked == true)
                IsSelected = true;
            if (TanggalTerima.IsChecked == true)
                IsSelected = true;

            return IsSelected;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            NomorTransaksi.IsChecked = true;
            KodeWilayah.IsChecked = true;
            NamaWilayah.IsChecked = true;
            KodePerkiraanDebet.IsChecked = true;
            NamaPerkiraanDebet.IsChecked = true;
            KodePerkiraanKredit.IsChecked = true;
            NamaPerkiraanKredit.IsChecked = true;
            Uraian.IsChecked = true;
            JumlahNominal.IsChecked = true;
            TanggalTerima.IsChecked = true;

        }

        private void Kosongkan_Click(object sender, RoutedEventArgs e)
        {
            NomorTransaksi.IsChecked = false;
            KodeWilayah.IsChecked = false;
            NamaWilayah.IsChecked = false;
            KodePerkiraanDebet.IsChecked = false;
            NamaPerkiraanDebet.IsChecked = false;
            KodePerkiraanKredit.IsChecked = false;
            NamaPerkiraanKredit.IsChecked = false;
            Uraian.IsChecked = false;
            JumlahNominal.IsChecked = false;
            TanggalTerima.IsChecked = false;
        }
    }
}
