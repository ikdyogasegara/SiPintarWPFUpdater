using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Perencanaan.Atribut;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Perencanaan.Atribut.Material
{
    public class OnOpenDeleteFormCommand : AsyncCommandBase
    {
        private readonly MaterialViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDeleteFormCommand(MaterialViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.SelectedData == null)
                return;

            await OpenDialogAsync();
        }

        [ExcludeFromCodeCoverage]
        public async Task OpenDialogAsync()
        {
            if (!_isTest)
            {
                await DialogHost.Show(new DialogGlobalYesCancelView(
                    "Hapus Data Material?",
                    "Data material yang akan dihapus tidak dapat dibatalkan.",
                    "warning",
                    _viewModel.OnSubmitDeleteCommand,
                    "Hapus",
                    "Batal",
                    false,
                    false,
                    "perencanaan"
                ), "PerencanaanRootDialog");
            }
        }
    }
}
