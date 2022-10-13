using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Hublang.Atribut;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Hublang.Atribut.TarifAirTangki
{
    public class OnOpenDeleteFormCommand : AsyncCommandBase
    {
        private readonly TarifAirTangkiViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDeleteFormCommand(TarifAirTangkiViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        [ExcludeFromCodeCoverage]
        public override async Task ExecuteAsync(object parameter)
        {
            _ = _viewModel;
            if (!_isTest)
            {
                if (_viewModel.SelectedData != null)
                    await DialogHost.Show(new DialogGlobalYesCancelView("Hapus Data Tarif Air Tangki ?",
                        "Data tarif air tangki yang akan dihapus tidak dapat dibatalkan.",
                        "warning", _viewModel.OnSubmitDeleteFormCommand, "Hapus", "Batal", false, false, "hublang"), "HublangRootDialog");
                else
                    await DialogHost.Show(new DialogGlobalView("Hapus Tarif Tangki Air", "Silahkan pilih data", "warning", "Ok", false, "hublang"), "HublangRootDialog");

            }
            await Task.FromResult(_isTest);
        }
    }
}
