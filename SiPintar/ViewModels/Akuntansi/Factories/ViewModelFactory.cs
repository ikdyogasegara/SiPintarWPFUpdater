using System;
using System.Diagnostics.CodeAnalysis;
using SiPintar.State.Navigators;

namespace SiPintar.ViewModels.Akuntansi.Factories
{
    [ExcludeFromCodeCoverage]
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly CreateViewModel<VoucherViewModel> _createVoucherViewModel;
        private readonly CreateViewModel<PostingAkuntansiViewModel> _createPostingAkuntansiViewModel;
        private readonly CreateViewModel<PostingKeuanganViewModel> _createPostingKeuanganViewModel;
        private readonly CreateViewModel<JurnalViewModel> _createJurnalViewModel;
        private readonly CreateViewModel<PenyusutanViewModel> _createPenyusutanViewModel;
        private readonly CreateViewModel<MasterDataViewModel> _createMasterDataViewModel;
        private readonly CreateViewModel<BantuanViewModel> _createBantuanViewModel;
        private readonly CreateViewModel<PengaturanViewModel> _createPengaturanViewModel;

        public ViewModelFactory(
            CreateViewModel<VoucherViewModel> createVoucherViewModel,
            CreateViewModel<PostingAkuntansiViewModel> createPostingAkuntansiViewModel,
            CreateViewModel<PostingKeuanganViewModel> createPostingKeuanganViewModel,
            CreateViewModel<JurnalViewModel> createJurnalViewModel,
            CreateViewModel<PenyusutanViewModel> createPenyusutanViewModel,
            CreateViewModel<MasterDataViewModel> createMasterDataViewModel,
            CreateViewModel<BantuanViewModel> createBantuanViewModel,
            CreateViewModel<PengaturanViewModel> createPengaturanViewModel)
        {
            _createVoucherViewModel = createVoucherViewModel;
            _createPostingAkuntansiViewModel = createPostingAkuntansiViewModel;
            _createPostingKeuanganViewModel = createPostingKeuanganViewModel;
            _createJurnalViewModel = createJurnalViewModel;
            _createPenyusutanViewModel = createPenyusutanViewModel;
            _createMasterDataViewModel = createMasterDataViewModel;
            _createBantuanViewModel = createBantuanViewModel;
            _createPengaturanViewModel = createPengaturanViewModel;
        }

        public ViewModelBase CreateViewModel(AkuntansiViewType viewType)
        {

            return viewType switch
            {
                AkuntansiViewType.Voucher => _createVoucherViewModel(),
                AkuntansiViewType.PostingAkuntansi => _createPostingAkuntansiViewModel(),
                AkuntansiViewType.PostingKeuangan => _createPostingKeuanganViewModel(),
                AkuntansiViewType.Jurnal => _createJurnalViewModel(),
                AkuntansiViewType.Penyusutan => _createPenyusutanViewModel(),
                AkuntansiViewType.MasterData => _createMasterDataViewModel(),
                AkuntansiViewType.Bantuan => _createBantuanViewModel(),
                AkuntansiViewType.Pengaturan => _createPengaturanViewModel(),

                _ => throw new ArgumentException("The ViewType does not have a ViewModel.", "viewType")
            };
        }
    }
}
