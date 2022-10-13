using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Personalia.Kepegawaian;

namespace SiPintar.Commands.Personalia.Kepegawaian.Pensiun
{
    public class OnOpenPostingFormCommand : AsyncCommandBase
    {
        private readonly PensiunViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenPostingFormCommand(PensiunViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            await DialogHelper.ShowDialogGlobalYesCancelViewAsync(_isTest, false,
                "PersonaliaRootDialog",
                $"Konfirmasi Menghitung Ulang Jadwal Pensiun Pegawai ? ",
                null,
                "confirmation", _viewModel.OnSubmitPostingFormCommand, "Posting", "Batal", false, false, "Personalia");

            await Task.FromResult(_isTest);
        }
    }
}
