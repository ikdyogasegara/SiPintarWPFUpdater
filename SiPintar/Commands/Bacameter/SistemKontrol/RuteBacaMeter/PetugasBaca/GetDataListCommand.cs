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

namespace SiPintar.Commands.Bacameter.SistemKontrol.RuteBacaMeter.PetugasBaca
{
    public class GetDataListCommand : AsyncCommandBase
    {
        private readonly PetugasBacaViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public GetDataListCommand(PetugasBacaViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.PetugasList = new ObservableCollection<MasterPetugasBacaDto>();

            _viewModel.IsLoadingPetugas = true;

            string ErrorMessage = null;
            var param = new Dictionary<string, dynamic>
            {
                { "PageSize" , _viewModel.LimitDataPetugas.Key },
                { "CurrentPage" , _viewModel.CurrentPagePetugas },
            };

            if (!string.IsNullOrEmpty(_viewModel.SearchKeywordForm))
                param.Add("PetugasBaca", _viewModel.SearchKeywordForm);

            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{ApiVersion}/master-petugas-baca", param));

            if (!Response.IsError)
            {
                var Result = Response.Data;

                if (Result.Status && Result.Data != null)
                {
                    var ListData = Result.Data.ToObject<ObservableCollection<MasterPetugasBacaDto>>();

                    _viewModel.PetugasList = ListData;
                    _viewModel.TotalPagePetugas = Result.TotalPage;
                    _viewModel.TotalRecordPetugas = Convert.ToInt32(Result.Record);
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

            _viewModel.IsPreviousButtonEnabledPetugas = _viewModel.CurrentPagePetugas > 1;
            _viewModel.IsNextButtonEnabledPetugas = _viewModel.CurrentPagePetugas < _viewModel.TotalPagePetugas;
            _viewModel.IsLeftPageNumberActivePetugas = _viewModel.CurrentPagePetugas == 1;
            _viewModel.IsRightPageNumberActivePetugas = _viewModel.CurrentPagePetugas == _viewModel.TotalPagePetugas;
            _viewModel.IsLeftPageNumberFillerVisiblePetugas = _viewModel.CurrentPagePetugas != 2;
            _viewModel.IsRightPageNumberFillerVisiblePetugas = _viewModel.CurrentPagePetugas < _viewModel.TotalPagePetugas - 1;
            _viewModel.IsMiddlePageNumberVisiblePetugas = _viewModel.CurrentPagePetugas > 1 && _viewModel.CurrentPagePetugas < _viewModel.TotalPagePetugas;

            _viewModel.IsEmptyPetugas = _viewModel.PetugasList.Count == 0;

            _viewModel.IsLoadingPetugas = false;

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
