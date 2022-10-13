using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;
using SiPintar.Views.Gudang.ProsesTransaksi.SupervisiPengajuanPembelian;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.SupervisiPengajuanPembelian
{
    public class OnOpenTambahBarangSatuanCommand : AsyncCommandBase
    {
        private readonly SupervisiPengajuanPembelianViewModel Vm;
        public readonly bool IsTest;

        public OnOpenTambahBarangSatuanCommand(SupervisiPengajuanPembelianViewModel _vm, bool _isTest = false)
        {
            Vm = _vm;
            IsTest = _isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (Vm.SelectedData is null)
            {
                await DialogHelper.ShowDialogGlobalViewAsync(IsTest, true, "GudangRootDialog", "Tambah Barang Satuan", "Data Pengajuan Belum Dipilih!",
                    "warning", "Oke", false, "gudang");
            }
            else
            {
                await DialogHelper.ShowCustomDialogViewAsync(
                IsTest,
                true,
                "GudangRootDialog",
                GetInstance
            );
            }
            await Task.FromResult(IsTest);
        }

        private object GetInstance() => new DialogTambahBarangView(Vm);
    }
}
