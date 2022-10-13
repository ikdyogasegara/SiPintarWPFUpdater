using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Views.Billing.Supervisi.PenghapusanRekening
{
    public partial class SettingTableFormView : UserControl
    {
        private readonly PenghapusanRekeningViewModel _viewModel;
        public SettingTableFormView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (PenghapusanRekeningViewModel)DataContext;

            CheckButton();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);

            var param = new Dictionary<string, bool?>
            {
                { "NoSamb", NoSamb.IsChecked },
                { "Golongan", Golongan.IsChecked },
                { "Rayon", Rayon.IsChecked },
                { "Wilayah", Wilayah.IsChecked },
                { "Kelurahan", Kelurahan.IsChecked },
                { "Alamat", Alamat.IsChecked },
                { "StanLalu", StanLalu.IsChecked },
                { "StanSkrg", StanSkrg.IsChecked },
                { "StanAngkat", StanAngkat.IsChecked },
                { "Pakai", Pakai.IsChecked },
                { "BiayaPemakaian", BiayaPemakaian.IsChecked },
                { "Administrasi", Administrasi.IsChecked },
                { "Pemeliharaan", Pemeliharaan.IsChecked },
                { "Retribusi", Retribusi.IsChecked },
                { "Pelayanan", Pelayanan.IsChecked },
                { "AdministrasiLain", AdministrasiLain.IsChecked },
                { "PemeliharaanLain", PemeliharaanLain.IsChecked },
                { "RetribusiLain", RetribusiLain.IsChecked },
                { "Meterai", Meterai.IsChecked },
                { "Ppn", Ppn.IsChecked },
                { "DendaPakai0", DendaPakai0.IsChecked },
                { "AirLimbah", AirLimbah.IsChecked },
                { "Rekair", Rekair.IsChecked }
            };

            _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitSettingTableCommand).ExecuteAsync(param));
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

        private void CheckButton()
        {
            var isSelected = CheckSelected();

            if (isSelected)
                OkButton.IsEnabled = true;
            else
                OkButton.IsEnabled = false;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e) => CheckButton();

        private bool CheckSelected()
        {
            var isSelected = false;

            if (NoSamb.IsChecked == true)
                isSelected = true;
            if (Golongan.IsChecked == true)
                isSelected = true;
            if (Rayon.IsChecked == true)
                isSelected = true;
            if (Wilayah.IsChecked == true)
                isSelected = true;
            if (Kelurahan.IsChecked == true)
                isSelected = true;
            if (Alamat.IsChecked == true)
                isSelected = true;

            if (StanLalu.IsChecked == true)
                isSelected = true;
            if (StanSkrg.IsChecked == true)
                isSelected = true;
            if (StanAngkat.IsChecked == true)
                isSelected = true;
            if (Pakai.IsChecked == true)
                isSelected = true;
            if (BiayaPemakaian.IsChecked == true)
                isSelected = true;
            if (Administrasi.IsChecked == true)
                isSelected = true;
            if (Pemeliharaan.IsChecked == true)
                isSelected = true;
            if (Retribusi.IsChecked == true)
                isSelected = true;
            if (Pelayanan.IsChecked == true)
                isSelected = true;
            if (DendaPakai0.IsChecked == true)
                isSelected = true;
            if (AirLimbah.IsChecked == true)
                isSelected = true;
            if (AdministrasiLain.IsChecked == true)
                isSelected = true;
            if (PemeliharaanLain.IsChecked == true)
                isSelected = true;
            if (RetribusiLain.IsChecked == true)
                isSelected = true;
            if (Ppn.IsChecked == true)
                isSelected = true;
            if (Meterai.IsChecked == true)
                isSelected = true;
            if (Rekair.IsChecked == true)
                isSelected = true;

            return isSelected;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            NoSamb.IsChecked = true;
            Golongan.IsChecked = true;
            Rayon.IsChecked = true;
            Wilayah.IsChecked = true;
            Kelurahan.IsChecked = true;
            StanLalu.IsChecked = true;
            StanSkrg.IsChecked = true;
            StanAngkat.IsChecked = true;
            Pakai.IsChecked = true;
            BiayaPemakaian.IsChecked = true;
            Administrasi.IsChecked = true;
            Pemeliharaan.IsChecked = true;
            Retribusi.IsChecked = true;
            Pelayanan.IsChecked = true;
            DendaPakai0.IsChecked = true;
            AirLimbah.IsChecked = true;
            AdministrasiLain.IsChecked = true;
            PemeliharaanLain.IsChecked = true;
            RetribusiLain.IsChecked = true;
            Ppn.IsChecked = true;
            Meterai.IsChecked = true;
            Rekair.IsChecked = true;
        }

        private void Kosongkan_Click(object sender, RoutedEventArgs e)
        {
            NoSamb.IsChecked = false;
            Golongan.IsChecked = false;
            Rayon.IsChecked = false;
            Wilayah.IsChecked = false;
            Kelurahan.IsChecked = false;
            StanLalu.IsChecked = false;
            StanSkrg.IsChecked = false;
            StanAngkat.IsChecked = false;
            Pakai.IsChecked = false;
            BiayaPemakaian.IsChecked = false;
            Administrasi.IsChecked = false;
            Pemeliharaan.IsChecked = false;
            Retribusi.IsChecked = false;
            Pelayanan.IsChecked = false;
            DendaPakai0.IsChecked = false;
            AirLimbah.IsChecked = false;
            AdministrasiLain.IsChecked = false;
            PemeliharaanLain.IsChecked = false;
            RetribusiLain.IsChecked = false;
            Ppn.IsChecked = false;
            Meterai.IsChecked = false;
            Rekair.IsChecked = false;
        }
    }
}
