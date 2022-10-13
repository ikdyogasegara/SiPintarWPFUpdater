using System.Collections.Generic;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Commands.Hublang.Pelayanan.Permohonan
{
    public class OnSubmitBatalSpkFormCommand : AsyncCommandBase
    {
        private readonly PermohonanViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitBatalSpkFormCommand(PermohonanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            DialogHelper.CloseDialog(_isTest, false, "PerencanaanRootDialog");

            var selectedData = _viewModel.SelectedData;
            selectedData.IsUpdatingKoreksi = true;

            var param = new Dictionary<string, dynamic>
            {
                { "IdPermohonan", _viewModel.SelectedData.IdPermohonan },
                { "IdAlasanBatal", _viewModel.SelectedAlasanBatal.IdAlasanBatal }
            };

            var Response = await Task.Run(() => _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/{_viewModel.EndPointSpk}-pembatalan", null, param));
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    selectedData.AlasanBatalWpf = _viewModel.SelectedAlasanBatal.AlasanBatal;
                    selectedData.StatusPermohonanWpf = "SPK Dibatalkan";
                    selectedData.IsUpdatingKoreksi = false;
                    DialogHelper.ShowSnackbar(_isTest, "success", Result.Ui_msg, "perencanaan");
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", Result.Ui_msg, "perencanaan");
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error?.Message, "perencanaan");

            selectedData.IsUpdatingKoreksi = false;
            await Task.FromResult(_isTest);
        }
    }
}
