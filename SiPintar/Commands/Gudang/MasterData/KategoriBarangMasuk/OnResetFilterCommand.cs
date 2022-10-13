using System.Threading.Tasks;
using SiPintar.ViewModels.Gudang.MasterData;

namespace SiPintar.Commands.Gudang.MasterData.KategoriBarangMasuk
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly KategoriBarangMasukViewModel _vm;
        private readonly bool _isTest;

        public OnResetFilterCommand(KategoriBarangMasukViewModel vm, bool isTest = false)
        {
            _vm = vm;
            _isTest = isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            _vm.IsNamaKategoriBarangMasukChecked = false;
            _vm.FilterNamaKategoriBarangMasuk = null!;
            await Task.FromResult(_isTest);
        }
    }
}
