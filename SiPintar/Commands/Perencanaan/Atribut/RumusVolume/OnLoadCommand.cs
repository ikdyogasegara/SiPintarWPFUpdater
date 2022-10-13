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
using SiPintar.ViewModels.Perencanaan.Atribut;
using SiPintar.Views;

namespace SiPintar.Commands.Perencanaan.Atribut.RumusVolume
{
    [ExcludeFromCodeCoverage]
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly RumusVolumeViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = App.Current.Resources["api_version"]?.ToString();

        public OnLoadCommand(RumusVolumeViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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

            var param = new Dictionary<string, dynamic>();

            Type type = typeof(MasterRumusVolumeOngkosDto);
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property.Name != "IdPdam" && property.Name != "IdUserRequest" && property.GetValue(_viewModel.FilterData) != null)
                {
                    param.Add(property.Name, property.GetValue(_viewModel.FilterData));
                }
            }

            if (_isTest)
                param.Add("TestId", _viewModel.TestId);

            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{ApiVersion}/master-rumus-volume-ongkos", param));

            _viewModel.DataList = new ObservableCollection<MasterRumusVolumeOngkosDto>();

            if (!Response.IsError)
            {
                var Result = Response.Data;

                if (Result.Status && Result.Data != null)
                    _viewModel.DataList = Result.Data.ToObject<ObservableCollection<MasterRumusVolumeOngkosDto>>();
                else
                    ErrorMessage = Response.Data.Ui_msg;
            }
            else
            {
                ErrorMessage = Response.Error.Message;
            }

            ShowSnackbar(ErrorMessage);

            if (_viewModel.DataList.Count == 0)
                _viewModel.IsEmpty = true;

            _viewModel.IsLoading = false;

            _ = GetOngkosListAsync();

            await Task.FromResult(_isTest);
        }

        public async Task GetOngkosListAsync()
        {
            _viewModel.OngkosList = new ObservableCollection<MasterOngkosDto>();

            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/master-ongkos");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.OngkosList = Result.Data.ToObject<ObservableCollection<MasterOngkosDto>>();
                }
            }
        }

        [ExcludeFromCodeCoverage]
        private void ShowSnackbar(string ErrorMessage)
        {
            if (ErrorMessage != null && !_isTest)
                Application.Current.Dispatcher.Invoke(delegate
                {
                    var mainView = (PerencanaanView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                    mainView.ShowSnackbar(ErrorMessage, "danger");
                });
        }
    }
}
