using System.Windows.Input;
using SiPintar.Commands.Personalia.Kepegawaian.PangkatPilihan;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Personalia.Kepegawaian
{
    public class PangkatPilihanViewModel : ViewModelBase
    {
        public PangkatPilihanViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi);
        }

        public ICommand OnLoadCommand { get; }

    }
}
