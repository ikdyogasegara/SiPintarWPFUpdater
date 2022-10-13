using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.ViewModels.Personalia.Kepegawaian;
using SiPintar.Views.Personalia.Kepegawaian.DiklatPelatihan;

namespace SiPintar.Commands.Personalia.Kepegawaian.DiklatPelatihan
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly DiklatPelatihanViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(DiklatPelatihanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..
            _viewModel.IsEdit = false;

            _viewModel.FormNoSertifikat = null;
            _viewModel.FormTglAwal = DateTime.Today;
            _viewModel.FormTglAkhir = DateTime.Today;
            _viewModel.FormUraian = null;
            _viewModel.FormTempat = null;
            _viewModel.FormPenyelenggara = null;
            _viewModel.FormDetailList = new ObservableCollection<DiklatDetailDto>();
            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "PersonaliaRootDialog", () => new DialogFormView(_viewModel));

            await Task.FromResult(_isTest);
        }
    }
}
