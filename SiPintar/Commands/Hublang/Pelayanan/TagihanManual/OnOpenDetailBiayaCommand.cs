using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Hublang.Pelayanan;
using SiPintar.Views.Global.Dialog;
using SiPintar.Views.Hublang.Pelayanan.TagihanManual;

namespace SiPintar.Commands.Hublang.Pelayanan.TagihanManual
{
    public class OnOpenDetailBiayaCommand : AsyncCommandBase
    {
        private readonly TagihanManualViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDetailBiayaCommand(TagihanManualViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        [ExcludeFromCodeCoverage]
        public override async Task ExecuteAsync(object parameter)
        {
            if (!_isTest)
            {
                if (_viewModel.SelectedData != null) await DialogHost.Show(new DetailBiayaView(_viewModel), "HublangRootDialog");
                else await DialogHost.Show(new DialogGlobalView("Detail", "Silahkan pilih data", "warning"), "HublangRootDialog");
            }
            await Task.FromResult(_isTest);
        }
    }
}
