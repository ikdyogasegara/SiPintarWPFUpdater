using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views;
using SiPintar.Views.Billing.Supervisi.RekeningAir;

namespace SiPintar.Commands.Billing.Supervisi.RekeningAir
{
    public class OnOpenRiwayatPiutangCommand : AsyncCommandBase
    {
        private readonly RekeningAirViewModel _viewModel;
        private readonly bool _isTest;
        private readonly IRestApiClientModel _restApi;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnOpenRiwayatPiutangCommand(RekeningAirViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.SelectedData is null)
                return;

            string ErrorMessage = null;
            _viewModel.IsEmptyDialog = false;

            _viewModel.PiutangList = new ObservableCollection<RekeningAirPiutangDto>();

            _viewModel.IsLoadingForm = true;

            OpenDialog();

            var param = new Dictionary<string, dynamic>
            {
                { "IdPelangganAir" , _viewModel.SelectedData.IdPelangganAir },
            };

            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/rekening-air-piutang", param);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.PiutangList = Result.Data.ToObject<ObservableCollection<RekeningAirPiutangDto>>();
                }
                else
                {
                    ErrorMessage = Response.Data.Ui_msg;
                }
            }
            else
            {
                ErrorMessage = Response.Error.Message;
            }

            ShowSnackbar(ErrorMessage);
            _viewModel.IsEmptyDialog = !_viewModel.PiutangList.Any();
            _viewModel.IsLoadingForm = false;
            await Task.FromResult(true);
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
            if (!_isTest) _ = DialogHost.Show(new RiwayatPiutangView(_viewModel), "BillingRootDialog");
        }
    }
}
