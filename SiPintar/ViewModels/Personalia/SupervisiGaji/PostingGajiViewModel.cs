using System.Windows.Input;
using SiPintar.Commands.Personalia.SupervisiGaji.PostingGaji;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Personalia.SupervisiGaji
{
    public class PostingGajiViewModel : ViewModelBase
    {
        public PostingGajiViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi);
        }

        public ICommand OnLoadCommand { get; }

    }
}
