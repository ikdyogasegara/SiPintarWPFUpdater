using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;
using SiPintar.Views.Gudang.ProsesTransaksi.PengajuanPengeluaranBarang;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.TransaksiBarangKeluar
{
    [ExcludeFromCodeCoverage]
    public class OnOpenCariBarangCommand : AsyncCommandBase
    {
        private readonly TransaksiBarangKeluarViewModel _vm;
        private readonly bool _isTest;

        public OnOpenCariBarangCommand(TransaksiBarangKeluarViewModel vm, bool isTest = false)
        {
            _vm = vm;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            await DialogHelper.ShowCustomDialogViewAsync(_isTest, true,
                "PengajuanPengeluaranDialog", GetInstance);
            _ = await Task.FromResult(_isTest);
        }

        private object GetInstance() => new DialogPilihBarangView(_vm, (data) =>
        {
            _vm.CariBarangForm = data;
            return true;
        });
    }
}
