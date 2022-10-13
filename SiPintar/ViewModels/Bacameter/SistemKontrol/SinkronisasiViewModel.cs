using System.Windows.Input;
using SiPintar.Commands.Bacameter.SistemKontrol.Sinkronisasi;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Bacameter.SistemKontrol
{
    public class SinkronisasiViewModel : ViewModelBase
    {
        public SinkronisasiViewModel(IRestApiClientModel restApi)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi);
        }

        public ICommand OnLoadCommand { get; }
    }
}
