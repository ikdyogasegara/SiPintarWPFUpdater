using System;
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

namespace SiPintar.Commands.Billing.Atribut.Tarif.Diameter
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly DiameterViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;
        private readonly Dictionary<string, dynamic> _testBody;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnLoadCommand(DiameterViewModel viewModel, IRestApiClientModel restApi, bool isTest = false, Dictionary<string, dynamic> testBody = null)
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

            _viewModel.ListYear = new List<int>();

            _viewModel.IsLoading = true;

            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/master-tarif-diameter", _testBody);

            _viewModel.MasterDiameterList = new ObservableCollection<MasterDiameterDto>();

            if (!Response.IsError)
            {
                var Result = Response.Data;

                if (Result.Status && Result.Data != null)
                    _viewModel.MasterDiameterList = Result.Data.ToObject<ObservableCollection<MasterDiameterDto>>();
                else
                    ErrorMessage = Response.Data.Ui_msg;
            }
            else
            {
                ErrorMessage = Response.Error.Message;
            }

            ShowSnackbar(ErrorMessage);

            _viewModel.IsEmpty = !_viewModel.MasterDiameterList.Any();

            int minYear = 0;
            if (!_viewModel.IsEmpty)
            {
                minYear = (_viewModel.MasterDiameterList.Min(g => g.PeriodeMulaiBerlaku) ?? ((DateTime.Now.Year * 100) + DateTime.Now.Month)) / 100;
            }
            else
            {
                minYear = DateTime.Now.Year - 10;
            }
            _viewModel.ListYear = new List<int>(Enumerable.Range(minYear, DateTime.Now.Year - minYear + 1));

            if (!_isTest) Application.Current.Dispatcher.Invoke(() => _viewModel.ReloadData());

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
