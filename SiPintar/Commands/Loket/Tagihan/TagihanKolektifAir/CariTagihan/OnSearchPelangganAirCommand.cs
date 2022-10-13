using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Utilities;
using SiPintar.ViewModels.Loket.Tagihan;
using SiPintar.ViewModels.Loket.Tagihan.TagihanKolektifAir;

namespace SiPintar.Commands.Loket.Tagihan.TagihanKolektifAir.CariTagihan
{
    public class OnSearchPelangganAirCommand : AsyncCommandBase
    {
        private readonly CariTagihanViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSearchPelangganAirCommand(CariTagihanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoadingForm = true;
            _viewModel.ListSearchPelangganAir = new ObservableCollection<MasterPelangganAirWpf>();
            _viewModel.IsAllSelectedSearchPelangganAir = false;

            var param = parameter as Dictionary<string, dynamic>;
            await GetPelangganAirAsync(param);
            await CheckForTutupLoketAsync();
            _viewModel.IsLoadingForm = false;
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private async Task GetPelangganAirAsync(Dictionary<string, dynamic> param)
        {
            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-pelanggan-air", param);

            _viewModel.ListSearchPelangganAir = new ObservableCollection<MasterPelangganAirWpf>();

            if (!response.IsError)
            {
                var result = response.Data;

                if (result.Status && result.Data != null)
                {
                    _viewModel.ListSearchPelangganAir = result.Data.ToObject<ObservableCollection<MasterPelangganAirWpf>>();
                    _viewModel.TotalPage = result.TotalPage;
                    _viewModel.TotalRecord = result.Record;
                }
            }

            _viewModel.IsPreviousButtonEnabled = _viewModel.CurrentPage > 1;
            _viewModel.IsNextButtonEnabled = _viewModel.CurrentPage < _viewModel.TotalPage;
            _viewModel.IsLeftPageNumberActive = _viewModel.CurrentPage == 1;
            _viewModel.IsRightPageNumberActive = _viewModel.CurrentPage == _viewModel.TotalPage;
            _viewModel.IsLeftPageNumberFillerVisible = _viewModel.CurrentPage != 2;
            _viewModel.IsRightPageNumberFillerVisible = _viewModel.CurrentPage != _viewModel.TotalPage - 1;
            _viewModel.IsMiddlePageNumberVisible = _viewModel.CurrentPage > 1 && _viewModel.CurrentPage < _viewModel.TotalPage;
            _viewModel.IsEmptyAir = _viewModel.ListSearchPelangganAir.Count == 0;
            _viewModel.IsAllSelectedSearchPelangganAir = false;
        }

        [ExcludeFromCodeCoverage]
        private async Task CheckForTutupLoketAsync()
        {
            if (!_isTest)
            {
                AppSetting.LoketTutup = false;

                var today = DateTime.Now;
                var tglMulai = new DateTime(today.Year, today.Month, today.Day, 0, 0, 0);
                var tglAkhir = new DateTime(today.Year, today.Month, today.Day, 23, 59, 59);

                var param = new Dictionary<string, dynamic>
                {
                    { "IdLoket" , Application.Current.Resources["IdLoket"]?.ToString() },
                    { "TglPenerimaanMulai" , tglMulai },
                    { "TglPenerimaanSampaiDengan" , tglAkhir },
                };

                var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/tutup-loket", param);
                if (!response.IsError)
                {
                    var result = response.Data;
                    if (result.Status)
                    {
                        var temp = result.Data.ToObject<ObservableCollection<TutupLoketDto>>();

                        if (temp != null && temp.Count > 0)
                        {
                            AppSetting.LoketTutup = true;
                            _viewModel.ParentPage.TanggalTransaksi = DateTime.Now.AddDays(1);
                        }
                    }
                }
            }
        }
    }
}
