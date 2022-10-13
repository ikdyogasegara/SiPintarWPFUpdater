using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Utilities;
using SiPintar.ViewModels.Loket.Angsuran;

namespace SiPintar.Commands.Loket.Angsuran.DetailAngsuran
{
    public class OnSearchPelangganCommand : AsyncCommandBase
    {
        private readonly DetailAngsuranViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSearchPelangganCommand(DetailAngsuranViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoadingFormDetail = true;

            try
            {
                var param = new Dictionary<string, dynamic>
                    {
                        { "PageSize" , 100 },
                        { "CurrentPage" , 1 },
                    };
                if (!string.IsNullOrEmpty(_viewModel.SearchNosamb))
                    param.Add("Nama", _viewModel.SearchNosamb);
                if (!string.IsNullOrEmpty(_viewModel.SearchName))
                    param.Add("NoSamb", _viewModel.SearchName);

                var Response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-pelanggan-air", param));

                _viewModel.DataPelanggan = new ObservableCollection<MasterPelangganAirDto>();

                if (!Response.IsError)
                {
                    var Result = Response.Data;

                    if (Result.Status && Result.Data != null)
                    {
                        var ListData = Result.Data.ToObject<ObservableCollection<MasterPelangganAirDto>>();

                        _viewModel.DataPelanggan = ListData;
                    }
                }

                _viewModel.IsEmptyFormDetail = _viewModel.DataPelanggan.Count == 0;
            }
            catch (Exception) { throw; }

            _viewModel.IsLoadingFormDetail = false;

            await Task.FromResult(_isTest);
        }

    }
}
