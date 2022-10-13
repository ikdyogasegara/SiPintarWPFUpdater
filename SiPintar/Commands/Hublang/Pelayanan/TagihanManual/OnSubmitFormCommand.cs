using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Commands.Hublang.Pelayanan.TagihanManual
{
    public class OnSubmitFormCommand : AsyncCommandBase
    {
        private readonly TagihanManualViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitFormCommand(TagihanManualViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            DialogHelper.CloseDialog(_isTest, false, "HublangRootDialog");
            object dg = null;

            _viewModel.FormData.Detail = _viewModel.FormDataDetail.ToList();

            RestApiResponse response = null;
            var selectedData = _viewModel.SelectedData;

            if (_viewModel.SelectedPelanggan != null)
            {
                if (_viewModel.SelectedPelanggan is MasterPelangganAirDto)
                {
                    var temp = _viewModel.SelectedPelanggan as MasterPelangganAirDto;
                    _viewModel.FormData.IdPelangganAir = temp.IdPelangganAir;
                }
                if (_viewModel.SelectedPelanggan is MasterPelangganLimbahDto)
                {
                    var temp = _viewModel.SelectedPelanggan as MasterPelangganLimbahDto;
                    _viewModel.FormData.IdPelangganLimbah = temp.IdPelangganAir;
                }
                if (_viewModel.SelectedPelanggan is MasterPelangganLlttDto)
                {
                    var temp = _viewModel.SelectedPelanggan as MasterPelangganLlttDto;
                    _viewModel.FormData.IdPelangganLltt = temp.IdPelangganAir;
                }
            }

            var type = typeof(RekeningNonAirDto);
            var properties = type.GetProperties();
            var body = new Dictionary<string, dynamic>();
            foreach (var property in properties)
            {
                if (property.Name != "IdPdam" && property.Name != "IdUserRequest")
                {
                    body.Add(property.Name, property.GetValue(_viewModel.FormData));
                }
            }

            if (_viewModel.IsEdit)
            {
                selectedData.IsUpdatingKoreksi = true;
                response = await _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/rekening-non-air", null, body);
            }
            else
            {
                dg = await DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, false, "HublangRootDialog", "Mohon tunggu", "sedang memproses tindakan...", "");
                response = await Task.Run(() => _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/rekening-non-air", body));
                DialogHelper.CloseDialog(_isTest, false, "HublangRootDialog", dg);
            }

            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    if (_viewModel.IsEdit)
                    {
                        UpdateRecord(selectedData);
                        selectedData.IsUpdatingKoreksi = false;
                    }

                    DialogHelper.ShowSnackbar(_isTest, "success", result.Ui_msg);
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg);
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message);


            if (!_viewModel.IsEdit)
            {
                await Task.Run(() => ((AsyncCommandBase)_viewModel.OnRefreshCommand).ExecuteAsync(null));
            }

            selectedData.IsUpdatingKoreksi = false;
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void UpdateRecord(RekeningNonAirWpf selectedData)
        {
            if (!_isTest)
            {
                selectedData.KeteranganWpf = _viewModel.FormData.Keterangan;
                selectedData.IdRayonWpf = _viewModel.RayonForm.IdRayon;
                selectedData.KodeRayonWpf = _viewModel.RayonForm.KodeRayon;
                selectedData.NamaRayonWpf = _viewModel.RayonForm.NamaRayon;
                selectedData.KodeWilayahWpf = _viewModel.RayonForm.KodeWilayah;
                selectedData.NamaWilayahWpf = _viewModel.RayonForm.NamaWilayah;
                selectedData.KodeKelurahanWpf = _viewModel.KelurahanForm.KodeKelurahan;
                selectedData.NamaKelurahanWpf = _viewModel.KelurahanForm.NamaKelurahan;
                selectedData.NamaWpf = _viewModel.FormData.Nama;
                selectedData.AlamatWpf = _viewModel.FormData.Alamat;
                selectedData.TanggalMulaiTagihWpf = _viewModel.FormData.TanggalMulaiTagih;
                selectedData.TotalWpf = _viewModel.FormData.Total;

                selectedData.DetailWpf = new List<RekeningNonAirDetailDto>();
                foreach (var i in _viewModel.FormDataDetail)
                {
                    selectedData.DetailWpf.Add(new RekeningNonAirDetailDto { Parameter = i.Parameter, Value = i.Value, PostBiaya = i.PostBiaya });
                }
            }
        }
    }
}
