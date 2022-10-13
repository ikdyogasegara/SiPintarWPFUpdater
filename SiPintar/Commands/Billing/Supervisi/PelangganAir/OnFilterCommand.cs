using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Commands.Billing.Supervisi.PelangganAir
{
    public class OnFilterCommand : AsyncCommandBase
    {
        private readonly PelangganAirViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnFilterCommand(PelangganAirViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.MasterPelangganList = null;
            _viewModel.IsLoading = true;

            var param = new Dictionary<string, dynamic>
            {
                { "PageSize" , _viewModel.LimitData.Key },
                { "CurrentPage" , _viewModel.CurrentPage },
            };

            var type = typeof(MasterPelangganAirDto);
            var properties = type.GetProperties();

            foreach (var property in properties)
            {
                if (property.Name != "IdPdam" && property.Name != "IdUserRequest" && property.GetValue(_viewModel.PelangganFilter) != null)
                {
                    param.Add(property.Name, property.GetValue(_viewModel.PelangganFilter));
                }
            }

            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-pelanggan-air", param);
            _viewModel.MasterPelangganList = new ObservableCollection<MasterPelangganAirWpf>();

            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    _viewModel.MasterPelangganList = result.Data.ToObject<ObservableCollection<MasterPelangganAirWpf>>();
                    _viewModel.TotalRecord = result.Record;
                    _viewModel.TotalPage = result.TotalPage;
                    _viewModel.IsLoading = false;
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", result.Ui_msg, "billing");
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message, "billing");

            _viewModel.IsPreviousButtonEnabled = _viewModel.CurrentPage > 1;
            _viewModel.IsNextButtonEnabled = _viewModel.CurrentPage < _viewModel.TotalPage;
            _viewModel.IsLeftPageNumberActive = _viewModel.CurrentPage == 1;
            _viewModel.IsRightPageNumberActive = _viewModel.CurrentPage == _viewModel.TotalPage;
            _viewModel.IsLeftPageNumberFillerVisible = _viewModel.CurrentPage != 2;
            _viewModel.IsRightPageNumberFillerVisible = _viewModel.CurrentPage != _viewModel.TotalPage - 1;
            _viewModel.IsMiddlePageNumberVisible = _viewModel.CurrentPage > 1 && _viewModel.CurrentPage < _viewModel.TotalPage;
            _viewModel.IsEmpty = !_viewModel.MasterPelangganList.Any();
            _viewModel.IsLoading = false;
            await Task.FromResult(_isTest);
        }
    }
}
