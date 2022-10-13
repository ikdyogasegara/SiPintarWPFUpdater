using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.ViewModels.Perencanaan.Atribut;

namespace SiPintar.Commands.Perencanaan.Atribut.RumusVolume
{
    [ExcludeFromCodeCoverage]
    public class OnRefreshCommand : AsyncCommandBase
    {
        private readonly RumusVolumeViewModel _viewModel;

        public OnRefreshCommand(RumusVolumeViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _ = Task.Run(() => { _viewModel.OnLoadCommand.Execute(null); });

            await Task.FromResult(true);
        }
    }
}
