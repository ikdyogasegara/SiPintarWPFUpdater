using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.SupervisiPengajuanPembelian
{
    public class OnOpenDeleteFormCommand : AsyncCommandBase
    {
        private readonly SupervisiPengajuanPembelianViewModel Vm;
        public readonly bool IsTest;

        public OnOpenDeleteFormCommand(SupervisiPengajuanPembelianViewModel _vm, bool _isTest = false)
        {
            Vm = _vm;
            IsTest = _isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            bool? CaseDelete = parameter as bool?;
            if (CaseDelete.HasValue)
            {
                Vm.IsDeletePengajuan = CaseDelete.Value;

                if (Vm.IsDeletePengajuan && Vm.SelectedData == null)
                {
                    await DialogHelper.ShowDialogGlobalViewAsync(IsTest, true, "GudangRootDialog",
                    $"Hapus {(Vm.IsDeletePengajuan ? "Pengajuan" : "Barang")}",
                    $"Data {(Vm.IsDeletePengajuan ? "Pengajuan" : "Barang")} belum dipilih!",
                    "warning", "Oke", false, "gudang");
                }
                else if (!Vm.IsDeletePengajuan && Vm.SelectedDataDetail == null)
                {
                    await DialogHelper.ShowDialogGlobalViewAsync(IsTest, true, "GudangRootDialog",
                    $"Hapus {(Vm.IsDeletePengajuan ? "Pengajuan" : "Barang")}",
                    $"Data {(Vm.IsDeletePengajuan ? "Pengajuan" : "Barang")} belum dipilih!",
                    "warning", "Oke", false, "gudang");
                }
                else
                {
                    await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                        IsTest,
                        true,
                        "GudangRootDialog",
                        $"Hapus {(Vm.IsDeletePengajuan ? "Pengajuan" : "Barang")}",
                        $"Apakah anda yakin ingin menghapus {(Vm.IsDeletePengajuan ? "Pengajuan" : "Barang")} ?",
                        "confirmation",
                        Vm.OnSubmitDeleteFormCommand,
                        "Oke",
                        "Batal",
                        false, false, "gudang"
                    );
                }
            }
            await Task.FromResult(IsTest);
        }
    }
}
