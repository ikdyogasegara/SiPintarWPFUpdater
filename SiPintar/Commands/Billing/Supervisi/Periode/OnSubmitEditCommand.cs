using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views;
using SiPintar.Views.Global.Dialog;
using SiPintar.Views.Global.Other;

namespace SiPintar.Commands.Billing.Supervisi.Periode
{
    public class OnSubmitEditCommand : AsyncCommandBase
    {
        private readonly PeriodeViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitEditCommand(PeriodeViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var selectedData = _viewModel.SelectedData;
            selectedData.IsUpdatingKoreksi = true;

            var body = new Dictionary<string, dynamic>
            {
                { "IdPeriode", _viewModel.SelectedData.IdPeriode },
                { "TglMulaiTagih", Convert.ToDateTime(_viewModel.TglMulaiTagihForm).ToString("yyyy-MM-dd'T00:00:00Z'")},
                { "TglMulaiDenda1", Convert.ToDateTime(_viewModel.TglDenda1Form).ToString("yyyy-MM-dd'T00:00:00Z'")},
                { "TglMulaiDenda2", Convert.ToDateTime(_viewModel.TglDenda2Form).ToString("yyyy-MM-dd'T00:00:00Z'")},
                { "TglMulaiDenda3",Convert.ToDateTime(_viewModel.TglDenda3Form).ToString("yyyy-MM-dd'T00:00:00Z'")},
                { "TglMulaiDenda4", Convert.ToDateTime(_viewModel.TglDenda4Form).ToString("yyyy-MM-dd'T00:00:00Z'")},
                { "TglMulaiDendaPerBulan", Convert.ToDateTime(_viewModel.TglMulaiDendaForm).ToString("yyyy-MM-dd'T00:00:00Z'") }
            };

            if (_viewModel.IsCheckedRayon && _viewModel.SelectedRayon != null)
            {
                body.Add("IdRayon", _viewModel.SelectedRayon.IdRayon);
            }

            if (_viewModel.IsCheckedWilayah && _viewModel.SelectedWilayah != null)
            {
                body.Add("IdWilayah", _viewModel.SelectedWilayah.IdWilayah);
            }

            var response = await Task.Run(() => _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/master-periode-rekening-update-tgl-denda", null, body));
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    selectedData.TglMulaiDenda1Wpf = _viewModel.TglDenda1Form;
                    selectedData.TglMulaiDenda2Wpf = _viewModel.TglDenda2Form;
                    selectedData.TglMulaiDenda3Wpf = _viewModel.TglDenda3Form;
                    selectedData.TglMulaiDenda4Wpf = _viewModel.TglDenda4Form;
                    selectedData.TglMulaiDendaPerBulanWpf = _viewModel.TglMulaiDendaForm;
                    selectedData.IsUpdatingKoreksi = false;

                    DialogHelper.ShowSnackbar(_isTest, "success", response.Data.Ui_msg, "billing");
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg, "billing");
            }
            else
            {
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message, "billing");
            }

            selectedData.IsUpdatingKoreksi = false;
        }
    }
}
