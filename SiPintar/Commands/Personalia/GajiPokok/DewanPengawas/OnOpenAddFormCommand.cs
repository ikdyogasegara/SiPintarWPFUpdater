using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.GajiPokok;
using SiPintar.Views.Personalia.GajiPokok.DewanPengawas;

namespace SiPintar.Commands.Personalia.GajiPokok.DewanPengawas
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly DewanPengawasViewModel _viewModel;
        private readonly bool _isTest;
        private readonly IRestApiClientModel _restApi;

        public OnOpenAddFormCommand(DewanPengawasViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
            _restApi = restApi;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..
            await GetTunjanganAsync();
            await GetPotonganAsync();

            _viewModel.IsEdit = false;
            _viewModel.FormJabatan2 = null;
            _viewModel.FormFlagPersen = false;
            _viewModel.FormNominalNonPersen = 0;
            _viewModel.FormNominalPersen = 0;
            _viewModel.FormBerdasarkan = null;
            _viewModel.FormPersenDari = null;
            _viewModel.FormIdTunjangan = null;
            _viewModel.FormIdPotongan = null;
            _viewModel.SelectedDataPegawaiForm = new MasterPegawaiDto();
            _viewModel.SelectedDataPegawaiRefForm = new MasterPegawaiDto();
            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "PersonaliaRootDialog", () => new DialogFormView(_viewModel));

            await Task.FromResult(_isTest);
        }

        public async Task GetTunjanganAsync()
        {
            _viewModel.IsLoading = true;
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-jenis-tunjangan");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.TunjanganList = Result.Data.ToObject<ObservableCollection<MasterJenisTunjanganWpf>>();
                }
            }
            _viewModel.IsLoading = false;
        }

        public async Task GetPotonganAsync()
        {
            _viewModel.IsLoading = true;
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-jenis-potongan");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.PotonganList = Result.Data.ToObject<ObservableCollection<MasterJenisPotonganWpf>>();
                }
            }
            _viewModel.IsLoading = false;
        }
    }
}
