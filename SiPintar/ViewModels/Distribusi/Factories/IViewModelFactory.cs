using SiPintar.State.Navigators;

namespace SiPintar.ViewModels.Distribusi.Factories
{
    public interface IViewModelFactory
    {
        ViewModelBase CreateViewModel(DistribusiViewType viewType);

    }
}
