using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Utilities;
using SiPintar.ViewModels.Perencanaan.Atribut;
using SiPintar.Views.Perencanaan.Atribut.Ongkos;

namespace SiPintar.Commands.Perencanaan.Atribut.Ongkos
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly OngkosViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = App.Current.Resources["api_version"]?.ToString();

        public OnOpenAddFormCommand(OngkosViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
            _restApi = restApi;
        }

        public override async Task ExecuteAsync(object parameter)
        {

            if (_viewModel.IsLoading)
                return;

            _viewModel.IsEdit = false;
            _viewModel.OngkosForm = new MasterOngkosDto()
            {
                OngkosLimbah = _viewModel.SelectedDataKategori.Value,
                Perhitungan = "Reguler",
            };

            _viewModel.SelectedDataTipe = new KeyValuePair<int, string>();
            _viewModel.SelectedDataPostBiaya = new KeyValuePair<int, string>();

            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/master-paket-material");

            _viewModel.PaketMaterialList = new ObservableCollection<MasterPaketMaterialDto>();

            if (!Response.IsError)
            {
                var Result = Response.Data;

                if (Result.Status && Result.Data != null)
                {
                    _viewModel.PaketMaterialList = Result.Data.ToObject<ObservableCollection<MasterPaketMaterialDto>>();
                    _viewModel.TotalRecord = (int)Response.Data.Record;
                    _viewModel.TotalPage = Result.TotalPage;
                }
            }

            _viewModel.IsLoadingForm = true;

            ShowDialog();

            _viewModel.IsLoadingForm = false;

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest) _ = DialogHost.Show(new DialogFormView(_viewModel), "PerencanaanRootDialog");
        }
    }
}
