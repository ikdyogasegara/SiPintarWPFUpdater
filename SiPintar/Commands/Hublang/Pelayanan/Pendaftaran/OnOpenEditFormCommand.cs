using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan;
using SiPintar.Views.Hublang.Pelayanan.Pendaftaran;

namespace SiPintar.Commands.Hublang.Pelayanan.Pendaftaran
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly PendaftaranViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(PendaftaranViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedData == null)
                return;

            var tipe = parameter as string; // for detail or edit
            _viewModel.IsEdit = tipe == "edit";
            _viewModel.IsDetail = tipe == "detail";

            object dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, false, "HublangRootDialog", "Mohon tunggu", "sedang memproses tindakan...", "");

            if (_viewModel.SelectedData.StatusPermohonanText == "Selesai" && _viewModel.IsEdit == true)
            {
                DialogHelper.CloseDialog(_isTest, false, "HublangRootDialog", dg);

                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false,
                    "HublangRootDialog",
                    "Koreksi Tidak diijinkan",
                    "Data yang sudah selesai tidak dapat dikoreksi !",
                    "warning",
                    "Batal",
                    false,
                    "hublang");

                return;
            }

            _viewModel.IsLoadingForm = true;
            _viewModel.IsNewFotoSuratPernyataan = false;
            _viewModel.IsNewFotoKk = false;
            _viewModel.IsNewFotoKtp = false;
            _viewModel.IsNewFotoImb = false;

            _viewModel.FormData = JsonConvert.DeserializeObject<PermohonanNonPelangganForm>(JsonConvert.SerializeObject(_viewModel.SelectedData));

            await GetFotoAsync();

            _viewModel.IsLoadingForm = false;

            DialogHelper.CloseDialog(_isTest, false, "HublangRootDialog", dg);

            if (!_isTest) await DialogHost.Show(new KoreksiFormView(_viewModel), "HublangRootDialog");

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private async Task GetFotoAsync()
        {
            if (!_isTest)
            {
                if (_viewModel.FormData.IdPermohonan == null)
                    return;

                var detail = _viewModel.FormData;

                var param = new Dictionary<string, dynamic> { { "IdPermohonan", _viewModel.FormData.IdPermohonan } };

                var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/permohonan-non-pelanggan-link-foto", param);
                if (!response.IsError)
                {
                    var result = response.Data;
                    if (result.Status)
                    {
                        var data = result.Data.ToObject<ObservableCollection<PermohonanNonPelangganDto>>();

                        if (data != null)
                        {
                            detail.FotoKtp = data[0].FotoKtp;
                            detail.FotoKk = data[0].FotoKk;
                            detail.FotoSuratPernyataan = data[0].FotoSuratPernyataan;
                            detail.FotoImb = data[0].FotoImb;
                            detail.FotoBukti1 = data[0].FotoBukti1;
                            detail.FotoBukti2 = data[0].FotoBukti2;
                            detail.FotoBukti3 = data[0].FotoBukti3;

                            _viewModel.FormData = detail;

                            _viewModel.FotoKtpUri = await Task.Run(() => ImageUriHelper.SetUriAsync(detail.FotoKtp));
                            _viewModel.FotoKkUri = await Task.Run(() => ImageUriHelper.SetUriAsync(detail.FotoKk));
                            _viewModel.FotoSuratPernyataanUri = await Task.Run(() => ImageUriHelper.SetUriAsync(detail.FotoSuratPernyataan));
                            _viewModel.FotoImbUri = await Task.Run(() => ImageUriHelper.SetUriAsync(detail.FotoImb));
                        }
                    }
                    else
                    {
                        DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg);
                    }
                }
                else
                {
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message);
                }
            }
        }
    }
}
