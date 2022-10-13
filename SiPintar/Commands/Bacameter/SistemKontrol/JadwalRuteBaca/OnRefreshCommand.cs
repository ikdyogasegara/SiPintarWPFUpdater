using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.ViewModels.Bacameter.SistemKontrol;

namespace SiPintar.Commands.Bacameter.SistemKontrol.JadwalRuteBaca
{
    [ExcludeFromCodeCoverage]
    public class OnRefreshCommand : AsyncCommandBase
    {
        private readonly JadwalRuteBacaViewModel _viewModel;
        private readonly bool _isTest;

        public OnRefreshCommand(JadwalRuteBacaViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.LoadPageCommand();

            await Task.FromResult(_isTest);
        }
    }
}
