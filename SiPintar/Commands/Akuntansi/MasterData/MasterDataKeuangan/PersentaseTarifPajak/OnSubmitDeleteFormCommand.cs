using System.Collections.Generic;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataKeuangan;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataKeuangan.PersentaseTarifPajak
{
    public class OnSubmitDeleteFormCommand : AsyncCommandBase
    {
        private readonly PersentaseTarifPajakViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitDeleteFormCommand(PersentaseTarifPajakViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            DialogHelper.CloseDialog(_isTest, false, "AkuntansiRootDialog");
            object dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, false,
                "AkuntansiRootDialog", "Mohon tunggu",
                "sedang memproses tindakan...",
                null, true, true, 20);

            _ = _restApi;

            //Start - Submit Delete
            DialogHelper.CloseDialog(_isTest, false, "AkuntansiRootDialog", dg);

            //End - Get Filter Data
            await ((AsyncCommandBase)_viewModel.OnRefreshCommand).ExecuteAsync(null);
            await Task.FromResult(_isTest);
        }
    }
}
