using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views;

namespace SiPintar.Commands.Billing.Supervisi.PenghapusanRekening
{
    public class OnSearchPiutangCommand : AsyncCommandBase
    {
        private readonly PenghapusanRekeningViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;
        private readonly Dictionary<string, dynamic> _testBody;

        public OnSearchPiutangCommand(PenghapusanRekeningViewModel viewModel, IRestApiClientModel restApi, bool isTest = false, Dictionary<string, dynamic> testBody = null)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
            _testBody = testBody;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.NoSambPiutang == null)
                return;

            _viewModel.PiutangList = new ObservableCollection<RekeningAirPiutangWpf>();
            _viewModel.FirstPiutang = new RekeningAirPiutangWpf();
            _viewModel.TotalPiutang = 0;
            _viewModel.IsLoadingForm = true;

            var param = new Dictionary<string, dynamic>
            {
                { "NoSamb" , _viewModel.NoSambPiutang },
                { "SelainHapusSecaraAkuntansi" , true },
            };

            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/rekening-air-piutang", _testBody ?? param);
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    _viewModel.PiutangList = result.Data.ToObject<ObservableCollection<RekeningAirPiutangWpf>>();
                    _viewModel.FirstPiutang = _viewModel.PiutangList.FirstOrDefault();
                    _viewModel.TotalPiutang = _viewModel.PiutangList.Sum(p => p.Total);
                }
                else
                {
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg);
                }
            }
            else
            {
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message);
            }

            _viewModel.IsEmptyDialog = !_viewModel.PiutangList.Any();
            _viewModel.IsLoadingForm = false;

            await Task.FromResult(true);
        }
    }
}
