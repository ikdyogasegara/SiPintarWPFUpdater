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
using SiPintar.ViewModels.Billing.Atribut.Tarif;
using SiPintar.Views;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Billing.Atribut.Tarif.Golongan
{
    public class OnSubmitEditCommand : AsyncCommandBase
    {
        private readonly GolonganViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnSubmitEditCommand(GolonganViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            string SuccessMessage = null;
            string ErrorMessage = null;

            _viewModel.IsLoadingForm = true;

            _viewModel.GolonganForm.Bb2 = _viewModel.GolonganForm.Ba1;
            _viewModel.GolonganForm.Bb3 = _viewModel.GolonganForm.Ba2;
            _viewModel.GolonganForm.Bb4 = _viewModel.GolonganForm.Ba3;
            _viewModel.GolonganForm.Bb5 = _viewModel.GolonganForm.Ba4;

            Type type = typeof(MasterGolonganDto);
            PropertyInfo[] properties = type.GetProperties();
            var body = new Dictionary<string, dynamic>();

            foreach (PropertyInfo property in properties)
            {
                if (property.Name != "IdPdam" && property.Name != "IdUserRequest")
                {
                    body.Add(property.Name, property.GetValue(_viewModel.GolonganForm));
                }
            }

            var Response = await Task.Run(() => _restApi.PatchAsync($"/api/{ApiVersion}/master-tarif-golongan", null, body));
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                    SuccessMessage = Response.Data.Ui_msg;
                else
                    ErrorMessage = Response.Data.Ui_msg;
            }
            else
            {
                ErrorMessage = Response.Error.Message;
            }

            await ShowDialogAsync(ErrorMessage, SuccessMessage);

            _viewModel.IsLoadingForm = false;
        }

        [ExcludeFromCodeCoverage]
        public async Task ShowDialogAsync(string ErrorMessage, string SuccessMessage)
        {
            if (!_isTest)
            {
                if (ErrorMessage != null)
                {
                    await DialogHost.Show(new DialogGlobalView("Terjadi Kesalahan", ErrorMessage, "error"), "AtributTarifGolonganDialog");
                }
                else if (SuccessMessage != null)
                {
                    DialogHost.CloseDialogCommand.Execute(null, null);
                    var mainView = (BillingView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                    if (mainView != null)
                        mainView.ShowSnackbar(SuccessMessage, "success");
                    _viewModel.OnRefreshCommand.Execute(null);
                }
            }
        }
    }
}
