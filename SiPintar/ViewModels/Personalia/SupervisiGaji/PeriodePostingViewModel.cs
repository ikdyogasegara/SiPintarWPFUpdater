using System.Windows.Input;
using SiPintar.Commands.Personalia.SupervisiGaji.PeriodePosting;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Personalia.SupervisiGaji
{
    public class PeriodePostingViewModel : ViewModelBase
    {
        public PeriodePostingViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi);
        }

        public ICommand OnLoadCommand { get; }

    }
}
