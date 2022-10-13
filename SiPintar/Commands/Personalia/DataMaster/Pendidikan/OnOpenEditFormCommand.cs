using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.DataMaster;
using SiPintar.Views.Personalia.DataMaster.Pendidikan;

namespace SiPintar.Commands.Personalia.DataMaster.Pendidikan
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly PendidikanViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(PendidikanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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
                _viewModel.FormPendidikan = _viewModel.SelectedData.Pendidikan;
                _viewModel.FormUrutan = _viewModel.SelectedData.Urutan;
                _viewModel.FormIdGolonganPegawaiMin = _viewModel.SelectedData.IdGolonganPegawaiMin;
                _viewModel.FormIdGolonganPegawaiMax = _viewModel.SelectedData.IdGolonganPegawaiMax;
                _viewModel.UrutanList = new ObservableCollection<int>(Enumerable.Range(1, 100).ToList());
                await GetGolonganAsync();
                await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "PersonaliaRootDialog", () => new DialogFormView(_viewModel));
            }
            else
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false,
                    "PersonaliaRootDialog",
                    "Koreksi Data Pendidikan",
                    "Silahkan pilih data",
                    "warning",
                    "Ok",
                    false,
                    "Personalia");

            await Task.FromResult(_isTest);
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
                    _viewModel.GolonganList = Result.Data.ToObject<ObservableCollection<MasterGolonganPegawaiDto>>();
                }
            }
            _viewModel.IsLoading = false;
        }
    }
}
