using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Personalia.Kepegawaian;
using SiPintar.Views.Personalia.Kepegawaian.PerjalananDinas;

namespace SiPintar.Commands.Personalia.Kepegawaian.PerjalananDinas
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly PerjalananDinasViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(PerjalananDinasViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..
            _viewModel.IsEdit = false;

            _viewModel.FormSpt1 = null;
            _viewModel.FormSpt2 = null;
            _viewModel.FormSpt3 = null;
            _viewModel.FormSpt4 = NumberToRomanHelper.getRomanNumber(DateTime.Today.Month, true);
            _viewModel.FormSpt5 = null;
            _viewModel.FormSppd1 = null;
            _viewModel.FormSppd2 = null;
            _viewModel.FormSppd3 = null;
            _viewModel.FormSppd4 = NumberToRomanHelper.getRomanNumber(DateTime.Today.Month, true);
            _viewModel.FormSppd5 = null;
            _viewModel.FormDasar = null;
            _viewModel.FormKeperluan = null;
            _viewModel.FormTempatBerangkat = null;
            _viewModel.FormTempatTujuan = null;

            _viewModel.FormTglBerangkat = DateTime.Today;
            _viewModel.FormTglKembali = DateTime.Today;
            _viewModel.FormLamaDinas = 0;
            _viewModel.FormTransportasi = null;
            _viewModel.FormBebanAnggaran = null;
            _viewModel.FormKeterangan = null;
            _viewModel.FormPesertaList = new ObservableCollection<SppdPesertaWpf>();
            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "PersonaliaRootDialog", () => new DialogFormView(_viewModel));

            await Task.FromResult(_isTest);
        }
    }
}
