using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Atribut;
using SiPintar.Views;

namespace SiPintar.Commands.Billing.Atribut.SumberAir
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly SumberAirViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnLoadCommand(SumberAirViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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

            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{ApiVersion}/master-sumber-air"));

            _viewModel.SumberAirList = new ObservableCollection<MasterSumberAirDto>();

            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status && Result.Data != null)
                    _viewModel.SumberAirList = Result.Data.ToObject<ObservableCollection<MasterSumberAirDto>>();
                else
                    ErrorMessage = Response.Data.Ui_msg;
            }
            else
            {
                ErrorMessage = Response.Error.Message;
            }

            ShowDialog(ErrorMessage);

            if (_viewModel.SumberAirList.Count == 0)
                _viewModel.IsEmpty = true;

            _viewModel.IsLoading = false;
        }

        private void ShowDialog(string ErrorMessage)
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
