using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Global.BackgroundProcess
{
    [ExcludeFromCodeCoverage]
    public class OnOpenListDialogCommand : AsyncCommandBase
    {
        private readonly dynamic _viewModel;

        public OnOpenListDialogCommand(dynamic ViewModel)
        {
            _viewModel = ViewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var current = parameter as string;

            string ModalDialog = null;
            switch (current)
            {
                case "bacameter":
                    ModalDialog = "BacameterRootDialog";
                    break;
                case "billing":
                    ModalDialog = "BillingRootDialog";
                    break;
                case "hublang":
                    ModalDialog = "HublangRootDialog";
                    break;
                case "loket":
                    ModalDialog = "LoketRootDialog";
                    break;
                case "distribusi":
                    ModalDialog = "DistribusiRootDialog";
                    break;
                case "perencanaan":
                    ModalDialog = "PerencanaanRootDialog";
                    break;
                case "gudang":
                    ModalDialog = "GudangRootDialog";
                    break;
                case "personalia":
                    ModalDialog = "PersonaliaRootDialog";
                    break;
                case "akuntansi":
                    ModalDialog = "AkuntansiRootDialog";
                    break;
                case "keuangan":
                    ModalDialog = "KeuanganRootDialog";
                    break;
                case "laporan":
                    ModalDialog = "LaporanRootDialog";
                    break;
                default:
                    break;
            }

            SetData();

            if (DialogHost.IsDialogOpen(ModalDialog))
                DialogHost.Close(ModalDialog);

            _ = DialogHost.Show(new DialogBgProcess(_viewModel), ModalDialog);

            await Task.FromResult(true);
        }

        private void SetData()
        {
            var data = new List<BackgroundProcessHelper.ProcessObject>(App.GetBackgroundProcessList());
            data.Reverse();

            _viewModel.BgProcessList = data;
            _viewModel.IsEmptyProcess = _viewModel.BgProcessList == null || _viewModel.BgProcessList.Count == 0;

            if (!_viewModel.IsEmptyProcess)
            {
                var check = ((List<BackgroundProcessHelper.ProcessObject>)_viewModel.BgProcessList).FirstOrDefault(i => i.Status == 0);
                _viewModel.IsProcessRunning = check != null;
            }
        }
    }
}
