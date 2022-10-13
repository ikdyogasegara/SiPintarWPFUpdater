using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.Tunjangan;
using SiPintar.Views.Personalia.Tunjangan.TunjanganBulanan;

namespace SiPintar.Commands.Personalia.Tunjangan.TunjanganBulanan
{
    public class OnOpenSelectPeriodeCommand : AsyncCommandBase
    {
        private readonly TunjanganBulananViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnOpenSelectPeriodeCommand(TunjanganBulananViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var filterPeriode = _viewModel.FilterPeriode;
            var filterKodeGaji = _viewModel.FilterKodeGaji;

            await GetPeriodeAsync();
            await GetKodeGajiAsync();

            _viewModel.FilterPeriode = filterPeriode;
            _viewModel.FilterKodeGaji = filterKodeGaji;
            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "PersonaliaRootDialog", () => new DialogPeriodeView(_viewModel));

            await Task.FromResult(_isTest);
        }

        public async Task GetPeriodeAsync()
        {
            _viewModel.IsLoading = true;
            var param = new Dictionary<string, dynamic> { { "Status", true } };
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-periode-rekening", param);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.PeriodeList = Result.Data.ToObject<ObservableCollection<MasterPeriodeDto>>();
                }
            }
            _viewModel.IsLoading = false;
        }

        public async Task GetKodeGajiAsync()
        {
            _viewModel.IsLoading = true;
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-kode-gaji");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.KodeGajiList = Result.Data.ToObject<ObservableCollection<MasterKodeGajiDto>>();
                }
            }
            _viewModel.IsLoading = false;
        }
    }
}
