using System.Collections.Generic;
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
    public class OnFilterCommand : AsyncCommandBase
    {
        private readonly PostingViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnFilterCommand(PostingViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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

            var param = new Dictionary<string, dynamic>
            {
                {"PageSize" ,  _viewModel.Limit},
                { "CurrentPage" , _viewModel.CurrentPage },
            };

            if (_viewModel.SelectedUser != null)
                param.Add("IdUser", _viewModel.SelectedUser.IdUser);

            if (_viewModel.SelectedDataTahunPosting.HasValue)
                param.Add("TahunPosting", _viewModel.SelectedDataTahunPosting);

            if (_viewModel.SelectedDataTahunRekening != null)
                param.Add("TahunPeriodeDRD", _viewModel.SelectedDataTahunRekening.Tahun);


            var ResponseRiwayatPosting = await _restApi.GetAsync("/api/v1/riwayat-posting", param);
            _viewModel.RiwayatPosting = new List<RiwayatPostingDto>();

            if (!ResponseRiwayatPosting.IsError)
            {
                var Result = ResponseRiwayatPosting.Data;

                if (Result.Status && Result.Data != null)
                {
                    _viewModel.RiwayatPosting = Result.Data.ToObject<List<RiwayatPostingDto>>().OrderByDescending(x => x.WaktuPosting).ToList();
                    _viewModel.TotalRecord = (int)ResponseRiwayatPosting.Data.Record;
                    _viewModel.TotalPage = Result.TotalPage;
                }
                else
                    ErrorMessage = ResponseRiwayatPosting.Data.Ui_msg;
            }
            else
            {
                ErrorMessage = ResponseRiwayatPosting.Error.Message;
            }

            _viewModel.IsPreviousButtonEnabled = _viewModel.CurrentPage > 1;
            _viewModel.IsNextButtonEnabled = _viewModel.CurrentPage < _viewModel.TotalPage;
            _viewModel.IsLeftPageNumberActive = _viewModel.CurrentPage == 1;
            _viewModel.IsRightPageNumberActive = _viewModel.CurrentPage == _viewModel.TotalPage;
            _viewModel.IsLeftPageNumberFillerVisible = _viewModel.CurrentPage != 2;
            _viewModel.IsRightPageNumberFillerVisible = _viewModel.CurrentPage != _viewModel.TotalPage - 1;
            _viewModel.IsMiddlePageNumberVisible = _viewModel.CurrentPage > 1 && _viewModel.CurrentPage < _viewModel.TotalPage;

            if (_viewModel.RiwayatPosting.Count == 0)
                _viewModel.IsEmpty = true;

            ShowResult(ErrorMessage);

            _viewModel.IsLoading = false;
            _viewModel.IsLoadingForm = false;


            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void ShowResult(string ErrorMessage)
        {
            if (ErrorMessage != null && !_isTest)
                App.Current.Dispatcher.Invoke(delegate
                {
                    var mainView = (BillingView)App.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                    if (mainView != null)
                        mainView.ShowSnackbar(ErrorMessage, "danger");
                });
        }

    }
}
