using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using NPOI.SS.Formula.Functions;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels;
using SiPintar.ViewModels.Main;

namespace SiPintar.Commands.Main
{
    [ExcludeFromCodeCoverage]
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly IRestApiClientModel _restApi;
        private readonly MainViewModel _viewModel;
        private readonly bool _isTest;

        public OnLoadCommand(MainViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _restApi = restApi;
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var getData = new CurrentUserData(_restApi);

            await getData.GetResponseMessageDataAsync();

            if (!_isTest)
            {
                MapConfigHelper.GmapApiKey = MasterListGlobal.Pengaturan.FirstOrDefault(i => i.Key == "gmap_api_key")?.Value ?? "AIzaSyDRvGO56pjOOkekfbAylTaT1Hsf6juZuls";
                MapConfigHelper.DefaultLatitude = MasterListGlobal.Pengaturan.FirstOrDefault(i => i.Key == "default_latitude")?.Value ?? "-8.650512771672066";
                MapConfigHelper.DefaultLongitude = MasterListGlobal.Pengaturan.FirstOrDefault(i => i.Key == "default_longitude")?.Value ?? "115.22227112485419";
            }

            _viewModel.UpdateState();

            await Task.FromResult(_isTest);
        }
    }
}
