using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using AppBusiness.Data.DTOs;
using SiPintar.Utilities;
using SiPintar.ViewModels.Bacameter.SistemKontrol.TarifGolongan;
using SiPintar.Views;

namespace SiPintar.Commands.Bacameter.SistemKontrol.TarifGolongan.Golongan
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly GolonganViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;
        private readonly Dictionary<string, dynamic> _testBody;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnLoadCommand(GolonganViewModel viewModel, IRestApiClientModel restApi, bool isTest = false, Dictionary<string, dynamic> testBody = null)
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

            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/master-tarif-golongan", _testBody);

            _viewModel.MasterGolonganList = new ObservableCollection<MasterGolonganDto>();
            _viewModel.MasterGolonganListGroup = new ListCollectionView(_viewModel.MasterGolonganList);

            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status && Result.Data != null)
                    _viewModel.MasterGolonganList = Result.Data.ToObject<ObservableCollection<MasterGolonganDto>>();
                else
                    ErrorMessage = Response.Data.Ui_msg;
            }
            else
            {
                ErrorMessage = Response.Error.Message;
            }

            ShowSnackbar(ErrorMessage);

            _viewModel.IsEmpty = !_viewModel.MasterGolonganList.Any();

            int minYear = 0;
            if (!_viewModel.IsEmpty)
            {
                minYear = (_viewModel.MasterGolonganList.Min(g => g.PeriodeMulaiBerlaku) ?? ((DateTime.Now.Year * 100) + DateTime.Now.Month)) / 100;
            }
            else
            {
                minYear = DateTime.Now.Year - 10;
            }
            _viewModel.ListYear = new List<int>(Enumerable.Range(minYear, DateTime.Now.Year - minYear + 1));

            if (!_isTest) Application.Current.Dispatcher.Invoke(delegate { _viewModel.ReloadData(); });

            _viewModel.IsLoading = false;
        }

        [ExcludeFromCodeCoverage]
        public void ShowSnackbar(string ErrorMessage)
        {
            if (ErrorMessage != null && !_isTest)
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    var mainView = (BacameterView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                    if (mainView != null)
                        mainView.ShowSnackbar(ErrorMessage, "danger");
                });
            }
        }
    }
}
