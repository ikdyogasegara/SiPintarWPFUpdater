using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataKeuangan;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataKeuangan.SaldoAwal
{
    public class OnRefreshCommand : AsyncCommandBase
    {
        private readonly SaldoAwalViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnRefreshCommand(SaldoAwalViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoading = true;

            var param = new Dictionary<string, dynamic>
            {
                { "PageSize" , 1 },
                { "CurrentPage", 1 },
                { "Tglsaldo", _viewModel.FilterTglRekonsiliasi!.Value.Date.ToString("yyyy-MM-dd",null) },
            };


            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-saldo-awal-rekon-bank", param);
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status && result.Data != null)
                {
                    _viewModel.Data = result.Data.ToObject<List<MasterSaldoAwalRekonBankDto>>()![0];
                    if (_viewModel.Data.SaldoAwalRekon.Count == 0 || _isTest)
                    {
                        _viewModel.Data = new()
                        {
                            TotalSaldoKasBank = 0,
                            SaldoAwalRekon = new(_viewModel.BankList!.Select(x => x.ToSaldoAwalRekonDto()))
                        };
                    }
                    _viewModel.TotalSaldoAwal = _viewModel.Data.TotalSaldoKasBank + _viewModel.Data.SaldoAwalRekon.Sum(x => x.JumlahSaldo);
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg!);
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message);

            _viewModel.IsLoading = false;

            await Task.FromResult(_isTest);
        }
    }
}
