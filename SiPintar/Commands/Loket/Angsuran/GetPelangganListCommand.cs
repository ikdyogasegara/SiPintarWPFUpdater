using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Utilities;
using SiPintar.ViewModels.Loket;
using SiPintar.ViewModels.Loket.Tagihan;


namespace SiPintar.Commands.Loket.Angsuran
{
    public class GetPelangganListCommand : AsyncCommandBase
    {
        private readonly AngsuranViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current?.Resources["api_version"]?.ToString();

        public GetPelangganListCommand(AngsuranViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoadingForm = true;

            var param = new Dictionary<string, dynamic>
            {
                { "PageSize" , 100 },
                { "CurrentPage" , 1 },
            };
            if (!string.IsNullOrEmpty(_viewModel.NamaPelangganForm))
                param.Add("Nama", _viewModel.NamaPelangganForm);
            if (!string.IsNullOrEmpty(_viewModel.NoSambForm))
                param.Add("NoSamb", _viewModel.NoSambForm);
            if (!string.IsNullOrEmpty(_viewModel.AlamatForm))
                param.Add("Alamat", _viewModel.AlamatForm);

            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{ApiVersion}/master-pelanggan-air", param));

            _viewModel.PelangganList = new ObservableCollection<MasterPelangganAirDto>();

            if (!Response.IsError)
            {
                var Result = Response.Data;

                if (Result.Status && Result.Data != null)
                {
                    var ListData = Result.Data.ToObject<ObservableCollection<MasterPelangganAirDto>>();

                    _viewModel.PelangganList = ListData;
                }
            }

            _viewModel.IsEmptyForm1 = _viewModel.PelangganList.Count == 0;
            _viewModel.IsLoadingForm = false;
            await Task.FromResult(_isTest);
        }

    }
}
