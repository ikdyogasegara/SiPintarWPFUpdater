using System.Threading.Tasks;
using SiPintar.ViewModels.Akuntansi.Penyusutan;

namespace SiPintar.Commands.Akuntansi.Penyusutan.DataAktiva
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly DataAktivaViewModel _viewModel;

        public OnResetFilterCommand(DataAktivaViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsKodeAktivaChecked = false;
            _viewModel.IsGolonganAktivaChecked = false;
            _viewModel.IsUraianChecked = false;
            _viewModel.IsNomorBarangChecked = false;
            _viewModel.IsTanggalPerolehChecked = false;
            _viewModel.IsGantiJadiChecked = false;
            _viewModel.IsSisaChecked = false;
            _viewModel.IsHabisChecked = false;
            _viewModel.IsAtdpChecked = false;
            _viewModel.IsAktivaChecked = false;
            _viewModel.IsAmortisasiChecked = false;

            _viewModel.OnLoadCommand.Execute(null);

            await Task.FromResult(true);
        }
    }
}
