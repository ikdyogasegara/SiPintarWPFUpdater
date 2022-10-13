using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Billing.Supervisi.PelangganLimbah;

namespace SiPintar.Commands.Billing.Supervisi.PelangganLimbah
{
    public class OnOpenPerbaruiDataRekeningCommand : AsyncCommandBase
    {
        private readonly PelangganLimbahViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnOpenPerbaruiDataRekeningCommand(PelangganLimbahViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading)
                return;

            _viewModel.IsLoadingForm = true;

            OpenDialog();

            await GetPeriodeAsync();

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
                    _viewModel.PeriodeList = Result.Data.ToObject<ObservableCollection<MasterPeriodeDto>>();
                }
            }
        }
    }
}
