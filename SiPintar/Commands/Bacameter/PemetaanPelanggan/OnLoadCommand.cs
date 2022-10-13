using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Bacameter;

namespace SiPintar.Commands.Bacameter.PemetaanPelanggan
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly PemetaanPelangganViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources?["api_version"]?.ToString();

        public OnLoadCommand(PemetaanPelangganViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoading = false;
            _viewModel.MarkerList = new List<MapMarkerObject>();

            _ = GetPeriodeListAsync();
            _ = GetRayonListAsync();

            await Task.FromResult(_isTest);
        }

        public async Task GetPeriodeListAsync()
        {
            var param = new Dictionary<string, dynamic>
            {
                { "Status", true }
            };

            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/master-periode-rekening", param);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status && Result.Data != null)
                {
                    _viewModel.PeriodeList = Result.Data.ToObject<ObservableCollection<MasterPeriodeDto>>();

                    if (_viewModel.PeriodeList.Count > 0)
                        _viewModel.SelectedPeriode = _viewModel.PeriodeList[0];
                }
            }
        }

        public async Task GetRayonListAsync()
        {
            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/master-rayon");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status && Result.Data != null)
                {
                    _viewModel.RayonList = Result.Data.ToObject<ObservableCollection<MasterRayonDto>>();
                }
            }
        }
    }
}
