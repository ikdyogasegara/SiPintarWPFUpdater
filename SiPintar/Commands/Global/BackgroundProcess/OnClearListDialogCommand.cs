using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using SiPintar.Helpers;

namespace SiPintar.Commands.Global.BackgroundProcess
{
    [ExcludeFromCodeCoverage]
    public class OnClearListDialogCommand : AsyncCommandBase
    {
        private readonly dynamic _viewModel;

        public OnClearListDialogCommand(dynamic ViewModel)
        {
            _viewModel = ViewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            App.ClearAllBackgroundProcess();
            var data = App.GetBackgroundProcessList();

            _viewModel.BgProcessList = data;
            _viewModel.IsEmptyProcess = _viewModel.BgProcessList == null || _viewModel.BgProcessList.Count == 0;

            if (!_viewModel.IsEmptyProcess)
            {
                var check = ((List<BackgroundProcessHelper.ProcessObject>)_viewModel.BgProcessList).FirstOrDefault(i => i.Status == 0);
                _viewModel.IsProcessRunning = check != null;
            }

            await Task.FromResult(true);
        }
    }
}
