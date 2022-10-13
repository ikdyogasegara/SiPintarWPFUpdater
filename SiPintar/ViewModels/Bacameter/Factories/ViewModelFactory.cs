using System;
using System.Diagnostics.CodeAnalysis;
using SiPintar.State.Navigators;

namespace SiPintar.ViewModels.Bacameter.Factories
{
    [ExcludeFromCodeCoverage]
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly CreateViewModel<SupervisiViewModel> _createSupervisiViewModel;
        private readonly CreateViewModel<ProduktivitasViewModel> _createProduktivitasViewModel;
        private readonly CreateViewModel<PemetaanPelangganViewModel> _createPemetaanPelangganViewModel;
        private readonly CreateViewModel<SistemKontrolViewModel> _createSistemKontrolViewModel;
        private readonly CreateViewModel<BantuanViewModel> _createBantuanViewModel;

        public ViewModelFactory(
            CreateViewModel<SupervisiViewModel> createSupervisiViewModel,
            CreateViewModel<ProduktivitasViewModel> createProduktivitasViewModel,
            CreateViewModel<PemetaanPelangganViewModel> createPemetaanPelangganViewModel,
            CreateViewModel<SistemKontrolViewModel> createSistemKontrolViewModel,
            CreateViewModel<BantuanViewModel> createBantuanViewModel)
        {
            _createSupervisiViewModel = createSupervisiViewModel;
            _createProduktivitasViewModel = createProduktivitasViewModel;
            _createPemetaanPelangganViewModel = createPemetaanPelangganViewModel;
            _createSistemKontrolViewModel = createSistemKontrolViewModel;
            _createBantuanViewModel = createBantuanViewModel;
        }

        public ViewModelBase CreateViewModel(BacameterViewType viewType)
        {

            return viewType switch
            {
                BacameterViewType.Supervisi => _createSupervisiViewModel(),
                BacameterViewType.Produktivitas => _createProduktivitasViewModel(),
                BacameterViewType.PemetaanPelanggan => _createPemetaanPelangganViewModel(),
                BacameterViewType.SistemKontrol => _createSistemKontrolViewModel(),
                BacameterViewType.Bantuan => _createBantuanViewModel(),

                _ => throw new ArgumentException("The ViewType does not have a ViewModel.", "viewType")
            };
        }
    }
}
