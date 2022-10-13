using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Perencanaan.Atribut;

namespace SiPintar.Commands.Perencanaan.Atribut.Ongkos
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly OngkosViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = App.Current.Resources["api_version"]?.ToString();

        public OnLoadCommand(OngkosViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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
                {"PageSize" ,  _viewModel.Limit},
                { "CurrentPage" , _viewModel.CurrentPage },
            };

            Type type = typeof(MasterOngkosDto);
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property.Name != "IdPdam" && property.Name != "IdUserRequest" && property.GetValue(_viewModel.OngkosFilter) != null)
                {
                    param.Add(property.Name, property.GetValue(_viewModel.OngkosFilter));
                }
            }

            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/master-ongkos", param);

            _viewModel.MasterOngkosList = new List<MasterOngkosDto>();

            if (!Response.IsError)
            {
                var Result = Response.Data;

                if (Result.Status && Result.Data != null)
                {
                    _viewModel.MasterOngkosList = Result.Data.ToObject<ObservableCollection<MasterOngkosDto>>().OrderByDescending(x => x.IdOngkos).ToList();
                    _viewModel.TotalRecord = (int)Response.Data.Record;
                    _viewModel.TotalPage = Result.TotalPage;
                }
                else
                    ErrorMessage = Response.Data.Ui_msg;
            }
            else
            {
                ErrorMessage = Response.Error.Message;
            }

            _viewModel.IsEmpty = !_viewModel.MasterOngkosList.Any();
            _viewModel.IsLoading = false;

            if (App.OpenedWindow.ContainsKey("perencanaan"))
                DialogHelper.SnackbarPerencanaanHandler(ErrorMessage, null, "error", null, _isTest, "PerencanaanRootDialog", App.OpenedWindow["perencanaan"]);

            await Task.FromResult(_isTest);
        }
    }
}
