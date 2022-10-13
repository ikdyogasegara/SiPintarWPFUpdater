using System.Threading.Tasks;
using SiPintar.ViewModels.Loket.Tagihan.TagihanKolektifAir;

namespace SiPintar.Commands.Loket.Tagihan.TagihanKolektifAir.RiwayatTransaksi
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly RiwayatTransaksiViewModel _viewModel;
        private readonly bool _isTest;

        public OnResetFilterCommand(RiwayatTransaksiViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.FilterKasir = null;
            _viewModel.FilterLoket = null;
            _viewModel.FilterTahun = null;
            _viewModel.FilterPeriode = null;

            _viewModel.IsTahunChecked = false;
            _viewModel.IsLoketChecked = false;
            _viewModel.IsKasirChecked = false;
            _viewModel.IsPeriodeChecked = false;

            await Task.FromResult(_isTest);
        }
    }
}
