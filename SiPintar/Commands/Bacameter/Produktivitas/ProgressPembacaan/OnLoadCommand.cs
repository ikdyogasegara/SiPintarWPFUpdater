using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Utilities;
using SiPintar.ViewModels.Bacameter.Produktivitas;
using SiPintar.Views;

namespace SiPintar.Commands.Bacameter.Produktivitas.ProgressPembacaan
{
    [ExcludeFromCodeCoverage]
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly ProgressPembacaanViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnLoadCommand(ProgressPembacaanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoading = true;

            await GetDataListAsync();

            _viewModel.IsPreviousButtonEnabled = _viewModel.CurrentPage > 1;
            _viewModel.IsNextButtonEnabled = _viewModel.CurrentPage < _viewModel.TotalPage;
            _viewModel.IsLeftPageNumberActive = _viewModel.CurrentPage == 1;
            _viewModel.IsRightPageNumberActive = _viewModel.CurrentPage == _viewModel.TotalPage;
            _viewModel.IsLeftPageNumberFillerVisible = _viewModel.CurrentPage != 2;
            _viewModel.IsRightPageNumberFillerVisible = _viewModel.CurrentPage != _viewModel.TotalPage - 1;
            _viewModel.IsMiddlePageNumberVisible = _viewModel.CurrentPage > 1 && _viewModel.CurrentPage < _viewModel.TotalPage;

            _viewModel.IsEmpty = !_viewModel.DataList.Any();

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
                { "Kategori", "progress pembacaan" },
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

            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{ApiVersion}/summary-produktifitas-bacameter", param));

            _viewModel.DataList = new ObservableCollection<SummaryProduktifitasBacameterProgressPembacaanDto>();

            if (!Response.IsError)
            {
                var Result = Response.Data;

                if (Result.Status && Result.Data != null)
                {
                    _viewModel.DataList = Result.Data.ToObject<ObservableCollection<SummaryProduktifitasBacameterProgressPembacaanDto>>();

                    _viewModel.TotalPage = Result.TotalPage;
                    _viewModel.TotalRecord = Result.Record;
                }
                else
                    ErrorMessage = Response.Data.Ui_msg;
            }
            else
            {
                ErrorMessage = Response.Error.Message;
            }

            ShowSnackbar(ErrorMessage);

            await Task.FromResult(true);
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
