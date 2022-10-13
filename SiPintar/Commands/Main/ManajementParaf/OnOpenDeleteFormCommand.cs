using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Main;

namespace SiPintar.Commands.Main.ManajementParaf
{
    public class OnOpenDeleteFormCommand : AsyncCommandBase
    {
        private readonly ManajementParafViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDeleteFormCommand(ManajementParafViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedData != null)
                _ = DialogHelper.ShowDialogGlobalYesCancelViewAsync(_isTest, false,
                    "MainRootDialog",
                    $"Hapus Data Paraf {_viewModel.SelectedData.IdParaf}?",
                    $"Data Paraf {_viewModel.SelectedData.IdParaf} yang akan dihapus tidak dapat dibatalkan.",
                    "warning", _viewModel.OnSubmitDeleteFormCommand, "Hapus", "Batal", false, false, "main");
            else
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false,
                    "MainRootDialog",
                    "Hapus Data Paraf",
                    "Silahkan pilih data",
                    "warning",
                    "Ok",
                    false,
                    "main");

            await Task.FromResult(_isTest);
        }
    }
}
