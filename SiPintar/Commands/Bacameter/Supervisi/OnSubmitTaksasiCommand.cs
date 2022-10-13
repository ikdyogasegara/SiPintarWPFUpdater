using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Bacameter;

namespace SiPintar.Commands.Bacameter.Supervisi
{
    public class OnSubmitTaksasiCommand : AsyncCommandBase
    {
        private readonly SupervisiViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitTaksasiCommand(SupervisiViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoadingForm = true;
            _viewModel.SelectedData.IsUpdatingKoreksi = true;

            var body = new Dictionary<string, dynamic>
            {
                { "IdPelangganAir", _viewModel.SelectedData.IdPelangganAir},
                { "IdPeriode", _viewModel.SelectedData.IdPeriode },
                { "Taksasi", true }
            };

            if (_isTest)
                body.Add("TestId", _viewModel.TestId);

            var response = await Task.Run(() => _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/rekening-air-update", null, body));
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    _viewModel.SelectedData.TaksasiWpf = body["Taksasi"];
                }
                else
                {
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg, "bacameter");
                }
            }
            else
            {
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error?.Message, "bacameter");
            }

            _viewModel.IsLoadingForm = false;
            _viewModel.SelectedData.IsUpdatingKoreksi = false;
        }
    }
}
