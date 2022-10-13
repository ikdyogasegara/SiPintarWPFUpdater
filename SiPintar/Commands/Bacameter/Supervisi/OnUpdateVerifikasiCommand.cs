using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Bacameter;

namespace SiPintar.Commands.Bacameter.Supervisi
{
    public class OnUpdateVerifikasiCommand : AsyncCommandBase
    {
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnUpdateVerifikasiCommand(IRestApiClientModel restApi, bool isTest = false)
        {
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var param = parameter as RekeningAirWpf;
            if (param != null)
            {
                param.IsUpdatingVerifikasi = true;
                await Task.Delay(500);
                if (param.FlagVerifikasiWpf.HasValue && param.FlagVerifikasiWpf.Value)
                {
                    var paramSend = new Dictionary<string, dynamic>
                    {
                        {"IdPeriode" , param.IdPeriode},
                        {"IdPelangganAir" , new List<int?>() { param.IdPelangganAir }},
                    };

                    var response = await Task.Run(() => _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/rekening-air-unverifikasi", null, paramSend));
                    if (!response.IsError)
                    {
                        var result = response.Data;
                        if (result.Status)
                        {
                            param.FlagVerifikasiWpf = false;
                        }
                        else
                        {
                            DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg, "bacameter");

                        }
                    }
                    else
                    {
                        DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message, "bacameter");
                    }
                }
                else
                {
                    var waktu = DateTime.Now;
                    var paramSend = new Dictionary<string, dynamic>
                    {
                        {"IdPeriode" , param.IdPeriode},
                        {"IdPelangganAir" , new List<int?>() { param.IdPelangganAir }},
                        { "WaktuVerifikasi", waktu }
                    };

                    var response = await Task.Run(() => _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/rekening-air-verifikasi", null, paramSend));
                    if (!response.IsError)
                    {
                        var result = response.Data;
                        if (result.Status)
                        {
                            param.FlagVerifikasiWpf = true;
                            param.WaktuVerifikasiWpf = waktu;
                        }
                        else
                        {
                            DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg, "bacameter");
                        }
                    }
                    else
                    {
                        DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message, "bacameter");
                    }
                }
                param.IsUpdatingVerifikasi = false;
            }
            _ = _isTest;
        }
    }
}
