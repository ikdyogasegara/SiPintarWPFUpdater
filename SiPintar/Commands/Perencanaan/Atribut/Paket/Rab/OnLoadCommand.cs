using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Utilities;
using SiPintar.ViewModels.Perencanaan.Atribut.Paket;
using SiPintar.Views;

namespace SiPintar.Commands.Perencanaan.Atribut.Paket.Rab
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly PaketRabViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnLoadCommand(PaketRabViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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

            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{ApiVersion}/master-paket"));

            _viewModel.DataList = new ObservableCollection<MasterPaketDto>();

            if (!Response.IsError)
            {
                var Result = Response.Data;

                if (Result.Status && Result.Data != null)
                    _viewModel.DataList = Result.Data.ToObject<ObservableCollection<MasterPaketDto>>();
                else
                    ErrorMessage = Response.Data.Ui_msg;
            }
            else
            {
                ErrorMessage = Response.Error.Message;
            }

            ShowDialog(ErrorMessage);

            if (_viewModel.DataList.Count == 0)
                _viewModel.IsEmpty = true;

            _ = GetPaketMaterialAsync();
            _ = GetPaketOngkosAsync();

            _viewModel.IsLoading = false;
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog(string ErrorMessage)
        {
            if (ErrorMessage != null && !_isTest)
                Application.Current.Dispatcher.Invoke(delegate
                {
                    var mainView = (PerencanaanView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                    mainView.ShowSnackbar(ErrorMessage, "danger");
                });
        }

        public async Task GetPaketMaterialAsync()
        {
            _viewModel.PaketMaterialList = new ObservableCollection<MasterPaketMaterialDto>();

            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/master-paket-material");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.PaketMaterialList = Result.Data.ToObject<ObservableCollection<MasterPaketMaterialDto>>();
                }
            }
        }

        public async Task GetPaketOngkosAsync()
        {
            _viewModel.PaketOngkosList = new ObservableCollection<MasterPaketOngkosDto>();

            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/master-paket-ongkos");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.PaketOngkosList = Result.Data.ToObject<ObservableCollection<MasterPaketOngkosDto>>();
                }
            }
        }
    }
}
