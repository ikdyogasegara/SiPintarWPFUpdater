using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan;
using SiPintar.Views.Hublang.Pelayanan.BAPengembalian;

namespace SiPintar.Commands.Hublang.Pelayanan.BAPengembalian
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly BaPengembalianViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;


        private readonly string ApiVersion = App.Current?.Resources["api_version"]?.ToString();


        public OnOpenAddFormCommand(BaPengembalianViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {

            object dg = DialogHelper.ShowDialogGlobalLoadingAsync(
                _isTest,
                false,
                "HublangRootDialog",
                "Mohon tunggu",
                "sedang mempersiapkan data...");

            await GetDataHistoriAsync();

            if (!_viewModel.PeriodeTransaksiList.Any())
            {
                DialogHelper.CloseDialog(_isTest, true, "HublangRootDialog", dg);
                await DialogHelper.ShowDialogGlobalViewAsync(
                    _isTest,
                    true,
                    "HublangRootDialog",
                    "Berita Acara Pengembalian",
                    "Maaf Pelanggan Belum Mempunyai Riwayat Pelunasan",
                    "warning",
                    "OK",
                    false,
                    "hublang");

                return;
            }

            await GetDataPetugasAsync();
            await GetDataKondisiMeterAsync();

            _viewModel.IsAdd = true;
            _viewModel.SelectedData = new RekeningAirPengembalianDto();
            _viewModel.SelectedPeriodeTransaksi = new RekeningAirPelunasanDanPembatalanDto();
            _viewModel.KalkulasiRekening = new ResultKalkulasiRekeningAirDto();



            DialogHelper.CloseDialog(_isTest, false, "HublangRootDialog", dg);


            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "HublangRootDialog", GetInstance);


            await Task.FromResult(_isTest);
        }

        private object GetInstance() => new DialogForm1View(_viewModel);

        public async Task GetDataHistoriAsync()
        {
            var Param = new Dictionary<string, dynamic>();
            Param.Add("StatusTransaksi", true);
            Param.Add("pageSize", 1000);
            Param.Add("currentPage", _viewModel.CurrentPage);
            Param.Add("IdPelangganAir", _viewModel.SelectedPelanggan.IdPelangganAir);


            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/rekening-air-history-pelunasan-pembatalan", Param);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.PeriodeTransaksiList = Result.Data.ToObject<ObservableCollection<RekeningAirPelunasanDanPembatalanDto>>();
                }
            }


        }

        public async Task GetDataPetugasAsync()
        {
            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/master-pegawai");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.MasterPegawaiList = Result.Data.ToObject<ObservableCollection<MasterPegawaiDto>>();
                }
            }
        }

        public async Task GetDataKondisiMeterAsync()
        {
            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/master-kondisi-meter");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.KondisiMeterList = Result.Data.ToObject<ObservableCollection<MasterKondisiMeterDto>>();
                }
            }
        }


    }
}
