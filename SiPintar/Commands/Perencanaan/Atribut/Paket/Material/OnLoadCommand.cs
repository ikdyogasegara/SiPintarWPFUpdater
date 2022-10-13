using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Utilities;
using SiPintar.ViewModels.Perencanaan.Atribut.Paket;
using SiPintar.Views;

namespace SiPintar.Commands.Perencanaan.Atribut.Paket.Material
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly PaketMaterialViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;
        private new string ErrorMessage;

        private readonly string ApiVersion = App.Current.Resources["api_version"]?.ToString();

        public OnLoadCommand(PaketMaterialViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var section = parameter as string;

            ErrorMessage = null;

            switch (section)
            {
                case "paket":
                    await GetPaketAsync();
                    break;
                case "rincian":
                    await GetRincianAsync(_viewModel.SelectedPaket);
                    break;
                default:
                    await GetPaketAsync();
                    break;
            }

            ShowSnackbar();
        }

        private async Task GetPaketAsync()
        {
            _viewModel.PaketList = new ObservableCollection<MasterPaketMaterialDto>();
            _viewModel.RincianList = new List<MasterPaketMaterialDetailDto>();

            _viewModel.IsEmptyPaket = false;

            _viewModel.IsLoadingPaket = true;

            Dictionary<string, dynamic> param = null;
            if (_isTest)
                param = new Dictionary<string, dynamic> { { "TestId", _viewModel.TestId } };

            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{ApiVersion}/master-paket-material", param));

            if (!Response.IsError)
            {
                var Result = Response.Data;

                if (Result.Status && Result.Data != null)
                    _viewModel.PaketList = Result.Data.ToObject<ObservableCollection<MasterPaketMaterialDto>>();
                else
                    ErrorMessage = Response.Data.Ui_msg;
            }
            else
            {
                ErrorMessage = Response.Error.Message;
            }

            _viewModel.IsEmptyPaket = _viewModel.PaketList.Count == 0;

            _viewModel.IsLoadingPaket = false;
        }

        private async Task GetRincianAsync(MasterPaketMaterialDto PaketMaterial)
        {
            _viewModel.RincianList = new List<MasterPaketMaterialDetailDto>();

            if (PaketMaterial == null || PaketMaterial.Detail == null || PaketMaterial.Detail.Count == 0)
            {
                _viewModel.IsEmptyRincian = true;
                _viewModel.IsLoadingRincian = false;
                return;
            }

            _viewModel.IsEmptyRincian = false;

            _viewModel.IsLoadingRincian = true;

            _viewModel.RincianList = new List<MasterPaketMaterialDetailDto>(PaketMaterial.Detail);

            _viewModel.IsLoadingRincian = false;

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private void ShowSnackbar()
        {
            if (ErrorMessage != null && !_isTest)
                Application.Current.Dispatcher.Invoke(delegate
                {
                    var mainView = (PerencanaanView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                    mainView.ShowSnackbar(ErrorMessage, "danger");
                });
        }
    }
}
