using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Utilities;
using SiPintar.ViewModels.Loket.Angsuran;


namespace SiPintar.Commands.Loket.Angsuran.DetailAngsuran
{
    public class OnLoadCommandAngsuran : AsyncCommandBase
    {
        private readonly DetailAngsuranViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommandAngsuran(DetailAngsuranViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.Parent.DataSelected == null)
                return;

            _viewModel.SelectedData = _viewModel.Parent.DataSelected;

            try
            {
                var param = new Dictionary<string, dynamic>
                {
                    { "IdAngsuran" , _viewModel.SelectedData.IdAngsuran },
                };

                RestApiResponse Response = null;
                if (_viewModel.Parent.ParentCurrentSection == "Tunggakan")
                {
                    Response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/rekening-angsuran-air-detail", param));
                    _viewModel.DetailAngsuranAirList = new ObservableCollection<RekeningAirAngsuranDetailDto>();
                    if (!Response.IsError)
                    {
                        var Result = Response.Data;

                        if (Result.Status && Result.Data != null)
                        {
                            var ListData = Result.Data.ToObject<ObservableCollection<RekeningAirAngsuranDetailDto>>();

                            _viewModel.DetailAngsuranAirList = ListData;
                        }
                    };

                    _viewModel.DetailAngsuranList = _viewModel.DetailAngsuranAirList;
                    _viewModel.DetailAngsuran = new RekeningAirAngsuranDetailDto();
                }
                else
                {
                    Response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/rekening-angsuran-non-air-detail", param));
                    _viewModel.DetailAngsuranNonAirList = new ObservableCollection<RekeningNonAirAngsuranDetailDto>();
                    if (!Response.IsError)
                    {
                        var Result = Response.Data;

                        if (Result.Status && Result.Data != null)
                        {
                            var ListData = Result.Data.ToObject<ObservableCollection<RekeningNonAirAngsuranDetailDto>>();

                            _viewModel.DetailAngsuranNonAirList = ListData;
                        }
                    };

                    _viewModel.DetailAngsuranList = _viewModel.DetailAngsuranNonAirList;
                    _viewModel.DetailAngsuran = new RekeningNonAirAngsuranDetailDto();

                }

            }
            catch (Exception)
            {
                throw;
            }


            await MasterPelangganAsync();
            await Task.FromResult(_isTest);
        }

        private async Task MasterPelangganAsync()
        {
            try
            {
                var param = new Dictionary<string, dynamic>
                {
                    { "PageSize" , 100 },
                    { "CurrentPage" , 1 },
                    { "Nosamb" , _viewModel.SelectedData.NoSamb },
                };


                var Response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-pelanggan-air", param));

                _viewModel.Parent.PelangganList = new ObservableCollection<MasterPelangganAirDto>();

                if (!Response.IsError)
                {
                    var Result = Response.Data;

                    if (Result.Status && Result.Data != null)
                    {
                        var ListData = Result.Data.ToObject<ObservableCollection<MasterPelangganAirDto>>();

                        _viewModel.Parent.PelangganList = ListData;
                    }
                };


                _viewModel.SelectedPelanggan = _viewModel.Parent.PelangganList.FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
