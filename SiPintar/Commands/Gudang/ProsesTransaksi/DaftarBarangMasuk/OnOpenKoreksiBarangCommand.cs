using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;
using SiPintar.Views.Gudang.ProsesTransaksi.DaftarBarangMasuk;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.DaftarBarangMasuk
{
    public class OnOpenKoreksiBarangCommand : AsyncCommandBase
    {
        private readonly DaftarBarangMasukViewModel _vm;
        private readonly bool _isTest;

        public OnOpenKoreksiBarangCommand(DaftarBarangMasukViewModel vm, bool isTest = false)
        {
            _vm = vm;
            _isTest = isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            if (_vm.SelectedDaftarBarangMasuk != null && _vm.SelectedDaftarBarangMasukDetail != null)
            {
                var loading = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, true, "GudangRootDialog", "Mohon Tunggu", "Sedang memuat data ...");
                await UpdateListData.ProcessAsync(_isTest, new List<string>
                {
                    "MasterKategoriBarangMasuk"
                });
                AppDispatcherHelper.Run(_isTest, () => _vm.KategoriBarangMasuk = MasterListGlobal.MasterKategoriBarangMasuk);
                DialogHelper.CloseDialog(_isTest, true, "GudangRootDialog", loading);

                _ = DialogHelper.ShowCustomDialogViewAsync(_isTest, true, "GudangRootDialog", GetDialog);
            }
            else
            {
                _ = DialogHelper.ShowDialogGlobalViewAsync(_isTest, true, "GudangRootDialog", "Informasi", "Data belum dipilih!", "warning", moduleName: "gudang");
            }
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private object GetDialog() => new DialogKoreksiTambahBarang(_vm, isEdit: true);
    }
}
