using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Personalia.Tunjangan;

namespace SiPintar.Commands.Personalia.Tunjangan.PengecualianTunjangan
{
    public class OnSelectPegawaiCommand : AsyncCommandBase
    {
        private readonly PengecualianTunjanganViewModel _viewModel;
        private readonly bool _isTest;

        public OnSelectPegawaiCommand(PengecualianTunjanganViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _ = _viewModel;
            DialogHelper.CloseDialog(_isTest, false, "PersonaliaInnerDialog");

            await Task.FromResult(_isTest);
        }
    }
}
