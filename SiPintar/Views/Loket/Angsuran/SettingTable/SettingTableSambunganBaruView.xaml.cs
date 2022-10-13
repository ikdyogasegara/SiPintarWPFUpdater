using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Loket.Angsuran;

namespace SiPintar.Views.Loket.Angsuran.SettingTable
{
    public partial class SettingTableSambunganBaruView : UserControl
    {
        private readonly AngsuranSambunganBaruViewModel _viewModel;
        public SettingTableSambunganBaruView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (AngsuranSambunganBaruViewModel)DataContext;

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
                { "Nama", Nama.IsChecked },
                { "JenisAngsuran", JenisAngsuran.IsChecked },
                { "NoAngsuran", NoAngsuran.IsChecked },
                { "Termin", Termin.IsChecked },
                { "Jumlah", Jumlah.IsChecked },
                { "Alamat", Alamat.IsChecked },
                { "WaktuDaftar", WaktuDaftar.IsChecked },
                { "DibebankanKepada", DibebankanKepada.IsChecked },
                { "NoSamb", NoSamb.IsChecked },
                { "NomorBA", NomorBA.IsChecked },
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

            if (Nama.IsChecked == true)
                IsSelected = true;
            if (JenisAngsuran.IsChecked == true)
                IsSelected = true;
            if (NoAngsuran.IsChecked == true)
                IsSelected = true;
            if (Termin.IsChecked == true)
                IsSelected = true;
            if (Jumlah.IsChecked == true)
                IsSelected = true;
            if (Alamat.IsChecked == true)
                IsSelected = true;
            if (WaktuDaftar.IsChecked == true)
                IsSelected = true;
            if (DibebankanKepada.IsChecked == true)
                IsSelected = true;
            if (NoSamb.IsChecked == true)
                IsSelected = true;
            if (NomorBA.IsChecked == true)
                IsSelected = true;

            return IsSelected;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            Nama.IsChecked = true;
            JenisAngsuran.IsChecked = true;
            NoAngsuran.IsChecked = true;
            Termin.IsChecked = true;
            Jumlah.IsChecked = true;
            Alamat.IsChecked = true;
            WaktuDaftar.IsChecked = true;
            DibebankanKepada.IsChecked = true;
            NoSamb.IsChecked = true;
            NomorBA.IsChecked = true;
        }

        private void Kosongkan_Click(object sender, RoutedEventArgs e)
        {
            Nama.IsChecked = false;
            JenisAngsuran.IsChecked = false;
            NoAngsuran.IsChecked = false;
            Termin.IsChecked = false;
            Jumlah.IsChecked = false;
            Alamat.IsChecked = false;
            WaktuDaftar.IsChecked = false;
            DibebankanKepada.IsChecked = false;
            NoSamb.IsChecked = false;
            NomorBA.IsChecked = false;
        }
    }
}
