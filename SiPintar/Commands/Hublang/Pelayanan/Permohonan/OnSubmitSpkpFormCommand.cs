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
    public class OnSubmitSpkpFormCommand : AsyncCommandBase
    {
        private readonly PermohonanViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitSpkpFormCommand(PermohonanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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

            var response = await Task.Run(() => _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/{_viewModel.EndPointSpkp}", null, param));
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    var dataResponse = result.Data.ToObject<ObservableCollection<PermohonanWpf>>();
                    UpdateRecord(selectedData, dataResponse);
                    selectedData.IsUpdatingKoreksi = false;

                    var nomorSpkp = dataResponse[0].RabWpf.NomorSpkp ?? "-";
                    var nomorSppb = dataResponse[0].RabWpf.NomorSppb ?? "-";
                    await ShowCetakDialogAsync(nomorSpkp, nomorSppb);
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
                selectedData.RabWpf.TanggalSpkp = resultData[0].RabWpf.TanggalSpkp;
                selectedData.RabWpf.NomorSpkp = resultData[0].RabWpf.NomorSpkp ?? "-";
                selectedData.RabWpf.TanggalSppb = resultData[0].RabWpf.TanggalSppb;
                selectedData.RabWpf.NomorSppb = resultData[0].RabWpf.NomorSppb ?? "-";
                selectedData.StatusPermohonanWpf = resultData[0].StatusPermohonanWpf ?? "-";

                if (resultData[0].Pelaksana != null)
                {
                    selectedData.PelaksanaWpf = new List<PermohonanPetugasDto>();
                    foreach (var i in resultData[0].Pelaksana)
                    {
                        selectedData.PelaksanaWpf.Add(new PermohonanPetugasDto { IdPegawai = i.IdPegawai, Divisi = i.Divisi, IdDivisi = i.IdDivisi, NamaPegawai = i.NamaPegawai });
                    }
                }
            }
        }

        [ExcludeFromCodeCoverage]
        private async Task ShowCetakDialogAsync(string nomorSpkp, string nomorSppb)
        {
            if (!_isTest)
            {
                var param = new Dictionary<string, dynamic>()
                {
                    { "IdPermohonan", _viewModel.SelectedData.IdPermohonan }
                };

                var fitur = "SPKP";

                switch (_viewModel.SelectedJenisPelanggan)
                {
                    case "Pelanggan Air":
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganAirSpkp).IsCetak = true;
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganAirSpkp).IsPreview = true;
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganAirSpkp).TemplateName = $"{fitur}PelangganAir_{_viewModel.SelectedTipePermohonanComboBox.KodeTipePermohonan}";
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganAirSpkp).CustomParam = param;

                        await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                            _isTest,
                            false,
                            "PerencanaanRootDialog",
                            $"SPKP-SPPB Berhasil",
                            $"No SPKP-SPPB : {nomorSpkp} | {nomorSppb}",
                            "success",
                            _viewModel.OnCetakCommandPelangganAirSpkp,
                            "Cetak",
                            "Tutup",
                            moduleName: "perencanaan");

                        break;
                    case "Pelanggan Limbah":
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganLimbahSpkp).IsCetak = true;
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganLimbahSpkp).IsPreview = true;
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganLimbahSpkp).TemplateName = $"{fitur}PelangganLimbah_{_viewModel.SelectedTipePermohonanComboBox.KodeTipePermohonan}";
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganLimbahSpkp).CustomParam = param;

                        await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                            _isTest,
                            false,
                            "PerencanaanRootDialog",
                            $"SPKP-SPPB Berhasil",
                            $"No SPKP-SPPB : {nomorSpkp} | {nomorSppb}",
                            "success",
                            _viewModel.OnCetakCommandPelangganLimbahSpkp,
                            "Cetak",
                            "Tutup",
                            moduleName: "perencanaan");

                        break;
                    case "Pelanggan L2T2":
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganLlttSpkp).IsCetak = true;
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganLlttSpkp).IsPreview = true;
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganLlttSpkp).TemplateName = $"{fitur}PelangganLltt_{_viewModel.SelectedTipePermohonanComboBox.KodeTipePermohonan}";
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganLlttSpkp).CustomParam = param;

                        await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                            _isTest,
                            false,
                            "PerencanaanRootDialog",
                            $"SPKP-SPPB Berhasil",
                            $"No SPKP-SPPB : {nomorSpkp} | {nomorSppb}",
                            "success",
                            _viewModel.OnCetakCommandPelangganLlttSpkp,
                            "Cetak",
                            "Tutup",
                            moduleName: "perencanaan");

                        break;
                    case "Non Pelanggan":
                        ((OnCetakCommand)_viewModel.OnCetakCommandNonPelangganSpkp).IsCetak = true;
                        ((OnCetakCommand)_viewModel.OnCetakCommandNonPelangganSpkp).IsPreview = true;
                        ((OnCetakCommand)_viewModel.OnCetakCommandNonPelangganSpkp).TemplateName = $"{fitur}NonPelanggan_{_viewModel.SelectedTipePermohonanComboBox.KodeTipePermohonan}";
                        ((OnCetakCommand)_viewModel.OnCetakCommandNonPelangganSpkp).CustomParam = param;

                        await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                            _isTest,
                            false,
                            "PerencanaanRootDialog",
                            $"SPKP-SPPB Berhasil",
                            $"No SPKP-SPPB : {nomorSpkp} | {nomorSppb}",
                            "success",
                            _viewModel.OnCetakCommandNonPelangganSpkp,
                            "Cetak",
                            "Tutup",
                            moduleName: "perencanaan");

                        break;
                }
            }
        }
    }
}
