using System.Windows.Input;
using SiPintar.Commands.Bacameter.SistemKontrol.PerawatanDatabase;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Bacameter.SistemKontrol
{
    public class PerawatanDatabaseViewModel : ViewModelBase
    {
        public PerawatanDatabaseViewModel(IRestApiClientModel restApi)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi);
        }

        public ICommand OnLoadCommand { get; }
    }
}
