using System.Windows.Input;
using SiPintar.Commands.Personalia.SupervisiGaji.Laporan;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Personalia.SupervisiGaji
{
    public class LaporanViewModel : ViewModelBase
    {
        public LaporanViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi);
        }

        public ICommand OnLoadCommand { get; }

    }
}
