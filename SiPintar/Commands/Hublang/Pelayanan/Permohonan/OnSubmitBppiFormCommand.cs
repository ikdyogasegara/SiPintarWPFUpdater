using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using Newtonsoft.Json.Linq;
using SiPintar.Commands.Global;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Commands.Hublang.Pelayanan.Permohonan
{
    public class OnSubmitBppiFormCommand : AsyncCommandBase
    {
        private readonly PermohonanViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitBppiFormCommand(PermohonanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            DialogHelper.CloseDialog(_isTest, false, "PerencanaanRootDialog");

            var param = parameter as Dictionary<string, dynamic>;
            var selectedData = _viewModel.SelectedData;
            selectedData.IsUpdatingKoreksi = true;

            var response = await Task.Run(() => _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/{_viewModel.EndPointBppi}", null, param));
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    var dataResponse = result.Data.ToObject<ObservableCollection<PermohonanWpf>>();
                    UpdateRecord(selectedData, dataResponse);
                    selectedData.IsUpdatingKoreksi = false;

                    var nomorBppi = dataResponse[0].RabWpf.NomorBppi ?? "-";
                    await ShowCetakDialogAsync(nomorBppi);
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", result.Ui_msg, "perencanaan");
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error?.Message, "perencanaan");

            selectedData.IsUpdatingKoreksi = false;
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void UpdateRecord(PermohonanWpf selectedData, ObservableCollection<PermohonanWpf> resultData)
        {
            if (!_isTest)
            {
                selectedData.RabWpf.TanggalBppi = resultData[0].RabWpf.TanggalBppi;
                selectedData.RabWpf.NomorBppi = resultData[0].RabWpf.NomorBppi ?? "-";
                selectedData.StatusPermohonanWpf = resultData[0].StatusPermohonanWpf ?? "-";
            }
        }

        [ExcludeFromCodeCoverage]
        private async Task ShowCetakDialogAsync(string nomorBppi)
        {
            if (!_isTest)
            {
                var param = new Dictionary<string, dynamic>()
                {
                    { "IdPermohonan", _viewModel.SelectedData.IdPermohonan }
                };

                var fitur = "BPPI";

                switch (_viewModel.SelectedJenisPelanggan)
                {
                    case "Pelanggan Air":
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganAirBppi).IsCetak = true;
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganAirBppi).IsPreview = true;
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganAirBppi).TemplateName = $"{fitur}PelangganAir_{_viewModel.SelectedTipePermohonanComboBox.KodeTipePermohonan}";
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganAirBppi).CustomParam = param;

                        await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                            _isTest,
                            false,
                            "PerencanaanRootDialog",
                            $"Buat BPPI Berhasil",
                            $"No BPPI : {nomorBppi}",
                            "success",
                            _viewModel.OnCetakCommandPelangganAirBppi,
                            "Cetak",
                            "Tutup",
                            moduleName: "perencanaan");

                        break;
                    case "Pelanggan Limbah":
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganLimbahBppi).IsCetak = true;
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganLimbahBppi).IsPreview = true;
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganLimbahBppi).TemplateName = $"{fitur}PelangganLimbah_{_viewModel.SelectedTipePermohonanComboBox.KodeTipePermohonan}";
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganLimbahBppi).CustomParam = param;

                        await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                            _isTest,
                            false,
                            "PerencanaanRootDialog",
                            $"Buat BPPI Berhasil",
                            $"No BPPI : {nomorBppi}",
                            "success",
                            _viewModel.OnCetakCommandPelangganLimbahBppi,
                            "Cetak",
                            "Tutup",
                            moduleName: "perencanaan");

                        break;
                    case "Pelanggan L2T2":
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganLlttBppi).IsCetak = true;
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganLlttBppi).IsPreview = true;
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganLlttBppi).TemplateName = $"{fitur}PelangganLltt_{_viewModel.SelectedTipePermohonanComboBox.KodeTipePermohonan}";
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganLlttBppi).CustomParam = param;

                        await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                            _isTest,
                            false,
                            "PerencanaanRootDialog",
                            $"Buat BPPI Berhasil",
                            $"No BPPI : {nomorBppi}",
                            "success",
                            _viewModel.OnCetakCommandPelangganLlttBppi,
                            "Cetak",
                            "Tutup",
                            moduleName: "perencanaan");

                        break;
                    case "Non Pelanggan":
                        ((OnCetakCommand)_viewModel.OnCetakCommandNonPelangganBppi).IsCetak = true;
                        ((OnCetakCommand)_viewModel.OnCetakCommandNonPelangganBppi).IsPreview = true;
                        ((OnCetakCommand)_viewModel.OnCetakCommandNonPelangganBppi).TemplateName = $"{fitur}NonPelanggan_{_viewModel.SelectedTipePermohonanComboBox.KodeTipePermohonan}";
                        ((OnCetakCommand)_viewModel.OnCetakCommandNonPelangganBppi).CustomParam = param;

                        await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                            _isTest,
                            false,
                            "PerencanaanRootDialog",
                            $"Buat BPPI Berhasil",
                            $"No BPPI : {nomorBppi}",
                            "success",
                            _viewModel.OnCetakCommandNonPelangganBppi,
                            "Cetak",
                            "Tutup",
                            moduleName: "perencanaan");

                        break;
                }
            }
        }
    }
}
