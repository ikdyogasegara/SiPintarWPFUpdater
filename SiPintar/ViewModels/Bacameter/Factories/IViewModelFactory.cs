using SiPintar.State.Navigators;

namespace SiPintar.ViewModels.Bacameter.Factories
{
    public interface IViewModelFactory
    {
        ViewModelBase CreateViewModel(BacameterViewType viewType);

    }
}
