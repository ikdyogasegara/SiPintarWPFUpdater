using SiPintar.Commands.Gudang.MasterData.BarangHabis;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Gudang.MasterData
{
    public class BarangHabisViewModel : VmBase
    {
        public BarangHabisViewModel(MasterDataViewModel _parent, IRestApiClientModel _restApi, bool _isTest = false)
        {
            _ = _parent;
            OnLoadCommand = new OnLoadCommand(this, _restApi, _isTest);
        }
    }
}
