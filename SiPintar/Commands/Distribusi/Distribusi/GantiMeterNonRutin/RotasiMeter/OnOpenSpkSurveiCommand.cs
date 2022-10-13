using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Distribusi.Distribusi;
using SiPintar.ViewModels.Distribusi.Distribusi.GantiMeterNonRutin;
using SiPintar.Views.Distribusi.Distribusi.GantiMeterNonRutin.RotasiMeter;

namespace SiPintar.Commands.Distribusi.Distribusi.GantiMeterNonRutin.RotasiMeter
{
    public class OnOpenSpkSurveiCommand : AsyncCommandBase
    {
        private readonly RotasiMeterNonRutinViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnOpenSpkSurveiCommand(RotasiMeterNonRutinViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _ = GetPetugasAsync();
            _viewModel.FormPetugasTambahan.Clear();
            _viewModel.DataWpfList = new ObservableCollection<GantimeterWpf>();
            foreach (var item in _viewModel.DataList)
            {
                _viewModel.DataWpfList.Add(new GantimeterWpf()
                {
                    IdPelangganAir = item.IdPelangganAir,
                    Tahun = item.Tahun,
                    NoSamb = item.NoSamb,
                    Nama = item.Nama,
                    IsSelected = false
                });
            }

            _viewModel.IsAllSelected = _viewModel.DataWpfList.Any();

            _ = DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "DistribusiRootDialog", GetInstance);
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

        private object GetInstance() => new DialogFormSpkRotasiMeterView(_viewModel);
    }
}
