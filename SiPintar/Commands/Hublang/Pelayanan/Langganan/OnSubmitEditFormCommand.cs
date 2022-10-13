using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Commands.Hublang.Pelayanan.Langganan
{
    public class OnSubmitEditFormCommand : AsyncCommandBase
    {
        private readonly LanggananViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitEditFormCommand(LanggananViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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

            var response = await _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/{_viewModel.EndPoint}", null, body);
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    UpdateRecord(selectedData, result.Data.ToObject<ObservableCollection<MasterPelangganGlobalWpf>>());
                    await UploadFotoAsync(selectedData);
                    _viewModel.IsLoadingForm = false;
                    selectedData.IsUpdatingKoreksi = false;
                    DialogHelper.ShowSnackbar(_isTest, "success", response.Data.Ui_msg, "hublang");
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
        private void UpdateRecord(MasterPelangganGlobalWpf selectedData, ObservableCollection<MasterPelangganGlobalWpf> resultData)
        {
            if (!_isTest && resultData != null)
            {
                selectedData.NamaWpf = resultData[0].NamaWpf;
                selectedData.AlamatWpf = resultData[0].AlamatWpf;
                selectedData.IdGolonganWpf = resultData[0].IdGolonganWpf;
                selectedData.KodeGolonganWpf = resultData[0].KodeGolonganWpf;
                selectedData.NamaGolonganWpf = resultData[0].NamaGolonganWpf;
                selectedData.IdDiameterWpf = resultData[0].IdDiameterWpf;
                selectedData.KodeDiameterWpf = resultData[0].KodeDiameterWpf;
                selectedData.NamaDiameterWpf = resultData[0].NamaDiameterWpf;
                selectedData.IdTarifLimbahWpf = resultData[0].IdTarifLimbahWpf;
                selectedData.KodeTarifLimbahWpf = resultData[0].KodeTarifLimbahWpf;
                selectedData.NamaTarifLimbahWpf = resultData[0].NamaTarifLimbahWpf;
                selectedData.IdTarifLlttWpf = resultData[0].IdTarifLlttWpf;
                selectedData.KodeTarifLlttWpf = resultData[0].KodeTarifLlttWpf;
                selectedData.NamaTarifLlttWpf = resultData[0].NamaTarifLlttWpf;
                selectedData.NoHpWpf = resultData[0].NoHpWpf;
                selectedData.NoTelpWpf = resultData[0].NoTelpWpf;
                selectedData.NoKkWpf = resultData[0].NoKkWpf;
                selectedData.NoKtpWpf = resultData[0].NoKtpWpf;
                selectedData.EmailWpf = resultData[0].EmailWpf;
                selectedData.IdRayonWpf = resultData[0].IdRayonWpf;
                selectedData.KodeRayonWpf = resultData[0].KodeRayonWpf;
                selectedData.NamaRayonWpf = resultData[0].NamaRayonWpf;
                selectedData.IdAreaWpf = resultData[0].IdAreaWpf;
                selectedData.KodeAreaWpf = resultData[0].KodeAreaWpf;
                selectedData.NamaAreaWpf = resultData[0].NamaAreaWpf;
                selectedData.IdWilayahWpf = resultData[0].IdWilayahWpf;
                selectedData.KodeWilayahWpf = resultData[0].KodeWilayahWpf;
                selectedData.NamaWilayahWpf = resultData[0].NamaWilayahWpf;
                selectedData.IdKelurahanWpf = resultData[0].IdKelurahanWpf;
                selectedData.KodeKelurahanWpf = resultData[0].KodeKelurahanWpf;
                selectedData.NamaKelurahanWpf = resultData[0].NamaKelurahanWpf;
                selectedData.IdKecamatanWpf = resultData[0].IdKecamatanWpf;
                selectedData.KodeKecamatanWpf = resultData[0].KodeKecamatanWpf;
                selectedData.NamaKecamatanWpf = resultData[0].NamaKecamatanWpf;
                selectedData.IdCabangWpf = resultData[0].IdCabangWpf;
                selectedData.KodeCabangWpf = resultData[0].KodeCabangWpf;
                selectedData.NamaCabangWpf = resultData[0].NamaCabangWpf;
                selectedData.IdStatusWpf = resultData[0].IdStatusWpf;
                selectedData.NamaStatusWpf = resultData[0].NamaStatusWpf;
                selectedData.IdFlagWpf = resultData[0].IdFlagWpf;
                selectedData.NamaFlagWpf = resultData[0].NamaFlagWpf;
                selectedData.IdKolektifWpf = resultData[0].IdKolektifWpf;
                selectedData.KodeKolektifWpf = resultData[0].KodeKolektifWpf;
                selectedData.NamaKolektifWpf = resultData[0].NamaKolektifWpf;
                selectedData.IdAdministrasiLainWpf = resultData[0].IdAdministrasiLainWpf;
                selectedData.KodeAdministrasiLainWpf = resultData[0].KodeAdministrasiLainWpf;
                selectedData.NamaAdministrasiLainWpf = resultData[0].NamaAdministrasiLainWpf;
                selectedData.IdPemeliharaanLainWpf = resultData[0].IdPemeliharaanLainWpf;
                selectedData.KodePemeliharaanLainWpf = resultData[0].KodePemeliharaanLainWpf;
                selectedData.NamaPemeliharaanLainWpf = resultData[0].NamaPemeliharaanLainWpf;
                selectedData.IdRetribusiLainWpf = resultData[0].IdRetribusiLainWpf;
                selectedData.KodeRetribusiLainWpf = resultData[0].KodeRetribusiLainWpf;
                selectedData.NamaRetribusiLainWpf = resultData[0].NamaRetribusiLainWpf;
                selectedData.IdBlokWpf = resultData[0].IdBlokWpf;
                selectedData.NamaBlokWpf = resultData[0].NamaBlokWpf;
                selectedData.PenghuniWpf = resultData[0].PenghuniWpf;
                selectedData.IdJenisBangunanWpf = resultData[0].IdJenisBangunanWpf;
                selectedData.NamaJenisBangunanWpf = resultData[0].NamaJenisBangunanWpf;
                selectedData.IdKepemilikanWpf = resultData[0].IdKepemilikanWpf;
                selectedData.NamaKepemilikanWpf = resultData[0].NamaKepemilikanWpf;
                selectedData.IdPekerjaanWpf = resultData[0].IdPekerjaanWpf;
                selectedData.NamaPekerjaanWpf = resultData[0].NamaPekerjaanWpf;
                selectedData.IdPeruntukanWpf = resultData[0].IdPeruntukanWpf;
                selectedData.NamaPeruntukanWpf = resultData[0].NamaPeruntukanWpf;
                selectedData.IdSumberAirWpf = resultData[0].IdSumberAirWpf;
                selectedData.KodeSumberAirWpf = resultData[0].KodeSumberAirWpf;
                selectedData.NamaSumberAirWpf = resultData[0].NamaSumberAirWpf;
                selectedData.RtWpf = resultData[0].RtWpf;
                selectedData.RwWpf = resultData[0].RwWpf;
                selectedData.LatitudeWpf = resultData[0].LatitudeWpf;
                selectedData.LongitudeWpf = resultData[0].LongitudeWpf;
                selectedData.AlamatMapWpf = resultData[0].AlamatMapWpf;
                selectedData.DayaListrikWpf = resultData[0].DayaListrikWpf;
                selectedData.LuasRumahWpf = resultData[0].LuasRumahWpf;
                selectedData.LuasTanahWpf = resultData[0].LuasTanahWpf;
                selectedData.KodePostWpf = resultData[0].KodePostWpf;
                selectedData.UrutanBacaWpf = resultData[0].UrutanBacaWpf;
                selectedData.NoSeriMeterWpf = resultData[0].NoSeriMeterWpf;
                selectedData.IdMerekMeterWpf = resultData[0].IdMerekMeterWpf;
                selectedData.KodeMerekMeterWpf = resultData[0].KodeMerekMeterWpf;
                selectedData.NamaMerekMeterWpf = resultData[0].NamaMerekMeterWpf;
                selectedData.IdKondisiMeterWpf = resultData[0].IdKondisiMeterWpf;
                selectedData.KodeKondisiMeterWpf = resultData[0].KodeKondisiMeterWpf;
                selectedData.NamaKondisiMeterWpf = resultData[0].NamaKondisiMeterWpf;
                selectedData.IdDmaWpf = resultData[0].IdDmaWpf;
                selectedData.KodeDmaWpf = resultData[0].KodeDmaWpf;
                selectedData.NamaDmaWpf = resultData[0].NamaDmaWpf;
                selectedData.IdDmzWpf = resultData[0].IdDmzWpf;
                selectedData.KodeDmzWpf = resultData[0].KodeDmzWpf;
                selectedData.NamaDmzWpf = resultData[0].NamaDmzWpf;
                selectedData.DayaListrikWpf = resultData[0].DayaListrikWpf;
                selectedData.KeteranganWpf = resultData[0].KeteranganWpf;
                selectedData.NamaPemilikWpf = resultData[0].NamaPemilikWpf;
                selectedData.AlamatPemilikWpf = resultData[0].AlamatPemilikWpf;
            }
        }

        [ExcludeFromCodeCoverage]
        private async Task UploadFotoAsync(MasterPelangganGlobalWpf selectedData)
        {
            if (!_isTest && (_viewModel.IsNewFotoRumah1 || _viewModel.IsNewFotoRumah2 || _viewModel.IsNewFotoRumah3))
            {
                var bodyUpload = new Dictionary<string, dynamic>();

                switch (_viewModel.SelectedJenisPelanggan)
                {
                    case "Pelanggan Air":
                        bodyUpload.Add("IdPelangganAir", _viewModel.FormData.IdPelangganAir);
                        break;
                    case "Pelanggan Limbah":
                        bodyUpload.Add("IdPelangganLimbah", _viewModel.FormData.IdPelangganLimbah);
                        break;
                    case "Pelanggan L2T2":
                        bodyUpload.Add("IdPelangganLltt", _viewModel.FormData.IdPelangganLltt);
                        break;
                }

                if (_viewModel.FotoRumah1Uri != null && !_viewModel.FotoRumah1Uri.OriginalString.ToLower().Contains("http") && _viewModel.IsNewFotoRumah1)
                    bodyUpload.Add("file_FotoRumah1", _viewModel.FotoRumah1Uri.OriginalString);
                if (_viewModel.FotoRumah2Uri != null && !_viewModel.FotoRumah2Uri.OriginalString.ToLower().Contains("http") && _viewModel.IsNewFotoRumah2)
                    bodyUpload.Add("file_FotoRumah2", _viewModel.FotoRumah2Uri.OriginalString);
                if (_viewModel.FotoRumah3Uri != null && !_viewModel.FotoRumah3Uri.OriginalString.ToLower().Contains("http") && _viewModel.IsNewFotoRumah3)
                    bodyUpload.Add("file_FotoRumah3", _viewModel.FotoRumah3Uri.OriginalString);

                var responseUpload = await _restApi.UploadAsync($"/api/{_restApi.GetApiVersion()}/{_viewModel.EndPoint}-upload-foto", bodyUpload);
                if (!responseUpload.IsError)
                {
                    var resultUpload = responseUpload.Data;
                    if (resultUpload.Status)
                    {
                        var tempFoto = resultUpload.Data.ToObject<ObservableCollection<NamaFotoDto>>();
                        if (tempFoto != null)
                        {
                            if (!string.IsNullOrWhiteSpace(tempFoto[0].FotoRumah1))
                                selectedData.FotoRumah1Wpf = tempFoto[0].FotoRumah1;
                            if (!string.IsNullOrWhiteSpace(tempFoto[0].FotoRumah2))
                                selectedData.FotoRumah2Wpf = tempFoto[0].FotoRumah2;
                            if (!string.IsNullOrWhiteSpace(tempFoto[0].FotoRumah3))
                                selectedData.FotoRumah3Wpf = tempFoto[0].FotoRumah3;
                        }
                    }

                    DialogHelper.ShowSnackbar(_isTest, "success", "Upload Foto Berhasil", "hublang");
                }
            }
        }
    }
}
