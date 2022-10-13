using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.ViewModels.Personalia.DataMaster;

namespace SiPintar.Commands.Personalia.DataMaster.Pegawai
{
    [ExcludeFromCodeCoverage]
    public class OnDeleteFotoCommand : AsyncCommandBase
    {
        private readonly PegawaiViewModel _viewModel;
        private readonly bool _isTest;

        public OnDeleteFotoCommand(PegawaiViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter == null)
                return;

            var param = parameter as string;

            if (param == "FotoPegawai")
                _viewModel.FotoPegawaiUri = null;
            else if (param == "FotoKtp")
                _viewModel.FotoKtpUri = null;
            else if (param == "FotoKk")
                _viewModel.FotoKkUri = null;

            await Task.FromResult(_isTest);
        }
    }
}
