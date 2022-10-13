using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan.Info;

namespace SiPintar.Commands.Hublang.Pelayanan.Info.HistoriPermohonan
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly HistoriPermohonanViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(HistoriPermohonanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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
            int? idPelangganAir = TemporariDataHelper.IdPelangganAir;
            int? idPelangganLimbah = TemporariDataHelper.IdPelangganLimbah;
            int? idPelangganLltt = TemporariDataHelper.IdPelangganLltt;

            param = new Dictionary<string, dynamic>
            {
                { "Limit" , 100000000 },
            };

            if (idPelangganAir.HasValue)
                param.Add("IdPelangganAir", idPelangganAir);

            if (idPelangganLimbah.HasValue)
                param.Add("IdPelangganLimbah", idPelangganLimbah);

            if (idPelangganLltt.HasValue)
                param.Add("IdPelangganLltt", idPelangganLltt);


            Response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/histori-permohonan-global", param));
            _viewModel.HistoriPermohonanList = new ObservableCollection<SummaryHistoriPermohonanDto>();

            if (!Response.IsError)
            {
                var Result = Response.Data;

                if (Result.Status && Result.Data != null)
                    _viewModel.HistoriPermohonanList = Result.Data.ToObject<ObservableCollection<SummaryHistoriPermohonanDto>>();
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", Response.Data.Ui_msg);
            }
            else
            {
                DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error.Message);
            }


            _viewModel.IsEmpty = _viewModel.HistoriPermohonanList.Count == 0;

            _viewModel.IsLoading = false;

            await Task.FromResult(_isTest);
        }
    }
}
