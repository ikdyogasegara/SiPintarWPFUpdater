using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.DataMaster;
using SiPintar.Views.Personalia.DataMaster.Keluarga;

namespace SiPintar.Commands.Personalia.DataMaster.Keluarga
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly KeluargaViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(KeluargaViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..

            _viewModel.IsEdit = false;
            _viewModel.AnggotaKeluargaForm = new MasterKeluargaDto();
            await GetPekerjaanAsync();
            await GetJenisKelaminAsync();
            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "PersonaliaInnerDialog", () => new DialogFormView(_viewModel));

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
