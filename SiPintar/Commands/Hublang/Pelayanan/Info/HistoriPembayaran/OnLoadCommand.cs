using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan.Info;

namespace SiPintar.Commands.Hublang.Pelayanan.Info.HistoriPembayaran
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly HistoriPembayaranViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(HistoriPembayaranViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoading = true;

            RestApiResponse response = null;
            Dictionary<string, dynamic> param = null;
            int? idPelangganAir = TemporariDataHelper.IdPelangganAir;
            int? idPelangganLimbah = TemporariDataHelper.IdPelangganLimbah;
            int? idPelangganLltt = TemporariDataHelper.IdPelangganLltt;

            param = new Dictionary<string, dynamic>
            {
                { "Limit" , 48 },
                { "TipeHistori" , _viewModel.TipeHistori },
            };

            if (idPelangganAir.HasValue)
                param.Add("IdPelangganAir", idPelangganAir);

            if (idPelangganLimbah.HasValue)
                param.Add("IdPelangganLimbah", idPelangganLimbah);

            if (idPelangganLltt.HasValue)
                param.Add("IdPelangganLltt", idPelangganLltt);


            response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/histori-pembayaran-global", param));
            _viewModel.HistoriPembayaranList = new ObservableCollection<SummaryHistoriPembayaranDto>();

            if (!response.IsError)
            {
                var Result = response.Data;

                if (Result.Status && Result.Data != null)
                    _viewModel.HistoriPembayaranList = Result.Data.ToObject<ObservableCollection<SummaryHistoriPembayaranDto>>();
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg);
            }
            else
            {
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message);
            }


            _viewModel.IsEmpty = _viewModel.HistoriPembayaranList.Count == 0;

            _viewModel.IsLoading = false;

            await Task.FromResult(_isTest);
        }
    }
}
