using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.Tunjangan;

namespace SiPintar.Commands.Personalia.Tunjangan.MasterTunjangan
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly MasterTunjanganViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(MasterTunjanganViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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

            if (_viewModel.IsKodeJenisTunjanganChecked)
                param.Add("KodeJenisTunjangan", _viewModel.FilterKodeJenisTunjangan);
            if (_viewModel.IsNamaJenisTunjanganChecked)
                param.Add("NamaJenisTunjangan", _viewModel.FilterNamaJenisTunjangan);
            if (_viewModel.IsTipeChecked)
                param.Add("Tipe", _viewModel.FilterTipe);

            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-jenis-tunjangan", param));
            _viewModel.MasterTunjanganList = new ObservableCollection<MasterJenisTunjanganDto>();
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status && Result.Data != null)
                {
                    _viewModel.MasterTunjanganList = Result.Data.ToObject<ObservableCollection<MasterJenisTunjanganDto>>();
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", Response.Data.Ui_msg, "personalia");
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error.Message, "personalia");
            _viewModel.IsEmpty = !_viewModel.MasterTunjanganList.Any();

            _viewModel.IsLoading = false;
            await Task.FromResult(_isTest);
        }

    }
}
