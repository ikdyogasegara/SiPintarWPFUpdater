using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Utilities;
using SiPintar.ViewModels.Bacameter.Produktivitas;
using SiPintar.Views;

namespace SiPintar.Commands.Bacameter.Produktivitas.PetugasBacaPerHari
{
    [ExcludeFromCodeCoverage]
    public class GetDataHarianCommand : AsyncCommandBase
    {
        private readonly PetugasBacaPerHariViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public GetDataHarianCommand(PetugasBacaPerHariViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.Parent.SelectedPeriode == null)
                return;

            _viewModel.IsLoadingHarian = true;

            await GetDataListAsync();

            _viewModel.IsEmptyHarian = _viewModel.DataListHarian == null || !_viewModel.DataListHarian.Any();

            CalculateSummary();

            _viewModel.IsLoadingHarian = false;

            await Task.FromResult(_isTest);
        }

        private async Task GetDataListAsync()
        {
            string ErrorMessage = null;

            var param = new Dictionary<string, dynamic>
            {
                { "PageSize", 1000 },
                { "CurrentPage", 1 },
                { "IdPeriode", _viewModel.Parent.SelectedPeriode?.IdPeriode },
                { "Kategori", "petugas baca per hari" },
                { "IdPetugasBaca", _viewModel.SelectedPetugas.IdPetugasBaca },
            };

            if (_viewModel.Parent.TglBacaAwalFilter != null)
                param.Add("WaktuBacaMulai", _viewModel.Parent.TglBacaAwalFilter);
            if (_viewModel.Parent.TglBacaAkhirFilter != null)
                param.Add("WaktuBacaSampaiDengan", _viewModel.Parent.TglBacaAkhirFilter);
            if (_viewModel.Parent.TglKirimAwalFilter != null)
                param.Add("WaktuKirimHasilBacaMulai", _viewModel.Parent.TglKirimAwalFilter);
            if (_viewModel.Parent.TglKirimAkhirFilter != null)
                param.Add("WaktuKirimHasilBacaSampaiDengan", _viewModel.Parent.TglKirimAkhirFilter);
            if (_viewModel.Parent.RayonFilter != null)
                param.Add("IdRayon", _viewModel.Parent.RayonFilter.IdRayon);
            if (_viewModel.Parent.WilayahFilter != null)
                param.Add("IdWilayah", _viewModel.Parent.WilayahFilter.IdWilayah);
            if (_viewModel.Parent.KelurahanFilter != null)
                param.Add("IdKelurahan", _viewModel.Parent.KelurahanFilter.IdKelurahan);

            if (_isTest)
                param.Add("TestId", _viewModel.TestId);

            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{ApiVersion}/summary-produktifitas-bacameter", param));

            _viewModel.DataListHarian = new ObservableCollection<SummaryProduktifitasBacameterPetugasBacaPerHariDto>();

            if (!Response.IsError)
            {
                var Result = Response.Data;

                if (Result.Status && Result.Data != null)
                    _viewModel.DataListHarian = Result.Data.ToObject<ObservableCollection<SummaryProduktifitasBacameterPetugasBacaPerHariDto>>();
                else
                    ErrorMessage = Response.Data.Ui_msg;
            }
            else
            {
                ErrorMessage = Response.Error.Message;
            }

            ShowSnackbar(ErrorMessage);

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private void ShowSnackbar(string ErrorMessage)
        {
            if (ErrorMessage != null && !_isTest)
                Application.Current.Dispatcher.Invoke(delegate
                {
                    var mainView = (BacameterView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                    if (mainView != null)
                        mainView.ShowSnackbar(ErrorMessage, "danger");
                });
        }

        private void CalculateSummary()
        {
            int TargetBacaSummary = 0;
            int Terbaca = 0;
            int SisaBacaSummary = 0;
            int TotalBacaSummary = 0;
            DateTime? LastUpdateSummary = null;

            if (_viewModel.DataListHarian != null)
            {
                LastUpdateSummary = DateTime.Now;

                foreach (var item in _viewModel.DataListHarian)
                {
                    TargetBacaSummary += item.Target != null ? (int)item.Target : 0;
                    Terbaca += item.Terbaca != null ? (int)item.Terbaca : 0;

                    if (item.Tanggal != null && ((DateTime)item.Tanggal) <= DateTime.Now)
                        TotalBacaSummary += item.Terbaca != null ? (int)item.Terbaca : 0;

                    if (item.LastWaktuKirimHasilBaca != null && ((DateTime)item.LastWaktuKirimHasilBaca) > LastUpdateSummary)
                        LastUpdateSummary = (DateTime)item.LastWaktuKirimHasilBaca;
                }

                SisaBacaSummary = TargetBacaSummary - Terbaca;
            }

            _viewModel.TargetBacaSummary = TargetBacaSummary;
            _viewModel.SisaBacaSummary = SisaBacaSummary;
            _viewModel.TotalBacaSummary = TotalBacaSummary;
            _viewModel.LastUpdateSummary = LastUpdateSummary;
        }
    }
}
