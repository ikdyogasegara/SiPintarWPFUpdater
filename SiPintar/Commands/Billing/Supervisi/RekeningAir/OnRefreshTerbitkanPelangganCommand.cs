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
    public class OnRefreshTerbitkanPelangganCommand : AsyncCommandBase
    {
        private readonly RekeningAirViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnRefreshTerbitkanPelangganCommand(RekeningAirViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoadingPenerbitan = true;
            _viewModel.PenerbitanPelangganList = new ObservableCollection<RekeningAirWpf>();

            var param = new Dictionary<string, dynamic>
            {
                {"IdPeriode" , _viewModel.SelectedDataPeriode.IdPeriode},
                {"PageSize" , 1000000},
            };

            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/rekening-air-daftar-penerbitan-pelanggan", param);
            if (!response.IsError)
            {
                var result = response.Data;

                if (result.Status && result.Data != null)
                    _viewModel.PenerbitanPelangganList = result.Data.ToObject<ObservableCollection<RekeningAirWpf>>();
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg, "billing");

            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message, "billing");

            if (_viewModel.PenerbitanPelangganList != null)
            {
                _viewModel.IsEmptyDialog = !_viewModel.PenerbitanPelangganList.Any();
                _viewModel.IsAllSelectedTerbitkanPelanggan = _viewModel.PenerbitanPelangganList.Any();
            }

            _viewModel.IsLoadingPenerbitan = false;
        }
    }
}
