using SiPintar.State.Navigators;

namespace SiPintar.ViewModels.Gudang.Factories
{
    public interface IViewModelFactory
    {
        ViewModelBase CreateViewModel(GudangViewType viewType);

    }
}
