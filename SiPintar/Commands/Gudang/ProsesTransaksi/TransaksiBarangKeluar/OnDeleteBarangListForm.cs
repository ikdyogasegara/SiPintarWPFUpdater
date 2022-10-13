using System.Collections.ObjectModel;
using System.Threading.Tasks;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.TransaksiBarangKeluar
{
    public class OnDeleteBarangListForm : AsyncCommandBase
    {
        private readonly TransaksiBarangKeluarViewModel _vm;
        private readonly bool _isTest;

        public OnDeleteBarangListForm(TransaksiBarangKeluarViewModel vm, bool isTest = false)
        {
            _vm = vm;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_vm.SelectedBarangFormPengeluaran != null && _vm.Form?.DetailWpf != null)
            {
                var temp = new ObservableCollection<ParamPengajuanPengeluaranBarangDetailWpf>(_vm.Form.DetailWpf);
                temp.Remove(_vm.SelectedBarangFormPengeluaran);
                _vm.Form.DetailWpf = temp;
                _vm.SelectedBarangFormPengeluaran = null;
            }
            _ = await Task.FromResult(_isTest);
        }
    }
}
