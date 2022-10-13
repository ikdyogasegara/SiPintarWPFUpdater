using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using SiPintar.Utilities;
using SiPintar.ViewModels.Bacameter.SistemKontrol.TarifGolongan;
using SiPintar.Views;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Bacameter.SistemKontrol.TarifGolongan.Diameter
{
    public class OnSubmitDeleteCommand : AsyncCommandBase
    {
        private readonly DiameterViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnSubmitDeleteCommand(DiameterViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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

            var entityId = new Dictionary<string, dynamic>
            {
                { "IdDiameter", _viewModel.SelectedData.IdDiameter }
            };

            var Response = await Task.Run(() => _restApi.DeleteAsync($"/api/{ApiVersion}/master-tarif-diameter", entityId));
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

            await ShowDialogAsync(ErrorMessage, SuccessMessage);

            _viewModel.IsLoadingForm = false;
        }

        [ExcludeFromCodeCoverage]
        public async Task ShowDialogAsync(string ErrorMessage, string SuccessMessage)
        {
            if (!_isTest)
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
                if (ErrorMessage != null)
                {
                    await DialogHost.Show(new DialogGlobalView("Terjadi Kesalahan", ErrorMessage, "error"), "BacameterRootDialog");
                }
                else if (SuccessMessage != null)
                {
                    var mainView = (BacameterView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                    if (mainView != null)
                        mainView.ShowSnackbar(SuccessMessage, "success");
                    _viewModel.OnRefreshCommand.Execute(null);
                }
            }
        }
    }
}
