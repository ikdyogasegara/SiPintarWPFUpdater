using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using SiPintar.Helpers;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.PengajuanPembelian
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly PengajuanPembelianViewModel _vm;
        private readonly bool _isTest;

        public OnLoadCommand(PengajuanPembelianViewModel vm, bool isTest = false)
        {
            _vm = vm;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _vm.WilayahList = null;
            AppDispatcherHelper.Run(_isTest, () =>
            {
                _ = Task.Run(() => ((AsyncCommandBase)_vm.OnRefreshCommand).ExecuteAsync(null!));
            });
            await UpdateListData.ProcessAsync(_isTest, new List<string>
            {
                "MasterWilayah",
            });
            _vm.WilayahList = JArray.FromObject(MasterListGlobal.MasterWilayah).ToObject<ObservableCollection<MasterWilayahGudangWpf>>();
            _vm.WilayahList ??= new();
            await Task.FromResult(_isTest);
        }
    }
}
