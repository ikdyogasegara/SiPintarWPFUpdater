using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan.KoreksiRekeningAir;

namespace SiPintar.Commands.Hublang.Pelayanan.KoreksiRekeningAir.PermohonanKoreksi
{
    [ExcludeFromCodeCoverage]
    public class OnSubmitEditCommand : AsyncCommandBase
    {
        private readonly PermohonanKoreksiViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitEditCommand(PermohonanKoreksiViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var selectedData = _viewModel.SelectedData;
            selectedData.IsUpdatingKoreksi = true;

            _viewModel.IsLoadingForm = true;

            var body = new Dictionary<string, dynamic>
            {
                {"IdPermohonan", _viewModel.SelectedData.IdPermohonan},
                {"IdTipePermohonan", _viewModel.SelectedData.IdTipePermohonan},
                {"IdJenisNonAir", _viewModel.SelectedData.NonAirReguler?.IdJenisNonAir},
                {"DetailBiaya", SetBiaya()},
                {"IdUser", Application.Current.Resources["IdUser"].ToString()},
                {"IdPelangganAir", _viewModel.SelectedData.IdPelangganAir},
                {"Keterangan", _viewModel.AlasanForm},
                {"DetailParameter", SetParameter()},
                {"IdRayon", _viewModel.SelectedData.IdRayon},
                {"IdKelurahan", _viewModel.SelectedData.IdKelurahan},
                {"IdGolongan", _viewModel.SelectedData.IdGolongan},
                {"IdDiameter", _viewModel.SelectedData.IdDiameter},
            };

            var response = await _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/permohonan-pelanggan-air", null, body);
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    UpdateRecord(selectedData, result.Data.ToObject<ObservableCollection<PermohonanWpf>>());
                    await UploadFotoAsync(selectedData.IdPermohonan);
                    selectedData.IsUpdatingKoreksi = false;
                    _viewModel.IsLoadingForm = false;
                    DialogHelper.ShowSnackbar(_isTest, "success", result.Ui_msg, "hublang");
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg, "hublang");
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message, "hublang");

            selectedData.IsUpdatingKoreksi = false;
            _viewModel.IsLoadingForm = false;
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
                    if (item.IsSelected)
                        periodeSelected.Add((int)item.IdPeriode);
                }

                var result = new List<ParamPermohonanPelangganAirDetailDto>() { new ParamPermohonanPelangganAirDetailDto() { Parameter = parameterName, TipeData = tipeData, Value = string.Join(",", periodeSelected.ToArray()) } };

                return result;
            }

            return new List<ParamPermohonanPelangganAirDetailDto>();
        }

        [ExcludeFromCodeCoverage]
        private void UpdateRecord(PermohonanWpf selectedData, ObservableCollection<PermohonanWpf> resultData)
        {
            if (!_isTest)
            {
                selectedData.KeteranganWpf = resultData[0].KeteranganWpf;
                selectedData.ParameterWpf = new List<PermohonanDetailDto>();
                foreach (var i in resultData[0].ParameterWpf)
                {
                    selectedData.ParameterWpf.Add(new PermohonanDetailDto { Parameter = i.Parameter, TipeData = i.TipeData, Value = i.Value });
                }
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
    }
}
