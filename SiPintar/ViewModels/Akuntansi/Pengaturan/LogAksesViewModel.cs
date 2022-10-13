using System.Windows.Input;
using SiPintar.Commands.Akuntansi.Pengaturan.LogAkses;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Akuntansi.Pengaturan
{
    public class LogAksesViewModel : ViewModelBase
    {
        public LogAksesViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
        }

        public ICommand OnLoadCommand { get; }
    }
}
