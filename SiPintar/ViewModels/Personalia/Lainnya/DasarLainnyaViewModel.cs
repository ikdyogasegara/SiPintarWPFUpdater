using System.Windows.Input;
using SiPintar.Commands.Personalia.Lainnya.DasarLainnya;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Personalia.Lainnya
{
    public class DasarLainnyaViewModel : ViewModelBase
    {
        public DasarLainnyaViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi);
        }

        public ICommand OnLoadCommand { get; }

    }
}
