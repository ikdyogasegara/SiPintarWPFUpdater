using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan;


namespace SiPintar.Commands.Hublang.Pelayanan.RotasiMeter
{
    public class OnSubmitFormCommand : AsyncCommandBase
    {
        private readonly RotasiMeterViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitFormCommand(RotasiMeterViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var selectedData = new JadwalGantiMeterWpf();

            var body = parameter as Dictionary<string, dynamic>;

            RestApiResponse response;
            if (_viewModel.IsFor == "add")
            {
                _ = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, false, "DistribusiRootDialog", "Mohon tunggu", "sedang memproses tindakan...", "");
                response = await _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/jadwal-ganti-meter", body);
            }
            else
            {
                selectedData = _viewModel.SelectedData;
                selectedData.IsUpdatingKoreksi = true;
                response = await _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/jadwal-ganti-meter", null, body);
            }

            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    if (_viewModel.IsFor == "add")
                    {
                        _viewModel.IsExists = result.Ui_msg == "Data yang sama sudah ada dan belum lebih dari 60 hari !";

                        if (_viewModel.IsExists)
                        {
                            OpenDialogWarning();
                        }
                        else
                        {
                            await ((AsyncCommandBase)_viewModel.OnRefreshCommand).ExecuteAsync(null);
                        }
                    }
                    else
                    {
                        UpdateRecord(selectedData, result.Data.ToObject<ObservableCollection<JadwalGantiMeterWpf>>());
                        selectedData.IsUpdatingKoreksi = false;
                        DialogHelper.ShowSnackbar(_isTest, "success", result.Ui_msg, "distribusi");
                    }
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg, "distribusi");
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message, "distribusi");

            selectedData.IsUpdatingKoreksi = false;
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void UpdateRecord(JadwalGantiMeterWpf selectedData, ObservableCollection<JadwalGantiMeterWpf> resultData)
        {
            if (!_isTest)
            {
                selectedData.LatitudeWpf = resultData[0].LatitudeWpf;
                selectedData.LongitudeWpf = resultData[0].LongitudeWpf;
            }
        }

        [ExcludeFromCodeCoverage]
        private void OpenDialogWarning()
        {
            if (!_isTest)
            {
                _ = DialogHelper.ShowDialogGlobalViewAsync(
                    false,
                    true,
                    "DistribusiRootDialog",
                    "Data Sudah Ada",
                    $"Data yang sama sudah ada dan belum lebih dari 60 hari !",
                    "warning",
                    moduleName: "distribusi");
            }
        }
    }
}
