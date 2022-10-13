using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Bacameter.SistemKontrol.WilayahAdministrasi;
using SiPintar.Views;

namespace SiPintar.Commands.Bacameter.SistemKontrol.WilayahAdministrasi.Kecamatan
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly KecamatanViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;
        private new string ErrorMessage;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnLoadCommand(KecamatanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var section = parameter as string;

            ErrorMessage = null;

            switch (section)
            {
                case "cabang":
                    await GetCabangAsync();
                    break;
                case "kecamatan":
                    await GetKecamatanAsync(_viewModel.SelectedCabang?.IdCabang);
                    break;
                case "kelurahan":
                    await GetKelurahanAsync(_viewModel.SelectedKecamatan?.IdKecamatan);
                    break;
                default:
                    await GetCabangAsync();
                    break;
            }

            ShowResult();
        }

        private async Task GetCabangAsync()
        {
            _viewModel.IsEmptyCabang = false;

            _viewModel.IsLoadingCabang = true;

            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{ApiVersion}/master-cabang"));

            _viewModel.CabangList = new ObservableCollection<MasterCabangDto>();

            if (!Response.IsError)
            {
                var Result = Response.Data;

                if (Result.Status && Result.Data != null)
                    _viewModel.CabangList = Result.Data.ToObject<ObservableCollection<MasterCabangDto>>();
                else
                    ErrorMessage = Response.Data.Ui_msg;
            }
            else
            {
                ErrorMessage = Response.Error.Message;
            }

            if (_viewModel.CabangList.Count == 0)
                _viewModel.IsEmptyCabang = true;

            _viewModel.IsLoadingCabang = false;
        }

        private async Task GetKecamatanAsync(int? IdCabang)
        {
            if (IdCabang == null)
            {
                _viewModel.IsEmptyKecamatan = true;
                _viewModel.IsLoadingKecamatan = false;
                return;
            }

            _viewModel.IsEmptyKecamatan = false;

            _viewModel.IsLoadingKecamatan = true;

            var param = new Dictionary<string, dynamic>
            {
                { "IdCabang", IdCabang }
            };

            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{ApiVersion}/master-kecamatan", param));

            _viewModel.KecamatanList = new ObservableCollection<MasterKecamatanDto>();

            if (!Response.IsError)
            {
                var Result = Response.Data;

                if (Result.Status && Result.Data != null)
                    _viewModel.KecamatanList = Result.Data.ToObject<ObservableCollection<MasterKecamatanDto>>();
                else
                    ErrorMessage = Response.Data.Ui_msg;
            }
            else
            {
                ErrorMessage = Response.Error.Message;
            }

            if (_viewModel.KecamatanList.Count == 0)
                _viewModel.IsEmptyKecamatan = true;

            _viewModel.IsLoadingKecamatan = false;
        }

        private async Task GetKelurahanAsync(int? IdKecamatan)
        {
            if (IdKecamatan == null)
            {
                _viewModel.IsEmptyKelurahan = true;
                _viewModel.IsLoadingKelurahan = false;
                return;
            }

            _viewModel.IsEmptyKelurahan = false;

            _viewModel.IsLoadingKelurahan = true;

            var param = new Dictionary<string, dynamic>
            {
                { "IdKecamatan", IdKecamatan }
            };

            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{ApiVersion}/master-kelurahan", param));

            _viewModel.KelurahanList = new ObservableCollection<MasterKelurahanDto>();

            if (!Response.IsError)
            {
                var Result = Response.Data;

                if (Result.Status && Result.Data != null)
                    _viewModel.KelurahanList = Result.Data.ToObject<ObservableCollection<MasterKelurahanDto>>();
                else
                    ErrorMessage = Response.Data.Ui_msg;
            }
            else
            {
                ErrorMessage = Response.Error.Message;
            }

            if (_viewModel.KelurahanList.Count == 0)
                _viewModel.IsEmptyKelurahan = true;

            _viewModel.IsLoadingKelurahan = false;
        }

        private void ShowResult()
        {
            if (ErrorMessage != null)
            {
                AppDispatcherHelper.Run(_isTest, () =>
                {
                    var mainView = (BacameterView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                    if (mainView != null)
                        mainView.ShowSnackbar(ErrorMessage, "danger");
                });
            }
        }
    }
}
