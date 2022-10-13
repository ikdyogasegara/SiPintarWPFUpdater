using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Utilities;
using SiPintar.ViewModels.Loket;

namespace SiPintar.Commands.Loket.Angsuran
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly AngsuranViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(AngsuranViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.CurrentSection = "Tunggakan";
            _viewModel.IsDetailAngsuranVisible = false;
            _ = GetJenisNonAirAsync();
            _ = GetPetugasAsync();

            await Task.FromResult(_isTest);
        }

        public async Task GetPetugasAsync()
        {
            var param = new Dictionary<string, dynamic>
            {
                { "PageSize" , 1000 },
                { "CurrentPage" , 1 },
            };

            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-user", param);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.PetugasList = Result.Data.ToObject<ObservableCollection<MasterUserDto>>();
                }
            }
        }

        public async Task GetJenisNonAirAsync()
        {
            var param = new Dictionary<string, dynamic>
            {
                { "PageSize" , 1000 },
                { "CurrentPage" , 1 },
            };

            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-jenis-non-air", param);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.JenisNonAirList = Result.Data.ToObject<ObservableCollection<MasterJenisNonAirDto>>();
                }
            }
        }
    }
}
