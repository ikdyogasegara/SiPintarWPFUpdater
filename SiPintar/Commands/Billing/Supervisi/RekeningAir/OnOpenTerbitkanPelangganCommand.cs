using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Billing.Supervisi.RekeningAir;

namespace SiPintar.Commands.Billing.Supervisi.RekeningAir
{
    public class OnOpenTerbitkanPelangganCommand : AsyncCommandBase
    {

        private readonly RekeningAirViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnOpenTerbitkanPelangganCommand(RekeningAirViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            #region akses
            if (_viewModel.AksesTerbitkan == false)
            {
                DialogHelper.ShowInvalidAkses(_isTest);
                return;
            }
            #endregion

            if (_viewModel.SelectedDataPeriode?.IdPeriode == null) return;

            _viewModel.IsLoadingPenerbitan = true;
            _viewModel.PenerbitanPelangganList = new ObservableCollection<RekeningAirWpf>();

            if (!_isTest) _ = DialogHost.Show(new TerbitkanFormView(_viewModel), "BillingRootDialog");

            await GetPelangganAirAsync();
            _viewModel.IsLoadingPenerbitan = false;
        }

        [ExcludeFromCodeCoverage]
        private async Task GetPelangganAirAsync()
        {
            var param = new Dictionary<string, dynamic>
            {
                {"IdPeriode" , _viewModel.SelectedDataPeriode.IdPeriode},
                {"PageSize" , 10000},
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
        }

    }
}
