using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.ViewModels.Perencanaan.Atribut;

namespace SiPintar.Commands.Perencanaan.Atribut.RumusVolume
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly RumusVolumeViewModel _viewModel;
        private readonly bool _isTest;

        public OnResetFilterCommand(RumusVolumeViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.FilterData = new MasterRumusVolumeOngkosDto() { FlagHapus = false };

            _viewModel.IsFilterKodeOngkosChecked = false;
            _viewModel.IsFilterNamaOngkosChecked = false;

            ReloadData();

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private void ReloadData()
        {
            if (!_isTest)
                _viewModel.OnLoadCommand.Execute(null);
        }

    }
}
