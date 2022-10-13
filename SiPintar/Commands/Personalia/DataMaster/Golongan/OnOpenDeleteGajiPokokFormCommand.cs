using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Personalia.DataMaster;

namespace SiPintar.Commands.Personalia.DataMaster.Golongan
{
    public class OnOpenDeleteGajiPokokFormCommand : AsyncCommandBase
    {
        private readonly GolonganViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDeleteGajiPokokFormCommand(GolonganViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedDataGajiPokok != null)
                await DialogHelper.ShowDialogGlobalYesCancelViewAsync(_isTest, false, "PersonaliaRootDialog", $"Hapus Master Gaji Pokok",
                    $"Yakin Menghapus Gaji Pokok Golongan \"{_viewModel.SelectedData.KodeGolonganPegawai}\" Masa Kerja \"{_viewModel.SelectedDataGajiPokok.MasaKerja}\" ini ?",
                    "warning", _viewModel.OnSubmitDeleteGajiPokokFormCommand, "Hapus", "Batal", false, false, "Personalia");
            else
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false,
                    "PersonaliaRootDialog",
                    "Hapus Data Gaji Pokok",
                    "Silahkan pilih data",
                    "warning",
                    "Ok",
                    false,
                    "Personalia");
            await Task.FromResult(_isTest);
        }
    }
}
