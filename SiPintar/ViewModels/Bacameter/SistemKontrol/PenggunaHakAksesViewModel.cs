using System.Windows.Input;
using SiPintar.Commands.Bacameter.SistemKontrol.PenggunaHakAkses;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Bacameter.SistemKontrol
{
    public class PenggunaHakAksesViewModel : ViewModelBase
    {
        public PenggunaHakAksesViewModel(IRestApiClientModel restApi)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi);
        }

        public ICommand OnLoadCommand { get; }
    }
}
