using System;
using System.Diagnostics.CodeAnalysis;
using SiPintar.State.Navigators;

namespace SiPintar.ViewModels.Personalia.Factories
{
    [ExcludeFromCodeCoverage]
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly CreateViewModel<DataMasterViewModel> _createDataMasterViewModel;
        private readonly CreateViewModel<KepegawaianViewModel> _createKepegawaianViewModel;
        private readonly CreateViewModel<GajiPokokViewModel> _createGajiPokokViewModel;
        private readonly CreateViewModel<TunjanganViewModel> _createTunjanganViewModel;
        private readonly CreateViewModel<PotonganViewModel> _createPotonganViewModel;
        private readonly CreateViewModel<LainnyaViewModel> _createLainnyaViewModel;
        private readonly CreateViewModel<SupervisiGajiViewModel> _createSupervisiGajiViewModel;
        private readonly CreateViewModel<BantuanViewModel> _createBantuanViewModel;

        public ViewModelFactory(
            CreateViewModel<DataMasterViewModel> createDataMasterViewModel,
            CreateViewModel<KepegawaianViewModel> createKepegawaianViewModel,
            CreateViewModel<GajiPokokViewModel> createGajiPokokViewModel,
            CreateViewModel<TunjanganViewModel> createTunjanganViewModel,
            CreateViewModel<PotonganViewModel> createPotonganViewModel,
            CreateViewModel<LainnyaViewModel> createLainnyaViewModel,
            CreateViewModel<SupervisiGajiViewModel> createSupervisiGajiViewModel,
            CreateViewModel<BantuanViewModel> createBantuanViewModel)
        {
            _createDataMasterViewModel = createDataMasterViewModel;
            _createKepegawaianViewModel = createKepegawaianViewModel;
            _createGajiPokokViewModel = createGajiPokokViewModel;
            _createTunjanganViewModel = createTunjanganViewModel;
            _createPotonganViewModel = createPotonganViewModel;
            _createLainnyaViewModel = createLainnyaViewModel;
            _createSupervisiGajiViewModel = createSupervisiGajiViewModel;
            _createBantuanViewModel = createBantuanViewModel;
        }

        public ViewModelBase CreateViewModel(PersonaliaViewType viewType)
        {

            return viewType switch
            {
                PersonaliaViewType.DataMaster => _createDataMasterViewModel(),
                PersonaliaViewType.Kepegawaian => _createKepegawaianViewModel(),
                PersonaliaViewType.GajiPokok => _createGajiPokokViewModel(),
                PersonaliaViewType.Tunjangan => _createTunjanganViewModel(),
                PersonaliaViewType.Potongan => _createPotonganViewModel(),
                PersonaliaViewType.Lainnya => _createLainnyaViewModel(),
                PersonaliaViewType.SupervisiGaji => _createSupervisiGajiViewModel(),
                PersonaliaViewType.Bantuan => _createBantuanViewModel(),

                _ => throw new ArgumentException("The ViewType does not have a ViewModel.", "viewType")
            };
        }
    }
}
