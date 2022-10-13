using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using Newtonsoft.Json.Linq;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.PostingKeuangan;

namespace SiPintar.Commands.Akuntansi.PostingKeuangan.ProsesSaldoHarian
{
    public class OnRefreshCommand : AsyncCommandBase
    {
        private readonly ProsesSaldoHarianViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnRefreshCommand(ProsesSaldoHarianViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedLoket == null) return;

            _viewModel.IsLoading = true;

            var param = new Dictionary<string, dynamic>
            {
                { "PageSize" , 1 },
                { "CurrentPage", 1 },
                { "TglSetor", ((DateTime)_viewModel.TglKasHariIni!).ToString("yyyy-MM-dd",null) },
                { "IdLoket", _viewModel.SelectedLoket!.IdLoket! }
            };

            _viewModel.BankList ??= new();

            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/saldo-setor", param);
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    result.Data ??= new JArray();
                    _viewModel.DataList = result.Data.ToObject<ObservableCollection<SaldoSetorDto>>();
                    _viewModel.ProsesSaldoHarianForm = _viewModel.DataList!.FirstOrDefault().ToSaldoSetorWpf(_viewModel.BankList.ToList());
                    _viewModel.TotalPenyetoranBank = _viewModel.ProsesSaldoHarianForm.Detail!.Sum(x => x.JumlahSetor);
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg!);
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message);

            _viewModel.IsEmpty = false;
            _viewModel.IsLoading = false;

            await Task.FromResult(_isTest);
        }
    }
}
