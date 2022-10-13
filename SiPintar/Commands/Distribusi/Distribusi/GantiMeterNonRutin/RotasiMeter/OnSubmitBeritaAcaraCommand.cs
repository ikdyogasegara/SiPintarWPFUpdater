using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Distribusi.Distribusi.GantiMeterNonRutin;

namespace SiPintar.Commands.Distribusi.Distribusi.GantiMeterNonRutin.RotasiMeter
{
    public class OnSubmitBeritaAcaraCommand : AsyncCommandBase
    {
        private readonly RotasiMeterNonRutinViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitBeritaAcaraCommand(RotasiMeterNonRutinViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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

            var petugas = new List<ParamPetugasBaJadwalGantiMeterDto>();
            foreach (var item in _viewModel.FormPetugasTambahan)
            {
                petugas.Add(new ParamPetugasBaJadwalGantiMeterDto()
                {
                    IdPegawai = item.IdPegawai
                });
            };

            var detail = new List<ParamJadwalGantiMeterBaDetailDto>()
            {
                new ParamJadwalGantiMeterBaDetailDto()
                {
                    IdPelangganAir = _viewModel.SelectedData.IdPelangganAir,
                    AngkaMeterLama = _viewModel.AngkaMeterLama,
                    AngkaMeterBaru = _viewModel.AngkaMeterBaru,
                    NoSeriMeter = _viewModel.SelectedData.NoSeriMeter,
                    IdDiameter = _viewModel.SelectedData.IdDiameter,
                    IdMerekMeter = _viewModel.SelectedData.IdMerekMeter,
                    FlagBatal = false,
                    Keterangan = _viewModel.Keterangan,
                    IdAlasanBatal = 0,
                }
            };

            var items = new ParamJadwalGantiMeterBaDto()
            {
                Tahun = _viewModel.Parent.TahunFilter,
                Petugas = petugas,
                TanggalBa = _viewModel.TanggalBa,
                IdUser = int.Parse(Application.Current.Resources["IdUser"].ToString() ?? "0"),
                Detail = detail
            };


            Type type = typeof(ParamJadwalGantiMeterBaDto);
            PropertyInfo[] properties = type.GetProperties();
            var body = new Dictionary<string, dynamic>();
            foreach (PropertyInfo property in properties)
            {
                if (property.Name != "IdPdam" && property.Name != "IdUserRequest")
                {
                    body.Add(property.Name, property.GetValue(items));
                }
            }


            var Response = await Task.Run(() => _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/jadwal-ganti-meter-berita-acara", body));
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

            _ = UploadFotoAsync();

            DialogHelper.CloseDialog(_isTest, false, "DistribusiRootDialog", dg);

            var filter = _viewModel.Parent.Filter;
            _viewModel.Parent.OnFilterCommand.Execute(filter);
            await Task.FromResult(_isTest);
        }

        private async Task UploadFotoAsync()
        {
            if (_viewModel.FotoMeterLamaUri == null && _viewModel.FotoMeterBaruUri == null)
                return;

            string SuccessMessage = null;
            string ErrorMessage = null;


            var body = new Dictionary<string, dynamic>
            {
                {"Tahun" , _viewModel.Parent.TahunFilter},
                { "IdPelangganAir", _viewModel.SelectedData.IdPelangganAir}
            };

            if (_viewModel.FotoMeterLamaUri != null && !_viewModel.FotoMeterLamaUri.OriginalString.ToLower().Contains("http"))
                body.Add("file_Bukti1", _viewModel.FotoMeterLamaUri.OriginalString);
            if (_viewModel.FotoMeterBaruUri != null && !_viewModel.FotoMeterBaruUri.OriginalString.ToLower().Contains("http"))
                body.Add("file_Bukti2", _viewModel.FotoMeterBaruUri.OriginalString);


            var Response = await Task.Run(() => _restApi.UploadAsync($"/api/{_restApi.GetApiVersion()}/jadwal-ganti-meter-berita-acara-upload-foto", body));
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                    SuccessMessage = Response.Data.Ui_msg;
                else
                    ErrorMessage = Response.Data.Ui_msg;
            }
            else
            {
                ErrorMessage = Response.Error.Message;
            }

            if (ErrorMessage != null)
                DialogHelper.ShowSnackbar(_isTest, "danger", ErrorMessage, "distribusi");

            if (SuccessMessage != null)
                Debug.WriteLine(SuccessMessage);

            await Task.FromResult(_isTest);
        }
    }

}
