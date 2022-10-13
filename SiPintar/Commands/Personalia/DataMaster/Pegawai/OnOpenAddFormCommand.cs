using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.DataMaster;
using SiPintar.Views.Personalia.DataMaster.Pegawai;

namespace SiPintar.Commands.Personalia.DataMaster.Pegawai
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly PegawaiViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(PegawaiViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..
            _viewModel.JenisFormPegawai = parameter as string;

            _viewModel.IsEdit = false;
            _viewModel.IsLoading = true;
            _viewModel.FormCurrentPage = 1;
            _viewModel.FormData = new MasterPegawaiDto { FlagKawin = false, IdJenisKelamin = 1 };
            _viewModel.FotoPegawaiUri = null;
            _viewModel.FotoKtpUri = null;
            _viewModel.FotoKkUri = null;
            _viewModel.IsFotoKtpChecked = _viewModel.FotoKtpUri != null;
            _viewModel.IsFotoKkChecked = _viewModel.FotoKkUri != null;

            await GetAgamaAsync();
            await GetGolonganDarahAsync();
            await GetPendidikanAsync();
            await GetStatusAsync();
            await GetJabatanAsync();
            await GetDepartemenAsync();
            await GetDivisiAsync();
            await GetAreaKerjaAsync();
            await GetGolonganAsync();

            if (_viewModel.JenisFormPegawai == "Pegawai")
                await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "PersonaliaRootDialog", () => new DialogPegawaiFormView(_viewModel));
            else if (_viewModel.JenisFormPegawai == "NonPegawai")
                await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "PersonaliaRootDialog", () => new DialogNonPegawaiFormView(_viewModel));
            _viewModel.IsLoading = false;

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
    }
}
