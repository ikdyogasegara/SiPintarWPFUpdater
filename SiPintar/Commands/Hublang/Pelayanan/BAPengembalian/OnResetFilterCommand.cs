using System.Threading.Tasks;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Commands.Hublang.Pelayanan.BAPengembalian
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly BaPengembalianViewModel _viewModel;
        private readonly bool _isTest;

        public OnResetFilterCommand(BaPengembalianViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsCabangChecked = false;
            _viewModel.IsNomorRegisterChecked = false;
            _viewModel.IsNoSambunganChecked = false;
            _viewModel.IsNamaChecked = false;
            _viewModel.IsAlamatChecked = false;
            _viewModel.IsWilayahChecked = false;
            _viewModel.IsTanggalInputChecked = false;
            _viewModel.IsUserInputChecked = false;
            _viewModel.IsUserVerifikasiChecked = false;

            _viewModel.FilterCabang = null;
            _viewModel.FilterNomorRegister = null;
            _viewModel.FilterNoSambungan = null;
            _viewModel.FilterNama = null;
            _viewModel.FilterAlamat = null;
            _viewModel.FilterWilayah = null;
            _viewModel.FilterTanggalInputAwal = null;
            _viewModel.FilterTanggalInputAkhir = null;
            _viewModel.FilterUserInput = null;
            _viewModel.FilterUserVerifikasi = null;

            await Task.FromResult(_isTest);
        }
    }
}
