using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Atribut;
using SiPintar.ViewModels.Billing.Atribut.RuteBacaMeter;

namespace SiPintar.Commands.Billing.Atribut.RuteBacaMeter
{
    public class GetDataPetugasCommand : AsyncCommandBase
    {
        private readonly RuteBacaMeterViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

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

            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-petugas-baca", param);
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    _viewModel.PetugasBacaList = result.Data.ToObject<ObservableCollection<MasterPetugasBacaDto>>();
                }
            }

            CloseLoadingForm();

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void ShowLoadingForm()
        {
            if (_viewModel.PageViewModel is PerRayonViewModel rayon)
                rayon.IsLoadingForm = true;
            else if (_viewModel.PageViewModel is PerPetugasBacaViewModel petugas)
                petugas.IsLoadingForm = true;
        }

        [ExcludeFromCodeCoverage]
        private void CloseLoadingForm()
        {
            if (_viewModel.PageViewModel is PerRayonViewModel rayon)
            {
                rayon.IsLoadingForm = false;
                rayon.IsEmptyPetugasSearch = _viewModel.PetugasBacaList.Count == 0;
            }
            else if (_viewModel.PageViewModel is PerPetugasBacaViewModel petugas)
            {
                petugas.IsLoadingForm = false;
            }
        }
    }
}
