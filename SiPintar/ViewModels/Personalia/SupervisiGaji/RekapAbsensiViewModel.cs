using System.Windows.Input;
using SiPintar.Commands.Personalia.SupervisiGaji.RekapAbsensi;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Personalia.SupervisiGaji
{
    public class RekapAbsensiViewModel : ViewModelBase
    {
        public RekapAbsensiViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi);
        }

        public ICommand OnLoadCommand { get; }

    }
}
