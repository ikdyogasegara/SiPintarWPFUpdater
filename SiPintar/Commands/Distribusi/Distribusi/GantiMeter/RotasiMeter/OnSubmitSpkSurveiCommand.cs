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
    public class OnSubmitSpkSurveiCommand : AsyncCommandBase
    {
        private readonly RotasiMeterViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitSpkSurveiCommand(RotasiMeterViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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

            var ListIdPelanggan = new List<int>();
            if (_viewModel.DataWpfList != null)
            {
                foreach (var item in _viewModel.DataWpfList)
                {
                    if (item.IsSelected == true && item.IdPelangganAir != null)
                    {
                        ListIdPelanggan.Add(Convert.ToInt32(item.IdPelangganAir));
                    }
                }
            }

            var petugas = new List<ParamPetugasSpkJadwalGantiMeterDto>();
            foreach (var item in _viewModel.FormPetugasTambahan)
            {
                petugas.Add(new ParamPetugasSpkJadwalGantiMeterDto()
                {
                    IdPegawai = item.IdPegawai
                });
            };

            var param = new Dictionary<string, dynamic>
            {
                {"Tahun" , _viewModel.Parent.TahunFilter},
                {"TanggalSpk" , DateTime.Now},
                {"IdUser" , Application.Current.Resources["IdUser"].ToString()},
                {"IdPelangganAir" , ListIdPelanggan},
                {"Petugas" , petugas},
                {"Sppb" , new List<ParamJadwalGantiMeterSpkSPPBDto>()},
            };

            var Response = await Task.Run(() => _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/jadwal-ganti-meter-spk", param));
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

            var filter = _viewModel.Parent.Filter;
            _viewModel.Parent.OnFilterCommand.Execute(filter);
            await Task.FromResult(_isTest);
        }

    }
}
