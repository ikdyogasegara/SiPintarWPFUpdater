using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json.Linq;
using SiPintar.Commands.Global;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Hublang.Pelayanan.Pendaftaran
{
    public class OnSubmitAddFormCommand : AsyncCommandBase
    {
        private readonly PendaftaranViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitAddFormCommand(PendaftaranViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // DialogHelper.ShowLoading(_isTest, "HublangRootDialog");

            _viewModel.IsLoading = true;
            var body = parameter as Dictionary<string, dynamic>;

            var response = await _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/permohonan-non-pelanggan", body);
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
                        _viewModel.FormData.IdPermohonan = Convert.ToInt32(dataResponse[0].GetValue("IdPermohonan", StringComparison.OrdinalIgnoreCase) ?? 0);

                        await UploadFotoAsync();
                        await ShowCetakDialogAsync(nomorPermohonan.ToString());
                        await ((AsyncCommandBase)_viewModel.OnRefreshCommand).ExecuteAsync(null);
                    }
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg, "hublang");
            }
            else
            {
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message, "hublang");
            }

            _viewModel.IsLoading = false;
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private async Task UploadFotoAsync()
        {
            if (!_isTest && (_viewModel.IsNewFotoSuratPernyataan == true || _viewModel.IsNewFotoKk == true || _viewModel.IsNewFotoKtp == true || _viewModel.IsNewFotoImb == true))
            {
                var bodyUpload = new Dictionary<string, dynamic>
                {
                    {"IdPermohonan", _viewModel.FormData.IdPermohonan}
                };

                if (_viewModel.FotoSuratPernyataanUri != null && !_viewModel.FotoSuratPernyataanUri.OriginalString.ToLower().Contains("http") && _viewModel.IsNewFotoSuratPernyataan)
                    bodyUpload.Add("file_FotoSuratPernyataan", _viewModel.FotoSuratPernyataanUri.OriginalString);
                if (_viewModel.FotoKkUri != null && !_viewModel.FotoKkUri.OriginalString.ToLower().Contains("http") && _viewModel.IsNewFotoKk)
                    bodyUpload.Add("file_FotoKK", _viewModel.FotoKkUri.OriginalString);
                if (_viewModel.FotoKtpUri != null && !_viewModel.FotoKtpUri.OriginalString.ToLower().Contains("http") && _viewModel.IsNewFotoKtp)
                    bodyUpload.Add("file_FotoKtp", _viewModel.FotoKtpUri.OriginalString);
                if (_viewModel.FotoImbUri != null && !_viewModel.FotoImbUri.OriginalString.ToLower().Contains("http") && _viewModel.IsNewFotoImb)
                    bodyUpload.Add("file_FotoImb", _viewModel.FotoImbUri.OriginalString);

                var responseUpload = await _restApi.UploadAsync($"/api/{_restApi.GetApiVersion()}/permohonan-non-pelanggan-upload-foto", bodyUpload);

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
        private async Task ShowCetakDialogAsync(string nomorPermohonan)
        {
            if (!_isTest)
            {
                var param = new Dictionary<string, dynamic>()
                {
                    { "IdPermohonan", _viewModel.FormData.IdPermohonan },
                };

                ((OnCetakCommand)_viewModel.OnCetakCommandNonPelanggan).IsCetak = true;
                ((OnCetakCommand)_viewModel.OnCetakCommandNonPelanggan).IsPreview = true;
                ((OnCetakCommand)_viewModel.OnCetakCommandNonPelanggan).TemplateName = $"Pendaftaran_Bukti_{_viewModel.SelectedTipePermohonanComboBox.KodeTipePermohonan}";
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
    }
}
