using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Perencanaan.Atribut.Paket;

namespace SiPintar.Commands.Perencanaan.Atribut.Paket.Rab
{
    public class OnSubmitEditCommand : AsyncCommandBase
    {
        private readonly PaketRabViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnSubmitEditCommand(PaketRabViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            string SuccessMessage = null;
            string ErrorMessage = null;

            _viewModel.IsLoadingForm = true;

            DialogHelper.ShowLoading(_isTest, "PerencanaanRootDialog");

            var body = new Dictionary<string, dynamic>
            {
                { "IdPaket", _viewModel.SelectedData.IdPaket },
                { "KodePaket", _viewModel.KodeForm },
                { "NamaPaket", _viewModel.NamaForm },
                { "IdPaketMaterial", _viewModel.PaketMaterialForm.IdPaketMaterial },
                { "IdPaketOngkos", _viewModel.PaketOngkosForm.IdPaketOngkos },
                { "PpnMaterial", _viewModel.PpnMaterialForm },
                { "PpnMaterialTambahan", _viewModel.PpnMaterialTambahanForm },
                { "PpnOngkos", _viewModel.PpnOngkosForm },
                { "PpnOngkosTambahan", _viewModel.PpnOngkosTambahanForm },
                { "PersentaseKeuntungan", _viewModel.PersentaseKeuntunganForm },
                { "PersentaseJasaDariBahan", _viewModel.PersentaseJasaDariBahanForm },
                { "HargaNetMaterial", _viewModel.HargaNetMaterialForm },
                { "HargaNetOngkos", _viewModel.HargaNetOngkosForm },
                { "HargaNetPaket", _viewModel.HargaPaketForm },
                { "Status", true },
            };

            var Response = await Task.Run(() => _restApi.PatchAsync($"/api/{ApiVersion}/master-paket", null, body));
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                    SuccessMessage = Response.Data.Ui_msg;
                else
                    ErrorMessage = Response.Data.Ui_msg;
            }
            else
            {
                ErrorMessage = Response.Error.Message;
            }

            if (App.OpenedWindow.ContainsKey("perencanaan"))
                DialogHelper.ShowSuccessErrorDialog(ErrorMessage, SuccessMessage, _isTest,
                    "PerencanaanRootDialog", App.OpenedWindow["perencanaan"], true, _viewModel.OnRefreshCommand, true);

            _viewModel.IsLoadingForm = false;
        }
    }
}
