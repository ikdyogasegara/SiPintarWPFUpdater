using System;
using System.Diagnostics.CodeAnalysis;
using SiPintar.State.Navigators;

namespace SiPintar.ViewModels.Gudang.Factories
{
    [ExcludeFromCodeCoverage]
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly CreateViewModel<MasterDataViewModel> _createDataMasterViewModel;
        private readonly CreateViewModel<ProsesTransaksiViewModel> _createProsesTransaksi;
        private readonly CreateViewModel<PengolahanViewModel> _createPengolahanViewModel;
        private readonly CreateViewModel<BantuanViewModel> _createBantuanViewModel;

        public ViewModelFactory(
            CreateViewModel<MasterDataViewModel> createMenu1ViewModel,
            CreateViewModel<ProsesTransaksiViewModel> createMenu2ViewModel,
            CreateViewModel<PengolahanViewModel> createMenu3ViewModel,
            CreateViewModel<BantuanViewModel> createBantuanViewModel)
        {
            _createProsesTransaksi = createMenu2ViewModel;
            _createPengolahanViewModel = createMenu3ViewModel;
            _createDataMasterViewModel = createMenu1ViewModel;
            _createBantuanViewModel = createBantuanViewModel;
        }

        public ViewModelBase CreateViewModel(GudangViewType viewType)
        {

            return viewType switch
            {
                GudangViewType.ProsesTransaksi => _createProsesTransaksi(),
                GudangViewType.Pengolahan => _createPengolahanViewModel(),
                GudangViewType.MasterData => _createDataMasterViewModel(),
                GudangViewType.Bantuan => _createBantuanViewModel(),

                _ => throw new ArgumentException("The ViewType does not have a ViewModel.", "viewType")
            };
        }
    }
}
