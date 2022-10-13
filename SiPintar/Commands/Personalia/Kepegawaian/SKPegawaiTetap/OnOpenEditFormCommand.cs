using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.Kepegawaian;
using SiPintar.Views.Personalia.Kepegawaian.SKPegawaiTetap;

namespace SiPintar.Commands.Personalia.Kepegawaian.SKPegawaiTetap
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly SKPegawaiTetapViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(SKPegawaiTetapViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..
            _viewModel.IsEdit = true;

            if (_viewModel.SelectedData != null)
            {
                if (_viewModel.SelectedData.Verifikasi == true)
                    await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false,
                        "PersonaliaRootDialog",
                        "Koreksi tidak diizinkan",
                        "Data sudah diverifikasi, Jika ingin melakukan koreksi harap hubungi admin",
                        "error",
                        "Batal",
                        false,
                        "Personalia");
                else
                {
                    _viewModel.FormSk = _viewModel.SelectedData.NoSk;
                    _viewModel.FormTglSk = _viewModel.SelectedData.TglSk;
                    _viewModel.FormTglBerlaku = _viewModel.SelectedData.TglBerlaku;
                    _viewModel.FormKeterangan = _viewModel.SelectedData.Keterangan;
                    _viewModel.FormDetailList = new ObservableCollection<MutasiStatusTetapDetailDto>(_viewModel.SelectedData.Detail);
                    await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "PersonaliaRootDialog", () => new DialogFormView(_viewModel));
                }
            }
            else
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false,
                    "PersonaliaRootDialog",
                    "Koreksi SK Pegawai Tetap",
                    "Silahkan pilih data",
                    "warning",
                    "Ok",
                    false,
                    "Personalia");

            await Task.FromResult(_isTest);
        }
    }
}
