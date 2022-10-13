using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Main;
using SiPintar.Views;

namespace SiPintar.Commands.Main.ManajementParaf
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly ManajementParafViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(ManajementParafViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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

            var param = new Dictionary<string, dynamic>
            {
                { "PageSize" , _viewModel.LimitData.Key },
                { "CurrentPage" , _viewModel.CurrentPage },
            };

            var response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-paraf", param));

            _viewModel.DataList = new ObservableCollection<MasterParafDto>();

            if (!response.IsError)
            {
                var result = response.Data;

                if (result.Status && result.Data != null)
                {
                    var data = result.Data.ToObject<ObservableCollection<MasterParafDto>>();

                    _viewModel.DataList = data;
                    _viewModel.TotalPage = result.TotalPage;
                    _viewModel.TotalRecord = result.Record;
                }
                else
                    errorMessage = response.Data.Ui_msg;
            }
            else
            {
                errorMessage = response.Error.Message;
            }

            _viewModel.IsPreviousButtonEnabled = _viewModel.CurrentPage > 1;
            _viewModel.IsNextButtonEnabled = _viewModel.CurrentPage < _viewModel.TotalPage;
            _viewModel.IsLeftPageNumberActive = _viewModel.CurrentPage == 1;
            _viewModel.IsRightPageNumberActive = _viewModel.CurrentPage == _viewModel.TotalPage;
            _viewModel.IsLeftPageNumberFillerVisible = _viewModel.CurrentPage != 2;
            _viewModel.IsRightPageNumberFillerVisible = _viewModel.CurrentPage != _viewModel.TotalPage - 1;
            _viewModel.IsMiddlePageNumberVisible = _viewModel.CurrentPage > 1 && _viewModel.CurrentPage < _viewModel.TotalPage;

            _viewModel.IsEmpty = _viewModel.DataList.Count == 0;
            _viewModel.IsLoading = false;

            ShowDialog(errorMessage);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog(string errorMessage)
        {
            if (!_isTest && errorMessage != null)
                DialogHelper.ShowSnackbar(_isTest, "danger", errorMessage, "main");
        }
    }
}
