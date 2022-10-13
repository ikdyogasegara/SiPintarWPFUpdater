using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Atribut;
using SiPintar.Views;

namespace SiPintar.Commands.Hublang.Atribut.JenisNonAir
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly JenisNonAirViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(JenisNonAirViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..
            _viewModel.IsLoading = true;

            var param = new Dictionary<string, dynamic>
            {
                { "PageSize" , _viewModel.LimitData.Key },
                { "CurrentPage" , _viewModel.CurrentPage },
            };

            if (_viewModel.IsKodeJenisNonAirChecked && !string.IsNullOrWhiteSpace(_viewModel.FilterKodeJenisNonAir))
                param.Add("KodeJenisNonAir", _viewModel.FilterKodeJenisNonAir);
            if (_viewModel.IsNamaJenisNonAirChecked && !string.IsNullOrWhiteSpace(_viewModel.FilterNamaJenisNonAir))
                param.Add("NamaJenisNonAir", _viewModel.FilterNamaJenisNonAir);

            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-jenis-non-air", param));
            _viewModel.JenisNonAirList = new ObservableCollection<MasterJenisNonAirDto>();
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status && Result.Data != null)
                {
                    _viewModel.JenisNonAirList = Result.Data.ToObject<ObservableCollection<MasterJenisNonAirDto>>();
                    _viewModel.TotalPage = Result.TotalPage;
                    _viewModel.TotalRecord = Result.Record;
                }
                else
                    ShowResult(Response.Data.Ui_msg);
            }
            else
                ShowResult(Response.Error.Message);

            _viewModel.IsPreviousButtonEnabled = _viewModel.CurrentPage > 1;
            _viewModel.IsNextButtonEnabled = _viewModel.CurrentPage < _viewModel.TotalPage;
            _viewModel.IsLeftPageNumberActive = _viewModel.CurrentPage == 1;
            _viewModel.IsRightPageNumberActive = _viewModel.CurrentPage == _viewModel.TotalPage;
            _viewModel.IsLeftPageNumberFillerVisible = _viewModel.CurrentPage != 2;
            _viewModel.IsRightPageNumberFillerVisible = _viewModel.CurrentPage != _viewModel.TotalPage - 1;
            _viewModel.IsMiddlePageNumberVisible = _viewModel.CurrentPage > 1 && _viewModel.CurrentPage < _viewModel.TotalPage;
            _viewModel.IsPageNumberVisible = _viewModel.TotalPage > 1;

            _viewModel.IsEmpty = _viewModel.JenisNonAirList.Count == 0;
            _viewModel.IsLoading = false;

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void ShowResult(string ErrorMessage)
        {
            if (ErrorMessage != null && !_isTest)
                Application.Current.Dispatcher.Invoke(delegate { ShowError(ErrorMessage); });
        }

        [ExcludeFromCodeCoverage]
        private void ShowError(string ErrorMessage)
        {
            {
                var mainView = (HublangView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                if (mainView != null)
                    mainView.ShowSnackbar(ErrorMessage, "danger");
            }
        }
    }
}
