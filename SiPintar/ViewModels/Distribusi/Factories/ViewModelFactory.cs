using System;
using System.Diagnostics.CodeAnalysis;
using SiPintar.State.Navigators;

namespace SiPintar.ViewModels.Distribusi.Factories
{
    [ExcludeFromCodeCoverage]
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly CreateViewModel<DistribusiViewModel> _createDistribusiViewModel;
        private readonly CreateViewModel<NotifikasiViewModel> _createNotifikasiViewModel;
        private readonly CreateViewModel<AtributViewModel> _createAtributViewModel;
        private readonly CreateViewModel<BantuanViewModel> _createBantuanViewModel;

        public ViewModelFactory(
            CreateViewModel<DistribusiViewModel> createDistribusiViewModel,
            CreateViewModel<NotifikasiViewModel> createNotifikasiViewModel,
            CreateViewModel<AtributViewModel> createAtributViewModel,
            CreateViewModel<BantuanViewModel> createBantuanViewModel)
        {
            _createDistribusiViewModel = createDistribusiViewModel;
            _createNotifikasiViewModel = createNotifikasiViewModel;
            _createAtributViewModel = createAtributViewModel;
            _createBantuanViewModel = createBantuanViewModel;
        }

        public ViewModelBase CreateViewModel(DistribusiViewType viewType)
        {

            return viewType switch
            {
                DistribusiViewType.Distribusi => _createDistribusiViewModel(),
                DistribusiViewType.Notifikasi => _createNotifikasiViewModel(),
                DistribusiViewType.Atribut => _createAtributViewModel(),
                DistribusiViewType.Bantuan => _createBantuanViewModel(),

                _ => throw new ArgumentException("The ViewType does not have a ViewModel.", "viewType")
            };
        }
    }
}
