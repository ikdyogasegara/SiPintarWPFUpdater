using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;

namespace SiPintar.Commands.Global.BackgroundProcess
{
    [ExcludeFromCodeCoverage]
    public class OnAddToBgProcessCommand : AsyncCommandBase
    {
        private readonly dynamic _viewModel;
        private readonly IRestApiClientModel _restApi;

        public OnAddToBgProcessCommand(dynamic ViewModel, IRestApiClientModel restApi)
        {
            _viewModel = ViewModel;
            _restApi = restApi;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var param = parameter as BackgroundProcessHelper.ProcessObject;

            App.AddToBackgroundProcess(param);
            App.BackgroundProcessRun(_restApi);

            _viewModel.IsProcessRunning = true;

            await Task.FromResult(true);
        }
    }
}
