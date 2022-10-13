using System.Collections.Generic;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.Tunjangan;

namespace SiPintar.Commands.Personalia.Tunjangan.TunjanganBulanan
{
    public class OnSubmitDeleteFormCommand : AsyncCommandBase
    {
        private readonly TunjanganBulananViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitDeleteFormCommand(TunjanganBulananViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            object dg = null;
            DialogHelper.CloseDialog(_isTest, false, "PersonaliaRootDialog");
            dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, false, "PersonaliaRootDialog", "Mohon tunggu", "sedang memproses tindakan...", "");

            var body = new Dictionary<string, dynamic>();
            if (_viewModel.DeleteMode1)
                body.Add("IdTunjanganBulanan", _viewModel.SelectedData.IdTunjanganBulanan);
            else if (_viewModel.DeleteMode2)
            {
                body.Add("IdKodeGaji", _viewModel.SelectedData.IdKodeGaji);
                body.Add("KodePeriode", _viewModel.SelectedData.KodePeriode);
                body.Add("IdJenisTunjangan", _viewModel.SelectedData.IdJenisTunjangan);
            }
            else if (_viewModel.DeleteMode3)
            {
                body.Add("IdKodeGaji", _viewModel.SelectedData.IdKodeGaji);
                body.Add("KodePeriode", _viewModel.SelectedData.KodePeriode);
            }

            RestApiResponse Response = await Task.Run(() => _restApi.DeleteAsync($"/api/{_restApi.GetApiVersion()}/master-set-tunjangan-bulanan", body));

            DialogHelper.CloseDialog(_isTest, false, "PersonaliaRootDialog", dg);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                DialogHelper.ShowSnackbar(_isTest, Result.Status ? "success" : "danger", Result.Ui_msg, "personalia");
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error.Message, "personalia");

            await Task.Run(() => ((AsyncCommandBase)_viewModel.OnLoadCommand).ExecuteAsync(null));

            await Task.FromResult(_isTest);
        }
    }
}
