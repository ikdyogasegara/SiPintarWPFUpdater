using System.Threading.Tasks;
using SiPintar.ViewModels.Akuntansi.Voucher;

namespace SiPintar.Commands.Akuntansi.Voucher.IsiEdit
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly IsiEditViewModel _viewModel;

        public OnResetFilterCommand(IsiEditViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsNomorBuktiChecked = false;
            _viewModel.IsUraianChecked = false;
            _viewModel.IsNilaiVoucherChecked = false;
            _viewModel.IsDibayarkanKepadaChecked = false;
            _viewModel.IsTanggalTransChecked = false;
            _viewModel.IsTanggalKoreksiChecked = false;

            _viewModel.OnLoadCommand.Execute(null);

            await Task.FromResult(true);
        }
    }
}
