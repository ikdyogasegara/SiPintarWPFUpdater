using System.Windows.Input;
using SiPintar.Commands.Bacameter.SistemKontrol.SmsGateway;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Bacameter.SistemKontrol
{
    public class SmsGatewayViewModel : ViewModelBase
    {
        public SmsGatewayViewModel(IRestApiClientModel restApi)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi);
        }

        public ICommand OnLoadCommand { get; }
    }
}
