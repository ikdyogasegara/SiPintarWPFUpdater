using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Loket.Angsuran;
using SiPintar.Views.Loket.Angsuran.DetailAngsuran;

namespace SiPintar.Commands.Loket.Angsuran.DetailAngsuran
{
    public class OnOpenUbahPenanggungBebanAngsuranCommand : AsyncCommandBase
    {
        private readonly DetailAngsuranViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnOpenUbahPenanggungBebanAngsuranCommand(DetailAngsuranViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.DataPelanggan.Clear();
            _viewModel.SearchName = "";
            _viewModel.SearchNosamb = "";
            _viewModel.NosambBefore = _viewModel.SelectedPelanggan.NoSamb;
            _viewModel.NamaBefore = _viewModel.SelectedPelanggan.Nama;
            _viewModel.Parent.NoHpForm = _viewModel.Parent._nonair.SelectedData?.NoHp;
            _viewModel.Parent.NoTelpForm = _viewModel.Parent._nonair.SelectedData?.NoTelp;
            _viewModel.PelangganSelected = new MasterPelangganAirDto();
            try
            {
                var param = new Dictionary<string, dynamic>
                {
                    { "PageSize" , 100 },
                    { "CurrentPage" , 1 },
                };


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
                };

            }
            catch (Exception) { throw; }
            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "LoketRootDialog", GetInstance);
            await Task.FromResult(_isTest);
        }

        private object GetInstance() => new UbahPenanggungBebanAngsuranView(_viewModel);
    }
}
