using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Distribusi.Distribusi.GantiMeter;

namespace SiPintar.Commands.Distribusi.Distribusi.GantiMeter.RotasiMeter
{
    public class OnSubmitBeritaAcaraCommand : AsyncCommandBase
    {
        private readonly RotasiMeterViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitBeritaAcaraCommand(RotasiMeterViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            DialogHelper.CloseDialog(_isTest, false, "DistribusiRootDialog");
            _viewModel.IsLoading = true;


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
                TanggalBa = DateTime.Now,
                IdUser = int.Parse(Application.Current.Resources["IdUser"].ToString() ?? string.Empty),
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

            var filter = _viewModel.Parent.Filter;
            _viewModel.Parent.OnFilterCommand.Execute(filter);
            await Task.FromResult(_isTest);
        }
    }
}
