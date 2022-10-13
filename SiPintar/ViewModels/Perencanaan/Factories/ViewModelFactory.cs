using System;
using System.Diagnostics.CodeAnalysis;
using SiPintar.State.Navigators;

namespace SiPintar.ViewModels.Perencanaan.Factories
{
    [ExcludeFromCodeCoverage]
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly CreateViewModel<PerencanaanViewModel> _createPerencanaanViewModel;
        private readonly CreateViewModel<AtributViewModel> _createAtributViewModel;
        private readonly CreateViewModel<BantuanViewModel> _createBantuanViewModel;

        public ViewModelFactory(
            CreateViewModel<PerencanaanViewModel> createPerencanaanViewModel,
            CreateViewModel<AtributViewModel> createAtributViewModel,
            CreateViewModel<BantuanViewModel> createBantuanViewModel)
        {
            _createPerencanaanViewModel = createPerencanaanViewModel;
            _createAtributViewModel = createAtributViewModel;
            _createBantuanViewModel = createBantuanViewModel;
        }

        public ViewModelBase CreateViewModel(PerencanaanViewType viewType)
        {

            return viewType switch
            {
                PerencanaanViewType.Perencanaan => _createPerencanaanViewModel(),
                PerencanaanViewType.Atribut => _createAtributViewModel(),
                PerencanaanViewType.Bantuan => _createBantuanViewModel(),

                _ => throw new ArgumentException("The ViewType does not have a ViewModel.", "viewType")
            };
        }
    }
}
