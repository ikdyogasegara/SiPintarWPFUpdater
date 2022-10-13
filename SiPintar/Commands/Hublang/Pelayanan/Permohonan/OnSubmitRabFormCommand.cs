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
    public class OnSubmitRabFormCommand : AsyncCommandBase
    {
        private readonly PermohonanViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitRabFormCommand(PermohonanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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
                response = await _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/{_viewModel.EndPointRab}", body);
            }
            else
            {
                selectedData = _viewModel.SelectedData;
                selectedData.IsUpdatingKoreksi = true;
                response = await _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/{_viewModel.EndPointRab}", null, body);
            }

            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    if (_viewModel.IsFor == "add")
                    {
                        var dataResponse = result.Data.ToObject<ObservableCollection<JObject>>();
                        var nomorRab = dataResponse[0].GetValue("NomorRab", StringComparison.OrdinalIgnoreCase) ?? "-";
                        _viewModel.FormData.IdPermohonan = Convert.ToInt32(dataResponse[0].GetValue("IdPermohonan", StringComparison.OrdinalIgnoreCase) ?? 0);

                        await UploadFotoAsync();
                        DialogHelper.CloseDialog(_isTest, false, Identifier: "PerencanaanRootDialog", dg);
                        await ShowCetakDialogAsync(nomorRab.ToString());
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
            if (!_isTest && (_viewModel.IsNewFotoDenah1 == true || _viewModel.IsNewFotoDenah2 == true || _viewModel.IsNewFotoBuktiRab1 == true || _viewModel.IsNewFotoBuktiRab2 == true || _viewModel.IsNewFotoBuktiRab3 == true))
            {
                var bodyUpload = new Dictionary<string, dynamic>
                {
                    { "IdPermohonan", _viewModel.FormData.IdPermohonan }
                };

                if (_viewModel.FotoDenah1Uri != null && !_viewModel.FotoDenah1Uri.OriginalString.ToLower().Contains("http") && _viewModel.IsNewFotoDenah1)
                    bodyUpload.Add("file_FotoDenah1", _viewModel.FotoDenah1Uri.OriginalString);
                if (_viewModel.FotoDenah2Uri != null && !_viewModel.FotoDenah2Uri.OriginalString.ToLower().Contains("http") && _viewModel.IsNewFotoDenah2)
                    bodyUpload.Add("file_FotoDenah2", _viewModel.FotoDenah2Uri.OriginalString);
                if (_viewModel.FotoBuktiRab1Uri != null && !_viewModel.FotoBuktiRab1Uri.OriginalString.ToLower().Contains("http") && _viewModel.IsNewFotoBuktiRab1)
                    bodyUpload.Add("file_FotoBukti1", _viewModel.FotoBuktiRab1Uri.OriginalString);
                if (_viewModel.FotoBuktiRab2Uri != null && !_viewModel.FotoBuktiRab2Uri.OriginalString.ToLower().Contains("http") && _viewModel.IsNewFotoBuktiRab2)
                    bodyUpload.Add("file_FotoBukti2", _viewModel.FotoBuktiRab2Uri.OriginalString);
                if (_viewModel.FotoBuktiRab3Uri != null && !_viewModel.FotoBuktiRab3Uri.OriginalString.ToLower().Contains("http") && _viewModel.IsNewFotoBuktiRab3)
                    bodyUpload.Add("file_FotoBukti3", _viewModel.FotoBuktiRab3Uri.OriginalString);

                var responseUpload = await _restApi.UploadAsync($"/api/{_restApi.GetApiVersion()}/{_viewModel.EndPointRab}-upload-foto", bodyUpload);
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

                selectedData.RabWpf.DistribusiFlagBiayaDibebankankePdam = resultData[0].RAB.DistribusiFlagBiayaDibebankankePdam;
                selectedData.RabWpf.DistribusiFlagDialihkanKeVendor = resultData[0].RAB.DistribusiFlagDialihkanKeVendor;
                selectedData.RabWpf.DistribusiNamaPaket = resultData[0].RAB.DistribusiNamaPaket;
                selectedData.RabWpf.PersilFlagBiayaDibebankanKePdam = resultData[0].RAB.PersilFlagBiayaDibebankanKePdam;
                selectedData.RabWpf.PersilFlagDialihkanKeVendor = resultData[0].RAB.PersilFlagDialihkanKeVendor;
                selectedData.RabWpf.PersilNamaPaket = resultData[0].RAB.PersilNamaPaket;

                var detailPipaPersil = new List<PermohonanRabDetailDto>();
                var detailPipaDistribusi = new List<PermohonanRabDetailDto>();

                if (resultData[0].RAB.DetailPersil != null)
                {
                    foreach (var i in resultData[0].RAB.DetailPersil)
                    {
                        detailPipaPersil.Add(new PermohonanRabDetailDto
                        {
                            Kode = i.Kode,
                            Tipe = i.Tipe,
                            Uraian = i.Uraian,
                            Satuan = i.Satuan,
                            HargaSatuan = i.HargaSatuan,
                            Qty = i.Qty,
                            Jumlah = i.Jumlah,
                            Ppn = i.Ppn,
                            Keuntungan = i.Keuntungan,
                            JasaDariBahan = i.JasaDariBahan,
                            Total = i.Total,
                            Kelompok = i.Kelompok,
                            PostBiaya = i.PostBiaya,
                            FlagBiayaDibebankanKePdam = i.FlagBiayaDibebankanKePdam,
                            FlagDialihkanKeVendor = i.FlagDialihkanKeVendor
                        });
                    }
                }

                if (resultData[0].RAB.DetailDistribusi != null)
                {
                    foreach (var i in resultData[0].RAB.DetailDistribusi)
                    {
                        detailPipaDistribusi.Add(new PermohonanRabDetailDto
                        {
                            Kode = i.Kode,
                            Tipe = i.Tipe,
                            Uraian = i.Uraian,
                            Satuan = i.Satuan,
                            HargaSatuan = i.HargaSatuan,
                            Qty = i.Qty,
                            Jumlah = i.Jumlah,
                            Ppn = i.Ppn,
                            Keuntungan = i.Keuntungan,
                            JasaDariBahan = i.JasaDariBahan,
                            Total = i.Total,
                            Kelompok = i.Kelompok,
                            PostBiaya = i.PostBiaya,
                            FlagBiayaDibebankanKePdam = i.FlagBiayaDibebankanKePdam,
                            FlagDialihkanKeVendor = i.FlagDialihkanKeVendor
                        });
                    }
                }

                selectedData.RabWpf.DetailDistribusi = detailPipaDistribusi;
                selectedData.RabWpf.DetailPersil = detailPipaPersil;
            }
        }

        [ExcludeFromCodeCoverage]
        private async Task ShowCetakDialogAsync(string nomorRab)
        {
            if (!_isTest)
            {
                var param = new Dictionary<string, dynamic>()
                {
                    { "IdPermohonan", _viewModel.FormData.IdPermohonan }
                };

                var fitur = "RAB";

                switch (_viewModel.SelectedJenisPelanggan)
                {
                    case "Pelanggan Air":
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganAirRab).IsCetak = true;
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganAirRab).IsPreview = true;
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganAirRab).TemplateName = $"{fitur}PelangganAir_{_viewModel.SelectedTipePermohonanComboBox.KodeTipePermohonan}";
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganAirRab).CustomParam = param;

                        await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                            _isTest,
                            false,
                            "PerencanaanRootDialog",
                            $"Tambah Data Berhasil",
                            $"No RAB : {nomorRab}",
                            "success",
                            _viewModel.OnCetakCommandPelangganAirRab,
                            "Cetak",
                            "Tutup",
                            moduleName: "perencanaan");

                        break;
                    case "Pelanggan Limbah":
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganLimbahRab).IsCetak = true;
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganLimbahRab).IsPreview = true;
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganLimbahRab).TemplateName = $"{fitur}PelangganLimbah_{_viewModel.SelectedTipePermohonanComboBox.KodeTipePermohonan}";
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganLimbahRab).CustomParam = param;

                        await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                            _isTest,
                            false,
                            "PerencanaanRootDialog",
                            $"Tambah Data Berhasil",
                            $"No RAB : {nomorRab}",
                            "success",
                            _viewModel.OnCetakCommandPelangganLimbahRab,
                            "Cetak",
                            "Tutup",
                            moduleName: "perencanaan");

                        break;
                    case "Pelanggan L2T2":
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganLlttRab).IsCetak = true;
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganLlttRab).IsPreview = true;
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganLlttRab).TemplateName = $"{fitur}PelangganLltt_{_viewModel.SelectedTipePermohonanComboBox.KodeTipePermohonan}";
                        ((OnCetakCommand)_viewModel.OnCetakCommandPelangganLlttRab).CustomParam = param;

                        await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                            _isTest,
                            false,
                            "PerencanaanRootDialog",
                            $"Tambah Data Berhasil",
                            $"No RAB : {nomorRab}",
                            "success",
                            _viewModel.OnCetakCommandPelangganLlttRab,
                            "Cetak",
                            "Tutup",
                            moduleName: "perencanaan");

                        break;
                    case "Non Pelanggan":
                        ((OnCetakCommand)_viewModel.OnCetakCommandNonPelangganRab).IsCetak = true;
                        ((OnCetakCommand)_viewModel.OnCetakCommandNonPelangganRab).IsPreview = true;
                        ((OnCetakCommand)_viewModel.OnCetakCommandNonPelangganRab).TemplateName = $"{fitur}NonPelanggan_{_viewModel.SelectedTipePermohonanComboBox.KodeTipePermohonan}";
                        ((OnCetakCommand)_viewModel.OnCetakCommandNonPelangganRab).CustomParam = param;

                        await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                            _isTest,
                            false,
                            "PerencanaanRootDialog",
                            $"Tambah Data Berhasil",
                            $"No RAB : {nomorRab}",
                            "success",
                            _viewModel.OnCetakCommandNonPelangganRab,
                            "Cetak",
                            "Tutup",
                            moduleName: "perencanaan");

                        break;
                }
            }
        }
    }
}
