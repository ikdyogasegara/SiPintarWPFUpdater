using SiPintar.State.Navigators;

namespace SiPintar.ViewModels.Loket.Factories
{
    public interface IViewModelFactory
    {
        ViewModelBase CreateViewModel(LoketViewType viewType);

    }
}
