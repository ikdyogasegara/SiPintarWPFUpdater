using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Commands.Hublang.Pelayanan.BAPengembalian
{
    public class OnHitungCommand : AsyncCommandBase
    {
        private readonly BaPengembalianViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnHitungCommand(BaPengembalianViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var param = parameter as Dictionary<string, dynamic>;
            decimal TotalPakai = param["Pakai"];
            var ParamSend = new Dictionary<string, dynamic>();
            ParamSend.Add("Simulasi", true);
            ParamSend.Add("Pakai", TotalPakai);
            ParamSend.Add("IdGolongan", _viewModel.SelectedPelanggan.IdGolongan);
            ParamSend.Add("IdDiameter", _viewModel.SelectedPelanggan.IdDiameter);
            RestApiResponse Response = await Task.Run(() => _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/rekening-air-kalkulasi", ParamSend));
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status && Result.Data != null)
                {
                    var temp = Result.Data.ToObject<ObservableCollection<ResultKalkulasiRekeningAirDto>>();
                    if (temp.Count > 0)
                    {
                        _viewModel.KalkulasiRekening = temp[0];
                    }
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", Result.Ui_msg);
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error?.Message);
        }
    }
}
