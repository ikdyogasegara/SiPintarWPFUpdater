using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Utilities;
using SiPintar.ViewModels.Bacameter;

namespace SiPintar.Commands.Bacameter.Produktivitas
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly ProduktivitasViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnLoadCommand(ProduktivitasViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            await GetPeriodeAsync();

            _viewModel.UpdatePage("Petugas");

            var BulanPeriode = _viewModel.SelectedPeriode != null && _viewModel.SelectedPeriode.KodePeriode != null ? Convert.ToInt32(_viewModel.SelectedPeriode.KodePeriode.ToString().Substring(4)) : DateTime.Now.Month;
            var TahunPeriode = _viewModel.SelectedPeriode != null && _viewModel.SelectedPeriode.Tahun != null ? (int)_viewModel.SelectedPeriode.Tahun : DateTime.Now.Year;

            _viewModel.IsTglBacaChecked = true;
            _viewModel.TglBacaAwalFilter = new DateTime(TahunPeriode, BulanPeriode, 1);
            _viewModel.TglBacaAkhirFilter = new DateTime(TahunPeriode, BulanPeriode, 28);

            _ = GetRayonAsync();
            _ = GetWilayahAsync();
            _ = GetKelurahanAsync();
            _ = GetPetugasBacaAsync();

            await Task.FromResult(_isTest);
        }

        private async Task GetPeriodeAsync()
        {
            var param = new Dictionary<string, dynamic>
            {
                { "status", true }
            };

            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/master-periode-rekening", param);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.PeriodeList = Result.Data.ToObject<ObservableCollection<MasterPeriodeDto>>();

                    if (_viewModel.PeriodeList != null && _viewModel.PeriodeList.Count > 0)
                        _viewModel.SelectedPeriode = _viewModel.PeriodeList[0];
                }
            }
        }

        private async Task GetRayonAsync()
        {
            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/master-rayon");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                    _viewModel.RayonList = Result.Data.ToObject<ObservableCollection<MasterRayonDto>>();
            }
        }

        private async Task GetWilayahAsync()
        {
            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/master-wilayah");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                    _viewModel.WilayahList = Result.Data.ToObject<ObservableCollection<MasterWilayahDto>>();
            }
        }

        private async Task GetKelurahanAsync()
        {
            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/master-kelurahan");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                    _viewModel.KelurahanList = Result.Data.ToObject<ObservableCollection<MasterKelurahanDto>>();
            }
        }

        private async Task GetPetugasBacaAsync()
        {
            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/master-petugas-baca");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                    _viewModel.PetugasList = Result.Data.ToObject<ObservableCollection<MasterPetugasBacaDto>>();
            }
        }
    }
}
