using SiPintar.State.Navigators;

namespace SiPintar.ViewModels.Hublang.Factories
{
    public interface IViewModelFactory
    {
        ViewModelBase CreateViewModel(HublangViewType viewType);

    }
}
