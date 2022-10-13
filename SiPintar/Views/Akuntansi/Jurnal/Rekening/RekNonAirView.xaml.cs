using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SiPintar.Commands;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.Jurnal.Rekening;

namespace SiPintar.Views.Akuntansi.Jurnal.Rekening
{
    /// <summary>
    /// Interaction logic for RekNonAirView.xaml
    /// </summary>
    public partial class RekNonAirView : UserControl
    {
        public RekNonAirView()
        {
            InitializeComponent();
        }

        private void Bulan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Tahun_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is RekNonAirViewModel viewModel && viewModel.SelectedPeriode?.IdPeriode != null)
            {
                _ = ((AsyncCommandBase)viewModel.OnCetakDataCommand).ExecuteAsync((bool)RadioRincian.IsChecked! ? "rincian" : "konsolidasi");
            }
            else
            {
                _ = DialogHelper.ShowDialogGlobalViewAsync(
                false,
                false,
                "AkuntansiRootDialog",
                $"Jurnal Rekening Non Air (JRNA)",
                $"Periode posting belum dipilih !",
                "warning",
                "Ok",
                false,
                "Akuntansi");
            }
        }
    }
}
