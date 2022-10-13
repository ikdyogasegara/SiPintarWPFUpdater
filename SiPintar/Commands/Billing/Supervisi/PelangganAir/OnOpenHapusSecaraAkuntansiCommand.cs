using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Billing.Supervisi.PelangganAir;

namespace SiPintar.Commands.Billing.Supervisi.PelangganAir
{
    public class OnOpenHapusSecaraAkuntansiCommand : AsyncCommandBase
    {
        private readonly PelangganAirViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnOpenHapusSecaraAkuntansiCommand(PelangganAirViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            #region akses
            if (_viewModel.AksesSetUsulanHapusSecaraAkuntansi == false)
            {
                DialogHelper.ShowInvalidAkses(_isTest);
                return;
            }
            #endregion

            if (_viewModel.IsLoading)
                return;

            _viewModel.IsLoading = true;

            var param = new Dictionary<string, dynamic>
            {
                { "PageSize" , _viewModel.LimitData.Key },
                { "CurrentPage" , _viewModel.CurrentPage },
                { "IdPelangganAir" , _viewModel.SelectedData.IdPelangganAir },
                { "SelainHapusSecaraAkuntansi", true}
            };

            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/rekening-air-piutang", param);
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    var piutang = result.Data.ToObject<ObservableCollection<RekeningAirPiutangDto>>();

                    if (piutang.Any())
                    {
                        _viewModel.ListPeriodeHapusAkuntansi = piutang;
                        _viewModel.IsUpdateMasterPelangganChecked = false;
                        _viewModel.IsLoading = false;

                        _ = DialogHelper.ShowCustomDialogViewAsync(_isTest, true, "BillingRootDialog",
                            GetDialog);
                    }
                    else
                    {
                        ShowWarning();
                    }
                }
                else
                {
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg, "billing");
                }
            }
            else
            {
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message, "billing");
            }

            _viewModel.IsLoading = false;
            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private object GetDialog() => new HapusSecaraAkuntansiView(_viewModel);

        [ExcludeFromCodeCoverage]
        private void ShowWarning()
        {
            if (!_isTest)
            {
                _ = DialogHelper.ShowDialogGlobalViewAsync(
                    false,
                    true,
                    "BillingRootDialog",
                    "Cek Piutang",
                    $"Pelanggan Tidak Memiliki Piutang !",
                    "warning",
                    moduleName: "billing");
            }
        }
    }
}
