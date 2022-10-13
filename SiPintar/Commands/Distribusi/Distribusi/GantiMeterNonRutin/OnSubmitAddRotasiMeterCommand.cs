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
using SiPintar.ViewModels.Distribusi.Distribusi;

namespace SiPintar.Commands.Distribusi.Distribusi.GantiMeterNonRutin
{
    [ExcludeFromCodeCoverage]
    public class OnSubmitAddRotasiMeterCommand : AsyncCommandBase
    {
        private readonly GantiMeterNonRutinViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitAddRotasiMeterCommand(GantiMeterNonRutinViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            object dg = null;
            DialogHelper.CloseDialog(_isTest, false, "DistribusiRootDialog");
            dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, false, "DistribusiRootDialog", "Mohon tunggu", "sedang memproses tindakan...", "");

            _viewModel.PelangganDetail = new ObservableCollection<MasterPelangganAirDetailDto>();

            var param = new Dictionary<string, dynamic>
            {
                { "PageSize" , 1000 },
                { "CurrentPage" , 1 },
                { "IdPelangganAir" , _viewModel.SelectedPelanggan.IdPelangganAir },
            };

            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-pelanggan-air-detail", param);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.PelangganDetail = Result.Data.ToObject<ObservableCollection<MasterPelangganAirDetailDto>>();
                }
            }

            var detailPelanggan = _viewModel.PelangganDetail.FirstOrDefault();
            var items = new ParamJadwalGantiMeterDto()
            {
                IdPelangganAir = _viewModel.SelectedPelanggan.IdPelangganAir,
                Tahun = _viewModel.TahunFilter,
                Rutin = false,
                IdJenisGantiMeter = 0,
                IdGolongan = _viewModel.SelectedPelanggan.IdGolongan,
                IdDiameter = _viewModel.SelectedPelanggan.IdDiameter,
                IdRayon = _viewModel.SelectedPelanggan.IdRayon,
                IdKelurahan = _viewModel.SelectedPelanggan.IdKelurahan,
                IdMerekMeter = _viewModel.SelectedPelanggan.IdMerekMeter,
                NoSeriMeter = _viewModel.SelectedPelanggan.NoSeriMeter,

                //Now
                TglMeter = detailPelanggan?.TglMeter != null ? detailPelanggan.TglMeter : DateTime.Now,
                TglJadwal = null,

                Latitude = _viewModel.SelectedPelanggan.Latitude,
                Longitude = _viewModel.SelectedPelanggan.Longitude,
                FlagHapus = false,
                IdJenisNonAir = 4,
                DetailBiaya = new List<ParamJadwalGantiMeterBiayaDto>(),
            };


            Type type = typeof(ParamJadwalGantiMeterDto);
            PropertyInfo[] properties = type.GetProperties();
            var body = new Dictionary<string, dynamic>();
            foreach (PropertyInfo property in properties)
            {
                if (property.Name != "IdPdam" && property.Name != "IdUserRequest")
                {
                    body.Add(property.Name, property.GetValue(items));
                }
            }


            Response = await Task.Run(() => _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/jadwal-ganti-meter", body));
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    DialogHelper.ShowSnackbar(_isTest, "success", Result.Ui_msg, "distribusi");
                }
                else
                {
                    DialogHelper.ShowSnackbar(_isTest, "danger", Result.Ui_msg, "distribusi");
                }
            }
            else
            {
                DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error?.Message, "distribusi");
            }
            //End - Send Data

            DialogHelper.CloseDialog(_isTest, false, "DistribusiRootDialog", dg);
            _ = ((AsyncCommandBase)_viewModel.OnFilterCommand).ExecuteAsync(null);
            await Task.FromResult(_isTest);
        }

    }
}
