using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan.Info;

namespace SiPintar.Commands.Hublang.Pelayanan.Info.Tagihan
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly TagihanViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(TagihanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoading = true;
            _viewModel.RekeningAirCount = 0;
            _viewModel.RekeningLimbahCount = 0;
            _viewModel.RekeningLlttCount = 0;
            _viewModel.RekeningNonAirCount = 0;

            RestApiResponse Response = null;
            int? IdPelangganAir = null;

            if (_viewModel.SelectedPelanggan is MasterPelangganAirDto)
            {
                var data = _viewModel.SelectedPelanggan as MasterPelangganAirDto;
                IdPelangganAir = data.IdPelangganAir;

            }
            else if (_viewModel.SelectedPelanggan is MasterPelangganLimbahDto)
            {
                var data = _viewModel.SelectedPelanggan as MasterPelangganLimbahDto;

                IdPelangganAir = data.IdPelangganAir;
            }
            else
            {
                var data = _viewModel.SelectedPelanggan as MasterPelangganLlttDto;

                IdPelangganAir = data.IdPelangganAir;

            }

            var param = new Dictionary<string, dynamic>
            {
                {"ListIdPelangganAir",IdPelangganAir},
                {"Tanggal", DateTime.Now.ToString("yyyy-MM-dd")}
            };

            Response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/payment-tagihan-by-id-pelanggan-air", param));
            _viewModel.TagihanList = new ObservableCollection<DaftarTagihanDto>();

            if (!Response.IsError)
            {
                var Result = Response.Data;

                if (Result.Status && Result.Data != null)
                {
                    var temp = Result.Data.ToObject<ObservableCollection<PaymentDto>>();
                    _viewModel.RekeningAirCount = temp[0].RekeningAir.Count;
                    _viewModel.RekeningAirTotal = temp[0].RekeningAir.Sum(c => c.Total);

                    _viewModel.RekeningLimbahCount = temp[0].RekeningLimbah.Count;
                    _viewModel.RekeningLimbahTotal = temp[0].RekeningLimbah.Sum(c => c.Biaya);

                    _viewModel.RekeningLlttCount = temp[0].RekeningLltt.Count;
                    _viewModel.RekeningLlttTotal = temp[0].RekeningLltt.Sum(c => c.Biaya);

                    _viewModel.RekeningNonAirCount = temp[0].RekeningNonAir.Count;
                    _viewModel.RekeningNonAirTotal = temp[0].RekeningNonAir.Sum(c => c.Total);

                    _viewModel.RekeningAmountCount = temp[0].RekeningAir.Count + temp[0].RekeningLimbah.Count +
                        temp[0].RekeningLltt.Count + temp[0].RekeningNonAir.Count;

                    _viewModel.RekeningAmountTotal = temp[0].RekeningAir.Sum(c => c.Total) + temp[0].RekeningLimbah.Sum(c => c.Biaya) +
                       temp[0].RekeningLltt.Sum(c => c.Biaya) + temp[0].RekeningNonAir.Sum(c => c.Total);

                    ExtractData(temp);
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", Response.Data.Ui_msg);
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error.Message);


            _viewModel.IsEmpty = _viewModel.TagihanList.Count == 0;

            _viewModel.IsLoading = false;

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void ExtractData(ObservableCollection<PaymentDto> result)
        {
            if (!_isTest)
            {
                var hasil = new ObservableCollection<DaftarTagihanDto>();
                foreach (var i in result[0].RekeningAir)
                {
                    hasil.Add(new DaftarTagihanDto
                    {
                        IdPdam = 0,
                        IdPelangganAir = i.IdPelangganAir,
                        IdPeriode = i.IdPeriode,
                        KodePeriode = i.KodePeriode,
                        NamaPeriode = i.NamaPeriode,
                        Rekening = "AIR",
                        Tagihan = i.Rekair,
                        Denda = i.Denda,
                        Total = i.Total,
                    });
                }

                foreach (var i in result[0].RekeningNonAir)
                {
                    hasil.Add(new DaftarTagihanDto
                    {
                        IdPdam = 0,
                        IdPelangganAir = i.IdPelangganAir,
                        IdPeriode = i.IdPeriode,
                        KodePeriode = i.KodePeriode,
                        NamaPeriode = i.NamaPeriode,
                        Keterangan = i.NamaJenisNonAir,
                        Rekening = "NON-AIR",
                        Tagihan = i.Total,
                        Denda = 0,
                        Total = i.Total,
                    });
                }

                _viewModel.TagihanList = hasil;
            }
        }
    }
}
