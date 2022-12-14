using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Atribut.Loket;
using SiPintar.Views;

namespace SiPintar.Commands.Billing.Atribut.Loket.LoketPembatalan
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly LoketPembatalanViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnLoadCommand(LoketPembatalanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            string ErrorMessage = null;
            _viewModel.IsEmpty = false;

            _viewModel.IsLoading = true;

            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{ApiVersion}/master-alasan-batal"));

            _viewModel.PembatalanLoketList = new ObservableCollection<MasterAlasanBatalDto>();

            if (!Response.IsError)
            {
                var Result = Response.Data;

                if (Result.Status && Result.Data != null)
                    _viewModel.PembatalanLoketList = Result.Data.ToObject<ObservableCollection<MasterAlasanBatalDto>>();
                else
                    ErrorMessage = Response.Data.Ui_msg;
            }
            else
            {
                ErrorMessage = Response.Error.Message;
            }

            ShowDialog(ErrorMessage);

            if (_viewModel.PembatalanLoketList.Count == 0)
                _viewModel.IsEmpty = true;

            _viewModel.IsLoading = false;
        }

        private void ShowDialog(string ErrorMessage)
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
