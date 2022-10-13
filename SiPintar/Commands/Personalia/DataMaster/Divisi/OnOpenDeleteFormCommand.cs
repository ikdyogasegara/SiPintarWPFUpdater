using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Personalia.DataMaster;

namespace SiPintar.Commands.Personalia.DataMaster.Divisi
{
    public class OnOpenDeleteFormCommand : AsyncCommandBase
    {
        private readonly DivisiViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDeleteFormCommand(DivisiViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedData != null)
                await DialogHelper.ShowDialogGlobalYesCancelViewAsync(_isTest, false, "PersonaliaRootDialog", $"Hapus Master Divisi",
                    $"Yakin Menghapus Divisi \"{_viewModel.SelectedData.Divisi}\" ini ?",
                    "warning", _viewModel.OnSubmitDeleteFormCommand, "Hapus", "Batal", false, false, "Personalia");
            else
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false,
                    "PersonaliaRootDialog",
                    "Hapus Data Divisi",
                    "Silahkan pilih data",
                    "warning",
                    "Ok",
                    false,
                    "Personalia");
            await Task.FromResult(_isTest);
        }
    }
}
