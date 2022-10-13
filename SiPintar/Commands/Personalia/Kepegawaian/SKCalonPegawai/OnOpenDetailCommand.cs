using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.ViewModels.Personalia.Kepegawaian;
using SiPintar.Views.Personalia.Kepegawaian.SKCalonPegawai;

namespace SiPintar.Commands.Personalia.Kepegawaian.SKCalonPegawai
{
    public class OnOpenDetailCommand : AsyncCommandBase
    {
        private readonly SKCalonPegawaiViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDetailCommand(SKCalonPegawaiViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..
            if (_viewModel.SelectedData != null)
            {
                _viewModel.FormSk = _viewModel.SelectedData.NoSk;
                _viewModel.FormTglSk = _viewModel.SelectedData.TglSk;
                _viewModel.FormTglBerlaku = _viewModel.SelectedData.TglBerlaku;
                _viewModel.FormKeterangan = _viewModel.SelectedData.Keterangan;
                _viewModel.FormDetailList = new ObservableCollection<MutasiStatusCapegDetailDto>(_viewModel.SelectedData.Detail);
                await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "PersonaliaRootDialog", () => new DialogDetailView(_viewModel));
            }
            else
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false,
                    "PersonaliaRootDialog",
                    "Detail SK Calon Pegawai",
                    "Silahkan pilih data",
                    "warning",
                    "Ok",
                    false,
                    "Personalia");

            await Task.FromResult(_isTest);
        }
    }
}
