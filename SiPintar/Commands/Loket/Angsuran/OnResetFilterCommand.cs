using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.ViewModels.Loket;
using SiPintar.ViewModels.Loket.Angsuran;

namespace SiPintar.Commands.Loket.Angsuran
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly AngsuranViewModel _viewModel;
        private readonly bool _isTest;

        public OnResetFilterCommand(AngsuranViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsJenisPelangganChecked = false;
            _viewModel.IsKategoriChecked = false;
            _viewModel.IsStatusBayarChecked = false;
            _viewModel.IsNoSambChecked = false;
            _viewModel.IsNamaChecked = false;
            _viewModel.IsStatusPublishChecked = false;
            _viewModel.IsPetugasChecked = false;

            LoadCommand();

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void LoadCommand()
        {
            if (_viewModel.PageViewModel == null || _isTest)
                return;

            if (_viewModel.PageViewModel is AngsuranTunggakanViewModel tunggakan)
                tunggakan.OnLoadCommand.Execute(null);
            else if (_viewModel.PageViewModel is AngsuranSambunganBaruViewModel sambungan)
                sambungan.OnLoadCommand.Execute(null);
            else if (_viewModel.PageViewModel is AngsuranNonAirViewModel nonair)
                nonair.OnLoadCommand.Execute(null);
        }
    }
}
