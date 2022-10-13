using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Atribut.WilayahAdministrasi;
using SiPintar.Views;

namespace SiPintar.Commands.Billing.Atribut.WilayahAdministrasi.AreaWilayah
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly AreaWilayahViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;
        private new string ErrorMessage;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnLoadCommand(AreaWilayahViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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
                case "wilayah":
                    await GetWilayahAsync();
                    break;
                case "area":
                    await GetAreaAsync(_viewModel.SelectedWilayah?.IdWilayah);
                    break;
                case "rayon":
                    await GetRayonAsync(_viewModel.SelectedArea?.IdArea);
                    break;
                default:
                    await GetWilayahAsync();
                    break;
            }

            ShowSnackbar();
        }

        private async Task GetWilayahAsync()
        {
            _viewModel.IsEmptyWilayah = false;

            _viewModel.IsLoadingWilayah = true;

            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{ApiVersion}/master-wilayah"));

            _viewModel.WilayahList = new ObservableCollection<MasterWilayahDto>();

            if (!Response.IsError)
            {
                var Result = Response.Data;

                if (Result.Status && Result.Data != null)
                    _viewModel.WilayahList = Result.Data.ToObject<ObservableCollection<MasterWilayahDto>>();
                else
                    ErrorMessage = Response.Data.Ui_msg;
            }
            else
            {
                ErrorMessage = Response.Error.Message;
            }

            if (_viewModel.WilayahList.Count == 0)
                _viewModel.IsEmptyWilayah = true;

            _viewModel.IsLoadingWilayah = false;
        }

        private async Task GetAreaAsync(int? IdWilayah)
        {
            if (IdWilayah == null)
            {
                _viewModel.IsEmptyArea = true;
                _viewModel.IsLoadingArea = false;
                return;
            }

            _viewModel.IsEmptyArea = false;

            _viewModel.IsLoadingArea = true;

            var param = new Dictionary<string, dynamic>
            {
                { "IdWilayah", IdWilayah }
            };

            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{ApiVersion}/master-area", param));

            _viewModel.AreaList = new ObservableCollection<MasterAreaDto>();

            if (!Response.IsError)
            {
                var Result = Response.Data;

                if (Result.Status && Result.Data != null)
                    _viewModel.AreaList = Result.Data.ToObject<ObservableCollection<MasterAreaDto>>();
                else
                    ErrorMessage = Response.Data.Ui_msg;
            }
            else
            {
                ErrorMessage = Response.Error.Message;
            }

            if (_viewModel.AreaList.Count == 0)
                _viewModel.IsEmptyArea = true;

            _viewModel.IsLoadingArea = false;
        }

        private async Task GetRayonAsync(int? IdArea)
        {
            if (IdArea == null)
            {
                _viewModel.IsEmptyRayon = true;
                _viewModel.IsLoadingRayon = false;
                return;
            }

            _viewModel.IsEmptyRayon = false;

            _viewModel.IsLoadingRayon = true;

            var param = new Dictionary<string, dynamic>
            {
                { "IdArea", IdArea }
            };

            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{ApiVersion}/master-rayon", param));

            _viewModel.RayonList = new ObservableCollection<MasterRayonDto>();

            if (!Response.IsError)
            {
                var Result = Response.Data;

                if (Result.Status)
                    _viewModel.RayonList = Result.Data.ToObject<ObservableCollection<MasterRayonDto>>();
                else
                    ErrorMessage = Response.Data.Ui_msg;
            }
            else
            {
                ErrorMessage = Response.Error.Message;
            }

            if (_viewModel.RayonList.Count == 0)
                _viewModel.IsEmptyRayon = true;

            _viewModel.IsLoadingRayon = false;
        }

        private void ShowSnackbar()
        {
            if (ErrorMessage != null)
            {
                AppDispatcherHelper.Run(_isTest, () =>
                {
                    var mainView = (BillingView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                    mainView.ShowSnackbar(ErrorMessage, "danger");
                });
            }
        }
    }
}
