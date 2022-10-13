using System.Windows.Input;
using SiPintar.Commands.Personalia.Lainnya.StatusKeluargaPegawai;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Personalia.Lainnya
{
    public class StatusKeluargaPegawaiViewModel : ViewModelBase
    {
        public StatusKeluargaPegawaiViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi);
        }

        public ICommand OnLoadCommand { get; }

    }
}
