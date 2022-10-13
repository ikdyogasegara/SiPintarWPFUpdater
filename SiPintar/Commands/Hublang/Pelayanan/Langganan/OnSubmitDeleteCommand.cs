using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Commands.Hublang.Pelayanan.Langganan
{
    public class OnSubmitDeleteCommand : AsyncCommandBase
    {
        private readonly LanggananViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitDeleteCommand(LanggananViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            string successMessage = null;
            string ErrorMessage = null;

            _viewModel.IsLoadingForm = true;

            DialogHelper.ShowLoading(_isTest, "HublangRootDialog");

            var param = new Dictionary<string, dynamic>();

            switch (_viewModel.SelectedJenisPelanggan)
            {
                case "Pelanggan Air":
                    param.Add("IdPelangganAir", _viewModel.SelectedData.IdPelangganAir);
                    break;
                case "Pelanggan Limbah":
                    param.Add("IdPelangganLimbah", _viewModel.SelectedData.IdPelangganLimbah);
                    break;
                case "Pelanggan L2T2":
                    param.Add("IdPelangganLltt", _viewModel.SelectedData.IdPelangganLltt);
                    break;
            }
            var response = await Task.Run(() => _restApi.DeleteAsync($"/api/{_restApi.GetApiVersion()}/{_viewModel.EndPoint}", param));
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                    successMessage = response.Data.Ui_msg;
                else
                    ErrorMessage = response.Data.Ui_msg;
            }
            else
            {
                ErrorMessage = response.Error.Message;
            }

            if (App.OpenedWindow.ContainsKey("hublang"))
                DialogHelper.ShowSuccessErrorDialog(ErrorMessage, successMessage, _isTest, "HublangRootDialog", App.OpenedWindow["hublang"], true, _viewModel.OnRefreshCommand);

            _viewModel.IsLoadingForm = false;
        }
    }
}
