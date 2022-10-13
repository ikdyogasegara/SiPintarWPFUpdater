using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Utilities;
using SiPintar.ViewModels.Bacameter.SistemKontrol;
using SiPintar.ViewModels.Bacameter.SistemKontrol.RuteBacaMeter;

namespace SiPintar.Commands.Bacameter.SistemKontrol.RuteBacaMeter
{
    public class GetDataRayonCommand : AsyncCommandBase
    {
        private readonly RuteBacaMeterViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public GetDataRayonCommand(RuteBacaMeterViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            ShowLoadingForm();

            _viewModel.RayonList = new ObservableCollection<MasterRayonDto>();

            var param = new Dictionary<string, dynamic>();

            if (!string.IsNullOrEmpty(_viewModel.KodeRayonFilter))
                param.Add("KodeRayon", _viewModel.KodeRayonFilter);
            if (!string.IsNullOrEmpty(_viewModel.NamaRayonFilter))
                param.Add("NamaRayon", _viewModel.NamaRayonFilter);

            if (_isTest)
                param.Add("TestId", _viewModel.TestId);

            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/master-rayon", param);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.RayonList = Result.Data.ToObject<ObservableCollection<MasterRayonDto>>();
                }
            }

            CloseLoadingForm();

            await Task.FromResult(_isTest);
        }

        private void ShowLoadingForm()
        {
            if (_viewModel.PageViewModel is DataRayonViewModel rayon)
                rayon.IsLoadingForm = true;
            else if (_viewModel.PageViewModel is ViewModels.Bacameter.SistemKontrol.RuteBacaMeter.PetugasBacaViewModel petugas)
                petugas.IsLoadingForm = true;
        }

        private void CloseLoadingForm()
        {
            if (_viewModel.PageViewModel is DataRayonViewModel rayon)
            {
                rayon.IsLoadingForm = false;
            }
            else if (_viewModel.PageViewModel is ViewModels.Bacameter.SistemKontrol.RuteBacaMeter.PetugasBacaViewModel petugas)
            {
                petugas.IsLoadingForm = false;
                petugas.IsEmptyRayonSearch = _viewModel.RayonList.Count == 0;
            }
        }
    }
}
