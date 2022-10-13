using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Views.Hublang.Pelayanan.TagihanManual
{
    /// <summary>
    /// Interaction logic for SettingTableFormView.xaml
    /// </summary>
    public partial class SettingTableFormView : UserControl
    {
        private readonly TagihanManualViewModel Vm;

        public SettingTableFormView(object dataContext)
        {
            InitializeComponent();
            Vm = dataContext as TagihanManualViewModel;
            if (Vm != null)
                DataContext = Vm;

            CheckButton();
            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void Kosongkan_Click(object sender, RoutedEventArgs e)
        {
            JenisTipePelanggan.IsChecked = false;
            NomorNonAir.IsChecked = false;
            KodeJenisNonAir.IsChecked = false;
            NamaJenisNonAir.IsChecked = false;
            Total.IsChecked = false;
            NomorPelanggan.IsChecked = false;
            Nama.IsChecked = false;
            Alamat.IsChecked = false;
            Keterangan.IsChecked = false;
            KodeTarif.IsChecked = false;
            NamaTarif.IsChecked = false;
            KodeRayon.IsChecked = false;
            NamaRayon.IsChecked = false;
            KodeWilayah.IsChecked = false;
            NamaWilayah.IsChecked = false;
            KodeKelurahan.IsChecked = false;
            NamaKelurahan.IsChecked = false;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            JenisTipePelanggan.IsChecked = true;
            NomorNonAir.IsChecked = true;
            KodeJenisNonAir.IsChecked = true;
            NamaJenisNonAir.IsChecked = true;
            Total.IsChecked = true;
            NomorPelanggan.IsChecked = true;
            Nama.IsChecked = true;
            Alamat.IsChecked = true;
            Keterangan.IsChecked = true;
            KodeTarif.IsChecked = true;
            NamaTarif.IsChecked = true;
            KodeRayon.IsChecked = true;
            NamaRayon.IsChecked = true;
            KodeWilayah.IsChecked = true;
            NamaWilayah.IsChecked = true;
            KodeKelurahan.IsChecked = true;
            NamaKelurahan.IsChecked = true;
        }

        private bool CheckSelected()
        {
            var isSelected = false;

            if (JenisTipePelanggan.IsChecked == true)
                isSelected = true;
            if (NomorNonAir.IsChecked == true)
                isSelected = true;
            if (KodeJenisNonAir.IsChecked == true)
                isSelected = true;
            if (NamaJenisNonAir.IsChecked == true)
                isSelected = true;
            if (Total.IsChecked == true)
                isSelected = true;
            if (NomorPelanggan.IsChecked == true)
                isSelected = true;
            if (Nama.IsChecked == true)
                isSelected = true;
            if (Alamat.IsChecked == true)
                isSelected = true;
            if (Keterangan.IsChecked == true)
                isSelected = true;
            if (KodeTarif.IsChecked == true)
                isSelected = true;
            if (NamaTarif.IsChecked == true)
                isSelected = true;
            if (KodeRayon.IsChecked == true)
                isSelected = true;
            if (NamaRayon.IsChecked == true)
                isSelected = true;
            if (KodeWilayah.IsChecked == true)
                isSelected = true;
            if (NamaWilayah.IsChecked == true)
                isSelected = true;
            if (KodeKelurahan.IsChecked == true)
                isSelected = true;
            if (NamaKelurahan.IsChecked == true)
                isSelected = true;

            return isSelected;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);

            var param = new Dictionary<string, bool?>
            {
                { "JenisTipePelanggan", JenisTipePelanggan.IsChecked },
                { "NomorNonAir", NomorNonAir.IsChecked },
                { "KodeJenisNonAir", KodeJenisNonAir.IsChecked },
                { "NamaJenisNonAir", NamaJenisNonAir.IsChecked },
                { "Total", Total.IsChecked },
                { "NomorPelanggan", NomorPelanggan.IsChecked },
                { "Nama", Nama.IsChecked },
                { "Alamat", Alamat.IsChecked },
                { "Keterangan", Keterangan.IsChecked },
                { "KodeTarif", KodeTarif.IsChecked },
                { "NamaTarif", NamaTarif.IsChecked },
                { "KodeRayon", KodeRayon.IsChecked },
                { "NamaRayon", NamaRayon.IsChecked },
                { "KodeWilayah", KodeWilayah.IsChecked },
                { "NamaWilayah", NamaWilayah.IsChecked },
                { "KodeKelurahan", KodeKelurahan.IsChecked },
                { "NamaKelurahan", NamaKelurahan.IsChecked },
            };

            _ = Task.Run(() => ((AsyncCommandBase)Vm.OnSubmitSettingTableCommand).ExecuteAsync(param));
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e) => CheckButton();

        private void CheckButton()
        {
            var isSelected = CheckSelected();

            if (isSelected)
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


    }
}
