using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataKeuangan;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataKeuangan.SaldoAwal
{
    public class OnSubmitEditFormCommand : AsyncCommandBase
    {
        private readonly SaldoAwalViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitEditFormCommand(SaldoAwalViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            DialogHelper.CloseDialog(_isTest, false, "KoreksiSaldoAwalDialog");
            _viewModel.IsLoadingForm = true;

            _viewModel.IsAdd = _viewModel.SaldoAwalForm!.SaldoAwalRekon.Any(x => x.IdSaldoKasBank == null && x.TanggalSaldo == null);

            _viewModel.SaldoAwalForm.SaldoAwalRekon.ForEach(i =>
            {
                i.JumlahSaldoOri = i.JumlahSaldo;
                i.TanggalSaldo = _viewModel.FilterTglRekonsiliasi;
            });

            _viewModel.ParamSaldoAwalForm = _viewModel.SaldoAwalForm.ToParamSaldoAwalRekonBankDto();

            var type = typeof(ParamSaldoAwalRekonBankDto);
            var properties = type.GetProperties();
            var body = new Dictionary<string, dynamic>();
            foreach (var property in properties)
            {
                if (property.Name != "IdPdam" && property.Name != "IdUserRequest")
                {
                    body.Add(property.Name, property.GetValue(_viewModel.ParamSaldoAwalForm)!);
                }
            }

            RestApiResponse response;
            if (_viewModel.IsAdd)
                response = await _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/master-saldo-awal-rekon-bank", body);
            else
                response = await _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/master-saldo-awal-rekon-bank", null!, body);

            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    DialogHelper.ShowSnackbar(_isTest, "success", result.Ui_msg!, "akuntansi");
                    _viewModel.OnRefreshCommand.Execute(null);
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", result.Ui_msg!, "akuntansi");
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message!, "akuntansi");

            _viewModel.IsLoadingForm = false;

            DialogHelper.CloseDialog(_isTest, false, "AkuntansiRootDialog");

            await Task.FromResult(_isTest);
        }
    }
}
