using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Atribut;

namespace SiPintar.Commands.Billing.Atribut.PetugasBaca
{
    public class OnSubmitAddFormCommand : AsyncCommandBase
    {
        private readonly PetugasBacaViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        private string SuccessMessage;
        private new string ErrorMessage;

        private int IdPetugasBaca;

        public OnSubmitAddFormCommand(PetugasBacaViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            SuccessMessage = null;
            ErrorMessage = null;

            _viewModel.IsLoadingForm = true;

            DialogHelper.ShowLoading(_isTest, "BillingRootDialog");

            await ProsesDataAsync();
            if (ErrorMessage == null)
            {
                await GetIdPetugasBacaAsync();

                if (IdPetugasBaca != 0)
                {
                    _viewModel.FormData.IdPetugasBaca = IdPetugasBaca;
                    await ((AsyncCommandBase)_viewModel.UploadFileCommand).ExecuteAsync(null);
                }
            }

            if (App.OpenedWindow.ContainsKey("billing"))
                DialogHelper.ShowSuccessErrorDialog(ErrorMessage, SuccessMessage, _isTest, "BillingRootDialog", App.OpenedWindow["billing"], true, _viewModel.OnRefreshCommand);

            _viewModel.IsLoadingForm = false;

            await Task.FromResult(_isTest);
        }

        private async Task ProsesDataAsync()
        {
            var body = new Dictionary<string, dynamic>()
            {
                { "Password", _viewModel.PasswordForm }
            };

            Type type = typeof(ParamMasterPetugasBacaDto);
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property.Name != "IdPdam" && property.Name != "IdUserRequest" && property.Name != "Password")
                {
                    body.Add(property.Name, property.GetValue(_viewModel.FormData));
                }
            }

            if (_isTest)
                body.Add("TestId", _viewModel.TestId);

            var Response = await Task.Run(() => _restApi.PostAsync($"/api/{ApiVersion}/master-petugas-baca", body));
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
        }

        private async Task GetIdPetugasBacaAsync()
        {
            if (_viewModel.FormData.TglLahir == null)
                return;

            var param = new Dictionary<string, dynamic>
            {
                { "TglLahirMulai", _viewModel.FormData.TglLahir },
                { "TglLahirSampaiDengan", _viewModel.FormData.TglLahir },
                { "PageSize" , 10000 },
                { "CurrentPage" , 1 },
                { "FlagHapus" , false }
            };

            if (_isTest)
                param.Add("TestId", _viewModel.TestId);

            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{ApiVersion}/master-petugas-baca", param));
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    var data = Result.Data.ToObject<ObservableCollection<MasterPetugasBacaDto>>();

                    if (data.Count > 0)
                        IdPetugasBaca = data[^1].IdPetugasBaca != null ? (int)data[^1].IdPetugasBaca : 0;
                }
                else
                    ErrorMessage = Response.Data.Ui_msg;
            }
            else
            {
                ErrorMessage = Response.Error.Message;
            }
        }
    }
}
