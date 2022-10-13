using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Utilities;
using SiPintar.ViewModels.Main;
using SiPintar.Views;

namespace SiPintar.Commands.Main.DaftarPengguna
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly DaftarPenggunaViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(DaftarPenggunaViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            string ApiVersion = Application.Current.Resources["api_version"]?.ToString();
            string CurrentIdUser = Application.Current.Resources["IdUser"]?.ToString();

            string ErrorMessage = null;
            _viewModel.IsEmpty = false;

            _viewModel.IsLoading = true;

            var param = new Dictionary<string, dynamic>
            {
                { "PageSize" , _viewModel.LimitData.Key },
                { "CurrentPage" , _viewModel.CurrentPage },
            };

            if (_isTest)
                param.Add("TestId", _viewModel.TestId);

            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{ApiVersion}/master-user", param));

            _viewModel.DataList = new ObservableCollection<MasterUserDto>();

            if (!Response.IsError)
            {
                var Result = Response.Data;

                if (Result.Status && Result.Data != null)
                {
                    var data = Result.Data.ToObject<ObservableCollection<MasterUserDto>>();

                    foreach (var item in data)
                    {
                        if (CurrentIdUser != null && item.IdUser == Convert.ToInt32(CurrentIdUser))
                        {
                            data.Remove(item);
                            break;
                        }
                    }

                    _viewModel.DataList = data;
                    _viewModel.TotalPage = Result.TotalPage;
                    _viewModel.TotalRecord = Result.Record - 1;
                }

                else
                    ErrorMessage = Response.Data.Ui_msg;
            }
            else
            {
                ErrorMessage = Response.Error.Message;
            }

            _viewModel.IsPreviousButtonEnabled = _viewModel.CurrentPage > 1;
            _viewModel.IsNextButtonEnabled = _viewModel.CurrentPage < _viewModel.TotalPage;
            _viewModel.IsLeftPageNumberActive = _viewModel.CurrentPage == 1;
            _viewModel.IsRightPageNumberActive = _viewModel.CurrentPage == _viewModel.TotalPage;
            _viewModel.IsLeftPageNumberFillerVisible = _viewModel.CurrentPage != 2;
            _viewModel.IsRightPageNumberFillerVisible = _viewModel.CurrentPage != _viewModel.TotalPage - 1;
            _viewModel.IsMiddlePageNumberVisible = _viewModel.CurrentPage > 1 && _viewModel.CurrentPage < _viewModel.TotalPage;

            ShowDialog(ErrorMessage);

            _viewModel.IsEmpty = _viewModel.DataList.Count == 0;

            _ = GetRoleAsync();
            _ = GetLoketAsync();

            _viewModel.IsLoading = false;
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog(string ErrorMessage)
        {
            if (ErrorMessage != null && !_isTest)
                Application.Current.Dispatcher.Invoke(delegate
                {
                    var mainView = (MainView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                    if (mainView != null)
                        mainView.ShowSnackbar(ErrorMessage, "danger");
                });
        }

        public async Task GetRoleAsync()
        {
            string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/master-user-role");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.UserRoleList = Result.Data.ToObject<ObservableCollection<MasterUserRoleDto>>();
                }
            }
        }

        public async Task GetLoketAsync()
        {
            string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/master-loket");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.LoketList = Result.Data.ToObject<ObservableCollection<MasterLoketDto>>();
                }
            }
        }
    }
}
