using System.Windows.Input;
using SiPintar.Commands.Personalia.Potongan.PotonganBulanan;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Personalia.Potongan
{
    public class PotonganBulananViewModel : ViewModelBase
    {
        public PotonganBulananViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi);
        }

        public ICommand OnLoadCommand { get; }

    }
}
