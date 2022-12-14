using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.DataMaster;
using SiPintar.Views.Personalia.DataMaster.Divisi;

namespace SiPintar.Commands.Personalia.DataMaster.Divisi
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly DivisiViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(DivisiViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..
            await GetDivisiAsync();
            await GetDepartemenAsync();
            _viewModel.IsEdit = false;
            _viewModel.FormDivisi = null;
            _viewModel.FormDivisiAtas = null;
            _viewModel.FormDepartemen = null;
            _viewModel.FormUrutan = null;
            _viewModel.UrutanList = new ObservableCollection<int>(Enumerable.Range(1, 100).ToList());
            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "PersonaliaRootDialog", () => new DialogFormView(_viewModel));

            await Task.FromResult(_isTest);
        }

        public async Task GetDivisiAsync()
        {
            _viewModel.IsLoading = true;
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-divisi");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.DivisiAtasList = Result.Data.ToObject<ObservableCollection<MasterDivisiDto>>();
                }
            }
            _viewModel.IsLoading = false;
        }

        public async Task GetDepartemenAsync()
        {
            _viewModel.IsLoading = true;
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-departemen");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.DepartemenList = Result.Data.ToObject<ObservableCollection<MasterDepartemenDto>>();
                }
            }
            _viewModel.IsLoading = false;
        }
    }
}
