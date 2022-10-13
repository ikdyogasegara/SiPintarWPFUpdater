using SiPintar.State.Navigators;

namespace SiPintar.ViewModels.Billing.Factories
{
    public interface IViewModelFactory
    {
        ViewModelBase CreateViewModel(BillingViewType viewType);

    }
}
