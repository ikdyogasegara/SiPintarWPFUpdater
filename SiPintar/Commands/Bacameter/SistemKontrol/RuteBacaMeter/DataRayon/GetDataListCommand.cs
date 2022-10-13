using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Utilities;
using SiPintar.ViewModels.Bacameter.SistemKontrol.RuteBacaMeter;
using SiPintar.Views;

namespace SiPintar.Commands.Bacameter.SistemKontrol.RuteBacaMeter.DataRayon
{
    public class GetDataListCommand : AsyncCommandBase
    {
        private readonly DataRayonViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public GetDataListCommand(DataRayonViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.RayonList = new ObservableCollection<MasterRayonDto>();

            _viewModel.IsLoadingRayon = true;

            string ErrorMessage = null;
            var param = new Dictionary<string, dynamic>
            {
                { "PageSize" , _viewModel.LimitDataRayon.Key },
                { "CurrentPage" , _viewModel.CurrentPageRayon },
            };

            if (!string.IsNullOrEmpty(_viewModel.SearchKeywordForm))
                param.Add("NamaRayon", _viewModel.SearchKeywordForm);

            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{ApiVersion}/master-rayon", param));

            if (!Response.IsError)
            {
                var Result = Response.Data;

                if (Result.Status && Result.Data != null)
                {
                    var ListData = Result.Data.ToObject<ObservableCollection<MasterRayonDto>>();

                    _viewModel.RayonList = ListData;
                    _viewModel.TotalPageRayon = Result.TotalPage;
                    _viewModel.TotalRecordRayon = Convert.ToInt32(Result.Record);
                }
                else
                {
                    ErrorMessage = Response.Data.Ui_msg;
                }
            }
            else
            {
                ErrorMessage = Response.Error.Message;
            }

            ShowResult(ErrorMessage);

            _viewModel.IsPreviousButtonEnabledRayon = _viewModel.CurrentPageRayon > 1;
            _viewModel.IsNextButtonEnabledRayon = _viewModel.CurrentPageRayon < _viewModel.TotalPageRayon;
            _viewModel.IsLeftPageNumberActiveRayon = _viewModel.CurrentPageRayon == 1;
            _viewModel.IsRightPageNumberActiveRayon = _viewModel.CurrentPageRayon == _viewModel.TotalPageRayon;
            _viewModel.IsLeftPageNumberFillerVisibleRayon = _viewModel.CurrentPageRayon != 2;
            _viewModel.IsRightPageNumberFillerVisibleRayon = _viewModel.CurrentPageRayon < _viewModel.TotalPageRayon - 1;
            _viewModel.IsMiddlePageNumberVisibleRayon = _viewModel.CurrentPageRayon > 1 && _viewModel.CurrentPageRayon < _viewModel.TotalPageRayon;

            _viewModel.IsEmptyRayon = _viewModel.RayonList.Count == 0;

            _viewModel.IsLoadingRayon = false;

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void ShowResult(string ErrorMessage)
        {
            if (ErrorMessage != null && !_isTest)
                Application.Current.Dispatcher.Invoke(delegate
                {
                    if (Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive) is BacameterView mainView)
                        mainView.ShowSnackbar(ErrorMessage, "danger");
                });
        }
    }
}
