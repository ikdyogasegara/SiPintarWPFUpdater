using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Atribut;

namespace SiPintar.Commands.Billing.Atribut.RuteBacaMeter
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly RuteBacaMeterViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(RuteBacaMeterViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _ = GetPeriodeAsync();

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private async Task GetPeriodeAsync()
        {
            var param = new Dictionary<string, dynamic>
            {
                { "status", true }
            };

            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-periode-rekening", param);
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    _viewModel.PeriodeList = result.Data.ToObject<ObservableCollection<MasterPeriodeDto>>();
                }
            }
        }
    }
}
