using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Perencanaan.Atribut;
using SiPintar.Views.Perencanaan.Atribut.RumusVolume;

namespace SiPintar.Commands.Perencanaan.Atribut.RumusVolume
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly RumusVolumeViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(RumusVolumeViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading)
                return;

            _viewModel.IsEdit = false;
            _viewModel.FormData = new MasterRumusVolumeOngkosDto();
            _viewModel.SelectedOngkos = null;
            _viewModel.IsBatas1Active = true;
            _viewModel.IsBatas2Active = false;
            _viewModel.IsBatas3Active = false;
            _viewModel.IsBatas4Active = false;
            _viewModel.IsBatas5Active = false;

            _viewModel.IsLoadingForm = true;

            ShowDialog();

            _viewModel.IsLoadingForm = false;

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest)
                _ = DialogHost.Show(new DialogFormView(_viewModel), "PerencanaanRootDialog");
        }
    }
}
