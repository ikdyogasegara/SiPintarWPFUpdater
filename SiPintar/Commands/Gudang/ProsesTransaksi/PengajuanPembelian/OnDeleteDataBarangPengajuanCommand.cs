using System.Threading.Tasks;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.PengajuanPembelian
{
    public class OnDeleteDataBarangPengajuanCommand : AsyncCommandBase
    {
        private readonly PengajuanPembelianViewModel _vm;
        private readonly bool _isTest;

        public OnDeleteDataBarangPengajuanCommand(PengajuanPembelianViewModel vm, bool isTest = false)
        {
            _vm = vm;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_vm.SelectedBarangPengajuan != null && _vm.SelectedBarangPengajuan != null)
            {
                var remove = _vm.DaftarBarangPengajuan?.Remove(_vm.SelectedBarangPengajuan) ?? false;
                if (remove)
                {
                    _vm.DaftarBarangPengajuan = new(_vm.DaftarBarangPengajuan ?? new());
                }
            }
            await Task.FromResult(_isTest);
        }
    }
}
