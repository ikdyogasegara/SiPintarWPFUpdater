using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using AppBusiness.Data.DTOs;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using SiPintar.Utilities;
using SiPintar.ViewModels.Bacameter.SistemKontrol.DistribusiPelanggan;
using SiPintar.Views;

namespace SiPintar.Commands.Bacameter.SistemKontrol.DistribusiPelanggan.PerKecamatan
{
    [ExcludeFromCodeCoverage]
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly PerKecamatanViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnLoadCommand(PerKecamatanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoading = true;

            await GetDataListAsync();

            _viewModel.Plot = "Kolom";
            _viewModel.JumlahDistribusiPelangganSeriesVisibility = true;
            _viewModel.ZoomingModeX = ZoomingOptions.X;
            _viewModel.ZoomingModeY = ZoomingOptions.Y;
            _viewModel.Formatter = value => value.ToString("N0");
            _viewModel.SeriesCollection = new ChartValues<ObservableValue> { };
            _viewModel.MapperColumn = Mappers.Xy<ObservableValue>()
                .X((item, index) => index)
                .Y(item => item.Value)
                .Fill((item, index) => new SolidColorBrush(GetColor(index)))
                .Stroke((item, index) => new SolidColorBrush(GetColor(index)));
            _viewModel.MapperRow = Mappers.Xy<ObservableValue>()
                .Y((item, index) => index)
                .X(item => item.Value)
                .Fill((item, index) => new SolidColorBrush(GetColor(index)))
                .Stroke((item, index) => new SolidColorBrush(GetColor(index)));
            _viewModel.MapperLine = Mappers.Xy<ObservableValue>()
                .X((item, index) => index)
                .Y(item => item.Value)
                .Fill((item, index) => new SolidColorBrush(GetColor(index)))
                .Stroke((item, index) => new SolidColorBrush(GetColor(index)));

            _viewModel.LoadSeries();

            _viewModel.IsLoading = false;

            await Task.FromResult(_isTest);
        }

        private async Task GetDataListAsync()
        {
            string ErrorMessage = null;

            var param = new Dictionary<string, dynamic>
            {
                { "PageSize", 1000 },
                { "CurrentPage", 1 },
                { "IdPeriode", _viewModel.Parent.SelectedPeriode?.IdPeriode },
                { "Kategori", "per kecamatan" }
            };

            if (_isTest)
                param.Add("TestId", _viewModel.TestId);

            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{ApiVersion}/summary-distribusi-pelanggan", param));

            _viewModel.KecamatanList = new ObservableCollection<SummaryDistribusiPelangganDto>();

            if (!Response.IsError)
            {
                var Result = Response.Data;

                if (Result.Status && Result.Data != null)
                    _viewModel.KecamatanList = Result.Data.ToObject<ObservableCollection<SummaryDistribusiPelangganDto>>();
                else
                    ErrorMessage = Response.Data.Ui_msg;
            }
            else
            {
                ErrorMessage = Response.Error.Message;
            }

            _viewModel.IsEmpty = _viewModel.KecamatanList.Count == 0;

            ShowSnackbar(ErrorMessage);

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private Color GetColor(int index)
        {
            int newindex = index <= 9 ? index : (index % 10);

            return _viewModel.ColorList[newindex];
        }

        [ExcludeFromCodeCoverage]
        private void ShowSnackbar(string ErrorMessage)
        {
            if (ErrorMessage != null && !_isTest)
                Application.Current.Dispatcher.Invoke(delegate
                {
                    var mainView = (BacameterView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                    if (mainView != null)
                        mainView.ShowSnackbar(ErrorMessage, "danger");
                });
        }
    }
}
