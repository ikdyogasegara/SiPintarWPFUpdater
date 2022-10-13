using System.Threading.Tasks;
using SiPintar.ViewModels.Akuntansi.Voucher;

namespace SiPintar.Commands.Akuntansi.Voucher.PembayaranPembatalan
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly PembayaranPembatalanViewModel _viewModel;

        public OnResetFilterCommand(PembayaranPembatalanViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsNomorBuktiChecked = false;
            _viewModel.IsUraianChecked = false;
            _viewModel.IsNilaiVoucherChecked = false;
            _viewModel.IsTanggalTransChecked = false;
            _viewModel.IsTanggalBayarChecked = false;

            _viewModel.OnLoadCommand.Execute(null);

            await Task.FromResult(true);
        }
    }
}
