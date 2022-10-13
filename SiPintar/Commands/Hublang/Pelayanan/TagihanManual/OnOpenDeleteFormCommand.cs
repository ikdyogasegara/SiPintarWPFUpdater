using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Hublang.Pelayanan;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Hublang.Pelayanan.TagihanManual
{
    public class OnOpenDeleteFormCommand : AsyncCommandBase
    {
        private readonly TagihanManualViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDeleteFormCommand(TagihanManualViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        [ExcludeFromCodeCoverage]
        public override async Task ExecuteAsync(object parameter)
        {
            if (!_isTest)
            {
                if (_viewModel.SelectedData == null)
                {
                    await DialogHost.Show(new DialogGlobalView("Hapus Data Tagihan Non Air", "Silahkan pilih data", "warning", "Ok", false, "hublang"), "HublangRootDialog");
                }
                else if (_viewModel.SelectedData.StatusTransaksiWpf == true)
                {
                    await DialogHost.Show(new DialogGlobalView("Hapus Data Tagihan Non Air", "Tagihan Lunas tidak dapat dihapus !", "warning", "Ok", false, "hublang"), "HublangRootDialog");
                }
                else
                {
                    await DialogHost.Show(new DialogGlobalYesCancelView("Hapus Data Tagihan Non Air?",
                        "Data tagihan non air yang akan dihapus tidak dapat dibatalkan.",
                        "warning", _viewModel.OnSubmitDeleteFormCommand, "Hapus", "Batal", false, false, "hublang"), "HublangRootDialog");
                }
            }
            await Task.FromResult(_isTest);
        }
    }
}
