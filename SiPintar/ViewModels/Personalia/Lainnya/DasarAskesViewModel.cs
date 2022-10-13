using System.Windows.Input;
using SiPintar.Commands.Personalia.Lainnya.DasarAskes;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Personalia.Lainnya
{
    public class DasarAskesViewModel : ViewModelBase
    {
        public DasarAskesViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi);
        }

        public ICommand OnLoadCommand { get; }

    }
}
