using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan.KoreksiRekeningAir;

namespace SiPintar.Commands.Hublang.Pelayanan.KoreksiRekeningAir.UsulanKoreksi
{
    public class OnSubmitAddCommand : AsyncCommandBase
    {
        private readonly UsulanKoreksiViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private string _successMessage;
        private string _errorMessage;

        public OnSubmitAddCommand(UsulanKoreksiViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.PiutangAirList == null)
                return;

            _successMessage = null;
            _errorMessage = null;

            _viewModel.IsLoadingForm = true;

            DialogHelper.ShowLoading(_isTest, "HublangRootDialog");

            await ProsesAsync();

            if (App.OpenedWindow.ContainsKey("hublang"))
                DialogHelper.ShowSuccessErrorDialog(_errorMessage, _successMessage, _isTest, "HublangRootDialog", App.OpenedWindow["hublang"], true, _viewModel.OnRefreshCommand);

            _viewModel.IsLoadingForm = false;
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private async Task ProsesAsync()
        {
            foreach (var item in _viewModel.PiutangAirList)
            {
                if (item.IsInputKoreksi == true)
                {
                    var body = new Dictionary<string, dynamic>
                    {
                        {"IdPeriode", item.IdPeriode},
                        {"WaktuUsulan", DateTime.Now},
                        {"IdPermohonan", _viewModel.SelectedPermohonanAir.IdPermohonan},
                        {"IdRayon", item.IdRayon},
                        {"IdKelurahan", item.IdKelurahan},
                        {"IdGolongan", item.IdGolongan},
                        {"IdDiameter", item.IdDiameter},
                        {"IdUser", Application.Current.Resources["IdUser"].ToString()},
                        {"Keterangan", _viewModel.SelectedPermohonanAir.Keterangan},
                        {"IdPelangganAir", item.IdPelangganAir},
                        {"StanLalu", item.StanLalu},
                        {"StanSkrg", item.StanSkrg},
                        {"StanAngkat", item.StanAngkat},
                        {"Pakai", item.Pakai},
                        {"BiayaPemakaian", item.BiayaPemakaian},
                        {"Administrasi", item.Administrasi},
                        {"Pemeliharaan", item.Pemeliharaan},
                        {"Retribusi", item.Retribusi},
                        {"Pelayanan", item.Pelayanan},
                        {"AirLimbah", item.AirLimbah},
                        {"DendaPakai0", item.DendaPakai0},
                        {"AdministrasiLain", item.AdministrasiLain},
                        {"PemeliharaanLain", item.PemeliharaanLain},
                        {"RetribusiLain", item.RetribusiLain},
                        {"Ppn", item.Ppn},
                        {"Meterai", item.Meterai},
                        {"RekAir", item.Rekair},
                        {"Denda", item.Denda},
                        {"Total", item.Total},
                        {"FlagHanyaAbonemen", item.FlagHanyaAbonemen},
                        {"StanLalu_Usulan", item.StanLalu_Usulan},
                        {"StanSkrg_Usulan", item.StanSkrg_Usulan},
                        {"StanAngkat_Usulan", item.StanAngkat_Usulan},
                        {"Pakai_Usulan", item.Pakai_Usulan},
                        {"BiayaPemakaian_Usulan", item.BiayaPemakaian_Usulan},
                        {"Administrasi_Usulan", item.Administrasi_Usulan},
                        {"Pemeliharaan_Usulan", item.Pemeliharaan_Usulan},
                        {"Retribusi_Usulan", item.Retribusi_Usulan},
                        {"Pelayanan_Usulan", item.Pelayanan_Usulan},
                        {"AirLimbah_Usulan", item.AirLimbah_Usulan},
                        {"DendaPakai0_Usulan", item.DendaPakai0_Usulan},
                        {"AdministrasiLain_Usulan", item.AdministrasiLain_Usulan},
                        {"PemeliharaanLain_Usulan", item.PemeliharaanLain_Usulan},
                        {"RetribusiLain_Usulan", item.RetribusiLain_Usulan},
                        {"Ppn_Usulan", item.Ppn_Usulan},
                        {"Meterai_Usulan", item.Meterai_Usulan},
                        {"RekAir_Usulan", item.Rekair_Usulan},
                        {"Denda_Usulan", item.Denda_Usulan},
                        {"Total_Usulan", item.Total_Usulan},
                    };

                    var response = await Task.Run(() => _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/permohonan-koreksi-rekening-air", body));
                    if (!response.IsError)
                    {
                        var result = response.Data;
                        if (result.Status)
                        {
                            await UploadFotoAsync(item);
                            _successMessage = response.Data.Ui_msg;
                        }
                        else
                            _errorMessage = response.Data.Ui_msg;
                    }
                    else
                        _errorMessage = response.Error.Message;
                }
            }
        }

        [ExcludeFromCodeCoverage]
        private async Task UploadFotoAsync(RekeningAirPiutangWpf item)
        {
            if (!_isTest && (item.IsNewFotoBukti1 == true || item.IsNewFotoBukti2 == true || item.IsNewFotoBukti3 == true))
            {
                var bodyUpload = new Dictionary<string, dynamic>
                {
                    { "IdPermohonan", _viewModel.SelectedPermohonanAir.IdPermohonan },
                    { "IdPeriode", item.IdPeriode }
                };

                if (item.FotoBukti1Uri != null && !item.FotoBukti1Uri.OriginalString.ToLower().Contains("http") && item.IsNewFotoBukti1)
                    bodyUpload.Add("file_FotoBukti1", item.FotoBukti1Uri.OriginalString);
                if (item.FotoBukti2Uri != null && !item.FotoBukti2Uri.OriginalString.ToLower().Contains("http") && item.IsNewFotoBukti2)
                    bodyUpload.Add("file_FotoBukti2", item.FotoBukti2Uri.OriginalString);
                if (item.FotoBukti3Uri != null && !item.FotoBukti3Uri.OriginalString.ToLower().Contains("http") && item.IsNewFotoBukti3)
                    bodyUpload.Add("file_FotoBukti3", item.FotoBukti3Uri.OriginalString);

                var responseUpload = await Task.Run(() => _restApi.UploadAsync($"/api/{_restApi.GetApiVersion()}/permohonan-koreksi-rekening-upload-foto", bodyUpload));
                if (!responseUpload.IsError)
                {
                    var resultUpload = responseUpload.Data;
                    if (resultUpload.Status)
                    {
                        DialogHelper.ShowSnackbar(_isTest, "success", "Upload Foto Berhasil", "hublang");
                    }
                }
                else
                    _errorMessage = responseUpload.Error.Message;
            }
        }
    }
}
