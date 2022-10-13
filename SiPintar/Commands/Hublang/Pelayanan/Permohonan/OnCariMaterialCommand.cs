using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Commands.Hublang.Pelayanan.Permohonan
{
    [ExcludeFromCodeCoverage]
    public class OnCariMaterialCommand : AsyncCommandBase
    {
        private readonly PermohonanViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnCariMaterialCommand(PermohonanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoadingCariMaterialOngkos = true;
            _viewModel.DaftarMaterial.Clear();
            var body = parameter as Dictionary<string, dynamic>;

            RestApiResponse Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-material", body);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status && Result.Data != null)
                {
                    _viewModel.DaftarMaterial = Result.Data.ToObject<ObservableCollection<MasterMaterialDto>>();
                    _viewModel.TotalPageMaterialOngkos = Result.TotalPage;
                    _viewModel.TotalRecordMaterialOngkos = Result.Record;
                    _viewModel.IsLoadingCariMaterialOngkos = false;
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", Result.Ui_msg);
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error?.Message);

            _viewModel.IsPreviousButtonEnabledMaterialOngkos = _viewModel.CurrentPageMaterialOngkos > 1;
            _viewModel.IsNextButtonEnabledMaterialOngkos = _viewModel.CurrentPageMaterialOngkos < _viewModel.TotalPageMaterialOngkos;
            _viewModel.IsLeftPageNumberActiveMaterialOngkos = _viewModel.CurrentPageMaterialOngkos == 1;
            _viewModel.IsRightPageNumberActiveMaterialOngkos = _viewModel.CurrentPageMaterialOngkos == _viewModel.TotalPageMaterialOngkos;
            _viewModel.IsLeftPageNumberFillerVisibleMaterialOngkos = _viewModel.CurrentPageMaterialOngkos != 2;
            _viewModel.IsRightPageNumberFillerVisibleMaterialOngkos = _viewModel.CurrentPageMaterialOngkos != _viewModel.TotalPageMaterialOngkos - 1;
            _viewModel.IsMiddlePageNumberVisibleMaterialOngkos = _viewModel.CurrentPageMaterialOngkos > 1 && _viewModel.CurrentPageMaterialOngkos < _viewModel.TotalPageMaterialOngkos;
            _viewModel.IsEmptyFormMaterial = _viewModel.TotalRecordMaterialOngkos == 0;
            _viewModel.IsLoadingCariMaterialOngkos = false;

            await Task.FromResult(_isTest);
        }
    }
}
