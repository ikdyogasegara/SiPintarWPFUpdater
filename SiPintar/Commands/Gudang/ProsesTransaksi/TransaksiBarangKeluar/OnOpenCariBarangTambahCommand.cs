using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;
using SiPintar.Views.Gudang.ProsesTransaksi.PengajuanPengeluaranBarang;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.TransaksiBarangKeluar
{
    public class OnOpenCariBarangTambahCommand : AsyncCommandBase
    {
        private readonly TransaksiBarangKeluarViewModel _vm;
        private readonly bool _isTest;

        public OnOpenCariBarangTambahCommand(TransaksiBarangKeluarViewModel vm, bool isTest = false)
        {
            _vm = vm;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            await DialogHelper.ShowCustomDialogViewAsync(_isTest, true,
                "TambahBarangDialog", GetInstance);
            _ = await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private object GetInstance() => new DialogPilihBarangView(_vm, (data) =>
        {
            _vm.CariBarangForm = data;
            return true;
        });
    }
}
