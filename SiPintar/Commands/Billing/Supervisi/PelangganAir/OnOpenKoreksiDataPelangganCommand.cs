using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Billing.Supervisi.PelangganAir;

namespace SiPintar.Commands.Billing.Supervisi.PelangganAir
{
    public class OnOpenKoreksiDataPelangganCommand : AsyncCommandBase
    {
        private readonly PelangganAirViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnOpenKoreksiDataPelangganCommand(PelangganAirViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading)
                return;

            _viewModel.IsLoadingForm = true;

            OpenDialog();

            _ = GetGolonganAsync();
            _ = GetDiameterAsync();
            _ = GetRayonAsync();
            _ = GetKelurahanAsync();
            _ = GetKolektifAsync();
            _ = GetSumberAirAsync();
            _ = GetDmaAsync();
            _ = GetDmzAsync();
            _ = GetBlokAsync();
            _ = GetMerekMeterAsync();
            _ = GetKondisiMeterAsync();
            _ = GetAdministrasiLainAsync();
            _ = GetPemeliharaanLainAsync();
            _ = GetRetribusiLainAsync();
            _ = GetFlagAsync();
            _ = GetStatusAsync();

            _viewModel.PelangganForm = _viewModel.SelectedData;

            _viewModel.PelangganDetailForm = new MasterPelangganAirDetailDto();
            var param = new Dictionary<string, dynamic>
            {
                { "IdPelangganAir" , _viewModel.SelectedData.IdPelangganAir },
            };

            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-pelanggan-air-detail", param);
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    _viewModel.PelangganDetailForm = result.Data.Any() ? result.Data[0].ToObject<MasterPelangganAirDetailDto>() : new MasterPelangganAirDetailDto();
                }
            }

            _viewModel.IsLoadingForm = false;
        }

        [ExcludeFromCodeCoverage]
        public void OpenDialog()
        {
            if (!_isTest) _ = DialogHost.Show(new KoreksiDataPelangganView(_viewModel), "BillingRootDialog");
        }

        [ExcludeFromCodeCoverage]
        private async Task GetGolonganAsync()
        {
            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-tarif-golongan");
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    _viewModel.GolonganList = result.Data.ToObject<ObservableCollection<MasterGolonganDto>>();
                }
            }
        }

        [ExcludeFromCodeCoverage]
        private async Task GetDiameterAsync()
        {
            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-tarif-diameter");
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    _viewModel.DiameterList = result.Data.ToObject<ObservableCollection<MasterDiameterDto>>();
                }
            }
        }

        [ExcludeFromCodeCoverage]
        private async Task GetRayonAsync()
        {
            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-rayon");
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    _viewModel.RayonList = result.Data.ToObject<ObservableCollection<MasterRayonDto>>();
                }
            }
        }

        [ExcludeFromCodeCoverage]
        private async Task GetKelurahanAsync()
        {
            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-kelurahan");
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    _viewModel.KelurahanList = result.Data.ToObject<ObservableCollection<MasterKelurahanDto>>();
                }
            }
        }

        [ExcludeFromCodeCoverage]
        private async Task GetKolektifAsync()
        {
            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-kolektif");
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    _viewModel.KolektifList = result.Data.ToObject<ObservableCollection<MasterKolektifDto>>();
                }
            }
        }

        [ExcludeFromCodeCoverage]
        private async Task GetSumberAirAsync()
        {
            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-sumber-air");
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    _viewModel.SumberAirList = result.Data.ToObject<ObservableCollection<MasterSumberAirDto>>();
                }
            }
        }

        [ExcludeFromCodeCoverage]
        private async Task GetDmaAsync()
        {
            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-dma");
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    _viewModel.DmaList = result.Data.ToObject<ObservableCollection<MasterDmaDto>>();
                }
            }
        }

        [ExcludeFromCodeCoverage]
        private async Task GetDmzAsync()
        {
            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-dmz");
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    _viewModel.DmzList = result.Data.ToObject<ObservableCollection<MasterDmzDto>>();
                }
            }
        }

        [ExcludeFromCodeCoverage]
        private async Task GetBlokAsync()
        {
            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-blok");
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    _viewModel.BlokList = result.Data.ToObject<ObservableCollection<MasterBlokDto>>();
                }
            }
        }

        [ExcludeFromCodeCoverage]
        private async Task GetMerekMeterAsync()
        {
            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-merek-meter");
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    _viewModel.MerekMeterList = result.Data.ToObject<ObservableCollection<MasterMerekMeterDto>>();
                }
            }
        }

        [ExcludeFromCodeCoverage]
        private async Task GetKondisiMeterAsync()
        {
            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-kondisi-meter");
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    _viewModel.KondisiMeterList = result.Data.ToObject<ObservableCollection<MasterKondisiMeterDto>>();
                }
            }
        }

        [ExcludeFromCodeCoverage]
        private async Task GetAdministrasiLainAsync()
        {
            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-tarif-administrasi-lain");
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    _viewModel.AdministrasiLainList = result.Data.ToObject<ObservableCollection<MasterAdministrasiLainDto>>();
                }
            }
        }

        [ExcludeFromCodeCoverage]
        private async Task GetPemeliharaanLainAsync()
        {
            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-tarif-pemeliharaan-lain");
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    _viewModel.PemeliharaanLainList = result.Data.ToObject<ObservableCollection<MasterPemeliharaanLainDto>>();
                }
            }
        }

        [ExcludeFromCodeCoverage]
        private async Task GetRetribusiLainAsync()
        {
            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-tarif-retribusi-lain");
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    _viewModel.RetribusiLainList = result.Data.ToObject<ObservableCollection<MasterRetribusiLainDto>>();
                }
            }
        }

        [ExcludeFromCodeCoverage]
        private async Task GetFlagAsync()
        {
            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-flag");
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    _viewModel.FlagList = result.Data.ToObject<ObservableCollection<MasterFlagDto>>();
                }
            }
        }

        [ExcludeFromCodeCoverage]
        private async Task GetStatusAsync()
        {
            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-status");
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    _viewModel.StatusList = result.Data.ToObject<ObservableCollection<MasterStatusDto>>();
                }
            }
        }
    }
}
