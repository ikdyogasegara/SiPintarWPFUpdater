using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Views.Hublang.Pelayanan.BAPengembalian
{
    /// <summary>
    /// Interaction logic for SettingTableFormView.xaml
    /// </summary>
    public partial class SettingTableFormView : UserControl
    {
        private readonly BaPengembalianViewModel Vm;

        public SettingTableFormView(object dataContext)
        {
            InitializeComponent();
            Vm = dataContext as BaPengembalianViewModel;
            if (Vm != null)
                DataContext = Vm;

            CheckButton();
            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void Kosongkan_Click(object sender, RoutedEventArgs e)
        {
            UserInput.IsChecked = false;
            TanggalInput.IsChecked = false;
            RekairLama.IsChecked = false;
            NomorSambungan.IsChecked = false;
            Wilayah.IsChecked = false;
            NamaPelanggan.IsChecked = false;
            NoBeritaAcara.IsChecked = false;
            Alamat.IsChecked = false;
            Gol.IsChecked = false;
            Kelurahan.IsChecked = false;
            Kecamatan.IsChecked = false;
            Cabang.IsChecked = false;
            Alasan.IsChecked = false;
            RekairBaru.IsChecked = false;
            Keterangan.IsChecked = false;
            Bulan.IsChecked = false;
            KondisiMeter.IsChecked = false;
            Rayon.IsChecked = false;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            Rayon.IsChecked = true;
            TanggalInput.IsChecked = true;
            RekairLama.IsChecked = true;
            NomorSambungan.IsChecked = true;
            Wilayah.IsChecked = true;
            NamaPelanggan.IsChecked = true;
            NoBeritaAcara.IsChecked = true;
            Alamat.IsChecked = true;
            Gol.IsChecked = true;
            Kelurahan.IsChecked = true;
            Kecamatan.IsChecked = true;
            Cabang.IsChecked = true;
            Alasan.IsChecked = true;
            UserInput.IsChecked = true;
            RekairBaru.IsChecked = true;
            Keterangan.IsChecked = true;
            Bulan.IsChecked = true;
            KondisiMeter.IsChecked = true;
        }

        private bool CheckSelected()
        {
            bool IsSelected = false;

            if (KondisiMeter.IsChecked == true)
                IsSelected = true;
            if (Bulan.IsChecked == true)
                IsSelected = true;
            if (Keterangan.IsChecked == true)
                IsSelected = true;
            if (NomorSambungan.IsChecked == true)
                IsSelected = true;
            if (Wilayah.IsChecked == true)
                IsSelected = true;
            if (NamaPelanggan.IsChecked == true)
                IsSelected = true;
            if (NoBeritaAcara.IsChecked == true)
                IsSelected = true;
            if (Alamat.IsChecked == true)
                IsSelected = true;
            if (Gol.IsChecked == true)
                IsSelected = true;
            if (Kelurahan.IsChecked == true)
                IsSelected = true;
            if (Kecamatan.IsChecked == true)
                IsSelected = true;
            if (Alasan.IsChecked == true)
                IsSelected = true;
            if (UserInput.IsChecked == true)
                IsSelected = true;
            if (Keterangan.IsChecked == true)
                IsSelected = true;
            if (TanggalInput.IsChecked == true)
                IsSelected = true;
            if (RekairLama.IsChecked == true)
                IsSelected = true;
            if (RekairBaru.IsChecked == true)
                IsSelected = true;
            if (Rayon.IsChecked == true)
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
                { "Rayon", Rayon.IsChecked },
                { "Bulan", Bulan.IsChecked },
                { "RekairLama", RekairLama.IsChecked },
                { "NomorSambungan", NomorSambungan.IsChecked },
                { "Wilayah", Wilayah.IsChecked },
                { "NamaPelanggan", NamaPelanggan.IsChecked },
                { "NoBeritaAcara", NoBeritaAcara.IsChecked },
                { "Alamat", Alamat.IsChecked },
                { "Gol", Gol.IsChecked },
                { "Kelurahan", Kelurahan.IsChecked },
                { "Kecamatan", Kecamatan.IsChecked },
                { "Cabang", Cabang.IsChecked },
                { "Alasan", Alasan.IsChecked },
                { "UserInput", UserInput.IsChecked },
                { "Keterangan", Keterangan.IsChecked },
                { "TanggalInput", TanggalInput.IsChecked },
                { "RekairBaru", RekairBaru.IsChecked },
                { "KondisiMeter", KondisiMeter.IsChecked },
            };

            _ = Task.Run(() => ((AsyncCommandBase)Vm.OnSubmitSettingTableCommand).ExecuteAsync(param));
        }
    }
}
