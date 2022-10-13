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
    public class OnSubmitBaFormCommand : AsyncCommandBase
    {
        private readonly PermohonanViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitBaFormCommand(PermohonanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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
                dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, false, "DistribusiRootDialog", "Mohon tunggu", "sedang memproses tindakan...", "");
                response = await _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/{_viewModel.EndPointBa}", body);
            }
            else
            {
                selectedData = _viewModel.SelectedData;
                selectedData.IsUpdatingKoreksi = true;
                response = await _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/{_viewModel.EndPointBa}", null, body);
            }

            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    if (_viewModel.IsFor == "add")
                    {
                        var dataResponse = result.Data.ToObject<ObservableCollection<JObject>>();
                        var nomorBeritaAcara = dataResponse[0].GetValue("NomorBa", StringComparison.OrdinalIgnoreCase) ?? "-";
                        _viewModel.FormData.IdPermohonan = Convert.ToInt32(dataResponse[0].GetValue("IdPermohonan", StringComparison.OrdinalIgnoreCase) ?? 0);

                        await UploadFotoAsync();
                        DialogHelper.CloseDialog(_isTest, false, Identifier: "DistribusiRootDialog", dg);
                        await ShowCetakDialogAsync(nomorBeritaAcara.ToString());
                        await ((AsyncCommandBase)_viewModel.OnRefreshCommand).ExecuteAsync(null);
                    }
                    else
                    {
                        UpdateRecord(selectedData, result.Data.ToObject<ObservableCollection<PermohonanWpf>>());
                        await UploadFotoAsync();
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
        private async Task UploadFotoAsync()
        {
            if (!_isTest && (_viewModel.IsNewFotoBuktiBeritaAcara1 == true || _viewModel.IsNewFotoBuktiBeritaAcara2 == true || _viewModel.IsNewFotoBuktiBeritaAcara3 == true))
            {
                var bodyUpload = new Dictionary<string, dynamic>
                {
                    { "IdPermohonan", _viewModel.FormData.IdPermohonan }
                };

                if (_viewModel.FotoBuktiBeritaAcara1Uri != null && !_viewModel.FotoBuktiBeritaAcara1Uri.OriginalString.ToLower().Contains("http") && _viewModel.IsNewFotoBuktiBeritaAcara1)
                    bodyUpload.Add("file_FotoBukti1", _viewModel.FotoBuktiBeritaAcara1Uri.OriginalString);
                if (_viewModel.FotoBuktiBeritaAcara2Uri != null && !_viewModel.FotoBuktiBeritaAcara2Uri.OriginalString.ToLower().Contains("http") && _viewModel.IsNewFotoBuktiBeritaAcara2)
                    bodyUpload.Add("file_FotoBukti2", _viewModel.FotoBuktiBeritaAcara2Uri.OriginalString);
                if (_viewModel.FotoBuktiBeritaAcara3Uri != null && !_viewModel.FotoBuktiBeritaAcara3Uri.OriginalString.ToLower().Contains("http") && _viewModel.IsNewFotoBuktiBeritaAcara3)
                    bodyUpload.Add("file_FotoBukti3", _viewModel.FotoBuktiBeritaAcara3Uri.OriginalString);

                var responseUpload = await _restApi.UploadAsync($"/api/{_restApi.GetApiVersion()}/{_viewModel.EndPointBa}-upload-foto", bodyUpload);
                if (!responseUpload.IsError)
                {
                    var resultUpload = responseUpload.Data;
                    if (resultUpload.Status)
                    {
                        DialogHelper.ShowSnackbar(_isTest, "success", "Upload Foto Berhasil", "distribusi");
                    }
                }
            }
        }

        [ExcludeFromCodeCoverage]
        private void UpdateRecord(PermohonanWpf selectedData, ObservableCollection<PermohonanWpf> resultData)
        {
            if (!_isTest)
            {
                selectedData.IdTipePendaftaranSambunganWpf = resultData[0].IdTipePendaftaranSambungan;
                selectedData.NamaWpf = resultData[0].Nama;
                selectedData.AlamatWpf = resultData[0].Alamat;
                selectedData.RtWpf = resultData[0].Rt;
                selectedData.RwWpf = resultData[0].Rw;
                selectedData.NoHpWpf = resultData[0].NoHp;
                selectedData.NoTelpWpf = resultData[0].NoTelp;
                selectedData.EmailWpf = resultData[0].Email;
                selectedData.IdPekerjaanWpf = resultData[0].IdPekerjaan;
                selectedData.NoKkWpf = resultData[0].NoKk;
                selectedData.NoKtpWpf = resultData[0].NoKtp;
                selectedData.IdRayonWpf = resultData[0].IdRayon;
                selectedData.KodeRayonWpf = resultData[0].KodeRayon;
                selectedData.NamaRayonWpf = resultData[0].NamaRayon;
                selectedData.IdWilayahWpf = resultData[0].IdWilayah;
                selectedData.KodeWilayahWpf = resultData[0].KodeWilayah;
                selectedData.NamaWilayahWpf = resultData[0].NamaWilayah;
                selectedData.IdKelurahanWpf = resultData[0].IdKelurahan;
                selectedData.KodeKelurahanWpf = resultData[0].KodeKelurahan;
                selectedData.NamaKelurahanWpf = resultData[0].NamaKelurahan;
                selectedData.NamaKecamatanWpf = resultData[0].NamaKecamatan;
                selectedData.NamaCabangWpf = resultData[0].NamaCabang;
                selectedData.IdBlokWpf = resultData[0].IdBlok;
                selectedData.NamaBlokWpf = resultData[0].NamaBlok;
                selectedData.IdGolonganWpf = resultData[0].IdGolongan;
                selectedData.KodeGolonganWpf = resultData[0].KodeGolongan;
                selectedData.NamaGolonganWpf = resultData[0].NamaGolongan;
                selectedData.IdDiameterWpf = resultData[0].IdDiameter;
                selectedData.KodeDiameterWpf = resultData[0].KodeDiameter;
                selectedData.NamaDiameterWpf = resultData[0].NamaDiameter;
                selectedData.IdAdministrasiLainWpf = resultData[0].IdAdministrasiLain;
                selectedData.IdPemeliharaanLainWpf = resultData[0].IdPemeliharaanLain;
                selectedData.IdRetribusiLainWpf = resultData[0].IdRetribusiLain;
                selectedData.FlagLanjutKeLanggananLimbahWpf = resultData[0].FlagLanjutKeLanggananLimbah;
                selectedData.PenghuniWpf = resultData[0].Penghuni;
                selectedData.LuasTanahWpf = resultData[0].LuasTanah;
                selectedData.LuasRumahWpf = resultData[0].LuasRumah;
                selectedData.IdJenisBangunanWpf = resultData[0].IdJenisBangunan;
                selectedData.IdKepemilikanWpf = resultData[0].IdKepemilikan;
                selectedData.IdPeruntukanWpf = resultData[0].IdPeruntukan;
                selectedData.IdSumberAirWpf = resultData[0].IdSumberAir;
                selectedData.KodePostWpf = resultData[0].KodePost;
                selectedData.DayaListrikWpf = resultData[0].DayaListrik;
                selectedData.KeteranganWpf = resultData[0].Keterangan;
                selectedData.AlamatPemilikWpf = resultData[0].AlamatPemilik;
                selectedData.NamaPemilikWpf = resultData[0].NamaPemilik;
                selectedData.NosambDepanWpf = resultData[0].NosambDepan;
                selectedData.NosambBelakangWpf = resultData[0].NosambBelakang;
                selectedData.NosambKananWpf = resultData[0].NosambKanan;
                selectedData.NosambKiriWpf = resultData[0].NosambKiri;
                selectedData.IdDmaWpf = resultData[0].IdDma;
                selectedData.IdDmzWpf = resultData[0].IdDmz;

                selectedData.BeritaAcaraWpf.DistribusiFlagBiayaDibebankankePdam = resultData[0].BeritaAcara.DistribusiFlagBiayaDibebankankePdam;
                selectedData.BeritaAcaraWpf.DistribusiFlagDialihkanKeVendor = resultData[0].BeritaAcara.DistribusiFlagDialihkanKeVendor;
                selectedData.BeritaAcaraWpf.DistribusiNamaPaket = resultData[0].BeritaAcara.DistribusiNamaPaket;

                selectedData.BeritaAcaraWpf.PersilFlagBiayaDibebankanKePdam = resultData[0].BeritaAcara.PersilFlagBiayaDibebankanKePdam;
                selectedData.BeritaAcaraWpf.PersilFlagDialihkanKeVendor = resultData[0].BeritaAcara.PersilFlagDialihkanKeVendor;
                selectedData.BeritaAcaraWpf.PersilNamaPaket = resultData[0].BeritaAcara.PersilNamaPaket;

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
        private async Task ShowCetakDialogAsync(string nomorBeritaAcara)
        {
            if (!_isTest)
            {
                var param = new Dictionary<string, dynamic>()
                {
                    { "IdPermohonan", _viewModel.FormData.IdPermohonan }
                };

                var fitur = "BA";

                switch (_viewModel.SelectedJenisPelanggan)
                {
                    case "Pelanggan Air":
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganAirBeritaAcara).IsCetak = true;
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganAirBeritaAcara).IsPreview = true;
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganAirBeritaAcara).TemplateName = $"{fitur}PelangganAir_{_viewModel.SelectedTipePermohonanComboBox.KodeTipePermohonan}";
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganAirBeritaAcara).CustomParam = param;

                        await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                            _isTest,
                            false,
                            "DistribusiRootDialog",
                            $"Tambah Data Berhasil",
                            $"No BeritaAcara : {nomorBeritaAcara}",
                            "success",
                            _viewModel.OnCetakCommandPelangganAirBeritaAcara,
                            "Cetak",
                            "Tutup",
                            moduleName: "distribusi");

                        break;
                    case "Pelanggan Limbah":
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganLimbahBeritaAcara).IsCetak = true;
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganLimbahBeritaAcara).IsPreview = true;
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganLimbahBeritaAcara).TemplateName = $"{fitur}PelangganLimbah_{_viewModel.SelectedTipePermohonanComboBox.KodeTipePermohonan}";
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganLimbahBeritaAcara).CustomParam = param;

                        await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                            _isTest,
                            false,
                            "DistribusiRootDialog",
                            $"Tambah Data Berhasil",
                           $"No BeritaAcara : {nomorBeritaAcara}",
                            "success",
                            _viewModel.OnCetakCommandPelangganLimbahBeritaAcara,
                            "Cetak",
                            "Tutup",
                            moduleName: "distribusi");

                        break;
                    case "Pelanggan L2T2":
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganLlttBeritaAcara).IsCetak = true;
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganLlttBeritaAcara).IsPreview = true;
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganLlttBeritaAcara).TemplateName = $"{fitur}PelangganLltt_{_viewModel.SelectedTipePermohonanComboBox.KodeTipePermohonan}";
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganLlttBeritaAcara).CustomParam = param;

                        await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                            _isTest,
                            false,
                            "DistribusiRootDialog",
                            $"Tambah Data Berhasil",
                            $"No BeritaAcara : {nomorBeritaAcara}",
                            "success",
                            _viewModel.OnCetakCommandPelangganLlttBeritaAcara,
                            "Cetak",
                            "Tutup",
                            moduleName: "distribusi");

                        break;
                    case "Non Pelanggan":
                        ((OnCetakCommand)_viewModel.OnCetakCommandNonPelangganBeritaAcara).IsCetak = true;
                        ((OnCetakCommand)_viewModel.OnCetakCommandNonPelangganBeritaAcara).IsPreview = true;
                        ((OnCetakCommand)_viewModel.OnCetakCommandNonPelangganBeritaAcara).TemplateName = $"{fitur}NonPelanggan_{_viewModel.SelectedTipePermohonanComboBox.KodeTipePermohonan}";
                        ((OnCetakCommand)_viewModel.OnCetakCommandNonPelangganBeritaAcara).CustomParam = param;

                        await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                            _isTest,
                            false,
                            "DistribusiRootDialog",
                            $"Tambah Data Berhasil",
                            $"No BeritaAcara : {nomorBeritaAcara}",
                            "success",
                            _viewModel.OnCetakCommandNonPelangganBeritaAcara,
                            "Cetak",
                            "Tutup",
                            moduleName: "distribusi");

                        break;
                }
            }
        }
    }
}
