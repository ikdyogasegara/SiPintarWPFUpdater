using System;
using System.Diagnostics.CodeAnalysis;
using SiPintar.State.Navigators;

namespace SiPintar.ViewModels.Loket.Factories
{
    [ExcludeFromCodeCoverage]
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly CreateViewModel<TagihanViewModel> _createTagihanViewModel;
        private readonly CreateViewModel<AngsuranViewModel> _createAngsuranViewModel;
        private readonly CreateViewModel<LaporanViewModel> _createLaporanViewModel;
        private readonly CreateViewModel<TutupLoketViewModel> _createTutupLoketViewModel;
        private readonly CreateViewModel<SetoranViewModel> _createSetoranViewModel;
        private readonly CreateViewModel<BantuanViewModel> _createBantuanViewModel;

        public ViewModelFactory(
            CreateViewModel<TagihanViewModel> createTagihanViewModel,
            CreateViewModel<AngsuranViewModel> createAngsuranViewModel,
            CreateViewModel<LaporanViewModel> createLaporanViewModel,
            CreateViewModel<TutupLoketViewModel> createTutupLoketViewModel,
            CreateViewModel<SetoranViewModel> createSetoranViewModel,
            CreateViewModel<BantuanViewModel> createBantuanViewModel)
        {
            _createTagihanViewModel = createTagihanViewModel;
            _createAngsuranViewModel = createAngsuranViewModel;
            _createLaporanViewModel = createLaporanViewModel;
            _createTutupLoketViewModel = createTutupLoketViewModel;
            _createSetoranViewModel = createSetoranViewModel;
            _createBantuanViewModel = createBantuanViewModel;
        }

        public ViewModelBase CreateViewModel(LoketViewType viewType)
        {

            return viewType switch
            {
                LoketViewType.Tagihan => _createTagihanViewModel(),
                LoketViewType.Angsuran => _createAngsuranViewModel(),
                LoketViewType.Laporan => _createLaporanViewModel(),
                LoketViewType.TutupLoket => _createTutupLoketViewModel(),
                LoketViewType.Setoran => _createSetoranViewModel(),
                LoketViewType.Bantuan => _createBantuanViewModel(),

                _ => throw new ArgumentException("The ViewType does not have a ViewModel.", "viewType")
            };
        }
    }
}
