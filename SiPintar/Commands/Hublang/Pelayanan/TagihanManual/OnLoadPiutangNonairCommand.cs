using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Commands.Hublang.Pelayanan.TagihanManual
{
    public class OnLoadPiutangNonairCommand : AsyncCommandBase
    {
        private readonly TagihanManualViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadPiutangNonairCommand(TagihanManualViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var param = new Dictionary<string, dynamic>
            {
                { "PageSize" , 1000000 },
                { "CurrentPage" , 1 },
                { "StatusTransaksi" , false },

            };

            var cekPiutang = false;

            if (_viewModel.IsEdit == false)
            {
                if (_viewModel.SelectedPelanggan is MasterPelangganAirDto)
                {
                    param.Add("IdPelangganAir", _viewModel.SelectedPelanggan.IdPelangganAir);
                    cekPiutang = true;
                }
                else if (_viewModel.SelectedPelanggan is MasterPelangganLimbahDto)
                {
                    param.Add("IdPelangganLimbah", _viewModel.SelectedPelanggan.IdPelangganLimbah);
                    cekPiutang = true;
                }
                else if (_viewModel.SelectedPelanggan is MasterPelangganLlttDto)
                {
                    param.Add("IdPelangganLltt", _viewModel.SelectedPelanggan.IdPelangganLltt);
                    cekPiutang = true;
                }
            }
            else
            {
                if (_viewModel.SelectedData.IdPelangganAir != null)
                {
                    param.Add("IdPelangganAir", _viewModel.SelectedData.IdPelangganAir);
                    cekPiutang = true;
                }
                else if (_viewModel.SelectedData.IdPelangganLimbah != null)
                {
                    param.Add("IdPelangganLimbah", _viewModel.SelectedData.IdPelangganLimbah);
                    cekPiutang = true;
                }
                else if (_viewModel.SelectedData.IdPelangganLltt != null)
                {
                    param.Add("IdPelangganLltt", _viewModel.SelectedData.IdPelangganLltt);
                    cekPiutang = true;
                }
            }

            if (cekPiutang)
            {
                var response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/rekening-non-air", param));
                _viewModel.PiutangNonAirList = new ObservableCollection<RekeningNonAirDto>();

                if (!response.IsError)
                {
                    var result = response.Data;
                    if (result.Status && result.Data != null)
                    {
                        _viewModel.PiutangNonAirList = result.Data.ToObject<ObservableCollection<RekeningNonAirDto>>();
                    }
                    else
                        DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg);
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message);
            }

            await Task.FromResult(_isTest);
        }
    }
}
