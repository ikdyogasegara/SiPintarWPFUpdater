using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using AppBusiness.Data.DTOs;
using SiPintar.Utilities;
using SiPintar.ViewModels.Loket;

namespace SiPintar.Commands.Loket.Angsuran
{
    public class GetAngsuranKalkulasiCommand : AsyncCommandBase
    {

        private readonly AngsuranViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public GetAngsuranKalkulasiCommand(AngsuranViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoadingForm = true;

            var detailList = new List<ParamRekeningAngsuranAirKalkulasiDetailDto>();
            foreach (var item in _viewModel.RekeningAirList.Where(x => x.IsSelected).ToList())
            {
                var detail = new ParamRekeningAngsuranAirKalkulasiDetailDto();
                detail.IdPelangganAir = item.IdPelangganAir;
                detail.IdPeriode = item.IdPeriode;
                detail.KodePeriode = item.KodePeriode;
                detail.NamaPeriode = item.NamaPeriode;
                detail.BiayaPemakaian = item.BiayaPemakaian;
                detail.Administrasi = item.Administrasi;
                detail.Pemeliharaan = item.Pemeliharaan;
                detail.Retribusi = item.Retribusi;
                detail.Pelayanan = item.Pelayanan;
                detail.AirLimbah = item.AirLimbah;
                detail.DendaPakai0 = item.DendaPakai0;
                detail.AdministrasiLain = item.AdministrasiLain;
                detail.PemeliharaanLain = item.PemeliharaanLain;
                detail.RetribusiLain = item.RetribusiLain;
                detail.Meterai = item.Meterai;
                detail.RekAir = item.Rekair;
                detail.Denda = item.Denda;
                detail.Ppn = item.Ppn;
                detail.Total = item.Total;


                detailList.Add(detail);
            }


            var param = new Dictionary<string, dynamic>
            {
                { "PersentaseUangMukaMaksimal", _viewModel.DpMaxForm  },
                { "UangMuka", _viewModel.UangMukaForm  },
                { "jumlahTermin", _viewModel.TerminForm },
                { "tglMulaiTagihPertama", DateTime.Now},
                { "Detail", detailList},
                { "NonAirLainnya", new List<ParamRekeningAngsuranNonAirLainnyaKalkulasiDetailDto>()},
                { "Limbah", new List<ParamRekeningAngsuranLimbahLainnyaKalkulasiDetailDto>()},
                { "Lltt", new List<ParamRekeningAngsuranLlttLainnyaKalkulasiDetailDto>()},
            };

            var Response = await Task.Run(() => _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/rekening-angsuran-air-kalkulasi", param));

            _viewModel.AngsuranKalkulasi = new ObservableCollection<ParamRekeningAngsuranAirDetailDto>();

            if (!Response.IsError)
            {
                var Result = Response.Data;

                if (Result.Status && Result.Data != null)
                {
                    var data = Result.Data.ToObject<ObservableCollection<ParamRekeningAngsuranAirDetailDto>>();

                    _viewModel.AngsuranKalkulasi = data;
                    _viewModel.AngsuranKalkulasiGroup = new ListCollectionView(_viewModel.AngsuranKalkulasi);
                    using (_viewModel.AngsuranKalkulasiGroup.DeferRefresh())
                    {
                        _viewModel.AngsuranKalkulasiGroup.GroupDescriptions.Add(new PropertyGroupDescription("Termin"));
                    }
                }
            }

            _viewModel.IsEmptyForm2 = _viewModel.AngsuranKalkulasi.Count == 0;
            _viewModel.IsLoadingForm = false;

            await Task.FromResult(_isTest);
        }
    }
}
