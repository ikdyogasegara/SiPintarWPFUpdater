using System.Windows.Input;
using SiPintar.Commands.Personalia.Lainnya.DasarPensiun;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Personalia.Lainnya
{
    public class DasarPensiunViewModel : ViewModelBase
    {
        public DasarPensiunViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi);
        }

        public ICommand OnLoadCommand { get; }

    }
}
