using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Distribusi.Distribusi.GantiMeterNonRutin;
using SiPintar.Views.Distribusi.Distribusi.GantiMeterNonRutin.RotasiMeter;

namespace SiPintar.Commands.Distribusi.Distribusi.GantiMeterNonRutin.RotasiMeter
{
    public class OnOpenBeritaAcaraCommand : AsyncCommandBase
    {
        private readonly RotasiMeterNonRutinViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnOpenBeritaAcaraCommand(RotasiMeterNonRutinViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _ = GetPetugasAsync();
            _viewModel.FormPetugasTambahan.Clear();


            var param = new Dictionary<string, dynamic>
            {
                { "IdPelangganAir" , _viewModel.SelectedData.IdPelangganAir },
            };
            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-pelanggan-air-detail", param));
            _viewModel.Parent.PelangganDetail = new ObservableCollection<MasterPelangganAirDetailDto>();
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status && Result.Data != null)
                {
                    _viewModel.Parent.PelangganDetail = Result.Data.ToObject<ObservableCollection<MasterPelangganAirDetailDto>>();
                }
            }

            _viewModel.NoHp = _viewModel.Parent.PelangganDetail.FirstOrDefault()?.NoHp;

            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "DistribusiRootDialog", GetInstance);
            await Task.FromResult(_isTest);
        }
        public async Task GetPetugasAsync()
        {
            var response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-pegawai", null));
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status && result.Data != null)
                    _viewModel.PetugasListForm = result.Data.ToObject<ObservableCollection<MasterPegawaiDto>>();
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg);
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message);
        }


        private object GetInstance() => new DialogFormBaRotasiMeterView(_viewModel);
    }
}
