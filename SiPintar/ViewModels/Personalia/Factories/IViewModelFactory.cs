using SiPintar.State.Navigators;

namespace SiPintar.ViewModels.Personalia.Factories
{
    public interface IViewModelFactory
    {
        ViewModelBase CreateViewModel(PersonaliaViewType viewType);

    }
}
