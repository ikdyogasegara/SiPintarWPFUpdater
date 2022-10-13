using System;
using System.Diagnostics.CodeAnalysis;
using SiPintar.State.Navigators;


namespace SiPintar.ViewModels.Laporan.Factories
{
    [ExcludeFromCodeCoverage]
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly CreateViewModel<LaporanModuleViewModel> _createLaporanModuleViewModel;
        private readonly CreateViewModel<BantuanViewModel> _createBantuanViewModel;

        public ViewModelFactory(
            CreateViewModel<LaporanModuleViewModel> createLaporanModuleViewModel,
            CreateViewModel<BantuanViewModel> createBantuanViewModel)
        {
            _createLaporanModuleViewModel = createLaporanModuleViewModel;
            _createBantuanViewModel = createBantuanViewModel;
        }

        public ViewModelBase CreateViewModel(LaporanViewType viewType)
        {
            return viewType switch
            {
                LaporanViewType.Laporan => _createLaporanModuleViewModel(),
                LaporanViewType.Bantuan => _createBantuanViewModel(),
                _ => throw new ArgumentException("The ViewType does not have a ViewModel.", "viewType")
            };
        }
    }
}
