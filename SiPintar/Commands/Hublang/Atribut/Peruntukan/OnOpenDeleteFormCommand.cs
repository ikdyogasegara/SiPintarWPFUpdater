using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Hublang.Atribut;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Hublang.Atribut.Peruntukan
{
    public class OnOpenDeleteFormCommand : AsyncCommandBase
    {
        private readonly PeruntukanViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDeleteFormCommand(PeruntukanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        [ExcludeFromCodeCoverage]
        public override async Task ExecuteAsync(object parameter)
        {
            if (!_isTest)
            {
                if (_viewModel.SelectedData != null)
                    await DialogHost.Show(new DialogGlobalYesCancelView("Hapus Data Peruntukan?",
                        "Data Peruntukan yang akan dihapus tidak dapat dibatalkan.",
                        "warning", _viewModel.OnSubmitDeleteFormCommand, "Hapus", "Batal", false, false, "hublang"), "HublangRootDialog");
                else
                    await DialogHost.Show(new DialogGlobalView("Hapus Data Peruntukan", "Silahkan pilih data", "warning", "Ok", false, "hublang"), "HublangRootDialog");

            }
            await Task.FromResult(_isTest);
        }
    }
}
