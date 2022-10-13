using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using Newtonsoft.Json.Linq;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.Kepegawaian;
using SiPintar.Views.Personalia.Kepegawaian.Pensiun;

namespace SiPintar.Commands.Personalia.Kepegawaian.Pensiun
{
    public class OnOpenVerifikasiFormCommand : AsyncCommandBase
    {
        private readonly PensiunViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnOpenVerifikasiFormCommand(PensiunViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
            _restApi = restApi;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedData != null)
            {
                await GetJenisPensiunAsync();
                await GetMkgPegawaiAsync();
                await GetFotoAsync();

                _viewModel.FormJenisPensiun = null;
                _viewModel.FormSk1 = null;
                _viewModel.FormSk2 = null;
                _viewModel.FormSk3 = null;
                _viewModel.FormSk4 = null;
                _viewModel.FormSk5 = null;
                _viewModel.FormTgl = DateTime.Today;

                await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "PersonaliaRootDialog", () => new DialogFormView(_viewModel));
            }
            else
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false,
                    "PersonaliaRootDialog",
                    "Verifikasi Pensiun",
                    "Silahkan pilih data",
                    "warning",
                    "Ok",
                    false,
                    "Personalia");

            await Task.FromResult(_isTest);
        }

        public async Task GetJenisPensiunAsync()
        {
            _viewModel.IsLoading = true;
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-jenis-pensiun");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.JenisPensiunFormList = Result.Data.ToObject<ObservableCollection<MasterJenisPensiunDto>>();
                }
            }
            _viewModel.IsLoading = false;
        }

        public async Task GetMkgPegawaiAsync()
        {
            _viewModel.IsLoading = true;
            var param = new Dictionary<string, dynamic> { { "IdPegawai", _viewModel.SelectedData.IdPegawai } };
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-pegawai-mkg", param);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.MasaKerjaPegawai = Result.Data.ToObject<ObservableCollection<MasterPegawaiMkgDto>>()?.FirstOrDefault();
                }
            }
            _viewModel.IsLoading = false;
        }

        private async Task GetFotoAsync()
        {
            if (_viewModel.SelectedData.IdPegawai == null)
                return;

            string ErrorMessage = null;
            var selectedData = _viewModel.SelectedData;

            var param = new Dictionary<string, dynamic>
            {
                { "IdPegawai" , _viewModel.SelectedData.IdPegawai }
            };

            if (_isTest)
                param.Add("TestId", _viewModel.SelectedData.IdPegawai);

            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-pegawai-link-foto", param);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    var data = Result.Data.ToObject<JArray>();

                    selectedData.Foto = ((JObject)data[0]).GetValue("fotoPegawai")?.ToString();

                    _viewModel.SelectedData = selectedData;

                    _viewModel.FotoPegawaiUri = !string.IsNullOrEmpty(selectedData.Foto) ? new Uri(selectedData.Foto, UriKind.Absolute) : null;
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

            if (ErrorMessage != null)
                Debug.WriteLine(ErrorMessage);
        }
    }
}
