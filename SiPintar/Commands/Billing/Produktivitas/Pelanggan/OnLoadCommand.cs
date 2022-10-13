using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using LiveCharts;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Produktivitas;
using SiPintar.Views;

namespace SiPintar.Commands.Billing.Produktivitas.Pelanggan
{
    [ExcludeFromCodeCoverage]
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly PelangganViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnLoadCommand(PelangganViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoading = true;

            await GetDataListAsync();

            _viewModel.ChartVisibility = Visibility.Visible;
            _viewModel.Plot = "Kolom";
            _viewModel.JumlahPelangganSeriesVisibility = true;
            _viewModel.TerbacaSeriesVisibility = true;
            _viewModel.FotoMeterSeriesVisibility = true;
            _viewModel.FotoRumahSeriesVisibility = true;
            _viewModel.VideoSeriesVisibility = true;
            _viewModel.GpsSeriesVisibility = true;
            _viewModel.ZoomingModeX = ZoomingOptions.X;
            _viewModel.ZoomingModeY = ZoomingOptions.Y;
            _viewModel.Formatter = value => value.ToString("N0");

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
                { "Kategori", "pelanggan" },
            };

            if (_viewModel.Parent.TglBacaAwalFilter != null)
                param.Add("WaktuBacaMulai", _viewModel.Parent.TglBacaAwalFilter);
            if (_viewModel.Parent.TglBacaAkhirFilter != null)
                param.Add("WaktuBacaSampaiDengan", _viewModel.Parent.TglBacaAkhirFilter);
            if (_viewModel.Parent.TglKirimAwalFilter != null)
                param.Add("WaktuKirimHasilBacaMulai", _viewModel.Parent.TglKirimAwalFilter);
            if (_viewModel.Parent.TglKirimAkhirFilter != null)
                param.Add("WaktuKirimHasilBacaSampaiDengan", _viewModel.Parent.TglKirimAkhirFilter);
            if (_viewModel.Parent.RayonFilter != null)
                param.Add("IdRayon", _viewModel.Parent.RayonFilter.IdRayon);
            if (_viewModel.Parent.WilayahFilter != null)
                param.Add("IdWilayah", _viewModel.Parent.WilayahFilter.IdWilayah);
            if (_viewModel.Parent.KelurahanFilter != null)
                param.Add("IdKelurahan", _viewModel.Parent.KelurahanFilter.IdKelurahan);
            if (_viewModel.Parent.PembacaMeterFilter != null)
                param.Add("IdPetugasBaca", _viewModel.Parent.PembacaMeterFilter.IdPetugasBaca);

            if (_isTest)
                param.Add("TestId", _viewModel.TestId);

            var response = await Task.Run(() => _restApi.GetAsync($"/api/{ApiVersion}/summary-produktifitas-bacameter", param));

            _viewModel.ProduktivitasPelanggan = new ObservableCollection<SummaryProduktifitasBacameterPelangganDto>();

            if (!response.IsError)
            {
                var result = response.Data;

                if (result.Status && result.Data != null)
                    _viewModel.ProduktivitasPelanggan = result.Data.ToObject<ObservableCollection<SummaryProduktifitasBacameterPelangganDto>>();
                else
                    ErrorMessage = response.Data.Ui_msg;
            }
            else
            {
                ErrorMessage = response.Error.Message;
            }

            ShowSnackbar(ErrorMessage);

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private void ShowSnackbar(string errorMessage)
        {
            if (errorMessage != null && !_isTest)
                Application.Current.Dispatcher.Invoke(delegate
                {
                    var mainView = (BillingView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                    if (mainView != null)
                        mainView.ShowSnackbar(errorMessage, "danger");
                });
        }

    }
}
