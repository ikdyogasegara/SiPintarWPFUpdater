using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Billing.Supervisi.RekeningLimbah;

namespace SiPintar.Commands.Billing.Supervisi.RekeningLimbah
{
    public class OnOpenTerbitkanPelangganCommand : AsyncCommandBase
    {
        private readonly RekeningLimbahViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnOpenTerbitkanPelangganCommand(RekeningLimbahViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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

            ShowDialog();

            await GetPelangganLimbahAsync();

            _viewModel.IsLoadingForm = false;
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest)
                _ = DialogHost.Show(new TerbitkanPelangganView(_viewModel), "BillingRootDialog");
        }

        private async Task GetPelangganLimbahAsync()
        {
            var param = new Dictionary<string, dynamic>
            {
                { "IdPeriode", _viewModel.PeriodeFilter.IdPeriode }
            };

            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/rekening-limbah-daftar-penerbitan-pelanggan", param);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.TerbitkanPelangganList =
                        Result.Data.ToObject<ObservableCollection<MasterPelangganLimbahWpf>>();
                }
            }

            _viewModel.IsEmptyDialog = !_viewModel.TerbitkanPelangganList.Any();
            _viewModel.IsSelectAllTerbitkanPelanggan = _viewModel.TerbitkanPelangganList.Any();
        }
    }
}
