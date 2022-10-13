using SiPintar.State.Navigators;

namespace SiPintar.ViewModels.Perencanaan.Factories
{
    public interface IViewModelFactory
    {
        ViewModelBase CreateViewModel(PerencanaanViewType viewType);

    }
}
