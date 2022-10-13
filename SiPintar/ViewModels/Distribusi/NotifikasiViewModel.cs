using SiPintar.Commands.Distribusi.Notifikasi;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Distribusi
{
    public class NotifikasiViewModel : VmBase
    {
        public NotifikasiViewModel(IRestApiClientModel restApi)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi);
        }

    }
}
