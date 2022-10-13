using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Billing.Supervisi.RekeningAir;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Billing.Supervisi.RekeningAir
{
    public class OnOpenHasilBacaUlangDialogCommand : AsyncCommandBase
    {
        private readonly RekeningAirViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnOpenHasilBacaUlangDialogCommand(RekeningAirViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.SelectedData == null)
                return;

            if (_viewModel.SelectedData.FlagRequestBacaUlang != 2)
                OpenWarning();

            _viewModel.IsLoadingForm = true;
            _viewModel.IsOpenBacaUlang = true;

            OpenDialog();

            await GetDetailAsync();

            _viewModel.IsLoadingForm = false;

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private async Task GetDetailAsync()
        {
            var param = new Dictionary<string, dynamic>
            {
                { "IdPelangganAir", _viewModel.SelectedData.IdPelangganAir },
                { "IdPeriode", _viewModel.SelectedData.IdPeriode }
            };

            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/rekening-air-view-hasil-baca-ulang", param);
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    var temp = result.Data.ToObject<ObservableCollection<RekeningAirHasilBacaUlangDto>>();

                    if (temp.Count > 0)
                        _viewModel.HasilBacaUlang = temp[^1];
                }
            }
        }

        [ExcludeFromCodeCoverage]
        private void OpenDialog()
        {
            if (!_isTest) _ = DialogHost.Show(new HasilBacaUlangView(_viewModel), "BillingRootDialog");
        }

        [ExcludeFromCodeCoverage]
        private void OpenWarning()
        {
            if (!_isTest) _ = DialogHost.Show(new DialogGlobalView("Informasi Baca Ulang", "Tidak ditemukan data hasil baca ulang. Mungkin hasil baca ulang belum dikirim oleh petugas lapangan / Anda tidak melakukan request baca ulang.", "warning"), "BillingRootDialog");
        }
    }
}
