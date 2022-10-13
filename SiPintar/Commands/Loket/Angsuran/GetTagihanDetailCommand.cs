using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Utilities;
using SiPintar.ViewModels.Loket;

namespace SiPintar.Commands.Loket.Angsuran
{
    public class GetTagihanDetailCommand : AsyncCommandBase
    {
        private readonly AngsuranViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public GetTagihanDetailCommand(AngsuranViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoadingForm = true;
            if (_viewModel.JenisAngsuran == "Tunggakan")
            {
                var param = new Dictionary<string, dynamic>
                {
                    { "PageSize" , 100 },
                    { "CurrentPage" , 1 },
                };

                if (!string.IsNullOrEmpty(_viewModel.SelectedPelanggan.Nama))
                    param.Add("Nama", _viewModel.SelectedPelanggan.Nama);
                if (!string.IsNullOrEmpty(_viewModel.SelectedPelanggan.NoSamb))
                    param.Add("NoSamb", _viewModel.SelectedPelanggan.NoSamb);
                if (!string.IsNullOrEmpty(_viewModel.SelectedPelanggan.Alamat))
                    param.Add("Alamat", _viewModel.SelectedPelanggan.Alamat);

                var Response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/rekening-air-piutang", param));

                _viewModel.RekeningAirList = new ObservableCollection<TagihanRekeningAirWpf>();

                if (!Response.IsError)
                {
                    var Result = Response.Data;

                    if (Result.Status && Result.Data != null)
                    {
                        var ListData = Result.Data.ToObject<ObservableCollection<TagihanRekeningAirWpf>>();
                        _viewModel.RekeningAirList = new ObservableCollection<TagihanRekeningAirWpf>(ListData.Where(x => (x.Total ?? 0) > 0).ToList());
                    }
                }

                _viewModel.IsEmptyForm2 = _viewModel.RekeningAirList.Count == 0;
                _viewModel.JumlahPokok = _viewModel.RekeningAirList.Sum(x => x.Total ?? 0);
                _viewModel.JumlahDpMaxForm = _viewModel.RekeningAirList.Sum(x => x.Total ?? 0) * _viewModel.DpMaxForm / 100;
                _viewModel.JumlahDpMinForm = _viewModel.RekeningAirList.Sum(x => (x.Administrasi ?? 0) + (x.AdministrasiLain ?? 0) + (x.Pemeliharaan ?? 0) + (x.PemeliharaanLain ?? 0) + (x.Retribusi ?? 0) + (x.RetribusiLain ?? 0) + (x.Denda ?? 0) + (x.Meterai ?? 0));

                _viewModel.IsPiutangAirAllSelected = true;

                Response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-pelanggan-air-detail",
                    new Dictionary<string, dynamic>() {
                        { "IdPelangganAir" , _viewModel.SelectedPelanggan.IdPelangganAir }
                    }));
                if (!Response.IsError)
                {
                    var result = Response.Data;
                    if (result.Status && result.Data != null)
                    {
                        var listData = result.Data.ToObject<ObservableCollection<MasterPelangganAirDetailDto>>();
                        if (listData.Count > 0)
                        {
                            _viewModel.NoHpForm = listData[0].NoHp;
                            _viewModel.NoTelpForm = listData[0].NoTelp;
                        }
                    }
                }

                Response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/rekening-non-air",
                    new Dictionary<string, dynamic>() {
                        { "IdPelangganAir" , _viewModel.SelectedPelanggan.IdPelangganAir }
                    }));
                if (!Response.IsError)
                {
                    var result = Response.Data;
                    if (result.Status && result.Data != null)
                    {
                        var listData = result.Data.ToObject<ObservableCollection<TagihanRekeningNonAirWpf>>();

                        _viewModel.RekeningNonAirList = listData;
                    }
                }

            }
            _viewModel.IsLoadingForm = false;
            _ = await Task.FromResult(_isTest);
        }
    }
}
