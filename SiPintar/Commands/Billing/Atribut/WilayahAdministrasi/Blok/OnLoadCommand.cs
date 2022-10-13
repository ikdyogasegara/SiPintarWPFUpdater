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

namespace SiPintar.Commands.Billing.Atribut.WilayahAdministrasi.Blok
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly BlokViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnLoadCommand(BlokViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            string ErrorMessage = null;
            _viewModel.IsEmpty = false;

            _viewModel.IsLoading = true;

            _ = GetRayonAsync();

            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{ApiVersion}/master-blok"));

            _viewModel.MasterBlokList = new ObservableCollection<MasterBlokDto>();

            if (!Response.IsError)
            {
                var Result = Response.Data;

                if (Result.Status && Result.Data != null)
                    _viewModel.MasterBlokList = Result.Data.ToObject<ObservableCollection<MasterBlokDto>>();
                else
                    ErrorMessage = Response.Data.Ui_msg;
            }
            else
            {
                ErrorMessage = Response.Error.Message;
            }

            ShowSnackbar(ErrorMessage);

            if (_viewModel.MasterBlokList.Count == 0)
                _viewModel.IsEmpty = true;

            _viewModel.IsLoading = false;
        }

        private void ShowSnackbar(string ErrorMessage)
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

        private async Task GetRayonAsync()
        {
            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{ApiVersion}/master-rayon"));
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status && Result.Data.Any())
                {
                    _viewModel.RayonList = Result.Data.ToObject<ObservableCollection<MasterRayonDto>>();
                }
            }
        }
    }
}
