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
using SiPintar.ViewModels.Akuntansi.Jurnal.Kas;

namespace SiPintar.Views.Akuntansi.Jurnal.Kas
{
    /// <summary>
    /// Interaction logic for PembayaranKasView.xaml
    /// </summary>
    public partial class PembayaranKasView : UserControl
    {
        public PembayaranKasView()
        {
            InitializeComponent();
        }

        private void PeriodeAwal_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PeriodeAwal.SelectedDate != null)
            {
                DateTime selectedDate = PeriodeAwal.SelectedDate ?? DateTime.Now;
                var lastDay = DateTime.DaysInMonth(selectedDate.Year, selectedDate.Month);
                PeriodeAkhir.SelectedDate = new DateTime(selectedDate.Year, selectedDate.Month, lastDay);
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is PembayaranKasViewModel viewModel && viewModel.PeriodeAwal != null && viewModel.PeriodeAkhir != null)
            {
                if (PeriodeAwal.SelectedDate!.Value.Year == PeriodeAkhir.SelectedDate!.Value.Year && PeriodeAwal.SelectedDate!.Value.Month == PeriodeAkhir.SelectedDate!.Value.Month)
                {
                    _ = ((AsyncCommandBase)viewModel.OnCetakDataCommand).ExecuteAsync((bool)RadioDetail.IsChecked! ? "detail" : "rekap");
                }
                else
                {
                    _ = DialogHelper.ShowDialogGlobalViewAsync(
                    false,
                    false,
                    "AkuntansiRootDialog",
                    $"Jurnal Pembayaran Kas (JBK)",
                    $"Periode posting harus dalam bulan yang sama!",
                    "warning",
                    "Ok",
                    false,
                    "Akuntansi");
                }
            }
            else
            {
                _ = DialogHelper.ShowDialogGlobalViewAsync(
                false,
                false,
                "AkuntansiRootDialog",
                $"Jurnal Pembayaran Kas (JBK)",
                $"Periode posting belum dipilih !",
                "warning",
                "Ok",
                false,
                "Akuntansi");
            }
        }
    }
}
