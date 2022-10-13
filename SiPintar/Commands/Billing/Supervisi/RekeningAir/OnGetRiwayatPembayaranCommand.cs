using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Commands.Billing.Supervisi.RekeningAir
{
    public class OnGetRiwayatPembayaranCommand : AsyncCommandBase
    {
        private readonly RekeningAirViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnGetRiwayatPembayaranCommand(RekeningAirViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.YearPembayaran.HasValue)
            {
                AppDispatcherHelper.Run(_isTest, () =>
                {
                    _viewModel.PembayaranList?.Clear();
                    _viewModel.PembayaranNonAirList?.Clear();
                });

                _viewModel.IsLoadRiwayatBayar = _viewModel.IsEmptyRiwayatBayar = _viewModel.IsEmptyRiwayatBayarNonAir = true;
                var param = new Dictionary<string, dynamic>
                {
                    { "IdPelangganAir" , _viewModel.SelectedData.IdPelangganAir},
                    { "StatusTransaksi", true },
                    { "TahunTransaksi", _viewModel.YearPembayaran },
                };
                var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/rekening-air-history-pelunasan-pembatalan", param);
                if (!response.IsError)
                {
                    if (response.Data.Status && response.Data.Data != null)
                    {
                        _viewModel.TotalRekAirRiwayatBayarAir = decimal.Zero;
                        _viewModel.TotalDendaRiwayatBayarAir = decimal.Zero;
                        _viewModel.TotalJumlahRiwayatBayarAir = decimal.Zero;
                        _viewModel.PembayaranList = response.Data.Data.ToObject<ObservableCollection<RekeningAirPelunasanDanPembatalanDto>>();
                        _viewModel.IsEmptyRiwayatBayar = (_viewModel.PembayaranList?.Count ?? 0) == 0;
                        _viewModel.PembayaranList.ForEach(x =>
                        {
                            _viewModel.TotalRekAirRiwayatBayarAir += x.Rekair ?? decimal.Zero;
                            _viewModel.TotalDendaRiwayatBayarAir += x.Denda ?? decimal.Zero;
                            _viewModel.TotalJumlahRiwayatBayarAir += x.Total ?? decimal.Zero;
                        });
                    }
                    else
                    {
                        DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg, moduleName: "billing");
                    }
                }
                else
                {
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message, moduleName: "billing");
                }

                var responseNonAir = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/rekening-non-air-history-pelunasan-pembatalan", param);
                if (!responseNonAir.IsError)
                {
                    if (responseNonAir.Data.Status && responseNonAir.Data.Data != null)
                    {
                        _viewModel.TotalJumlahRiwayatBayarNonAir = decimal.Zero;
                        _viewModel.PembayaranNonAirList = responseNonAir.Data.Data.ToObject<ObservableCollection<RekeningNonAirPelunasanDanPembatalanDto>>();
                        _viewModel.IsEmptyRiwayatBayarNonAir = (_viewModel.PembayaranNonAirList?.Count ?? 0) == 0;
                        _viewModel.PembayaranNonAirList.ForEach(x =>
                        {
                            _viewModel.TotalJumlahRiwayatBayarNonAir += x.Total ?? decimal.Zero;
                        });
                    }
                    else
                    {
                        DialogHelper.ShowSnackbar(_isTest, "danger", responseNonAir.Data.Ui_msg, moduleName: "billing");
                    }
                }
                else
                {
                    DialogHelper.ShowSnackbar(_isTest, "danger", responseNonAir.Error.Message, moduleName: "billing");
                }

                _viewModel.IsLoadRiwayatBayar = false;
            }
            await Task.FromResult(_isTest);
        }
    }
}
