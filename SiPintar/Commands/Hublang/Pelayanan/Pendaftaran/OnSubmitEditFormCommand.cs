using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Commands.Hublang.Pelayanan.Pendaftaran
{
    public class OnSubmitEditFormCommand : AsyncCommandBase
    {
        private readonly PendaftaranViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitEditFormCommand(PendaftaranViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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

            var body = parameter as Dictionary<string, dynamic>;

            var response = await _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/permohonan-non-pelanggan", null, body);
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    UpdateRecord(selectedData);
                    DialogHelper.ShowSnackbar(_isTest, "success", response.Data.Ui_msg, "hublang");
                    await UploadFotoAsync(selectedData);

                    _viewModel.IsLoadingForm = false;
                    selectedData.IsUpdatingKoreksi = false;
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg, "hublang");
            }
            else
            {
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message, "hublang");
            }

            _viewModel.IsLoadingForm = false;
            selectedData.IsUpdatingKoreksi = false;
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private async Task UploadFotoAsync(PermohonanNonPelangganWpf selectedData)
        {
            if (!_isTest && (_viewModel.IsNewFotoSuratPernyataan == true || _viewModel.IsNewFotoKk == true || _viewModel.IsNewFotoKtp == true || _viewModel.IsNewFotoImb == true))
            {
                var bodyUpload = new Dictionary<string, dynamic>
                {
                    { "IdPermohonan", _viewModel.FormData.IdPermohonan }
                };

                if (_viewModel.FotoSuratPernyataanUri != null && !_viewModel.FotoSuratPernyataanUri.OriginalString.ToLower().Contains("http") && _viewModel.IsNewFotoSuratPernyataan)
                    bodyUpload.Add("file_FotoSuratPernyataan", _viewModel.FotoSuratPernyataanUri.OriginalString);
                if (_viewModel.FotoKkUri != null && !_viewModel.FotoKkUri.OriginalString.ToLower().Contains("http") && _viewModel.IsNewFotoKk)
                    bodyUpload.Add("file_FotoKK", _viewModel.FotoKkUri.OriginalString);
                if (_viewModel.FotoKtpUri != null && !_viewModel.FotoKtpUri.OriginalString.ToLower().Contains("http") && _viewModel.IsNewFotoKtp)
                    bodyUpload.Add("file_FotoKtp", _viewModel.FotoKtpUri.OriginalString);
                if (_viewModel.FotoImbUri != null && !_viewModel.FotoImbUri.OriginalString.ToLower().Contains("http") && _viewModel.IsNewFotoImb)
                    bodyUpload.Add("file_FotoImb", _viewModel.FotoImbUri.OriginalString);

                var responseUpload = await Task.Run(() => _restApi.UploadAsync($"/api/{_restApi.GetApiVersion()}/permohonan-non-pelanggan-upload-foto", bodyUpload));
                if (!responseUpload.IsError)
                {
                    var resultUpload = responseUpload.Data;
                    if (resultUpload.Status)
                    {
                        var tempFoto = resultUpload.Data.ToObject<ObservableCollection<NamaFotoDto>>();

                        if (!string.IsNullOrWhiteSpace(tempFoto[0].FotoSuratPernyataan))
                            selectedData.FotoSuratPernyataanWpf = tempFoto[0].FotoSuratPernyataan;
                        if (!string.IsNullOrWhiteSpace(tempFoto[0].FotoImb))
                            selectedData.FotoImbWpf = tempFoto[0].FotoImb;
                        if (!string.IsNullOrWhiteSpace(tempFoto[0].FotoKk))
                            selectedData.FotoKkWpf = tempFoto[0].FotoKk;
                        if (!string.IsNullOrWhiteSpace(tempFoto[0].FotoKtp))
                            selectedData.FotoKtpWpf = tempFoto[0].FotoKtp;
                    }
                }
            }
        }

        [ExcludeFromCodeCoverage]
        private void UpdateRecord(PermohonanNonPelangganWpf selectedData)
        {
            if (!_isTest)
            {
                selectedData.NomorPermohonanWpf = _viewModel.FormData.NomorPermohonan;
                selectedData.WaktuPermohonanWpf = _viewModel.FormData.WaktuPermohonan;
                selectedData.NamaWpf = _viewModel.FormData.Nama;
                selectedData.AlamatWpf = _viewModel.FormData.Alamat;
                selectedData.NoHpWpf = _viewModel.FormData.NoHp;
                selectedData.IdRayonWpf = _viewModel.FormData.IdRayon;
                selectedData.KodeRayonWpf = _viewModel.RayonForm != null ? _viewModel.RayonForm.KodeRayon : "-";
                selectedData.NamaRayonWpf = _viewModel.RayonForm != null ? _viewModel.RayonForm.NamaRayon : "-";
                selectedData.NamaWilayahWpf = _viewModel.RayonForm != null ? _viewModel.RayonForm.NamaWilayah : "-";
                selectedData.IdKelurahanWpf = _viewModel.FormData.IdKelurahan;
                selectedData.KodeKelurahanWpf = _viewModel.KelurahanForm != null ? _viewModel.KelurahanForm.KodeKelurahan : "-";
                selectedData.NamaKelurahanWpf = _viewModel.KelurahanForm != null ? _viewModel.KelurahanForm.NamaKelurahan : "-";
                selectedData.IdBlokWpf = _viewModel.FormData.IdBlok;
                selectedData.NamaBlokWpf = _viewModel.BlokForm != null ? _viewModel.BlokForm.NamaBlok : "-";
                selectedData.PenghuniWpf = _viewModel.FormData.Penghuni;
                selectedData.IdJenisBangunanWpf = _viewModel.FormData.IdJenisBangunan;
                selectedData.NamaJenisBangunanWpf = _viewModel.JenisBangunanForm != null ? _viewModel.JenisBangunanForm.NamaJenisBangunan : "-";
                selectedData.IdPekerjaanWpf = _viewModel.FormData.IdPekerjaan;
                selectedData.NamaPekerjaanWpf = _viewModel.PekerjaanForm != null ? _viewModel.PekerjaanForm.NamaPekerjaan : "-";
                selectedData.NosambDepanWpf = _viewModel.FormData.NosambDepan;
                selectedData.NosambBelakangWpf = _viewModel.FormData.NosambBelakang;
                selectedData.NosambKiriWpf = _viewModel.FormData.NosambKiri;
                selectedData.NosambKananWpf = _viewModel.FormData.NosambKanan;
                selectedData.AlamatMapWpf = _viewModel.FormData.AlamatMap;
                selectedData.RtWpf = _viewModel.FormData.Rt;
                selectedData.RwWpf = _viewModel.FormData.Rw;
                selectedData.LatitudeWpf = _viewModel.FormData.Latitude;
                selectedData.LongitudeWpf = _viewModel.FormData.Longitude;
                selectedData.FlagLanjutKeLanggananLimbahWpf = _viewModel.FormData.FlagLanjutKeLanggananLimbah;
                selectedData.KeteranganWpf = _viewModel.FormData.Keterangan;
                selectedData.NoKtpWpf = _viewModel.FormData.NoKtp;
                selectedData.NoKkWpf = _viewModel.FormData.NoKk;
                selectedData.EmailWpf = _viewModel.FormData.Email;
                selectedData.LuasTanahWpf = _viewModel.FormData.LuasTanah;
                selectedData.LuasRumahWpf = _viewModel.FormData.LuasRumah;
                selectedData.IdKepemilikanWpf = _viewModel.FormData.IdKepemilikan;
                selectedData.NamaKepemilikanWpf = _viewModel.FormData.NamaKepemilikan;
                selectedData.IdPeruntukanWpf = _viewModel.FormData.IdPeruntukan;
                selectedData.NamaPeruntukanWpf = _viewModel.FormData.NamaPeruntukan;
                selectedData.IdSumberAirWpf = _viewModel.FormData.IdSumberAir;
                selectedData.KodePostWpf = _viewModel.FormData.KodePost;
                selectedData.DayaListrikWpf = _viewModel.FormData.DayaListrik;
                selectedData.NamaPemilikWpf = _viewModel.FormData.NamaPemilik;
                selectedData.AlamatPemilikWpf = _viewModel.FormData.AlamatPemilik;
            }
        }
    }
}
