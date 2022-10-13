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
    public class GetDataPetugasCommand : AsyncCommandBase
    {
        private readonly RuteBacaMeterViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public GetDataPetugasCommand(RuteBacaMeterViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            ShowLoadingForm();

            _viewModel.PetugasBacaList = new ObservableCollection<MasterPetugasBacaDto>();

            var param = new Dictionary<string, dynamic>();

            if (!string.IsNullOrEmpty(_viewModel.KodePetugasFilter))
                param.Add("KodePetugasBaca", _viewModel.KodePetugasFilter);
            if (!string.IsNullOrEmpty(_viewModel.NamaPetugasFilter))
                param.Add("PetugasBaca", _viewModel.NamaPetugasFilter);

            if (_isTest)
                param.Add("TestId", _viewModel.TestId);

            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/master-petugas-baca", param);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.PetugasBacaList = Result.Data.ToObject<ObservableCollection<MasterPetugasBacaDto>>();
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
                rayon.IsEmptyPetugasSearch = _viewModel.PetugasBacaList.Count == 0;
            }
            else if (_viewModel.PageViewModel is ViewModels.Bacameter.SistemKontrol.RuteBacaMeter.PetugasBacaViewModel petugas)
            {
                petugas.IsLoadingForm = false;
            }
        }
    }
}
