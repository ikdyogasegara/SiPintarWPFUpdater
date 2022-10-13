using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.MasterData;
using SiPintar.Views.Gudang.MasterData.KategoriBarangMasuk;

namespace SiPintar.Commands.Gudang.MasterData.KategoriBarangMasuk
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly KategoriBarangMasukViewModel _vm;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(KategoriBarangMasukViewModel vm, bool isTest = false)
        {
            _vm = vm;
            _isTest = isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            _vm.IsAdd = false;
            await DialogHelper.ShowCustomDialogViewAsync(_isTest, true, "GudangRootDialog", GetInstance);
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private object GetInstance() => new DialogFormView(_vm);
    }
}
