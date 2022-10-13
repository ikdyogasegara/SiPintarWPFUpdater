using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.DataMaster;


namespace SiPintar.Commands.Personalia.DataMaster.Pegawai
{
    public class OnSubmitFormCommand : AsyncCommandBase
    {
        private readonly PegawaiViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private string SuccessMessage;
        private new string ErrorMessage;

        public OnSubmitFormCommand(PegawaiViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..
            SuccessMessage = null;
            ErrorMessage = null;

            object dg = null;
            DialogHelper.CloseDialog(_isTest, false, "PersonaliaRootDialog");
            dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, false, "PersonaliaRootDialog", "Mohon tunggu", "sedang memproses tindakan...", "");

            await ProcessDataAsync();
            if (ErrorMessage == null)
            {
                if (_viewModel.FormData.IdPegawai == null)
                {
                    await GetIdPegawaiAsync();
                }

                await ((AsyncCommandBase)_viewModel.OnUploadFileCommand).ExecuteAsync(null);
            }

            DialogHelper.CloseDialog(_isTest, false, "PersonaliaRootDialog", dg);

            if (ErrorMessage != null)
                DialogHelper.ShowSnackbar(_isTest, "danger", ErrorMessage, "personalia");
            else if (SuccessMessage != null)
                DialogHelper.ShowSnackbar(_isTest, "success", SuccessMessage, "personalia");

            await Task.Run(() => ((AsyncCommandBase)_viewModel.OnLoadCommand).ExecuteAsync(null));
            await Task.FromResult(_isTest);
        }

        private async Task ProcessDataAsync()
        {
            Type type = typeof(MasterPegawaiDto);
            PropertyInfo[] properties = type.GetProperties();
            var body = new Dictionary<string, dynamic>();
            foreach (PropertyInfo property in properties)
            {
                if (property.Name != "IdPdam" && property.Name != "IdUserRequest" && property.Name != "Foto" && property.Name != "FotoKtp" && property.Name != "FotoKk" && property.GetValue(_viewModel.FormData) != null)
                {
                    body.Add(property.Name, property.GetValue(_viewModel.FormData));
                }
            }

            RestApiResponse Response = null;
            if (!_viewModel.IsEdit)
            {
                Response = await Task.Run(() => _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/master-pegawai", body));
            }
            else
            {
                Response = await Task.Run(() => _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/master-pegawai", null, body));
            }

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

        private async Task GetIdPegawaiAsync()
        {
            var param = new Dictionary<string, dynamic>
            {
                { "NoPegawai", _viewModel.FormData.NoPegawai },
            };

            if (_isTest)
                param.Add("TestId", _viewModel.FormData.IdPegawai);

            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-pegawai", param));
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    var data = Result.Data.ToObject<ObservableCollection<MasterPegawaiDto>>();

                    if (data.Count > 0)
                        _viewModel.FormData.IdPegawai = data.FirstOrDefault().IdPegawai != null ? data.FirstOrDefault().IdPegawai : 0;
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
