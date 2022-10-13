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
using SiPintar.ViewModels.Bacameter.SistemKontrol;
using SiPintar.Views;

namespace SiPintar.Commands.Bacameter.SistemKontrol.DataPelanggan
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly DataPelangganViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnLoadCommand(DataPelangganViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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

            var param = new Dictionary<string, dynamic>
            {
                { "PageSize" , _viewModel.LimitData.Key },
                { "CurrentPage" , _viewModel.CurrentPage },
            };

            Type type = typeof(MasterPelangganAirDto);
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property.Name != "IdPdam" && property.Name != "IdUserRequest" && property.GetValue(_viewModel.FormFilter) != null)
                {
                    param.Add(property.Name, property.GetValue(_viewModel.FormFilter));
                }
            }

            param = AdditionalParameter(param);

            if (_isTest)
                param.Add("TestId", _viewModel.TestId);

            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/master-pelanggan-air", param);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    var data = Result.Data.ToObject<ObservableCollection<MasterPelangganAirDto>>();

                    _viewModel.DataList = data;
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

            _viewModel.IsEmpty = _viewModel.DataList == null || !_viewModel.DataList.Any();

            _viewModel.IsLoading = false;

            _ = GetGolonganAsync();
            _ = GetDiameterAsync();
            _ = GetRayonAsync();
            _ = GetBlokAsync();
            _ = GetKelurahanAsync();
            _ = GetWilayahAsync();

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private Dictionary<string, dynamic> AdditionalParameter(Dictionary<string, dynamic> param)
        {
            if (_viewModel.RentangWaktu1Filter != null)
                param.Add("TanggalDaftarMulai", _viewModel.RentangWaktu1Filter);
            if (_viewModel.RentangWaktu2Filter != null)
                param.Add("TanggalDaftarAkhir", _viewModel.RentangWaktu2Filter);

            return param;
        }

        private void ShowSnackbar(string ErrorMessage)
        {
            if (ErrorMessage != null)
            {
                AppDispatcherHelper.Run(_isTest, () =>
                {
                    var mainView = (BacameterView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                    if (mainView != null)
                        mainView.ShowSnackbar(ErrorMessage, "danger");
                });
            }
        }

        public async Task GetGolonganAsync()
        {
            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/master-tarif-golongan");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.GolonganList = Result.Data.ToObject<ObservableCollection<MasterGolonganDto>>();
                }
            }
        }

        public async Task GetDiameterAsync()
        {
            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/master-tarif-diameter");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.DiameterList = Result.Data.ToObject<ObservableCollection<MasterDiameterDto>>();
                }
            }
        }

        public async Task GetRayonAsync()
        {
            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/master-rayon");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.RayonList = Result.Data.ToObject<ObservableCollection<MasterRayonDto>>();
                }
            }
        }

        public async Task GetBlokAsync()
        {
            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/master-blok");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.BlokList = Result.Data.ToObject<ObservableCollection<MasterBlokDto>>();
                }
            }
        }

        public async Task GetKelurahanAsync()
        {
            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/master-kelurahan");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.KelurahanList = Result.Data.ToObject<ObservableCollection<MasterKelurahanDto>>();
                }
            }
        }

        public async Task GetWilayahAsync()
        {
            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/master-wilayah");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.WilayahList = Result.Data.ToObject<ObservableCollection<MasterWilayahDto>>();
                }
            }
        }
    }
}
