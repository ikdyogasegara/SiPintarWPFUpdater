using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Atribut.Tarif;
using SiPintar.Views;

namespace SiPintar.Commands.Billing.Atribut.Tarif.AdministrasiLain
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly AdministrasiLainViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;
        private readonly Dictionary<string, dynamic> _testBody;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnLoadCommand(AdministrasiLainViewModel viewModel, IRestApiClientModel restApi, bool isTest = false, Dictionary<string, dynamic> testBody = null)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
            _testBody = testBody;
        }

        public override async Task ExecuteAsync(object parameter)
        {

            string ErrorMessage = null;
            _viewModel.IsEmpty = false;

            _viewModel.IsLoading = true;

            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/master-tarif-administrasi-lain", _testBody);

            _viewModel.DataAdministrasi = new ObservableCollection<MasterAdministrasiLainDto>();

            if (!Response.IsError)
            {
                var Result = Response.Data;

                if (Result.Status && Result.Data != null)
                    _viewModel.DataAdministrasi = Result.Data.ToObject<ObservableCollection<MasterAdministrasiLainDto>>();
                else
                    ErrorMessage = Response.Data.Ui_msg;
            }
            else
            {
                ErrorMessage = Response.Error.Message;
            }

            ShowSnackbar(ErrorMessage);

            if (_viewModel.DataAdministrasi.Count == 0)
                _viewModel.IsEmpty = true;

            _viewModel.TotalData = _viewModel.DataAdministrasi.Count.ToString();
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
    }
}
