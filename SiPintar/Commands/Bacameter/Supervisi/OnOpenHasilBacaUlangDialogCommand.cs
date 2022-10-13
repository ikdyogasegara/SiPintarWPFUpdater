using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Utilities;
using SiPintar.ViewModels.Bacameter;
using SiPintar.Views.Bacameter.Supervisi;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Bacameter.Supervisi
{
    public class OnOpenHasilBacaUlangDialogCommand : AsyncCommandBase
    {
        private readonly SupervisiViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnOpenHasilBacaUlangDialogCommand(SupervisiViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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

        private async Task GetDetailAsync()
        {
            var param = new Dictionary<string, dynamic>
            {
                { "IdPelangganAir", _viewModel.SelectedData.IdPelangganAir },
                { "IdPeriode", _viewModel.SelectedData.IdPeriode }
            };

            if (_isTest)
                param.Add("TestId", _viewModel.TestId);

            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/rekening-air-view-hasil-baca-ulang", param);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    var result = Result.Data.ToObject<ObservableCollection<RekeningAirHasilBacaUlangDto>>();

                    if (result.Count > 0)
                        _viewModel.HasilBacaUlang = result[^1];
                }
            }
        }

        [ExcludeFromCodeCoverage]
        public void OpenDialog()
        {
            if (!_isTest) _ = DialogHost.Show(new HasilBacaUlangView(_viewModel), "BacameterRootDialog");
        }

        [ExcludeFromCodeCoverage]
        public void OpenWarning()
        {
            if (!_isTest) _ = DialogHost.Show(new DialogGlobalView("Informasi Baca Ulang", "Tidak ditemukan data hasil baca ulang. Mungkin hasil baca ulang belum dikirim oleh petugas lapangan / Anda tidak melakukan request baca ulang.", "warning"), "BacameterRootDialog");
        }
    }
}
