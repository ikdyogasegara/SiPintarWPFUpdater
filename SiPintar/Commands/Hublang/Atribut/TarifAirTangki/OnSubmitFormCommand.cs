using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Atribut;
using SiPintar.Views;

namespace SiPintar.Commands.Hublang.Atribut.TarifAirTangki
{
    public class OnSubmitFormCommand : AsyncCommandBase
    {
        private readonly TarifAirTangkiViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitFormCommand(TarifAirTangkiViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoading = true;

            var Data = parameter as MasterTarifTangkiDto;
            Type type = typeof(MasterTarifTangkiDto);
            PropertyInfo[] properties = type.GetProperties();
            var body = new Dictionary<string, dynamic>();
            foreach (PropertyInfo property in properties)
            {
                if (property.Name != "IdPdam" && property.Name != "IdUserRequest")
                {
                    body.Add(property.Name, property.GetValue(Data));
                }
            }

            RestApiResponse Response = null;
            if (_viewModel.IsEdit)
                Response = await Task.Run(() => _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/master-tarif-tangki", null, body));
            else
                Response = await Task.Run(() => _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/master-tarif-tangki", body));

            _viewModel.IsLoading = false;

            if (!Response.IsError)
            {
                if (!_isTest)
                    Application.Current.Dispatcher.Invoke(() => { DialogHost.Close("HublangRootDialog"); });
                var Result = Response.Data;
                ShowResult(Result.Ui_msg, Result.Status);
            }
            else
                ShowResult(Response.Error.Message, false);

            await Task.Run(() => ((AsyncCommandBase)_viewModel.OnLoadCommand).ExecuteAsync(Data));
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void ShowResult(string Msg, bool Status)
        {
            if (Msg != null && !_isTest)
                Application.Current.Dispatcher.Invoke(delegate
                {
                    var mainView = (HublangView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                    if (mainView != null)
                        mainView.ShowSnackbar(Msg, Status ? "success" : "danger");
                });
        }
    }
}
