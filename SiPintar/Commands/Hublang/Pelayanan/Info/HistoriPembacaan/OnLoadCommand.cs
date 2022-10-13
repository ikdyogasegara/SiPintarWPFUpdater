using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan.Info;

namespace SiPintar.Commands.Hublang.Pelayanan.Info.HistoriPembacaan
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly HistoriPembacaanViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(HistoriPembacaanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoading = true;

            RestApiResponse Response = null;
            Dictionary<string, dynamic> param = null;
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

            param = new Dictionary<string, dynamic>
                {
                    { "PageSize" , 24 },
                    { "CurrentPage" , 1},
                    { "IdPelangganAir", IdPelangganAir}
                };

            Response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/rekening-air-riwayat-pakai", param));
            _viewModel.HistoriPembacaanList = new ObservableCollection<RiwayatPemakaianAirDto>();

            if (!Response.IsError)
            {
                var Result = Response.Data;

                if (Result.Status && Result.Data != null)
                    _viewModel.HistoriPembacaanList = Result.Data.ToObject<ObservableCollection<RiwayatPemakaianAirDto>>();
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", Response.Data.Ui_msg);
            }
            else
            {
                DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error.Message);
            }


            _viewModel.IsEmpty = _viewModel.HistoriPembacaanList.Count == 0;

            _viewModel.IsLoading = false;

            await Task.FromResult(_isTest);
        }
    }
}
