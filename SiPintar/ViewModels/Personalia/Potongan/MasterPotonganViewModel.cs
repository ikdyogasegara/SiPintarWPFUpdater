using System.Windows.Input;
using SiPintar.Commands.Personalia.Potongan.MasterPotongan;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Personalia.Potongan
{
    public class MasterPotonganViewModel : ViewModelBase
    {
        public MasterPotonganViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi);
        }

        public ICommand OnLoadCommand { get; }

    }
}
