using System.Windows.Input;
using SiPintar.Commands.Akuntansi.Pengaturan.BackupDatabase;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Akuntansi.Pengaturan
{
    public class BackupDatabaseViewModel : ViewModelBase
    {
        public BackupDatabaseViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
        }

        public ICommand OnLoadCommand { get; }
    }
}
