using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Views.Billing.Supervisi.Periode
{
    public partial class SettingTableFormView : UserControl
    {
        private readonly PeriodeViewModel _viewModel;
        public SettingTableFormView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (PeriodeViewModel)DataContext;

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
                { "Bulan", Bulan.IsChecked },
                { "TglMulaiTagih", TglMulaiTagih.IsChecked },
                { "PelangganAir", PelangganAir.IsChecked },
                { "PelangganLimbah", PelangganLimbah.IsChecked },
                { "PelangganL2T2", PelangganL2T2.IsChecked },
                { "RekeningAir", RekeningAir.IsChecked },
                { "RekeningLimbah", RekeningLimbah.IsChecked },
                { "RekeningL2T2", RekeningL2T2.IsChecked },
                { "Status", Status.IsChecked },
                { "JumlahPakaiAir", JumlahPakaiAir.IsChecked },
                { "JumlahKelainan", JumlahKelainan.IsChecked },
                { "JumlahTaksir", JumlahTaksir.IsChecked },
                { "PelangganAirPublish", PelangganAirPublish.IsChecked },
                { "PelangganLimbahPublish", PelangganLimbahPublish.IsChecked },
                { "PelangganLlttPublish", PelangganLlttPublish.IsChecked }
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

            if (Bulan.IsChecked == true)
                IsSelected = true;
            if (TglMulaiTagih.IsChecked == true)
                IsSelected = true;
            if (PelangganAir.IsChecked == true)
                IsSelected = true;
            if (PelangganLimbah.IsChecked == true)
                IsSelected = true;
            if (PelangganL2T2.IsChecked == true)
                IsSelected = true;
            if (RekeningAir.IsChecked == true)
                IsSelected = true;
            if (RekeningLimbah.IsChecked == true)
                IsSelected = true;
            if (RekeningL2T2.IsChecked == true)
                IsSelected = true;
            if (Status.IsChecked == true)
                IsSelected = true;
            if (JumlahPakaiAir.IsChecked == true)
                IsSelected = true;
            if (JumlahKelainan.IsChecked == true)
                IsSelected = true;
            if (JumlahTaksir.IsChecked == true)
                IsSelected = true;
            if (PelangganAirPublish.IsChecked == true)
                IsSelected = true;
            if (PelangganLimbahPublish.IsChecked == true)
                IsSelected = true;
            if (PelangganLlttPublish.IsChecked == true)
                IsSelected = true;

            return IsSelected;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            Bulan.IsChecked = true;
            TglMulaiTagih.IsChecked = true;
            PelangganAir.IsChecked = true;
            PelangganLimbah.IsChecked = true;
            PelangganL2T2.IsChecked = true;
            RekeningAir.IsChecked = true;
            RekeningLimbah.IsChecked = true;
            RekeningL2T2.IsChecked = true;
            Status.IsChecked = true;
            JumlahPakaiAir.IsChecked = true;
            JumlahKelainan.IsChecked = true;
            JumlahTaksir.IsChecked = true;
            PelangganAirPublish.IsChecked = true;
            PelangganLimbahPublish.IsChecked = true;
            PelangganLlttPublish.IsChecked = true;

        }

        private void Kosongkan_Click(object sender, RoutedEventArgs e)
        {
            Bulan.IsChecked = false;
            TglMulaiTagih.IsChecked = false;
            PelangganAir.IsChecked = false;
            PelangganLimbah.IsChecked = false;
            PelangganL2T2.IsChecked = false;
            RekeningAir.IsChecked = false;
            RekeningLimbah.IsChecked = false;
            RekeningL2T2.IsChecked = false;
            Status.IsChecked = false;
            JumlahPakaiAir.IsChecked = false;
            JumlahKelainan.IsChecked = false;
            JumlahTaksir.IsChecked = false;
            PelangganAirPublish.IsChecked = false;
            PelangganLimbahPublish.IsChecked = false;
            PelangganLlttPublish.IsChecked = false;
        }
    }
}
