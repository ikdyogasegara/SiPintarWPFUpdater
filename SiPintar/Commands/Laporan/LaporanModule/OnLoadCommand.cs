using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Laporan;

namespace SiPintar.Commands.Laporan.LaporanModule
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly LaporanModuleViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(LaporanModuleViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            SetModule();
            _viewModel.LoadingMsg = "Sedang mengambil daftar laporan ...";
            _viewModel.IsLoading = true;
            _viewModel.DataReportGroup = null;
            _viewModel.DataReport = null;
            ErrorMessage = null;
            _viewModel.DataReportGroup = await GetDataReportGroupAsync(_restApi, _restApi.GetApiVersion());
            if (ErrorMessage != null)
            {
                DialogHelper.ShowSnackbar(_isTest, "danger", ErrorMessage, "laporan", true);
            }

            _viewModel.DataReport = null;
            await GetReportListAsync();
            _viewModel.InvokeDataUpdate();
            _ = _isTest;
        }

        [ExcludeFromCodeCoverage]
        private async Task GetReportListAsync()
        {
            var param = new Dictionary<string, dynamic>()
            {
                { "IdModule", _viewModel.IdModule },
                { "ListOnly", true }
            };
            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-report", param, isReport: true);
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    _viewModel.DataReport = result.Data.ToObject<ObservableCollection<ReportModelDto>>();
                }
                else
                {
                    DialogHelper.ShowSnackbar(_isTest, "danger", result.Ui_msg, "laporan", true);
                }
            }
            else
            {
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message, "laporan", true);
            }
            _viewModel.DataReport ??= new ObservableCollection<ReportModelDto>();
        }


        [ExcludeFromCodeCoverage]
        private void SetModule()
        {
            if (!_isTest)
            {
                var module = "DEFAULT";
                if (!string.IsNullOrWhiteSpace(_viewModel.SelectedModule))
                {
                    module = "ApiReportModule" + _viewModel.SelectedModule[0].ToString().ToUpper() +
                             _viewModel.SelectedModule[1..].ToLower();
                }
                Application.Current.Resources["CURRENT_MODULE"] = module;
            }
        }
    }
}
