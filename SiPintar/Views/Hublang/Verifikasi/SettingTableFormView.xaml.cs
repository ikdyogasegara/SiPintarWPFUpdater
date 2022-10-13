using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Hublang;

namespace SiPintar.Views.Hublang.Verifikasi
{
    /// <summary>
    /// Interaction logic for SettingTableFormView.xaml
    /// </summary>
    public partial class SettingTableFormView : UserControl
    {
        private readonly VerifikasiViewModel _vm;

        public SettingTableFormView(object dataContext)
        {
            InitializeComponent();
            _vm = dataContext as VerifikasiViewModel;
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
            Wilayah.IsChecked = false;
            NamaPelanggan.IsChecked = false;
            NoBeritaAcara.IsChecked = false;
            Alamat.IsChecked = false;
            Kelurahan.IsChecked = false;
            Kecamatan.IsChecked = false;
            Cabang.IsChecked = false;
            Alasan.IsChecked = false;
            Biaya.IsChecked = false;
            UserInput.IsChecked = false;
            UserBeritaAcara.IsChecked = false;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            Jenis.IsChecked = true;
            Status.IsChecked = true;
            NomorRegister.IsChecked = true;
            Wilayah.IsChecked = true;
            NamaPelanggan.IsChecked = true;
            NoBeritaAcara.IsChecked = true;
            Alamat.IsChecked = true;
            Kelurahan.IsChecked = true;
            Kecamatan.IsChecked = true;
            Cabang.IsChecked = true;
            Alasan.IsChecked = true;
            Biaya.IsChecked = true;
            UserInput.IsChecked = true;
            UserBeritaAcara.IsChecked = true;
        }

        private bool CheckSelected()
        {
            var isSelected = false;

            if (Jenis.IsChecked == true)
                isSelected = true;
            if (Status.IsChecked == true)
                isSelected = true;
            if (NomorRegister.IsChecked == true)
                isSelected = true;
            if (Wilayah.IsChecked == true)
                isSelected = true;
            if (NamaPelanggan.IsChecked == true)
                isSelected = true;
            if (NoBeritaAcara.IsChecked == true)
                isSelected = true;
            if (Alamat.IsChecked == true)
                isSelected = true;
            if (Kelurahan.IsChecked == true)
                isSelected = true;
            if (Kecamatan.IsChecked == true)
                isSelected = true;
            if (Alasan.IsChecked == true)
                isSelected = true;
            if (Biaya.IsChecked == true)
                isSelected = true;
            if (UserInput.IsChecked == true)
                isSelected = true;
            if (UserBeritaAcara.IsChecked == true)
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
                { "Wilayah", Wilayah.IsChecked },
                { "NamaPelanggan", NamaPelanggan.IsChecked },
                { "NoBeritaAcara", NoBeritaAcara.IsChecked },
                { "Alamat", Alamat.IsChecked },
                { "Kelurahan", Kelurahan.IsChecked },
                { "Kecamatan", Kecamatan.IsChecked },
                { "Cabang", Cabang.IsChecked },
                { "Alasan", Alasan.IsChecked },
                { "Biaya", Biaya.IsChecked },
                { "UserInput", UserInput.IsChecked },
                { "UserBeritaAcara", UserBeritaAcara.IsChecked },
            };

            _ = Task.Run(() => ((AsyncCommandBase)_vm.OnSubmitSettingTableCommand).ExecuteAsync(param));
        }
    }
}
