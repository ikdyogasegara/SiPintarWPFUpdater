using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi;
using SiPintar.Views;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.AnggaranPerputaranUang
{
    public class OnSubmitEditCommand : AsyncCommandBase
    {
        private readonly AnggaranPerputaranUangViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnSubmitEditCommand(AnggaranPerputaranUangViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            string successMessage = null;
            string errorMessage = null;

            _viewModel.IsEdit = true;
            _viewModel.IsLoadingForm = true;

            Type type = typeof(LaporanPerputaranUangDto);
            PropertyInfo[] properties = type.GetProperties();
            var body = new Dictionary<string, dynamic>();

            foreach (PropertyInfo property in properties)
            {
                if (property.Name != "IdPdam" && property.Name != "IdUserRequest")
                {
                    body.Add(property.Name, property.GetValue(_viewModel.AnggaranPerputaranUangForm));
                }
            }

            var Response = await Task.Run(() => _restApi.PatchAsync($"/api/{ApiVersion}/laporan-perputaran-uang", null, body));

            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                    successMessage = Response.Data.Ui_msg;
                else
                    errorMessage = Response.Data.Ui_msg;
            }
            else
            {
                errorMessage = Response.Error.Message;
            }

            await ShowDialogAsync(errorMessage, successMessage);
            _viewModel.IsLoadingForm = false;
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        public async Task ShowDialogAsync(string errorMessage, string successMessage)
        {
            if (!_isTest)
            {
                if (errorMessage != null)
                {
                    await DialogHost.Show(new DialogGlobalView("Terjadi Kesalahan", errorMessage, "error"), "AkuntansiRootDialog");
                }
                else if (successMessage != null)
                {
                    DialogHost.CloseDialogCommand.Execute(null, null);
                    var mainView = (AkuntansiView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                    if (mainView != null)
                        mainView.ShowSnackbar(successMessage, "success");
                    await Task.Run(() => ((AsyncCommandBase)_viewModel.OnRefreshCommand).ExecuteAsync(null));
                }
            }
        }

    }
}
