using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Bacameter.SistemKontrol;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Bacameter.SistemKontrol.DataPembacaan
{
    public class OnOpenChangeStatusConfirmCommand : AsyncCommandBase
    {
        private readonly DataPembacaanViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenChangeStatusConfirmCommand(DataPembacaanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.SelectedData == null)
                return;

            var section = parameter as string;
            _viewModel.StatusSection = section;

            var SelectedDate = _viewModel.SelectedData.NamaPeriode;

            ShowDialog(SelectedDate, section);

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog(string SelectedDate, string section)
        {
            var HeaderInfo = section == "open" ? "Aktifkan Periode" : "Tutup Periode";
            var SectionInfo = section == "open" ? "mengaktifkan kembali" : "menutup";

            if (!_isTest)
                _ = DialogHost.Show(new DialogGlobalYesCancelView(
                    HeaderInfo,
                    $@"Apakah Anda yakin ingin {SectionInfo} data pembacaan periode {SelectedDate}?",
                    "warning",
                    _viewModel.OnSubmitChangeStatusCommand,
                    "Proses",
                    "Batal",
                    false,
                    true,
                    "bacameter"
                ), "BacameterRootDialog");
        }
    }
}
