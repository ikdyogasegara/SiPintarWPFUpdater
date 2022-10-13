using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Hublang.Pelayanan.KoreksiRekeningAir;

namespace SiPintar.Views.Hublang.Pelayanan.KoreksiRekeningAir.PermohonanKoreksi
{
    public partial class SettingTableFormView : UserControl
    {
        private readonly PermohonanKoreksiViewModel _vm;

        public SettingTableFormView(object dataContext)
        {
            InitializeComponent();
            _vm = dataContext as PermohonanKoreksiViewModel;
            if (_vm != null)
                DataContext = _vm;

            CheckButton();
            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void Kosongkan_Click(object sender, RoutedEventArgs e)
        {
            Jenis.IsChecked = false;
            Status.IsChecked = false;
            NomorRegister.IsChecked = false;
            NamaPelanggan.IsChecked = false;
            Alamat.IsChecked = false;
            Alasan.IsChecked = false;
            Rayon.IsChecked = false;
            Wilayah.IsChecked = false;
            Kelurahan.IsChecked = false;
            Kecamatan.IsChecked = false;
            Cabang.IsChecked = false;

        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            Jenis.IsChecked = true;
            Status.IsChecked = true;
            NomorRegister.IsChecked = true;
            NamaPelanggan.IsChecked = true;
            Alamat.IsChecked = true;
            Alasan.IsChecked = true;
            Rayon.IsChecked = true;
            Wilayah.IsChecked = true;
            Kelurahan.IsChecked = true;
            Kecamatan.IsChecked = true;
            Cabang.IsChecked = true;
        }

        private bool CheckSelected()
        {
            bool isSelected;
            if (Jenis.IsChecked == true)
                _ = true;
            if (Status.IsChecked == true)
                _ = true;
            if (NomorRegister.IsChecked == true)
                _ = true;
            if (NamaPelanggan.IsChecked == true)
                _ = true;
            if (Alamat.IsChecked == true)
                _ = true;
            if (Alasan.IsChecked == true)
                _ = true;
            if (Rayon.IsChecked == true)
                _ = true;
            if (Wilayah.IsChecked == true)
                _ = true;
            if (Kelurahan.IsChecked == true)
                _ = true;
            if (Kecamatan.IsChecked == true)
                _ = true;
            if (Cabang.IsChecked == true)
                _ = true;
            isSelected = true;

            return isSelected;
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

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);

            var param = new Dictionary<string, bool?>
            {
                { "Jenis", Jenis.IsChecked },
                { "Status", Status.IsChecked },
                { "NomorRegister", NomorRegister.IsChecked },
                { "NamaPelanggan", NamaPelanggan.IsChecked },
                { "Alamat", Alamat.IsChecked },
                { "Alasan", Alasan.IsChecked },
                { "Rayon", Rayon.IsChecked },
                { "Wilayah", Wilayah.IsChecked },
                { "Kelurahan", Kelurahan.IsChecked },
                { "Kecamatan", Kecamatan.IsChecked },
                { "Cabang", Cabang.IsChecked },
            };

            _ = Task.Run(() => ((AsyncCommandBase)_vm.OnSubmitSettingTableCommand).ExecuteAsync(param));
        }
    }
}
