using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views;

namespace SiPintar.Commands.Billing.Supervisi.Posting
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly PostingViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(PostingViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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
            _viewModel.IsLoadingForm = true;
            var minYear = DateTime.Now.Year - 10;
            _viewModel.ListTahunPosting = new List<int>(Enumerable.Range(minYear, 10 + 1));

            var param = new Dictionary<string, dynamic>
            {
                {"PageSize" ,  _viewModel.Limit},
                { "CurrentPage" , _viewModel.CurrentPage },
            };

            var responseRiwayatPosting = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/riwayat-posting", param);

            _viewModel.RiwayatPosting = new List<RiwayatPostingDto>();

            if (!responseRiwayatPosting.IsError)
            {
                var result = responseRiwayatPosting.Data;

                if (result.Status && result.Data != null)
                {
                    _viewModel.RiwayatPosting = result.Data.ToObject<List<RiwayatPostingDto>>().OrderByDescending(x => x.WaktuPosting).ToList();
                    _viewModel.TotalRecord = (int)responseRiwayatPosting.Data.Record;
                    _viewModel.TotalPage = result.TotalPage;
                }
                else
                    ErrorMessage = responseRiwayatPosting.Data.Ui_msg;
            }
            else
            {
                ErrorMessage = responseRiwayatPosting.Error.Message;
            }

            _viewModel.IsPreviousButtonEnabled = _viewModel.CurrentPage > 1;
            _viewModel.IsNextButtonEnabled = _viewModel.CurrentPage < _viewModel.TotalPage;
            _viewModel.IsLeftPageNumberActive = _viewModel.CurrentPage == 1;
            _viewModel.IsRightPageNumberActive = _viewModel.CurrentPage == _viewModel.TotalPage;
            _viewModel.IsLeftPageNumberFillerVisible = _viewModel.CurrentPage != 2;
            _viewModel.IsRightPageNumberFillerVisible = _viewModel.CurrentPage != _viewModel.TotalPage - 1;
            _viewModel.IsMiddlePageNumberVisible = _viewModel.CurrentPage > 1 && _viewModel.CurrentPage < _viewModel.TotalPage;

            if (_viewModel.RiwayatPosting.Count > 0)
                _viewModel.IsEmpty = false;

            _ = GetUserAsync();
            _ = GetTahunRekeningAsync();

            _viewModel.IsLoading = false;
            _viewModel.IsLoadingForm = false;
            ShowResult(ErrorMessage);
        }

        [ExcludeFromCodeCoverage]
        private async Task GetUserAsync()
        {
            var responseKasir = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-user");
            _viewModel.MasterUserList = new ObservableCollection<MasterUserDto>();
            if (!responseKasir.IsError)
            {
                var result = responseKasir.Data;

                if (result.Status && result.Data != null)
                    _viewModel.MasterUserList = result.Data.ToObject<ObservableCollection<MasterUserDto>>();
            }
        }

        [ExcludeFromCodeCoverage]
        private async Task GetTahunRekeningAsync()
        {
            var responsePeriode = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-periode-rekening");
            _viewModel.MasterTahunRekeningList = new List<MasterPeriodeDto>();

            if (!responsePeriode.IsError)
            {
                var result = responsePeriode.Data;
                if (result.Status && result.Data != null)
                {
                    _viewModel.MasterTahunRekeningList = result.Data.ToObject<List<MasterPeriodeDto>>().GroupBy(x => x.Tahun).Select(x => x.First()).ToList();
                }

            }
        }

        [ExcludeFromCodeCoverage]
        private void ShowResult(string ErrorMessage)
        {
            if (ErrorMessage != null && !_isTest)
                Application.Current.Dispatcher.Invoke(delegate
                {
                    var mainView = (BillingView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                    if (mainView != null)
                        mainView.ShowSnackbar(ErrorMessage, "danger");
                });
        }
    }
}
