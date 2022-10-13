using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.ViewModels.Personalia.Kepegawaian;
using SiPintar.Views.Personalia.Kepegawaian.PerjalananDinas;

namespace SiPintar.Commands.Personalia.Kepegawaian.PerjalananDinas
{
    public class OnOpenEditPesertaFormCommand : AsyncCommandBase
    {
        private readonly PerjalananDinasViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenEditPesertaFormCommand(PerjalananDinasViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..
            if (_viewModel.SelectedDataPeserta != null)
            {
                _viewModel.IsLoading = true;
                _viewModel.IsEditPeserta = true;

                _viewModel.FormPesertaData = new SppdPesertaWpf
                {
                    NoPegawai = _viewModel.SelectedDataPeserta.NoPegawai,
                    NamaPegawai = _viewModel.SelectedDataPeserta.NamaPegawai,
                    Jabatan = _viewModel.SelectedDataPeserta.Jabatan,
                    KodeGolonganPegawai = _viewModel.SelectedDataPeserta.KodeGolonganPegawai,

                    IdPegawai = _viewModel.SelectedDataPeserta.IdPegawai,
                    Biaya = _viewModel.SelectedDataPeserta.Biaya,
                };
                _viewModel.FormPesertaBiayaList = new ObservableCollection<SppdPesertaBiayaDto>(_viewModel.FormPesertaData.Biaya);

                _viewModel.IsLoading = false;

                await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "PersonaliaInnerDialog", () => new DialogPesertaFormView(_viewModel));
            }
            else
            {
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false,
                    "PersonaliaInnerDialog",
                    "Koreksi Peserta",
                    "Silahkan pilih data",
                    "warning",
                    "Ok",
                    false,
                    "Personalia");
            }
            await Task.FromResult(_isTest);
        }
    }
}
