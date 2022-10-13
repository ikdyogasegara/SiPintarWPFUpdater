using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;
using SiPintar.Views.Gudang.ProsesTransaksi.PengajuanPengeluaranBarang;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.TransaksiBarangKeluar
{
    public class OnOpenKoreksiDataDetailCommand : AsyncCommandBase
    {
        private readonly TransaksiBarangKeluarViewModel _vm;
        private readonly bool _isTest;

        public OnOpenKoreksiDataDetailCommand(TransaksiBarangKeluarViewModel vm, bool isTest = false)
        {
            _vm = vm;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_vm.SelectedDataDetail == null)
            {
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, true, "GudangRootDialog", "Koreksi Qty",
                    "Data belum dipilih!",
                    "warning", moduleName: "gudang");
            }
            else
            {
                await DialogHelper.ShowCustomDialogViewAsync(_isTest, true,
                    "GudangRootDialog", GetInstance);
            }
            _ = await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private object GetInstance() => new DialogKoreksiDataDetailView(_vm);
    }
}
