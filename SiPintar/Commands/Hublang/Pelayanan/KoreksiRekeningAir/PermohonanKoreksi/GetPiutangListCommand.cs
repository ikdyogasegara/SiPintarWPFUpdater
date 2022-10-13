using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan.KoreksiRekeningAir;

namespace SiPintar.Commands.Hublang.Pelayanan.KoreksiRekeningAir.PermohonanKoreksi
{
    [ExcludeFromCodeCoverage]
    public class GetPiutangListCommand : AsyncCommandBase
    {
        private readonly PermohonanKoreksiViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public GetPiutangListCommand(PermohonanKoreksiViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if ((_viewModel.IsFor != "detail" && _viewModel.SelectedPelangganAir == null) || (_viewModel.IsFor == "detail" && _viewModel.SelectedData == null))
                return;

            var idPelangganAir = _viewModel.IsFor == "detail" ? _viewModel.SelectedData.IdPelangganAir : _viewModel.SelectedPelangganAir.IdPelangganAir;
            var listIdPeriode = _viewModel.IsFor == "detail" ? GetDetailPiutang() : new List<int>();

            _viewModel.IsLoadingForm = true;

            var param = new Dictionary<string, dynamic>
            {
                {"PageSize", 100000},
                {"CurrentPage", 1},
                {"IdPelangganAir", idPelangganAir},
            };

            var response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/rekening-air-piutang", param));

            _viewModel.PiutangAirList = new ObservableCollection<RekeningAirPiutangWpf>();
            _viewModel.TotalPiutangBiayaPemakaian = 0;

            if (!response.IsError)
            {
                var result = response.Data;

                if (result.Status && result.Data != null)
                {
                    var listData = result.Data.ToObject<ObservableCollection<RekeningAirPiutangWpf>>();

                    if (listData != null && !_isTest)
                    {
                        foreach (var item in listData)
                        {
                            _viewModel.TotalPiutangBiayaPemakaian += item.BiayaPemakaian ?? 0;

                            if (_viewModel.IsFor == "detail")
                            {
                                item.IsSelected = listIdPeriode.Contains((int)item.IdPeriode);
                            }
                        }

                        _viewModel.PiutangAirList = listData;
                    }
                }
            }

            _viewModel.IsEmptyForm2 = _viewModel.PiutangAirList.Count == 0;
            _viewModel.IsLoadingForm = false;
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private List<int> GetDetailPiutang()
        {
            var listIdPeriode = new List<int>();

            if (!_isTest && _viewModel.SelectedData.ParameterWpf != null)
            {
                foreach (var item in _viewModel.SelectedData.ParameterWpf)
                {
                    var data = item.Value != null ? item.Value.Split(',') : null;
                    if (data != null)
                    {
                        foreach (var subitem in data)
                        {
                            listIdPeriode.Add(Convert.ToInt32(subitem));
                        }
                    }
                }
            }

            return listIdPeriode;
        }
    }
}
