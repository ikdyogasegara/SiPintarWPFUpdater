using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.GajiPokok;

namespace SiPintar.Commands.Personalia.GajiPokok.DewanPengawas
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly DewanPengawasViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(DewanPengawasViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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

            if (_viewModel.IsNoPegawaiChecked)
                param.Add("NoPegawai", _viewModel.FilterNoPegawai);
            if (_viewModel.IsNamaPegawaiChecked)
                param.Add("NamaPegawai", _viewModel.FilterNamaPegawai);

            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-gaji-pegawai-dewan-pengawas", param));
            _viewModel.DewanPengawasList = new ObservableCollection<MasterPegawaiGajiDewanPengawasDto>();
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status && Result.Data != null)
                {
                    _viewModel.DewanPengawasList = Result.Data.ToObject<ObservableCollection<MasterPegawaiGajiDewanPengawasDto>>();
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", Response.Data.Ui_msg, "personalia");
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error.Message, "personalia");
            _viewModel.IsEmpty = !_viewModel.DewanPengawasList.Any();

            _viewModel.IsLoading = false;
            await Task.FromResult(_isTest);
        }
    }
}
