using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.GajiPokok;
using SiPintar.Views.Personalia.GajiPokok.Direksi;

namespace SiPintar.Commands.Personalia.GajiPokok.Direksi
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly DireksiViewModel _viewModel;
        private readonly bool _isTest;
        private readonly IRestApiClientModel _restApi;

        public OnOpenEditFormCommand(DireksiViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
            _restApi = restApi;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..

            _viewModel.IsEdit = true;

            if (_viewModel.SelectedData != null)
            {
                await GetTunjanganAsync();
                await GetPotonganAsync();

                _viewModel.FormFlagPersen = _viewModel.SelectedData.FlagPersen;
                _viewModel.FormNominalNonPersen = _viewModel.SelectedData.Jumlah;
                _viewModel.FormNominalPersen = _viewModel.SelectedData.Persentase;
                _viewModel.FormBerdasarkan = _viewModel.SelectedData.Berdasarkan;
                _viewModel.FormPersenDari = _viewModel.SelectedData.PersenDari;

                //_viewModel.TunjanganList.Where(t => t.IdJenisTunjangan == _viewModel.SelectedData.IdJenisTunjangan).ForEach(t => t.IsChecked = true)
                //_viewModel.PotonganList.Where(t => t.IdJenisPotongan == _viewModel.SelectedData.IdJenisPotongan).ForEach(t => t.IsChecked = true)
                //_viewModel.FormIdTunjangan = _viewModel.SelectedData.IdJenisTunjangan
                //_viewModel.FormIdPotongan = _viewModel.SelectedData.IdJenisPotongan
                _viewModel.SelectedDataPegawaiForm = new MasterPegawaiDto
                {
                    IdPegawai = _viewModel.SelectedData.IdPegawai,
                    NoPegawai = _viewModel.SelectedData.NoPegawai,
                    NamaPegawai = _viewModel.SelectedData.NamaPegawai,
                };
                _viewModel.SelectedDataPegawaiRefForm = new MasterPegawaiDto
                {
                    IdPegawai = _viewModel.SelectedData.IdPegawaiRef,
                    NoPegawai = _viewModel.SelectedData.NoPegawaiRef,
                    NamaPegawai = _viewModel.SelectedData.NamaPegawaiRef,
                };
                await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "PersonaliaRootDialog", () => new DialogFormView(_viewModel));
            }
            else
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false,
                    "PersonaliaRootDialog",
                    "Koreksi Gaji Direksi",
                    "Silahkan pilih data",
                    "warning",
                    "Ok",
                    false,
                    "Personalia");

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
