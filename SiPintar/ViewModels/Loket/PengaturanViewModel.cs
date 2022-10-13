using System.Windows.Input;
using SiPintar.Commands.Loket.Pengaturan;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Loket
{
    public class PengaturanViewModel : ViewModelBase
    {
        public PengaturanViewModel(IRestApiClientModel restApi)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi);
        }

        public ICommand OnLoadCommand { get; }
    }
}
