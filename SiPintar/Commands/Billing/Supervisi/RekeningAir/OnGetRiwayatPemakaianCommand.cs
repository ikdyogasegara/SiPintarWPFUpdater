using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Commands.Billing.Supervisi.RekeningAir
{
    public class OnGetRiwayatPemakaianCommand : AsyncCommandBase
    {
        private readonly RekeningAirViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnGetRiwayatPemakaianCommand(RekeningAirViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.YearPemakaian.HasValue)
            {
                if (_viewModel.IsLoading)
                    return;

                _viewModel.IsEmptyDialog = false;
                _viewModel.PemakaianList?.Clear();
                _viewModel.IsLoadingForm = true;

                var param = new Dictionary<string, dynamic>
                {
                    { "IdPelangganAir" , _viewModel.SelectedData.IdPelangganAir },
                    { "TahunPeriode" , _viewModel.YearPemakaian },
                };

                var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/rekening-air-riwayat-pakai", param);
                if (!response.IsError)
                {
                    var result = response.Data;
                    if (result.Status)
                    {
                        _viewModel.PemakaianList = result.Data.ToObject<ObservableCollection<RiwayatPemakaianAirDto>>();
                    }
                    else
                    {
                        DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg, moduleName: "billing");
                    }
                }
                else
                {
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message, moduleName: "billing");
                }

                _viewModel.IsEmptyDialog = !_viewModel.PemakaianList.Any();
                _viewModel.IsLoadingForm = false;
            }

            await Task.FromResult(_isTest);
        }
    }
}
