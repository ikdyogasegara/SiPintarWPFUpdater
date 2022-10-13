using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.Kepegawaian;
using SiPintar.Views.Personalia.Kepegawaian.PerjalananDinas;

namespace SiPintar.Commands.Personalia.Kepegawaian.PerjalananDinas
{
    public class OnOpenAddPesertaBiayaFormCommand : AsyncCommandBase
    {
        private readonly PerjalananDinasViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnOpenAddPesertaBiayaFormCommand(PerjalananDinasViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
            _restApi = restApi;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..
            if (_viewModel.SelectedDataPegawai == null)
                return;

            _viewModel.IsLoading = true;
            _viewModel.IsEditPesertaBiaya = false;
            await GetSppdBiayaAsync();

            _viewModel.FormBiayaSppd = null;
            _viewModel.FormDeskripsi = null;
            _viewModel.FormBiaya = 0;
            _viewModel.FormQty = 0;
            _viewModel.FormJumlah = 0;
            _viewModel.FormKeteranganBiaya = null;

            _viewModel.IsLoading = false;

            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "PersonaliaInnerInnerDialog", () => new DialogPesertaBiayaFormView(_viewModel));

            await Task.FromResult(_isTest);
        }

        public async Task GetSppdBiayaAsync()
        {
            _viewModel.IsLoading = true;
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-sppd-biaya");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.SppdBiayaFormList = Result.Data.ToObject<ObservableCollection<MasterSppdBiayaDto>>();
                }
            }
            _viewModel.IsLoading = false;
        }
    }
}
