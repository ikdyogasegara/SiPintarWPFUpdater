using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.Kepegawaian;
using SiPintar.Views.Personalia.Kepegawaian.PerjalananDinas;

namespace SiPintar.Commands.Personalia.Kepegawaian.PerjalananDinas
{
    public class OnOpenEditPesertaBiayaFormCommand : AsyncCommandBase
    {
        private readonly PerjalananDinasViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnOpenEditPesertaBiayaFormCommand(PerjalananDinasViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
            _restApi = restApi;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..
            if (_viewModel.SelectedDataPesertaBiaya != null)
            {
                _viewModel.IsLoading = true;
                _viewModel.IsEditPesertaBiaya = true;

                var biayaSppd = _viewModel.SelectedDataPesertaBiaya.IdBiayaSppd;
                await GetSppdBiayaAsync();

                _viewModel.FormBiayaSppd = biayaSppd;
                _viewModel.FormDeskripsi = _viewModel.SelectedDataPesertaBiaya.Deskripsi;
                _viewModel.FormBiaya = _viewModel.SelectedDataPesertaBiaya.Biaya;
                _viewModel.FormQty = _viewModel.SelectedDataPesertaBiaya.Qty;
                _viewModel.FormJumlah = _viewModel.SelectedDataPesertaBiaya.Jumlah;
                _viewModel.FormKeteranganBiaya = _viewModel.SelectedDataPesertaBiaya.Keterangan;

                _viewModel.IsLoading = false;

                await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "PersonaliaInnerInnerDialog", () => new DialogPesertaBiayaFormView(_viewModel));
            }
            else
            {
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false,
                    "PersonaliaInnerInnerDialog",
                    "Koreksi Biaya Peserta",
                    "Silahkan pilih data",
                    "warning",
                    "Ok",
                    false,
                    "Personalia");
            }
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
