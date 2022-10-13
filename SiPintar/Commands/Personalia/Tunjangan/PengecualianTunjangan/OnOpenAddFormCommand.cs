using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.Tunjangan;
using SiPintar.Views.Personalia.Tunjangan.PengecualianTunjangan;

namespace SiPintar.Commands.Personalia.Tunjangan.PengecualianTunjangan
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly PengecualianTunjanganViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(PengecualianTunjanganViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
            _restApi = restApi;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..

            _viewModel.IsEdit = false;
            _viewModel.FormFlagRutin = true;
            _viewModel.FormKodePeriode = null;
            _viewModel.FormKeterangan = null;
            _viewModel.SelectedDataPegawai = new MasterPegawaiDto();
            await GetPeriodeAsync();
            await GetJenisTunjanganAsync();
            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "PersonaliaRootDialog", () => new DialogFormView(_viewModel));

            await Task.FromResult(_isTest);
        }

        public async Task GetJenisTunjanganAsync()
        {
            _viewModel.IsLoading = true;
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-jenis-tunjangan");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.JenisTunjanganListForm = Result.Data.ToObject<ObservableCollection<PengecualianTunjanganDetailWpf>>();
                }
            }
            _viewModel.IsLoading = false;
        }

        public async Task GetPeriodeAsync()
        {
            _viewModel.IsLoading = true;
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-periode-rekening");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.PeriodeListForm = Result.Data.ToObject<ObservableCollection<MasterPeriodeDto>>();
                }
            }
            _viewModel.IsLoading = false;
        }
    }
}
