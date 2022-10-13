using System.Windows.Input;
using SiPintar.Commands.Personalia.Potongan.PengecualianPotongan;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Personalia.Potongan
{
    public class PengecualianPotonganViewModel : ViewModelBase
    {
        public PengecualianPotonganViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi);
        }

        public ICommand OnLoadCommand { get; }

    }
}
