using System.Collections.Generic;
using System.Windows.Input;
using SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.RekapAnggaranPerputaranUang;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi
{
    public class RekapAnggaranPerputaranUangViewModel : VmBase
    {
        public RekapAnggaranPerputaranUangViewModel(MasterDataAkuntansiViewModel parentViewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
            OnRefreshCommand = new OnRefreshCommand(this, restApi, isTest);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this, isTest);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this, isTest);
        }

    }
}
