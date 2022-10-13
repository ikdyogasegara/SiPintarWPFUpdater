using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Commands.Hublang.Pelayanan.Permohonan
{
    public class OnHitungTarifAirTangkiCommand : AsyncCommandBase
    {
        private readonly PermohonanViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnHitungTarifAirTangkiCommand(PermohonanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var param = parameter as Dictionary<string, dynamic>;
            var response = await Task.Run(() => _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/hitung-tarif-tangki", param));
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status && result.Data != null)
                {
                    var tempData = result.Data.ToObject<ObservableCollection<ResultKalkulasiAirTangkiDto>>();
                    _viewModel.HasilHitungRumus = tempData.Count > 0 ? tempData[0] : null;
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg);
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message);

            await Task.FromResult(_isTest);
        }
    }
}
