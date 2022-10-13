using System;
using System.Linq;
using System.Windows.Controls;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.PostingKeuangan;

namespace SiPintar.Views.Akuntansi.PostingKeuangan
{
    public partial class ProsesSaldoHarianView : UserControl
    {
        public ProsesSaldoHarianView()
        {
            InitializeComponent();

            Sisa.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            Sisa.GotFocus += DecimalValidationHelper.Object_GotFocus;
            Sisa.LostFocus += DecimalValidationHelper.Object_LostFocus;

            JmlPenerimaanHariIni.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            JmlPenerimaanHariIni.GotFocus += DecimalValidationHelper.Object_GotFocus;
            JmlPenerimaanHariIni.LostFocus += DecimalValidationHelper.Object_LostFocus;

            HitungTotal();
        }

        private void DataGridSide_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is ProsesSaldoHarianViewModel vm && vm.SelectedLoket != null)
                vm.OnRefreshCommand.Execute(null);
        }

        private void JmlSetor_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            DecimalValidationHelper.DecimalValidationTextBox(sender, e);
        }

        private void JmlSetor_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            DecimalValidationHelper.Object_GotFocus(sender, e);
        }

        private void JmlSetor_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            DecimalValidationHelper.Object_LostFocus(sender, e);
            HitungTotal();
        }

        private void HitungTotal()
        {
            if (DataContext is ProsesSaldoHarianViewModel vm && vm.ProsesSaldoHarianForm != null)
                vm.TotalPenyetoranBank = vm.ProsesSaldoHarianForm!.Detail!.Sum(x => x.JumlahSetor);
        }

        private void TanggalKasHariIni_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is ProsesSaldoHarianViewModel vm)
            {
                TglKasSebelumnya.SelectedDate = ((DateTime)TglKasHariIni.SelectedDate!).AddDays(-1);
                if (vm.SelectedLoket != null)
                    vm.OnRefreshCommand.Execute(null);
            }
        }

        private void OkButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is ProsesSaldoHarianViewModel vm)
            {
                vm.ProsesSaldoHarianForm!.TglSetor = vm.TglKasHariIni;
                vm.ProsesSaldoHarianForm.IdLoket = vm.SelectedLoket!.IdLoket;

                if (vm.ProsesSaldoHarianForm!.JumlahPenerimaan < vm.TotalPenyetoranBank)
                    _ = DialogHelper.ShowDialogGlobalYesCancelViewAsync(false, false, "AkuntansiRootDialog",
                        "Konfirmasi Proses Saldo Harian",
                        $"Jumlah Total Setoran melebihi Jumlah Total Penerimaan Hari ini" +
                        $"\nLanjutkan?"
                        , "warning", vm.OnSubmitFormCommand,
                        "Ya", "Batal", false, false, "akuntansi");
                else
                    _ = DialogHelper.ShowDialogGlobalYesCancelViewAsync(false, false, "AkuntansiRootDialog",
                        "Konfirmasi Proses Saldo Harian",
                        $"Jumlah Total Setoran ke Bank Hari ini" +
                        $"\nSebesar : Rp {DecimalValidationHelper.FormatDecimalToStringId(vm.TotalPenyetoranBank)}" +
                        $"\nYakin untuk dilanjutkan?"
                        , "warning", vm.OnSubmitFormCommand,
                        "Ya", "Batal", false, false, "akuntansi");
            }
        }
    }
}

