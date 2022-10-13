using System.Threading.Tasks;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Loket.Tagihan.TagihanKolektifAir;

namespace SiPintar.Commands.Loket.Tagihan.TagihanKolektifAir.RiwayatTransaksi
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly RiwayatTransaksiViewModel _viewModel;
        private readonly bool _isTest;

        public OnLoadCommand(RiwayatTransaksiViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.Parent.ListSelectedPelangganAir == null && _viewModel.Parent.ListSelectedNonAir == null)
                return;

            _viewModel.IsLoading = true;
            _viewModel.FilterKasir = null;
            _viewModel.FilterLoket = null;
            _viewModel.FilterTahun = null;
            _viewModel.FilterPeriode = null;
            _viewModel.FilterKolektif = null;

            _viewModel.IsEmptyAir = true;
            _viewModel.IsEmptyNonAir = true;

            _viewModel.IsTahunChecked = false;
            _viewModel.IsLoketChecked = false;
            _viewModel.IsKasirChecked = false;
            _viewModel.IsPeriodeChecked = false;
            _viewModel.IsKolektifChecked = false;

            _viewModel.KasirList = MasterListGlobal.MasterUser;
            _viewModel.LoketList = MasterListGlobal.MasterLoket;
            _viewModel.AlasanList = MasterListGlobal.MasterAlasanBatal;
            _viewModel.PeriodeList = MasterListGlobal.MasterPeriode;
            _viewModel.KolektifList = MasterListGlobal.MasterKolektif;

            if (_viewModel.Parent.ListSelectedPelangganAir != null)
            {
                _viewModel.IsEmptyAir = false;
            }

            if (_viewModel.Parent.ListSelectedNonAir != null)
            {
                _viewModel.IsEmptyNonAir = false;
            }

            _viewModel.IsLoading = false;
            _viewModel.FilterTipeTransaksi = _viewModel.TipeTransaksi[0];
            _viewModel.ParentPage.IsFullPage = true;
            await Task.FromResult(_isTest);
        }
    }
}
