using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.ViewModels.Personalia.Kepegawaian;
using SiPintar.Views.Personalia.Kepegawaian.SKPegawaiTetap;

namespace SiPintar.Commands.Personalia.Kepegawaian.SKPegawaiTetap
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly SKPegawaiTetapViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(SKPegawaiTetapViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..
            _viewModel.IsEdit = false;

            _viewModel.FormSk1 = null;
            _viewModel.FormSk2 = null;
            _viewModel.FormSk3 = null;
            _viewModel.FormSk4 = NumberToRomanHelper.getRomanNumber(DateTime.Today.Month, true);
            _viewModel.FormSk5 = DateTime.Today.Year.ToString();
            _viewModel.FormTglSk = null;
            _viewModel.FormTglBerlaku = null;
            _viewModel.FormKeterangan = null;
            _viewModel.FormDetailList = new ObservableCollection<MutasiStatusTetapDetailDto>();
            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "PersonaliaRootDialog", () => new DialogFormView(_viewModel));

            await Task.FromResult(_isTest);
        }
    }
}
