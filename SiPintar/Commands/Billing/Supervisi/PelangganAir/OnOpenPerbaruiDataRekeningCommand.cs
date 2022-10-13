using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Billing.Supervisi.PelangganAir;

namespace SiPintar.Commands.Billing.Supervisi.PelangganAir
{
    public class OnOpenPerbaruiDataRekeningCommand : AsyncCommandBase
    {
        private readonly PelangganAirViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnOpenPerbaruiDataRekeningCommand(PelangganAirViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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

            _viewModel.ListYearPerbaruiData = new List<int>();

            _viewModel.IsLoadingForm = true;

            OpenDialog();

            int minYear = DateTime.Now.Year - 10;
            _viewModel.ListYearPerbaruiData = new List<int>(Enumerable.Range(minYear, DateTime.Now.Year - minYear + 1));

            _viewModel.IsLoadingForm = false;

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        public void OpenDialog()
        {
            if (!_isTest) _ = DialogHost.Show(new PerbaruiDataRekeningView(_viewModel), "BillingRootDialog");
        }
    }
}
