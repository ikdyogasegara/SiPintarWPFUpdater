using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.ViewModels.Akuntansi.PostingKeuangan;

namespace SiPintar.Commands.Akuntansi.PostingKeuangan.PengeluaranLainnya
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly PengeluaranLainnyaViewModel _viewModel;

        public OnResetFilterCommand(PengeluaranLainnyaViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsTanggalInputChecked = false;
            _viewModel.IsUraianChecked = false;
            _viewModel.IsNoTransChecked = false;
            _viewModel.IsWilayahChecked = false;
            _viewModel.IsPerkiraanDebetChecked = false;
            _viewModel.IsPerkiraanKreditChecked = false;

            _viewModel.OnLoadCommand.Execute(null);

            await Task.FromResult(true);
        }
    }
}
