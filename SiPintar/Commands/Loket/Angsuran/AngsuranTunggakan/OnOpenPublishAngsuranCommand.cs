using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Loket.Angsuran;

namespace SiPintar.Commands.Loket.Angsuran.AngsuranTunggakan
{
    public class OnOpenPublishAngsuranCommand : AsyncCommandBase
    {
        private readonly AngsuranTunggakanViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenPublishAngsuranCommand(AngsuranTunggakanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedData?.NoAngsuran != null)
            {
                if (_viewModel.IsEnabledBa)
                {
                    await DialogHelper.ShowDialogGlobalViewAsync(
                        _isTest,
                        true,
                        identfier: "LoketRootDialog",
                        header: "Publish Angsuran",
                        message: "Berita Acara belum dibuat!",
                        moduleName: "loket",
                        type: "warning");
                }
                else
                {
                    await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                        _isTest,
                        false,
                        "LoketRootDialog",
                        $"Konfirmasi Publish Angsuran",
                        $"Publish angsuran tunggakan atas nama {_viewModel.SelectedData.Nama} sekarang ?",
                        "success",
                        _viewModel.OnSubmitPublishAngsuranCommand,
                        "Publish",
                        "Batal",
                        false,
                        false,
                        "loket");
                }
            }
            else
            {
                await DialogHelper.ShowDialogGlobalViewAsync(
                    _isTest,
                    false,
                    "LoketRootDialog",
                    "Publish Angsuran",
                    "Data belum dipilih",
                    "error",
                    "OK",
                    false,
                    "loket");
            }

            await Task.FromResult(_isTest);
        }
    }
}
