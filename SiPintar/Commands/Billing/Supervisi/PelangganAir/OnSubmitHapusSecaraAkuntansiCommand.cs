using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Billing.Supervisi.PelangganAir
{
    public class OnSubmitHapusSecaraAkuntansiCommand : AsyncCommandBase
    {
        private readonly PelangganAirViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitHapusSecaraAkuntansiCommand(PelangganAirViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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

            var param = new Dictionary<string, dynamic>()
            {
                {"KodePeriodeAwal", _viewModel.KodePeriodeHapusSecaraAkuntansi1},
                {"KodePeriodeAkhir", _viewModel.KodePeriodeHapusSecaraAkuntansi2},
                {"IdPelangganAir", _viewModel.SelectedData.IdPelangganAir},
                {"UpdateMasterPelanggan", _viewModel.IsUpdateMasterPelangganChecked}
            };

            var response = await Task.Run(() => _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/rekening-air-set-hapus-secara-akuntansi", null, param));
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    if (_viewModel.IsUpdateMasterPelangganChecked)
                    {
                        selectedData.IdFlagWpf = 5;
                        selectedData.NamaFlagWpf = "Usulan Hapus Secara Akuntansi";
                    }
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg, "billing");
            }
            else
            {
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error?.Message, "billing");
            }

            _viewModel.IsLoadingForm = false;
            selectedData.IsUpdatingKoreksi = false;
        }
    }
}
