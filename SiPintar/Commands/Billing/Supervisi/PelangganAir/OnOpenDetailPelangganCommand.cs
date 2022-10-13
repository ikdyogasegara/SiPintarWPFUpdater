using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views;
using SiPintar.Views.Billing.Supervisi.PelangganAir;

namespace SiPintar.Commands.Billing.Supervisi.PelangganAir
{
    public class OnOpenDetailPelangganCommand : AsyncCommandBase
    {
        private readonly PelangganAirViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnOpenDetailPelangganCommand(PelangganAirViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading)
                return;

            DialogHelper.ShowLoading(_isTest, "BillingRootDialog");

            string ErrorMessage = null;

            _viewModel.DetailData = new MasterPelangganAirDetailDto();

            _viewModel.IsLoadingForm = true;

            OpenDialog();

            var param = new Dictionary<string, dynamic>
            {
                { "IdPelangganAir" , _viewModel.SelectedData.IdPelangganAir },
            };

            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-pelanggan-air-detail", param);
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    _viewModel.DetailData = result.Data.Any() ? result.Data[0].ToObject<MasterPelangganAirDetailDto>() : new MasterPelangganAirDetailDto();
                }
                else
                {
                    ErrorMessage = response.Data.Ui_msg;
                }
            }
            else
            {
                ErrorMessage = response.Error.Message;
            }

            ShowSnackbar(ErrorMessage);

            _viewModel.IsLoadingForm = false;
        }

        [ExcludeFromCodeCoverage]
        public void ShowSnackbar(string ErrorMessage)
        {
            if (ErrorMessage != null && !_isTest)
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    var mainView = (BillingView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                    mainView.ShowSnackbar(ErrorMessage, "danger");
                });
            }
        }

        [ExcludeFromCodeCoverage]
        public void OpenDialog()
        {
            if (!_isTest)
            {
                Application.Current.Dispatcher.Invoke(delegate { CloseDialog(); });

                _ = DialogHost.Show(new DetailPelangganView(_viewModel), "BillingRootDialog");
            }
        }

        [ExcludeFromCodeCoverage]
        private void CloseDialog()
        {
            DialogHost.Close("BillingRootDialog");
        }
    }
}
