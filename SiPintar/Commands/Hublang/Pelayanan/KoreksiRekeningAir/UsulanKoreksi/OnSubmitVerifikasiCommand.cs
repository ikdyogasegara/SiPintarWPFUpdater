using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan.KoreksiRekeningAir;

namespace SiPintar.Commands.Hublang.Pelayanan.KoreksiRekeningAir.UsulanKoreksi
{
    [ExcludeFromCodeCoverage]
    public class OnSubmitVerifikasiCommand : AsyncCommandBase
    {
        private readonly UsulanKoreksiViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitVerifikasiCommand(UsulanKoreksiViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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

            if (_viewModel.Parent.IsBilling == false)
            {
                var body = new Dictionary<string, dynamic>
                {
                    { "IdPermohonan", _viewModel.SelectedData.IdPermohonan},
                    { "IdPeriode", _viewModel.SelectedData.IdPeriode },
                };

                string endPoint = null;

                if (_viewModel.StatusVerifikasiForm == 1)
                {
                    endPoint = "permohonan-koreksi-rekening-air-verifikasi-lapangan";
                    body.Add("KeteranganStatusVerifikasiLapangan", null);
                    body.Add("WaktuStatusVerifikasiLapangan", DateTime.Now);
                }
                else if (_viewModel.StatusVerifikasiForm == 2)
                {
                    endPoint = "permohonan-koreksi-rekening-air-penolakan-lapangan";
                    body.Add("KeteranganStatusVerifikasiLapangan", _viewModel.AlasanPenolakanForm);
                    body.Add("WaktuStatusVerifikasiLapangan", DateTime.Now);
                }

                var response = await _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/{endPoint}", null, body);
                if (!response.IsError)
                {
                    var result = response.Data;
                    if (result.Status)
                    {
                        UpdateRecordLapangan(selectedData);
                        DialogHelper.ShowSnackbar(_isTest, "success", result.Ui_msg, "hublang");
                    }
                    else
                        DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg, "hublang");
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message, "hublang");
            }
            else
            {
                var body = new Dictionary<string, dynamic>
                {
                    { "IdPermohonan", _viewModel.SelectedData.IdPermohonan},
                    { "IdPeriode", _viewModel.SelectedData.IdPeriode },
                };

                string endPoint = null;

                if (_viewModel.StatusVerifikasiForm == 1)
                {
                    endPoint = "permohonan-koreksi-rekening-air-verifikasi-pusat";
                    body.Add("KeteranganStatusVerifikasiPusat", null);
                    body.Add("WaktuStatusVerifikasiPusat", DateTime.Now);
                }
                else if (_viewModel.StatusVerifikasiForm == 2)
                {
                    endPoint = "permohonan-koreksi-rekening-air-penolakan-pusat";
                    body.Add("KeteranganStatusVerifikasiPusat", _viewModel.AlasanPenolakanForm);
                    body.Add("WaktuStatusVerifikasiPusat", DateTime.Now);
                }

                var response = await _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/{endPoint}", null, body);
                if (!response.IsError)
                {
                    var result = response.Data;
                    if (result.Status)
                    {
                        UpdateRecordPusat(selectedData);
                        DialogHelper.ShowSnackbar(_isTest, "success", result.Ui_msg, "billing");
                    }
                    else
                        DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg, "billing");
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message, "billing");
            }

            selectedData.IsUpdatingKoreksi = false;
            _viewModel.IsLoadingForm = false;
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void UpdateRecordLapangan(PermohonanKoreksiRekeningWpf selectedData)
        {
            if (!_isTest)
            {
                selectedData.StatusVerifikasiLapanganWpf = _viewModel.StatusVerifikasiForm;
                selectedData.WaktuStatusVerifikasiLapanganWpf = DateTime.Now;
                selectedData.KeteranganStatusVerifikasiLapanganWpf = _viewModel.AlasanPenolakanForm;
            }
        }

        [ExcludeFromCodeCoverage]
        private void UpdateRecordPusat(PermohonanKoreksiRekeningWpf selectedData)
        {
            if (!_isTest)
            {
                selectedData.StatusVerifikasiPusatWpf = _viewModel.StatusVerifikasiForm;
                selectedData.WaktuStatusVerifikasiPusatWpf = DateTime.Now;
                selectedData.KeteranganStatusVerifikasiPusatWpf = _viewModel.AlasanPenolakanForm;
            }
        }
    }
}
