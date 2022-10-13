using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Commands.Hublang.Pelayanan.SimulasiTarif
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly SimulasiTarifViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(SimulasiTarifViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            object dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, true,
                "HublangRootDialog", "Mohon tunggu", "sedang memuat data...");
            var response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-tarif-golongan", null));
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status && result.Data != null)
                    _viewModel.DaftarGolongan = result.Data.ToObject<ObservableCollection<MasterGolonganDto>>();
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", result.Ui_msg);
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error?.Message);

            response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-tarif-diameter", null));
            if (!response.IsError)
            {
                var Result = response.Data;
                if (Result.Status && Result.Data != null)
                    _viewModel.DaftarDiameter = Result.Data.ToObject<ObservableCollection<MasterDiameterDto>>();
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", Result.Ui_msg);
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error?.Message);

            DialogHelper.CloseDialog(_isTest, true, "HublangRootDialog", dg);

            await Task.FromResult(_isTest);
        }
    }
}
