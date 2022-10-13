using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Bacameter;

namespace SiPintar.Commands.Bacameter.Supervisi
{
    public class OnSubmitKoreksiCommand : AsyncCommandBase
    {
        private readonly SupervisiViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitKoreksiCommand(SupervisiViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var selectedData = _viewModel.SelectedData;
            selectedData.IsUpdatingKoreksi = true;

            var param = parameter as Dictionary<string, dynamic>;

            var response = await Task.Run(() => _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/rekening-air-update", null, param));
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    selectedData.StanLaluWpf = param["StanLalu"];
                    selectedData.StanSkrgWpf = param["StanSkrg"];
                    selectedData.StanAngkatWpf = param["StanAngkat"];
                    selectedData.PakaiWpf = param["Pakai"];
                    selectedData.PakaiKalkulasiWpf = param["PakaiKalkulasi"];
                    selectedData.BiayaPemakaianWpf = param["BiayaPemakaian"];
                    selectedData.AdministrasiWpf = param["Administrasi"];
                    selectedData.PemeliharaanWpf = param["Pemeliharaan"];
                    selectedData.RetribusiWpf = param["Retribusi"];
                    selectedData.PelayananWpf = param["Pelayanan"];
                    selectedData.AirLimbahWpf = param["AirLimbah"];
                    selectedData.DendaPakai0Wpf = param["DendaPakai0"];
                    selectedData.AdministrasiLainWpf = param["DendaPakai0"];
                    selectedData.PemeliharaanLainWpf = param["PemeliharaanLain"];
                    selectedData.RetribusiLainWpf = param["RetribusiLain"];
                    selectedData.PpnWpf = param["Ppn"];
                    selectedData.MeteraiWpf = param["Meterai"];
                    selectedData.RekairWpf = param["Rekair"];
                    selectedData.TotalWpf = param["Total"];
                    selectedData.DendaWpf = param["Denda"];
                    selectedData.PetugasBacaWpf = param["PetugasBaca"];
                    selectedData.KelainanWpf = param["Kelainan"];
                    selectedData.TaksirWpf = param["Taksir"];
                    selectedData.TaksasiWpf = param["Taksasi"];
                    selectedData.FlagKoreksiWpf = true;
                    selectedData.WaktuKoreksiWpf = param["WaktuKoreksi"];
                }
                else
                {
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg, "bacameter");
                }
            }
            else
            {
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error?.Message, "bacameter");
            }

            selectedData.IsUpdatingKoreksi = false;
        }
    }
}
