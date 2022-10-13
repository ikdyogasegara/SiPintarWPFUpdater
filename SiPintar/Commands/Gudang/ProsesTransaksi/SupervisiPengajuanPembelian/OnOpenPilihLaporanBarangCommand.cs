using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;
using SiPintar.Views.Gudang.ProsesTransaksi.SupervisiPengajuanPembelian;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.SupervisiPengajuanPembelian
{
    public class OnOpenPilihLaporanBarangCommand : AsyncCommandBase
    {
        private readonly SupervisiPengajuanPembelianViewModel Vm;
        public readonly bool IsTest;

        public OnOpenPilihLaporanBarangCommand(SupervisiPengajuanPembelianViewModel _vm, bool _isTest = false)
        {
            Vm = _vm;
            IsTest = _isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (Vm.SelectedData != null)
            {
                await DialogHelper.ShowCustomDialogViewAsync(
                    IsTest,
                    true,
                    "GudangRootDialog",
                    GetInstance
                );
            }
            else
            {
                await DialogHelper.ShowDialogGlobalViewAsync(IsTest, true, "GudangRootDialog",
                    "Buat Laporan",
                    "Data Pengajuan belum dipilih!",
                    "warning",
                    "Oke",
                    false,
                    "gudang");
            }
            await Task.FromResult(IsTest);
        }

        private object GetInstance() => new DialogPilihLaporanView(Vm);
    }
}
