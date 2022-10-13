using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Personalia.DataMaster;

namespace SiPintar.Commands.Personalia.DataMaster.Pegawai
{
    public class OnOpenDeleteFormCommand : AsyncCommandBase
    {
        private readonly PegawaiViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDeleteFormCommand(PegawaiViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedData != null)
                await DialogHelper.ShowDialogGlobalYesCancelViewAsync(_isTest, false, "PersonaliaRootDialog", "Hapus Data Pegawai",
                    $"Yakin menghapus data pegawai \"{_viewModel.SelectedData.NamaPegawai}\" ini ?",
                    "warning", _viewModel.OnSubmitDeleteFormCommand, "Hapus", "Batal", false, false, "Personalia");
            else
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false,
                    "PersonaliaRootDialog",
                    "Hapus Data Pegawai",
                    "Silahkan pilih data",
                    "warning",
                    "Ok",
                    false,
                    "Personalia");
            await Task.FromResult(_isTest);
        }
    }
}
