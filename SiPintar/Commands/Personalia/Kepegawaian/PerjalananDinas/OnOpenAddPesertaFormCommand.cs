using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.ViewModels.Personalia.Kepegawaian;
using SiPintar.Views.Personalia.Kepegawaian.PerjalananDinas;

namespace SiPintar.Commands.Personalia.Kepegawaian.PerjalananDinas
{
    public class OnOpenAddPesertaFormCommand : AsyncCommandBase
    {
        private readonly PerjalananDinasViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddPesertaFormCommand(PerjalananDinasViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..
            if (_viewModel.SelectedDataPegawai == null)
                return;

            _viewModel.IsLoading = true;
            _viewModel.IsEditPeserta = false;
            DialogHelper.CloseDialog(_isTest, false, "PersonaliaInnerDialog");

            _viewModel.FormPesertaData = new SppdPesertaWpf
            {
                NoPegawai = _viewModel.SelectedDataPegawai.NoPegawai,
                NamaPegawai = _viewModel.SelectedDataPegawai.NamaPegawai,
                Jabatan = _viewModel.SelectedDataPegawai.Jabatan,
                KodeGolonganPegawai = _viewModel.SelectedDataPegawai.KodeGolonganPegawai,

                IdPegawai = _viewModel.SelectedDataPegawai.IdPegawai,
                Biaya = new List<SppdPesertaBiayaDto>()
            };
            _viewModel.FormPesertaBiayaList = new ObservableCollection<SppdPesertaBiayaDto>(_viewModel.FormPesertaData.Biaya);

            _viewModel.IsLoading = false;

            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "PersonaliaInnerDialog", () => new DialogPesertaFormView(_viewModel));

            await Task.FromResult(_isTest);
        }
    }
}
