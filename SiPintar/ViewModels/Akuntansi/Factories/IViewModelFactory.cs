using SiPintar.State.Navigators;

namespace SiPintar.ViewModels.Akuntansi.Factories
{
    public interface IViewModelFactory
    {
        ViewModelBase CreateViewModel(AkuntansiViewType viewType);

    }
}
