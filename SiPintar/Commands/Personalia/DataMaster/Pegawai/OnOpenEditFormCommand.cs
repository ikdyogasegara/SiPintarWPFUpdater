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
using SiPintar.ViewModels.Personalia.DataMaster;
using SiPintar.Views.Personalia.DataMaster.Pegawai;

namespace SiPintar.Commands.Personalia.DataMaster.Pegawai
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly PegawaiViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(PegawaiViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..
            _viewModel.IsEdit = true;

            if (_viewModel.SelectedData != null)
            {
                if (_viewModel.SelectedData.IdStatusPegawai == 1 || _viewModel.SelectedData.IdStatusPegawai == 2)
                    _viewModel.JenisFormPegawai = "Pegawai";
                else if (_viewModel.SelectedData.IdStatusPegawai == 3 || _viewModel.SelectedData.IdStatusPegawai == 4)
                    _viewModel.JenisFormPegawai = "NonPegawai";

                await GetAgamaAsync();
                await GetGolonganDarahAsync();
                await GetPendidikanAsync();
                await GetStatusAsync();
                await GetJabatanAsync();
                await GetDepartemenAsync();
                await GetDivisiAsync();
                await GetAreaKerjaAsync();
                await GetGolonganAsync();

                _viewModel.FormCurrentPage = 1;
                _viewModel.FormData = (MasterPegawaiDto)_viewModel.SelectedData.Clone();

                await GetFotoAsync();

                if (_viewModel.JenisFormPegawai == "Pegawai")
                    await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "PersonaliaRootDialog", () => new DialogPegawaiFormView(_viewModel));
                else if (_viewModel.JenisFormPegawai == "NonPegawai")
                    await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "PersonaliaRootDialog", () => new DialogNonPegawaiFormView(_viewModel));
            }
            else
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false,
                    "PersonaliaRootDialog",
                    "Koreksi Data Pegawai",
                    "Silahkan pilih data",
                    "warning",
                    "Ok",
                    false,
                    "Personalia");

            await Task.FromResult(_isTest);
        }

        public async Task GetAgamaAsync()
        {
            _viewModel.IsLoading = true;
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-agama");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.AgamaList = Result.Data.ToObject<ObservableCollection<MasterAgamaDto>>();
                }
            }
            _viewModel.IsLoading = false;
        }

        public async Task GetGolonganDarahAsync()
        {
            _viewModel.IsLoading = true;
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-golongan-darah");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.GolonganDarahList = Result.Data.ToObject<ObservableCollection<MasterGolonganDarahDto>>();
                }
            }
            _viewModel.IsLoading = false;
        }

        public async Task GetPendidikanAsync()
        {
            _viewModel.IsLoading = true;
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-pendidikan");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.PendidikanList = Result.Data.ToObject<ObservableCollection<MasterPendidikanDto>>();
                }
            }
            _viewModel.IsLoading = false;
        }

        public async Task GetStatusAsync()
        {
            _viewModel.IsLoading = true;
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-status-pegawai");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.StatusPegawaiList = Result.Data.ToObject<ObservableCollection<MasterStatusPegawaiDto>>();
                    if (_viewModel.JenisFormPegawai == "Pegawai")
                    {
                        _viewModel.StatusPegawaiCopyList = new ObservableCollection<MasterStatusPegawaiDto>(
                            Result.Data.ToObject<ObservableCollection<MasterStatusPegawaiDto>>().Where(s => s.IdStatusPegawai == 1 || s.IdStatusPegawai == 2)
                        );
                    }
                    else if (_viewModel.JenisFormPegawai == "NonPegawai")
                    {
                        _viewModel.StatusPegawaiCopyList = new ObservableCollection<MasterStatusPegawaiDto>(
                            Result.Data.ToObject<ObservableCollection<MasterStatusPegawaiDto>>().Where(s => s.IdStatusPegawai == 3 || s.IdStatusPegawai == 4)
                        );
                    }
                }
            }
            _viewModel.IsLoading = false;
        }

        public async Task GetJabatanAsync()
        {
            _viewModel.IsLoading = true;
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-jabatan");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.JabatanList = Result.Data.ToObject<ObservableCollection<MasterJabatanDto>>();
                }
            }
            _viewModel.IsLoading = false;
        }

        public async Task GetDepartemenAsync()
        {
            _viewModel.IsLoading = true;
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-departemen");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.DepartemenList = Result.Data.ToObject<ObservableCollection<MasterDepartemenDto>>();
                }
            }
            _viewModel.IsLoading = false;
        }

        public async Task GetDivisiAsync()
        {
            _viewModel.IsLoading = true;
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-divisi");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.DivisiList = Result.Data.ToObject<ObservableCollection<MasterDivisiDto>>();
                    _viewModel.DivisiCopyList = Result.Data.ToObject<ObservableCollection<MasterDivisiDto>>();
                }
            }
            _viewModel.IsLoading = false;
        }

        public async Task GetAreaKerjaAsync()
        {
            _viewModel.IsLoading = true;
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-area-kerja");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.AreaKerjaList = Result.Data.ToObject<ObservableCollection<MasterAreaKerjaDto>>();
                }
            }
            _viewModel.IsLoading = false;
        }

        public async Task GetGolonganAsync()
        {
            _viewModel.IsLoading = true;
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-golongan-pegawai");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.GolonganPegawaiList = Result.Data.ToObject<ObservableCollection<MasterGolonganPegawaiDto>>();
                }
            }
            _viewModel.IsLoading = false;
        }

        private async Task GetFotoAsync()
        {
            if (_viewModel.FormData.IdPegawai == null)
                return;

            string ErrorMessage = null;
            var detail = _viewModel.FormData;

            var param = new Dictionary<string, dynamic>
            {
                { "IdPegawai" , _viewModel.FormData.IdPegawai }
            };

            if (_isTest)
                param.Add("TestId", _viewModel.FormData.IdPegawai);

            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-pegawai-link-foto", param);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    var data = Result.Data.ToObject<JArray>();

                    detail.Foto = ((JObject)data[0]).GetValue("fotoPegawai")?.ToString();
                    detail.FotoKtp = ((JObject)data[0]).GetValue("fotoKtp")?.ToString();
                    detail.FotoKk = ((JObject)data[0]).GetValue("fotoKk")?.ToString();

                    _viewModel.FormData = detail;

                    _viewModel.FotoPegawaiUri = !string.IsNullOrEmpty(detail.Foto) ? new Uri(detail.Foto, UriKind.Absolute) : null;
                    _viewModel.FotoKtpUri = !string.IsNullOrEmpty(detail.FotoKtp) ? new Uri(detail.FotoKtp, UriKind.Absolute) : null;
                    _viewModel.FotoKkUri = !string.IsNullOrEmpty(detail.FotoKk) ? new Uri(detail.FotoKk, UriKind.Absolute) : null;

                    _viewModel.IsFotoKtpChecked = _viewModel.FotoKtpUri != null;
                    _viewModel.IsFotoKkChecked = _viewModel.FotoKkUri != null;
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
