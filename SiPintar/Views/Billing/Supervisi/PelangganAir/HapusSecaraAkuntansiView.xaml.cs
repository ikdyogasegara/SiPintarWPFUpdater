using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Views.Billing.Supervisi.PelangganAir
{
    public partial class HapusSecaraAkuntansiView : UserControl
    {
        private readonly PelangganAirViewModel _vm;
        public HapusSecaraAkuntansiView(object dataContext)
        {
            InitializeComponent();
            _vm = dataContext as PelangganAirViewModel;
            DataContext = _vm;
            CheckButton();
            PreviewKeyUp += new KeyEventHandler(HandleKey);
        }

        private void HandleKey(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
        }

        private void CheckButton()
        {
            if (_vm.KodePeriodeHapusSecaraAkuntansi1 != null && _vm.KodePeriodeHapusSecaraAkuntansi2 != null && _vm.KodePeriodeHapusSecaraAkuntansi2 >= _vm.KodePeriodeHapusSecaraAkuntansi1)
            {
                OkButton.IsEnabled = true;
            }
            else
            {
                OkButton.IsEnabled = false;
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
            _ = ((AsyncCommandBase)_vm.OnSubmitHapusSecaraAkuntansiCommand).ExecuteAsync(null);
        }

        private void Bulan_OnSelectionChanged(object sender, SelectionChangedEventArgs e) => CheckButton();
    }
}
