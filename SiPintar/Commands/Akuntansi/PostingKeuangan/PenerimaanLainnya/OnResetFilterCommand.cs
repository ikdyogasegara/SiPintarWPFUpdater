using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.ViewModels.Akuntansi.PostingKeuangan;

namespace SiPintar.Commands.Akuntansi.PostingKeuangan.PenerimaanLainnya
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly PenerimaanLainnyaViewModel _viewModel;

        public OnResetFilterCommand(PenerimaanLainnyaViewModel viewModel)
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

            _viewModel.FilterTanggalInputAwal = string.Empty;
            _viewModel.FilterTanggalInputAkhir = string.Empty;
            _viewModel.FilterUraian = string.Empty;
            _viewModel.FilterNoTrans = string.Empty;
            _viewModel.FilterWilayah = null!;
            _viewModel.FilterPerkiraanDebet = null!;
            _viewModel.FilterPerkiraanKredit = null!;

            _viewModel.OnRefreshCommand.Execute(null);

            await Task.FromResult(true);
        }
    }
}
