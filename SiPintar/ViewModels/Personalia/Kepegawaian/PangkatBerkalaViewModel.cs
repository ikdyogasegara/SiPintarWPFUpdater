using System.Windows.Input;
using SiPintar.Commands.Personalia.Kepegawaian.PangkatBerkala;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Personalia.Kepegawaian
{
    public class PangkatBerkalaViewModel : ViewModelBase
    {
        public PangkatBerkalaViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi);
        }

        public ICommand OnLoadCommand { get; }

    }
}
