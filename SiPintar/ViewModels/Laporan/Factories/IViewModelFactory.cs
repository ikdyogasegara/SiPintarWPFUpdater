using SiPintar.State.Navigators;

namespace SiPintar.ViewModels.Laporan.Factories
{
    public interface IViewModelFactory
    {
        ViewModelBase CreateViewModel(LaporanViewType viewType);
    }
}
