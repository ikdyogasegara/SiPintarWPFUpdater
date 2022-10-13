using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.Tunjangan;

namespace SiPintar.Commands.Personalia.Tunjangan.PengecualianTunjangan
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly PengecualianTunjanganViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(PengecualianTunjanganViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..
            _viewModel.IsLoading = true;

            var param = new Dictionary<string, dynamic>();

            if (_viewModel.IsNikChecked)
                param.Add("NoPegawai", _viewModel.FilterNik);
            if (_viewModel.IsNamaChecked)
                param.Add("NamaPegawai", _viewModel.FilterNama);

            await GetJenisTunjanganAsync();

            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/pengecualian-tunjangan", param));
            _viewModel.PengecualianTunjanganList = new ObservableCollection<PengecualianTunjanganWpf>();
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status && Result.Data != null)
                {
                    _viewModel.PengecualianTunjanganList = Result.Data.ToObject<ObservableCollection<PengecualianTunjanganWpf>>();
                    CheckJenisTunjangan();
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", Response.Data.Ui_msg, "personalia");
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error.Message, "personalia");
            _viewModel.IsEmpty = !_viewModel.PengecualianTunjanganList.Any();

            _viewModel.IsLoading = false;
            await Task.FromResult(_isTest);
        }

        public async Task GetJenisTunjanganAsync()
        {
            _viewModel.IsLoading = true;
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-jenis-tunjangan");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.JenisTunjanganList = Result.Data.ToObject<ObservableCollection<PengecualianTunjanganDetailDto>>();
                }
            }
            _viewModel.IsLoading = false;
        }

        public void CheckJenisTunjangan()
        {
            if (!_viewModel.PengecualianTunjanganList.Any())
                return;

            _viewModel.PengecualianTunjanganList.ForEach(p => p.DetailNamaTunjangan = !_viewModel.JenisTunjanganList.Except(p.Detail).Any() ? "SEMUA" : string.Join(", ", p.Detail.Select(d => $"\"{d.NamaJenisTunjangan}\"")));
        }
    }
}
