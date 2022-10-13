using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Billing.Supervisi.PelangganL2T2;

namespace SiPintar.Commands.Billing.Supervisi.PelangganL2T2
{
    public class OnOpenPerbaruiDataRekeningCommand : AsyncCommandBase
    {
        private readonly PelangganL2T2ViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnOpenPerbaruiDataRekeningCommand(PelangganL2T2ViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _ = _restApi;
            if (_viewModel.IsLoading)
                return;

            _viewModel.IsLoadingForm = true;

            OpenDialog();

            _ = GetPeriodeAsync();

            _viewModel.IsLoadingForm = false;

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        public void OpenDialog()
        {
            if (!_isTest) _ = DialogHost.Show(new PerbaruiDataRekeningView(_viewModel), "BillingRootDialog");
        }

        public async Task GetPeriodeAsync()
        {
            var param = new Dictionary<string, dynamic>
            {
                { "status", true }
            };

            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/master-periode-rekening", param);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.ListPeriodePerbaruiData = Result.Data.ToObject<ObservableCollection<MasterPeriodeDto>>();
                }
            }
        }
    }
}
