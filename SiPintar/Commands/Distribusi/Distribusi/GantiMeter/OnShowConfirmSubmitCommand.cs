using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Distribusi.Distribusi;

namespace SiPintar.Commands.Distribusi.Distribusi.GantiMeter
{
    [ExcludeFromCodeCoverage]
    public class OnShowConfirmSubmitCommand : AsyncCommandBase
    {
        private readonly GantiMeterViewModel _viewModel;
        private readonly bool _isTest;

        public OnShowConfirmSubmitCommand(GantiMeterViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            DialogHelper.CloseDialog(_isTest, true, "DistribusiRootDialog");
            await DialogHelper.ShowDialogGlobal3MessagesViewAsync(
                _isTest,
                false,
                "DistribusiRootDialog",
                $"Tambah ke Daftar Pelanggan Ganti Meter Rutin ?",
                $"Nama : {_viewModel.SelectedPelanggan.Nama}",
                $"No. Sambungan :  {_viewModel.SelectedPelanggan.NoSamb}",
                $"Alamat : {_viewModel.SelectedPelanggan.Alamat}",
                "confirmation",
                _viewModel.OnSubmitAddRotasiMeterCommand,
                "Tambah Data",
                "Kembali",
                false,
                false,
                "distribusi");
        }
    }
}
