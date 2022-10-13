using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.ViewModels.Billing;
using SiPintar.ViewModels.Billing.Produktivitas;

namespace SiPintar.Commands.Billing.Produktivitas
{
    [ExcludeFromCodeCoverage]
    public class OnFilterCommand : AsyncCommandBase
    {
        private readonly ProduktivitasViewModel _viewModel;
        private readonly bool _isTest;

        public OnFilterCommand(ProduktivitasViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.PageViewModel is PetugasViewModel petugas)
                petugas.OnLoadCommand.Execute(null);
            else if (_viewModel.PageViewModel is PembacaanPerTanggalViewModel pembacaan)
                pembacaan.OnLoadCommand.Execute(null);
            else if (_viewModel.PageViewModel is ProgressPembacaanViewModel progress)
                progress.OnLoadCommand.Execute(null);
            else if (_viewModel.PageViewModel is PelangganViewModel pelanggan)
                pelanggan.OnLoadCommand.Execute(null);
            else if (_viewModel.PageViewModel is PetugasBacaPerHariViewModel petugashari)
                petugashari.OnLoadCommand.Execute(null);

            await Task.FromResult(_isTest);
        }
    }
}
