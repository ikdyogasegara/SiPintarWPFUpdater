using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using SiPintar.Views.Billing.Supervisi.PelangganLimbah;

namespace SiPintar.Commands.Billing.Supervisi.PelangganLimbah
{
    public class OnOpenPiutangCommand : AsyncCommandBase
    {
        private readonly PelangganLimbahViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnOpenPiutangCommand(PelangganLimbahViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.SelectedData == null)
                return;

            string ErrorMessage = null;
            _viewModel.IsEmptyDialog = false;

            _viewModel.PiutangList = new ObservableCollection<RekeningLimbahPiutangDto>();

            _viewModel.IsLoadingForm = true;

            OpenDialog();

            var param = new Dictionary<string, dynamic>
            {
                { "IdPelangganLimbah" , _viewModel.SelectedData.IdPelangganLimbah },
            };

            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/rekening-limbah-piutang", param);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.PiutangList = Result.Data.ToObject<ObservableCollection<RekeningLimbahPiutangDto>>();
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
        }

        [ExcludeFromCodeCoverage]
        public void OpenDialog()
        {
            if (!_isTest) _ = DialogHost.Show(new PiutangView(_viewModel), "BillingRootDialog");
        }

        private void ShowSnackbar(string ErrorMessage)
        {
            if (ErrorMessage != null)
            {
                AppDispatcherHelper.Run(_isTest, () =>
                {
                    var mainView = (BillingView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                    mainView.ShowSnackbar(ErrorMessage, "danger");
                });
            }
        }
    }
}
