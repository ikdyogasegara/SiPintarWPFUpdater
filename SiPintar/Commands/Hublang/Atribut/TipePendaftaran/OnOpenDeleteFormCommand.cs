using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Hublang.Atribut;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Hublang.Atribut.TipePendaftaran
{
    public class OnOpenDeleteFormCommand : AsyncCommandBase
    {
        private readonly TipePendaftaranViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDeleteFormCommand(TipePendaftaranViewModel viewModel, bool isTest = false)
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
                    await DialogHost.Show(new DialogGlobalYesCancelView("Hapus Data Tipe Pendaftaran?",
                        "Data Tipe Pendaftaran yang akan dihapus tidak dapat dibatalkan.",
                        "warning", _viewModel.OnSubmitDeleteFormCommand, "Hapus", "Batal", false, false, "hublang"), "HublangRootDialog");
                else
                    await DialogHost.Show(new DialogGlobalView("Hapus Data Tipe Pendaftaran", "Silahkan pilih data", "warning", "Ok", false, "hublang"), "HublangRootDialog");

            }
            await Task.FromResult(_isTest);
        }
    }
}
