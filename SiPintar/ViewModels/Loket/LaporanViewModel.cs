using System.Windows.Input;
using SiPintar.Commands.Loket.Laporan;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Loket
{
    public class LaporanViewModel : ViewModelBase
    {
        public LaporanViewModel(IRestApiClientModel restApi)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi);
        }

        public ICommand OnLoadCommand { get; }
    }
}
