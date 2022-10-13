using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.Penyusutan;
using SiPintar.Views.Akuntansi.Penyusutan.DataAktiva;

namespace SiPintar.Commands.Akuntansi.Penyusutan.DataAktiva
{
    public class OnOpenPenyusutanFormCommand : AsyncCommandBase
    {
        private readonly DataAktivaViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnOpenPenyusutanFormCommand(DataAktivaViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _ = _restApi;

            _viewModel.IsLoadingForm = true;

            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "AkuntansiRootDialog", GetInstance);

            _viewModel.IsLoadingForm = false;
            await Task.FromResult(_isTest);
        }

        private object GetInstance() => new DialogPenyusutanAktivaView(_viewModel);
    }
}
