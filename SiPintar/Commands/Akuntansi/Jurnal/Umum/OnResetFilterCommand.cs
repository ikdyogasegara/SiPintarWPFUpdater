using System.Threading.Tasks;
using SiPintar.ViewModels.Akuntansi.Jurnal;

namespace SiPintar.Commands.Akuntansi.Jurnal.Umum
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly UmumViewModel _viewModel;

        public OnResetFilterCommand(UmumViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsNomorBuktiChecked = false;
            _viewModel.IsUraianChecked = false;
            _viewModel.IsNilaiVoucherChecked = false;
            _viewModel.IsTanggalTransChecked = false;
            _viewModel.IsTanggalKoreksiChecked = false;

            _viewModel.OnLoadCommand.Execute(null);

            await Task.FromResult(true);
        }
    }
}
