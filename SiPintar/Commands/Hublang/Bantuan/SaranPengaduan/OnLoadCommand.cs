using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Bantuan;

namespace SiPintar.Commands.Hublang.Bantuan.SaranPengaduan
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly SaranPengaduanViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = App.Current.Resources["api_version"]?.ToString();

        public OnLoadCommand(SaranPengaduanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            UpdateState();
            _ = GetMasterSaranPertanyaanAsync();


            await Task.FromResult(_isTest);
        }

        public void UpdateState()
        {
            _viewModel.NamaPetugas = App.Current.Resources["FullName"]?.ToString();
            _viewModel.NamaPDAM = App.Current.Resources["NamaPdam"]?.ToString();
        }
        public async Task GetMasterSaranPertanyaanAsync()
        {
            _viewModel.IsLoading = false;

            var ResponseSaranPertanyaan = await _restApi.GetAsync($"/api/{ApiVersion}/master-saran-pertanyaan");

            if (!ResponseSaranPertanyaan.IsError)
            {
                var Result = ResponseSaranPertanyaan.Data;

                if (Result.Status && Result.Data != null)
                    _viewModel.MasterSaranPertanyaan = Result.Data.ToObject<ObservableCollection<MasterSaranPertanyaanDto>>();

                if (ResponseSaranPertanyaan.HasData)
                {
                    int Total = _viewModel.MasterSaranPertanyaan.Count;
                    double Divide = Convert.ToDouble(Total) / 2;
                    var Rest = Convert.ToInt32(Math.Ceiling(Divide));

                    int Bag1 = Rest;
                    int Bag2 = Total - Rest;

                    _viewModel.SaranPertanyaanBagian1 = new List<dynamic>(_viewModel.MasterSaranPertanyaan.Take(Bag1));
                    _viewModel.SaranPertanyaanBagian2 = new List<dynamic>(_viewModel.MasterSaranPertanyaan.Skip(Bag1).Take(Bag2));

                }
            }
        }
    }
}
