using System.Windows.Input;
using SiPintar.Commands.Bacameter.SistemKontrol.PengaturanUmum;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Bacameter.SistemKontrol
{
    public class PengaturanUmumViewModel : ViewModelBase
    {
        public PengaturanUmumViewModel(IRestApiClientModel restApi)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi);
        }

        public ICommand OnLoadCommand { get; }
    }
}
