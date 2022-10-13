using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views;

namespace SiPintar.Commands.Billing.Supervisi.RekeningLimbah
{
    public class OnFilterCommand : AsyncCommandBase
    {
        private readonly RekeningLimbahViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnFilterCommand(RekeningLimbahViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsEmpty = false;

            _viewModel.RekeningList = new ObservableCollection<RekeningLimbahDto> { };

            _viewModel.IsLoading = true;

            var param = new Dictionary<string, dynamic>
            {
                { "PageSize" , _viewModel.LimitData.Key },
                { "CurrentPage" , _viewModel.CurrentPage },
            };

            Type type = typeof(RekeningLimbahDto);
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property.Name != "IdPdam" && property.Name != "IdUserRequest" && property.GetValue(_viewModel.RekeningFilter) != null)
                {
                    param.Add(property.Name, property.GetValue(_viewModel.RekeningFilter));
                }
            }

            param = AdditionalParameter(param);

            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/rekening-limbah", param);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.RekeningList = Result.Data.ToObject<ObservableCollection<RekeningLimbahDto>>();
                    _viewModel.TotalPage = Result.TotalPage;
                    _viewModel.TotalRecord = Result.Record;
                }
                else
                {
                    DialogHelper.ShowSnackbar(_isTest, "danger", Response.Data.Ui_msg, "billing");
                }
            }
            else
            {
                DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error.Message, "billing");
            }

            _viewModel.IsPreviousButtonEnabled = _viewModel.CurrentPage > 1;
            _viewModel.IsNextButtonEnabled = _viewModel.CurrentPage < _viewModel.TotalPage;
            _viewModel.IsLeftPageNumberActive = _viewModel.CurrentPage == 1;
            _viewModel.IsRightPageNumberActive = _viewModel.CurrentPage == _viewModel.TotalPage;
            _viewModel.IsLeftPageNumberFillerVisible = _viewModel.CurrentPage != 2;
            _viewModel.IsRightPageNumberFillerVisible = _viewModel.CurrentPage != _viewModel.TotalPage - 1;
            _viewModel.IsMiddlePageNumberVisible = _viewModel.CurrentPage > 1 && _viewModel.CurrentPage < _viewModel.TotalPage;

            _viewModel.IsEmpty = !_viewModel.RekeningList.Any();

            _viewModel.IsLoading = false;
        }

        private Dictionary<string, dynamic> AdditionalParameter(Dictionary<string, dynamic> param)
        {
            if (_viewModel.TglBayarAwalFilter != null)
                param.Add("TglBayarAwal", _viewModel.TglBayarAwalFilter);
            if (_viewModel.TglBayarAkhirFilter != null)
                param.Add("TglBayarAkhir", _viewModel.TglBayarAkhirFilter);
            if (_viewModel.TglUploadAwalFilter != null)
                param.Add("TglUploadAwal", _viewModel.TglUploadAwalFilter);
            if (_viewModel.TglUploadAkhirFilter != null)
                param.Add("TglUploadAkhir", _viewModel.TglUploadAkhirFilter);
            if (_viewModel.TglPublishAwalFilter != null)
                param.Add("TglPublishAwal", _viewModel.TglPublishAwalFilter);
            if (_viewModel.TglPublishAkhirFilter != null)
                param.Add("TglPublishAkhir", _viewModel.TglPublishAkhirFilter);
            if (_viewModel.BiayaAwalFilter != null)
                param.Add("BiayaAwal", _viewModel.BiayaAwalFilter);
            if (_viewModel.BiayaAkhirFilter != null)
                param.Add("BiayaAkhir", _viewModel.BiayaAkhirFilter);

            return param;
        }
    }
}
