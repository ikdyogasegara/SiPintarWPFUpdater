using System;
using System.Diagnostics.CodeAnalysis;
using SiPintar.State.Navigators;

namespace SiPintar.ViewModels.Billing.Factories
{
    [ExcludeFromCodeCoverage]
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly CreateViewModel<SupervisiViewModel> _createSupervisiViewModel;
        private readonly CreateViewModel<AtributViewModel> _createAtributViewModel;
        private readonly CreateViewModel<ProduktivitasViewModel> _createProduktivitasViewModel;
        private readonly CreateViewModel<BantuanViewModel> _createBantuanViewModel;

        public ViewModelFactory(
            CreateViewModel<SupervisiViewModel> createSupervisiViewModel,
            CreateViewModel<AtributViewModel> createAtributViewModel,
            CreateViewModel<ProduktivitasViewModel> createProduktivitasViewModel,
            CreateViewModel<BantuanViewModel> createBantuanViewModel)
        {
            _createSupervisiViewModel = createSupervisiViewModel;
            _createAtributViewModel = createAtributViewModel;
            _createProduktivitasViewModel = createProduktivitasViewModel;
            _createBantuanViewModel = createBantuanViewModel;
        }

        public ViewModelBase CreateViewModel(BillingViewType viewType)
        {
            return viewType switch
            {
                BillingViewType.Supervisi => _createSupervisiViewModel(),
                BillingViewType.Atribut => _createAtributViewModel(),
                BillingViewType.Produktivitas => _createProduktivitasViewModel(),
                BillingViewType.Bantuan => _createBantuanViewModel(),

                _ => throw new ArgumentException("The ViewType does not have a ViewModel.", nameof(viewType))
            };
        }
    }
}
