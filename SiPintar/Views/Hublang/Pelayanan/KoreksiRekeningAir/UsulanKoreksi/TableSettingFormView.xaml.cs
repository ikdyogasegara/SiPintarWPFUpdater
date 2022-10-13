using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Hublang.Pelayanan.KoreksiRekeningAir;

namespace SiPintar.Views.Hublang.Pelayanan.KoreksiRekeningAir.UsulanKoreksi
{
    public partial class TableSettingFormView : UserControl
    {
        private readonly UsulanKoreksiViewModel _viewModel;

        public TableSettingFormView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (UsulanKoreksiViewModel)DataContext;

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
                {"NamaPelanggan", NamaPelanggan.IsChecked},
                {"Alamat", Alamat.IsChecked},
                {"NomorRegister", NomorRegister.IsChecked},
                {"Alasan", Alasan.IsChecked},
                {"Rayon", Rayon.IsChecked},
                {"Wilayah", Wilayah.IsChecked},
                {"Kelurahan", Kelurahan.IsChecked},
                {"NomorBeritaAcara", NomorBeritaAcara.IsChecked},
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

            if (NamaPelanggan.IsChecked == true)
                isSelected = true;
            if (Alamat.IsChecked == true)
                isSelected = true;
            if (NomorRegister.IsChecked == true)
                isSelected = true;
            if (Alasan.IsChecked == true)
                isSelected = true;
            if (Rayon.IsChecked == true)
                isSelected = true;
            if (Wilayah.IsChecked == true)
                isSelected = true;
            if (Kelurahan.IsChecked == true)
                isSelected = true;
            if (NomorBeritaAcara.IsChecked == true)
                isSelected = true;

            return isSelected;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            NamaPelanggan.IsChecked = true;
            Alamat.IsChecked = true;
            NomorRegister.IsChecked = true;
            Alasan.IsChecked = true;
            Rayon.IsChecked = true;
            Wilayah.IsChecked = true;
            Kelurahan.IsChecked = true;
            NomorBeritaAcara.IsChecked = true;
        }

        private void Kosongkan_Click(object sender, RoutedEventArgs e)
        {
            NamaPelanggan.IsChecked = false;
            Alamat.IsChecked = false;
            NomorRegister.IsChecked = false;
            Alasan.IsChecked = false;
            Rayon.IsChecked = false;
            Wilayah.IsChecked = false;
            Kelurahan.IsChecked = false;
            NomorBeritaAcara.IsChecked = false;
        }
    }
}
