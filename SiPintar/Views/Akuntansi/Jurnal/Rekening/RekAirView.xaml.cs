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
    /// Interaction logic for RekAirView.xaml
    /// </summary>
    public partial class RekAirView : UserControl
    {
        public RekAirView()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is RekAirViewModel viewModel && viewModel.SelectedPeriode?.IdPeriode != null)
            {
                _ = ((AsyncCommandBase)viewModel.OnCetakDataCommand).ExecuteAsync((bool)RadioDetail.IsChecked! ? "detail" : "rekap");
            }
            else
            {
                _ = DialogHelper.ShowDialogGlobalViewAsync(
                false,
                false,
                "AkuntansiRootDialog",
                $"Jurnal Rekening Air (JRA)",
                $"Periode posting belum dipilih !",
                "warning",
                "Ok",
                false,
                "Akuntansi");
            }
        }

    }
}
