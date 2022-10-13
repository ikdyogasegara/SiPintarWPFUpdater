using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.DataMaster;
using SiPintar.Views.Personalia.DataMaster.Keluarga;

namespace SiPintar.Commands.Personalia.DataMaster.Keluarga
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly KeluargaViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(KeluargaViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..

            if (_viewModel.SelectedAnggotaKeluarga != null)
            {
                _viewModel.IsEdit = true;
                await GetPekerjaanAsync();
                await GetJenisKelaminAsync();
                _viewModel.AnggotaKeluargaForm = (MasterKeluargaDto)_viewModel.SelectedAnggotaKeluarga.Clone();
                await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "PersonaliaInnerDialog", () => new DialogFormView(_viewModel));
            }
            else
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false,
                    "PersonaliaInnerDialog",
                    "Koreksi Data Keluarga",
                    "Silahkan pilih data",
                    "warning",
                    "Ok",
                    false,
                    "Personalia");

            await Task.FromResult(_isTest);
        }

        public async Task GetPekerjaanAsync()
        {
            _viewModel.IsLoading = true;
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-pekerjaan");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.PekerjaanList = Result.Data.ToObject<ObservableCollection<MasterPekerjaanDto>>();
                }
            }
            _viewModel.IsLoading = false;
        }

        public async Task GetJenisKelaminAsync()
        {
            _viewModel.IsLoading = true;
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-jenis-kelamin");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.JenisKelaminList = Result.Data.ToObject<ObservableCollection<MasterJenisKelaminDto>>();
                }
            }
            _viewModel.IsLoading = false;
        }
    }
}
