using System.Windows;
using System.Windows.Controls;
using SiPintar.Commands;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.Jurnal.Instalasi;

namespace SiPintar.Views.Akuntansi.Jurnal.Instalasi
{
    /// <summary>
    /// Interaction logic for DaftarHutangView.xaml
    /// </summary>
    public partial class DaftarHutangView : UserControl
    {
        public DaftarHutangView()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is DaftarHutangViewModel vm && CheckButton())
            {
                if (vm.PeriodeAwal!.Value.Month != vm.PeriodeAkhir!.Value.Month)
                {
                    _ = DialogHelper.ShowDialogGlobalViewAsync(
                false,
                false,
                "AkuntansiRootDialog",
                $"Daftar Hutang Sudah & Harus Dibayar (DHHD)",
                $"Maaf, Pengisian tanggal harus dalam bulan yang sama.",
                "warning",
                "Ok",
                false,
                "Akuntansi");
                }
                else
                {
                    _ = ((AsyncCommandBase)vm.OnCetakDataCommand).ExecuteAsync(null!);
                }
            }
        }

        private void TipeProses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OkButton.IsEnabled = CheckButton();
        }

        private bool CheckButton()
        {
            return TipeProses.SelectedItem != null;
        }
    }
}
