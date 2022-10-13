using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Utilities;
using SiPintar.ViewModels.Bacameter.SistemKontrol;
using SiPintar.Views;

namespace SiPintar.Commands.Bacameter.SistemKontrol.LogAksesUser
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly LogAksesUserViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnLoadCommand(LogAksesUserViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            string ErrorMessage = null;
            _viewModel.IsEmpty = false;

            _viewModel.DataList = new ObservableCollection<MasterLoggerDto> { };

            _viewModel.IsLoading = true;

            var param = new Dictionary<string, dynamic>
            {
                { "PageSize" , _viewModel.LimitData.Key },
                { "CurrentPage" , _viewModel.CurrentPage },
            };

            Type type = typeof(ParamMasterLoggerFilterDto);
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property.Name != "IdPdam" && property.Name != "IdUserRequest" && property.GetValue(_viewModel.FilterData) != null)
                {
                    param.Add(property.Name, property.GetValue(_viewModel.FilterData));
                }
            }

            param = AdditionalParameter(param);

            if (_isTest)
                param.Add("TestId", _viewModel.TestId);

            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/master-logger", param);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.DataList = Result.Data.ToObject<ObservableCollection<MasterLoggerDto>>();
                    _viewModel.TotalPage = Result.TotalPage;
                    _viewModel.TotalRecord = Result.Record;
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

            _viewModel.IsPreviousButtonEnabled = _viewModel.CurrentPage > 1;
            _viewModel.IsNextButtonEnabled = _viewModel.CurrentPage < _viewModel.TotalPage;
            _viewModel.IsLeftPageNumberActive = _viewModel.CurrentPage == 1;
            _viewModel.IsRightPageNumberActive = _viewModel.CurrentPage == _viewModel.TotalPage;
            _viewModel.IsLeftPageNumberFillerVisible = _viewModel.CurrentPage != 2;
            _viewModel.IsRightPageNumberFillerVisible = _viewModel.CurrentPage != _viewModel.TotalPage - 1;
            _viewModel.IsMiddlePageNumberVisible = _viewModel.CurrentPage > 1 && _viewModel.CurrentPage < _viewModel.TotalPage;

            ShowSnackbar(ErrorMessage);

            _viewModel.IsEmpty = !_viewModel.DataList.Any();

            _ = GetUserRoleAsync();

            _viewModel.IsLoading = false;
        }

        [ExcludeFromCodeCoverage]
        private Dictionary<string, dynamic> AdditionalParameter(Dictionary<string, dynamic> param)
        {
            if (_viewModel.RentangWaktu1Filter != null)
                param.Add("WaktuUpdateMulai", _viewModel.RentangWaktu1Filter);
            if (_viewModel.RentangWaktu2Filter != null)
                param.Add("WaktuUpdateSampaiDengan", _viewModel.RentangWaktu2Filter);

            return param;
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

        public async Task GetUserRoleAsync()
        {
            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/master-user-role");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.RoleList = Result.Data.ToObject<ObservableCollection<MasterUserRoleDto>>();
                }
            }
        }
    }
}
