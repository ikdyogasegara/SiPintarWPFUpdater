using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Atribut.Loket;
using SiPintar.Views.Billing.Atribut.Loket.LoketTerdaftar;

namespace SiPintar.Commands.Billing.Atribut.Loket.LoketTerdaftar
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly LoketTerdaftarViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnOpenEditFormCommand(LoketTerdaftarViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.SelectedData == null)
                return;

            _viewModel.IsEdit = true;
            _viewModel.KodeLoketForm = _viewModel.SelectedData.KodeLoket;
            _viewModel.NamaLoketForm = _viewModel.SelectedData.NamaLoket;
            _viewModel.StatusForm = _viewModel.SelectedData.Status == true ? _viewModel.StatusList[0] : _viewModel.StatusList[1];
            _viewModel.FlagMitraForm = _viewModel.SelectedData.FlagMitra == true;

            _viewModel.IsLoadingForm = true;

            ShowDialog();

            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{ApiVersion}/master-wilayah"));
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status && Result.Data.Any())
                {
                    _viewModel.WilayahList = Result.Data.ToObject<ObservableCollection<MasterWilayahDto>>();
                }
            }

            if (_viewModel.WilayahList != null)
                _viewModel.WilayahForm = _viewModel.WilayahList.FirstOrDefault(i => i.KodeWilayah == _viewModel.SelectedData.KodeWilayah);

            _viewModel.IsLoadingForm = false;
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest)
                _ = DialogHost.Show(new DialogFormView(_viewModel), "BillingRootDialog");
        }
    }
}
