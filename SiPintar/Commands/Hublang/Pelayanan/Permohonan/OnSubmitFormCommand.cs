using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using Newtonsoft.Json.Linq;
using SiPintar.Commands.Global;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan;


namespace SiPintar.Commands.Hublang.Pelayanan.Permohonan
{
    public class OnSubmitFormCommand : AsyncCommandBase
    {
        private readonly PermohonanViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitFormCommand(PermohonanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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
                dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, false, "HublangRootDialog", "Mohon tunggu", "sedang memproses tindakan...", "");
                response = await _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/{_viewModel.EndPoint}", body);
            }
            else
            {
                selectedData = _viewModel.SelectedData;
                selectedData.IsUpdatingKoreksi = true;
                response = await _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/{_viewModel.EndPoint}", null, body);
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
                            DialogHelper.CloseDialog(_isTest, false, Identifier: "HublangRootDialog", dg);
                            OpenDialogWarning();
                        }
                        else
                        {
                            var dataResponse = result.Data.ToObject<ObservableCollection<JObject>>();
                            var nomorPermohonan = dataResponse[0].GetValue("NomorPermohonan", StringComparison.OrdinalIgnoreCase) ?? "-";
                            _viewModel.FormData.IdPermohonan = Convert.ToInt32(dataResponse[0].GetValue("IdPermohonan", StringComparison.OrdinalIgnoreCase) ?? 0);

                            await UploadFotoAsync();
                            DialogHelper.CloseDialog(_isTest, false, Identifier: "HublangRootDialog", dg);
                            await ShowCetakDialogAsync(nomorPermohonan.ToString());
                            await ((AsyncCommandBase)_viewModel.OnRefreshCommand).ExecuteAsync(null);
                        }
                    }
                    else
                    {
                        UpdateRecord(selectedData, result.Data.ToObject<ObservableCollection<PermohonanWpf>>());
                        await UploadFotoAsync();

                        selectedData.IsUpdatingKoreksi = false;
                        DialogHelper.ShowSnackbar(_isTest, "success", result.Ui_msg, "hublang");
                    }
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg, "hublang");
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message, "hublang");

            selectedData.IsUpdatingKoreksi = false;
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private async Task UploadFotoAsync()
        {
            if (!_isTest && (_viewModel.IsNewFotoBukti1 == true || _viewModel.IsNewFotoBukti2 == true || _viewModel.IsNewFotoBukti3 == true))
            {
                var bodyUpload = new Dictionary<string, dynamic>
                {
                    { "IdPermohonan", _viewModel.FormData.IdPermohonan }
                };

                if (_viewModel.FotoBukti1Uri != null && !_viewModel.FotoBukti1Uri.OriginalString.ToLower().Contains("http") && _viewModel.IsNewFotoBukti1)
                    bodyUpload.Add("file_FotoBukti1", _viewModel.FotoBukti1Uri.OriginalString);
                if (_viewModel.FotoBukti2Uri != null && !_viewModel.FotoBukti2Uri.OriginalString.ToLower().Contains("http") && _viewModel.IsNewFotoBukti2)
                    bodyUpload.Add("file_FotoBukti2", _viewModel.FotoBukti2Uri.OriginalString);
                if (_viewModel.FotoBukti3Uri != null && !_viewModel.FotoBukti3Uri.OriginalString.ToLower().Contains("http") && _viewModel.IsNewFotoBukti3)
                    bodyUpload.Add("file_FotoBukti3", _viewModel.FotoBukti3Uri.OriginalString);

                var responseUpload = await _restApi.UploadAsync($"/api/{_restApi.GetApiVersion()}/{_viewModel.EndPoint}-upload-foto", bodyUpload);
                if (!responseUpload.IsError)
                {
                    var resultUpload = responseUpload.Data;
                    if (resultUpload.Status)
                    {
                        DialogHelper.ShowSnackbar(_isTest, "success", "Upload Foto Berhasil", "hublang");
                    }
                }
            }
        }

        [ExcludeFromCodeCoverage]
        private void UpdateRecord(PermohonanWpf selectedData, ObservableCollection<PermohonanWpf> resultData)
        {
            if (!_isTest)
            {
                selectedData.KeteranganWpf = resultData[0].KeteranganWpf;
                selectedData.LatitudeWpf = resultData[0].LatitudeWpf;
                selectedData.LongitudeWpf = resultData[0].LongitudeWpf;
                selectedData.AlamatMapWpf = resultData[0].AlamatMapWpf;

                selectedData.ParameterWpf = new List<PermohonanDetailDto>();
                foreach (var i in resultData[0].ParameterWpf)
                {
                    selectedData.ParameterWpf.Add(new PermohonanDetailDto { Parameter = i.Parameter, TipeData = i.TipeData, Value = i.Value });
                }
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
                    "HublangRootDialog",
                    "Data Sudah Ada",
                    $"Data yang sama sudah ada dan belum lebih dari 60 hari !",
                    "warning",
                    moduleName: "hublang");
            }
        }

        [ExcludeFromCodeCoverage]
        private async Task ShowCetakDialogAsync(string nomorPermohonan)
        {
            if (!_isTest)
            {
                var param = new Dictionary<string, dynamic>()
                {
                    { "IdPermohonan", _viewModel.FormData.IdPermohonan }
                };

                var fitur = "Permohonan";

                switch (_viewModel.SelectedJenisPelanggan)
                {
                    case "Pelanggan Air":
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganAir).IsCetak = true;
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganAir).IsPreview = true;
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganAir).TemplateName = $"{fitur}PelangganAir_{_viewModel.SelectedTipePermohonanComboBox.KodeTipePermohonan}";
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganAir).CustomParam = param;

                        await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                            _isTest,
                            false,
                            "HublangRootDialog",
                            $"Tambah Data Berhasil",
                            $"No Registrasi : {nomorPermohonan}",
                            "success",
                            _viewModel.OnCetakCommandPelangganAir,
                            "Cetak",
                            "Tutup",
                            moduleName: "hublang");

                        break;
                    case "Pelanggan Limbah":
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganLimbah).IsCetak = true;
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganLimbah).IsPreview = true;
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganLimbah).TemplateName = $"{fitur}PelangganLimbah_{_viewModel.SelectedTipePermohonanComboBox.KodeTipePermohonan}";
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganLimbah).CustomParam = param;

                        await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                            _isTest,
                            false,
                            "HublangRootDialog",
                            $"Tambah Data Berhasil",
                            $"No Registrasi : {nomorPermohonan}",
                            "success",
                            _viewModel.OnCetakCommandPelangganLimbah,
                            "Cetak",
                            "Tutup",
                            moduleName: "hublang");

                        break;
                    case "Pelanggan L2T2":
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganLltt).IsCetak = true;
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganLltt).IsPreview = true;
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganLltt).TemplateName = $"{fitur}PelangganLltt_{_viewModel.SelectedTipePermohonanComboBox.KodeTipePermohonan}";
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganLltt).CustomParam = param;

                        await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                            _isTest,
                            false,
                            "HublangRootDialog",
                            $"Tambah Data Berhasil",
                            $"No Registrasi : {nomorPermohonan}",
                            "success",
                            _viewModel.OnCetakCommandPelangganLltt,
                            "Cetak",
                            "Tutup",
                            moduleName: "hublang");

                        break;
                    case "Non Pelanggan":
                        ((OnCetakCommand)_viewModel.OnCetakCommandNonPelanggan).IsCetak = true;
                        ((OnCetakCommand)_viewModel.OnCetakCommandNonPelanggan).IsPreview = true;
                        ((OnCetakCommand)_viewModel.OnCetakCommandNonPelanggan).TemplateName = $"{fitur}NonPelanggan_{_viewModel.SelectedTipePermohonanComboBox.KodeTipePermohonan}";
                        ((OnCetakCommand)_viewModel.OnCetakCommandNonPelanggan).CustomParam = param;

                        await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                            _isTest,
                            false,
                            "HublangRootDialog",
                            $"Tambah Data Berhasil",
                            $"No Registrasi : {nomorPermohonan}",
                            "success",
                            _viewModel.OnCetakCommandNonPelanggan,
                            "Cetak",
                            "Tutup",
                            moduleName: "hublang");

                        break;
                }
            }
        }
    }
}
