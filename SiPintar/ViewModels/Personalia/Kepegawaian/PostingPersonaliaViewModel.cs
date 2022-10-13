using System.Windows.Input;
using SiPintar.Commands.Personalia.Kepegawaian.PostingPersonalia;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Personalia.Kepegawaian
{
    public class PostingPersonaliaViewModel : ViewModelBase
    {
        public PostingPersonaliaViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi);
        }

        public ICommand OnLoadCommand { get; }

    }
}
