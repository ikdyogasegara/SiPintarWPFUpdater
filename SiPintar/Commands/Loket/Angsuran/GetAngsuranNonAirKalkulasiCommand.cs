using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Utilities;
using SiPintar.ViewModels.Loket;

namespace SiPintar.Commands.Loket.Angsuran
{
    public class GetAngsuranNonAirKalkulasiCommand : AsyncCommandBase
    {

        private readonly AngsuranViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public GetAngsuranNonAirKalkulasiCommand(AngsuranViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoadingForm = true;

            try
            {
                var detail = new ParamRekeningAngsuranNonAirKalkulasiDetailDto()
                {
                    IdJenisNonair = _viewModel.SelectedRekeningNonAir.IdJenisNonAir,
                    Total = _viewModel.SelectedRekeningNonAir.Total,
                    IdNonair = _viewModel.SelectedRekeningNonAir.IdNonAir,
                    IdPelangganAir = 0,
                    IdPelangganLimbah = 0,
                    IdPelangganLltt = 0,
                    IdRayon = 0,
                    IdKelurahan = 0,
                    IdGolongan = 0,
                    IdDiameter = 0,
                };

                var param = new Dictionary<string, dynamic>
                {
                    { "PersentaseUangMukaMaksimal", 50 },
                    { "UangMuka", _viewModel.UangMukaForm  },
                    { "jumlahTermin", _viewModel.TerminForm },
                    { "tglMulaiTagihPertama", DateTime.Now},
                    { "Detail", detail},
                };

                var Response = await Task.Run(() => _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/rekening-angsuran-non-air-kalkulasi", param));

                _viewModel.AngsuranNonAirKalkulasi = new ObservableCollection<ParamRekeningAngsuranNonAirDetailDto>();

                if (!Response.IsError)
                {
                    var Result = Response.Data;

                    if (Result.Status && Result.Data != null)
                    {
                        var data = Result.Data.ToObject<ObservableCollection<ParamRekeningAngsuranNonAirDetailDto>>();

                        _viewModel.AngsuranNonAirKalkulasi = data;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            _viewModel.IsEmptyForm2 = _viewModel.AngsuranNonAirKalkulasi.Count == 0;
            _viewModel.IsLoadingForm = false;

            await Task.FromResult(_isTest);
        }
    }
}
