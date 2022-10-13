using System.Windows.Input;
using SiPintar.Commands.Akuntansi.Pengaturan.SinkronasiBsbs;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Akuntansi.Pengaturan
{
    public class SinkronasiBsbsViewModel : ViewModelBase
    {
        public SinkronasiBsbsViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
        }

        public ICommand OnLoadCommand { get; }
    }
}
