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
using SiPintar.Views.Billing.Supervisi.PelangganL2T2;

namespace SiPintar.Commands.Billing.Supervisi.PelangganL2T2
{
    public class OnOpenPiutangCommand : AsyncCommandBase
    {
        private readonly PelangganL2T2ViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;
        private readonly Dictionary<string, dynamic> _testBody;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnOpenPiutangCommand(PelangganL2T2ViewModel viewModel, IRestApiClientModel restApi, bool isTest = false, Dictionary<string, dynamic> testBody = null)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
            _testBody = testBody;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.SelectedData == null)
                return;

            string ErrorMessage = null;
            _viewModel.IsEmptyDialog = false;

            _viewModel.PiutangList = new ObservableCollection<RekeningLlttPiutangDto>();
            _viewModel.TotalPiutang = 0;

            _viewModel.IsLoadingForm = true;

            OpenDialog();

            var param = new Dictionary<string, dynamic>
            {
                { "IdPelangganLltt" , _viewModel.SelectedData.IdPelangganLltt },
            };

            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/rekening-lltt-piutang", _testBody ?? param);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.PiutangList = Result.Data.ToObject<ObservableCollection<RekeningLlttPiutangDto>>();
                    _viewModel.TotalPiutang = _viewModel.PiutangList.Sum(p => p.Biaya);
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
