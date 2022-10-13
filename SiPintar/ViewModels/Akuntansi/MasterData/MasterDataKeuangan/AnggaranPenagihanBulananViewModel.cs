using System.Windows.Input;
using SiPintar.Commands.Akuntansi.MasterData.MasterDataKeuangan.AnggaranPenagihanBulanan;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Akuntansi.MasterData.MasterDataKeuangan
{
    public class AnggaranPenagihanBulananViewModel : VmBase
    {
        public AnggaranPenagihanBulananViewModel(MasterDataKeuanganViewModel parentViewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this, isTest);
        }
    }
}
