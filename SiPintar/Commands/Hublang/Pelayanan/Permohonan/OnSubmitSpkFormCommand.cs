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
    public class OnSubmitSpkFormCommand : AsyncCommandBase
    {
        private readonly PermohonanViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitSpkFormCommand(PermohonanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            object dg = null;
            var selectedData = new PermohonanWpf();

            var body = parameter as Dictionary<string, dynamic>;

            RestApiResponse response;
            if (_viewModel.IsFor == "add")
            {
                dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, false, "PerencanaanRootDialog", "Mohon tunggu", "sedang memproses tindakan...", "");
                response = await _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/{_viewModel.EndPointSpk}", body);
            }
            else
            {
                selectedData = _viewModel.SelectedData;
                selectedData.IsUpdatingKoreksi = true;
                response = await _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/{_viewModel.EndPointSpk}", null, body);
            }

            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    if (_viewModel.IsFor == "add")
                    {
                        var dataResponse = result.Data.ToObject<ObservableCollection<JObject>>();
                        var nomorSpk = dataResponse[0].GetValue("NomorSpk", StringComparison.OrdinalIgnoreCase) ?? "-";
                        _viewModel.FormData.IdPermohonan = Convert.ToInt32(dataResponse[0].GetValue("IdPermohonan", StringComparison.OrdinalIgnoreCase) ?? 0);

                        await UploadFotoAsync();
                        DialogHelper.CloseDialog(_isTest, false, Identifier: "PerencanaanRootDialog", dg);
                        await ShowCetakDialogAsync(nomorSpk.ToString());
                        await ((AsyncCommandBase)_viewModel.OnRefreshCommand).ExecuteAsync(null);
                    }
                    else
                    {
                        UpdateRecord(selectedData, result.Data.ToObject<ObservableCollection<PermohonanWpf>>());
                        await UploadFotoAsync();
                        selectedData.IsUpdatingKoreksi = false;
                        DialogHelper.ShowSnackbar(_isTest, "success", result.Ui_msg, "perencanaan");
                    }
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg, "perencanaan");
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message, "perencanaan");

            selectedData.IsUpdatingKoreksi = false;
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private async Task UploadFotoAsync()
        {
            if (!_isTest && (_viewModel.IsNewFotoBuktiSpk1 == true || _viewModel.IsNewFotoBuktiSpk2 == true || _viewModel.IsNewFotoBuktiSpk3 == true))
            {
                var bodyUpload = new Dictionary<string, dynamic>
                {
                    { "IdPermohonan", _viewModel.FormData.IdPermohonan }
                };

                if (_viewModel.FotoBuktiSpk1Uri != null && !_viewModel.FotoBuktiSpk1Uri.OriginalString.ToLower().Contains("http") && _viewModel.IsNewFotoBuktiSpk1)
                    bodyUpload.Add("file_FotoBukti1", _viewModel.FotoBuktiSpk1Uri.OriginalString);
                if (_viewModel.FotoBuktiSpk2Uri != null && !_viewModel.FotoBuktiSpk2Uri.OriginalString.ToLower().Contains("http") && _viewModel.IsNewFotoBuktiSpk2)
                    bodyUpload.Add("file_FotoBukti2", _viewModel.FotoBuktiSpk2Uri.OriginalString);
                if (_viewModel.FotoBuktiSpk3Uri != null && !_viewModel.FotoBuktiSpk3Uri.OriginalString.ToLower().Contains("http") && _viewModel.IsNewFotoBuktiSpk3)
                    bodyUpload.Add("file_FotoBukti3", _viewModel.FotoBuktiSpk3Uri.OriginalString);

                var responseUpload = await _restApi.UploadAsync($"/api/{_restApi.GetApiVersion()}/{_viewModel.EndPointSpk}-upload-foto", bodyUpload);
                if (!responseUpload.IsError)
                {
                    var resultUpload = responseUpload.Data;
                    if (resultUpload.Status)
                    {
                        DialogHelper.ShowSnackbar(_isTest, "success", "Upload Foto Berhasil", "perencanaan");
                    }
                }
            }
        }

        [ExcludeFromCodeCoverage]
        private void UpdateRecord(PermohonanWpf selectedData, ObservableCollection<PermohonanWpf> resultData)
        {
            if (!_isTest)
            {
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
        private async Task ShowCetakDialogAsync(string nomorSpk)
        {
            if (!_isTest)
            {
                var param = new Dictionary<string, dynamic>()
                {
                    { "IdPermohonan", _viewModel.FormData.IdPermohonan }
                };

                var fitur = "SPK";

                switch (_viewModel.SelectedJenisPelanggan)
                {
                    case "Pelanggan Air":
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganAirSpk).IsCetak = true;
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganAirSpk).IsPreview = true;
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganAirSpk).TemplateName = $"{fitur}PelangganAir_{_viewModel.SelectedTipePermohonanComboBox.KodeTipePermohonan}";
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganAirSpk).CustomParam = param;

                        await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                            _isTest,
                            false,
                            "PerencanaanRootDialog",
                            $"Tambah Data Berhasil",
                            $"No SPK : {nomorSpk}",
                            "success",
                            _viewModel.OnCetakCommandPelangganAirSpk,
                            "Cetak",
                            "Tutup",
                            moduleName: "perencanaan");

                        break;
                    case "Pelanggan Limbah":
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganLimbahSpk).IsCetak = true;
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganLimbahSpk).IsPreview = true;
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganLimbahSpk).TemplateName = $"{fitur}PelangganLimbah_{_viewModel.SelectedTipePermohonanComboBox.KodeTipePermohonan}";
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganLimbahSpk).CustomParam = param;

                        await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                            _isTest,
                            false,
                            "PerencanaanRootDialog",
                            $"Tambah Data Berhasil",
                           $"No SPK : {nomorSpk}",
                            "success",
                            _viewModel.OnCetakCommandPelangganLimbahSpk,
                            "Cetak",
                            "Tutup",
                            moduleName: "perencanaan");

                        break;
                    case "Pelanggan L2T2":
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganLlttSpk).IsCetak = true;
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganLlttSpk).IsPreview = true;
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganLlttSpk).TemplateName = $"{fitur}PelangganLltt_{_viewModel.SelectedTipePermohonanComboBox.KodeTipePermohonan}";
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganLlttSpk).CustomParam = param;

                        await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                            _isTest,
                            false,
                            "PerencanaanRootDialog",
                            $"Tambah Data Berhasil",
                            $"No SPK : {nomorSpk}",
                            "success",
                            _viewModel.OnCetakCommandPelangganLlttSpk,
                            "Cetak",
                            "Tutup",
                            moduleName: "perencanaan");

                        break;
                    case "Non Pelanggan":
                        ((OnCetakCommand)_viewModel.OnCetakCommandNonPelangganSpk).IsCetak = true;
                        ((OnCetakCommand)_viewModel.OnCetakCommandNonPelangganSpk).IsPreview = true;
                        ((OnCetakCommand)_viewModel.OnCetakCommandNonPelangganSpk).TemplateName = $"{fitur}NonPelanggan_{_viewModel.SelectedTipePermohonanComboBox.KodeTipePermohonan}";
                        ((OnCetakCommand)_viewModel.OnCetakCommandNonPelangganSpk).CustomParam = param;

                        await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                            _isTest,
                            false,
                            "PerencanaanRootDialog",
                            $"Tambah Data Berhasil",
                            $"No SPK : {nomorSpk}",
                            "success",
                            _viewModel.OnCetakCommandNonPelangganSpk,
                            "Cetak",
                            "Tutup",
                            moduleName: "perencanaan");

                        break;
                }
            }
        }
    }
}
