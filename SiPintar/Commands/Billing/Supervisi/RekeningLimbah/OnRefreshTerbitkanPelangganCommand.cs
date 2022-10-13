using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Commands.Billing.Supervisi.RekeningLimbah
{
    public class OnRefreshTerbitkanPelangganCommand : AsyncCommandBase
    {
        private readonly RekeningLimbahViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnRefreshTerbitkanPelangganCommand(RekeningLimbahViewModel viewModel, IRestApiClientModel restApi)
        {
            _viewModel = viewModel;
            _restApi = restApi;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoadingForm = true;

            var param = new Dictionary<string, dynamic>
            {
                { "IdPeriode", _viewModel.PeriodeFilter.IdPeriode }
            };

            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/rekening-limbah-daftar-penerbitan-pelanggan", param);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.TerbitkanPelangganList =
                        Result.Data.ToObject<ObservableCollection<MasterPelangganLimbahWpf>>();
                }
            }

            _viewModel.IsEmptyDialog = !_viewModel.TerbitkanPelangganList.Any();
            _viewModel.IsSelectAllTerbitkanPelanggan = _viewModel.TerbitkanPelangganList.Any();

            _viewModel.IsLoadingForm = false;
        }
    }
}
