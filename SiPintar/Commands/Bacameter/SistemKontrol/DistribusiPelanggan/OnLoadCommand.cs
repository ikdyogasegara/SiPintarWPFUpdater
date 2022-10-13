using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Utilities;
using SiPintar.ViewModels.Bacameter.SistemKontrol;

namespace SiPintar.Commands.Bacameter.SistemKontrol.DistribusiPelanggan
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly DistribusiPelangganViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnLoadCommand(DistribusiPelangganViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            await GetPeriodeAsync();
            _viewModel.SelectedJenis = _viewModel.JenisList[0];

            GetKecamatan();

            await Task.FromResult(_isTest);
        }

        public async Task GetPeriodeAsync()
        {
            var param = new Dictionary<string, dynamic>
            {
                { "status", true }
            };

            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/master-periode-rekening", param);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.PeriodeList = Result.Data.ToObject<ObservableCollection<MasterPeriodeDto>>();

                    if (_viewModel.PeriodeList != null && _viewModel.PeriodeList.Count > 0)
                        _viewModel.SelectedPeriode = _viewModel.PeriodeList[0];
                }
            }
        }

        [ExcludeFromCodeCoverage]
        private void GetKecamatan()
        {
            if (!_isTest)
                _ = ((AsyncCommandBase)_viewModel.GetKecamatanCommand).ExecuteAsync(null);
        }
    }
}
