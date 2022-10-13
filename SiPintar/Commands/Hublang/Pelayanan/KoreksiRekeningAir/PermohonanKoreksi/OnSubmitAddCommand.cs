using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using Newtonsoft.Json.Linq;
using Serilog;
using SiPintar.Commands.Global;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan.KoreksiRekeningAir;

namespace SiPintar.Commands.Hublang.Pelayanan.KoreksiRekeningAir.PermohonanKoreksi
{
    [ExcludeFromCodeCoverage]
    public class OnSubmitAddCommand : AsyncCommandBase
    {
        private readonly PermohonanKoreksiViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitAddCommand(PermohonanKoreksiViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoadingForm = true;
            _viewModel.Parent.IsLoading = true;

            var body = new Dictionary<string, dynamic>
            {
                {"IdTipePermohonan", _viewModel.Parent.TipeJenisKoreksiAir.IdTipePermohonan},
                {"IdJenisNonAir", _viewModel.Parent.TipeJenisKoreksiAir.IdJenisNonAir},
                {"DetailBiaya", SetBiaya()},
                {"IdUser", Application.Current.Resources["IdUser"].ToString()},
                {"IdPelangganAir", _viewModel.SelectedPelangganAir.IdPelangganAir},
                {"Keterangan", _viewModel.AlasanForm},
                {"DetailParameter", SetParameter()},
                {"IdRayon", _viewModel.SelectedPelangganAir.IdRayon},
                {"IdKelurahan", _viewModel.SelectedPelangganAir.IdKelurahan},
                {"IdGolongan", _viewModel.SelectedPelangganAir.IdGolongan},
                {"IdDiameter", _viewModel.SelectedPelangganAir.IdDiameter},
                {"KodeTipePermohonan", _viewModel.Parent.TipeJenisKoreksiAir.KodeTipePermohonan},
            };

            var response = await _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/permohonan-pelanggan-air", body);
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    _viewModel.IsExists = result.Ui_msg == "Data yang sama sudah ada dan belum lebih dari 60 hari !";

                    if (_viewModel.IsExists)
                    {
                        OpenDialogWarning();
                    }
                    else
                    {
                        var dataResponse = result.Data.ToObject<ObservableCollection<JObject>>();
                        var nomorPermohonan = dataResponse[0].GetValue("NomorPermohonan", StringComparison.OrdinalIgnoreCase) ?? "-";
                        var idPermohonan = Convert.ToInt32(dataResponse[0].GetValue("IdPermohonan", StringComparison.OrdinalIgnoreCase) ?? 0);

                        await UploadFotoAsync(idPermohonan);

                        await ShowCetakDialogAsync(nomorPermohonan.ToString(), idPermohonan);
                        await ((AsyncCommandBase)_viewModel.OnRefreshCommand).ExecuteAsync(null);
                    }
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg, "hublang");
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message, "hublang");

            _viewModel.IsLoadingForm = false;
            _viewModel.Parent.IsLoading = false;
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private List<ParamPermohonanPelangganAirBiayaDto> SetBiaya()
        {
            if (!_isTest)
            {
                var result = new List<ParamPermohonanPelangganAirBiayaDto>() { new ParamPermohonanPelangganAirBiayaDto() { Parameter = "Biaya Koreksi", PostBiaya = "lainnya", Value = _viewModel.BiayaForm } };

                return result;
            }

            return new List<ParamPermohonanPelangganAirBiayaDto>();
        }

        [ExcludeFromCodeCoverage]
        private List<ParamPermohonanPelangganAirDetailDto> SetParameter()
        {
            if (!_isTest)
            {
                var parameterName = string.Empty;
                var tipeData = string.Empty;

                foreach (var item in _viewModel.Parent.TipeJenisKoreksiAir.DetailParameter)
                {
                    parameterName = item.Parameter;
                    tipeData = item.TipeData;
                }

                var periodeSelected = new List<int>();
                foreach (var item in _viewModel.PiutangAirList)
                {
                    if (item.IsSelected && item.IdPeriode != null)
                    {
                        periodeSelected.Add((int)item.IdPeriode);
                    }
                }

                var result = new List<ParamPermohonanPelangganAirDetailDto>() { new ParamPermohonanPelangganAirDetailDto() { Parameter = parameterName, TipeData = tipeData, Value = string.Join(",", periodeSelected.ToArray()) } };

                return result;
            }

            return new List<ParamPermohonanPelangganAirDetailDto>();
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
        private async Task UploadFotoAsync(int? idPermohonan)
        {
            if (!_isTest && (_viewModel.IsNewFotoBukti1 == true || _viewModel.IsNewFotoBukti2 == true || _viewModel.IsNewFotoBukti3 == true))
            {
                var bodyUpload = new Dictionary<string, dynamic>
                {
                    { "IdPermohonan", idPermohonan }
                };

                if (_viewModel.FotoBukti1Uri != null && !_viewModel.FotoBukti1Uri.OriginalString.ToLower().Contains("http") && _viewModel.IsNewFotoBukti1)
                    bodyUpload.Add("file_FotoBukti1", _viewModel.FotoBukti1Uri.OriginalString);
                if (_viewModel.FotoBukti2Uri != null && !_viewModel.FotoBukti2Uri.OriginalString.ToLower().Contains("http") && _viewModel.IsNewFotoBukti2)
                    bodyUpload.Add("file_FotoBukti2", _viewModel.FotoBukti2Uri.OriginalString);
                if (_viewModel.FotoBukti3Uri != null && !_viewModel.FotoBukti3Uri.OriginalString.ToLower().Contains("http") && _viewModel.IsNewFotoBukti3)
                    bodyUpload.Add("file_FotoBukti3", _viewModel.FotoBukti3Uri.OriginalString);

                var responseUpload = await _restApi.UploadAsync($"/api/{_restApi.GetApiVersion()}/permohonan-pelanggan-air-upload-foto", bodyUpload);
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
        private async Task ShowCetakDialogAsync(string nomorPermohonan, int? idPermohonan)
        {
            if (!_isTest)
            {
                var param = new Dictionary<string, dynamic>() { { "IdPermohonan", idPermohonan } };

                ((OnCetakCommand)_viewModel.OnCetakCommandPelangganAir).IsCetak = true;
                ((OnCetakCommand)_viewModel.OnCetakCommandPelangganAir).IsPreview = true;
                ((OnCetakCommand)_viewModel.OnCetakCommandPelangganAir).TemplateName = $"PermohonanPelangganAir_KREKAIR";
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
                    "Tutup");

                _viewModel.IsLoadingForm = false;
                _viewModel.Parent.IsLoading = false;
            }
        }
    }
}
