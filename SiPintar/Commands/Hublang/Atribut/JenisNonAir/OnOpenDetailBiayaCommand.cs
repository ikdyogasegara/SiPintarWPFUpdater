using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Hublang.Atribut;
using SiPintar.Views.Global.Dialog;
using SiPintar.Views.Hublang.Atribut.JenisNonAir;

namespace SiPintar.Commands.Hublang.Atribut.JenisNonAir
{
    public class OnOpenDetailBiayaCommand : AsyncCommandBase
    {
        private readonly JenisNonAirViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDetailBiayaCommand(JenisNonAirViewModel viewModel, bool isTest = false)
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
                else await DialogHost.Show(new DialogGlobalView("Detail Biaya Jenis Non-Air", "Silahkan pilih data", "warning"), "HublangRootDialog");
            }
            await Task.FromResult(_isTest);
        }
    }
}
