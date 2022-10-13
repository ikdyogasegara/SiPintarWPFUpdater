using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan.KoreksiRekeningAir;

namespace SiPintar.Commands.Hublang.Pelayanan.KoreksiRekeningAir.PermohonanKoreksi
{
    [ExcludeFromCodeCoverage]
    public class OnCariPelangganCommand : AsyncCommandBase
    {
        private readonly PermohonanKoreksiViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnCariPelangganCommand(PermohonanKoreksiViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoadingForm = true;

            var param = parameter as Dictionary<string, dynamic>;
            var response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-pelanggan-air", param));
            _viewModel.DataPelanggan = new ObservableCollection<MasterPelangganGlobalWpf>();
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status && result.Data != null)
                {
                    _viewModel.DataPelanggan = result.Data.ToObject<ObservableCollection<MasterPelangganGlobalWpf>>();
                    _viewModel.TotalPagePelanggan = result.TotalPage;
                    _viewModel.TotalRecordPelanggan = result.Record;
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg);
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message);

            _viewModel.IsPreviousButtonEnabledPelanggan = _viewModel.CurrentPagePelanggan > 1;
            _viewModel.IsNextButtonEnabledPelanggan = _viewModel.CurrentPagePelanggan < _viewModel.TotalPagePelanggan;
            _viewModel.IsLeftPageNumberActivePelanggan = _viewModel.CurrentPagePelanggan == 1;
            _viewModel.IsRightPageNumberActivePelanggan = _viewModel.CurrentPagePelanggan == _viewModel.TotalPagePelanggan;
            _viewModel.IsLeftPageNumberFillerVisiblePelanggan = _viewModel.CurrentPagePelanggan != 2;
            _viewModel.IsRightPageNumberFillerVisiblePelanggan = _viewModel.CurrentPagePelanggan != _viewModel.TotalPagePelanggan - 1;
            _viewModel.IsMiddlePageNumberVisiblePelanggan = _viewModel.CurrentPagePelanggan > 1 && _viewModel.CurrentPagePelanggan < _viewModel.TotalPagePelanggan;

            _viewModel.IsLoadingForm = false;
            await Task.FromResult(_isTest);
        }
    }
}
