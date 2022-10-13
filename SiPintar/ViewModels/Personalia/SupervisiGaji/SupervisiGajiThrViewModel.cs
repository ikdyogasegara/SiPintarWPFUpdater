using System.Windows.Input;
using SiPintar.Commands.Personalia.SupervisiGaji.SupervisiGajiThr;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Personalia.SupervisiGaji
{
    public class SupervisiGajiThrViewModel : ViewModelBase
    {
        public SupervisiGajiThrViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi);
        }

        public ICommand OnLoadCommand { get; }

    }
}
