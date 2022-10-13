using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan.KoreksiRekeningAir;

namespace SiPintar.Commands.Hublang.Pelayanan.KoreksiRekeningAir.UsulanKoreksi
{
    [ExcludeFromCodeCoverage]
    public class GetPiutangListCommand : AsyncCommandBase
    {
        private readonly UsulanKoreksiViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;
        private ObservableCollection<RekeningAirPiutangWpf> _tempListPiutang;

        public GetPiutangListCommand(UsulanKoreksiViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedPermohonanAir == null)
                return;

            var listIdPeriode = new List<int>();
            if (_viewModel.SelectedPermohonanAir.Parameter != null)
            {
                foreach (var item in _viewModel.SelectedPermohonanAir.Parameter)
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

            _viewModel.IsLoadingForm = true;
            _tempListPiutang = new ObservableCollection<RekeningAirPiutangWpf>();

            foreach (var item in listIdPeriode)
            {
                await GetPiutangAsync(item);
            }

            _viewModel.PiutangAirList = _tempListPiutang;
            _viewModel.IsEmptyForm2 = _viewModel.PiutangAirList.Count == 0;

            await GetFotoAsync();

            _viewModel.IsLoadingForm = false;
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private async Task GetPiutangAsync(int idPeriode)
        {
            if (!_isTest)
            {
                var param = new Dictionary<string, dynamic>
                {
                    {"PageSize", 1}, {"CurrentPage", 1}, {"IdPelangganAir", _viewModel.SelectedPermohonanAir.IdPelangganAir}, {"IdPeriode", idPeriode},
                };

                var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/rekening-air-piutang", param);

                if (!response.IsError)
                {
                    var result = response.Data;

                    if (result.Status && result.Data != null)
                    {
                        var listData = result.Data.ToObject<ObservableCollection<RekeningAirPiutangWpf>>();
                        if (listData != null && listData.Any())
                        {
                            _tempListPiutang.Add(listData[0]);
                        }
                    }
                }
            }
        }

        [ExcludeFromCodeCoverage]
        private async Task GetFotoAsync()
        {
            if (!_isTest)
            {
                if (_viewModel.SelectedPermohonanAir.IdPermohonan == null)
                    return;

                var param = new Dictionary<string, dynamic> { { "IdPermohonan", _viewModel.SelectedPermohonanAir.IdPermohonan } };

                var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/permohonan-pelanggan-air-link-foto", param);
                if (!response.IsError)
                {
                    var result = response.Data;
                    if (result.Status)
                    {
                        var data = result.Data.ToObject<ObservableCollection<NamaFotoDto>>();

                        if (data != null)
                        {
                            _viewModel.FotoBukti1PermohonanUri = await Task.Run(() => ImageUriHelper.SetUriAsync(data[0].FotoBukti1));
                            _viewModel.FotoBukti2PermohonanUri = await Task.Run(() => ImageUriHelper.SetUriAsync(data[0].FotoBukti2));
                            _viewModel.FotoBukti3PermohonanUri = await Task.Run(() => ImageUriHelper.SetUriAsync(data[0].FotoBukti3));

                            _viewModel.IsFotoBukti1PermohonanFormChecked = _viewModel.FotoBukti1PermohonanUri != null;
                            _viewModel.IsFotoBukti2PermohonanFormChecked = _viewModel.FotoBukti2PermohonanUri != null;
                            _viewModel.IsFotoBukti3PermohonanFormChecked = _viewModel.FotoBukti3PermohonanUri != null;
                        }
                    }
                    else
                    {
                        DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg);
                    }
                }
                else
                {
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message);
                }
            }
        }
    }
}
