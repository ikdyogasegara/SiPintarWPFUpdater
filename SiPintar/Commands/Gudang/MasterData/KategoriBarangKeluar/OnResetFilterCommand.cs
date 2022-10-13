using System.Threading.Tasks;
using SiPintar.ViewModels.Gudang.MasterData;

namespace SiPintar.Commands.Gudang.MasterData.KategoriBarangKeluar
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly KategoriBarangKeluarViewModel Vm;
        public readonly bool IsTest;

        public OnResetFilterCommand(KategoriBarangKeluarViewModel _vm, bool _isTest = false)
        {
            Vm = _vm;
            IsTest = _isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            Vm.IsNamaKategoriBarangKeluarChecked = false;
            Vm.FilterNamaKategoriBarangKeluar = null;
            _ = Vm;
            await Task.FromResult(IsTest);
        }
    }
}
