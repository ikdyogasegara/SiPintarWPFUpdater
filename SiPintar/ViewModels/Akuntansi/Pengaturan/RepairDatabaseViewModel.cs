using System.Windows.Input;
using SiPintar.Commands.Akuntansi.Pengaturan.RepairDatabase;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Akuntansi.Pengaturan
{
    public class RepairDatabaseViewModel : ViewModelBase
    {
        public RepairDatabaseViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
        }

        public ICommand OnLoadCommand { get; }
    }
}
