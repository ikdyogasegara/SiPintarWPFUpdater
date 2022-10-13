using System;
using System.Diagnostics.CodeAnalysis;
using SiPintar.State.Navigators;

namespace SiPintar.ViewModels.Hublang.Factories
{
    [ExcludeFromCodeCoverage]
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly CreateViewModel<PelayananViewModel> _createMenu1ViewModel;
        private readonly CreateViewModel<VerifikasiViewModel> _createMenu2ViewModel;
        private readonly CreateViewModel<AtributViewModel> _createMenu3ViewModel;
        private readonly CreateViewModel<BantuanViewModel> _createBantuanViewModel;

        public ViewModelFactory(
            CreateViewModel<PelayananViewModel> createMenu1ViewModel,
            CreateViewModel<VerifikasiViewModel> createMenu2ViewModel,
            CreateViewModel<AtributViewModel> createMenu3ViewModel,
            CreateViewModel<BantuanViewModel> createBantuanViewModel)
        {
            _createMenu1ViewModel = createMenu1ViewModel;
            _createMenu2ViewModel = createMenu2ViewModel;
            _createMenu3ViewModel = createMenu3ViewModel;
            _createBantuanViewModel = createBantuanViewModel;
        }

        public ViewModelBase CreateViewModel(HublangViewType viewType)
        {

            return viewType switch
            {
                HublangViewType.Pelayanan => _createMenu1ViewModel(),
                HublangViewType.Verifikasi => _createMenu2ViewModel(),
                HublangViewType.Atribut => _createMenu3ViewModel(),
                HublangViewType.Bantuan => _createBantuanViewModel(),

                _ => throw new ArgumentException("The ViewType does not have a ViewModel.", "viewType")
            };
        }
    }
}
