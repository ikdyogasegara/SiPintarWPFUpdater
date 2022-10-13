using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Atribut;
using SiPintar.Views;

namespace SiPintar.Commands.Billing.Atribut.Flag
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly FlagViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string _apiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnLoadCommand(FlagViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            string errorMessage = null;
            _viewModel.IsEmpty = false;

            _viewModel.IsLoading = true;

            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{_apiVersion}/master-flag"));

            _viewModel.FlagList = new ObservableCollection<MasterFlagDto>();

            if (!Response.IsError)
            {
                var Result = Response.Data;

                if (Result.Status && Result.Data != null)
                    _viewModel.FlagList = Result.Data.ToObject<ObservableCollection<MasterFlagDto>>();
                else
                    errorMessage = Response.Data.Ui_msg;
            }
            else
            {
                errorMessage = Response.Error.Message;
            }

            ShowDialog(errorMessage);

            if (_viewModel.FlagList.Count == 0)
                _viewModel.IsEmpty = true;

            _viewModel.IsLoading = false;
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog(string ErrorMessage)
        {
            if (ErrorMessage != null && !_isTest)
                Application.Current.Dispatcher.Invoke(delegate
                {
                    var mainView = (BillingView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                    mainView.ShowSnackbar(ErrorMessage, "danger");
                });
        }
    }
}
