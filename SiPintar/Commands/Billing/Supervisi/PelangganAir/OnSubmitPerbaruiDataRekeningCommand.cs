using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Billing.Supervisi.PelangganAir
{
    public class OnSubmitPerbaruiDataRekeningCommand : AsyncCommandBase
    {
        private readonly PelangganAirViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitPerbaruiDataRekeningCommand(PelangganAirViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            string successMessage = null;
            string errorMessage = null;

            _viewModel.IsLoadingForm = true;

            var param = new ParamPerbaruiDataRekeningAirDto
            {
                KodePeriode = (_viewModel.YearPerbaruiData * 100) + _viewModel.MonthPerbaruiData,
                IdPelangganAir = new List<int?> { _viewModel.SelectedData.IdPelangganAir }
            };

            var type = typeof(ParamPerbaruiDataRekeningAirDto);
            var properties = type.GetProperties();
            var body = new Dictionary<string, dynamic>();

            foreach (var property in properties)
            {
                if (property.Name != "IdPdam" && property.Name != "IdUserRequest")
                {
                    body.Add(property.Name, property.GetValue(param));
                }
            }

            var response = await Task.Run(() => _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/rekening-air-perbarui-data", null, body));
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                    successMessage = response.Data.Ui_msg;
                else
                    errorMessage = response.Data.Ui_msg;
            }
            else
            {
                errorMessage = response.Error.Message;
            }

            await ShowDialogAsync(errorMessage, successMessage);

            _viewModel.IsLoadingForm = false;
        }

        [ExcludeFromCodeCoverage]
        private async Task ShowDialogAsync(string errorMessage, string successMessage)
        {
            if (!_isTest)
            {
                if (errorMessage != null)
                {
                    await DialogHost.Show(new DialogGlobalView("Terjadi Kesalahan", errorMessage, "error"), "SupervisiPelangganDialog");
                }
                else if (successMessage != null)
                {
                    DialogHost.CloseDialogCommand.Execute(null, null);
                    var mainView = (BillingView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                    mainView.ShowSnackbar(successMessage, "success");
                    _viewModel.OnFilterCommand.Execute(null);
                }
            }
        }
    }
}
