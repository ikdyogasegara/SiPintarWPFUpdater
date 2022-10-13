using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Billing.Supervisi.Periode;

namespace SiPintar.Commands.Billing.Supervisi.Periode
{
    public class OnOpenLimitChartFormCommand : AsyncCommandBase
    {
        private readonly PeriodeViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnOpenLimitChartFormCommand(PeriodeViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading)
                return;

            ShowDialog();

            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-periode-rekening-chart-limit"));
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status && Result.Data.Any())
                {
                    var data = Result.Data.ToObject<ObservableCollection<PengaturanDto>>();
                    _viewModel.LimitChart = data[0].Value;
                }
            }
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest)
                _ = DialogHost.Show(new LimitChartFormView(_viewModel), "BillingRootDialog");
        }
    }
}
