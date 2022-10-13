using System.Windows.Input;
using SiPintar.Commands.Personalia.Lainnya.DasarAstek;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Personalia.Lainnya
{
    public class DasarAstekViewModel : ViewModelBase
    {
        public DasarAstekViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi);
        }

        public ICommand OnLoadCommand { get; }

    }
}
