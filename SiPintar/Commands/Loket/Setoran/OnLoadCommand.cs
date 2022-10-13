using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Loket;

namespace SiPintar.Commands.Loket.Setoran
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly SetoranViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(SetoranViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.LoketName = Application.Current.Resources["NamaLoket"]?.ToString();

            _viewModel.IsLoading = true;
            await GetPeriodeAsync();

            if (!_isTest)
                _viewModel.GetSetoranCommand.Execute(null);

            _ = GetBankAsync();

            _viewModel.IsLoading = false;

            await Task.FromResult(_isTest);
        }

        public async Task GetPeriodeAsync()
        {
            var param = new Dictionary<string, dynamic>
            {
                { "status", true }
            };

            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-periode-rekening", param);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.PeriodeList = Result.Data.ToObject<ObservableCollection<MasterPeriodeDto>>();

                    if (_viewModel.PeriodeList != null && _viewModel.PeriodeList.Count > 0)
                        _viewModel.SelectedPeriode = _viewModel.PeriodeList[0];
                }
            }

            if (_viewModel.PeriodeList.Count == 0)
            {
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, true, "LoketRootDialog", "Setoran Loket", "Tidak ada periode yang aktif!", "warning", moduleName: "loket");
            }
        }

        public async Task GetBankAsync()
        {
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-bank");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.BankList = Result.Data.ToObject<ObservableCollection<MasterBankDto>>();
                }
            }

        }
    }
}
