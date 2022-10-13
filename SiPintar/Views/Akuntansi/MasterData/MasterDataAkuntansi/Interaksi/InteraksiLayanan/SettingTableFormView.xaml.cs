using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi;

namespace SiPintar.Views.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi.InteraksiLayanan
{
    /// <summary>
    /// Interaction logic for SettingTableFormView.xaml
    /// </summary>
    public partial class SettingTableFormView : UserControl
    {
        private readonly InteraksiLayananViewModel _viewModel;

        public SettingTableFormView(InteraksiLayananViewModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (InteraksiLayananViewModel)DataContext;

            CheckButton();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void Kosongkan_Click(object sender, RoutedEventArgs e)
        {
            KodeWilayah.IsChecked = false;
            NamaWilayah.IsChecked = false;
            KodeGolongan.IsChecked = false;
            NamaGolongan.IsChecked = false;
            KodePerkiraanDebet.IsChecked = false;
            NamaPerkiraanDebet.IsChecked = false;
            KodePerkiraanKredit.IsChecked = false;
            NamaPerkiraanKredit.IsChecked = false;
            FlagPembentukRekair.IsChecked = false;
            Keterangan.IsChecked = false;
            KodePerkiraan.IsChecked = false;
            NamaPerkiraan.IsChecked = false;
            KodeJenisNonAir.IsChecked = false;
            NamaJenisNonAir.IsChecked = false;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            KodeWilayah.IsChecked = true;
            NamaWilayah.IsChecked = true;
            KodeGolongan.IsChecked = true;
            NamaGolongan.IsChecked = true;
            KodePerkiraanDebet.IsChecked = true;
            NamaPerkiraanDebet.IsChecked = true;
            KodePerkiraanKredit.IsChecked = true;
            NamaPerkiraanKredit.IsChecked = true;
            FlagPembentukRekair.IsChecked = true;
            Keterangan.IsChecked = true;
            KodePerkiraan.IsChecked = true;
            NamaPerkiraan.IsChecked = true;
            KodeJenisNonAir.IsChecked = true;
            NamaJenisNonAir.IsChecked = true;
        }

        private bool CheckSelected()
        {
            bool IsSelected = false;

            if (KodeWilayah.IsChecked == true)
                IsSelected = true;
            if (NamaWilayah.IsChecked == true)
                IsSelected = true;
            if (KodeGolongan.IsChecked == true)
                IsSelected = true;
            if (NamaGolongan.IsChecked == true)
                IsSelected = true;
            if (KodePerkiraanDebet.IsChecked == true)
                IsSelected = true;
            if (NamaPerkiraanDebet.IsChecked == true)
                IsSelected = true;
            if (KodePerkiraanKredit.IsChecked == true)
                IsSelected = true;
            if (NamaPerkiraanKredit.IsChecked == true)
                IsSelected = true;
            if (FlagPembentukRekair.IsChecked == true)
                IsSelected = true;
            if (Keterangan.IsChecked == true)
                IsSelected = true;
            if (KodePerkiraan.IsChecked == true)
                IsSelected = true;
            if (NamaPerkiraan.IsChecked == true)
                IsSelected = true;
            if (KodeJenisNonAir.IsChecked == true)
                IsSelected = true;
            if (NamaJenisNonAir.IsChecked == true)
                IsSelected = true;


            return IsSelected;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e) => CheckButton();

        private void CheckButton()
        {
            bool IsSelected = CheckSelected();

            if (IsSelected)
                OkButton.IsEnabled = true;
            else
                OkButton.IsEnabled = false;
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

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
                { "KodeWilayah", KodeWilayah.IsChecked},
                { "NamaWilayah", NamaWilayah.IsChecked},
                { "KodeGolongan", KodeGolongan.IsChecked},
                { "NamaGolongan", NamaGolongan.IsChecked},
                { "KodePerkiraan3Debet", KodePerkiraanDebet.IsChecked},
                { "NamaPerkiraan3Debet", NamaPerkiraanDebet.IsChecked},
                { "KodePerkiraan3Kredit", KodePerkiraanKredit.IsChecked},
                { "NamaPerkiraan3Kredit", NamaPerkiraanKredit.IsChecked},
                { "FlagPembentukRekair", FlagPembentukRekair.IsChecked},
                { "Keterangan", Keterangan.IsChecked},
                { "KodePerkiraan3", KodePerkiraan.IsChecked},
                { "NamaPerkiraan3", NamaPerkiraan.IsChecked},
                { "KodeJenisNonAir", KodeJenisNonAir.IsChecked},
                { "NamaJenisNonAir", NamaJenisNonAir.IsChecked},

            };

            _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitSettingTableFormCommand).ExecuteAsync(param));
        }
    }
}
