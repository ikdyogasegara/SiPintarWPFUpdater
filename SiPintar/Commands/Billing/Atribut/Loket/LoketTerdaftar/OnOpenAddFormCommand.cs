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
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly LoketTerdaftarViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnOpenAddFormCommand(LoketTerdaftarViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading)
                return;

            _viewModel.IsEdit = false;
            _viewModel.KodeLoketForm = null;
            _viewModel.NamaLoketForm = null;
            _viewModel.WilayahForm = null;
            _viewModel.FlagMitraForm = false;
            _viewModel.StatusForm = _viewModel.StatusList[0];

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
